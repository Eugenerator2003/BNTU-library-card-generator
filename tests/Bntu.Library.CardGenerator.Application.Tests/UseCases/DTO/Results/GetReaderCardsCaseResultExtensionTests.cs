using Bntu.Library.CardGenerator.Application.UseCases.DTO.Results;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Results
{
    [TestClass]
    public class GetReaderCardsCaseResultExtensionTests
    {
        [DataTestMethod]
        [DataRow(3)]
        [DataRow(13)]
        public void ToGenerateReaderCardsCaseParameterTest(int groupCount)
        {
            string[] groups = new string[groupCount];
            for(int i = 0; i < groups.Length; i++)
            {
                groups[i] = RandomValuesGenerator.GetStringLine();
            }
            var inputResult = new GetReaderCardsCaseResult(groups);
            for(int i = 0; i < inputResult.Groups.Length; i++)
            {
                int cardsCount = Random.Shared.Next(1, 41);
                List<ReaderCard> readerCards = new List<ReaderCard>();
                for(int j = 0; j <  cardsCount; j++)
                {
                    readerCards.Add(RandomValuesGenerator.GenerateRandomReaderCard());
                }
                inputResult.SetGroupCards(groups[i], readerCards);
            }

            var actualParameter = inputResult.ToGenerateReaderCardsCaseParameter();
            CollectionAssert.AreEquivalent(inputResult.Groups, actualParameter.Groups);
            var actualDict = actualParameter.ReaderCards;
            foreach(var group in groups)
            {
                CollectionAssert.AreEquivalent((ICollection)inputResult[group]!, (ICollection)actualDict[group]);
            }
        }
    }
}
