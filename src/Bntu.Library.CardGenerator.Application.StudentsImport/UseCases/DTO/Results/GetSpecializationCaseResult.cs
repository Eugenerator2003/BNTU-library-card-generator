using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class GetSpecializationCaseResult : CaseResult
    {
        public SpecializationCollection Specializations { get; }

        public GetSpecializationCaseResult(bool success, string message, SpecializationCollection specializations)
            : base(success, message)
        {
            Specializations = specializations;
        }
    }
}
