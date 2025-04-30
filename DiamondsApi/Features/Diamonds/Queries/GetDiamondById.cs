using DiamondsApi.Infrastructure;
using DiamondsApi.Models;
using DiamondsApi.Shared.Slices;
using Microsoft.AspNetCore.Mvc;

namespace DiamondsApi.Features.Diamonds.Queries;

public class GetDiamondById : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/diamonds/{id}", async (
                [FromRoute] int id, GetDiamondByIdCommandHandler command) =>
            {
                var diamond = await command.Handle(id);
                return diamond is null ? Results.NotFound() : Results.Ok(diamond);
            })
            .WithName("GetDiamondById")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get diamond by ID",
                Description = "Returns a specific diamond by its ID"
            });
    }
}

public class GetDiamondByIdCommandHandler
{
    private readonly DiamondDbContext _context;
        
    public GetDiamondByIdCommandHandler(DiamondDbContext context)
    {
        _context = context;
    }
        
    public async Task<Diamond?> Handle(int id)
    {
        return await _context.Diamonds.FindAsync(id);
    }
}