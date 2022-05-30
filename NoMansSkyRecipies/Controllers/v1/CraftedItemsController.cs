using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities;
using NoMansSkyRecipies.Helpers;
using Serilog;
using Serilog.Events;

namespace NoMansSkyRecipies.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiVersion("1.0")]
    public class CraftedItemsController : ControllerBase
    {
        /// <summary>
        /// repository for Recipes
        /// </summary>
        private readonly IRecipeRepository _recipesRepository;

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
        public CraftedItemsController(ICraftedItemsRepository craftedItemsRepository, IRecipeRepository recipeRepository, IResourceRepository resourceRepository)
        {
            this._craftedItemsRepository = craftedItemsRepository;
            this._recipesRepository = recipeRepository;
            this._resourceRepository = resourceRepository;
        }

        /// <summary>
        /// Get list of all existing CraftableItems
        /// </summary>
        /// <returns>Ok if resulting collection is not empty, otherwise - NoContent</returns>
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCraftableItems()
        {
            var result = await Task.FromResult(this._craftedItemsRepository.GetAllItems().ToList());

            return result.Any() ? (ActionResult)Ok(result.Select(x => new DisplayedCraftableItem()
            {
                ItemName = x.Name,
                Value = x.Value
            })) : NotFound();
        }

        /// <summary>
        /// Gets the list of low-tier resources needed for crafting SINGLE CraftableItem
        /// </summary>
        /// <param name="itemName">name of required item</param>
        /// <param name="count">number of required items</param>
        /// <returns>Ok if requested item exists, otherwise - NoContent</returns>
        [HttpGet("{itemName}/FullRecipe"), AllowAnonymous]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCraftableItemWithFullRecipie(string itemName, int? count = null)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return BadRequest("Item name is required");
            }

            if (count.HasValue && count < 1)
            {
                return BadRequest("Invalid count value");
            }

            var result = await Task.FromResult(this._craftedItemsRepository.GetCraftableItemWithFullRecipie(itemName, count ?? 1));

            if (result != null)
            {
                return Ok(result);
            }

            Log.Information(
                "[{time}] [{level}] GetCraftableItemWithFullRecipie => Item with name {itemName} doesn't exist", new object[] { DateTime
                    .Now.ToString("dd.MM.yyyy HH:mm:ss"), LogEventLevel.Information, itemName});

            return NotFound();
        }

        /// <summary>
        /// returns CraftableItem with list of low-tier resources needed for crafting multiplied for requested number of items
        /// </summary>
        /// <param name="itemName">name of required item</param>
        /// <param name="count">number of required items</param>
        /// <returns>Ok if requested item exists, otherwise - NoContent</returns>
        [HttpGet("{itemName}/FullRecipie/{count}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCraftableItemWithMutipliedRecipie(string itemName, int count)
        {
            return await this.GetCraftableItemWithFullRecipie(itemName, count);
        }

        /// <summary>
        /// Delete CraftableItem with specified Name
        /// </summary>
        /// <param name="id">id of the CraftableItem to delete</param>
        /// <returns>Ok if item was deleted successfully,
        /// NoContent if item was not found,
        /// BadRequest if ModelState is invalid or exception has been thrown while deleting the item</returns>
        [HttpDelete("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult> DeleteCraftableItem(int id)
        {
            ActionResult result = Ok();

            if (!this._craftedItemsRepository.IsItemExists(id))
            {
                ModelState.AddModelError("itemName", $"Item with id {id} doesn't exist");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => this._craftedItemsRepository.DeleteItem(id));
                }
                catch (Exception e)
                {
                    result = BadRequest(e);
                }
            }
            else
            {
                result = BadRequest(ModelState.GetFormattedDictionary());
            }

            return result;
        }

        /// <summary>
        /// Adds new CraftableItem 
        /// </summary>
        /// <param name="model">CraftableItemModel with data for new CraftableItem to add</param>
        /// <returns>Ok if item was added successfully,
        /// BadRequest if ModelState is invalid or exception has been thrown while adding the item
        /// </returns>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddNewCraftableItem(CraftableItemModel model)
        {
            var item = await Task.FromResult(this._craftedItemsRepository.GetItemByName(model.Name));

            if (item != null)
            {
                ModelState.AddModelError("itemName", $"Item with name {model.Name} already exist");
            }

            if (ModelState.IsValid)
            {
                var craftableItem = new CraftableItem()
                {
                    Name = model.Name,
                    Value = model.Value,
                };

                try
                {
                    await Task.FromResult(this._craftedItemsRepository.AddItem(craftableItem));
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }

            return BadRequest(ModelState.GetFormattedDictionary());
        }

        /// <summary>
        /// Updates new CraftableItem 
        /// </summary>
        /// <param name="model">CraftableItemModel with data for new CraftableItem to update</param>
        /// <param name="id">Id of CraftableItem to update</param>
        /// <returns>Ok if item was added successfully,
        /// BadRequest if ModelState is invalid or exception has been thrown while adding the item
        /// </returns>
        [HttpPut("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdatedCraftableItem(int id, [FromBody] CraftableItemModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError("CraftableItemModel", "CraftableItemModel was not provided");
            }

            var item = await Task.FromResult(this._craftedItemsRepository.GetItemById(id));

            if (item == null)
            {
                ModelState.AddModelError("Item", $"Item with Id {id} doesn't exist");
                return NotFound(ModelState.GetFormattedDictionary());
            }

            if (ModelState.IsValid)
            {
                var craftableItem = new CraftableItem()
                {
                    Name = model.Name,
                    Value = model.Value,
                };

                try
                {
                    await Task.FromResult(this._craftedItemsRepository.UpdateItem(id, craftableItem));
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }

            return BadRequest(ModelState.GetFormattedDictionary());
        }
    }
}
