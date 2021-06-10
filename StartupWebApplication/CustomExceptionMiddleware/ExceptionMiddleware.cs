using log4net;
using Microsoft.AspNetCore.Http;
using PlantTrackerAPI.Models;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace PlantTrackerAPI.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpListenerException responseException)
            {
                _logger.ErrorFormat($"Something went wrong: {responseException}");
                await HandleExceptionAsync(httpContext, responseException);
            }
            catch (Exception exception)
            {
                _logger.ErrorFormat($"Something went wrong: {exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string content = exception.Message;

            return context.Response.WriteAsync(new ErrorDetailModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Something went wrong! Contact the administrator for more details."
            }.ToString());
        }

        private Task HandleExceptionAsync(HttpContext context, HttpListenerException responseException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseException.ErrorCode;
            string content = responseException.Message;

            return context.Response.WriteAsync(new ErrorDetailModel()
            {
                StatusCode = context.Response.StatusCode,
                Message = content
            }.ToString());
        }
    }
}
