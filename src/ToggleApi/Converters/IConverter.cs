namespace ToggleApi.Converters
{
    public interface IConverter<A, B>
    {
        B convert(A a);
    }
}
