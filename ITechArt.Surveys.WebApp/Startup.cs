using iTechArt.Common.Time;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Models;
using iTechArt.Surveys.Foundation.Services.Account;
using iTechArt.Surveys.Foundation.Services.Answers;
using iTechArt.Surveys.Foundation.Services.Files;
using iTechArt.Surveys.Foundation.Services.Surveys;
using iTechArt.Surveys.Foundation.Services.Users;
using iTechArt.Surveys.Repositories;
using iTechArt.Surveys.Repositories.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace iTechArt.Surveys.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.Configure<FileServiceConfiguration>(Configuration.GetSection("FileService"));
            
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoleStore<RoleStore>()
                .AddUserStore<UserStore>()
                .AddDefaultTokenProviders();

            services.AddDbContext<SurveysDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISurveysUnitOfWork, SurveysUnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyAnswersService, SurveySurveyAnswersService>();
            services.AddScoped<IFileService, FileService>();

            services.AddSingleton<ISystemClock, SystemClock>();
            services.AddSingleton<FileExtensionContentTypeProvider>();
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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}