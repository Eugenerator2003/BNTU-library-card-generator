using Bntu.Library.CardGenerator.Application.Services;
using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Startup
{
    public static class ApplicationStartupExtension
    {
        public static IServiceCollection UseApplication(this IServiceCollection service)
        {
            return service.UseApplicationServices()
                          .UseUseCases();
        }
        
        private static IServiceCollection UseApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ReaderCardService>();
            services.AddTransient<ReaderCardGenerator>();
            return services;
        }

        private static IServiceCollection UseUseCases(this IServiceCollection services)
        {
            services.AddTransient<ExportReaderCardsUseCase>();
            services.AddTransient<GenerateCardsUseCase>();
            services.AddTransient<GetReaderCardsUseCase>();
            return services;
        }
    }
}
