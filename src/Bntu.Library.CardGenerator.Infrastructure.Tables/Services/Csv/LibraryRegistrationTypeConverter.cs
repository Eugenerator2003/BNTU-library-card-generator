using CsvHelper.Configuration;
using CsvHelper;
using static Bntu.Library.CardGenerator.Domain.ValueObjects.StudyGroup;
using CsvHelper.TypeConversion;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class LibraryRegistrationTypeConverter : DefaultTypeConverter
    {
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is LibraryRegistration registration)
            {
                return registration.RegistrationDate.Year.ToString();
            }
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}