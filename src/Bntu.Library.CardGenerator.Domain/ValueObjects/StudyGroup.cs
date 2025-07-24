using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class StudyGroup
    {
        public string Number { get; }

        public StudyGroup(string number)
        {
            Number = number;
        }
    }
}
