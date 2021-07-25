using System.Diagnostics.CodeAnalysis;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Hahn.ApplicatonProcess.July2021.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Hahn.ApplicatonProcess.July2021.Web
{
  [ExcludeFromCodeCoverage]
  public class Startup
  {
    private IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration)
    {
      this._configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
      });
      services.AddMvc().AddFluentValidation();
      services.Configure<AppSettings>(_configuration);
      //services.AddResponseCompression();
      services.AddHealthChecks().AddCheck("health", () => HealthCheckResult.Healthy());
      services.AddControllersWithViews();
      services.AddCustomizedApiVersioning();
      services.AddCustomizedSwagger(_configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger(apiVersionDescriptionProvider, "swagger");
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      //app.UseResponseCompression();
      app.UseHealthChecks("/health");
      app.UseHttpsRedirection();
      app.UseFileServer();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
