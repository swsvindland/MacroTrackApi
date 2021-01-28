using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MacroTrackApi.Models;
using MacroTrackApi.Repositories;
using MacroTrackApi.Utils;

namespace MacroTrackApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);

            services.AddDbContext<UserContext>(opt => opt.UseSqlServer(JsonUtils.LoadSecrets().Database));
            services.AddDbContext<FoodContext>(opt => opt.UseSqlServer(JsonUtils.LoadSecrets().Database));
            services.AddDbContext<WaterContext>(opt => opt.UseSqlServer(JsonUtils.LoadSecrets().Database));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IWaterRepository, WaterRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}