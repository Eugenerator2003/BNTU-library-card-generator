using Bntu.Library.CardGenerator.Domain.Entities;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public abstract class GeneratedReaderCard
    {
        public int CodbarValue { get; }
        public int PId { get; }
        public DateTime IssueDate { get; }
        public DateTime ExpirationDate { get; }
        public string Lastname { get; }
        public string Firstname { get; }
        public string Faculty { get; }
        public string StudyGroup { get; }
        public StudyGroup.DepartmentType Department { get; }

        public GeneratedReaderCard(int codbarValue, int pId, DateTime issueDate, DateTime expirationDate, 
                                   string lastname, string firstname, string faculty, string studyGroup, StudyGroup.DepartmentType department)
        {
            CodbarValue = codbarValue;
            PId = pId;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Lastname = lastname;
            Firstname = firstname;
            Faculty = faculty;
            StudyGroup = studyGroup;
            Department = department;
        }

        public GeneratedReaderCard(ReaderCard readerCard)
            : this(readerCard.CodbarValue, readerCard.PId, readerCard.IssueDate,
                   readerCard.ExpirationDate, readerCard.Lastname, readerCard.Firstname,
                   readerCard.Faculty, readerCard.StudyGroup, readerCard.Department)
        {

        }
    }
}
