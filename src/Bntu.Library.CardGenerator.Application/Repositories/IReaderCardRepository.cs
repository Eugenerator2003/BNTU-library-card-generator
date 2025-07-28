using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Repositories
{
    public interface IReaderCardRepository
    {
        IList<ReaderCard> GetAllCards();
    }
}
