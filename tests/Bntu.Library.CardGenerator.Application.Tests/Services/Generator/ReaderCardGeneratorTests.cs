using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.Services.Generator
{
    [TestClass]
    public class ReaderCardGeneratorTests
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(30)]
        public void GenerateTest(int itemCount)
        {
            var list = new List<ReaderCard>();
            for(int i = 0; i < itemCount; i++)
            {
                list.Add(RandomReaderCardGenerator.GenerateRandom());
            }

            var generationResult = ReaderCardGenerator.Generate(list);
            Assert.IsTrue(generationResult.IsSuccess);
            Assert.AreEqual(string.Empty, generationResult.Message);
            Assert.IsNotNull(generationResult.Value);
            Assert.AreEqual(list.Count, generationResult.Value.Count);
            for(int i = 0; i < itemCount; i++)
            {
                var card = list[i];
                var generatedCard = generationResult.Value[i];
                Assert.AreEqual(card.CodbarValue, generatedCard.CodbarValue);
                Assert.AreEqual(card.PId, generatedCard.PId);
                Assert.AreEqual(card.IssueDate, generatedCard.IssueDate);
                Assert.AreEqual(card.ExpirationDate, generatedCard.ExpirationDate);
                Assert.AreEqual(card.Lastname, generatedCard.Lastname);
                Assert.AreEqual(card.Firstname, generatedCard.Firstname);
                Assert.AreEqual(card.Faculty, generatedCard.Faculty);
                Assert.AreEqual(card.StudyGroup, generatedCard.StudyGroup);
                Assert.AreEqual(card.Department, generatedCard.Department);
            }
        }
    }
}
