using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickTrackWeb.Areas.Identity;
using QuickTrackWeb.Services;
using QuickTrackWeb.Services.ClassEntityDataService;
using QuickTrackWeb.Services.StudentDataService;
using QuickTrackWeb.Services.TrackedItemDataService;
using QuickTrackWeb.Services.WeekDataService;
using QuickTrackWeb.Services.ProgressRecordDataService;
using QuickTrackWeb.Services.DownloadFile;
using QuickTrackWeb.EmbeddedApi.Services;
using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using QuickTrackWeb.EmbeddedApi.Repository;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace QuickTrackWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //START: MVC STUFF FOR CONTROLLERS
            services.AddControllers();
            services.AddMvc()
               .AddMvcOptions(o => o.OutputFormatters.Add(
                   new XmlDataContractSerializerOutputFormatter()))
               ;
            //END: MVC STUFF FOR CONTROLLERS


            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseNpgsql(

            //options.UseSqlServer(


#if DEBUG
            Configuration.GetConnectionString("DefaultConnection")));
#else
            GetPostgresqlConnectionString()));
#endif

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });



            services.AddHttpClient<IClassEntityDataService, DefaultClassEntityDataService>(client =>
            {
                client.BaseAddress = new Uri("https://quicktrack.rc-traviolia.com/");
            });

            services.AddHttpClient<IStudentDataService, DefaultStudentDataService>(client =>
            {
                client.BaseAddress = new Uri("https://quicktrack.rc-traviolia.com/");
            });
            services.AddHttpClient<ITrackedItemDataService, DefaultTrackedItemDataService>(client =>
            {
                client.BaseAddress = new Uri("https://quicktrack.rc-traviolia.com/");
            });
            services.AddHttpClient<IWeekDataService, DefaultWeekDataService>(client =>
            {
                client.BaseAddress = new Uri("https://quicktrack.rc-traviolia.com/");
            });
            services.AddHttpClient<IProgressRecordDataService, DefaultProgressRecordDataService>(client =>
            {
                client.BaseAddress = new Uri("https://quicktrack.rc-traviolia.com/");
            });

            //For the Standalone Api: client.BaseAddress = new Uri("https://localhost:44369/");

            //MVC ADD
            services.AddScoped<IQuickTrackEmbeddedApiRepository, EmbeddedQuickTrackApiRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            //MVC ADD


            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddTransient<IDownloadFileService, DownloadFileService>();
            
            services.AddScoped<SessionDataService>();

            //DI Broken?
            //I want to inject these into a base class, but can't
            //services.AddTransient<UserManager<IdentityUser>>();
            //services.AddTransient<RoleManager<IdentityUser>>();
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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //DEPRECATED (I think)
                //For DownloadFileController
                //endpoints.MapControllerRoute(
                //   name: "default",
                //   pattern: "{controller}/{action}");

                endpoints.MapControllers();

                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private string GetPostgresqlConnectionString()
        {
            var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            // Parse connection URL to connection string for Npgsql
            connUrl = connUrl.Replace("postgres://", string.Empty);
            var pgUserPass = connUrl.Split("@")[0];
            var pgHostPortDb = connUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];
            string connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
            return connStr;
        }
    }
}
