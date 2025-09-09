using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public static class GenerateReaderCardsCaseResultExtension
    {
        public static ExportReaderCardsCaseParameter ToExportReaderCardsCaseParameter(this GenerateReaderCardsCaseResult result, string group)
        {
            return new ExportReaderCardsCaseParameter(result.GeneratedCards[group]);
        }

        public static ExportReaderCardsCaseParameter ToExportReaderCardsCaseParameter(this GenerateReaderCardsCaseResult result)
        {
            var list = result.GeneratedCards.Values.SelectMany(a => a.ToList()).ToList();
            return new ExportReaderCardsCaseParameter(new ReadOnlyCollection<GeneratedReaderCard>(list));
        }
    }
}
