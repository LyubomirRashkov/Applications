using AutoMapper;
using RealEstates.Models;
using RealEstates.Services.Dtos.Export;
using RealEstates.Services.Dtos.Import;
using System.Linq;

namespace RealEstates.Services.AutoMapper
{
    public class RealEstatesProfile : Profile
    {
        public RealEstatesProfile()
        {
            this.CreateMap<TagInputModel, Tag>();

            this.CreateMap<Property, PropertyOutputModel>()
                .ForMember(x => x.Price, y => y.MapFrom(s => s.Price == null ? 0 : s.Price))
                .ForMember(x => x.PropertyType, y => y.MapFrom(s => s.Type.Name))
                .ForMember(x => x.BuildingType, y => y.MapFrom(s => s.BuildingType.Name))
                .ForMember(x => x.Tags, y => y.MapFrom(s => string.Join(", ", (s.PropertiesTags
                                                                                .Select(pt => pt.Tag)
                                                                                .OrderBy(t => t.Importance)
                                                                                .Select(t => t.Name)))));

            this.CreateMap<District, DistrictOutputModel>()
                .ForMember(x => x.AveragePricePerSquareMeter, y => y.MapFrom(s => s.Properties
                                                                                    .Average(p => p.Price / (decimal)p.Size) ?? 0))
                .ForMember(x => x.PropertiesCount, y => y.MapFrom(s => s.Properties.Count));
        }
    }
}
