using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Util
{
    public class ApplicationResult<T> where T : class
    {
        public enum ResultType
        {
            ValidationError,
            Error,
            Success
        }

        public static ApplicationResult<T> ValidationError(string? message)
        {
            return new ApplicationResult<T>(ResultType.ValidationError, null, message);
        }

        public static ApplicationResult<T> Error(string? message)
        {
            return new ApplicationResult<T>(ResultType.Error, null, message);
        }

        public static ApplicationResult<T> Success(T? value, string? message)
        {
            return new ApplicationResult<T>(ResultType.Success, value, message);
        }

        public ResultType Type { get; }

        public T? Value { get; }

        public string Message { get; }

        public bool IsSuccess => Type == ResultType.Success;

        public bool IsOperationError => Type == ResultType.Error;
        
        public bool IsValidationError => Type == ResultType.ValidationError;

        public ApplicationResult(ResultType type, T? value, string? message)
        {
            Type = type;
            Value = value;
            Message = message ?? "";
        }
    }
}
