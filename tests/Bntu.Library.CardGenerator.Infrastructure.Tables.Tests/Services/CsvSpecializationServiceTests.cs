using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services
{
    [TestClass]
    public class CsvSpecializationServiceTests
    {
        private static string[] lines = [
            "IdCategSocioProf;Libelle;parent",
            "1;профессионально;1",
            "2;непрофессионально;2",
            "3;стронг;5",
            "4;текст;4"
            ];

        private static Specialization[] specialiations = [
            new Specialization(1, "профессионально", 1),
            new Specialization(2, "непрофессионально", 2),
            new Specialization(3, "стронг", 5),
            new Specialization(4, "текст", 4),
            ];

        private static string filepath = "file.csv";

        [TestInitialize]
        public void Initialize()
        {
            File.WriteAllLines(filepath, lines);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(filepath);
        }

        [TestMethod]
        public void GetProfessionsSuccessTest()
        {
            var service = new CsvSpecializationService(filepath);
            var result = service.GetSpecializations();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            foreach (var specialization in specialiations)
            {
                var actual = result.Value[specialization.Id];
                Assert.IsNotNull(actual);
                Assert.AreEqual(specialization.Name, actual.Name);
            }
        }

        [TestMethod]
        public void GetProfessionsFailTest()
        {
            var wrongFilePath = "file.xlsx";
            var service = new CsvSpecializationService(wrongFilePath);
            var result = service.GetSpecializations();
            Assert.IsTrue(result.IsOperationError);
            Assert.IsTrue(result.Message.Contains(wrongFilePath));
        }
    }
}
