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
    public class GenerateReaderCardsCaseResultTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var result = new GenerateReaderCardsCaseResult();
            Assert.AreEqual(0, result.Groups.Length);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(12)]
        public void AddGeneratedCardsTest(int groupsCount)
        {
            var actualResult = new GenerateReaderCardsCaseResult();
            var expectedDictionary = new Dictionary<string, IList<GeneratedReaderCard>>();
            for(int i = 0; i < groupsCount; i++)
            {
                var group = RandomValuesGenerator.GetStringLine();
                int cardsCount = group.Length;
                List<GeneratedReaderCard> cards = new List<GeneratedReaderCard>();
                for(int j = 0; j < cardsCount; j++)
                {
                    var card = RandomValuesGenerator.GenerateGeneratedReaderCard(group);
                    cards.Add(card);
                }
                actualResult.AddGeneratedCards(group, cards);
                expectedDictionary.Add(group, cards);
            }

            CollectionAssert.AreEquivalent(expectedDictionary.Keys, actualResult.Groups);
            foreach(var group in expectedDictionary.Keys)
            {
                CollectionAssert.AreEquivalent((ICollection)expectedDictionary[group], (ICollection)actualResult.GeneratedCards[group]);
            }
        }

        [TestMethod]
        public void AddGeneratedCardsNullExceptionTest()
        {
            var actualResult = new GenerateReaderCardsCaseResult();
            var group = RandomValuesGenerator.GetStringLine();
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            {
                actualResult.AddGeneratedCards(group, null!);
            });

            Assert.AreEqual("cards", ex.ParamName);
        }

        [TestMethod]
        public void AddGeneratedCardsZeroCollectionExceptionTest()
        {
            var actualResult = new GenerateReaderCardsCaseResult();
            var group = RandomValuesGenerator.GetStringLine();
            var ex = Assert.ThrowsException<ArgumentException>(() =>
            {
                actualResult.AddGeneratedCards(group, new List<GeneratedReaderCard>());
            });

            Assert.AreEqual("cards", ex.ParamName);
        }

        [TestMethod]
        public void SetSeccussTrueTest()
        {
            var result = new GenerateReaderCardsCaseResult();
            result.SetSuccess(true);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SetSeccussFalseTest()
        {
            var result = new GenerateReaderCardsCaseResult();
            result.SetSuccess(false);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void SetMessageTest()
        {
            var result = new GenerateReaderCardsCaseResult();
            var message = "daksd;asd";
            result.SetMessage(message);
            Assert.AreEqual(message, result.Message);
        }
    }
}
