using RealEstates.Services.Dtos.Export;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictOutputModel> GetTopMostExpensiveDistricts(int districtsCount);
    }
}
