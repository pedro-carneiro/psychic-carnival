namespace ToggleApi.Converters
{
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;

    public class AppOverrideRequestConverter : IConverter<AppOverrideRequest, AppOverride>
    {
        public AppOverrideRequestConverter()
        {
        }

        AppOverride IConverter<AppOverrideRequest, AppOverride>.convert(AppOverrideRequest a)
        {
            return new AppOverride()
            {
                ToggleId = a.ToggleId,
                Application = a.Application,
                Value = a.Value
            };
        }
    }
}
