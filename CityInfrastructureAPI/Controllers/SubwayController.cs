using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CityInfrastructure.DAL.Interfaces;
using CityInfrastructure.DAL.Repositories.Subway;
using CityInfrastructure.Data;
using CityInfrastructure.Data.Model;
using CityInfrastructure.DTO.Model.Subway;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CityInfrastructureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubwayController : ControllerBase
    {
        private InfrastructureDbContext db;
        private ISubwayStationsRepository stationsRepo;
        private ISubwayLinesRepository linesRepo;


        public SubwayController(InfrastructureDbContext db, ISubwayStationsRepository stationsRepo, ISubwayLinesRepository linesRepo)
        {
            this.db = db;
            this.stationsRepo = stationsRepo;
            this.linesRepo = linesRepo;
        }

        [HttpGet("Stations")]
        public IEnumerable<SubwayStationDTO> GetStations()
        {
            return this.stationsRepo.GetFullList();
        }

        [HttpGet("Lines")]
        public IEnumerable<SubwayLineDTO> GetLines()
        {
                var t = this.linesRepo.GetFullList();
                return t;
        }

        [HttpGet("Intersections")]
        public IEnumerable<string> GetIntersections()
        {
            return new List<string>() { "Line1", "Line2" };

        }

    }
}
