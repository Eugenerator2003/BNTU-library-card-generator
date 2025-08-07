using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases
{
    public class GetExportedInformationUseCase
    {
        private IExportedInformationService exportedInformationService;

        public GetExportedInformationUseCase(IExportedInformationService exportedInformationService)
        {
            this.exportedInformationService = exportedInformationService;
        }

        public GetExportedInformationCaseResult GetExportedInformation()
        {
            var result = exportedInformationService.GetExportedInformation();
            return new GetExportedInformationCaseResult(result.IsSuccess, result.Message, result.Value ?? new List<ExportedInformation>());
        }
    }
}
