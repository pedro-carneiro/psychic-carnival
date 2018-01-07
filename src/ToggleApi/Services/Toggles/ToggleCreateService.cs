namespace ToggleApi.Services.Toggles
{
    using AutoMapper;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;

    public class ToggleCreateService : ICreateService<ToggleRequest, ToggleResponse>
    {
        private readonly ApiDbContext _context;

        public ToggleCreateService(ApiDbContext context)
        {
            _context = context;
        }

        ToggleResponse ICreateService<ToggleRequest, ToggleResponse>.Create(ToggleRequest request)
        {
            var toggle = Mapper.Map<Toggle>(request);

            _context.Toggles.Add(toggle);
            _context.SaveChanges();

            return Mapper.Map<ToggleResponse>(toggle);
        }
    }
}
