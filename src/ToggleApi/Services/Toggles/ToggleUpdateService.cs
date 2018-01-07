namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Requests;
    using System.Linq;

    public class ToggleUpdateService : IUpdateService<ToggleRequest>
    {
        private readonly ApiDbContext _context;

        public ToggleUpdateService(ApiDbContext context)
        {
            _context = context;
        }

        void IUpdateService<ToggleRequest>.Update(ToggleRequest request)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == request.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            toggle.Name = request.Name;
            toggle.DefaultValue = request.DefaultValue;

            _context.Toggles.Update(toggle);
            _context.SaveChanges();
        }
    }
}
