using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Domain.Entities
{
    public class ReaderCard
    {
        public int CodbarValue => cardInfo.CodbarValue;

        public int PId => cardInfo.PId;

        public DateTime IssueDate => cardInfo.IssueDate;

        public DateTime ExpirationDate => cardInfo.ExpirationDate;

        public string Lastname => student.Lastname;

        public string Firstname => student.Firstname;

        public string Faculty => student.Faculty.Name;

        public string StudyGroup => student.StudyGroup.Number;

        public StudyGroup.DepartmentType Department => student.StudyGroup.Department;

        public string PhotoName => photo.Name;

        private readonly CardInfo cardInfo;
        private readonly Student student;
        private readonly Photo photo;

        public ReaderCard(CardInfo cardInfo, Student student, Photo photo)
        {
            this.cardInfo = cardInfo;
            this.student = student;
            this.photo = photo;
        }
    }
}
