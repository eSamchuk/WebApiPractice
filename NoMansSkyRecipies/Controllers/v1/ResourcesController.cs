using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using Nms.StaticData;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities;
using NoMansSkyRecipies.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using NoMansSkyRecipies.CQRS.Queries;

namespace NoMansSkyRecipies.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ResourcesController : ControllerBase
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

        //private IMemoryCache _cache;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="recipeRepository">repository for Recipes</param>
        /// <param name="resourceRepository">repository for Resources</param>
        /// <param name="mediator">mediator</param>
        //IMemoryCache cache
        public ResourcesController(IResourceRepository resourceRepository,
            IRecipeRepository recipeRepository,
            IMediator mediator
            )
        {
            this._resourceRepository = resourceRepository;
            this._recipeRepository = recipeRepository;
            this._mediator = mediator;
            //this._cache = cache;
        }

        /// <summary>
        /// Get list of all existing Resources
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetResources()
        {
            var resources = (await Task.FromResult(this._resourceRepository.GetAllItems()))
                .Select(x => new DisplayedResource()
                {
                    ResourceName = x.Name,
                    ResourceTypeName = x.RawResourceType.ResourceTypeName,
                    Value = x.Value
                });

            return resources.Any() ? (ActionResult)Ok(resources) : NoContent();
        }

        /// <summary>
        /// Get list of all existing Resources
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet("All")]
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
        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetResourceById(int id)
        {
            RawResource resource = await Task.FromResult(this._resourceRepository.GetItemById(id));
            return resource != null ? (ActionResult)Ok(resource.MapToDisplayedResource()) : NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Ok if result, otherwise - NoContent</returns>
        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetResourceById_new(int id)
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
        public async Task<ActionResult> GetSimplifiedResources()
        {
            var resources = (await Task.FromResult(this._resourceRepository.GetAllItems())).ToList();
            return resources.Any() ? (ActionResult)Ok(resources.Select(x => x.Name)) : NoContent();
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

            if (resourceCategory == SimplifiedResourceCategories.Gases)
            {
                predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.Gas;
            }
            else if (resourceCategory == SimplifiedResourceCategories.Plants)
            {
                predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.HarvestedPlant;
            }
            else if (resourceCategory == SimplifiedResourceCategories.Minerals)
            {
                predicate = x => x.RawResourceType.ResourceTypeName == ResourceTypesNames.MinedMineral;
            }
            else
            {
                ModelState.AddModelError("Category", "Unknown resource category");
            }

            if (ModelState.IsValid)
            {
                var resources = await Task.FromResult(this._resourceRepository.GetItemsByCondition(predicate).ToList());

                if (resources.Any())
                {
                    result = Ok(resources.Select(x => new DisplayedResource
                    {
                        ResourceName = x.Name,
                        Value = x.Value
                    }));
                }
                else
                {
                    result = NoContent();
                }
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
        [HttpGet("{resourceId:int}/UsedIn")]
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
        /// <returns> OK if model is valid and Resource was added successfully,
        /// BadRequest if model contains bad data
        /// 500 if exception has been thrown while saving new resource
        /// </returns>
        [HttpPost()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddResource([FromBody] RawResourceModel model)
        {
            ActionResult result = Ok();

            var resources = await Task.FromResult(this._resourceRepository.GetAllItems());
            var names = resources.Select(x => x.Name);
            

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
                try
                {
                    await Task.FromResult(this._resourceRepository.AddItem(new RawResource()
                    {
                        Name = model.Name,
                        Value = model.Value,
                        RawResourceTypeId = resourceType.Id
                    }));
                }
                catch (Exception e)
                {
                    result = StatusCode(500); 
                    ModelState.AddModelError("Save", $"An error occured while saving resource: {e.Message} {e.InnerException?.Message}");
                }
            }

            if (!ModelState.IsValid)
            {
                result = BadRequest(ModelState.GetFormattedDictionary());
            }

            return result;
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
        [HttpDelete("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteItem([FromRoute]int id)
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
        [HttpPut("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateItem([FromRoute]int id, [FromBody] RawResourceModel model)
        {
            ActionResult result = Ok();

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
                    await Task.FromResult(this._resourceRepository.UpdateItem(id, new RawResource()
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
    }
}
