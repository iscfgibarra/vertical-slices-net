using DiamondsApi.Infrastructure;
using DiamondsApi.Models;
using DiamondsApi.Shared.Slices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiamondsApi.Features.Diamonds.Commands;

public class DeleteDiamond : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/diamonds/{id}", async (
                [FromRoute] int id,
                DeleteDiamondCommandHandler command) =>
            {
                var result = await command.Handle(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteDiamond")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a diamond",
                Description = "Deletes an existing diamond by ID"
            });
    }
}

public class DeleteDiamondCommandHandler
{
    private readonly DiamondDbContext _context;
        
    public DeleteDiamondCommandHandler(DiamondDbContext context)
    {
        _context = context;
    }
        
    public async Task<bool> Handle(int id)
    {
        var diamond = await _context.Diamonds.FindAsync(id);
        if (diamond is null)
        {
            return false;
        }

        _context.Diamonds.Remove(diamond);
        await _context.SaveChangesAsync();
        return true;
    }
} 