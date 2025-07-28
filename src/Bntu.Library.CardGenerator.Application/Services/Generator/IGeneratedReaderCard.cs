using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public interface IGeneratedReaderCard
    {
        int CodbarValue { get; }
        int PId { get; }
        DateTime IssueDate { get; }
        DateTime ExpirationDate { get; }
        string Lastname { get; }
        string Firstname { get; }
        string Faculty { get; }
        string StudyGroup { get; }
        StudyGroup.DepartmentType Department { get; }
    }
}
