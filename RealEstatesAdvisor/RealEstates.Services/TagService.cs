using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.AutoMapper;
using RealEstates.Services.Dtos.Import;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class TagService : ITagService
    {
        private readonly RealEstatesDbContext dbContext;

        private readonly IPropertiesService propertiesService;

        public TagService(RealEstatesDbContext dbContext, IPropertiesService propertiesService)
        {
            this.dbContext = dbContext;
            this.propertiesService = propertiesService;
        }

        public void AddTags(IEnumerable<TagInputModel> tagInputModels)
        {
            var tags = MapperCreator.Mapper.Map<IEnumerable<Tag>>(tagInputModels);

            this.dbContext.Tags.AddRange(tags);

            this.dbContext.SaveChanges();
        }

        public void AddTagsToPropertiesRelations()
        {
            var expensivePropertyTagId = GetTagId("скъп имот");

            var cheapPropertyTagId = GetTagId("евтин имот");

            var newPropertyTagId = GetTagId("ново строителство");

            var oldPropertyTagId = GetTagId("старо строителство");

            var largePropertyTagId = GetTagId("голям имот");

            var smallPropertyTagId = GetTagId("малък имот");

            var firstFloorPropertyTagId = GetTagId("първи етаж");

            var lastFloorPropertyTagId = GetTagId("последен етаж");

            IDictionary<string, decimal> averagePriceByDistrictName = new Dictionary<string, decimal>();

            var districts = this.dbContext.Districts
                .Include(d => d.Properties)
                .ToList();

            foreach (var district in districts)
            {
                var averagePricePerSquareMeter = this.propertiesService.AveragePricePerSquareMeter(district.Name);

                if (!averagePriceByDistrictName.ContainsKey(district.Name))
                {
                    averagePriceByDistrictName.Add(district.Name, averagePricePerSquareMeter);
                }
                else
                {
                    averagePriceByDistrictName[district.Name] = averagePricePerSquareMeter;
                }
            }

            var properties = this.dbContext.Properties
                .Include(p => p.PropertiesTags)
                .ToList();

            foreach (var property in properties)
            {
                if (property.Price.HasValue && property.Price >= averagePriceByDistrictName[property.District.Name])
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = expensivePropertyTagId
                        });
                }
                else if (property.Price.HasValue && property.Price < averagePriceByDistrictName[property.District.Name])
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = cheapPropertyTagId
                        });
                }

                if (property.Year.HasValue && property.Year >= 2000)
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = newPropertyTagId
                        });
                }
                else if (property.Year.HasValue && property.Year < 2000)
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = oldPropertyTagId
                        });
                }

                if (property.Size >= 100)
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = largePropertyTagId
                        });
                }
                else
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = smallPropertyTagId
                        });
                }

                if (property.Floor.HasValue && property.Floor == 1)
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = firstFloorPropertyTagId
                        });
                }

                if (property.Floor.HasValue && property.TotalFloors.HasValue && property.Floor == property.TotalFloors)
                {
                    property.PropertiesTags
                        .Add(new PropertyTag()
                        {
                            TagId = lastFloorPropertyTagId
                        });
                }
            }

            this.dbContext.SaveChanges();
        }

        private int GetTagId(string tagName) => this.dbContext.Tags.AsNoTracking().FirstOrDefault(t => t.Name == tagName).Id;
    }
}