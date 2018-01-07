namespace ToggleApi.Services.Toggles
{
    using AutoMapper;
    using ToggleApi.Exceptions;
    using ToggleApi.Models;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using System.Collections.Generic;
    using System.Linq;

    public class ToggleReadService : IReadService<ToggleRequest, ToggleResponse>
    {
        private readonly ApiDbContext _context;

        public ToggleReadService(ApiDbContext context)
        {
            _context = context;
        }

        ToggleResponse IReadService<ToggleRequest, ToggleResponse>.Get(ToggleRequest request)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == request.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            return Mapper.Map<ToggleResponse>(toggle);
        }

        IEnumerable<ToggleResponse> IReadService<ToggleRequest, ToggleResponse>.GetAll(ToggleRequest request)
        {
            var toggles = _context.Toggles.ToList();
            return Mapper.Map<IEnumerable<ToggleResponse>>(toggles);
        }
    }
}
