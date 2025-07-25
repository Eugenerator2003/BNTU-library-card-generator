using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.ValueObjects
{
    [TestClass]
    public class CardInfoTests
    {
        [DataTestMethod]
        [DataRow(1, 1, "20-01-2021", "22-02-2025")]
        [DataRow(3, 2, "10-03-1998", "01-03-2021")]
        public void InitializeTest(int codbar, int id, string issudeDateS, string expirationDateS)
        {
            DateTime issueDate = DateTime.Parse(issudeDateS);
            DateTime expirationDate = DateTime.Parse(expirationDateS);
            CardInfo cardInfo = new CardInfo(codbar, id, issueDate, expirationDate);
            Assert.AreEqual(codbar, cardInfo.CodbarValue);
            Assert.AreEqual(id, cardInfo.PId);
            Assert.AreEqual(issueDate, cardInfo.IssueDate);
            Assert.AreEqual(expirationDate, cardInfo.ExpirationDate);
        }
    }
}
