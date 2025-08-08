using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bntu.Library.CardGenerator.Application.Tests.StudentsImport.UseCases.DTO.Parameters
{
    [TestClass]
    public class GetSpecializationsCaseParameterTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var professions = new ProfessionCollection();

            var parameter = new GetSpecializationsCaseParameter(professions);

            Assert.AreSame(professions, parameter.ProfessionCollection);
        }
    }
}
