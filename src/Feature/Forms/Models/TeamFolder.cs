using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;

namespace Hackathon.Boilerplate.Feature.Forms.Models
{
    [SitecoreType(TemplateId = "B9EEFD6A-8628-4C18-9958-A03E088588F0", AutoMap = true)]
    public class TeamFolder : GlassBase
    {
        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<TeamInfo> Teams { get;set; }

    }
}
