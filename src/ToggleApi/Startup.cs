using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToggleApi.Models;
using ToggleApi.Models.Resources;
using ToggleApi.Services;
using ToggleApi.Services.Toggles;
using AutoMapper;

namespace ToggleApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToggleContext>(opt => opt.UseInMemoryDatabase("ToggleList"));
            services.AddMvc();
            services.AddAutoMapper();

            services.AddSingleton(typeof(ICreateService<ToggleResource>), typeof(ToggleCreateService));
            services.AddSingleton(typeof(IReadService<ToggleResource>), typeof(ToggleReadService));
            services.AddSingleton(typeof(IUpdateService<ToggleResource>), typeof(ToggleUpdateService));
            services.AddSingleton(typeof(IDeleteService<ToggleResource>), typeof(ToggleDeleteService));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
