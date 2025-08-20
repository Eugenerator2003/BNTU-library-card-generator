using Bntu.Library.CardGenerator.Application.StudentsImport.Util;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export
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
            if (specialization is null)
                Specialization = "";
            else if (long.TryParse(specialization, out long spec)) {
                Specialization = specialization;
            }
            else if (long.TryParse(SpecializationNumberComparer.GetFormattedNumber(specialization), out spec))
            {
                Specialization = spec.ToString();
            }
            else
            {
                Specialization = SpecializationNumberComparer.GetSpecializationNumber(specialization) ?? "";
            }
            GroupNumber = groupNumber;
            Department = department;
        }
    }
}
