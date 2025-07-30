using Bntu.Library.CardGenerator.Application.Util;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.Util
{
    [TestClass]
    public class ApplicationResultTests
    {
        [DataTestMethod]
        [DataRow("ValidationError", "значение", "Ошибка валидации")]
        [DataRow("Error", "-------", "Ошибка выполнения")]
        [DataRow("Success", "value....   ", "Успешное выполнение")]
        public void InitializeTest(string resultTypeS, string value, string message)
        {
            //string obj = new string() { Value = value };
            ApplicationResult<string>.ResultType resultType = Enum.Parse<ApplicationResult<string>.ResultType>(resultTypeS, true);
            ApplicationResult<string> applicationResult = new ApplicationResult<string>(resultType, value, message);
            Assert.AreEqual(message, applicationResult.Message);
            Assert.AreEqual(resultType, applicationResult.Type);
            Assert.AreEqual(value, applicationResult.Value);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsSuccessTest(bool isSuccess)
        {
            ApplicationResult<string>.ResultType resultType = isSuccess ? ApplicationResult<string>.ResultType.Success :
                                                                              ApplicationResult<string>.ResultType.Error;
            ApplicationResult<string> applicationResult = new ApplicationResult<string>(resultType, null, null);
            Assert.AreEqual(isSuccess, applicationResult.IsSuccess);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsOperationErrorTest(bool isError)
        {
            ApplicationResult<string>.ResultType resultType = isError ? ApplicationResult<string>.ResultType.Error :
                                                                              ApplicationResult<string>.ResultType.Success;
            ApplicationResult<string> applicationResult = new ApplicationResult<string>(resultType, null, null);
            Assert.AreEqual(isError, applicationResult.IsOperationError);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void IsValidationErrorTest(bool isValidationError)
        {
            ApplicationResult<string>.ResultType resultType = isValidationError ? ApplicationResult<string>.ResultType.ValidationError :
                                                                              ApplicationResult<string>.ResultType.Success;
            ApplicationResult<string> applicationResult = new ApplicationResult<string>(resultType, null, null);
            Assert.AreEqual(isValidationError, applicationResult.IsValidationError);
        }

        [TestMethod]
        public void ValidationErrorTest()
        {
            ApplicationResult<string> applicationResult = ApplicationResult<string>.ValidationError("");
            Assert.AreEqual(ApplicationResult<string>.ResultType.ValidationError, applicationResult.Type);
        }

        [TestMethod]
        public void ErrorTest()
        {
            ApplicationResult<string> applicationResult = ApplicationResult<string>.Error("");
            Assert.AreEqual(ApplicationResult<string>.ResultType.Error, applicationResult.Type);
        }

        [TestMethod]
        public void SuccessTest()
        {
            ApplicationResult<string> applicationResult = ApplicationResult<string>.Success("", "");
            Assert.AreEqual(ApplicationResult<string>.ResultType.Success, applicationResult.Type);
        }
    }
}
