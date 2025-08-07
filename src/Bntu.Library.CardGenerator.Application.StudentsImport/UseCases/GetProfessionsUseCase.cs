using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases
{
    public class GetProfessionsUseCase
    {
        private IProfessionService professionService;

        public GetProfessionsUseCase(IProfessionService professionService)
        {
            this.professionService = professionService;
        }

        public GetProfessionsCaseResult GetProfessions()
        {
            var result = professionService.GetProfessions();
            return new GetProfessionsCaseResult(result.IsSuccess, result.Message, result.Value ?? new ProfessionCollection());
        }
    }
}
