using System;
using Glass.Mapper.Sc;
using Hackathon.Boilerplate.Foundation.ContentManagement.Interfaces;
using Hackathon.Boilerplate.Foundation.ContentManagement.Models.SaudiAirlines.Foundation.Core.BaseClasses;
using Sitecore.SecurityModel;

namespace Hackathon.Boilerplate.Foundation.ContentManagement.Repositories
{
    public class ItemManagement : IItemManagement
    {
        private readonly ISitecoreService _sitecoreService;

        public ItemManagement(ISitecoreService sitecoreService)
        {
            _sitecoreService = sitecoreService;

        }

        /// <inheritdoc />
        public T CreateSitecoreItemUsingParentPath<T>(T child, string parentPath) where T : GlassBase
        {
            // Check if the child or the parentPath is null 
            if (child == null)
                throw new ArgumentNullException(nameof(child));
            if (string.IsNullOrWhiteSpace(parentPath))
                throw new ArgumentNullException(nameof(parentPath));

            //Get the parent item using the parent model from Sitecore
            var parentItem = _sitecoreService.GetItem<GlassParent>(
                new GetItemByPathOptions { Path = parentPath });

            if (parentItem == null)
                throw new ArgumentNullException($"ParentItem is null for parentPath of '{parentPath}'");

            using (new SecurityDisabler())
            {

                return _sitecoreService.CreateItem<T>(
                    new CreateByModelOptions { Model = child, Parent = parentItem });

            }
        }

        /// <inheritdoc />
        public T CreateSitecoreItem<T>(T child, string parentId) where T : GlassBase
        {
            ///// Check if the child or the parentPath is null 
            if (child == null)
                throw new ArgumentNullException(nameof(child));
            if (string.IsNullOrWhiteSpace(parentId))
                throw new ArgumentNullException(nameof(parentId));
            Guid id;
            Guid.TryParse(parentId, out id);
            if (id == null || id == Guid.Empty)
            {
                Sitecore.Diagnostics.Log.Error($"Error getting item by id:: provided id = {parentId} ", this);
                return null;
            }
            //Get the parent item using the parent model from Sitecore
            var parentItem = _sitecoreService.GetItem<GlassParent>(
                new GetItemByIdOptions { Id = id });

            if (parentItem == null)
                throw new ArgumentNullException($"ParentItem is null for parentId of '{parentId}'");

            using (new SecurityDisabler())
            {

                return _sitecoreService.CreateItem<T>(
                    new CreateByModelOptions { Model = child, Parent = parentItem });

            }
        }

        /// <inheritdoc />
        public T GetItemByPath<T>(string path) where T : GlassBase
        {
            try
            {
                T item = _sitecoreService.GetItem<T>(path);
                return item;
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error($"Error getting item with path = {path} : {ex.Message}",ex,this);
                return null;
            }
        }
    }
}
