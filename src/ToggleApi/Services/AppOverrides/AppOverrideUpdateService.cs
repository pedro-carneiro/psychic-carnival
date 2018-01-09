namespace ToggleApi.Services.AppOverrides
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Requests;
    using System.Linq;

    public class AppOverrideUpdateService : IUpdateService<AppOverrideRequest>
    {
        private readonly ApiDbContext _context;

        public AppOverrideUpdateService(ApiDbContext context)
        {
            _context = context;
        }

        void IUpdateService<AppOverrideRequest>.Update(AppOverrideRequest request)
        {
            var appOverride = _context.AppOverrides.FirstOrDefault(t => t.ToggleId == request.ToggleId && t.Application == request.Application);
            if (appOverride == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            appOverride.Value = request.Value;

            _context.AppOverrides.Update(appOverride);
            _context.SaveChanges();
        }
    }
}
