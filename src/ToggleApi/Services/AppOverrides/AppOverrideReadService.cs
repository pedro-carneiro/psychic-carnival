namespace ToggleApi.Services.AppOverrides
{
    using ToggleApi.Converters;
    using ToggleApi.Exceptions;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using System.Collections.Generic;
    using System.Linq;

    public class AppOverrideReadService : IReadService<AppOverrideRequest, AppOverrideResponse>
    {
        private readonly ApiDbContext _context;
        private readonly IConverter<AppOverride, AppOverrideResponse> _appOverrideConverter;

        public AppOverrideReadService(ApiDbContext context,
                                      IConverter<AppOverride, AppOverrideResponse> appOverrideConverter)
        {
            _context = context;
            _appOverrideConverter = appOverrideConverter;
        }

        AppOverrideResponse IReadService<AppOverrideRequest, AppOverrideResponse>.Get(AppOverrideRequest request)
        {
            var appOverride = _context.AppOverrides.FirstOrDefault(t => t.ToggleId == request.ToggleId && t.Application == request.Application);
            if (appOverride == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            return _appOverrideConverter.convert(appOverride);
        }

        IEnumerable<AppOverrideResponse> IReadService<AppOverrideRequest, AppOverrideResponse>.GetAll(AppOverrideRequest request)
        {
            List<AppOverrideResponse> responses = new List<AppOverrideResponse>();

            var toggles = _context.AppOverrides.ToList();

            foreach(var toggle in toggles)
            {
                responses.Add(_appOverrideConverter.convert(toggle));
            }
            return responses;
        }
    }
}
