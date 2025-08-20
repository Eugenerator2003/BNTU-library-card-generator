using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Startup
{
    public class HtmlTemplateFilesPathProvider
    {
        public void SetTemplatesPathes(HtmlFilesPathProvider filesPathProvider)
        {
            filesPathProvider.CardPath = "templates/card.html";
            filesPathProvider.PaperPath = "templates/paper.html";
            filesPathProvider.DocumentPath = "templates/document.html";
        }
    }
}
