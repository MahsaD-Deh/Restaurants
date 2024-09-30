using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant API",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
                        },
                        []
                    }

                });
            });


            builder.Services.AddEndpointsApiExplorer(); // add user register Endpoints to swagger

            //Middleware
            builder.Services.AddScoped<ErrorHandlingMiddle>();


            //Use Serilog
            /*
            builder.Host.UseSerilog((context, configuration) =>
            configuration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
            .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
                );
            */

            builder.Host.UseSerilog((context, configuration) =>
            configuration
            .ReadFrom.Configuration(context.Configuration));

        }
    }
}
