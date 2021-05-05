using System;
using Loris.Application;
using Loris.CrossCutting;
using Loris.Common.Webapi.Domain.Entities;
using Loris.Common.Webapi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Loris.Webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            //services.AddExceptionHandlerMiddleware();
            
            #region API Version

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            #endregion

            #region AppSettings

            var settings = Configuration.Get<AppSettings>();
            services.Configure((Action<AppSettings>)(setting =>
            {
                Configuration.Bind(setting);
            }));

            #endregion

            // IoC registrations
            services.AddServiceDependecies();

            // Auto Mapper (DTO <=> Entity DB)
            services.AddAutoMapper();

            // JWT Authentication
            services.AddJwtBearer(settings);

            // Add Swagger (NSwag.AspNetCore)
            services.AddOpenApiDocument(settings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            //app.UseExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUi3();
            app.UseOpenApi();
        }
    }
}
