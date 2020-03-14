using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TruckManager.Persistence.MongoDB;
using Serilog;
using TruckManager.Application.Persistence;
using TruckManager.Api.Filters;
using FluentValidation.AspNetCore;

namespace TruckManager.Api
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
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelAttribute());
                options.Filters.Add(new ErrorFormatResult());
            })
            .AddFluentValidation()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("QueryHandler")))
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("QueryHandler")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("CommandHandler")))
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("CommandHandler")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("QueryValidator")))
                    .AsSelf()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.Where(type => type.Name.Equals("CommandValidator")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            var databaseSettings = Configuration.GetSection("Database");

            services.AddSingleton<IMongoDBService>(x => new DatabaseService(
                    connectionString: databaseSettings["ConnectionString"],
                    database: databaseSettings["DatabaseName"]));

            services.AddSingleton(x => Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
