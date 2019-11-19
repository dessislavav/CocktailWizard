using CocktailWizard.Services.ConstantMessages;
using CocktailWizard.Services.CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Middlewares
{
    public class GeneralExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GeneralExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (BusinessLogicException exception)
            {

                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(exception.Message);
                }
            }

            catch (Exception)
            {
                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(ExceptionMessages.GeneralOopsMessage);
                }
                else
                {
                    context.Response.Redirect("error/generalerror");
                }
            }
        }



    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GeneralExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGeneralExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GeneralExceptionHandlerMiddleware>();
        }
    }
}
