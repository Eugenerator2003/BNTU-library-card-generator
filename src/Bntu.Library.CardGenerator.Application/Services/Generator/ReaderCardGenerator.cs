using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public class ReaderCardGenerator
    {
        public static ApplicationResult<IList<GeneratedReaderCard>> Generate(IList<ReaderCard> readerCards)
        {
            var generated = readerCards.Select(a => new GeneratedReaderCard(a)).ToList();
            return ApplicationResult<IList<GeneratedReaderCard>>.Success(generated, "");
        }
    }
}
