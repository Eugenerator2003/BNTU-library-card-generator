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

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx
{
    public class XlsxExportedInformationService : IExportedInformationService
    {
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

        private static readonly Dictionary<string, int> studentFieldIndices = studentFields
            .Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);

        private static readonly Dictionary<string, int> studyFieldIndices = studyFields
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
                    var rowData = GetFromRow(table.Rows[i]);
                    list.Add(rowData);
                }
                return ApplicationResult<IList<ExportedInformation>>.Success(list, "Экспортированные данные успешно загружены.");
            }
            catch (Exception ex)
            {
                return ApplicationResult<IList<ExportedInformation>>.Error($"Невозможно прочитать данные из файла {filename}. Строка данных - {i}: " +
                                                                           $"{ex.GetType()}:{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                reader.Dispose();
                stream.Dispose();
            }
        }

        private ExportedInformation GetFromRow(DataRow row)
        {
            var id = (int)row["#"];
            var student = GetStudentFromRow(row);
            var studyInformation = GetStudyFromRow(row);
            var contactInformation = GetContactFromRow(row);
            var photo = (string)row["Фотография"];

            var information = new ExportedInformation(id, student, studyInformation, contactInformation, photo);
            return information;
        }

        private ExportedStudentInformation GetStudentFromRow(DataRow row)
        {
            string? lastname = row[studentFieldIndices["Фамилия"]]?.ToString();
            string? firstname = row[studentFieldIndices["Имя"]]?.ToString();
            string? patronymic = row[studentFieldIndices["Отчество"]]?.ToString();

            if (string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(firstname) && string.IsNullOrEmpty(patronymic))
            {
                throw new InvalidDataException("Поля фамилии, имени и отчества не могут быть пустыми одновременно");
            }

            string? dateTimeString = row[studentFieldIndices["Дата рождения"]]?.ToString();
            if (!(!string.IsNullOrEmpty(dateTimeString) && DateTime.TryParse(dateTimeString, out DateTime birthDate)))
            {
                birthDate = new DateTime(1900, 1, 1);
            }

            string? passportNumber = row[studentFieldIndices["Серия и номер паспорта"]]?.ToString();
            string? passportInfo = row[studentFieldIndices["Кем выдан паспорт"]]?.ToString();

            string? sexString = row[studentFieldIndices["Пол"]]?.ToString();
            Person.SexType sexType;
            if (sexString == "девушка")
            {
                sexType = Person.SexType.Female;
            }
            else
            {
                sexType = Person.SexType.Male;
            }

            var student = new XlsxExportedStudentInformation(lastname ?? "", firstname ?? "", patronymic ?? "",
                                                             birthDate, passportNumber ?? "", passportInfo ?? "", sexType);
            return student;
        }

        private ExportedStudyInformation GetStudyFromRow(DataRow row)
        {
            string? studyStartString = row[studyFieldIndices["Дата начала обучения"]]?.ToString();
            string? studyEndString = row[studyFieldIndices["Дата окончания обучения"]]?.ToString();
            if (string.IsNullOrEmpty(studyEndString) || string.IsNullOrEmpty(studyStartString) ||
                !DateTime.TryParse(studyStartString, out DateTime studyDate) ||
                !DateTime.TryParse(studyEndString, out DateTime graduationDate))
                throw new InvalidDataException("Даты начала и окончания обучения не могут быть пустыми и должны быть в формате ГГГГ-ММ-ДД");

            string? faculty = row[studyFieldIndices["Факультет"]]?.ToString();
            string? specialization = row[studyFieldIndices["Код специальности"]]?.ToString();
            if (string.IsNullOrEmpty(faculty) || string.IsNullOrEmpty(specialization))
                throw new InvalidDataException("Факультет или специальность не могут быть пустыми.");

            string? group = row[studyFieldIndices["Номер группы"]]?.ToString();
            string? departmentString = row[studyFieldIndices["Форма обучения"]]?.ToString();
            if (string.IsNullOrEmpty(group) || string.IsNullOrEmpty(departmentString))
                throw new InvalidDataException("Группа или отделение не могут быть пустыми.");

            StudyGroup.DepartmentType department = departmentString == "дневное" ?
                                                    StudyGroup.DepartmentType.Fulltime :
                                                    StudyGroup.DepartmentType.Correspondence;

            var study = new XlsxExportedStudyInformation(studyDate, graduationDate, faculty, specialization, group, department);
            return study;
        }

        private ExportedContactInformation GetContactFromRow(DataRow row)
        {
            string? email = row["E-mail"]?.ToString();
            List<string> phones = new List<string>();
            string? phonesLine = row["Телефоны"]?.ToString();
            if (!string.IsNullOrEmpty(phonesLine))
            {
                foreach (var phoneS in phonesLine.Split(","))
                {
                    var phone = phoneS.Trim()
                                      .Replace("(", "")
                                      .Replace(")", "")
                                      .Replace("+", "")
                                      .Replace("-", "");
                    phones.Add(phone);
                }
            }

            string? cityType = row[XlsxOperations.ColumnLetterToIndex("AH")].ToString();
            string? city = row["Город"].ToString();
            if (string.IsNullOrEmpty(cityType) || cityType.Equals("Не указан") || string.IsNullOrEmpty(city))
            {
                return new XlsxExportedContactInformation(null, null, null, null, null, null, null, phones, email);
            }

            string? streetType = row[XlsxOperations.ColumnLetterToIndex("AJ")].ToString();
            string? street = row["Улица"]?.ToString();
            string? house = row["Дом"]?.ToString();
            string? building = row["Корпус"]?.ToString();
            string? apartment = row["Квартира"].ToString();

            var contact = new XlsxExportedContactInformation(cityType, city, streetType, street,
                                                            house, building, apartment, phones, email);
            return contact;
        }
    }
}
