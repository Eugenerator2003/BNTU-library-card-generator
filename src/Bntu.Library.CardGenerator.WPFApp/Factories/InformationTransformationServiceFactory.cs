using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class InformationTransformationServiceFactory : IFactory<InformationTransformationService>
    {
        public InformationTransformationService Create()
        {
            var studyProvider = StudyProvider.Instance;
            if (studyProvider.Professions is null || studyProvider.Specialization is null)
            {
                throw new ArgumentNullException("", "Профессии или специальности не были установлены");
            }
            return new InformationTransformationService(studyProvider.Professions, studyProvider.Specialization);
        }
    }
}
