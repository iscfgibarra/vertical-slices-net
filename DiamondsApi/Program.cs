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

/*
// API Endpoints with Swagger documentation
app.MapGet("/api/diamonds", async (DiamondService service) =>
{
    var diamonds = await service.GetAllDiamondsAsync();
    return Results.Ok(diamonds);
})
.WithName("GetAllDiamonds")
.WithOpenApi(operation => new(operation)
{
    Summary = "Get all diamonds",
    Description = "Returns a list of all diamonds in the inventory"
});





app.MapPut("/api/diamonds/{id}", async (int id, Diamond diamond, DiamondService service) =>
{
    var success = await service.UpdateDiamondAsync(id, diamond);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("UpdateDiamond")
.WithOpenApi(operation => new(operation)
{
    Summary = "Update a diamond",
    Description = "Updates an existing diamond in the inventory"
});

app.MapDelete("/api/diamonds/{id}", async (int id, DiamondService service) =>
{
    var success = await service.DeleteDiamondAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteDiamond")
.WithOpenApi(operation => new(operation)
{
    Summary = "Delete a diamond",
    Description = "Removes a diamond from the inventory"
});
*/

app.Run();
