using Bntu.Library.CardGenerator.Domain.Entities;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests
{
    internal class RandomReaderCardGenerator
    {
        private static Random random = new Random();

        public static ReaderCard GenerateRandom(string? group = null)
        {
            int codbar = random.Next();
            int pid = random.Next();
            int issueDays = random.Next(365 * 2);
            int expirationDays = random.Next(365 * 2);

            CardInfo cardInfo = new CardInfo(codbar, pid, DateTime.Now.AddDays(-issueDays), DateTime.Now.AddDays(expirationDays));

            Faculty faculty = new Faculty(GetStringLine());
            StudyGroup.DepartmentType type = random.Next(2) == 0 ? StudyGroup.DepartmentType.Correspondence :
                                                                   StudyGroup.DepartmentType.Fulltime;
            StudyGroup studyGroup = new StudyGroup(group ?? GetStringLine(), type);

            Student student = new Student(GetStringLine(), GetStringLine(), faculty, studyGroup);
            Photo photo = new Photo(GetStringLine());
            return new ReaderCard(cardInfo, student, photo);
        }

        private static string GetStringLine()
        {
            int count = random.Next(1, 50);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(Convert.ToChar(random.Next(char.MaxValue)));
            }
            return sb.ToString();
        }
    }
}
