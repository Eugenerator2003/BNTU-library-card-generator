using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class Student
    {
        public string Lastname { get; }

        public string Firstname { get; }

        public Faculty Faculty { get; }

        public StudyGroup StudyGroup { get; }

        public Student(string lastname, string firstname, Faculty faculty, StudyGroup studyGroup)
        {
            Lastname = lastname;
            Firstname = firstname;
            Faculty = faculty;
            StudyGroup = studyGroup;
        }
    }
}
