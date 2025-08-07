using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public class GetSpecializationCaseResult
    {
        public SpecializationCollection Specializations { get; }

        public GetSpecializationCaseResult(SpecializationCollection specializations)
        {
            Specializations = specializations;
        }
    }
}
