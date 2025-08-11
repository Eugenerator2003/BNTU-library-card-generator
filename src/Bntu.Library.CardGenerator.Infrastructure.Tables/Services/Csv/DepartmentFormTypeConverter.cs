using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bntu.Library.CardGenerator.Domain.ValueObjects.StudyGroup;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class DepartmentFormTypeConverter : DefaultTypeConverter
    {
        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is DepartmentType type)
            {
                return type switch
                {
                    DepartmentType.Fulltime => "очная",
                    DepartmentType.Correspondence => "заочная",
                    _ => ""
                };
            }
            return base.ConvertToString(value, row, memberMapData);
        }
    }
}
