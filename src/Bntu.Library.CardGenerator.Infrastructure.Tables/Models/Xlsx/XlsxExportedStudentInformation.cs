using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx
{
    public class XlsxExportedStudentInformation : ExportedStudentInformation
    {
        public XlsxExportedStudentInformation(string lastname, string firstname, string patronymic, 
                                              DateTime birthDate, string passportNumber, string passportInfo, Person.SexType sex) 
            : base(lastname, firstname, patronymic, birthDate, passportNumber, passportInfo, sex)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is XlsxExportedStudentInformation information &&
                   Lastname == information.Lastname &&
                   Firstname == information.Firstname &&
                   Patronymic == information.Patronymic &&
                   BirthDate == information.BirthDate &&
                   PassportNumber == information.PassportNumber &&
                   PassportInfo == information.PassportInfo &&
                   Sex == information.Sex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lastname, Firstname, Patronymic, BirthDate, PassportNumber, PassportInfo, Sex);
        }
    }
}
