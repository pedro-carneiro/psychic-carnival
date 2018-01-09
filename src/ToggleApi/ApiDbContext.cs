namespace ToggleApi
{
    using Microsoft.EntityFrameworkCore;
    using ToggleApi.Models.Entities;

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Toggle> Toggles { get; set; }
        public DbSet<AppOverride> AppOverrides { get; set; }
    }
}
