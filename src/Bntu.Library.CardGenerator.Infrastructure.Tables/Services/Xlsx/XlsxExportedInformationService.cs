using ExcelDataReader;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Util;
using System.Text.RegularExpressions;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx
{
    public class XlsxExportedInformationService : IExportedInformationService
    {
        private static string RemoveTrash(string? value, bool ignoreSpace = true)
        {
            if (value is null)
                return "";
            
            StringBuilder sb = new StringBuilder(value);
            sb.Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace("-", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace(";", "");
            if (!ignoreSpace)
            {
                sb.Replace(" ", "");
            }
            return sb.ToString();
        }

        private static string RemoveLetters(string? value)
        {
            if (value is null)
                return "";

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetter(value[i]))
                {
                    sb.Append(value[i]);
                }
            }
            return sb.ToString();
        }

        private static string[] studentFields =
        [
            "Фамилия",
            "Имя",
            "Отчество",
            "Дата рождения",
            "Серия и номер паспорта",
            "Кем выдан паспорт",
            "Пол"
        ];

        private static string[] studyFields = [
            "Дата начала обучения",
            "Дата окончания обучения",
            "Факультет",
            "Код специальности",
            "Номер группы",
            "Форма обучения"
        ];

        private static string[] contactFields = [
            "E-mail",
            "Телефоны",
            "Город",
            "Улица",
            "Дом",
            "Корпус",
            "Квартира"
        ];

        private static readonly Dictionary<string, int> studentFieldIndices = studentFields
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

        private static readonly Dictionary<string, int> studyFieldIndices = studyFields
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

        private static readonly Dictionary<string, int> contactFieldIndices = contactFields
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

        private string filename;

        public XlsxExportedInformationService(string filename)
        {
            this.filename = filename;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public ApplicationResult<IList<ExportedInformation>> GetExportedInformation()
        {
            IExcelDataReader reader;
            FileStream stream;
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                reader = ExcelReaderFactory.CreateReader(stream);
            }
            catch (Exception ex)
            {
                return ApplicationResult<IList<ExportedInformation>>.Error($"Невозможно открыть файл {filename}: {ex.GetType()}:{ex.Message}\n{ex.StackTrace}");
            }

            var conf = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var result = reader.AsDataSet(conf);
            var table = result.Tables[0];

            int i = 0;
            try
            {
                var list = new List<ExportedInformation>();
                for (i = 0; i < table.Rows.Count; i++)
                {
                    var rowData = GetFromRow(table.Rows[i], list);
                    list.Add(rowData);
                }
                return ApplicationResult<IList<ExportedInformation>>.Success(list, "Экспортированные данные успешно загружены.");
            }
            catch (Exception ex)
            {
                return ApplicationResult<IList<ExportedInformation>>.Error($"Невозможно прочитать данные из файла {filename}. Строка данных - {i + 2}: " +
                                                                           $"{ex.GetType()}:{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                reader.Dispose();
                stream.Dispose();
            }
        }

        private ExportedInformation GetFromRow(DataRow row, List<ExportedInformation> context)
        {
            var id = int.Parse(row["#"].ToString()!);
            var student = GetStudentFromRow(row);
            var studyInformation = GetStudyFromRow(row, context);
            var contactInformation = GetContactFromRow(row);
            var photo = GetPhoto(row);

            var information = new ExportedInformation(id, student, studyInformation, contactInformation, photo);
            return information;
        }

        private ExportedStudentInformation GetStudentFromRow(DataRow row)
        {
            string? lastname = row[studentFields[studentFieldIndices["Фамилия"]]]?.ToString();
            string? firstname = row[studentFields[studentFieldIndices["Имя"]]]?.ToString();
            string? patronymic = row[studentFields[studentFieldIndices["Отчество"]]]?.ToString();

            if (string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(firstname) && string.IsNullOrEmpty(patronymic))
            {
                throw new InvalidDataException("Поля фамилии, имени и отчества не могут быть пустыми одновременно");
            }

            string? dateTimeString = row[studentFields[studentFieldIndices["Дата рождения"]]]?.ToString();
            if (string.IsNullOrEmpty(dateTimeString) ||
                !DateTime.TryParse(dateTimeString, out DateTime birthDate) || birthDate.Year < 1900)
            {
                birthDate = new DateTime(1900, 1, 1);
            }

            //if (birthDate.Year < 1900)
            //{
            //    birthDate = new DateTime(1900, 1, 1);
            //}

            string? passportNumber = row[studentFields[studentFieldIndices["Серия и номер паспорта"]]]?.ToString();
            string? passportInfo = row[studentFields[studentFieldIndices["Кем выдан паспорт"]]]?.ToString();

            string? sexString = row[studentFields[studentFieldIndices["Пол"]]]?.ToString();
            Person.SexType sexType;
            if (sexString == "девушка")
            {
                sexType = Person.SexType.Female;
            }
            else
            {
                sexType = Person.SexType.Male;
            }

            var student = new XlsxExportedStudentInformation(RemoveTrash(lastname), RemoveTrash(firstname), RemoveTrash(patronymic),
                                                             birthDate, RemoveTrash(passportNumber), RemoveTrash(passportInfo), sexType);
            return student;
        }

        private ExportedStudyInformation GetStudyFromRow(DataRow row, IList<ExportedInformation> context)
        {
            ExportedInformation? classmate = null;

            string? group = row[studyFields[studyFieldIndices["Номер группы"]]]?.ToString();
            string? departmentString = row[studyFields[studyFieldIndices["Форма обучения"]]]?.ToString();
            if (string.IsNullOrEmpty(group) || string.IsNullOrEmpty(departmentString))
                throw new InvalidDataException("Группа или отделение не могут быть пустыми.");

            string? studyStartString = row[studyFields[studyFieldIndices["Дата начала обучения"]]]?.ToString();
            string? studyEndString = row[studyFields[studyFieldIndices["Дата окончания обучения"]]]?.ToString();
            if (string.IsNullOrEmpty(studyEndString) || string.IsNullOrEmpty(studyStartString) ||
                !DateTime.TryParse(studyStartString, out DateTime studyDate) ||
                !DateTime.TryParse(studyEndString, out DateTime graduationDate))
            {
                classmate = context.FirstOrDefault(a => a.StudyInformation.GroupNumber == group);
                if (classmate == null)
                    throw new InvalidDataException("Даты начала и окончания обучения не могут быть пустыми и должны быть в формате ГГГГ-ММ-ДД");
                studyDate = classmate.StudyInformation.StudyStartDate;
                graduationDate = classmate.StudyInformation.GraduationDate;
            }

            string? faculty = row[studyFields[studyFieldIndices["Факультет"]]]?.ToString();
            string? specialization = row[studyFields[studyFieldIndices["Код специальности"]]]?.ToString();
            if (string.IsNullOrEmpty(faculty))
                throw new InvalidDataException("Факультет не может быть пустым.");
            if (string.IsNullOrEmpty(specialization))
            {
                specialization = classmate?.StudyInformation.Specialization;
            }

            StudyGroup.DepartmentType department = departmentString == "дневное" ?
                                                    StudyGroup.DepartmentType.Fulltime :
                                                    StudyGroup.DepartmentType.Correspondence;

            var study = new XlsxExportedStudyInformation(studyDate, graduationDate, RemoveTrash(faculty), RemoveTrash(specialization, false), RemoveTrash(group), department);
            return study;
        }

        private ExportedContactInformation GetContactFromRow(DataRow row)
        {
            string? email = row[contactFields[contactFieldIndices["E-mail"]]]?.ToString();
            List<string> phones = new List<string>();
            string? phonesLine = row[contactFields[contactFieldIndices["Телефоны"]]]?.ToString();
            if (!string.IsNullOrEmpty(phonesLine))
            {
                phonesLine = RemoveLetters(phonesLine);
                Regex regex = new Regex(@"(?:\+?375|8)\s*\(?\d{2}\)?\s*\d{3}\s*\d{2}\s*\d{2}\b");
                foreach (var phoneS in regex.Matches(phonesLine))
                {
                    var phone = RemoveTrash(phoneS.ToString(), false);
                    phones.Add(phone);
                }
            }

            string? cityType = row[XlsxOperations.ColumnLetterToIndex("AH")].ToString();
            string? city = row[contactFields[contactFieldIndices["Город"]]].ToString();
            if (string.IsNullOrEmpty(cityType) || cityType.Equals("Не указан") || string.IsNullOrEmpty(city))
            {
                return new XlsxExportedContactInformation(null, null, null, null, null, null, null, phones, email);
            }

            string? streetType = row[XlsxOperations.ColumnLetterToIndex("AJ")].ToString();
            string? street = row[contactFields[contactFieldIndices["Улица"]]]?.ToString();
            string? house = row[contactFields[contactFieldIndices["Дом"]]]?.ToString();
            string? building = row[contactFields[contactFieldIndices["Корпус"]]]?.ToString();
            string? apartment = row[contactFields[contactFieldIndices["Квартира"]]].ToString();

            var contact = new XlsxExportedContactInformation(RemoveTrash(cityType), RemoveTrash(city), RemoveTrash(streetType), 
                                                             RemoveTrash(street), RemoveTrash(house), RemoveTrash(building), RemoveTrash(apartment), phones, 
                                                             RemoveTrash(email));
            return contact;
        }

        private string GetPhoto(DataRow row)
        {
            var value = row["Фотография"];
            return value is DBNull ? "" : (string)value;
        }
    }
}
