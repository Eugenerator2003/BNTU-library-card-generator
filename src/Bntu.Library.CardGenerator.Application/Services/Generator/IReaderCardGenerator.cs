using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public interface IReaderCardGenerator
    {
        ApplicationResult<IList<GeneratedReaderCard>> Generate(IList<ReaderCard> readerCards);
    }
}
