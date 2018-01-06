namespace ToggleApi.Services
{
    public interface ICreateService<T>
    {
        T Create(T resource);
    }
}
