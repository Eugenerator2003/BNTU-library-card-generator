using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsExport.Models.Import
{
    public class Specialization
    {
        public string Name { get; }

        public Specialization(string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Specialization specialization &&
                   Name == specialization.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
