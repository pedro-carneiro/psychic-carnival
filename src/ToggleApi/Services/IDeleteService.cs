namespace ToggleApi.Services
{
    public interface IDeleteService<T>
    {
        void Delete(T resource);
    }
}
