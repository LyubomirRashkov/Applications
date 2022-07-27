using AutoMapper;

namespace RealEstates.Services.AutoMapper
{
    public class MapperCreator
    {
        private static IMapper mapper;

        private MapperCreator()
        {
        }

        public static IMapper Mapper 
        {
            get
            {
                if (mapper == null)
                {
                    var config = new MapperConfiguration(cfg => cfg.AddProfile<RealEstatesProfile>());

                    mapper = config.CreateMapper();
                }

                return mapper;
            }
        }
    }
}
