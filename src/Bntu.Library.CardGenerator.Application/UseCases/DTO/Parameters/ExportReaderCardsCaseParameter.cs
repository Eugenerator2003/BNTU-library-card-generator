using Bntu.Library.CardGenerator.Application.Services.Generator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public class ExportReaderCardsCaseParameter
    {
        public IReadOnlyCollection<GeneratedReaderCard> GeneratedCards { get; }

        public ExportReaderCardsCaseParameter(IReadOnlyCollection<GeneratedReaderCard> generatedCards)
        {
            GeneratedCards = new ReadOnlyCollection<GeneratedReaderCard>(generatedCards.ToList());
        }
    }
}
