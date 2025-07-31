using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class GetReaderCardsCaseResultTests
    {
        [DataTestMethod]
        [DataRow(3)]
        [DataRow(7)]
        public void InitializeTests(int groupsCount)
        {
            string[] groups = GetGroups(groupsCount);

            var actualResult = new GetReaderCardsCaseResult(groups);
            CollectionAssert.AreEquivalent(groups, actualResult.Groups);
            for (int i = 0; i < groupsCount; i++)
            {
                Assert.IsNull(actualResult[groups[i]]);
            }
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(27)]
        public void SetGroupCardsTest(int groupsCount)
        {
            string[] groups = GetGroups(groupsCount);
            Dictionary<string, List<ReaderCard>> exptectedValues = new Dictionary<string, List<ReaderCard>>();
            var actualResult = new GetReaderCardsCaseResult(groups);
            for (int i = 0; i < groupsCount; i++)
            {
                var cardsCount = Random.Shared.Next(1, 41);
                List<ReaderCard> cards = new List<ReaderCard>();
                for (int j = 0; j < cardsCount; j++)
                {
                    cards.Add(RandomValuesGenerator.GenerateRandomReaderCard());
                }
                actualResult.SetGroupCards(groups[i], cards);
                exptectedValues.Add(groups[i], cards);
            }

            CollectionAssert.AreEquivalent(groups, actualResult.Groups);
            for (int i = 0; i < groupsCount; i++)
            {
                CollectionAssert.AreEquivalent(exptectedValues[groups[i]], (ICollection)actualResult[groups[i]]!);
            }
        }

        [TestMethod]
        public void SetGroupCardsExceptionTest()
        {
            string[] groups = GetGroups(10);
            var actualResult = new GetReaderCardsCaseResult(groups);
            List<ReaderCard> cards = new List<ReaderCard>();
            var fakeGroupNumber = RandomValuesGenerator.GetStringLine();
            var exception = Assert.ThrowsException<ArgumentException>(() =>
            {
                actualResult.SetGroupCards(fakeGroupNumber, cards);
            });

            Assert.AreEqual("groupNumber", exception.ParamName);
        }

        [TestMethod]
        public void GetGroupCardsExceptionTest()
        {
            string[] groups = GetGroups(10);
            var actualResult = new GetReaderCardsCaseResult(groups);
            List<ReaderCard> cards = new List<ReaderCard>();
            var fakeGroupNumber = RandomValuesGenerator.GetStringLine();
            var exception = Assert.ThrowsException<ArgumentException>(() =>
            {
                var cards = actualResult[fakeGroupNumber];
            });

            Assert.AreEqual("groupNumber", exception.ParamName);
        }

        private static string[] GetGroups(int groupsCount)
        {
            string[] groups = new string[groupsCount];
            for (int i = 0; i < groupsCount; i++)
            {
                groups[i] = RandomValuesGenerator.GetStringLine();
            }
            return groups;
        }
    }
}
