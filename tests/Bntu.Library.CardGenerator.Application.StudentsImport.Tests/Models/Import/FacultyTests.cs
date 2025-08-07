using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class FacultyTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var expectedName = "ФИТР";
            var faculty = new Faculty(expectedName);
            Assert.AreEqual(expectedName, faculty.Name);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var name = "ФИТР";
            var faculty1 = new Faculty(name);
            var faculty2 = new Faculty(name);
            var result = faculty1.Equals(faculty2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var faculty1 = new Faculty("ФИТР");
            var faculty2 = new Faculty("ФКП");
            var result = faculty1.Equals(faculty2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCodeSameTest()
        {
            var name = "ФИТР";
            var faculty1 = new Faculty(name);
            var faculty2 = new Faculty(name);
            var hash1 = faculty1.GetHashCode();
            var hash2 = faculty2.GetHashCode();
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCodeDifferentTest()
        {
            var faculty1 = new Faculty("ФИТР");
            var faculty2 = new Faculty("ФКП");
            var hash1 = faculty1.GetHashCode();
            var hash2 = faculty2.GetHashCode();
            Assert.AreNotEqual(hash1, hash2);
        }
    }
}
