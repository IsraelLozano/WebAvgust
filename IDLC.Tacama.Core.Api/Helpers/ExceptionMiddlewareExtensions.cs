﻿using MINEDU.IEST.Estudiante.Inf_Utils.CustomMiddleware;

namespace IDLC.Tacama.Core.Api.Helpers
{
    public static class ExceptionMiddlewareExtensions
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        //{
        //    app.UseExceptionHandler(appError =>
        //    {
        //        appError.Run(async context =>
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";

        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                logger.LogError($"Algo salio mal: {contextFeature.Error}");

        //                await context.Response.WriteAsync(new ErrorDetails()
        //                {
        //                    StatusCode = context.Response.StatusCode,
        //                    Message = "Internal Server Error."
        //                }.ToString());
        //            }
        //        });
        //    });
        //}

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

    }
}
