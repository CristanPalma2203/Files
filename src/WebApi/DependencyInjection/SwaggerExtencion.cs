using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace WebApi.DependencyInjection
{
    public static class SwaggerExtencion
    {
        public static void AddSwaggerConf(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Corelux Files API",
                    Description = "Stored file upload, download and metadata.",
                    Contact = new OpenApiContact
                    {
                        Name = "Corelux",
                        Email = string.Empty,
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
