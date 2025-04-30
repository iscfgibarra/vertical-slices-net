using System.Reflection;

namespace DiamondsApi.Shared.Slices;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterSlices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        
        var slices = currentAssembly.GetTypes()
            .Where(t => typeof(ISlice).IsAssignableFrom(t) 
                        && t!= typeof(ISlice) 
                        && t is { IsPublic: true, IsAbstract: false })
            .ToList();

        foreach (var slice in slices)
        {
            services.AddSingleton(typeof(ISlice), slice);
        }
        
        
        
        return services;
    }
}