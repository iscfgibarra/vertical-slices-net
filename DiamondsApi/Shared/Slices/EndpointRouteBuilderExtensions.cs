namespace DiamondsApi.Shared.Slices;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapSlicesEndpoints(this IEndpointRouteBuilder app)
    {

        foreach(ISlice slice in app.ServiceProvider.GetServices<ISlice>())
        {
            slice.AddEndpoint(app);
        }
        
        return app;
    }
}