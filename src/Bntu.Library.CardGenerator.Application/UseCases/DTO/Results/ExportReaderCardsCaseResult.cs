using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public class ExportReaderCardsCaseResult : CaseResult
    {
        public ExportReaderCardsCaseResult(bool success, string message) 
            : base(success, message)
        {
        }
    }
}
