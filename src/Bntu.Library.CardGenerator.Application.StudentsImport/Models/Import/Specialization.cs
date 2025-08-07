using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import
{
    public class Specialization
    {
        public int Id { get; }

        public string Name { get; }

        public int ProfessionId { get; }

        public Specialization(int id, string name, int professionId)
        {
            Name = name;
            Id = id;
            ProfessionId = professionId;
        }

        public override bool Equals(object? obj)
        {
            return obj is Specialization specialization &&
                   specialization.Id == Id && specialization.
                   Name == specialization.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
