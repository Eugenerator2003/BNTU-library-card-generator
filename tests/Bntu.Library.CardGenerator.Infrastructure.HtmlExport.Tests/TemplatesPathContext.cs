using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests
{
    [TestClass]
    public class TemplatesPathContextTest
    {
        [TestMethod]
        public void InitializeTest()
        {
            string document = "31231";
            string card = "kasdald";
            string paper = "jasdkljsad";
            var context = new TemplatesPathContext(document, paper, card);
            Assert.AreEqual(document, context.DocumentPath);
            Assert.AreEqual(paper, context.PaperPath);
            Assert.AreEqual(card, context.CardPath);
        }
    }
}
