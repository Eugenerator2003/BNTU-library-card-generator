using Bntu.Library.CardGenerator.Application.StudentsImport.Services;
using Bntu.Library.CardGenerator.Application.StudentsImport.UseCases;
using Bntu.Library.CardGenerator.WPFApp.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Startup
{
    public static class StudentsImportStartupExtension
    {
        public static IServiceCollection UseStudentsImport(this IServiceCollection services)
        {
            services.AddTransient(sp => new InformationTransformationServiceFactory().Create());
            return services.UseImportUseCases();
        }

        private static IServiceCollection UseImportUseCases(this IServiceCollection services)
        {
            services.AddTransient<GetExportedInformationUseCase>();
            services.AddTransient<GetProfessionsUseCase>();
            services.AddTransient<GetSpecializationsUseCase>();
            services.AddTransient<SaveImportInformationUseCase>();
            services.AddTransient<TransformExportedInformationUseCase>();
            return services;
        }
    }
}
