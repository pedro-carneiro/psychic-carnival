namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Requests;
    using System.Linq;

    public class ToggleDeleteService : IDeleteService<ToggleRequest>
    {
        private readonly ApiDbContext _context;

        public ToggleDeleteService(ApiDbContext context)
        {
            _context = context;
        }

        void IDeleteService<ToggleRequest>.Delete(ToggleRequest request)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == request.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            _context.Toggles.Remove(toggle);
            _context.SaveChanges();
        }
    }
}
