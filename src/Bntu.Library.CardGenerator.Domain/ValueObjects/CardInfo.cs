using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.ValueObjects
{
    public class CardInfo
    {
        public int CodbarValue { get; }

        public int PId { get; }

        public DateTime IssueDate { get; }

        public DateTime ExpirationDate { get; }

        public CardInfo(int codbarValue, int pId, DateTime issueDate, DateTime expirationDate)
        {
            CodbarValue = codbarValue;
            PId = pId;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
        }
    }
}
