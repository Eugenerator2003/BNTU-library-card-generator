using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export
{
    public abstract class ExportedContactInformation
    {
        public virtual string City { get; }

        public List<string> Phones { get; }

        public string? Email { get; }

        public ExportedContactInformation(string city, List<string> phones, string? email)
        {
            City = city;
            Phones = new List<string>(phones);
            Email = email;
        }

        public abstract string GetAddress();
    }
}
