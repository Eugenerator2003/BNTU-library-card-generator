using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models
{
    public class LibraryRegistration
    {
        public DateTime RegistrationDate { get; }

        public LibraryRegistration(DateTime registrationDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}
