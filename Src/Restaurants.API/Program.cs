using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Model;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Inject Services
    // add DB connection from Infrastructure layer to API layer
    builder.AddPresentation();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    //Serilog
    app.UseSerilogRequestLogging();

    //Middleware
    app.UseMiddleware<ErrorHandlingMiddle>();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.Seed();


    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        // Enable Swagger UI in Development
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });
    }

    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .MapIdentityApi<User>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed!!");
}
finally
{
    Log.CloseAndFlush();
}



public partial class Program { }