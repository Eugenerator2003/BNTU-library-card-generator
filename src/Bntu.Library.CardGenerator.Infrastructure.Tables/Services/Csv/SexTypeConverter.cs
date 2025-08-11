using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bntu.Library.CardGenerator.Application.StudentsImport.Models.Person;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class SexTypeConverter : DefaultTypeConverter
    {
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is SexType sex)
            {
                return sex switch
                {
                    SexType.Male => "мужской",
                    SexType.Female => "женский",
                    _ => ""
                };
            }
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}
