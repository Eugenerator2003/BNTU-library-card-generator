using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public static class ExportReaderCardsParameterExtension
    {
        public static ExportReaderCardsParameter FromGenerateReaderCardsCaseResult(this GenerateReaderCardsCaseResult result, string group)
        {
            return new ExportReaderCardsParameter(result.GeneratedCards[group]);
        }
    }
}
