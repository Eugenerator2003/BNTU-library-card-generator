using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters
{
    public class TransformExportedInformationCaseParameter
    {
        public IList<ExportedInformation> ExportedInformation { get; }

        public ImportingParameter ImportingParameter { get; }

        public TransformExportedInformationCaseParameter(IList<ExportedInformation> exportedInformation, ImportingParameter importingParameter)
        {
            ExportedInformation = exportedInformation;
            ImportingParameter = importingParameter;
        }
    }
}
