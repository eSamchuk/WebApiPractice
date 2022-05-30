using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Model;

namespace NoMansSkyRecipies.Controllers.v2
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/Extractors")]
    [ApiVersion("2.0")]
    public class ExtractorController : ControllerBase
    {
        private readonly IExctractorRepository _exctractorRepository;

        public ExtractorController(IExctractorRepository exctractorRepository)
        {
            _exctractorRepository = exctractorRepository;
        }

        [HttpGet()]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await Task.FromResult(this._exctractorRepository.GetAllItems());

            return result.Any() ? (ActionResult)Ok(result) : NotFound();
        }


        //[HttpPost()]
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        //public async Task<IActionResult> AddNewExtractor([FromBody] ExctractorModel model)
        //{
            
        //}
    }
}
