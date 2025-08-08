using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public class ReaderCardXlsRepository : IReaderCardRepository
    {
        private readonly string filename;

        public ReaderCardXlsRepository(string filename)
        {
            this.filename = filename;
        }

        public IList<ReaderCard> GetAllCards()
        {
            throw new NotImplementedException();
        }

        public ReaderCard GetCardByCodbar(int codbar)
        {
            throw new NotImplementedException();
        }

        public ReaderCard GetCardByPid(int pid)
        {
            throw new NotImplementedException();
        }

        public IList<ReaderCard> GetCardsByGroup(string groupNumber)
        {
            throw new NotImplementedException();
        }
    }
}
