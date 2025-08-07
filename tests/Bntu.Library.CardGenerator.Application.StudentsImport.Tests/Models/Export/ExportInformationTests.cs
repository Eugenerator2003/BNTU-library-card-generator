using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bntu.Library.CardGenerator.Application.StudentsImport.Models.Person;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Models.Export
{
    [TestClass]
    public class ExportInformationTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var studentMock = new Mock<ExportedStudentInformation>(null!, null!, null!, DateTime.Now, null!, null!, Person.SexType.Male);
            var studyMock = new Mock<ExportedStudyInformation>(DateTime.Now, DateTime.Now, null!, null!, null!, StudyGroup.DepartmentType.Correspondence);
            var concactMock = new Mock<ExportedContactInformation>(null!, new List<string>(), null!);

            int expectedId = 3230123;
            string exptectedPhotoname = "path://tofile.jpg";

            var info = new ExportedInformation(expectedId, studentMock.Object, studyMock.Object, concactMock.Object, exptectedPhotoname);
            Assert.AreEqual(expectedId, info.Id);
            Assert.AreEqual(studentMock.Object, info.Student);
            Assert.AreEqual(studyMock.Object, info.StudyInformation);
            Assert.AreEqual(concactMock.Object, info.ContactInformation);
            Assert.AreEqual(exptectedPhotoname, info.Photoname);
        }
    }
}
