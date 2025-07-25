using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.ValueObjects
{
    [TestClass]
    public class FacultyTests
    {
        [DataTestMethod]
        [DataRow("FTK")]
        [DataRow("AFK-21")]
        [DataRow("EF-24")]
        [DataRow("FMMP-27")]
        [DataRow("FTUG-30")]
        public void InitializeTests(string name)
        {
            Faculty faculty = new Faculty(name);
            Assert.AreEqual(name, faculty.Name);
        }
    }
}
