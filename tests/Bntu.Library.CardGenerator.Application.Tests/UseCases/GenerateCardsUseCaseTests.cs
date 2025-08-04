using Bntu.Library.CardGenerator.Application.UseCases;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases
{
    [TestClass]
    public class GenerateCardsUseCaseTests
    {
        [TestMethod]
        public void GenerateTest()
        {
            var cards = new Dictionary<string, IList<ReaderCard>?>();
            var random = Random.Shared;
            for(int i = 0; i < 11; i++)
            {
                var group = RandomValuesGenerator.GetStringLine();
                var list = new List<ReaderCard>();
                for(int j = 0; j < random.Next(1, 41); j++)
                {
                    list.Add(RandomValuesGenerator.GenerateRandomReaderCard(group));
                }
                cards.Add(group, list);
            }

            var useCase = new GenerateCardsUseCase();
            var parameter = new GenerateReaderCardsCaseParameter(cards);
            var actualResult = useCase.Generate(parameter);

            CollectionAssert.AreEquivalent(cards.Keys, actualResult.Groups);
            foreach(var group in actualResult.Groups)
            {
                var exptectedList = cards[group];
                var actualList = actualResult.GeneratedCards[group];
                Assert.AreEqual(exptectedList!.Count, actualList.Count);
                for(int i = 0; i < exptectedList.Count; i++)
                {
                    var expected = exptectedList[i];
                    var actual = actualList.ElementAt(i);
                    Assert.AreEqual(expected.PId, actual.PId);
                    Assert.AreEqual(expected.CodbarValue, actual.CodbarValue);
                    Assert.AreEqual(expected.Faculty, actual.Faculty);
                    Assert.AreEqual(expected.Firstname, actual.Firstname);
                    Assert.AreEqual(expected.Lastname, actual.Lastname);
                    Assert.AreEqual(expected.Department, actual.Department);
                    Assert.AreEqual(expected.IssueDate, actual.IssueDate);
                    Assert.AreEqual(expected.ExpirationDate, actual.ExpirationDate);
                    Assert.AreEqual(expected.StudyGroup, actual.StudyGroup);
                    Assert.AreEqual(expected.PhotoName, actual.PhotoName);
                }
            }
        }
    }
}
