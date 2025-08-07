using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class ImportingParameter
    {
        public DateTime CurrentYear { get; }

        public int StartCodbar { get; }

        public ImportingParameter(DateTime currentYear, int startCodbar)
        {
            CurrentYear = currentYear;
            StartCodbar = startCodbar;
        }

        public override bool Equals(object? obj)
        {
            return obj is ImportingParameter parameter &&
                   CurrentYear == parameter.CurrentYear &&
                   StartCodbar == parameter.StartCodbar;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CurrentYear, StartCodbar);
        }
    }
}
