using Bntu.Library.CardGenerator.Infrastructure.HtmlExport;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class HtmlGeneratedCardExporterFactory : IFactory<HtmlGeneratedCardExporter>
    {
        public HtmlGeneratedCardExporter Create()
        {
            var provider = HtmlFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(provider.RootFolder))
                throw new ArgumentNullException("", "Не указана корневая папка экспорта.");
            if (string.IsNullOrEmpty(provider.GroupFolder))
                throw new ArgumentNullException("", "Не указана папка для экспорта группы.");
            if (string.IsNullOrEmpty(provider.PhotoFolder))
                throw new ArgumentNullException("", "Не указана папка с фотографиями читателей.");
            var foldersContext = new ExportFoldersContext(provider.RootFolder, provider.Subfolder, provider.GroupFolder, provider.PhotoFolder);
            var templatesPathContext = new TemplatesPathContext(provider.DocumentPath!, provider.PaperPath!, provider.CardPath!);
            return new HtmlGeneratedCardExporter(foldersContext, templatesPathContext);
        }
    }
}
