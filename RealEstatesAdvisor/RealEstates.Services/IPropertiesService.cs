using RealEstates.Services.Dtos.Export;
using RealEstates.Services.Dtos.Import;
using System;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public interface IPropertiesService
    {
        IReadOnlyCollection<ConsoleKey> ValidKeys { get; }

        void AddProperties(IEnumerable<PropertyInputModel> propertyInputModels);

        IEnumerable<PropertyOutputModel> GetPropertiesWithMaxPriceAndMinSize(int maxPrice, int minSize);

        IEnumerable<PropertyOutputModel> GetPropertiesByTag(ConsoleKey consoleKey);

        decimal AveragePricePerSquareMeter();

        decimal AveragePricePerSquareMeter(string districtName);
    }
}
