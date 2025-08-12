using Bntu.Library.CardGenerator.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public abstract class BaseCsvReadRepository<T>
    {
        public string Filename { get; }

        protected CsvReader CsvReader { get; private set; } = null!;

        private StreamReader reader =null!;
        private readonly CsvFields fields;

        public BaseCsvReadRepository(string filename)
        {
            Filename = filename;
            fields = GetCsvFields();
        }

        protected List<T> GetAllItems(Predicate<CsvReader> rowFilter, bool firstItem)
        {
            if (!OpenStream())
            {
                throw new IOException($"Невозможно начать чтение файла с именем {Filename}");
            }
            SetCsvReader();

            int index = 1;
            List<T> cards = new List<T>();
            try
            {
                CsvReader.Read();
                CsvReader.ReadHeader();
                while (CsvReader.Read())
                {
                    if (rowFilter(CsvReader))
                    {
                        T card = ReadFromCurrentLine();
                        cards.Add(card);
                        if (firstItem)
                            break;
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Ошибка чтения данных в строке {index}. Текущее поле: {CurrentField()}", ex);
            }
            finally
            {
                CsvReader.Dispose();
                reader.Dispose();
            }
            return cards;
        }

        protected abstract CsvFields GetCsvFields();

        protected abstract T ReadFromCurrentLine();

        protected bool OpenStream()
        {
            try
            {
                reader = new StreamReader(Filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void SetCsvReader()
        {
            CsvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                BadDataFound = null,
                Delimiter = ";"
            });
        }

        protected string NextField()
        {
            return fields.NextField();
        }

        protected string CurrentField()
        {
            return fields.CurrentField();
        }

        protected void Close()
        {
            CsvReader?.Dispose();
            reader?.Dispose();
        }
    }
}