using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sneaker_Bar.Models;
using Sneaker_Bar.Model;
using Sneaker_Bar.Hubs;
using Sneaker_Bar.Services;
using Sneaker_Bar.Configuration;
using Microsoft.AspNetCore.SignalR;
using Sneaker_Bar.Services.UserConnections;

namespace Sneaker_Bar
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
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            services.AddDateService();
            services.AddRepositories();
            services.AddSignalR();

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("connectionString")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSingleton<IMailService, MailService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
       
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else if (env.IsEnvironment("Production"))
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error/");
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "confirmation",
                    pattern: "{controller=Sneakers}/{action=OrderConfirmation}/{id?}");
                endpoints.MapHub<NotificationHub>("/notification");
                endpoints.MapRazorPages();
            });
        }
    }
}
