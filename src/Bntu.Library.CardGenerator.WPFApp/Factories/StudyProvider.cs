using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class StudyProvider
    {
        public static StudyProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new StudyProvider();
                return instance;
            }
        }

        private static StudyProvider instance = null!;

        public ProfessionCollection? Professions { get; set; }

        public SpecializationCollection? Specialization { get; set; }

        private StudyProvider()
        {

        }
    }
}
