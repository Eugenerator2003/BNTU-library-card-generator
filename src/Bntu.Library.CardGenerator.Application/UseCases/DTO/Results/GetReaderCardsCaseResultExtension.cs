using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Results
{
    public static class GetReaderCardsCaseResultExtension
    {
        public static GenerateReaderCardsCaseParameter FromGetReaderCardsCaseResult(this GetReaderCardsCaseResult result)
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
