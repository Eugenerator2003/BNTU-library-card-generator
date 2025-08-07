using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class ImportInformation
    {
        public int Codbar { get; }
        
        public ImportStudentInformation Student { get; }

        public ImportContactInformation Contact { get; }

        public ImportStudyGroup Group { get; }

        public LibraryRegistration LibraryRegistration { get; }

        public ImportInformation(int codbar, ImportStudentInformation student, ImportContactInformation contact, ImportStudyGroup group, LibraryRegistration libraryRegistration)
        {
            Codbar = codbar;
            Student = student;
            Contact = contact;
            Group = group;
            LibraryRegistration = libraryRegistration;
        }

        public override bool Equals(object? obj)
        {
            return obj is ImportInformation information &&
                   Codbar == information.Codbar &&
                   EqualityComparer<ImportStudentInformation>.Default.Equals(Student, information.Student) &&
                   EqualityComparer<ImportContactInformation>.Default.Equals(Contact, information.Contact) &&
                   EqualityComparer<ImportStudyGroup>.Default.Equals(Group, information.Group) &&
                   EqualityComparer<LibraryRegistration>.Default.Equals(LibraryRegistration, information.LibraryRegistration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Codbar, Student, Contact, Group, LibraryRegistration);
        }
    }
}
