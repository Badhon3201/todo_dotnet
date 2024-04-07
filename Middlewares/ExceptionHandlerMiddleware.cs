
using System.Net;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
            await next(context);
        }catch(Exception e){
            context.Response.ContentType = "application/json";
            var problemDetails = new ProblemDetails{
                Instance = context.Request.Path,
                Detail = e.Message,
            };
            switch(e){
                case EntityNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    problemDetails.Title = "Not Found";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    break;
                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Bad Request";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;
                default :
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    break;   
            }
            await context.Response.WriteAsync(problemDetails.ToJson());
        }
    }
}