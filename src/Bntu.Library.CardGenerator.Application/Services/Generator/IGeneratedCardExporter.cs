using Bntu.Library.CardGenerator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Services.Generator
{
    public interface IGeneratedCardExporter<T> where T : IGeneratedReaderCard
    {
        ApplicationResult<EmptyResult> Export(IList<T> values);
    }
}
