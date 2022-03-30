using BusinessLayer;
using BusinessLayer.Contracts;
using DataAccess;
using DataAccess.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //configure DbContext 
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            //Configure the Services
            services.AddTransient<IShowsService, ShowsService>();
            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IShowsService, ShowsService>();
            services.AddTransient<IShowsRepository, ShowsRepository>();
            services.AddTransient<ITicketsRepository, TicketsRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddEntityFrameworkStores<AppDbContext>()
                  .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Theater_assignment1", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            // if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theater v1"));
            // }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AppDbInitializer.Seed(app);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            CreateUserRoles(serviceProvider).Wait();
            CreateStartupUsers(serviceProvider);
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("User"));
            }

            //Adding Cashier Role
            roleCheck = await RoleManager.RoleExistsAsync("Cashier");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Cashier"));
            }
        }

        private void CreateStartupUsers(IServiceProvider serviceProvider)
        {
            var userMgr = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var users = userMgr.Users;
            if (!users.Any(x => x.UserName == "admin@webdotnet.com"))
            {
                var user = new IdentityUser { UserName = "admin@webdotnet.com" };
                userMgr.CreateAsync(user, "P@ssw0rd").Wait();
                var registeredUser = userMgr.FindByNameAsync(user.UserName).Result;
                userMgr.AddToRoleAsync(registeredUser, "admin").Wait();
            }
        }
    }
}
