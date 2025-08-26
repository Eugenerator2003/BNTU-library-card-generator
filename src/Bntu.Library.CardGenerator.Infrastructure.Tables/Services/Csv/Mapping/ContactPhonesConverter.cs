using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping
{
    public class ContactPhonesConverter : DefaultTypeConverter
    {
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is ImportContactInformation contact)
            {
                return contact.Phones.FirstOrDefault() ?? "";
            }
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}