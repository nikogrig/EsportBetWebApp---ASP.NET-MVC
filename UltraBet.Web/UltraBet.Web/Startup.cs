using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltraBet.Data.Models;
using AutoMapper;
using UltraBet.Data;
using UltraBet.Infrastructure.DbExtension;
using UltraBet.Web.Hubs;
using UltraBet.Infrastructure.ServiceExtension;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using UltraBet.Services.BackGroundService.Contracts;
using UltraBet.Infrastructure.BackgroundJobExtension;

namespace UltraBet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BetPlusDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

           services.AddHangfire(x =>
               x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<BetPlusDbContext>()
                .AddDefaultTokenProviders();

            services.AddDomainServices();

            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddAutoMapper();

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBackGroundService bgr)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseHangfireServer();
            
            app.UseHangfireDashboard();
            
            app.UseBackGroundJob(bgr);

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("notifications");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
