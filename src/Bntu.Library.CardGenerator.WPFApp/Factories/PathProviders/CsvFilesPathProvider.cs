using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders
{
    public class CsvFilesPathProvider
    {
        public static CsvFilesPathProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new CsvFilesPathProvider();
                return instance;
            }
        }

        private static CsvFilesPathProvider instance = null!;

        public string? ReaderCardsSource { get; set; }

        public string? SpecializationSource { get; set; }

        public string? ProfessionSource { get; set; }

        public string? ImportInformationDestination { get; set; }

        private CsvFilesPathProvider()
        {

        }
    }
}
