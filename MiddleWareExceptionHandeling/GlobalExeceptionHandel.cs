
using Microsoft.AspNetCore.Mvc;
namespace OpenApiProject1.MiddleWareExceptionHandeling{
public class GlobalExeceptionHandel : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
            await next (context);
        }
        catch (Exception ex){
                var pb=new ProblemDetails();
                pb.Detail=ex.Message;
                pb.Title="Internal server error";
                pb.Instance=context.Request.Path;
                await context.Response.WriteAsJsonAsync(pb);
        }
         
    }
}
}