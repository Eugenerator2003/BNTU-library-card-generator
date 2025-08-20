using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases;
using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases
{
    [TestClass]
    public class ExportReaderCardsUseCaseTests
    {
        [TestMethod]
        public void GenerateSuccessTest()
        {
            List<GeneratedReaderCard> cards = new List<GeneratedReaderCard>();
            var useCase = GetUseCase(cards, true);
            var param = new ExportReaderCardsCaseParameter(new ReadOnlyCollection<GeneratedReaderCard>(cards));
            var actualResult = useCase.Export(param);

            Assert.IsTrue(actualResult.Success);
        }

        [TestMethod]
        public void GenerateFailTest()
        {
            List<GeneratedReaderCard> cards = new List<GeneratedReaderCard>();
            var useCase = GetUseCase(cards, false);
            var param = new ExportReaderCardsCaseParameter(new ReadOnlyCollection<GeneratedReaderCard>(cards));
            var actualResult = useCase.Export(param);

            Assert.IsFalse(actualResult.Success);
        }

        private ExportReaderCardsUseCase GetUseCase(IList<GeneratedReaderCard> cards, bool result)
        {
            var exporterMock = new Mock<IGeneratedCardExporter>();

            exporterMock.Setup(m => m.Export(cards))
                        .Returns(result ? 
                                 ApplicationResult<EmptyResult>.Success(null, "") :
                                 ApplicationResult<EmptyResult>.Error(""));
            return new ExportReaderCardsUseCase(exporterMock.Object);
        }
    }
}
