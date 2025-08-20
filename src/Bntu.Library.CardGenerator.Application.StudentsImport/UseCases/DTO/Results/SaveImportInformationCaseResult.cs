using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class SaveImportInformationCaseResult : CaseResult
    {
        public SaveImportInformationCaseResult(bool success, string message) 
            : base(success, message)
        {
        }
    }
}
