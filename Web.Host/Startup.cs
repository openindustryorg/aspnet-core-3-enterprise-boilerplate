using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Web.Host.Middleware;
using Web.Application.Services;
using Web.EntityFramework;
using Web.Application.Helpers;
using Web.Host.Controllers;

namespace Web.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            
            // configure entity framework DbContext
            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlite(connectionString);
            });

            // wire up dynamic controllers
            services.AddMvc(o => o.Conventions.Add(
                new GenericControllerRouteConvention()
            )).
            ConfigureApplicationPartManager(m => 
                m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()
            ));
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            
            services.AddCors();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddSwaggerGen(options => {
                options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.RelativePath)); 
                options.TagActionsBy(api => new[] { api.RelativePath }); });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();   
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            // migrate database changes on startup (includes initial db creation)
            // context.Database.Migrate();

            // generated swagger json and swagger ui middleware
            app.UseSwagger();

            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "thing.events/ Core"));

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
