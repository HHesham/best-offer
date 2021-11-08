using BestOffer.src.Domains;
using Microsoft.Extensions.DependencyInjection;

namespace BestOffer.Extensions
{
    public static class DependenciesRegisteration
    {
        public static void AddDomains(this IServiceCollection services)
        {
            services.AddScoped<OffersDomain>();
        }
    }
}
