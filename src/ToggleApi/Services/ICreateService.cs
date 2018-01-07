namespace ToggleApi.Services
{
    public interface ICreateService<TRequest, TResponse>
    {
        TResponse Create(TRequest request);
    }
}
