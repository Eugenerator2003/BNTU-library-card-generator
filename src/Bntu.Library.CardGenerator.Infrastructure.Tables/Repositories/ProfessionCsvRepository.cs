using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories
{
    public class ProfessionCsvRepository : BaseCsvReadRepository<Profession>
    {
        public ProfessionCsvRepository(string filename) : base(filename)
        {
        }

        public List<Profession> GetProfessions()
        {
            return GetAllItems(a => true, false);
        }

        protected override CsvFields GetCsvFields()
        {
            return new CsvFields(["IdProfession", "Libelle"]); 
        }

        protected override Profession ReadFromCurrentLine()
        {
            var id = int.Parse(CsvReader.GetField(NextField()) ?? "");
            var name = CsvReader.GetField(NextField()) ?? "";
            return new Profession(id, name);
        }
    }
}
