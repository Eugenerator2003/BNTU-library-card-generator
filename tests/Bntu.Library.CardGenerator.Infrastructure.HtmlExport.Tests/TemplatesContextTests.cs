using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests
{
    [TestClass]
    public class TemplatesContextTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            string document = "31231";
            string card = "kasdald";
            string paper = "jasdkljsad";
            var context = new TemplatesContext(document, paper, card);
            Assert.AreEqual(document, context.Document);
            Assert.AreEqual(paper, context.Paper);
            Assert.AreEqual(card, context.Card);
        }
    }
}
