using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class ProfessionTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var id = 42;
            var name = "Инженер";
            var profession = new Profession(id, name);

            Assert.AreEqual(id, profession.Id);
            Assert.AreEqual(name, profession.Name);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var profession1 = new Profession(1, "Программист");
            var profession2 = new Profession(1, "Программист");

            Assert.IsTrue(profession1.Equals(profession2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var profession1 = new Profession(1, "Программист");
            var profession2 = new Profession(2, "Инженер");

            Assert.IsFalse(profession1.Equals(profession2));
        }

        [TestMethod]
        public void GetHashCodeSameTest()
        {
            var profession1 = new Profession(1, "Программист");
            var profession2 = new Profession(1, "Программист");

            Assert.AreEqual(profession1.GetHashCode(), profession2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCodeDifferentTest()
        {
            var profession1 = new Profession(1, "Программист");
            var profession2 = new Profession(2, "Инженер");

            Assert.AreNotEqual(profession1.GetHashCode(), profession2.GetHashCode());
        }
    }
}
