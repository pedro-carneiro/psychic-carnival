namespace ToggleApi.Services.AppOverrides
{
    using ToggleApi.Converters;
    using ToggleApi.Exceptions;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;
    using System.Linq;

    public class AppOverrideCreateService : ICreateService<AppOverrideRequest, AppOverrideResponse>
    {
        private readonly ApiDbContext _context;
        private readonly IConverter<AppOverrideRequest, AppOverride> _appOverrideConverter;
        private readonly IConverter<AppOverride, AppOverrideResponse> _responseConverter;

        public AppOverrideCreateService(ApiDbContext context,
                                        IConverter<AppOverrideRequest, AppOverride> appOverrideConverter,
                                        IConverter<AppOverride, AppOverrideResponse> responseConverter)
        {
            _context = context;
            _appOverrideConverter = appOverrideConverter;
            _responseConverter = responseConverter;
        }

        AppOverrideResponse ICreateService<AppOverrideRequest, AppOverrideResponse>.Create(AppOverrideRequest request)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == request.ToggleId);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            var appOverride = _appOverrideConverter.convert(request);
            appOverride.ToggleName = toggle.Name;

            _context.AppOverrides.Add(appOverride);
            _context.SaveChanges();

            return _responseConverter.convert(appOverride);
        }
    }
}
