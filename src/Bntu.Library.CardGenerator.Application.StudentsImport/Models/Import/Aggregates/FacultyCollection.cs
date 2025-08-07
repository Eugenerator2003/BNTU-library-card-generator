using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates
{
    public class FacultyCollection : DictionaryCollection<Faculty>
    {
        public Faculty? this[string name]
        {
            get
            {
                return values.FirstOrDefault(v => v.Name == name);
            }
        }

        protected override bool Contains(Faculty item)
        {
            return values.Any(v => v.Name == item.Name);
        }
    }
}
