using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class CsvProfessionService : BaseCsvReadRepository<Profession>, IProfessionService
    {
        public CsvProfessionService(string filename) : base(filename)
        {
        }

        public ApplicationResult<ProfessionCollection> GetProfessions()
        {
            try
            {
                var items = GetAllItems(a => true, false);
                var profession = new ProfessionCollection();
                foreach (var item in items)
                {
                    profession.Add(item);
                }
                return ApplicationResult<ProfessionCollection>.Success(profession, "");
            }
            catch (Exception ex)
            {
                return ApplicationResult<ProfessionCollection>.Error(ex.Message);
            }
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
