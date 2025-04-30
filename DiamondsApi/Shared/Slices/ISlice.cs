namespace DiamondsApi.Shared.Slices;

public interface ISlice
{
    void AddEndpoint(IEndpointRouteBuilder app);
}