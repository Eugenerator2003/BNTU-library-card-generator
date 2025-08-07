using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results
{
    public abstract class CaseResult
    {
        public bool Success { get; }

        public string Message { get; }

        public CaseResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
