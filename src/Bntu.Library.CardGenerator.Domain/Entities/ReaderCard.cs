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

        public override bool Equals(object? obj)
        {
            return obj is ReaderCard card &&
                   CodbarValue == card.CodbarValue &&
                   PId == card.PId &&
                   IssueDate == card.IssueDate &&
                   ExpirationDate == card.ExpirationDate &&
                   Lastname == card.Lastname &&
                   Firstname == card.Firstname &&
                   Faculty == card.Faculty &&
                   StudyGroup == card.StudyGroup &&
                   Department == card.Department &&
                   PhotoName == card.PhotoName &&
                   EqualityComparer<CardInfo>.Default.Equals(cardInfo, card.cardInfo) &&
                   EqualityComparer<Student>.Default.Equals(student, card.student) &&
                   EqualityComparer<Photo>.Default.Equals(photo, card.photo);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(CodbarValue);
            hash.Add(PId);
            hash.Add(IssueDate);
            hash.Add(ExpirationDate);
            hash.Add(Lastname);
            hash.Add(Firstname);
            hash.Add(Faculty);
            hash.Add(StudyGroup);
            hash.Add(Department);
            hash.Add(PhotoName);
            hash.Add(cardInfo);
            hash.Add(student);
            hash.Add(photo);
            return hash.ToHashCode();
        }
    }
}
