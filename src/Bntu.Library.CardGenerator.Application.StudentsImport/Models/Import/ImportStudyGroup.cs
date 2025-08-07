using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class ImportStudyGroup
    {
        public Faculty Faculty { get; }

        public Specialization Specialization { get; }

        public Profession Profession { get; }

        public string Number { get; }

        public StudyGroup.DepartmentType DepartmentType { get; }

        public ImportStudyGroup(Faculty faculty, Profession profession, Specialization specialization, string number, StudyGroup.DepartmentType departmentType)
        {
            Faculty = faculty;
            Profession = profession;
            Specialization = specialization;
            Number = number;
            DepartmentType = departmentType;
        }

        public override bool Equals(object? obj)
        {
            return obj is ImportStudyGroup group &&
                   EqualityComparer<Faculty>.Default.Equals(Faculty, group.Faculty) &&
                   EqualityComparer<Specialization>.Default.Equals(Specialization, group.Specialization) &&
                   EqualityComparer<Profession>.Default.Equals(Profession, group.Profession) &&
                   Number == group.Number &&
                   DepartmentType == group.DepartmentType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Faculty, Profession, Specialization, Number, DepartmentType);
        }
    }
}
