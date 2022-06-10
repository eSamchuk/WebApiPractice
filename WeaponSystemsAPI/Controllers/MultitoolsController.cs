using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nms.Mappings;
using NmsRecipes.DAL.Interfaces;

namespace WeaponSystemsAPI.Controllers
{
    [Area("api")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MultitoolsController : ControllerBase
    {

        private readonly IResourceRepository resourceRepository;


        public MultitoolsController(IResourceRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMultitools()
        {
            var resource = await Task.FromResult(this.resourceRepository.GetItemById(1));

            if (resource != null)
            {
                return (ActionResult)Ok(resource.MapToDisplayedResource());
            }

            return NotFound();

            //return Ok("123");
        }

    }
}
