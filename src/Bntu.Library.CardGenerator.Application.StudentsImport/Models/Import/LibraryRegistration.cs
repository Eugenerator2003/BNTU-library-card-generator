using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class LibraryRegistration
    {
        public DateTime RegistrationDate { get; }

        public DateTime ExpirationDate { get; }

        public LibraryRegistration(DateTime registrationDate, DateTime expirationDate)
        {
            RegistrationDate = registrationDate;
            ExpirationDate = expirationDate;
        }
    }
}
