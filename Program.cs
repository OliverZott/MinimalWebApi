
using System.IO;
using System.Net.Mime;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

///
/// Example Porject for minimal Web-API
/// 
/// https://korzh.com/blog/single-file-web-service-aspnetcore
/// https://trello.com/c/fk13jez3/47-single-file-web-api-services
/// 
/// to run and check api: 
///     "dotnet run"
///     "http://localhost:5000/api/echo"
/// 
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder
        .Configure(app => app.Run(async context =>
        {
            if (context.Request.Path == "/api/echo" && context.Request.Method == "POST")
            {
                // getting contentn of POST request
                using var reader = new StreamReader(context.Request.Body);      // 'using' to create alias for type "using alias directive"
                var content = await reader.ReadToEndAsync();

                // sending it back in the response
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync(content);
            }
            else
            {
                // Return 404 or any endpoint
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"WRONG ENDPOINT: {context.Request.Path.ToString()}. Use POST request to /api/echo instead");
            }
        }))
    )
    .Build().Run();
