using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx;
using Bntu.Library.CardGenerator.WPFApp.PathProviders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Startup
{
    public static class TableInfrastructureStartupExtension
    {
        public static IServiceCollection UseTables(this IServiceCollection services)
        {
            return services.UseCsv()
                           .UseXlsx();
        }

        private static IServiceCollection UseCsv(this IServiceCollection services)
        {
            var pathProvider = CsvFilesPathProvider.Instance;
            services.AddTransient<IReaderCardRepository, ReaderCardCvsRepository>(sp => new ReaderCardCvsRepository(pathProvider.ReaderCardsSource!));
            services.AddTransient<ISpecializationService, CsvSpecializationService>(sp => new CsvSpecializationService(pathProvider.SpecializationSource!));
            services.AddTransient<IProfessionService, CsvProfessionService>(sp => new CsvProfessionService(pathProvider.ProfessionSource!));
            services.AddTransient<IImportInformationSaver, ImportInformationCsvSaver>(sp => new ImportInformationCsvSaver(pathProvider.ImportInformationDestination!));
            return services;
        }

        private static IServiceCollection UseXlsx(this IServiceCollection services)
        {
            var pathProvider = XlsxFilesPathProvider.Instance;
            services.AddTransient<IExportedInformationService, XlsxExportedInformationService>(sp => new XlsxExportedInformationService(pathProvider.ExportedInformationSource!));
            return services;
        }
    }
}
