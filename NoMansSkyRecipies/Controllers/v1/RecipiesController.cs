using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities;
using NoMansSkyRecipies.Helpers;

namespace NoMansSkyRecipies.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RecipesController : ControllerBase
    {
        /// <summary>
        /// repository for Recipes
        /// </summary>
        private readonly IRecipeRepository _recipeRepository;

        /// <summary>
        /// repository for Resources
        /// </summary>
        private readonly IResourceRepository _resourceRepository;

        /// <summary>
        /// repository for CraftedItems
        /// </summary>
        private readonly ICraftedItemsRepository _craftedItemsRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="recipeRepository">repository for Recipes</param>
        /// <param name="resourceRepository">repository for Resources</param>
        /// <param name="craftedItemsRepository">repository for CraftedItems</param>
        public RecipesController(IRecipeRepository recipeRepository, IResourceRepository resourceRepository, ICraftedItemsRepository craftedItemsRepository)
        {
            this._recipeRepository = recipeRepository;
            this._resourceRepository = resourceRepository;
            this._craftedItemsRepository = craftedItemsRepository;
        }

        /// <summary>
        /// Gets all existing Recipes
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetAllRecipes()
        {
            var result = await Task.FromResult(this._recipeRepository.GetAllItems().Select(DisplayedRecipeMapper.MapToDisplayedRecipie));

            return result.Any() ? (ActionResult)Ok(result) : NoContent();
        }

        /// <summary>
        ///  Gets Recipe for single CraftableItem with specified name
        /// </summary>
        /// <param name="itemName">name of CraftableItem to get recipe for</param>
        /// <returns>Ok if CraftableItem was found, otherwise - NoContent</returns>
        [HttpGet("{itemName}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetSingleItemRecipe(string itemName)
        {
            var recipe =  await Task.FromResult(this._recipeRepository.GetItemByName(itemName));

            return recipe != null ? (ActionResult)Ok(recipe.MapToDisplayedRecipie()) : NotFound();
        }

        /// <summary>
        /// Adds new Recipie to specified CraftableItem
        /// </summary>
        /// <param name="model">RecipeAddModel containing data for new Recipie</param>
        /// <returns>Created if Recipe was added successfully, otherwise - BadRequest with errors</returns>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddNewRecipe([FromBody] RecipeModel model)
        {
            var itemExist = await Task.FromResult(this._craftedItemsRepository.IsItemExists(model.TargetItemId));

            ////перевірити існування предмета, для якого додається рецепт
            if (!itemExist)
            {
                ModelState.AddModelError("itemName", $"Can't add recipe for item with id {model.TargetItemId} - item not found");
            }

            //// перевірити наявність списку ресурсів
            var neededResources = model.NeededResources;

            if (neededResources == null || neededResources.Count < 2)
            {
                ModelState.AddModelError("NeededResources", $"Can't add recipe for item with id {model.TargetItemId} - resources list is empty or contains only one resource");
            }

            //// невід'ємні значення їхньої кількості
            if (neededResources != null && neededResources.Any(x => x.neededAmount <= 0))
            {
                ModelState.AddModelError("NeededResources",
                    $"Can't add recipe for item item with id {model.TargetItemId} - resources list contains at least one resource with needed amount equal or less than zero");
            }
            //// наявність в БД кожного ресурсу
            if (neededResources != null)
            {
                foreach (var resource in neededResources)
                {
                    var foundResource = await Task.FromResult(this._resourceRepository.IsItemExists(resource.resourceName));
                    var foundCraftableItem = await Task.FromResult(this._craftedItemsRepository.IsItemExists(resource.resourceName));

                    if (!foundResource && !foundCraftableItem)
                    {
                        ModelState.AddModelError("NeededResources",
                            $"Can't add recipe for item item with id {model.TargetItemId} - resource {resource.resourceName} does not exist");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var item = await Task.FromResult(this._craftedItemsRepository.GetItemById(model.TargetItemId));
                Recipie newRecipe = new Recipie() { NeededResources = new List<NeededResource>() };

                if (item != null)
                {
                    ////витягти ІД CraftableItem
                    newRecipe.ResultingItemId = item.Id;

                    ////збираємо ресурси
                    foreach (var resource in neededResources)
                    {
                        var dbRes = await Task.FromResult(this._resourceRepository.GetItemByName(resource.resourceName));
                        var dbItem = await Task.FromResult(this._craftedItemsRepository.GetItemByName(resource.resourceName));

                        if (dbRes != null | dbItem != null)
                        {
                            newRecipe.NeededResources.Add(
                                new NeededResource
                                {
                                    CraftableItemId = dbItem?.Id,
                                    RawResourceId = dbRes?.Id,
                                    NeededAmount = resource.neededAmount,
                                    Recipie = newRecipe
                                });
                        }
                    }

                    var newId = await Task.FromResult(this._recipeRepository.AddItem(newRecipe));

                    if (newId != 0)
                    {
                        var t = new Uri($"{Request.GetEncodedUrl()}/{newRecipe.Id}").ToString();
                        return Created(new Uri($"{Request.GetEncodedUrl()}/{newRecipe.Id}"), newRecipe.MapToDisplayedRecipie());
                    }

                    return BadRequest();
                }
            }

            return BadRequest(ModelState.GetFormattedDictionary());
        }

        /// <summary>
        /// updates Recipe with new values from RecipeUpdateModel
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">model with new data for resource</param>
        /// <returns> OK if model is valid and Resource was updated successfully,
        /// BadRequest if model contains bad data
        /// 500 if exception has been thrown while saving new resource
        /// </returns>
        [HttpPut("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateItem([FromRoute] int id, [FromBody] RecipeModel model)
        {
            ActionResult result = Ok();

            var recipe = await Task.FromResult(this._recipeRepository.GetItemById(id));

            if (recipe == null)
            {
                ModelState.AddModelError("Name", $"Recipe with id '{id}' was not found");
            }

            if (model.NeededResources.Count < 2)
            {
                ModelState.AddModelError("Resources", "Not enough resources were provided for the recipe");
            }

            if (ModelState.IsValid)
            {
                var newRecipe = new Recipie()
                {
                    Id = id,
                    ResultingItemId = recipe.ResultingItemId,
                    NeededResources = new List<NeededResource>()
                };

                foreach (var resource in model.NeededResources)
                {
                    var dbRes = await Task.FromResult(this._resourceRepository.GetItemByName(resource.resourceName));
                    var dbItem = await Task.FromResult(this._craftedItemsRepository.GetItemByName(resource.resourceName));

                    if (dbRes != null | dbItem != null)
                    {
                        newRecipe.NeededResources.Add(
                            new NeededResource
                            {
                                CraftableItemId = dbItem?.Id,
                                RawResourceId = dbRes?.Id,
                                NeededAmount = resource.neededAmount,
                                Recipie = newRecipe
                            });
                    }
                    else
                    {
                        ModelState.AddModelError("Resources", $"Resource with name {resource.resourceName} does not exist");
                    }
                }

                if (newRecipe.NeededResources.Count >= 2)
                {
                    try
                    {
                        await Task.FromResult(this._recipeRepository.UpdateItem(id, newRecipe));
                    }
                    catch (Exception e)
                    {
                        result = StatusCode(500, e);
                    }
                }
                else
                {
                    ModelState.AddModelError("Resources", "Not enough valid resources were provided for the recipe");
                }
            }

            if (!ModelState.IsValid)
            {
                result = BadRequest(ModelState.GetFormattedDictionary());
            }

            return result;
        }

        /// <summary>
        /// deletes Recipe with specified Id
        /// </summary>
        /// <param name="id">id of the recipe to delete</param>
        /// <returns>
        /// Ok if Recipe exist and was deleted successfully
        /// NotFound if recipe does not exist
        /// BadRequest if exception has been thrown during recipe deleting
        /// </returns>
        [HttpDelete("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteItem([FromRoute] int id)
        {
            ActionResult result = Ok();

            if (await Task.FromResult(this._recipeRepository.IsItemExists(id)))
            {
                try
                {
                    await Task.Run(() => this._recipeRepository.DeleteItem(id));
                }
                catch (Exception e)
                {
                    result = BadRequest(e);
                }
            }
            else
            {
                result = NotFound();
            }

            return result;
        }
    }
}
