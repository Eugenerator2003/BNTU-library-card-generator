using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Csv
{
    [TestClass]
    public class CsvProfessionServiceTests
    {
        private static string[] lines = [
            "IdProfession;Libelle",
            "1;специально",
            "2;неспециально",
            "3;стринг",
            "4;лайн"
            ];

        private static Profession[] professions = [
            new Profession(1, "специально"),
            new Profession(2, "неспециально"),
            new Profession(3, "стринг"),
            new Profession(4, "лайн"),
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
            var service = new CsvProfessionService(filepath);
            var result = service.GetProfessions();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            foreach (var profession in professions)
            {
                var actual = result.Value[profession.Id];
                Assert.IsNotNull(actual);
                Assert.AreEqual(profession.Name, actual.Name);
            }
        }

        [TestMethod]
        public void GetProfessionsFailTest()
        {
            var wrongFilePath = "file.xlsx";
            var service = new CsvProfessionService(wrongFilePath);
            var result = service.GetProfessions();
            Assert.IsTrue(result.IsOperationError);
            Assert.IsTrue(result.Message.Contains(wrongFilePath));
        }
    }
}
