using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Domain.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bntu.Library.CardGenerator.Domain.ValueObjects;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public class ReaderCardCvsRepository : BaseCsvReadRepository<ReaderCard>, IReaderCardRepository
    {
        private static readonly string[] Fields =
        {
            "Штрих-код",
            "Дата записи в библиотеку",
            "Дата окончания обучения",
            "Фамилия",
            "Имя Отчество",
            "Факультет",
            "Примечания ( продолж.) (номер группы)",
            "Группа",
            "Фото"
        };

        public ReaderCardCvsRepository(string filename)
            : base(filename)
        {

        }

        public IList<ReaderCard> GetAllCards()
        {
            return GetAllItems(a => true, false);
        }

        public ReaderCard? GetCardByCodbar(int codbar)
        {
            return GetAllItems(r => int.Parse(r.GetField(Fields[0]) ?? "") == codbar, true).FirstOrDefault();
        }

        public ReaderCard? GetCardByPid(int pid)
        {
            return GetCardByCodbar(pid);
        }

        public IList<ReaderCard> GetCardsByGroup(string groupNumber)
        {
            return GetAllItems(r => (r.GetField(Fields[6]) ?? "") == groupNumber, false);
        }

        protected override CsvFields GetCsvFields()
        {
            return new CsvFields(Fields);
        }

        protected override ReaderCard ReadFromCurrentLine()
        {
            var codbar = int.Parse(CsvReader.GetField(NextField()) ?? "");
            var issueDate = DateTime.Parse(CsvReader.GetField(NextField()) ?? "");
            var expirationDate = DateTime.Parse(CsvReader.GetField(NextField()) ?? "");

            var lastname = CsvReader.GetField(NextField());
            if (string.IsNullOrEmpty(lastname))
                throw new Exception($"Фамилия не может быть пустой");

            var firstname = CsvReader.GetField(NextField());
            if (string.IsNullOrEmpty(firstname))
                throw new Exception($"Имя не может быть пустым");

            var facultyName = CsvReader.GetField(NextField());
            if (string.IsNullOrEmpty(facultyName))
                throw new Exception($"Факультет не может быть пустым");
            facultyName = facultyName.Split('-')[0];

            var group = CsvReader.GetField(NextField());
            if (string.IsNullOrEmpty(group))
                throw new Exception($"Факультет не может быть пустым");

            var departmentString = CsvReader.GetField(NextField());
            var department = departmentString == "Студенты ДО" ?
                                StudyGroup.DepartmentType.Fulltime :
                                StudyGroup.DepartmentType.Correspondence;

            var photoName = CsvReader.GetField(NextField());
            if (string.IsNullOrEmpty(photoName))
                throw new Exception($"Путь к фото не может быть пустым");

            CardInfo cardInfo = new CardInfo(codbar, codbar, issueDate, expirationDate);
            StudyGroup studyGroup = new StudyGroup(group, department);
            Faculty faculty = new Faculty(facultyName);
            Student student = new Student(lastname, firstname, faculty, studyGroup);
            Photo photo = new Photo(photoName);
            ReaderCard card = new ReaderCard(cardInfo, student, photo);
            return card;
        }
    }
}
