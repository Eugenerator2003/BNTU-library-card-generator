using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class GenerateReaderCardsCaseResultExtensionTests
    {
        [DataTestMethod]
        [DataRow(2)]
        [DataRow(9)]
        public void ToExportReaderCardsCaseParameterExtensionTest(int groupsCount)
        {
            var input = new GenerateReaderCardsCaseResult();
            List<string> groups = new List<string>();
            List<List<GeneratedReaderCard>> cards = new List<List<GeneratedReaderCard>>();
            for (int i = 0; i < groupsCount; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                groups.Add(group);
                List<GeneratedReaderCard> groupCards = new List<GeneratedReaderCard>();
                for (int j = 0; j < Random.Shared.Next(1, 41); j++)
                {
                    groupCards.Add(RandomValuesGenerator.GenerateGeneratedReaderCard(group));
                }
                cards.Add(groupCards);
                input.AddGeneratedCards(group, groupCards);
            }

            int exptectedGroupNumber = Random.Shared.Next(groupsCount);
            string expectedGroup = groups[exptectedGroupNumber];
            var exptectedCards = cards[exptectedGroupNumber];

            var actualResult = input.ToExportReaderCardsCaseParameter(expectedGroup);
            CollectionAssert.AreEquivalent(exptectedCards, (ICollection)actualResult.GeneratedCards);
        }
    }
}
