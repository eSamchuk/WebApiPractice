using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using CityInfrastructure.DAL.Interfaces;
using CityInfrastructure.Data;
using CityInfrastructure.Data.Model;
using CityInfrastructure.DTO.Model.Subway;
using Microsoft.EntityFrameworkCore;

namespace CityInfrastructure.DAL.Repositories.Subway
{
    public class SubwayLinesRepository : ISubwayLinesRepository
    {
        private Mapper mapper;
        private InfrastructureDbContext db;

        public SubwayLinesRepository(InfrastructureDbContext db)
        {
            this.db = db;
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<SubwayLine, SubwayLineDTO>().ForMember(d => d.StationsNames,
                    s => s.MapFrom(z => z.Stations.Select(y => y.Name)));
            });

            mapper = new Mapper(config);
        }


        public IEnumerable<SubwayLineDTO> GetFullList()
        {
            return mapper.Map<IEnumerable<SubwayLine>, IEnumerable<SubwayLineDTO>>(db.SubwayLines.Include(x => x.Stations));
        }

        public SubwayLineDTO GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubwayLineDTO> Find(Expression<Func<SubwayLineDTO, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public void AddItem(SubwayLineDTO item)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(SubwayLineDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
