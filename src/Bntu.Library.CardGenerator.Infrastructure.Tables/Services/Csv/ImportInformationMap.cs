using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class ImportInformationMap : ClassMap<ImportInformation>
    {
        public ImportInformationMap()
        {
            var formConverter = new DepartmentFormTypeConverter();

            Map(m => m.Photoname).Name("Фото").Index(0);
            Map(m => m.Codbar).Name("Штрих-код").Index(1);
            Map(m => m.Group.Faculty.Name).Name("Факультет").Index(2);
            Map(m => m.Student.Lastname).Name("Фамилия").Index(3);
            Map(m => m.Student.GivenName).Name("Имя Отчество").Index(4);
            Map(m => m.Student.BirthDate).Name("Дата рождения").Index(5).TypeConverterOption.Format("dd.MM.yyy");
            Map(m => m.LibraryRegistration.RegistrationDate).Name("Дата записи в библиотеку").Index(6).TypeConverterOption.Format("dd.MM.yyy");
            Map(m => m.Group.GraduationDate).Name("Дата окончания обучения").Index(7).TypeConverterOption.Format("dd.MM.yyy");
            Map(m => m.Student.PassportInfromation).Name("Примечания (паспортные данные)").Index(8);
            Map(m => m.Group.Number).Name("Примечания ( продолж. ) (номер группы)").Index(9);
            Map(m => m.Group.Profession).Name("Профессия / Специальность / Направление").TypeConverter<ProfessionConverter>().Index(10);

            Map(m => m.Group.DepartmentType)
                .Name("Группа")
                .TypeConverter<DepartmentGroupTypeConverter>()
                .Index(11);

            // выстрел в ноги от csvhelper
            Map(m => m.Group)
                .Convert(args =>
                {
                    var dept = args.Value.Group.DepartmentType;
                    return formConverter.ConvertToString(dept, null!, null!);
                })
                .Name("Форма обучения")
                .Index(12);

            Map(m => m.Group.Specialization).Name("Профиль / Специализация").TypeConverter<SpecializationConverter>().Index(13);
            Map(m => m.Student.Sex).Name("Пол").TypeConverter<SexTypeConverter>().Index(14);
            Map(m => m.LibraryRegistration).Name("Курс/стаж").TypeConverter<LibraryRegistrationTypeConverter>().Index(15);
            Map(m => m.Contact.Address).Name("Адрес").Index(16);
            Map(m => m.Contact.City).Name("Город").Index(17);
            Map(m => m.Contact).Name("Мобильный телефон").TypeConverter<ContactPhonesConverter>().Index(18);
            Map(m => m.Contact.Email).Name("E-Mail").Index(19);
        }
    }
}
