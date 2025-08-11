using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Util
{
    public class XlsxOperations
    {
        public static int ColumnLetterToIndex(string column)
        {
            int index = 0;
            foreach (char c in column.ToUpper())
            {
                index *= 26;
                index += (c - 'A' + 1);
            }
            return index - 1;
        }
    }
}
