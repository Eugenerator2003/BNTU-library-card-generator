using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Util
{
    public class ApplicationResult<T>
    {
        public enum ResultType
        {
            ValidationError,
            Error,
            Success
        }

        public ResultType Type { get; }

        public T? Value { get; }

        public string Message { get; }

        public ApplicationResult(ResultType type, T? value, string? message)
        {
            Type = type;
            Value = value;
            Message = message ?? "";
        }
    }
}
