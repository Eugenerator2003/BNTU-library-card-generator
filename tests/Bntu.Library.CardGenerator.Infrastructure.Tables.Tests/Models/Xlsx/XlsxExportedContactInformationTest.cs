using Bntu.Library.CardGenerator.Infrastructure.Tables.Models.Xlsx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Tests.Models.Xlsx
{
    [TestClass]
    public class XlsxExportedContactInformationTest
    {
        [TestMethod]
        public void InitializeTest()
        {
            string? cityType = "г.";
            string? city = "Минск";
            string? streetType = "пр-т";
            string? street = "Независимости";
            string? house = "д. 99";
            string? building = "б";
            string? appartment = "313";
            List<string> phones = ["+3753818931", "+312393130"];
            string? email = "email@gmail.com";
            var actual = new XlsxExportedContactInformation(cityType, city, streetType, street, house, building, appartment, phones, email);
            Assert.AreEqual(cityType, actual.CityType);
            Assert.AreEqual(city, actual.City);
            Assert.AreEqual(streetType, actual.StreetType);
            Assert.AreEqual(street, actual.Street);
            Assert.AreEqual(house, actual.House);
            Assert.AreEqual(building, actual.Building);
            Assert.AreEqual(appartment, actual.Apartment);
            CollectionAssert.AreEquivalent(phones, actual.Phones);
            Assert.AreEqual(email, actual.Email);
        }

        [TestMethod]
        public void InitializeNoCityTypeGetAddressTest()
        {
            string? cityType = null;
            string? city = "Минск";
            string? streetType = "пр-т";
            string? street = "Независимости";
            string? house = "д. 99";
            string? building = "б";
            string? appartment = "313";
            List<string> phones = ["+3753818931", "+312393130"];
            string? email = "email@gmail.com";
            var actual = new XlsxExportedContactInformation(cityType, city, streetType, street, house, building, appartment, phones, email);
            Assert.AreEqual(cityType, actual.CityType);
            Assert.AreEqual("Не указан в базе данных", actual.City);
            Assert.AreEqual(streetType, actual.StreetType);
            Assert.AreEqual(street, actual.Street);
            Assert.AreEqual(house, actual.House);
            Assert.AreEqual(building, actual.Building);
            Assert.AreEqual(appartment, actual.Apartment);
            CollectionAssert.AreEquivalent(phones, actual.Phones);
            Assert.AreEqual(email, actual.Email);
            Assert.AreEqual("Не указан в базе данных", actual.GetAddress());
        }

        [TestMethod]
        public void InitializeNoCityGetAddressTest()
        {
            string? cityType = null;
            string? city = "Минск";
            string? streetType = "пр-т";
            string? street = "Независимости";
            string? house = "99";
            string? building = "б";
            string? appartment = "313";
            List<string> phones = ["+3753818931", "+312393130"];
            string? email = "email@gmail.com";
            var actual = new XlsxExportedContactInformation(cityType, city, streetType, street, house, building, appartment, phones, email);
            Assert.AreEqual(cityType, actual.CityType);
            Assert.AreEqual("Не указан в базе данных", actual.City);
            Assert.AreEqual(streetType, actual.StreetType);
            Assert.AreEqual(street, actual.Street);
            Assert.AreEqual(house, actual.House);
            Assert.AreEqual(building, actual.Building);
            Assert.AreEqual(appartment, actual.Apartment);
            CollectionAssert.AreEquivalent(phones, actual.Phones);
            Assert.AreEqual(email, actual.Email);
        }

        [DataTestMethod]
        [DataRow("пр-т", "Проспектовский", "А", "12")]
        [DataRow("ул.", "Городская", null, "12")]
        [DataRow("переул.", "Граничный", "Б", null)]
        [DataRow(null, null, null, null)]
        public void GetAddressTest(string? streetType, string? street, string? building, string? apartment)
        {
            string? cityType = "г.";
            string? city = "Минск";
            string? house = "99";
            var actual = new XlsxExportedContactInformation(cityType, city, streetType, street, house, building, apartment, [], null);
            string exptected = $"{(streetType is not null ? streetType + " " + street + "," : "")}" +
                               $"д. {house}" +
                               $"{(building is not null ? ", корпус " + building : "")}" +
                               $"{(apartment is not null ? ", кв. "  + apartment : "")}";
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            var phones = new List<string> { "375334842000", "375291234567" };

            var info1 = new XlsxExportedContactInformation(
                cityType: "г",
                city: "Минск",
                streetType: "ул",
                street: "Пр. Независимости",
                house: "102",
                building: "2",
                appartment: "6",
                phones: phones,
                email: "example@mail.com"
            );

            var info2 = new XlsxExportedContactInformation(
                cityType: "г",
                city: "Минск",
                streetType: "ул",
                street: "Пр. Независимости",
                house: "102",
                building: "2",
                appartment: "6",
                phones: new List<string>(phones),
                email: "example@mail.com"
            );

            Assert.IsTrue(info1.Equals(info2));
        }

        [TestMethod]
        public void EqualsFalseTest()
        {
            var phones1 = new List<string> { "375334842000" };
            var phones2 = new List<string> { "375291234567" };

            var info1 = new XlsxExportedContactInformation(
                cityType: "г",
                city: "Минск",
                streetType: "ул",
                street: "Пр. Независимости",
                house: "102",
                building: "2",
                appartment: "6",
                phones: phones1,
                email: "example@mail.com"
            );

            var info2 = new XlsxExportedContactInformation(
                cityType: "г",
                city: "Минск",
                streetType: "ул",
                street: "Пр. Независимости",
                house: "102",
                building: "2",
                appartment: "6",
                phones: phones2,
                email: "example@mail.com"
            );

            Assert.IsFalse(info1.Equals(info2));
        }
    }
}
