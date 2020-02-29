using Glass.Mapper.Sc.Configuration.Fluent;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;

namespace Hackathon.Boilerplate.Feature.Forms.Models
{
    /// <summary>
    /// TeamInfo
    /// </summary>
    public class TeamInfo : GlassBase
    {
        /// <summary>
        /// TeamName
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// FirstMemberName
        /// </summary>
        public string FirstMemberName { get; set; }

        /// <summary>
        /// FirstMemberTwitter
        /// </summary>
        public string FirstMemberGithub { get; set; }

        /// <summary>
        /// FirstMemberTwitter
        /// </summary>
        public string FirstMemberTwitter { get; set; }

        /// <summary>
        /// FirstMemberLinkedIn
        /// </summary>
        public string FirstMemberLinkedIn { get; set; }

        /// <summary>
        /// SecondMemberName
        /// </summary>
        public string SecondMemberName { get; set; }

        /// <summary>
        /// SecondMemberGithub
        /// </summary>
        public string SecondMemberGithub { get; set; }

        /// <summary>
        /// SecondMemberTwitter
        /// </summary>
        public string SecondMemberTwitter { get; set; }

        /// <summary>
        /// SecondMemberLinkedIn
        /// </summary>
        public string SecondMemberLinkedIn { get; set; }

        /// <summary>
        /// ThirdMemberName
        /// </summary>
        public string ThirdMemberName { get; set; }

        /// <summary>
        /// ThirdMemberGithub
        /// </summary>
        public string ThirdMemberGithub { get; set; }

        /// <summary>
        /// ThirdMemberTwitter
        /// </summary>
        public string ThirdMemberTwitter { get; set; }

        /// <summary>
        /// ThirdMemberLinkedIn
        /// </summary>
        public string ThirdMemberLinkedIn { get; set; }

        public static SitecoreType<TeamInfo> Load()
        {
            var type = new SitecoreType<TeamInfo>();
            type.TemplateId("{EC4DE6FE-53CC-468F-BF3E-13DFEFFB6A99}");
            type.AutoMap();
            return type;
        }

    }
}
