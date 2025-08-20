using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public abstract class LazyCaseResult : CaseResult
    {
        public new bool Success { get; private set; }

        public new string Message { get; private set; }

        public LazyCaseResult() : base(false, "")
        {
            Message = "";
        }

        public void SetSuccess(bool success)
        {
            Success = success;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }
    }
}
