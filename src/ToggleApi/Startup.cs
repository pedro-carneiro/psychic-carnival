namespace ToggleApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;
    using ToggleApi.Services.AppOverrides;
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

            services.AddScoped(typeof(ICreateService<AppOverrideRequest, AppOverrideResponse>), typeof(AppOverrideCreateService));
            services.AddScoped(typeof(IReadService<AppOverrideRequest, AppOverrideResponse>), typeof(AppOverrideReadService));
            services.AddScoped(typeof(IUpdateService<AppOverrideRequest>), typeof(AppOverrideUpdateService));
            services.AddScoped(typeof(IDeleteService<AppOverrideRequest>), typeof(AppOverrideDeleteService));

            services.AddScoped(typeof(IConverter<ToggleRequest, Toggle>), typeof(ToggleRequestConverter));
            services.AddScoped(typeof(IConverter<Toggle, ToggleResponse>), typeof(ToggleResponseConverter));

            services.AddScoped(typeof(IConverter<AppOverrideRequest, AppOverride>), typeof(AppOverrideRequestConverter));
            services.AddScoped(typeof(IConverter<AppOverride, AppOverrideResponse>), typeof(AppOverrideResponseConverter));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
