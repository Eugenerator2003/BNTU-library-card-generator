using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models
{
    public abstract class ExportedInformation
    {
        public int Id { get; }

        public ExportedStudentInformation Student { get; }

        public ExportedStudyInformation StudyInformation { get; }

        public ExportedContactInformation ContactInformation { get; }

        protected ExportedInformation(int id, ExportedStudentInformation student, ExportedStudyInformation studyInformation, ExportedContactInformation contactInformation)
        {
            Id = id;
            Student = student;
            StudyInformation = studyInformation;
            ContactInformation = contactInformation;
        }
    }
}
