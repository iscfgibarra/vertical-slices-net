using DiamondsApi.Models;
using DiamondsApi.Infrastructure;
using DiamondsApi.Shared.Slices;
using Microsoft.AspNetCore.Mvc;

namespace DiamondsApi.Features.Diamonds.Commands;


public class CreateDiamond: ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/diamonds", async (
                [FromBody] CreateDiamondCommand diamond
                , [FromServices] CreateDiamondCommandHandler command
                , CancellationToken cancellationToken = default) =>
            {
                var id = await command.Handle(diamond, cancellationToken);
                return Results.Created($"/api/diamonds/{id}", id);
            })
            .WithDisplayName("CreateDiamond")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a diamond",
                Description = "Creates a new diamond"
            }).Produces<int>();
}
}


public sealed class CreateDiamondCommand(
    decimal Carat,
    string Cut,
    string Color,
    string Clarity,
    decimal Depth,
    decimal Table,
    decimal Price,
    decimal X,
    decimal Y,
    decimal Z
)
{
    public decimal Carat { get; } = Carat;
    public string Cut { get; } = Cut;
    public string Color { get; } = Color;
    public string Clarity { get; } = Clarity;
    public decimal Depth { get; } = Depth;
    public decimal Table { get; } = Table;
    public decimal Price { get; } = Price;
    public decimal X { get; } = X;
    public decimal Y { get; } = Y;
    public decimal Z { get; } = Z;


}

public class CreateDiamondCommandHandler
{
    private readonly DiamondDbContext _context;

    public CreateDiamondCommandHandler(DiamondDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDiamondCommand request, CancellationToken cancellationToken)
    {
        var diamond = new Diamond
        {
            Carat = request.Carat,
            Cut = request.Cut,
            Color = request.Color,
            Clarity = request.Clarity,
            Depth = request.Depth,
            Table = request.Table,
            Price = request.Price,
            X = request.X,
            Y = request.Y,
            Z = request.Z
        };

        _context.Diamonds.Add(diamond);
        await _context.SaveChangesAsync(cancellationToken);

        return diamond.Id;
    }
}