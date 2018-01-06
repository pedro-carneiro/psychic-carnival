namespace ToggleApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ToggleApi.Models.Resources;
    using ToggleApi.Services;
    using ToggleApi.Services.Toggles;
    using AutoMapper;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase("ToggleList"));
            services.AddMvc();
            services.AddAutoMapper();

            services.AddScoped(typeof(ICreateService<ToggleResource>), typeof(ToggleCreateService));
            services.AddScoped(typeof(IReadService<ToggleResource>), typeof(ToggleReadService));
            services.AddScoped(typeof(IUpdateService<ToggleResource>), typeof(ToggleUpdateService));
            services.AddScoped(typeof(IDeleteService<ToggleResource>), typeof(ToggleDeleteService));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
