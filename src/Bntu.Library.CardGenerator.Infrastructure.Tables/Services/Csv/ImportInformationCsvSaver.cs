using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.Util;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class ImportInformationCsvSaver : IImportInformationSaver
    {
        private string filename;

        public ImportInformationCsvSaver(string filename)
        {
            this.filename = filename;
        }

        public ApplicationResult<EmptyResult> Save(IList<ImportInformation> importInformation)
        {
            try
            {
                WriteToFile(importInformation);
                return ApplicationResult<EmptyResult>.Success(null, null);
            }
            catch (Exception ex)
            {
                return ApplicationResult<EmptyResult>.Error(ex.Message);
            }
        }

        private void WriteToFile(IList<ImportInformation> importInformation)
        {
            StreamWriter? stream = null;
            CsvWriter? writer = null;
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc = Encoding.GetEncoding(1251);
                stream = new StreamWriter(filename, false, enc);
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                };
                writer = new CsvWriter(stream, config);
            }
            catch
            {
                throw new IOException($"Невозможно открыть файл {filename} для записи");
            }

            writer.Context.RegisterClassMap<ImportInformationMap>();
            writer.WriteRecords(importInformation);

            writer?.Dispose();
            stream?.Dispose();
        }
    }
}
