using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection.Metadata.Ecma335;

namespace EduWork.UI.Configurations
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebAssemblyHostConfiguration configuration) 
        {
            var clientAzureAdOptions = new ClientAzureAdOptions();
            var downstreamApiSettings = new DownstreamApiOptions();
            configuration.GetSection(ClientAzureAdOptions.Section).Bind(clientAzureAdOptions);
            configuration.GetSection(DownstreamApiOptions.Section).Bind(downstreamApiSettings);

            services.AddScoped(sp =>
            {
                var authorizationMessageHandler = sp.GetRequiredService<AuthorizationMessageHandler>();
                authorizationMessageHandler.InnerHandler = new HttpClientHandler();
                authorizationMessageHandler = authorizationMessageHandler.ConfigureHandler(
                    authorizedUrls: new[] { downstreamApiSettings.BaseUrl },
                    scopes: new[] { downstreamApiSettings.Scope });
                return new HttpClient(authorizationMessageHandler)
                {
                    BaseAddress = new Uri(downstreamApiSettings.BaseUrl ?? string.Empty)
                };
            });


            services.AddMsalAuthentication(options =>
            {
                configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(downstreamApiSettings.Scope);
            });

            return services;
        }
    }
}
