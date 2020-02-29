using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces
{
    interface IItemManagement
    {
        /// <summary>Add a new Site-core item</summary>
        /// <param name="child">Child item</param>
        /// <param name="parentPath">Parent item path</param>
        /// <returns>New item created</returns>
        T CreateSitecoreItemUsingParentPath<T>(T child, string parentPath) where T : GlassBase;

        /// <summary>Add a new Site-core item</summary>
        /// <param name="child">Child item</param>
        /// <param name="parentId">Parent item id</param>
        /// <returns>New item created</returns>
        T CreateSitecoreItem<T>(T child, string parentId) where T : GlassBase;
    }
}
