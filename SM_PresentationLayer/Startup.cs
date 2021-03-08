using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Validation.FluentValidation;
using SM_InfrastuctureLayer.Context;
using SM_IoCLayer;

namespace SM_PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                 options.UseSqlServer(
                                 Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterServices();//This is for using Services.


            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.AddControllersWithViews().AddNewtonsoftJson()
               .AddFluentValidation();

            services.AddTransient<IValidator<RegisterDto>, RegisterValidation>();
            services.AddTransient<IValidator<LoginDto>, LoginValidation>();
            services.AddTransient<IValidator<ExternalLoginDto>, ExternalLoginValidation>();
            services.AddTransient<IValidator<SendPostDto>, PostValidation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(name: "profile",
                    pattern: "profile/{userName}",
                    defaults: new { controller = "Profile", action = "Detail" });

                endpoints.MapControllerRoute(name: "post",
                    pattern: "post/{id}",
                    defaults: new { controller = "Post", action = "PostDetail" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
