using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Models
{
    using System;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    namespace SaudiAirlines.Foundation.Core.BaseClasses
    {
        // Glass base class
        public abstract class GlassBase
        {
            /// <summary>
            /// Get or set Name property
            /// </summary>
            [SitecoreInfo(SitecoreInfoType.Name)]
            public virtual string Name { get; set; }

            /// <summary>
            /// Get or set Id property
            /// </summary>
            [SitecoreId]
            public virtual Guid Id { get; set; }

            /// <summary>
            /// Get or Set the Language Property
            /// </summary>
            [SitecoreInfo(SitecoreInfoType.Language)]
            public virtual string Language { get; set; }

            /// <summary>
            /// Get or set DisplayName property
            /// </summary>
            [SitecoreInfo(SitecoreInfoType.DisplayName)]
            public virtual string DisplayName { get; set; }

            /// <summary>
            /// Template Id
            /// </summary>
            [SitecoreInfo(SitecoreInfoType.TemplateId)]
            public virtual Guid TemplateId { get; set; }
        }

        // Glass parent class
        public class GlassParent
        {
            /// <summary>
            /// Get or set Id property
            /// </summary>
            [SitecoreId]
            public virtual Guid Id { get; set; }
        }
    }

}
