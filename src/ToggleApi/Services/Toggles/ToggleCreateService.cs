namespace ToggleApi.Services.Toggles
{
    using AutoMapper;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Resources;
    using ToggleApi.Services;

    public class ToggleCreateService : ICreateService<ToggleResource>
    {
        private readonly ApiDbContext _context;

        public ToggleCreateService(ApiDbContext context)
        {
            _context = context;
        }

        ToggleResource ICreateService<ToggleResource>.Create(ToggleResource resource)
        {
            var toggle = Mapper.Map<Toggle>(resource);

            _context.Toggles.Add(toggle);
            _context.SaveChanges();

            return Mapper.Map<ToggleResource>(toggle);
        }
    }
}
