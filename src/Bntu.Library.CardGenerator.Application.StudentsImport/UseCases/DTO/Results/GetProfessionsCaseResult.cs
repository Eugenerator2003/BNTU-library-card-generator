using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class GetProfessionsCaseResult
    {
        public ProfessionCollection Professions { get; }

        public GetProfessionsCaseResult(ProfessionCollection professions)
        {
            Professions = professions;
        }
    }
}
