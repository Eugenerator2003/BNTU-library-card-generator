using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public class ExportReaderCardsCaseResult
    {
        public bool IsSuccess { get; }
        
        public string Message { get; }

        public ExportReaderCardsCaseResult(bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message ?? "";
        }
    }
}
