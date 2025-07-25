using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Tests.ValueObjects
{
    [TestClass]
    public class StudyGroupTests
    {
        [DataTestMethod]
        [DataRow("123132103012", "fulltime")]
        [DataRow("123132103012", "correspondence")]
        [DataRow("3812321393", "fulltime")]
        [DataRow("3812321393", "correspondence")]
        public void InitializeTest(string number, string departmentS)
        {
            StudyGroup.DepartmentType department = Enum.Parse<StudyGroup.DepartmentType>(departmentS, true);
            StudyGroup studyGroup = new StudyGroup(number, department);
            Assert.AreEqual(number, studyGroup.Number);
            Assert.AreEqual(department, studyGroup.Department);
        }
    }
}
