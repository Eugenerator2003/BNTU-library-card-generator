using Bntu.Library.CardGenerator.Application.StudentsExport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models
{
    public class ImportStudentInformation
    {
        public Person.SexType Sex { get; }

        public string Lastname { get; }

        public string GivenName { get; }

        public DateTime BirthDate { get; }

        public string PassportInfromation { get; }

        public ImportStudentInformation(Person.SexType set, string lastname, string givenName, DateTime birthDate, string passportInfromation)
        {
            Sex = set;
            Lastname = lastname;
            GivenName = givenName;
            BirthDate = birthDate;
            PassportInfromation = passportInfromation;
        }
    }
}
