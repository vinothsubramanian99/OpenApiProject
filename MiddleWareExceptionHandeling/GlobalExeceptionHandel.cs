
using System.Text;
using Microsoft.AspNetCore.Mvc;
namespace OpenApiProject1.MiddleWareExceptionHandeling
{

    public class GlobalExeceptionHandel : IMiddleware
    {

        private readonly ILogger<GlobalExeceptionHandel> _logger;

        public GlobalExeceptionHandel(ILogger<GlobalExeceptionHandel> logger)
        { _logger = logger; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                _logger.LogInformation($"API called from source: {userAgent}");
                // Log the request 
                //var request = await FormatRequest(context.Request);
                //_logger.LogInformation($"Request: {request}");

            }
            catch (Exception ex)
            {
                var pb = new ProblemDetails
                {
                    Detail = ex.Message,
                    Title = ex is ValidationException ? "Validation error" : "Internal server error",
                    Instance = context.Request.Path,
                    Status = ex is ValidationException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = pb.Status.Value;
                await context.Response.WriteAsJsonAsync(pb);

            }
        }

      private async Task<string> FormatRequest(HttpRequest request) 
      { var body = request.Body; 
      var buffer = new byte[Convert.ToInt32(request.ContentLength)];
       await body.ReadAsync(buffer.AsMemory(0, buffer.Length)); 
       var bodyAsText = Encoding.UTF8.GetString(buffer); 
       request.Body.Position = 0; 
       return $"{request.Method} {request.Path} {request.QueryString} {bodyAsText}"; }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin); // Ensure the stream is at the beginning

            using (var reader = new StreamReader(response.Body, leaveOpen: true))
            {
                var text = await reader.ReadToEndAsync(); // Read the body as a string
                response.Body.Seek(0, SeekOrigin.Begin); // Reset the stream position
                return $"{response.StatusCode}: {text}";
            }
        }

    }
}

/*public class GlobalExeceptionHandel : IMiddleware
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
}*/
