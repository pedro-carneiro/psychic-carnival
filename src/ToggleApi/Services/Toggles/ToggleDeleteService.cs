namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Resources;
    using System.Linq;

    public class ToggleDeleteService : IDeleteService<ToggleResource>
    {
        private readonly ApiDbContext _context;

        public ToggleDeleteService(ApiDbContext context)
        {
            _context = context;
        }

        void IDeleteService<ToggleResource>.Delete(ToggleResource resource)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == resource.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            _context.Toggles.Remove(toggle);
            _context.SaveChanges();
        }
    }
}
