using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public class ReaderCardsCaseResult
    {
        private Dictionary<string, IList<ReaderCard>?> groupCards;

        public string[] Groups => groupCards.Keys.ToArray();

        public IList<ReaderCard>? this[string groupNumber]
        {
            get
            {
                if (!groupCards.ContainsKey(groupNumber))
                {
                    throw new ArgumentException($"Коллекция не содержит указанной группы {groupNumber}");
                }
                return groupCards[groupNumber];
            }
        }

        public ReaderCardsCaseResult(string[] groups)
        {
            groupCards = new Dictionary<string, IList<ReaderCard>?>();
            foreach (var group in groups)
            {
                groupCards.Add(group, null);
            }
        }

        public void SetGroupCards(string groupNumber, IList<ReaderCard> readerCards)
        {
            if (!groupCards.ContainsKey(groupNumber))
            {
                throw new ArgumentException($"Коллекция не содержит указанной группы {groupNumber}");
            }

            groupCards[groupNumber] = readerCards;
        }
    }
}
