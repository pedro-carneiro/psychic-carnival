namespace ToggleApi.Services
{
    public interface IUpdateService<T>
    {
        void Update(T request);
    }
}
