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

        public int? ProfessionId { get; }

        public Specialization(int id, string name, int? professionId)
        {
            Name = name;
            Id = id;
            ProfessionId = professionId;
        }

        public Specialization(int id, string name) :
            this(id, name, null)
        {

        }

        public override bool Equals(object? obj)
        {
            return obj is Specialization specialization &&
                   Id == specialization.Id && 
                   Name == specialization.Name &&
                   ProfessionId == specialization.ProfessionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, ProfessionId);
        }
    }
}
