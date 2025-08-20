using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Application.Services;
using Bntu.Library.CardGenerator.Application.UseCases;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Domain.Entities;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases
{
    [TestClass]
    public class GetReaderCardsUseCaseTests
    {
        [TestMethod]
        public void GetReaderCardsExixtsGroupsTest()
        {
            var cards = new Dictionary<string, List<ReaderCard>>();
            for (int i = 0; i < 5; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                List<ReaderCard> groupCards = new List<ReaderCard>();
                for (int j = 0; j < Random.Shared.Next(1, 41); j++)
                {
                    groupCards.Add(RandomValuesGenerator.GenerateRandomReaderCard(group));
                }
                cards.Add(group, groupCards);
            }

            var repo = GetRepository(cards);
            var useCase = GetUseCase(repo);
            var parameter = new GetReaderCardsCaseParameter(cards.Keys.ToList());
            var actualResult = useCase.GetReaderCards(parameter);

            AssertResult(cards, actualResult);
        }

        [TestMethod]
        public void GetReaderCardsNonExixtsAllGroupsTest()
        {
            var cards = new List<string>();
            var repositoryMock = new Mock<IReaderCardRepository>();
            for (int i = 0; i < 3; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                repositoryMock.Setup(m => m.GetCardsByGroup(group))
                              .Returns(new List<ReaderCard>());
                cards.Add(group);
            }

            var useCase = GetUseCase(repositoryMock.Object);
            var parameter = new GetReaderCardsCaseParameter(cards);

            var actualResult = useCase.GetReaderCards(parameter);

            Assert.IsFalse(actualResult.Success);
            CollectionAssert.AreEquivalent(cards, actualResult.Groups);
            for (int i = 0; i < cards.Count; i++)
            {
                Assert.IsNull(actualResult[cards[i]]);
            }
        }

        [TestMethod]
        public void GetReaderCardsNonExistsTest()
        {
            var cards = new Dictionary<string, List<ReaderCard>>();
            for (int i = 0; i < 5; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                if (Random.Shared.Next(1, 3) == 1)
                {
                    List<ReaderCard> groupCards = new List<ReaderCard>();
                    for (int j = 0; j < Random.Shared.Next(1, 41); j++)
                    {
                        groupCards.Add(RandomValuesGenerator.GenerateRandomReaderCard(group));
                    }
                    cards.Add(group, groupCards);
                }
                else
                {
                    cards.Add(group, new List<ReaderCard>());
                }
            }

            var repo = GetRepository(cards);
            var useCase = GetUseCase(repo);
            var parameter = new GetReaderCardsCaseParameter(cards.Keys.ToList());
            var actualResult = useCase.GetReaderCards(parameter);

            Assert.IsFalse(actualResult.Success);
            AssertResult(cards, actualResult);
        }

        private IReaderCardRepository GetRepository(Dictionary<string, List<ReaderCard>> cardsByGroup)
        {
            var repositoryMock = new Mock<IReaderCardRepository>();
            for (int i = 0; i < cardsByGroup.Count; i++)
            {
                string group = cardsByGroup.Keys.ElementAt(i);
                repositoryMock.Setup(m => m.GetCardsByGroup(group))
                              .Returns(cardsByGroup[group]);
            }
            return repositoryMock.Object;
        }

        private GetReaderCardsUseCase GetUseCase(IReaderCardRepository repository)
        {
            return new GetReaderCardsUseCase(new ReaderCardService(repository));
        }
        
        private void AssertResult(Dictionary<string, List<ReaderCard>> expectedCardsByGroup, 
                                  GetReaderCardsCaseResult actualResult)
        {
            CollectionAssert.AreEquivalent(expectedCardsByGroup.Keys, actualResult.Groups);
            foreach (var group in actualResult.Groups)
            {
                var expectedCards = expectedCardsByGroup[group];
                if (expectedCards.Count > 0)
                {
                    CollectionAssert.AreEquivalent(expectedCards, (ICollection)actualResult[group]!);
                }
                else
                {
                    Assert.IsNull(actualResult[group]);
                }
            }
        }
    }
}
