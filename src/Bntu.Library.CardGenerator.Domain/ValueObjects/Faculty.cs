using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class Faculty
    {
        public string Name { get; }

        public Faculty(string name)
        {
            Name = name;
        }
    }
}
