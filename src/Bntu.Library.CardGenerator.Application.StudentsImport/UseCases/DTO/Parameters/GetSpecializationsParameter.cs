using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters
{
    public class GetSpecializationsParameter
    {
        public ProfessionCollection ProfessionCollection { get; }

        public GetSpecializationsParameter(ProfessionCollection professionCollection)
        {
            ProfessionCollection = professionCollection;
        }
    }
}
