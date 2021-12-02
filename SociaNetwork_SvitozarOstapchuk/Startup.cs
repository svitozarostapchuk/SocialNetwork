using DAL;
using DAL.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Models;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BLL.Services;
using BLL.Interfaces;
using DAL.Interfaces;
using AutoMapper;
using WebApi.AutomapperProfiles;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SociaNetwork_SvitozarOstapchuk
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
            services
                .AddHttpContextAccessor();

            services
                .AddDbContext<SocialNetworkDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging());

            services
                .Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
;
            services
                .AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<SocialNetworkDbContext>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist"; 
            });

            services.AddSignalR();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserAutomapperProfile());
                mc.AddProfile(new FriendshipAutomapperProfile());
                mc.AddProfile(new MessageAutomapperProfile());
                mc.AddProfile(new UserSearchFilterAutomapperProfile());

            });

            IMapper mapper = mappingConfig.CreateMapper();
            
            services.AddSingleton(mapper);

            services.AddScoped<DbContext, SocialNetworkDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            services.AddTransient<IMessageService, MessageService>();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x => 
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromHours(1)
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole<int>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chatsocket");  
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}