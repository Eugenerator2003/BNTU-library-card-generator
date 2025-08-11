using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlxs
{
    internal class XlxsExportedStudentInformation : ExportedStudentInformation
    {
        public XlxsExportedStudentInformation(string lastname, string firstname, string patronymic, 
                                              DateTime birthDate, string passportNumber, string passportInfo, Person.SexType sex) 
            : base(lastname, firstname, patronymic, birthDate, passportNumber, passportInfo, sex)
        {
        }
    }
}
