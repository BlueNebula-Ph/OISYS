namespace Oisys.Service
{
    using System.IO;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;
    using Newtonsoft.Json.Serialization;
    using Oisys.Service.Helpers;
    using Oisys.Service.Services;
    using Oisys.Service.Services.Interfaces;
    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>
    /// <see cref="Startup"/> class API configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets read-only property configuration <see cref="IConfigurationRoot"/> class.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "OisysCorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });
            services.AddLogging();

            services.AddAutoMapper();

            services.AddDbContext<OisysDbContext>(opt =>
            {
                opt.UseInMemoryDatabase();

                opt.UseOpenIddict();
            });

            services.AddOpenIddict(opt =>
            {
                opt.AddEntityFrameworkCoreStores<OisysDbContext>();

                opt.AddMvcBinders();

                opt.EnableTokenEndpoint("/connect/token");

                opt.AllowPasswordFlow();

                opt.DisableHttpsRequirement();
            });

            services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc("v1", new Info { Title = "OISYS API", Version = "v1", Description = "Order and Inventory System API" });

                    // Set comments path for swagger
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "Oisys.Service.xml");
                    opt.IncludeXmlComments(xmlPath);
                });

            // Add application services
            services.AddTransient(typeof(ISummaryListBuilder<,>), typeof(SummaryListBuilder<,>));

            // Add adjustment service
            services.AddSingleton<IAdjustmentService, AdjustmentService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("OisysCorsPolicy");

            app.UseOpenIddict();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "OISYS API V1");
                    opt.RoutePrefix = "info";
                });

            OisysDbContext.Seed(app);
        }
    }
}
