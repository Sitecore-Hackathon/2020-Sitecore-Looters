using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Services
{
    public class MasterSitecoreService: SitecoreService,ISitecoreService
    {
        public MasterSitecoreService() : base("master")
        {

        }
    }
}
