using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.Util;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv
{
    public class ProfessionService : IProfessionService
    {
        private ProfessionCsvRepository csvRepository;

        public ProfessionService(ProfessionCsvRepository csvRepository)
        {
            this.csvRepository = csvRepository;
        }

        public ApplicationResult<ProfessionCollection> GetProfessions()
        {
            try
            {
                var items = csvRepository.GetProfessions();
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
    }
}
