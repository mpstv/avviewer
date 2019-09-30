using avviewer.Providers;
using avviewer.Providers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace avviewer
{
  public class Startup
  {
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseMiddleware<ExceptionMiddleware>();
      Configuration = new ConfigurationBuilder()
                  .SetBasePath(env.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .Build();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}"
        );
      });

      app.UseStaticFiles();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddMemoryCache();

      services.AddTransient<ITimeSeriesProvider, TimeSeriesProvider>();
      services.AddTransient<IAvApiProvider, AvApiProvider>();
    }
  }
}
