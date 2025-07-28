using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Util
{
    public class ApplicationResult
    {
        public enum ResultType
        {
            ValidationError,
            Error,
            Success
        }

        public ResultType Type { get; }
    }
}
