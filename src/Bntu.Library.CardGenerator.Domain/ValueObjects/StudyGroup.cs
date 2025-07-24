using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class StudyGroup
    {
        public enum DepartmentType
        {
            Fulltime,
            Correspondence
        }

        public DepartmentType Department { get; }

        public string Number { get; }

        public StudyGroup(string number, DepartmentType department)
        {
            Number = number;
            Department = department;
        }
    }
}
