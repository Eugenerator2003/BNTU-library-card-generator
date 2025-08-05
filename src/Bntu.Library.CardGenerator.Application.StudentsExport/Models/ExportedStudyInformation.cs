using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models
{
    public abstract class ExportedStudyInformation
    {
        public DateTime StudyStartDate { get; }

        public DateTime GraduationDate { get; }

        public string Faculty { get; }

        public string Specialization { get; }

        public string GroupNumber { get; }

        public StudyGroup.DepartmentType Department { get; }

        public ExportedStudyInformation(DateTime studyStartDate, DateTime graduationDate, string faculty, string specialization, string groupNumber, StudyGroup.DepartmentType department)
        {
            StudyStartDate = studyStartDate;
            GraduationDate = graduationDate;
            Faculty = faculty;
            Specialization = specialization;
            GroupNumber = groupNumber;
            Department = department;
        }
    }
}
