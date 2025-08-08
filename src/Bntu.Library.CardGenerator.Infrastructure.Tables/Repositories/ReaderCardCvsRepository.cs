using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Domain.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bntu.Library.CardGenerator.Domain.ValueObjects;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public class ReaderCardCvsRepository : IReaderCardRepository
    {
        private static readonly string[] Fields =
        {
            "Штрих-код",
            "Дата записи в библиотеку",
            "Дата окончания обучения",
            "Фамилия",
            "Имя Отчество",
            "Факультет",
            "Примечания ( продолж.) (номер группы)",
            "Группа",
            "Фото"
        };

        private CsvReader csv = null!;
        private int fieldIndex;
        private readonly string filename;

        public ReaderCardCvsRepository(string filename)
        {
            this.filename = filename;
        }

        public IList<ReaderCard> GetAllCards()
        {
            return ReadFromCsv(a => true, false);
        }

        public ReaderCard? GetCardByCodbar(int codbar)
        {
            return ReadFromCsv(r => int.Parse(r.GetField(Fields[0]) ?? "") == codbar, true).FirstOrDefault();
        }

        public ReaderCard? GetCardByPid(int pid)
        {
            return GetCardByCodbar(pid);
        }

        public IList<ReaderCard> GetCardsByGroup(string groupNumber)
        {
            return ReadFromCsv(r => (r.GetField(Fields[0]) ?? "") == groupNumber, true);
        }

        private List<ReaderCard> ReadFromCsv(Predicate<CsvReader> rowFilter, bool only)
        {
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(filename);
            }
            catch (Exception ex)
            {
                throw new IOException($"Невозможно начать чтение файла с именем {filename}", ex);
            }
            SetCsvReader(reader);
            int index = 1;
            List<ReaderCard> cards = new List<ReaderCard>();
            try
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (!rowFilter(csv))
                        continue;
                    ReaderCard card = GetCard(rowFilter);
                    if (only)
                        break;
                    index++;
                }
            }
            catch (Exception ex)
            {
                csv.Dispose();
                reader.Dispose();

                throw new InvalidDataException($"Ошибка чтения данных в строке {index}. Текущее поле: {Fields[fieldIndex]}", ex);
            }
            return cards;
        }

        private void SetCsvReader(StreamReader reader)
        {
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true,
                BadDataFound = null
            });
        }

        private ReaderCard GetCard(Predicate<CsvReader> rowFilter)
        {
            ResetFieldIndex();
            var codbar = int.Parse(csv.GetField(Fields[++fieldIndex]) ?? "");
            var issueDate = DateTime.Parse(csv.GetField(Fields[++fieldIndex]) ?? "");
            var expirationDate = DateTime.Parse(csv.GetField(Fields[++fieldIndex]) ?? "");

            var lastname = csv.GetField(Fields[++fieldIndex]);
            if (string.IsNullOrEmpty(lastname))
                throw new Exception($"Фамилия не может быть пустой");

            var firstname = csv.GetField(Fields[++fieldIndex]);
            if (string.IsNullOrEmpty(firstname))
                throw new Exception($"Имя не может быть пустым");

            var facultyName = csv.GetField(Fields[++fieldIndex]);
            if (string.IsNullOrEmpty(facultyName))
                throw new Exception($"Факультет не может быть пустым");

            var group = csv.GetField(Fields[++fieldIndex]);
            if (string.IsNullOrEmpty(group))
                throw new Exception($"Факультет не может быть пустым");

            var departmentString = csv.GetField(Fields[++fieldIndex]);
            var department = departmentString == "Студенты ДО" ?
                                StudyGroup.DepartmentType.Fulltime :
                                StudyGroup.DepartmentType.Correspondence;

            var photoName = csv.GetField(Fields[++fieldIndex]);
            if (string.IsNullOrEmpty(photoName))
                throw new Exception($"Путь к фото не может быть пустым");

            CardInfo cardInfo = new CardInfo(codbar, codbar, issueDate, expirationDate);
            StudyGroup studyGroup = new StudyGroup(group, department);
            Faculty faculty = new Faculty(facultyName);
            Student student = new Student(lastname, firstname, faculty, studyGroup);
            Photo photo = new Photo(photoName);
            ReaderCard card = new ReaderCard(cardInfo, student, photo);
            return card;
        }

        private void ResetFieldIndex()
        {
            fieldIndex = -1;
        }
    }
}
