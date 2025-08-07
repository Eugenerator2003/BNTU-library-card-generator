using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters
{
    public class SaveImportInformationCaseParameter
    {
        public IList<ImportInformation> ImportInformation { get; }

        public SaveImportInformationCaseParameter(IList<ImportInformation> importInformation)
        {
            ImportInformation = importInformation;
        }
    }
}
