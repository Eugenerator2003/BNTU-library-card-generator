using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.PathProviders
{
    public class HtmlFilesPathProvider
    {
        public static HtmlFilesPathProvider Instance
        { get
            {
                if (instance == null)
                    instance = new HtmlFilesPathProvider();
                return instance;
            } 
        }

        private static HtmlFilesPathProvider instance = null!;

        public string? DocumentPath { get; set; }

        public string? PaperPath { get; set; }

        public string? CardPath { get; set; }

        public string? RootFolder { get; }

        public string? Subfolder { get; }

        public string? GroupFolder { get; }

        public string? PhotoFolder { get; }

        private HtmlFilesPathProvider()
        {

        }
    }
}
