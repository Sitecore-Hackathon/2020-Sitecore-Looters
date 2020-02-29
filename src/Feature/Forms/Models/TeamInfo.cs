using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration.Fluent;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;

namespace Hackathon.Boilerplate.Feature.Forms.Models
{
    /// <summary>
    /// TeamInfo
    /// </summary>
    [SitecoreType(TemplateId = "EC4DE6FE-53CC-468F-BF3E-13DFEFFB6A99", AutoMap = true)]
    public class TeamInfo : GlassBase
    {
        /// <summary>
        /// TeamName
        /// </summary>
        public virtual string TeamName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public virtual string Country { get; set; }

        /// <summary>
        /// FirstMemberName
        /// </summary>
        public virtual string FirstMemberName { get; set; }

        /// <summary>
        /// FirstMemberTwitter
        /// </summary>
        public virtual string FirstMemberGithub { get; set; }

        /// <summary>
        /// FirstMemberTwitter
        /// </summary>
        public virtual string FirstMemberTwitter { get; set; }

        /// <summary>
        /// FirstMemberLinkedIn
        /// </summary>
        public virtual string FirstMemberLinkedIn { get; set; }

        /// <summary>
        /// SecondMemberName
        /// </summary>
        public virtual string SecondMemberName { get; set; }

        /// <summary>
        /// SecondMemberGithub
        /// </summary>
        public virtual string SecondMemberGithub { get; set; }

        /// <summary>
        /// SecondMemberTwitter
        /// </summary>
        public virtual string SecondMemberTwitter { get; set; }

        /// <summary>
        /// SecondMemberLinkedIn
        /// </summary>
        public virtual string SecondMemberLinkedIn { get; set; }

        /// <summary>
        /// ThirdMemberName
        /// </summary>
        public virtual string ThirdMemberName { get; set; }

        /// <summary>
        /// ThirdMemberGithub
        /// </summary>
        public virtual string ThirdMemberGithub { get; set; }

        /// <summary>
        /// ThirdMemberTwitter
        /// </summary>
        public virtual string ThirdMemberTwitter { get; set; }

        /// <summary>
        /// ThirdMemberLinkedIn
        /// </summary>
        public virtual string ThirdMemberLinkedIn { get; set; }

    }
}
