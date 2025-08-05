using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class ImportContactInformation
    {
        public string? City { get; }

        public string? Address { get; }

        public List<string> Phones { get; }

        public string? Email { get; }

        public ImportContactInformation(string? city, string? address, List<string> phones, string? email)
        {
            City = city;
            Address = address;
            Phones = new List<string>(phones);
            Email = email;
        }

    }
}
