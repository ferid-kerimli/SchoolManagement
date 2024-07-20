using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace SchoolManagement.Extentions;

public static class ExceptionHandlingException
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                    Log.Error($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = $"Internal Server Error: {contextFeature.Error}"
                    }));
                }
            });
        });
    }
}