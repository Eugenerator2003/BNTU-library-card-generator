using Bntu.Library.CardGenerator.Application.StudentsImport.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates
{
    public class ProfessionCollection : DictionaryCollection<Profession>
    {
        public Profession? this[int id]
        {
            get
            {
                return values.FirstOrDefault(p => p.Id == id);
            }
        }

        public Profession? this[string number]
        {
            get
            {
                return values.FirstOrDefault(v => SpecializationNumberComparer.Compare(number, v.Name));
            }
        }

        protected override bool Contains(Profession item)
        {
            return values.Any(v => v.Id == item.Id || (v.Name == item.Name && !string.IsNullOrEmpty(v.Name)));
        }
    }
}
