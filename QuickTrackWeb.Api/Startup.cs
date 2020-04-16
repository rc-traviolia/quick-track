using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickTrackWeb.Api.Services;
using QuickTrackWeb.Api.Repository;

namespace QuickTrackWeb.Api
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
            services.AddControllers();
            services.AddMvc()
               .AddMvcOptions(o => o.OutputFormatters.Add(
                   new XmlDataContractSerializerOutputFormatter()))
               ;
            services.AddDbContext<ApplicationDbContext>(options =>
                         //options.UseNpgsql(
                         options.UseSqlServer(

            //Configuration.GetConnectionString("DefaultConnection")));
#if DEBUG
            Configuration.GetConnectionString("DefaultConnection")));
#else
            GetPostgresqlConnectionString()));
#endif


            //The rest of the services
            services.AddScoped<IQuickTrackApiRepository, DefaultQuickTrackApiRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {

            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
