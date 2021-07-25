using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Hahn.ApplicatonProcess.July2021.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Hahn.ApplicatonProcess.July2021.Web.Extensions
{
  /// <summary>
  /// Swagger extensions.
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class SwaggerExtentions
  {
    /// <summary>
    /// Add swagger gen service to service collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSwaggerExamples();
      
      return services
        .AddSwaggerGen(c =>
        {
          var apiInfo = new OpenApiInfo()
          {
            Title = configuration.GetValue<string>(Configuration.Title),
            Version = "v1",
            Description = configuration.GetValue<string>(Configuration.Description)
          };

          c.SwaggerDoc("v1", apiInfo);

          // set the comments path for the swagger json and ui
          var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
          var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
          c.IncludeXmlComments(xmlPath);
          c.ExampleFilters();
          c.OperationFilter<AddResponseHeadersFilter>();
          c.EnableAnnotations();
        });
    }

    /// <summary>
    /// Use swagger gen service in application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="apiVersionDescriptionProvider">The api version description provider.</param>
    /// <param name="swaggerRoutePrefix">The swagger route prefix.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider, string swaggerRoutePrefix)
    {
      EnableSwaggerLowerCasePaths(app, swaggerRoutePrefix);

      return app.UseSwaggerUI(options =>
      {
        //options.RoutePrefix = swaggerRoutePrefix;
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/{swaggerRoutePrefix}/{description.GroupName}/swagger.json", description.GroupName);
        }
      });
    }

    private static void EnableSwaggerLowerCasePaths(IApplicationBuilder app, string swaggerRoutePrefix)
    {
      app.UseSwagger(
        c =>
        {
          //c.RouteTemplate = swaggerRoutePrefix + "/{documentName}/swagger.json";
          c.PreSerializeFilters.Add((document, request) =>
            {
              //Get resource names/ paths with lowercase Key
              var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
              //Remove all paths
              document.Paths.Clear();
              foreach (var pathItem in paths)
              {
                //Add resource names/ paths in lowercase
                document.Paths.Add(pathItem.Key, pathItem.Value);
              }
            });
        });
    }
  }
}
