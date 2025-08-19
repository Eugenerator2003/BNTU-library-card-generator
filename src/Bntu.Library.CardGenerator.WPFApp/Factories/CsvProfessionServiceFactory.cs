using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class CsvProfessionServiceFactory : IFactory<CsvProfessionService>
    {
        public CsvProfessionService Create()
        {
            var pathProvider = CsvFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(pathProvider.ProfessionSource))
                throw new ArgumentNullException("", "Не указан путь к csv-файл со специальностями.");
            return new CsvProfessionService(pathProvider.ProfessionSource);
        }
    }
}
