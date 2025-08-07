using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Services
{
    public interface ISpecializationService
    {
        ApplicationResult<SpecializationCollection> GetSpecializations(ProfessionCollection professionCollection);
    }
}
