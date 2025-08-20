using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class TransformExportedInformationCaseResult : CaseResult
    {
        public IList<ImportInformation> ImportInformation { get; }

        public TransformExportedInformationCaseResult(bool success, string message, IList<ImportInformation> importInformation) 
            : base(success, message)
        {
            ImportInformation = importInformation;
        }
    }
}
