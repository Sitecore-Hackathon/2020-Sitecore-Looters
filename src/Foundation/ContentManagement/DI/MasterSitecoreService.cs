using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Services
{
    public static class CoreServicesConfigurator
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
