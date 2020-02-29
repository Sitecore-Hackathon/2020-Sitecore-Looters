using Glass.Mapper.Sc;
using Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Services
{
    public class MasterSitecoreService : SitecoreService, IMasterSitecoreService
    {
        public MasterSitecoreService() : base("master")
        {

        }
    }
}
