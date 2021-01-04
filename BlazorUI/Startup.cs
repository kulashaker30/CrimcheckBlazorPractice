using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorUI.Data;
using OnlineClinic.Data.Repositories;
using OnlineClinic.Data.Entities;
using OnlineClinic.Core.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OnlineClinic.Core.Mappings;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineClinic.Core.Utilities;

namespace BlazorUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddDbContext<OnlineClinicDbContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(Configuration.GetConnectionString("OnlineClinic"));
                }
            );

            services.AddAutoMapper(typeof(OnlineClinicProfile));

            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordEncryptionUtil>(opts =>
            {
                var passwordSecurityKey = Environment.GetEnvironmentVariable("PasswordSecurityKey");
                return new PasswordEncryptionUtil(passwordSecurityKey);
            });
            
            services.AddScoped<CustomAuthStateProvider>(); // AuthenticationStateProvider

            services.AddAuthentication();
            services.AddAuthorization();
            
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
