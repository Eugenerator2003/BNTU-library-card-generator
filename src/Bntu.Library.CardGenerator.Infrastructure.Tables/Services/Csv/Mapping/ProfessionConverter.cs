using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv.Mapping
{
    public class ProfessionConverter : DefaultTypeConverter
    {
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is Profession profession)
            {
                return profession.Name;
            }
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}
