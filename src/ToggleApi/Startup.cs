namespace ToggleApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;
    using ToggleApi.Services.Toggles;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase("ToggleList"));
            services.AddMvc();

            services.AddScoped(typeof(ICreateService<ToggleRequest, ToggleResponse>), typeof(ToggleCreateService));
            services.AddScoped(typeof(IReadService<ToggleRequest, ToggleResponse>), typeof(ToggleReadService));
            services.AddScoped(typeof(IUpdateService<ToggleRequest>), typeof(ToggleUpdateService));
            services.AddScoped(typeof(IDeleteService<ToggleRequest>), typeof(ToggleDeleteService));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
