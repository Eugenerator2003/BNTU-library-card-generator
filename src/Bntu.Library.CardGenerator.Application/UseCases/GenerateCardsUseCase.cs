using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases
{
    public class GenerateCardsUseCase
    {
        public GenerateReaderCardCaseResult Generate(GenerateReaderCardCaseParameter parameter)
        {
            var result = new GenerateReaderCardCaseResult();
            var parameterDict = parameter.ReaderCards;
            foreach(var group in parameterDict.Keys)
            {
                var generationResult = ReaderCardGenerator.Generate(parameterDict[group].ToList());
                if (generationResult.IsSuccess)
                {
                    result.AddGeneratedCards(group, generationResult.Value!);
                }
            }
            return result;
        }
    }
}
