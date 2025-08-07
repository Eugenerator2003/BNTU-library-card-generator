using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export
{
    public abstract class ExportedStudentInformation
    {
        public string Lastname { get; }

        public string Firstname { get; }

        public string Patronymic { get; }

        public DateTime BirthDate { get; }

        public string PassportNumber { get; }

        public string PassportInfo { get; }

        public Person.SexType Sex { get; }

        public ExportedStudentInformation(string lastname, string firstname, string patronymic,
                                          DateTime birthDate, string passportNumber, string passportInfo, Person.SexType sex)
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
