using AutoMapper;
using RealEstates.Models;
using RealEstates.Services.Dtos.Export;

namespace RealEstates.AutoMapper
{
    public class RealEstatesProfile : Profile
    {
        public RealEstatesProfile()
        {
            this.CreateMap<Property, PropertyOutputModel>();
        }
    }
}
