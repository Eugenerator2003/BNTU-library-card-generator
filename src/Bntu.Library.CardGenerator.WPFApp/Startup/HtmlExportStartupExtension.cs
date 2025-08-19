using Bntu.Library.CardGenerator.Application.Services.Generator;
using Bntu.Library.CardGenerator.Infrastructure.HtmlExport;
using Bntu.Library.CardGenerator.WPFApp.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Startup
{
    public static class HtmlExportStartupExtension
    {
        public static IServiceCollection UseHtmlExport(this IServiceCollection services)
        {
            services.AddTransient<IGeneratedCardExporter, HtmlGeneratedCardExporter>(sp => new HtmlGeneratedCardExporterFactory().Create());
            return services;
        }
    }
}
