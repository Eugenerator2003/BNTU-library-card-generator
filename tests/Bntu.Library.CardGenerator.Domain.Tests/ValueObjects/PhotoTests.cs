using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.ValueObjects
{
    [TestClass]
    public class PhotoTests
    {
        [DataTestMethod]
        [DataRow(@"c:\\photos\31231.jpg")]
        [DataRow("312321838.jpg")]
        [DataRow("dasda-masmdas.png")]
        [DataRow(@"/files/photos/39129312.jpg")]
        public void InitializeTest(string name)
        {
            Photo photo = new Photo(name);
            Assert.AreEqual(name, photo.Name);
        }
    }
}
