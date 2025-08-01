using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public static class GenerateReaderCardsCaseResultExtension
    {
        public static ExportReaderCardsParameter FromGenerateReaderCardsCaseResult(this GenerateReaderCardsCaseResult result, string group)
        {
            return new ExportReaderCardsParameter(result.GeneratedCards[group]);
        }
    }
}
