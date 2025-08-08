using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using System;
using System.Collections.Generic;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models;
using Bntu.Library.CardGenerator.Domain.ValueObjects;
using Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Util;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Tests.Services
{
    [TestClass]
    public class InformationTransformationTests
    {
        [TestMethod]
        public void TransformSuccessTest()
        {
            var professions = InformationOperations.GetProfessions();
            var specializations = InformationOperations.GetSpecializations(professions);

            var service = new InformationTransformationService(InformationOperations.GetProfessionCollection(professions),
                                                               InformationOperations.GetSpecializationCollection(specializations));
            var expectedList = new List<ExportedInformation>()
            {
                InformationOperations.GetRandomExportedInformation(specializations)
            };
            var importingParameter = new ImportingParameter(DateTime.Now, 1);
            var actualResult = service.Transform(expectedList, importingParameter);
            Assert.IsTrue(actualResult.IsSuccess);
            Assert.AreEqual(actualResult.Message, "");
            Assert.IsNotNull(actualResult.Value);
            var actual = actualResult.Value[0];
            InformationOperations.AssertingEquals(1, expectedList[0], actual, professions);
        }

        [TestMethod]
        public void TransformManySuccessTest()
        {
            var professions = InformationOperations.GetProfessions();
            var specializations = InformationOperations.GetSpecializations(professions);

            var service = new InformationTransformationService(InformationOperations.GetProfessionCollection(professions),
                                                               InformationOperations.GetSpecializationCollection(specializations));

            var expectedList = new List<ExportedInformation>();
            for(int i = 0; i < 4000; i++)
            {
                expectedList.Add(InformationOperations.GetRandomExportedInformation(specializations));
            }
            var importingParameter = new ImportingParameter(DateTime.Now, 1);
            var actualResult = service.Transform(expectedList, importingParameter);
            Assert.IsTrue(actualResult.IsSuccess);
            Assert.AreEqual(actualResult.Message, "");
            Assert.IsNotNull(actualResult.Value);
            for(int i = 0; i < 4000; i++)
            {
                var actual = actualResult.Value[i];
                InformationOperations.AssertingEquals(importingParameter.StartCodbar + i, expectedList[i], actual, professions);
            }
        }

        [TestMethod]
        public void TransformErrorTest()
        {
            var professions = new ProfessionCollection();
            var specializations = new SpecializationCollection();

            var service = new InformationTransformationService(professions, specializations);

            var importParam = new ImportingParameter(DateTime.Now, 3000);

            IList<ExportedInformation> exportedInformation = null!;

            var result = service.Transform(exportedInformation!, importParam);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsOperationError);
            Assert.IsTrue(result.Message.Contains("System.NullReferenceException"));
        }
    }
}
