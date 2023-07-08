/*
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

class Program
{
    static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .Configure(app =>
            {
                app.Run(async context =>
                {
                    Console.WriteLine("Request received!");
                    var request = context.Request;
                    Console.WriteLine($"Method: {request.Method}");
                    Console.WriteLine($"Path: {request.Path}");

                    await context.Response.WriteAsync("Hello, World!");
                });
            })
            .Build();

        host.Run();
    }
}
*/