namespace ToggleApi.Services.Toggles
{
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;

    public class ToggleCreateService : ICreateService<ToggleRequest, ToggleResponse>
    {
        private readonly ApiDbContext _context;
        private readonly IConverter<ToggleRequest, Toggle> _toggleConverter;
        private readonly IConverter<Toggle, ToggleResponse> _responseConverter;

        public ToggleCreateService(ApiDbContext context,
                                   IConverter<ToggleRequest, Toggle> toggleConverter,
                                   IConverter<Toggle, ToggleResponse> responseConverter)
        {
            _context = context;
            _toggleConverter = toggleConverter;
            _responseConverter = responseConverter;
        }

        ToggleResponse ICreateService<ToggleRequest, ToggleResponse>.Create(ToggleRequest request)
        {
            var toggle = _toggleConverter.convert(request);

            _context.Toggles.Add(toggle);
            _context.SaveChanges();

            return _responseConverter.convert(toggle);
        }
    }
}
