using DiamondsApi.Infrastructure;
using DiamondsApi.Models;
using DiamondsApi.Shared.Slices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiamondsApi.Features.Diamonds.Commands;

public class UpdateDiamond : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/diamonds/{id}", async (
                [FromRoute] int id,
                [FromBody] UpdateDiamondRequest request,
                UpdateDiamondCommandHandler command) =>
            {
                var result = await command.Handle(id, request);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateDiamond")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update a diamond",
                Description = "Updates an existing diamond by ID"
            });
    }
}

public class UpdateDiamondRequest
{
    public string? Color { get; set; }
    public decimal? Price { get; set; }
    public decimal? Carat { get; set; }
}

public class UpdateDiamondCommandHandler
{
    private readonly DiamondDbContext _context;
        
    public UpdateDiamondCommandHandler(DiamondDbContext context)
    {
        _context = context;
    }
        
    public async Task<bool> Handle(int id, UpdateDiamondRequest request)
    {
        var diamond = await _context.Diamonds.FindAsync(id);
        if (diamond is null)
        {
            return false;
        }
        
        if (request.Color is not null)
            diamond.Color = request.Color;
        if (request.Price.HasValue)
            diamond.Price = request.Price.Value;
        if (request.Carat.HasValue)
            diamond.Carat = request.Carat.Value;

        await _context.SaveChangesAsync();
        return true;
    }
} 