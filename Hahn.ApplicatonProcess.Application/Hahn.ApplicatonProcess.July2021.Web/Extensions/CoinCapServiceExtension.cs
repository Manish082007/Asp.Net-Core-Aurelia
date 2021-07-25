using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using Hahn.ApplicatonProcess.July2021.Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Hahn.ApplicatonProcess.July2021.Web.Extensions
{
  /// <summary>
  /// Extension class on <see cref="IServiceCollection"/>
  /// </summary>
  [ExcludeFromCodeCoverage]
  public static class CoinCapServiceExtension
  {
    /// <summary>
    /// Registers the coin cap assets services
    /// </summary>
    /// <param name="serviceCollection">DI Container</param>
    /// <param name="configuration">Application configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddCoinCapService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
      serviceCollection
        .AddHttpClient("CoinCapHttpClient", client =>
        {
          client.BaseAddress = new Uri(configuration.GetValue<string>(Configuration.CoinCapApiUrl));
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip, deflate, br"));
        })
        .AddPolicyHandler(
          GetRetryPolicy(
            configuration.GetValue<int>(Configuration.CoinCapApiMaxRetries), 
            configuration.GetValue<int>(Configuration.CoinCapApiBackoffFactor)
          )
        )
        .AddPolicyHandler(GetTimeoutPolicy(configuration.GetValue<int>(Configuration.CoinCapApiHttpTimeout)));

      return serviceCollection;
    }

    /// <summary>
    /// Sets the retry and exponential backoff policy for coin cap assets api
    /// </summary>
    /// <returns>Policy for Http Response Message</returns>
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int maxRetries, int backOffFactor) =>
      HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(backOffFactor, retryAttempt)));

    /// <summary>
    /// Sets the timeout policy for coin cap assets api
    /// </summary>
    /// <returns>Policy for Http Response Message</returns>
    private static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(double httpTimeout) =>
      Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMinutes(httpTimeout));
  }
}
