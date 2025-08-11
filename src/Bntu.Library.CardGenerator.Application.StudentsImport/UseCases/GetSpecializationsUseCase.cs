using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
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
    public class GetSpecializationsUseCase
    {
        private ISpecializationService specializationService;

        public GetSpecializationsUseCase(ISpecializationService specializationService)
        {
            this.specializationService = specializationService;
        }

        public GetSpecializationCaseResult GetSpecializations()
        {
            var result = specializationService.GetSpecializations();
            return new GetSpecializationCaseResult(result.IsSuccess, result.Message, result.Value ?? new SpecializationCollection());
        }
    }
}
