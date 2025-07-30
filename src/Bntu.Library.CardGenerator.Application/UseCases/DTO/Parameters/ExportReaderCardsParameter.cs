using Bntu.Library.CardGenerator.Application.Services.Generator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public class ExportReaderCardsParameter
    {
        public IReadOnlyDictionary<string, IReadOnlyCollection<GeneratedReaderCard>> GeneratedCards { get; }

        public ExportReaderCardsParameter(IDictionary<string, IReadOnlyCollection<GeneratedReaderCard>> generatedCards)
        {
            GeneratedCards = new ReadOnlyDictionary<string, IReadOnlyCollection<GeneratedReaderCard>>(generatedCards);
        }
    }
}
