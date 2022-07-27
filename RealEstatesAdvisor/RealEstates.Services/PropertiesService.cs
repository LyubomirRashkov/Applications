using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.AutoMapper;
using RealEstates.Services.Dtos.Export;
using RealEstates.Services.Dtos.Import;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly RealEstatesDbContext dbContext;

        private readonly IDictionary<ConsoleKey, string> tagNamesByKeys;

        public PropertiesService(RealEstatesDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.tagNamesByKeys = new Dictionary<ConsoleKey, string>
            {
                { ConsoleKey.D1, "скъп имот" },

                { ConsoleKey.D2, "евтин имот"},

                { ConsoleKey.D3, "ново строителство" },

                { ConsoleKey.D4, "старо строителство" },

                { ConsoleKey.D5, "голям имот" },

                { ConsoleKey.D6, "малък имот" },

                { ConsoleKey.D7, "първи етаж"},

                { ConsoleKey.D8, "последен етаж" }
            };
        }

        public IReadOnlyCollection<ConsoleKey> ValidKeys => this.tagNamesByKeys.Keys.ToList();

        public void AddProperties(IEnumerable<PropertyInputModel> propertyInputModels)
        {
            List<District> districts = this.dbContext.Districts.ToList();

            List<PropertyType> propertyTypes = this.dbContext.PropertyTypes.ToList();

            List<BuildingType> buildingTypes = this.dbContext.BuildingTypes.ToList();

            List<Property> properties = new List<Property>();

            foreach (var propertyInputModel in propertyInputModels)
            {
                var district = districts.FirstOrDefault(d => d.Name == propertyInputModel.District);

                if (district == null)
                {
                    district = new District
                    {
                        Name = propertyInputModel.District
                    };

                    districts.Add(district);
                }

                var propertyType = propertyTypes.FirstOrDefault(pt => pt.Name == propertyInputModel.Type);

                if (propertyType == null)
                {
                    propertyType = new PropertyType
                    {
                        Name = propertyInputModel.Type
                    };

                    propertyTypes.Add(propertyType);
                }

                var buildingType = buildingTypes.FirstOrDefault(bt => bt.Name == propertyInputModel.BuildingType);

                if (buildingType == null)
                {
                    buildingType = new BuildingType
                    {
                        Name = propertyInputModel.BuildingType
                    };

                    buildingTypes.Add(buildingType);
                }

                var property = new Property
                {
                    Size = propertyInputModel.Size,
                    YardSize = propertyInputModel.YardSize <= 0 ? null : propertyInputModel.YardSize,
                    Floor = (propertyInputModel.Floor <= 0 || propertyInputModel.Floor > propertyInputModel.TotalFloors)
                            ? null
                            : propertyInputModel.Floor,
                    TotalFloors = (propertyInputModel.TotalFloors <= 0 || propertyInputModel.TotalFloors < propertyInputModel.Floor)
                                  ? null
                                  : propertyInputModel.TotalFloors,
                    Year = propertyInputModel.Year <= 1800 ? null : propertyInputModel.Year,
                    Price = propertyInputModel.Price <= 0 ? null : propertyInputModel.Price,
                    District = district,
                    Type = propertyType,
                    BuildingType = buildingType
                };

                properties.Add(property);
            }

            this.dbContext.Districts.AddRange(districts);

            this.dbContext.PropertyTypes.AddRange(propertyTypes);

            this.dbContext.BuildingTypes.AddRange(buildingTypes);

            this.dbContext.Properties.AddRange(properties);

            this.dbContext.SaveChanges();
        }

        public IEnumerable<PropertyOutputModel> GetPropertiesWithMaxPriceAndMinSize(int maxPrice, int minSize)
        {
            var propertyDtos = this.dbContext.Properties
                .AsNoTracking()
                .Where(p => p.Price <= maxPrice && p.Size >= minSize)
                .ProjectTo<PropertyOutputModel>(MapperCreator.Mapper.ConfigurationProvider)
                .OrderBy(p => p.Price)
                .ThenByDescending(p => p.Size)
                .ToList();

            return propertyDtos;
        }

        public IEnumerable<PropertyOutputModel> GetPropertiesByTag(ConsoleKey consoleKey)
        {
            var propertyDtos = this.dbContext.Properties
                .AsNoTracking()
                .Where(p => p.PropertiesTags.Any(pt => pt.Tag.Name == this.tagNamesByKeys[consoleKey]))
                .ProjectTo<PropertyOutputModel>(MapperCreator.Mapper.ConfigurationProvider)
                .OrderBy(p => p.Price)
                .ThenByDescending(p => p.Size)
                .ToList();

            return propertyDtos;
        }

        public decimal AveragePricePerSquareMeter()
        {
            decimal result = this.dbContext.Properties
                .AsNoTracking()
                .Where(p => p.Price.HasValue)
                .Average(p => p.Price / (decimal)p.Size) ?? 0;

            return result;
        }

        public decimal AveragePricePerSquareMeter(string districtName)
        {
            decimal result = this.dbContext.Properties
                .AsNoTracking()
                .Where(p => p.Price.HasValue && p.District.Name == districtName)
                .Average(p => p.Price / (decimal)p.Size) ?? 0;

            return result;
        }
    }
}
