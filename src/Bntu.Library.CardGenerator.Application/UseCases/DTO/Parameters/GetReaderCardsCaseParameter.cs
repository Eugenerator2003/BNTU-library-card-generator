using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public class GetReaderCardsCaseParameter
    {
        public IReadOnlyCollection<string> Groups
        {
            get
            {
                return new ReadOnlyCollection<string>(groups);
            }
        }

        private List<string> groups;

        public GetReaderCardsCaseParameter()
        {
            groups = new List<string>();
        }

        public GetReaderCardsCaseParameter(List<string> groups)
            : this()
        {
            groups.AddRange(groups);
        }

        public void AddGroup(string group)
        {
            groups.Add(group);
        }

        public void Clear()
        {
            groups.Clear();
        }
    }
}
