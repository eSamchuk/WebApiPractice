using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using Nms.StaticData;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities;
using NoMansSkyRecipies.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using NoMansSkyRecipies.CQRS.Commands;
using NoMansSkyRecipies.CQRS.Queries;

namespace NoMansSkyRecipies.Controllers.v3
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3.0")]
    public class ResourcesController : BaseController
    {
        /// <summary>
        /// repository for Resources
        /// </summary>
        private readonly IResourceRepository _resourceRepository;

        /// <summary>
        /// repository for Recipes
        /// </summary>
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMediator _mediator;

        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="recipeRepository">repository for Recipes</param>
        /// <param name="resourceRepository">repository for Resources</param>
        /// <param name="mediator">mediator</param>
        /// <param name="linkGenerator">Link generator</param>
        public ResourcesController(IResourceRepository resourceRepository,
            IRecipeRepository recipeRepository,
            IMediator mediator, LinkGenerator linkGenerator
            )
        {
            this._resourceRepository = resourceRepository;
            this._recipeRepository = recipeRepository;
            this._mediator = mediator;
            this._linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Get list of all existing Resources
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetAllResources()
        {
            var query = new GetResourcesQuery();
            var resources = await this._mediator.Send(query);
            return resources.Any() ? (ActionResult)Ok(resources) : NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Ok if result, otherwise - NoContent</returns>
        [HttpGet("{id:int}", Name = "GetResourceById")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetResourceById(int id)
        {
            var query = new GetResourceByIdQuery(id);
            var resource = await this._mediator.Send(query);
            return resource != null ? (IActionResult)Ok(resource) : NotFound();
        }

        /// <summary>
        /// Get list of all existing Resources names
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet("Simplified")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetSimplifiedResources()
        {
            var query = new GetSimplifiedResourcesQuery();
            var result = await this._mediator.Send(query);

            return result.Any() ? (IActionResult)Ok(result) : NotFound();

        }

        /// <summary>
        /// Gets all Resources which belongs to specified category
        /// </summary>
        /// <param name="resourceCategory">resources category to match with</param>
        /// <returns>Ok if resourceCategory is valid and resulting collection is not empty
        /// NoContent if resourceCategory is valid but resulting collection is empty
        /// otherwise BadRequest</returns>
        [HttpGet("{resourceCategory:alpha}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetResourcesByCategory(string resourceCategory)
        {
            ActionResult result = null;
            Expression<Func<RawResource, bool>> predicate = null;

            switch (resourceCategory)
            {
                case SimplifiedResourceCategories.Gases:
                    predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.Gas;
                    break;
                case SimplifiedResourceCategories.Plants:
                    predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.HarvestedPlant;
                    break;
                case SimplifiedResourceCategories.Minerals:
                    predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.MinedMineral;
                    break;
                default:
                    ModelState.AddModelError("Category", "Unknown resource category");
                    break;
            }

            if (ModelState.IsValid)
            {
                var request = new GetResourceByCategoryQuery(predicate);
                var resources = await this._mediator.Send(request);


                result = resources.Any() ? (ActionResult)Ok(resources) : NotFound();
            }
            else
            {
                result = BadRequest(ModelState.GetFormattedDictionary());
            }

            return result;
        }

        /// <summary>
        /// Gets list of all CraftableItems where specified resource is used in
        /// </summary>
        /// <param name="resourceName">name of resource</param>
        /// <returns>Ok if specified resource exist and resulting collection is not empty
        /// NoContent if specified resource exist but resulting collection is empty
        /// NotFound if specified resource does not exist</returns>
        [HttpGet("{resourceName:alpha}/UsedIn")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> UsedInRecipies(string resourceName)
        {
            ActionResult result = NotFound();

            var targetResource = await Task.FromResult(this._resourceRepository.GetItemByName(resourceName));

            if (targetResource != null)
            {
                var recipes = await Task.FromResult(this._recipeRepository.GetItemsByCondition(x =>
                        x.NeededResources.Select(z => z.RawResource.Name).Contains(resourceName))
                    .Select(x => x.ResultingItem.Name));

                result = recipes.Any() ? (ActionResult)Ok(recipes) : NoContent();
            }

            return result;
        }

        /// <summary>
        /// Gets list of all CraftableItems where specified resource is used in
        /// </summary>
        /// <param name="resourceId">id of resource</param>
        /// <returns>Ok if specified resource exist and resulting collection is not empty
        /// NoContent if specified resource exist but resulting collection is empty
        /// NotFound if specified resource does not exist</returns>
        [HttpGet("{resourceId:int:min(1)}/UsedIn")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> UsedInRecipies(int resourceId)
        {
            ActionResult result = NotFound();

            var targetResource = await Task.FromResult(this._resourceRepository.GetItemById(resourceId));

            if (targetResource != null)
            {
                var recipes = await Task.FromResult(this._recipeRepository.GetItemsByCondition(x =>
                        x.NeededResources.Select(z => z.RawResource.Id).Contains(resourceId))
                    .Select(x => x.ResultingItem.Name));

                result = recipes.Any() ? (ActionResult)Ok(recipes) : NoContent();
            }

            return result;
        }

        /// <summary>
        /// Adds new Resource
        /// </summary>
        /// <param name="model">RawResourceModel with data for new Resource</param>
        /// <param name="apiVersion">Version of API</param>
        /// <returns> OK if model is valid and Resource was added successfully,
        /// BadRequest if model contains bad data
        /// 500 if exception has been thrown while saving new resource
        /// </returns>
        [HttpPost(Name = nameof(AddResource))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddResource([FromBody] RawResourceModel model, ApiVersion apiVersion)
        {
            var names = (await Task.FromResult(this._resourceRepository.GetRawResourcesNames()));

            if (names.Contains(model.Name))
            {
                ModelState.AddModelError("Name", $"Resource with name '{model.Name}' already exist");
            }

            var resourceType = await Task.FromResult(this._resourceRepository.GetRawResourceTypes().FirstOrDefault(x => x.ResourceTypeName == model.ResourceType));

            if (resourceType == null)
            {
                ModelState.AddModelError("ResourceType", $"Resource type with name '{model.ResourceType}' doesn't exist");
            }

            if (ModelState.IsValid)
            {
                var query = new AddResourceCommand(model);
                var result = await this._mediator.Send(query);

                if (result.resource != null)
                {
                    result.resource.Links = this.GetResourceUris(result.newId, apiVersion);

                    return CreatedAtRoute("GetResourceById", new { Id = result.newId, version = apiVersion.ToString() }, result.resource);
                }
            }

            return BadRequest(ModelState.GetFormattedDictionary());
        }

        /// <summary>
        /// Delete Resource with specified name
        /// </summary>
        /// <param name="id">id of resource to delete</param>
        /// <returns>
        /// OK if resource exist and was deleted successfully,
        /// NotFound if resource does not exist
        /// Status500 if exception has been thrown while deleting new resource
        /// </returns>
        [HttpDelete("{id:int:min(1)}", Name = nameof(DeleteItem))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteItem([FromRoute] int id)
        {
            ActionResult result = Ok();

            try
            {
                if (await Task.FromResult(this._resourceRepository.IsItemExists(id)))
                {
                    await Task.Run(() => this._resourceRepository.DeleteItem(id));
                }
                else
                {
                    result = NotFound();
                }
            }
            catch (Exception e)
            {
                result = StatusCode(500, e);
            }

            return result;
        }

        /// <summary>
        /// updates Resource with new values from RawResourceModel
        /// </summary>
        /// <param name="model">model with new data for resource</param>
        /// <param name="id">id</param>
        /// <returns> OK if model is valid and Resource was updated successfully,
        /// BadRequest if model contains bad data
        /// 500 if exception has been thrown while saving new resource
        /// </returns>
        [HttpPut("{id:int:min(1)}", Name = nameof(UpdateItem))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateItem([FromRoute] int id, [FromBody]RawResourceModel model)
        {
            ActionResult result = NoContent();

            var resourceType = await Task.FromResult(this._resourceRepository.GetRawResourceTypes().FirstOrDefault(x => x.ResourceTypeName == model.ResourceType));

            if (resourceType == null)
            {
                ModelState.AddModelError("Resource type", $"Resource type with name '{model.ResourceType}' doesn't exist");
            }
            
            var isResourceExist = await Task.FromResult(this._resourceRepository.IsItemExists(id));

            if (!isResourceExist)
            {
                ModelState.AddModelError("Resource name", $"Resource with name {id} was not found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var itemId = await Task.FromResult(this._resourceRepository.UpdateItem(id, new RawResource()
                    {
                        Name = model.Name,
                        Value = model.Value,
                        RawResourceTypeId = resourceType.Id
                    }));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Save", $"An error occured while saving resource: {e.Message} {e.InnerException?.Message}");
                }
            }
            else
            {
                result = BadRequest(ModelState.GetFormattedDictionary());
            }

            return result;
        }

        public override List<ResourceUri> GetResourceUris(int id, ApiVersion apiVersion)
        {
            return new List<ResourceUri>
            {
                new ResourceUri
                {
                    Method = "GET",
                    Uri = this._linkGenerator.GetResourceUriByAction(HttpContext, "api", nameof(GetResourceById), id, apiVersion.ToString())
                },

                new ResourceUri
                {
                    Method = "PUT",
                    Uri = this._linkGenerator.GetResourceUriByAction(HttpContext, "api", nameof(UpdateItem), id, apiVersion.ToString())
                },

                new ResourceUri
                {
                    Method = "DELETE",
                    Uri = this._linkGenerator.GetResourceUriByAction(HttpContext, "api", nameof(DeleteItem), id, apiVersion.ToString())
                }
            };
        }
    }
}