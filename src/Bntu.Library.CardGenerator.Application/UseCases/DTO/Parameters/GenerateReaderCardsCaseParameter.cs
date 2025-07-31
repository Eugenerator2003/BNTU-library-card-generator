using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public class GenerateReaderCardsCaseParameter
    {
        public string[] Groups => groupCards.Keys.ToArray();

        public IReadOnlyDictionary<string, IReadOnlyCollection<ReaderCard>> ReaderCards
        {
            get
            {
                return new ReadOnlyDictionary<string, IReadOnlyCollection<ReaderCard>>(groupCards);
            }
        }

        private Dictionary<string, IReadOnlyCollection<ReaderCard>> groupCards;

        public GenerateReaderCardsCaseParameter(IDictionary<string, IList<ReaderCard>?> groupedReaderCards)
        {
            groupCards = new Dictionary<string, IReadOnlyCollection<ReaderCard>>();
            foreach(var pair in groupedReaderCards)
            {
                if (pair.Value != null && pair.Value.Count == 0)
                {
                    groupCards.Add(pair.Key, new ReadOnlyCollection<ReaderCard>(pair.Value));
                }
            }
        }
    }
}
