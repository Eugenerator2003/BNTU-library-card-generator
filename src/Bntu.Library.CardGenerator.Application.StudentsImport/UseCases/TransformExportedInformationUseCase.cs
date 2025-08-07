using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases
{
    public class TransformExportedInformationUseCase
    {
        private InformationTransformationService transformationService;

        public TransformExportedInformationUseCase(InformationTransformationService transformationService)
        {
            this.transformationService = transformationService;
        }

        public TransformExportedInformationCaseResult Transform(TransformExportedInformationCaseParameter parameter)
        {
            var result = transformationService.Transform(parameter.ExportedInformation, parameter.ImportingParameter);
            return new TransformExportedInformationCaseResult(result.IsSuccess,
                                                              result.Message,
                                                              result.Value ?? new List<ImportInformation>());
        }
    }
}
