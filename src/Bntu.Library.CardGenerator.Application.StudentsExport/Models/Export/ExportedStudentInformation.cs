using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models.Export
{
    public abstract class ExportedStudentInformation
    {
        public enum SexTypes
        {
            Female,
            Male
        }

        public string Lastname { get; }

        public string Firstname { get; }

        public string Patronymic { get; }

        public DateTime BirthDate { get; }

        public string PassportNumber { get; }

        public string PassportInfo { get; }

        public SexTypes Sex { get; }

        public ExportedStudentInformation(string lastname, string firstname, string patronymic,
                                          DateTime birthDate, string passportNumber, string passportInfo, SexTypes sex)
        {
            Lastname = lastname;
            Firstname = firstname;
            Patronymic = patronymic;
            BirthDate = birthDate;
            PassportNumber = passportNumber;
            PassportInfo = passportInfo;
            Sex = sex;
        }
    }
}
