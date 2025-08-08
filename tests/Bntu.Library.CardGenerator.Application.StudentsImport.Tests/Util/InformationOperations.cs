using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Util;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Util
{
    public static class InformationOperations
    {
        public class TestStudent : ExportedStudentInformation
        {
            public TestStudent(string lastname, string firstname, string patronymic,
                               DateTime birthDate, string passportNumber, string passportInfo, Person.SexType sex)
                : base(lastname, firstname, patronymic, birthDate, passportNumber, passportInfo, sex)
            {
            }
        }

        public class TestContact : ExportedContactInformation
        {
            public string Address { get; }

            public TestContact(string city, string address, List<string> phones, string? email)
                : base(city, phones, email)
            {
                Address = address;
            }

            public override string GetAddress()
            {
                return Address;
            }
        }

        public class TestStudy : ExportedStudyInformation
        {
            public TestStudy(DateTime studyStartDate, DateTime graduationDate, string faculty,
                             string specialization, string groupNumber, StudyGroup.DepartmentType department)
                : base(studyStartDate, graduationDate, faculty, specialization, groupNumber, department)
            {
            }
        }

        private static Random Random = new Random();

        public static List<Profession> GetProfessions()
        {
            var list = new List<Profession>();
            for (int i = 0; i < Random.Next(30, 50); i++)
            {
                list.Add(new Profession(i, GetFormattedSpecializationName()));
            }
            return list;
        }

        public static ProfessionCollection GetProfessionCollection(List<Profession> professions)
        {
            var collection = new ProfessionCollection();
            foreach (var p in professions)
            {
                collection.Add(p);
            }
            return collection;
        }

        public static List<Specialization> GetSpecializations(List<Profession> professions)
        {
            var list = new List<Specialization>();
            for (int i = 0; i < Random.Next(80, 250); i++)
            {
                list.Add(new Specialization(i, GetFormattedSpecializationName(), Random.Next(0, professions.Count)));
            }
            return list;
        }

        public static SpecializationCollection GetSpecializationCollection(List<Specialization> specializations)
        {
            var collection = new SpecializationCollection();
            foreach (var s in specializations)
            {
                collection.Add(s);
            }
            return collection;
        }

        public static string GetRandomSpecializationName(List<Specialization> specializations)
        {
            if (Random.Next(3) == 0)
                return "";
            return specializations[Random.Next(specializations.Count)].Name;
        }


        public static ExportedInformation GetRandomExportedInformation(List<Specialization> specializations)
        {
            var student = new TestStudent(GetLine(), GetLine(), GetLine(), GetDateBefore(2003, 900),
                                          GetLine(), GetLine(), Random.Next(2) == 1 ? Person.SexType.Male : Person.SexType.Female);
            var contact = new TestContact(GetLine(), GetLine(), GetPhones(), GetLine());
            var study = new TestStudy(GetDateAfter(DateTime.Now.Year, DateTime.Now.DayOfYear), GetDateAfter(DateTime.Now.Year + 4, 365),
                                      GetLine(), GetRandomSpecializationName(specializations), GetLine(),
                                      Random.Next(2) == 1 ? StudyGroup.DepartmentType.Fulltime : StudyGroup.DepartmentType.Correspondence);

            var info = new ExportedInformation(Random.Next(1, 100), student, study, contact, GetLine());
            return info;
        }

        public static void AssertingEquals(int expectedCodbar, ExportedInformation expected, ImportInformation transformed, List<Profession> professions)
        {
            Assert.AreEqual(expectedCodbar, transformed.Codbar);

            Assert.AreEqual(expected.Student.Lastname, transformed.Student.Lastname);
            Assert.AreEqual($"{expected.Student.Firstname} {expected.Student.Patronymic}", transformed.Student.GivenName);
            Assert.AreEqual($"{expected.Student.PassportNumber} {expected.Student.PassportInfo}", transformed.Student.PassportInfromation);
            Assert.AreEqual(expected.Student.BirthDate, transformed.Student.BirthDate);
            Assert.AreEqual(expected.Student.Sex, transformed.Student.Sex);

            Assert.AreEqual(expected.ContactInformation.City, transformed.Contact.City);
            Assert.AreEqual(expected.ContactInformation.Email, transformed.Contact.Email);
            Assert.AreEqual(expected.ContactInformation.GetAddress(), transformed.Contact.Address);
            CollectionAssert.AreEquivalent(expected.ContactInformation.Phones, transformed.Contact.Phones);

            DateTime startStudyDate = expected.StudyInformation.StudyStartDate.Date;

            Assert.AreEqual($"{expected.StudyInformation.Faculty}-{startStudyDate.Year % 2000}", transformed.Group.Faculty.Name);
            Assert.AreEqual(expected.StudyInformation.GroupNumber, transformed.Group.Number);
            Assert.AreEqual(expected.StudyInformation.Specialization, SpecializationNumberComparer.GetSpecializationNumber(transformed.Group.Specialization?.Name ?? "") ?? "");
            if (transformed.Group.Specialization != null)
                Assert.AreEqual(professions.FirstOrDefault(p => p.Id == transformed.Group.Specialization.ProfessionId)?.Name,
                                transformed.Group.Profession?.Name);
            Assert.AreEqual(new DateTime(startStudyDate.Year, 9, 1),
                            transformed.LibraryRegistration.RegistrationDate);
            Assert.AreEqual(new DateTime(expected.StudyInformation.GraduationDate.Year, 9, 1),
                            transformed.Group.GraduationDate);
            Assert.AreEqual(expected.StudyInformation.Department, transformed.Group.DepartmentType);
        }

        private static string GetLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Random.Next(1, 51); i++)
            {
                sb.Append(Convert.ToChar(Random.Next(1, char.MaxValue)));
            }
            return sb.ToString();
        }

        private static string GetFormattedSpecializationName()
        {
            var prefixes = new[]
            {
                "Инженер", "Техник", "Механик", "Программист", "Экономист",
                "Архитектор", "Проектировщик", "Метролог", "Энергетик"
            };

            var suffixes = new[]
            {
            "механического цеха", "по оборудованию", "по качеству", "по автоматизации",
            "программист", "по ремонту", "дистанционно", "на предприятии"
            };

            var prefix = prefixes[Random.Next(prefixes.Length)];
            var suffix = suffixes[Random.Next(suffixes.Length)];

            var name = $"{prefix} ({suffix})";

            var code = $"{Random.Next(10, 99)} {Random.Next(10, 99)}-{Random.Next(10, 99)}";

            return $"{name} {code}";
        }

        private static DateTime GetDateBefore(int year, int maxDaysBefore)
        {
            DateTime dt = new DateTime(year, 1, 1);
            return dt.AddDays(-Random.Next(1, maxDaysBefore));
        }

        private static DateTime GetDateAfter(int year, int maxDaysAfter)
        {
            DateTime dt = new DateTime(year, 1, 1);
            return dt.AddDays(Random.Next(1, maxDaysAfter));
        }

        private static List<string> GetPhones()
        {
            var phones = new List<string>();
            for (int i = 0; i < Random.Next(0, 4); i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < 10; j++)
                {
                    sb.Append(Random.Next(0, 10).ToString());
                }
                phones.Add(sb.ToString());
            }
            return phones;
        }
    }
}
