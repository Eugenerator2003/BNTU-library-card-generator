using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Parameters;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases.DTO.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.UseCases
{
    public class SaveImportInformationUseCase
    {
        private IImportInformationSaver saver;

        public SaveImportInformationUseCase(IImportInformationSaver saver)
        {
            this.saver = saver;
        }

        public SaveImportInformationCaseResult Save(SaveImportInformationCaseParameter parameter)
        {
            var result = saver.Save(parameter.ImportInformation);
            return new SaveImportInformationCaseResult(result.IsSuccess, result.Message);
        }
    }
}
