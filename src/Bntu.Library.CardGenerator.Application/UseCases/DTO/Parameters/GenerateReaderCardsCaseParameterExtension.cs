using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public static class GenerateReaderCardsCaseParameterExtension
    {
        public static GenerateReaderCardsCaseParameter FromGetReaderCardsCaseResult(GetReaderCardsCaseResult result)
        {
            var dict = new Dictionary<string, IList<ReaderCard>?>();
            foreach(var key in result.Groups)
            {
                dict.Add(key, result[key]?.ToList());
            }
            return new GenerateReaderCardsCaseParameter(dict);
        } 
    }
}
