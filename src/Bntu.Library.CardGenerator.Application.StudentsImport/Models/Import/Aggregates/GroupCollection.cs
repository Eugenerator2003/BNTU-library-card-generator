using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates
{
    public class GroupCollection : DictionaryCollection<ImportStudyGroup>
    {
        public ImportStudyGroup? this[string number]
        {
            get
            {
                return values.FirstOrDefault(v => v.Number == number);
            }
        }

        protected override bool Contains(ImportStudyGroup item)
        {
            return values.Any(v => v.Number == item.Number);
        }
    }
}
