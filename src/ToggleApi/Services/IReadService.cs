namespace ToggleApi.Services
{
    using System.Collections.Generic;

    public interface IReadService<TRequest, TResponse>
    {
        TResponse Get(TRequest request);

        IEnumerable<TResponse> GetAll(TRequest request);
    }
}
