using Bntu.Library.CardGenerator.Application.Repositories;
using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Csv;
using Bntu.Library.CardGenerator.Infrastructure.Tables.Services.Xlsx;
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
            services.AddTransient<IReaderCardRepository, ReaderCardCvsRepository>();
            services.AddTransient<ISpecializationService, CsvSpecializationService>();
            services.AddTransient<IProfessionService, CsvProfessionService>();
            services.AddTransient<IImportInformationSaver, ImportInformationCsvSaver>();
            return services;
        }

        private static IServiceCollection UseXlsx(this IServiceCollection services)
        {
            services.AddTransient<IExportedInformationService, XlsxExportedInformationService>();
            return services;
        }
    }
}
