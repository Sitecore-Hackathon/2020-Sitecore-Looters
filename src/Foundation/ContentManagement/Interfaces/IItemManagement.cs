using Glass.Mapper.Sc;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces
{
    public interface IItemManagement
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

        /// <summary>
        /// Returns a an item by using the item path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Sitecore Item</returns>
        T GetItemByPath<T>(string path) where T : GlassBase;
    }
}
