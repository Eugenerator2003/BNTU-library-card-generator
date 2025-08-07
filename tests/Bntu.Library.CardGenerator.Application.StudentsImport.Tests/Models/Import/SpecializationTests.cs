using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Import
{
    [TestClass]
    public class SpecializationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var specialization = new Specialization(42, "Летающие крокодилы", 7);
            Assert.AreEqual(42, specialization.Id);
            Assert.AreEqual("Летающие крокодилы", specialization.Name);
            Assert.AreEqual(7, specialization.ProfessionId);
        }

        [TestMethod]
        public void InitializeNoProfessionIdTest()
        {
            var specialization = new Specialization(13, "Инженерия мемов");
            Assert.AreEqual(13, specialization.Id);
            Assert.AreEqual("Инженерия мемов", specialization.Name);
            Assert.IsNull(specialization.ProfessionId);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var s1 = new Specialization(1, "Химия", 100);
            var s2 = new Specialization(1, "Химия", 100);
            Assert.IsTrue(s1.Equals(s2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var s1 = new Specialization(1, "Химия", 100);
            var s2 = new Specialization(2, "Физика", 101);
            Assert.IsFalse(s1.Equals(s2));
        }

        [TestMethod]
        public void HashCodeSameTest()
        {
            var s1 = new Specialization(5, "Прототипирование", 8);
            var s2 = new Specialization(5, "Прототипирование", 8);
            Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
        }

        [TestMethod]
        public void HashCodeDifferentTest()
        {
            var s1 = new Specialization(9, "Лабораторное дело", 4);
            var s2 = new Specialization(10, "Космоинженерия", 5);
            Assert.AreNotEqual(s1.GetHashCode(), s2.GetHashCode());
        }
    }
}
