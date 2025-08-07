using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public abstract class DictionaryCollection<T>
    {
        protected List<T> values;

        public DictionaryCollection()
        {
            values = new List<T>();
        }

        public void Add(T item)
        {
            if (Contains(item))
            {
                throw new ArgumentException("Коллекция уже содержит данный элемент.", nameof(item));
            }
        }

        protected abstract bool Contains(T item);
    }
}
