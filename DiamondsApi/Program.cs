using DiamondsApi;
using DiamondsApi.Infrastructure;
using DiamondsApi.Models;
using DiamondsApi.Shared.Slices;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterPersistenceServices(builder.Configuration);
builder.Services.RegisterApplicationServices();


var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DiamondDbContext>();
    context.Database.EnsureCreated(); // This will create the database if it doesn't exist
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();

app.MapSlicesEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    
    //Agregado Swagger por medio de NSwag.AspNetCore
    app.UseSwaggerUi((options) =>
    {
        options.Path = "/openapi";
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.Run();
