namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Converters;
    using ToggleApi.Exceptions;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using System.Collections.Generic;
    using System.Linq;

    public class ToggleReadService : IReadService<ToggleRequest, ToggleResponse>
    {
        private readonly ApiDbContext _context;
        private readonly IConverter<Toggle, ToggleResponse> _toggleConverter;

        public ToggleReadService(ApiDbContext context,
                                 IConverter<Toggle, ToggleResponse> toggleConverter)
        {
            _context = context;
            _toggleConverter = toggleConverter;
        }

        ToggleResponse IReadService<ToggleRequest, ToggleResponse>.Get(ToggleRequest request)
        {
            var toggle = _context.Toggles.FirstOrDefault(t => t.Id == request.Id);
            if (toggle == null)
            {
                throw new ResourceNotFoundException("Resource not found!");
            }

            return _toggleConverter.convert(toggle);
        }

        IEnumerable<ToggleResponse> IReadService<ToggleRequest, ToggleResponse>.GetAll(ToggleRequest request)
        {
            List<ToggleResponse> responses = new List<ToggleResponse>();

            var toggles = _context.Toggles.ToList();

            foreach(var toggle in toggles)
            {
                responses.Add(_toggleConverter.convert(toggle));
            }
            return responses;
        }
    }
}
