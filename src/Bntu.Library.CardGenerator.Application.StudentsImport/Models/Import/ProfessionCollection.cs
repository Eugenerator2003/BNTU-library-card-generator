using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class ProfessionCollection
    {
        public Profession? this[int id]
        {
            get
            {
                return professions.FirstOrDefault(p => p.Id == id);
            }
        }

        private List<Profession> professions;

        public ProfessionCollection()
        {
            professions = new List<Profession>();
        }

        public void Add(Profession profession)
        {
            if (professions.Any(p => p.Id == profession.Id || p.Name == profession.Name))
            {
                throw new ArgumentException("Коллекция профессий уже содержит данную профессию.", nameof(profession));
            }

            professions.Add(profession);
        }
    }
}
