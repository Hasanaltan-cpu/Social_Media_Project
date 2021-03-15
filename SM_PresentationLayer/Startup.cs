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
using SM_ApplicationLayer.Hubs;
using SM_ApplicationLayer.Models.DTOs;
using SM_ApplicationLayer.Validation.FluentValidation;
using SM_DomainLayer.Entities.Concrete;
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

     
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                 options.UseSqlServer(
                                 Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterServices();//This is for using Services.

            services.AddSignalR();

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection =
            //        Configuration.GetSection("Authentication:Google");
            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});

          
           
            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuth = Configuration.GetSection("Authentication:Google");
                options.ClientId = "15652654321";
                options.ClientSecret = "123asdasew3123123";
            });
            services.AddControllersWithViews().AddNewtonsoftJson()
               .AddFluentValidation();

            services.AddTransient<IValidator<RegisterDto>, RegisterValidation>();
            services.AddTransient<IValidator<LoginDto>, LoginValidation>();
            services.AddTransient<IValidator<ExternalLoginDto>, ExternalLoginValidation>();
            services.AddTransient<IValidator<SendPostDto>, PostValidation>();

        }

       
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

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/Home/Index");
            });

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
