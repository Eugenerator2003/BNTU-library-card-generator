using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.UseCases.DTO.Parameters
{
    public class GetReaderCardsUseParameter
    {
        public IReadOnlyCollection<string> Groups
        {
            get
            {
                return new ReadOnlyCollection<string>(groups);
            }
        }

        private List<string> groups;

        public GetReaderCardsUseParameter(List<string> groups)
        {
            this.groups = groups;
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
