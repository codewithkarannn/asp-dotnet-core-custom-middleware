using System.Diagnostics;

namespace MiddlewareTimingDemo.Middleware;

public class RequestTimingMiddleware(RequestDelegate _next) 
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();

            context.Response.Headers["X-Processing-Time-ms"] = stopwatch.ElapsedMilliseconds.ToString();
               
            
            Console.WriteLine($"Request: {context.Request.Path} | Time: {  stopwatch.ElapsedMilliseconds.ToString()}ms");
            return Task.CompletedTask;
        });

        await _next(context);
    }
}