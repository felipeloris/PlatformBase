using Loris.Common.Domain.Entities;
using Loris.Common.Exceptions;
using Loris.Common.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
namespace Loris.Common.Webapi.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var guidError = Guid.NewGuid();
                _logger.LogError($"Unexpected error! ID={guidError}", ex);

                await HandleExceptionAsync(context, ex, guidError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, Guid guidError)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var responseModel = new TreatedResult<List<string>>(TreatedResultStatus.Error, ex?.Message);

            switch (ex)
            {
                /*
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Exchange = e.Errors;
                    break;
                */
                case KeyNotFoundException e:
                    responseModel.Status = TreatedResultStatus.NoDataFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case TreatedErrorException e:
                    responseModel.Status = TreatedResultStatus.InternalServerError;
                    responseModel.Message = e.Message;
                    break;
                default:
                    responseModel.Status = TreatedResultStatus.InternalServerError;
                    responseModel.Message = string.Format(Messages.SystemError(guidError), guidError);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return context.Response.WriteAsync(SerializeObject.ToJson(responseModel));
        }
    }
}
