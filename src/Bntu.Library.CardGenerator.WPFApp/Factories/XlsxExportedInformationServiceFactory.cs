using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class XlsxExportedInformationServiceFactory : IFactory<XlsxExportedInformationService>
    {
        public XlsxExportedInformationService Create()
        {
            var pathProvider = XlsxFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(pathProvider.ExportedInformationSource))
                throw new ArgumentException("", "Не указан путь к xlsx-файлу с экспортированными данными студентов.");
            return new XlsxExportedInformationService(pathProvider.ExportedInformationSource);
        }
    }
}
