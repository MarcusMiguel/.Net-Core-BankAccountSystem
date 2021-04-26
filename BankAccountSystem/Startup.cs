using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BankAccountSystem.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankAccountSystem
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
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("BankAccountSystem"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/SignIn";
            }
            );
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddNotyf(config => { config.DurationInSeconds = 3  ; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Index}/");

                endpoints.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            IdentityResult result;
            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            ApplicationUser admin = await userManager.FindByEmailAsync("admin@admin");

            if (admin == null)
            {
                admin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@admin",
              
                };
                await userManager.CreateAsync(admin, "Password");
            }
            await userManager.AddToRoleAsync(admin, "Admin");

            ApplicationUser user1 = await userManager.FindByEmailAsync("user1@email");

            if (user1 == null)
            {
                user1 = new ApplicationUser()
                {
                    UserName = "user1",
                    Email = "user1@email",
                    TipoConta = TipoConta.Pessoa_Física,
                    Credito = 800,
                    Saldo = 1200,
                    NumeroConta = 637550448623894768
                };
                await userManager.CreateAsync(user1, "Password");
            }

            ApplicationUser user2 = await userManager.FindByEmailAsync("user2@email");

            if (user2 == null)
            {
                user2 = new ApplicationUser()
                {
                    UserName = "user2",
                    Email = "user2@email",
                    TipoConta = TipoConta.Pessoa_Jurídica,
                    Credito = 800,
                    Saldo = 0,
                    NumeroConta = 637550448624023506
                };
                await userManager.CreateAsync(user2, "Password");
            }

        }
    }
}
