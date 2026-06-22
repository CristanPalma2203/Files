using Aplicacion.Exceptions;
using Aplicacion.Mappers;
using Dominio.Models.Regla;
using Dominio.Service;
using Infraestructura.Filters;
using Infraestructura.Service;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DependencyInjection;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapsterConfig = TypeAdapterConfig.GlobalSettings;
            mapsterConfig.Scan(typeof(MappingConfig).Assembly);
            services.AddSingleton(mapsterConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddHandlers();
            services.AddContextConfiguration(Configuration);
            services.AddScoped<UnitOfWordFilter>();
            services.AddAplicacionServices();
            services.AddTokenConfiguration(Configuration);
            services.AddHttpContextAccessor();
            services.AddRedis(Configuration);
            services.AddCorsConfig();
            services.AddSwaggerConf();
            services.AddTransient<IGuardarArchivoAlmacenamiento, GuardarArchivoAlmacenamiento>();

            services.Scan(scan =>
                scan.FromAssemblyOf<IExtensionesPermitidas>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRegla)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(UnitOfWordFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpException();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentacion SAPI V1");
            });
            app.UseCors("ApiCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
