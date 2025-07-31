using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Parameters
{
    [TestClass]
    public class GenerateReaderCardsCaseParameterTests
    {
        [DataTestMethod]
        [DataRow(3)]
        [DataRow(23)]
        public void InitializeTest(int groupCount)
        {
            var dict = new Dictionary<string, IList<ReaderCard>?>();
            for (int i = 0; i < groupCount; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                int studentsCount = Random.Shared.Next(1, 41);
                AddToDict(dict, group);
            }

            GenerateReaderCardsCaseParameter param = new GenerateReaderCardsCaseParameter(dict);
            AssertParam(dict, param);
        }

        [DataTestMethod]
        [DataRow(5, 1)]
        [DataRow(12, 11)]
        [DataRow(25, 25)]
        public void InitializeNullTest(int groupCount, int nullCount)
        {
            var dict = new Dictionary<string, IList<ReaderCard>?>();
            for (int i = 0; i < groupCount; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                if (nullCount > 0)
                {
                    dict.Add(group, null);
                    nullCount--;
                }
                else
                {
                    AddToDict(dict, group);
                }
            }

            GenerateReaderCardsCaseParameter param = new GenerateReaderCardsCaseParameter(dict);
            AssertParam(dict, param);
        }


        [DataTestMethod]
        [DataRow(3, 1)]
        [DataRow(8, 3)]
        [DataRow(13, 13)]
        public void InitializeZeroTest(int groupCount, int zeroCount)
        {
            var dict = new Dictionary<string, IList<ReaderCard>?>();
            for (int i = 0; i < groupCount; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                if (zeroCount > 0)
                {
                    dict.Add(group, new List<ReaderCard>());
                    zeroCount--;
                }
                else
                {
                    AddToDict(dict, group);
                }
            }

            GenerateReaderCardsCaseParameter param = new GenerateReaderCardsCaseParameter(dict);
            AssertParam(dict, param);
        }

        [DataTestMethod]
        [DataRow(4, 1, 1)]
        [DataRow(7, 2, 4)]
        [DataRow(20, 11, 9)]
        public void InitializeZeroNullTest(int groupCount, int zeroCount, int nullCount)
        {
            var dict = new Dictionary<string, IList<ReaderCard>?>();
            for (int i = 0; i < groupCount; i++)
            {
                string group = RandomValuesGenerator.GetStringLine();
                if (zeroCount > 0)
                {
                    dict.Add(group, new List<ReaderCard>());
                    zeroCount--;
                }
                else if (nullCount > 0)
                {
                    dict.Add(group, null);
                    nullCount--;
                }
                else
                {
                    AddToDict(dict, group);
                }
            }

            GenerateReaderCardsCaseParameter param = new GenerateReaderCardsCaseParameter(dict);
            AssertParam(dict, param);
        }

        private void AddToDict(Dictionary<string, IList<ReaderCard>?> dict, string groupName)
        {
            int studentsCount = Random.Shared.Next(1, 41);
            List<ReaderCard> studentsList = new List<ReaderCard>();
            for (int j = 0; j < studentsCount; j++)
            {
                studentsList.Add(RandomValuesGenerator.GenerateRandomReaderCard(groupName));
            }
            dict.Add(groupName, studentsList);
        }

        private void AssertParam(Dictionary<string, IList<ReaderCard>?> initialDict, 
                                 GenerateReaderCardsCaseParameter parameter)
        {
            var expectedDict = initialDict.Where(p => p.Value != null && p.Value.Count > 0).ToDictionary();
            CollectionAssert.AreEquivalent(expectedDict.Keys, parameter.Groups);
            foreach (var group in parameter.Groups)
            {
                CollectionAssert.AreEquivalent((ICollection)expectedDict[group]!, (ICollection)parameter.ReaderCards[group]);
            }
        }
    }
}
