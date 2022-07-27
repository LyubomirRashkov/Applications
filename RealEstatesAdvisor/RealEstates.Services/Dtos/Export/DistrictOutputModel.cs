namespace RealEstates.Services.Dtos.Export
{
    public class DistrictOutputModel
    {
        public string Name { get; set; }

        public decimal AveragePricePerSquareMeter { get; set; }

        public int PropertiesCount { get; set; }
    }
}
