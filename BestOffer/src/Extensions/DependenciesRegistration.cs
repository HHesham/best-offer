using BestOffer.Client;
using BestOffer.Domains;
using BestOffer.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestOffer.Extensions
{
    public static class DependenciesRegisteration
    {
        public static IServiceCollection AddDomains(this IServiceCollection services)
        {
            services.AddScoped<OffersDomain>();
            return services;
        }

        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OffersHttpClient>().Configure<ConfigKeys>(configuration.GetSection("BestOffers"));
            services.AddSingleton<OffersClient>().Configure<ConfigKeys>(configuration.GetSection("BestOffers"));
            return services;
        }

    }
}
