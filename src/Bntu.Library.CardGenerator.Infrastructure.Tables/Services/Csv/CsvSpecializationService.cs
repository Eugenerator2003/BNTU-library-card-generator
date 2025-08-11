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
    public class CsvSpecializationService : BaseCsvReadRepository<Specialization>, ISpecializationService
    {
        public CsvSpecializationService(string filename) : base(filename)
        {
        }

        public ApplicationResult<SpecializationCollection> GetSpecializations()
        {
            try
            {
                var items = GetAllItems(a => true, false);
                var specs = new SpecializationCollection();
                foreach(var item in items)
                {
                    specs.Add(item);
                }
                return ApplicationResult<SpecializationCollection>.Success(specs, "");
            }
            catch (Exception ex)
            {
                return ApplicationResult<SpecializationCollection>.Error(ex.Message);
            }
        }

        protected override CsvFields GetCsvFields()
        {
            return new CsvFields(["IdCategSocioProf", "Libelle", "parent"]);
        }

        protected override Specialization ReadFromCurrentLine()
        {
            var id = int.Parse(CsvReader.GetField(NextField()) ?? "");
            var name = CsvReader.GetField(NextField()) ?? "";
            var parentId = int.Parse(CsvReader.GetField(NextField()) ?? "");
            return new Specialization(id, name, parentId);
        }
    }
}
