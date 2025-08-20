using Bntu.Library.CardGenerator.Application.Services;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases
{
    public class GetReaderCardsUseCase
    {
        private ReaderCardService cardService;

        public GetReaderCardsUseCase(ReaderCardService cardService)
        {
            this.cardService = cardService;
        }

        public GetReaderCardsCaseResult GetReaderCards(GetReaderCardsCaseParameter parameter)
        {
            var groups = parameter.Groups.ToArray();
            var result = new GetReaderCardsCaseResult(groups);
            foreach(var group in parameter.Groups)
            {
                var groupCardsResult = cardService.GetReaderCardsByGroup(group);
                if (groupCardsResult.IsSuccess)
                {
                    result.SetGroupCards(group, groupCardsResult.Value!);
                }
                else
                {
                    result.SetSuccess(false);
                    result.SetMessage(groupCardsResult.Message);
                }
            }
            return result;
        }
    }
}
