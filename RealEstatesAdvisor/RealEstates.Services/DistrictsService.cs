using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services.AutoMapper;
using RealEstates.Services.Dtos.Export;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictsService
    {
        private readonly RealEstatesDbContext dbContext;

        public DistrictsService(RealEstatesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<DistrictOutputModel> GetTopMostExpensiveDistricts(int districtsCount)
        {
            var districtDtos = this.dbContext.Districts
                .AsNoTracking()
                .Where(d => d.Properties.Where(p => p.Price.HasValue).Count() >= 5)
                .OrderByDescending(d => d.Properties.Average(p => p.Price / (decimal)p.Size))
                .Take(districtsCount)
                .ProjectTo<DistrictOutputModel>(MapperCreator.Mapper.ConfigurationProvider)
                .ToList();

            return districtDtos;
        }
    }
}
