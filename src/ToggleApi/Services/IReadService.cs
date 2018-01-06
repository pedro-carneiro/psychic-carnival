namespace ToggleApi.Services
{
    using System.Collections.Generic;

    public interface IReadService<T>
    {
        T Get(long id);

        IEnumerable<T> GetAll();
    }
}
