using DiamondsApi.Infrastructure;
using DiamondsApi.Models;
using DiamondsApi.Shared.Slices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiamondsApi.Features.Diamonds.Queries;

public class GetDiamonds : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/diamonds", async (
                GetDiamondsCommandHandler command,
                [FromQuery] int page = 1,
                [FromQuery] int size = 10) =>
            {
                var result = await command.Handle(page, size);
                return Results.Ok(result);
            })
            .WithName("GetDiamonds")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get diamonds with pagination",
                Description = "Returns a paginated list of diamonds"
            });
    }
}

public class GetDiamondsCommandHandler
{
    private readonly DiamondDbContext _context;
        
    public GetDiamondsCommandHandler(DiamondDbContext context)
    {
        _context = context;
    }
        
    public async Task<PaginatedResult<Diamond>> Handle(int page, int size)
    {
        var totalCount = await _context.Diamonds.CountAsync();
        var items = await _context.Diamonds
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return new PaginatedResult<Diamond>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            Size = size,
            TotalPages = (int)Math.Ceiling(totalCount / (double)size)
        };
    }
}

public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalPages { get; set; }
}
