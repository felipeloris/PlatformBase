using Loris.Application.ApplicationService;
using Loris.Application.Interfaces;
using Loris.Services;
using Loris.Interfaces.Services;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Services;
using Loris.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;

namespace Loris.CrossCutting
{
    public static class IocHelper
    {
        public static void AddServiceDependecies(this IServiceCollection services)
        {
            /**************************************************************************
            => Singleton objects are the same for every object and every request
            services.Add(new ServiceDescriptor(typeof(ILog), new MyConsoleLogger())); 

            => Transient objects are always different; a new instance is provided to every controller and every service.
            services.Add(new ServiceDescriptor(typeof(ILog), typeof(MyConsoleLogger), ServiceLifetime.Transient));

            => Scoped objects are the same within a request, but different across different requests
            services.Add(new ServiceDescriptor(typeof(ILog), typeof(MyConsoleLogger), ServiceLifetime.Scoped));
            **************************************************************************/

            #region Database configuration

            /*
            var cnn = DatabaseHelper.RecoverDatabase().ConnString;
            services.AddDbContextPool<AuthDbContext>(
                options => options.UseNpgsql(cnn),
                poolSize: settings.DbContextPoolSize);
            */

            // Database injection (factory and configuration) 
            services.AddSingleton((Func<IServiceProvider, IDatabase>)(serviceProvider =>
            {
                //var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                //var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                return DatabaseHelper.RecoverDatabase();
            }));

            #endregion

            services.AddHttpContextAccessor();
            
            services.AddScoped<ILoginManager, LoginManagerJwt>();
            services.AddScoped(serviceProvider =>
            {
                var env = serviceProvider.GetRequiredService<ILoginManager>();
                return env.Login;
            });

            services.AddScoped<IUserAuthentication, BasicUserAuthentication>();

            //services.AddScoped<ILogger, xxx>();

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IRoleAppService, RoleAppService>();
            services.AddScoped<IResourceAppService, ResourceAppService>();

            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IAuthRoleService, AuthRoleService>();
            services.AddScoped<IAuthResourceService, AuthResourceService>();
        }
    }
}
