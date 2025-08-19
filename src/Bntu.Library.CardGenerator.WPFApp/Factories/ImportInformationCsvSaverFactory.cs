using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class ImportInformationCsvSaverFactory : IFactory<ImportInformationCsvSaver>
    {
        public ImportInformationCsvSaver Create()
        {
            var pathProvider = CsvFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(pathProvider.ImportInformationDestination))
                throw new ArgumentException("", "Не указан путь для выгрузки данных студентов для импорта.");
            return new ImportInformationCsvSaver(pathProvider.ImportInformationDestination);
        }
    }
}
