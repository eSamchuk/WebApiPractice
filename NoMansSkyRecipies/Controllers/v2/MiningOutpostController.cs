using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;
using NoMansSkyRecipies.Data.Entities.Energy;
using NoMansSkyRecipies.Data.Entities.Resources;
using NoMansSkyRecipies.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NoMansSkyRecipies.Controllers.v2
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/MiningOutposts")]
    [ApiVersion("2.0")]
    public class MiningOutpostController : ControllerBase
    {
        private readonly IMiningOutpostRepository _miningRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IHotspotClassesRepository _hotspotClassesRepository;

        public MiningOutpostController(
            IMiningOutpostRepository miningRepository,
            IResourceRepository resourceRepository,
            IHotspotClassesRepository hotspotClassesRepository)
        {
            this._miningRepository = miningRepository;
            this._resourceRepository = resourceRepository;
            this._hotspotClassesRepository = hotspotClassesRepository;
        }

        [HttpGet()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAllOutposts()
        {
            var result = await Task.FromResult(this._miningRepository.GetAllItems().Select(x => x.MapToDisplayed()));

            return result.Any() ? (ActionResult)Ok(result) : NotFound();
        }

        [HttpGet("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetOutpostById(int id)
        {
            var outpost = await Task.FromResult(this._miningRepository.GetItemById(id));

            if (outpost != null)
            {
                return Ok(outpost.MapToDisplayed());
            }

            return NotFound();
        }

        [HttpPost()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> AddNewOutpost([FromBody] OutpostModel outpost)
        {
            var minedResource = await Task.FromResult(this._resourceRepository.GetItemByName(outpost.MinedResource));

            if (minedResource == null)
            {
                ModelState.AddModelError("Resource", $"Resource with name {outpost.MinedResource} doesn't exists");
            }

            var resourceHotspotClass = await Task.FromResult(this._hotspotClassesRepository
                .GetItemsByCondition(x => x.Class == outpost.HotspotClass).FirstOrDefault());

            if (resourceHotspotClass == null)
            {
                ModelState.AddModelError("HotspotClass", $"{outpost.HotspotClass} is not valid Resource HotspotClass value");
            }

            HotspotClass energyHotspotClass = null;

            if (outpost.EnergySupply.Plant != null)
            {
                energyHotspotClass = await Task.FromResult(this._hotspotClassesRepository
                    .GetItemsByCondition(x => x.Class == outpost.EnergySupply.Plant.HotspotClass).FirstOrDefault());

                if (energyHotspotClass == null)
                {
                    ModelState.AddModelError("HotspotClass", $"{outpost.EnergySupply.Plant.HotspotClass} is not valid EMPlant HotspotClass value");
                }
            }
            
            if (ModelState.IsValid)
            {
                var newOutpost = new MiningOutpost
                {
                    HaveTeleport = outpost.HaveTeleport,
                    ResourceTypeId = minedResource.Id,
                    SupplyDepots = outpost.SupplyDepots,
                    HotspotClassId = resourceHotspotClass.Id,
                    Extractors = outpost.Exctractors.Select(x => new Extractor
                    {
                        ExtractionRate = x.ExtractionRate,
                    }).ToList(),
                    EnergySupply = new EnergySupply()
                    {
                        Batteries = outpost.EnergySupply.Batteries,
                        SolarPanels = outpost.EnergySupply.SolarPanels
                       
                    }
                };

                if (outpost.EnergySupply.Plant != null)
                {
                    newOutpost.EnergySupply.Plant = new ElectroMagneticPlant
                    {
                        HotspotClassId = energyHotspotClass.Id,
                        Generators = outpost.EnergySupply.Plant.Generators.Select(x => new ElectromagneticGenerator
                        {
                            Output = x.Output
                        }).ToList()
                    };
                }

                try
                {
                    var newId = await Task.FromResult(this._miningRepository.AddItem(newOutpost));

                    var result = await Task.FromResult(this._miningRepository.GetItemById(newId));

                    if (newId != 0)
                    {
                        return Created(new Uri($"{base.Request.GetEncodedUrl()}/{newId}"), result.MapToDisplayed());
                    }
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
