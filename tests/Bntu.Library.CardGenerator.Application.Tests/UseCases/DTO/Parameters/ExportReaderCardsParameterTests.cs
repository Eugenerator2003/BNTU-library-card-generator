using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Parameters
{
    [TestClass]
    public class ExportReaderCardsParameterTests
    {
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(27)]
        [DataRow(100)]
        public void InitializeTests(int count)
        {
            var group = RandomValuesGenerator.GetStringLine();
            var generatedList = new List<GeneratedReaderCard>();
            for(int i = 0; i < count; i++)
            {
                generatedList.Add(RandomValuesGenerator.GenerateGeneratedReaderCard(group));
            }
            var exptectedCollection = new ReadOnlyCollection<GeneratedReaderCard>(generatedList);
            var param = new ExportReaderCardsParameter(exptectedCollection);
            CollectionAssert.AreEqual(exptectedCollection, (ICollection)param.GeneratedCards);
        }
    }
}
