using RealEstates.Data;

namespace RealEstates.Services
{
    public interface IBaseService
    {
        void CreateDatabase(RealEstatesDbContext dbContext);
    }
}
