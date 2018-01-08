namespace ToggleApi.Converters
{
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Responses;

    public class ToggleResponseConverter : IConverter<Toggle, ToggleResponse>
    {
        public ToggleResponseConverter()
        {
        }

        ToggleResponse IConverter<Toggle, ToggleResponse>.convert(Toggle a)
        {
            return new ToggleResponse()
            {
                Id = a.Id,
                Name = a.Name,
                DefaultValue = a.DefaultValue
            };
        }
    }
}
