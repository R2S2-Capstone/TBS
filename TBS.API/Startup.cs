using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TBS.API.Filters;
using TBS.Data.Database;
using TBS.Data.Interfaces.Bids;
using TBS.Data.Interfaces.Notifications;
using TBS.Data.Interfaces.Posts;
using TBS.Data.Interfaces.Users.Authentication;
using TBS.Services.Bids;
using TBS.Services.Notifications;
using TBS.Services.Post;
using TBS.Services.Posts;
using TBS.Services.Users.Authentication;

namespace TBS.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }
 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddCors();

            services.AddFirebaseAuthentication(Configuration["Firebase:Issuer"], Configuration["Firebase:Audience"]);

            services.AddSingleton(Configuration);
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICarrierPostService, CarrierPostService>();
            services.AddScoped<ICarrierBidService, CarrierBidService>();
            services.AddScoped<IShipperPostService, ShipperPostService>();
            services.AddScoped<IShipperBidService, ShipperBidService>();

            services.AddHealthChecks().AddDbContextCheck<DatabaseContext>();

            services.AddDbContext<DatabaseContext>(options =>
              options.UseMySQL(
                  //Use dev database if in development or use the prod database if not
                  _environment.IsDevelopment() == true ? Configuration["ConnectionStrings:DevMySQL"] : Configuration["ConnectionStrings:DevMySQL"],
                  optionsBuilder => { optionsBuilder.MigrationsAssembly("TBS.Data"); }
                  )
              );
            
            if (_environment.IsDevelopment() == true) 
            {
                Configuration["URL"] = Configuration["URLS:DEV"];
            }
            else 
            {
                Configuration["URL"] = Configuration["URLS:PROD"];
            }

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Transportation Bidding System", Version = "v1" });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
            })
            .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory logger)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transportation Bidding System API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHealthChecks("/health");

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            UpdateDatabase(app);

            logger.AddFile($"Logs/TBS.txt");

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        //Will force create/migrate the database
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    context.Database.EnsureCreated();
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        // Database is already migrated. No issues here
                    }

                }
            }
        }
    }
}
