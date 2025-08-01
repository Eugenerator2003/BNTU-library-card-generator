using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases
{
    public class ExportReaderCardsUseCase
    {
        private IGeneratedCardExporter exporter;

        public ExportReaderCardsUseCase(IGeneratedCardExporter exporter)
        {
            this.exporter = exporter;
        }

        public ExportReaderCardsCaseResult Export(ExportReaderCardsCaseParameter parameter)
        {
            var exportResult = exporter.Export(parameter.GeneratedCards.ToList());
            return new ExportReaderCardsCaseResult(exportResult.IsSuccess, exportResult.Message);
        }
    }
}
