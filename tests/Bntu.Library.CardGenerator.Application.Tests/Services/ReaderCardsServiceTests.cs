using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Application.Services;
using Bntu.Library.CardGenerator.Domain.Entities;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.Services
{
    [TestClass]
    public class ReaderCardsServiceTests
    {
        [DataTestMethod]
        [DataRow("123213", 5)]
        [DataRow("318238123", 1)]
        [DataRow("FTUG-131023", 30)]
        public void GetReaderCardsByGroupSuccessTest(string group, int count)
        {
            var repositoryMoq = new Mock<IReaderCardRepository>();
            IList<ReaderCard> list = new List<ReaderCard>();
            for (int i = 0; i < count; i++)
            {
                list.Add(RandomReaderCardGenerator.GenerateRandom(group));
            }
            repositoryMoq.Setup(s => s.GetCardsByGroup(group)).Returns(() =>
            {
                return list;
            });

            var readerCardService = new ReaderCardService(repositoryMoq.Object);
            var result = readerCardService.GetReaderCardsByGroup(group);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(string.Empty, result.Message);
            Assert.IsNotNull(result.Value);
            CollectionAssert.AreEqual((ICollection)list, (ICollection)result.Value);

            repositoryMoq.Verify(s => s.GetCardsByGroup(group), Times.Once());
        }

        [DataTestMethod]
        [DataRow("group313")]
        [DataRow("-3912931")]
        public void GetReaderCardsByGroupFailTest(string group)
        {
            var repositoryMoq = new Mock<IReaderCardRepository>();
            repositoryMoq.Setup(s => s.GetCardsByGroup(group)).Returns(new List<ReaderCard>());
            var readerCardService = new ReaderCardService(repositoryMoq.Object);
            var result = readerCardService.GetReaderCardsByGroup(group);

            Assert.IsTrue(result.IsOperationError);
            Assert.IsFalse(result.IsValidationError);
            Assert.IsNull(result.Value);
            Assert.AreEqual($"Отсутствует информация по студентам указаннной группы {group}", result.Message);
            repositoryMoq.Verify(s => s.GetCardsByGroup(group), Times.Once);
        }
    }
}
