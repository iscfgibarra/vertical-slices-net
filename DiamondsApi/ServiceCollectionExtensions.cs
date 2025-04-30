using DiamondsApi.Features.Diamonds.Commands;
using DiamondsApi.Features.Diamonds.Queries;
using DiamondsApi.Infrastructure;
using DiamondsApi.Shared.Exceptions;
using DiamondsApi.Shared.Slices;
using Microsoft.EntityFrameworkCore;

namespace DiamondsApi;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CreateDiamondCommandHandler>();
        services.AddScoped<GetDiamondByIdCommandHandler>();
        services.AddScoped<GetDiamondsCommandHandler>();
        services.AddScoped<UpdateDiamondCommandHandler>();
        services.AddScoped<DeleteDiamondCommandHandler>();
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.RegisterSlices();
        
        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DiamondDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<DataSeeder>();
        
        return services;
    }
}