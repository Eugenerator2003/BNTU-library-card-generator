using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public interface IGenerator<T> where T : IGeneratedReaderCard
    {
        ApplicationResult<IList<T>> Generate(IList<ReaderCard> readerCards);
    }
}
