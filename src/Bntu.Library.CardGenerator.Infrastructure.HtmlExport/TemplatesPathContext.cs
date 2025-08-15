using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport
{
    public class TemplatesPathContext
    {
        public string DocumentPath { get; }

        public string PaperPath { get; }

        public string CardPath { get; }

        public TemplatesPathContext(string documentPath, string paperPath, string cardPath)
        {
            DocumentPath = documentPath;
            PaperPath = paperPath;
            CardPath = cardPath;
        }
    }
}
