namespace RealEstates.Services.Dtos.Export
{
    public class PropertyOutputModel
    {
        public string DistrictName { get; set; }

        public int Size { get; set; }

        public int Price { get; set; }

        public int? Year { get; set; }

        public string PropertyType { get; set; }

        public string BuildingType { get; set; }

        public string Tags { get; set; }
    }
}
