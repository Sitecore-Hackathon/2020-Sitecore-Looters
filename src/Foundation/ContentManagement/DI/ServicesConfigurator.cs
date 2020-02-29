using Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces;
using Hackathon.Boilerplate.Foundation.ContentManagement.Repositories;
using Hackathon.Boilerplate.Foundation.ContentManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.DI
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMasterSitecoreService, MasterSitecoreService>()
                .AddTransient<IItemManagement, ItemManagement>();
        }
    }
}
