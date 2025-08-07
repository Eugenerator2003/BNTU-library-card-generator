using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Util
{
    public class SpecializationNumberComparer
    {
        private static readonly string pattern = @"^([а-яА-Я\s,()/-]+)\s*([\d\s-]+)$";

        public static string GetFormattedNumber(string semiformattedNumber)
        {
            return semiformattedNumber.Replace(" ", "")
                                      .Replace("-", "");
        }

        public static string? GetSpecializationNumber(string notFormattedName)
        {
            Match match = Regex.Match(notFormattedName, pattern);
            if (match.Success)
            {
                string number = match.Groups[2].Value;
                return number;
            }
            else
            {
                return null;
            }
        }

        public static bool Compare(string semiformattedNumber, string notFormattedName)
        {
            string formattedNumber = GetFormattedNumber(semiformattedNumber);
            string? number = GetSpecializationNumber(notFormattedName);
            if (number == null)
                return false;
            else
                return GetFormattedNumber(number) == formattedNumber;
        }
    }
}
