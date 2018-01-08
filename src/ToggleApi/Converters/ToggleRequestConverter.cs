namespace ToggleApi.Converters
{
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;

    public class ToggleRequestConverter : IConverter<ToggleRequest, Toggle>
    {
        public ToggleRequestConverter()
        {
        }

        Toggle IConverter<ToggleRequest, Toggle>.convert(ToggleRequest a)
        {
            return new Toggle()
            {
                Id = a.Id,
                Name = a.Name,
                DefaultValue = a.DefaultValue
            };
        }
    }
}
