namespace ToggleApi.Services.AppOverrides
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Requests;
    using System.Linq;

    public class AppOverrideDeleteService : IDeleteService<AppOverrideRequest>
    {
        private readonly ApiDbContext _context;

        public AppOverrideDeleteService(ApiDbContext context)
        {
            _context = context;
        }

        void IDeleteService<AppOverrideRequest>.Delete(AppOverrideRequest request)
        {
            var appOverride = _context.AppOverrides.FirstOrDefault(t => t.ToggleId == request.ToggleId && t.Application == request.Application);
            if (appOverride == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            _context.AppOverrides.Remove(appOverride);
            _context.SaveChanges();
        }
    }
}
