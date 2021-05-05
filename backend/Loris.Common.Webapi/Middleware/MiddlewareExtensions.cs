using Loris.Common.Domain.Entities;
using Loris.Common.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Loris.Common.Webapi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        var guidError = Guid.NewGuid();

                        logger.LogError($"Unexpected error! ID={guidError}", contextFeature.Error);

                        await context.Response.WriteAsync(
                            SerializeObject.ToJson(new TreatedResult(TreatedResultStatus.InternalServerError, $"Internal Server Error. ({guidError})")));
                    }
                });
            });
        }

        #region ExceptionHandlerMiddleware

        public static IServiceCollection AddExceptionHandlerMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<ExceptionHandlerMiddleware>();
        }

        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        #endregion
    }
}
