using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class GetExportedInformationCaseResult : CaseResult
    {
        public IList<ExportedInformation> ExportedInformation { get; }

        public GetExportedInformationCaseResult(bool success, string message, IList<ExportedInformation> exportedInformation) 
            : base(success, message)
        {
            ExportedInformation = exportedInformation;
        }
    }
}
