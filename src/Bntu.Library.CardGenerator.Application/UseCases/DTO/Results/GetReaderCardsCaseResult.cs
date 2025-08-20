using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public class GetReaderCardsCaseResult
    {
        public int CardsCount => groupCards.Select(a => a.Value?.Count ?? 0).Sum();

        public string[] Groups => groupCards.Keys.ToArray();

        public IReadOnlyCollection<ReaderCard>? this[string groupNumber]
        {
            get
            {
                if (!groupCards.ContainsKey(groupNumber))
                {
                    throw new ArgumentException($"Коллекция не содержит указанной группы {groupNumber}", nameof(groupNumber));
                }
                return groupCards[groupNumber];
            }
        }

        private Dictionary<string, IReadOnlyCollection<ReaderCard>?> groupCards;

        public GetReaderCardsCaseResult(string[] groups)
        {
            groupCards = new Dictionary<string, IReadOnlyCollection<ReaderCard>?>();
            foreach (var group in groups)
            {
                groupCards.Add(group, null);
            }
        }

        public void SetGroupCards(string groupNumber, IList<ReaderCard> readerCards)
        {
            if (!groupCards.ContainsKey(groupNumber))
            {
                throw new ArgumentException($"Коллекция не содержит указанной группы {groupNumber}", nameof(groupNumber));
            }

            groupCards[groupNumber] = new ReadOnlyCollection<ReaderCard>(readerCards);
        }
    }
}
