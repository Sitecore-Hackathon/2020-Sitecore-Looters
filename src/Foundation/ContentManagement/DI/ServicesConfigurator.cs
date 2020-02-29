using Glass.Mapper.Sc;
using Hackathon.Boilerplate.Foundation.ContentManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.DI
{
    public static class ServicesConfigurator
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services) =>
            services.AddSingleton<ISitecoreService, MasterSitecoreService>();
    }
}
