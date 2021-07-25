using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicatonProcess.July2021.Web.Extensions
{
  [ExcludeFromCodeCoverage]
  public static class ApiVersioningExtension
  {
    /// <summary>
    /// Add custom api versioning to service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddCustomizedApiVersioning(this IServiceCollection services)
    {
      return services
        .AddApiVersioning(
          options =>
          {
            // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;
          })
        .AddVersionedApiExplorer(
          options =>
          {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
          });
    }
  }
}
