using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models.Import
{
    public class ImportStudyGroup
    {
        public Faculty Faculty { get; }

        public Specialization Specialization { get; }

        public string Number { get; }

        public StudyGroup.DepartmentType DepartmentType { get; }

        public DateTime GraduationDate { get; }

        public ImportStudyGroup(Faculty faculty, Specialization specialization, string number, StudyGroup.DepartmentType departmentType, DateTime graduationDate)
        {
            Faculty = faculty;
            Specialization = specialization;
            Number = number;
            DepartmentType = departmentType;
            GraduationDate = graduationDate;
        }

        public override bool Equals(object? obj)
        {
            return obj is ImportStudyGroup group &&
                   EqualityComparer<Faculty>.Default.Equals(Faculty, group.Faculty) &&
                   EqualityComparer<Specialization>.Default.Equals(Specialization, group.Specialization) &&
                   Number == group.Number &&
                   DepartmentType == group.DepartmentType &&
                   GraduationDate == group.GraduationDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Faculty, Specialization, Number, DepartmentType, GraduationDate);
        }
    }
}
