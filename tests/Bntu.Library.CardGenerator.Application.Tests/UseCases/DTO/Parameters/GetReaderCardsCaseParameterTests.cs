using Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.Tests.UseCases.DTO.Parameters
{
    [TestClass]
    public class GetReaderCardsCaseParameterTests
    {
        [TestMethod]
        public void EmptyInitializeTest()
        {
            var param = new GetReaderCardsCaseParameter();
            Assert.IsTrue(param.Groups.Count == 0);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(30)]
        [DataRow(4)]
        public void GroupsInitializeTest(int groupsCount)
        {
            var list = GetList(groupsCount);
            var param = new GetReaderCardsCaseParameter(list);
            GroupCollectionEqual(groupsCount, list, param.Groups);
        }

        [DataTestMethod]
        [DataRow(0, 2)]
        [DataRow(6, 7)]
        [DataRow(10, 77)]
        public void AddGroupTest(int startCount, int addCount)
        {
            var list = GetList(startCount);
            var param = new GetReaderCardsCaseParameter(list);
            for(int i = 0; i < addCount; i++)
            {
                string name = RandomValuesGenerator.GetStringLine();
                list.Add(name);
                param.AddGroup(name);
            }

            GroupCollectionEqual(startCount + addCount, list, param.Groups);
        }

        [DataTestMethod]
        [DataRow(0, 13)]
        [DataRow(19, 20)]
        [DataRow(30, 1)]
        public void ClearTest(int startCount, int addCount)
        {
            var list = GetList(addCount);
            var param = new GetReaderCardsCaseParameter(list);
            for (int i = 0; i < addCount; i++)
            {
                param.AddGroup(RandomValuesGenerator.GetStringLine());
            }

            param.Clear();
            Assert.IsTrue(param.Groups.Count == 0);
        }

        private void GroupCollectionEqual(int count, ICollection<string> expected, IReadOnlyCollection<string> actual)
        {
            Assert.AreEqual(count, actual.Count);
            for(int i = 0; i < count; i++)
            {
                Assert.AreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private List<string> GetList(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(RandomValuesGenerator.GetStringLine());
            }
            return list;
        }
    }
}
