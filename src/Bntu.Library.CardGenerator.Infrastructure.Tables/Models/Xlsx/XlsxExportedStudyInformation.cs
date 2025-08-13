using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx
{
    public class XlsxExportedStudyInformation : ExportedStudyInformation
    {
        public XlsxExportedStudyInformation(DateTime studyStartDate, DateTime graduationDate, string faculty,
                                            string specialization, string groupNumber, StudyGroup.DepartmentType department) 
            : base(studyStartDate, graduationDate, faculty, specialization, groupNumber, department)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is XlsxExportedStudyInformation information &&
                   StudyStartDate == information.StudyStartDate &&
                   GraduationDate == information.GraduationDate &&
                   Faculty == information.Faculty &&
                   Specialization == information.Specialization &&
                   GroupNumber == information.GroupNumber &&
                   Department == information.Department;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StudyStartDate, GraduationDate, Faculty, Specialization, GroupNumber, Department);
        }
    }
}
