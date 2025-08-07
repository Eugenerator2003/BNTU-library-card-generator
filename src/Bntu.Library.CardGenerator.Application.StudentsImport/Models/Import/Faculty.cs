using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class Faculty
    {
        public string Name { get; }

        public Faculty(int id, string name)
        {
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Faculty faculty &&
                   Name == faculty.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
