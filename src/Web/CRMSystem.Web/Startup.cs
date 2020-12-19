using System;
using CloudinaryDotNet;
using CRMSystem.Services;
using CRMSystem.Services.Contracts;
using CRMSystem.Services.Data.Contracts;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;

namespace CRMSystem.Web
{
    using System.Reflection;

    using CRMSystem.Data;
    using CRMSystem.Data.Common;
    using CRMSystem.Data.Common.Repositories;
    using CRMSystem.Data.Models;
    using CRMSystem.Data.Repositories;
    using CRMSystem.Data.Seeding;
    using CRMSystem.Services.Data;
    using CRMSystem.Services.Mapping;
    using CRMSystem.Services.Messaging;
    using CRMSystem.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //Add HangFire
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSqlServerStorage(
                        this.configuration.GetConnectionString("DefaultConnection"),
                        new SqlServerStorageOptions
                        {
                            PrepareSchemaIfNecessary = true,
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true,
                        });
            });

            //Add Facebook authentication
            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = this.configuration["Facebook:AppId"];
                    facebookOptions.AppSecret = this.configuration["Facebook:AppSecret"];
                    facebookOptions.Scope.Add("public_profile");
                    facebookOptions.Fields.Add("picture");

                });

            //add Google authentication
            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {

                    googleOptions.ClientId = this.configuration["Google:ClientId"];
                    googleOptions.ClientSecret = this.configuration["Google:ClientSecret"];
                    googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
                });


            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();


            //todo it would be unnecessary
            services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });



            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton(this.configuration);

            //Add Cloudinary
            var cloudinary = new Cloudinary(new Account()
            {
                Cloud = this.configuration["Cloudinary:CloudName"],
                ApiKey = this.configuration["Cloudinary:ApiKey"],
                ApiSecret = this.configuration["Cloudinary:ApiSecret"],
            });

            services.AddSingleton(cloudinary);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IOrganizationsService, OrganizationsService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IPhonesServices, PhonesServices>();
            services.AddTransient<IAddressesService, AddressesService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<ITemporaryCustomersService, TemporaryCustomersService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAccountsService, AccountsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard();


            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                        endpoints.MapControllers();
                        endpoints.MapHangfireDashboard();
                    });
        }
    }
}
