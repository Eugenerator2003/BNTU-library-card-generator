using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class GetProfessionsCaseResult : CaseResult
    {
        public ProfessionCollection Professions { get; }
        public GetProfessionsCaseResult(bool success, string message, ProfessionCollection professions) 
            : base(success, message)
        {
            Professions = professions;
        }
    }
}
