using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx
{
    internal class XlsxExportedContactInformation : ExportedContactInformation
    {
        public string? CityType { get; }

        public override string City
        {
            get
            {
                if (string.IsNullOrEmpty(CityType))
                {
                    return "Не указан в базе данны";
                }
                else
                {
                    return CityType + _city;
                }
            }
        }

        public string? StreetType { get; }

        public string? Street { get; }

        public string? House { get; }

        public string? Building { get; }
        
        public string? Appartment { get; }

        private string? _city;

        public XlsxExportedContactInformation(string? cityType, string? city, string? streetType, string? street, 
                                              string? house, string? building, string? appartment, List<string> phones, string? email) 
            : base(city, phones, email)
        {
            _city = city;
            CityType = cityType;
            StreetType = streetType;
            Street = street;
            House = house;
            Building = building;
            Appartment = appartment;
        }

        public override string GetAddress()
        {
            if (!string.IsNullOrEmpty(_city) || !string.IsNullOrEmpty(CityType))
            {
                string building = Building is null 
                                    ? "" 
                                    : $", корп. {Building}";
                return $"{StreetType} {Street}, д. {House}{building}, кв. {Appartment}";
            }
            else
            {
                return "Не указан в базе данных";
            } 
                
        }
    }
}
