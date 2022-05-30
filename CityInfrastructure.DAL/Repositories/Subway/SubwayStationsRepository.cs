using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using CityInfrastructure.DAL.Interfaces;
using CityInfrastructure.Data;
using CityInfrastructure.Data.Model;
using CityInfrastructure.DTO.Model.Subway;
using Microsoft.EntityFrameworkCore;

namespace CityInfrastructure.DAL.Repositories.Subway
{
    public class SubwayStationsRepository : ISubwayStationsRepository
    {
        private Mapper mapper;
        private InfrastructureDbContext db;

        public SubwayStationsRepository(InfrastructureDbContext db)
        {
            this.db = db;
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<SubwayStation, SubwayStationDTO>()
                    .ForMember(d => d.IntersectingStation, s => s.MapFrom(z => z.IntersectingStation.Name))
                    .ForMember(d => d.Line, s => s.MapFrom(z => z.Line.Name))
                    .ForMember(d => d.CurrentStatus, s => s.MapFrom(z => z.Statuses.LastOrDefault().StatusName)); 
            });
            
            mapper = new Mapper(config);
        }

        public void AddItem(SubwayStationDTO item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubwayStationDTO> Find(Expression<Func<SubwayStationDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubwayStationDTO> GetFullList()
        {
            var t = mapper.Map<IEnumerable<SubwayStation>, IEnumerable<SubwayStationDTO>>(
                db.SubwayStations
                    .Include(x => x.Statuses)
                    .Include(x => x.Line)
                    .Include(x => x.IntersectingStation).ToList()
                );
            return t;
        }

        public SubwayStationDTO GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(SubwayStationDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
