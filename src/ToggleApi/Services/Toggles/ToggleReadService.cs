namespace ToggleApi.Services.Toggles
{
    using AutoMapper;
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Resources;
    using System.Collections.Generic;
    using System.Linq;

    public class ToggleReadService : IReadService<ToggleResource>
    {
        private readonly ApiDbContext _context;

        public ToggleReadService(ApiDbContext context)
        {
            _context = context;
        }

        ToggleResource IReadService<ToggleResource>.Get(long id)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            return Mapper.Map<ToggleResource>(toggle);
        }

        IEnumerable<ToggleResource> IReadService<ToggleResource>.GetAll()
        {
            var toggles = _context.Toggles.ToList();
            return Mapper.Map<IEnumerable<ToggleResource>>(toggles);
        }
    }
}
