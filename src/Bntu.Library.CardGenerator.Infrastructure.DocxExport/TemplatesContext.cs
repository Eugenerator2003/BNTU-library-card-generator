using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.DocxExport
{
    public class TemplatesContext
    {
        public string Document { get; }

        public string Paper { get; }

        public string Card { get; }

        public TemplatesContext(string document, string paper, string card)
        {
            Document = document;
            Paper = paper;
            Card = card;
        }
    }
}
