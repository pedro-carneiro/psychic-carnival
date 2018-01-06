namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Resources;
    using System.Linq;

    public class ToggleUpdateService : IUpdateService<ToggleResource>
    {
        private readonly ApiDbContext _context;

        public ToggleUpdateService(ApiDbContext context)
        {
            _context = context;
        }

        void IUpdateService<ToggleResource>.Update(ToggleResource resource)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == resource.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            toggle.Name = resource.Name;
            toggle.DefaultValue = resource.DefaultValue;

            _context.Toggles.Update(toggle);
            _context.SaveChanges();
        }
    }
}
