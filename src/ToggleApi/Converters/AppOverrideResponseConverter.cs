namespace ToggleApi.Converters
{
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Responses;

    public class AppOverrideResponseConverter : IConverter<AppOverride, AppOverrideResponse>
    {
        public AppOverrideResponseConverter()
        {
        }

        AppOverrideResponse IConverter<AppOverride, AppOverrideResponse>.convert(AppOverride a)
        {
            return new AppOverrideResponse()
            {
                ToggleId = a.ToggleId,
                ToggleName = a.ToggleName,
                Value = a.Value
            };
        }
    }
}
