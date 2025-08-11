using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services
{
    public class ReaderCardService
    {
        private IReaderCardRepository readerCardRepository;

        public ReaderCardService(IReaderCardRepository readerCardRepository)
        {
            this.readerCardRepository = readerCardRepository;
        }

        public ApplicationResult<IList<ReaderCard>> GetReaderCardsByGroup(string number)
        {
            try
            {
                var requestResult = readerCardRepository.GetCardsByGroup(number);
                if (requestResult.Count == 0)
                {
                    return ApplicationResult<IList<ReaderCard>>.Error($"Отсутствует информация по студентам указаннной группы {number}");
                }

                return ApplicationResult<IList<ReaderCard>>.Success(requestResult, "");
            }
            catch(Exception ex)
            {
                return ApplicationResult<IList<ReaderCard>>.Error($"Ошибка получения данных читательских билетов: {ex.GetType()}:{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
