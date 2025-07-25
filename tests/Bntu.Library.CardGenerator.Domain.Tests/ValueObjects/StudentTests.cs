using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.ValueObjects
{
    [TestClass]
    public class StudentTests
    {
        [DataTestMethod]
        [DataRow("name", "name2", "ftk-2021", "2132313123", "fulltime")]
        [DataRow("Namislavov", "Imya", "ftug", "04581931923", "correspondence")]
        public void InitializeTest(string lastname, string firstname, string facultyName, string groupNumber, string departmentS)
        {
            Faculty faculty = new Faculty(facultyName);
            StudyGroup studyGroup = new StudyGroup(groupNumber, Enum.Parse<StudyGroup.DepartmentType>(departmentS, true));
            Student student = new Student(lastname, firstname, faculty, studyGroup);
            Assert.AreEqual(lastname, student.Lastname);
            Assert.AreEqual(firstname, student.Firstname);
            Assert.AreEqual(studyGroup, student.StudyGroup);
            Assert.AreEqual(faculty, student.Faculty);
        }
    }
}
