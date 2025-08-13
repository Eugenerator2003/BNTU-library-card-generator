using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Services.Xlsx
{
    [TestClass]
    public class XlsxExportedInformationServiceTests
    {
        private static string filesDirectory = "XlsxExamples";
        private static string validFile = Path.Combine(filesDirectory, "valid.xslx");

        private static ExportedInformation[] ExpectedInfos = [
            new ExportedInformation(1,
                new XlsxExportedStudentInformation("Иванов", "Егор", "Алексеевич", new DateTime(2010, 9, 23), "МР 9264259", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ффммпп", "10101", "156", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", "ул", "проспект Независимости", "102", "2", "6", ["375334842000"], "егор.иванов@yandex.by"),
                "иванов_егор.jpg"),

            new ExportedInformation(2,
                new XlsxExportedStudentInformation("Соловьёв", "Максим", "Дмитриевич", new DateTime(2016, 4, 1), "МР 6360326", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ффииттрр", "20202", "198", Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence),
                new XlsxExportedContactInformation("г", "Минск", "ул", "улица Толстого", "12", "3", "89", ["375292514329"], "максим.соловьёв@gmail.com"),
                "соловьёв_максим.jpg"),

            new ExportedInformation(3,
                new XlsxExportedStudentInformation("Морозов", "Сергей", "Алексеевич", new DateTime(1998, 12, 20), "ВН 3455589", "ОВД г. Минска", Person.SexType.Male),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ффииттрр", "40404", "120", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", "ул", "улица Козлова", "89", "3", null, ["375256235551"], "сергей.морозов@yandex.by"),
                "морозов_сергей.jpg"),

            new ExportedInformation(4,
                new XlsxExportedStudentInformation("Кузнецов", "Алексей", "Викторович", new DateTime(1997, 4, 29), "МР 4022139", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ааттфф", "20202", "173", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", "ул", "улица Козлова", "65", null, "87", ["375331420507"], "алексей.кузнецов@gmail.com"),
                "кузнецов_алексей.jpg"),

            new ExportedInformation(5,
                new XlsxExportedStudentInformation("Сидоров", "Алексей", "Андреевич", new DateTime(2008, 11, 17), "ВН 6771411", "ОВД г. Минска", Person.SexType.Male),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ааттфф", "10101", "154", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", "ул", "проспект Независимости", "174", null, "137", ["375294264758"], "алексей.сидоров@gmail.com"),
                "сидоров_алексей.jpg"),

            new ExportedInformation(6,
                new XlsxExportedStudentInformation("Егоров", "Максим", "Павлович", new DateTime(1998, 2, 27), "АН 2677469", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ммттфф", "30303", "134", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", null, null, null, null, null, ["375335846875"], "максим.егоров@gmail.com"),
                "егоров_максим.jpg"),

            new ExportedInformation(7,
                new XlsxExportedStudentInformation("Мельников", "Максим", "Максимович", new DateTime(2020, 7, 24), "АН 1648390", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ффииттрр", "50505", "191", Domain.ValueObjects.StudyGroup.DepartmentType.Correspondence),
                new XlsxExportedContactInformation("г", "Минск", "ул", "проспект Независимости", "136", "2", "141", ["375449989668"], "максим.мельников@gmail.com"),
                "мельников_максим.jpg"),

            new ExportedInformation(8,
                new XlsxExportedStudentInformation("Иванов", "Дмитрий", "Александрович", new DateTime(2012, 1, 13), "ВН 3864372", "ОВД г. Минска", Person.SexType.Female),
                new XlsxExportedStudyInformation(new DateTime(2018, 9, 1), new DateTime(2022, 6, 30), "ффммпп", "40404", "150", Domain.ValueObjects.StudyGroup.DepartmentType.Fulltime),
                new XlsxExportedContactInformation("г", "Минск", "ул", "проспект Независимости", "102", "2", null, ["375449051105"], "дмитрий.иванов@yandex.by"),
                "иванов_дмитрий.jpg")
            ];


        [TestMethod]
        public void GetExportedInformationTest()
        {
            var service = new XlsxExportedInformationService(validFile);
            var result = service.GetExportedInformation();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(ExpectedInfos.Length, result.Value.Count);
            for(int i = 0; i < ExpectedInfos.Length; i++)
            {

            }
        }
    }
}
