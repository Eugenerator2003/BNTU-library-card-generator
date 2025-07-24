using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class Photo
    {
        public string Name { get; }

        public Photo(string name)
        {
            Name = name;
        }
    }
}
