using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders
{
    public class XlsxFilesPathProvider
    {
        public static XlsxFilesPathProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new XlsxFilesPathProvider();
                return instance;
            }
        }

        private static XlsxFilesPathProvider instance = null!;

        public string? ExportedInformationSource { get; set; }

        private XlsxFilesPathProvider()
        {

        }
    }
}
