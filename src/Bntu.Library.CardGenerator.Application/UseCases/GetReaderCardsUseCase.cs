using Bntu.Library.CardGenerator.Application.Services;
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

        public ReaderCardsCaseResult GetReaderCards(params string[] groupNumbers)
        {
            var result = new ReaderCardsCaseResult(groupNumbers);
            foreach(var group in groupNumbers)
            {
                var groupCardsResult = cardService.GetReaderCardsByGroup(group);
                if (groupCardsResult.IsSuccess)
                {
                    result.SetGroupCards(group, groupCardsResult.Value!);
                }
            }
            return result;
        }
    }
}
