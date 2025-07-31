using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public class GenerateReaderCardsCaseResult
    {
        public string[] Groups => generatedCards.Keys.ToArray();

        public IReadOnlyDictionary<string, IReadOnlyCollection<GeneratedReaderCard>> GeneratedCards
        {
            get
            {
                return new ReadOnlyDictionary<string, IReadOnlyCollection<GeneratedReaderCard>>(generatedCards);
            }
        }

        private Dictionary<string, IReadOnlyCollection<GeneratedReaderCard>> generatedCards;

        public GenerateReaderCardsCaseResult()
        {
            generatedCards = new Dictionary<string, IReadOnlyCollection<GeneratedReaderCard>>();
        }

        public void AddGeneratedCards(string groups, IList<GeneratedReaderCard> cards)
        {
            if (cards == null || cards.Count == 0)
            {
                throw new ArgumentException("Сгенерированные билеты не могут быть null и их количество должно быть больше 0.", nameof(cards));
            }

            generatedCards.Add(groups, new ReadOnlyCollection<GeneratedReaderCard>(cards));
        }
    }
}
