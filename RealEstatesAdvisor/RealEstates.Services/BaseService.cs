using Microsoft.EntityFrameworkCore;
using RealEstates.Data;

namespace RealEstates.Services
{
    public class BaseService : IBaseService
    {
        public void CreateDatabase(RealEstatesDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
