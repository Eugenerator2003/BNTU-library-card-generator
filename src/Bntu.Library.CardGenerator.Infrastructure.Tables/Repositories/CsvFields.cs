using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public class CsvFields
    {
        private string[] fields;
        private int index;

        public CsvFields(string[] fields)
        {
            this.fields = fields;
            index = -1;
        }

        public string CurrentField()
        {
            if (index == -1)
                return "";
            return fields[index];
        }

        public string NextField()
        {
            if (index + 1 == fields.Length)
                index = -1;
            return fields[++index];
        }
    }
}
