using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class CsvSpecializationServiceFactory : IFactory<CsvSpecializationService>
    {
        public CsvSpecializationService Create()
        {
            var pathProvider = CsvFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(pathProvider.SpecializationSource))
                throw new ArgumentNullException("", "Не указан путь к csv-файл с направлениями специальностей.");
            return new CsvSpecializationService(pathProvider.SpecializationSource);
        }
    }
}
