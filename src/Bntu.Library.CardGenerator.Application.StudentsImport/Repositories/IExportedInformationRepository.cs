using Bntu.Library.CardGenerator.Application.StudentsExport.Models.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Repositories
{
    public interface IExportedInformationRepository
    {
        public IList<ExportedInformation> GetAllExportedInformation();
    }
}
