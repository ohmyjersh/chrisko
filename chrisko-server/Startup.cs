using ChrisKo.Cache;
using ChrisKo.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChrisKo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("ConnectionStrings:MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("ConnectionStrings:MongoConnection:Database").Value;
            });
            services.AddTransient<IChriskoRepository, ChriskoRepository>();

                services.AddSingleton<IDistributedCache>(factory =>
                {
                    return new RedisCache(new RedisCacheOptions
                    {
                        Configuration = Configuration.GetSection("ConnectionStrings:RedisConnection:Database").Value,
                        InstanceName = Configuration.GetSection("ConnectionStrings:RedisConnection:Database").Value
                    });
                });
                services.AddSingleton<IRedisService, RedisService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder =>
                    builder.WithOrigins("*"));
            app.UseMvc();
        }
    }
}
