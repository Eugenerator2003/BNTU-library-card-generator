using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx
{
    public class XlsxExportedContactInformation : ExportedContactInformation
    {
        public string? CityType { get; }

        public override string City
        {
            get
            {
                if (string.IsNullOrEmpty(CityType) || string.IsNullOrEmpty(_city))
                {
                    return "Не указан в базе данных";
                }
                else
                {
                    return _city;
                }
            }
        }

        public string? StreetType { get; }

        public string? Street { get; }

        public string? House { get; }

        public string? Building { get; }
        
        public string? Apartment { get; }

        private string? _city;

        public XlsxExportedContactInformation(string? cityType, string? city, string? streetType, string? street, 
                                              string? house, string? building, string? appartment, List<string> phones, string? email) 
            : base(city!, phones, email)
        {
            _city = city;
            CityType = cityType;
            StreetType = streetType;
            Street = street;
            House = house;
            Building = building;
            Apartment = appartment;
        }

        public override string GetAddress()
        {
            if (!string.IsNullOrEmpty(_city) && !string.IsNullOrEmpty(CityType))
            {
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrEmpty(StreetType))
                {
                    sb.Append(StreetType);
                    sb.Append(' ');
                    sb.Append(Street);
                    sb.Append(", ");
                }

                sb.Append($"д. {House}");

                if (string.IsNullOrEmpty(Building))
                {
                    sb.Append(", корпус");
                    sb.Append(Building);
                }

                if (string.IsNullOrEmpty(Apartment))
                {
                    sb.Append(", кв");
                    sb.Append(Apartment);
                }

                return sb.ToString();
            }
            else
            {
                return "Не указан в базе данных";
            } 
                
        }

        public override bool Equals(object? obj)
        {
            return obj is XlsxExportedContactInformation information &&
                   City == information.City &&
                   information.Phones.Count == Phones.Count &&
                   !Phones.Except(information.Phones).Any() &&
                   Email == information.Email &&
                   CityType == information.CityType &&
                   City == information.City &&
                   StreetType == information.StreetType &&
                   Street == information.Street &&
                   House == information.House &&
                   Building == information.Building &&
                   Apartment == information.Apartment &&
                   _city == information._city;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(City);
            hash.Add(Phones);
            hash.Add(Email);
            hash.Add(CityType);
            hash.Add(City);
            hash.Add(StreetType);
            hash.Add(Street);
            hash.Add(House);
            hash.Add(Building);
            hash.Add(Apartment);
            hash.Add(_city);
            return hash.ToHashCode();
        }
    }
}
