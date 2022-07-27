using RealEstates.Data;
using RealEstates.Services;
using RealEstates.Services.Dtos.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RealEstates.Importer
{
    public class Importer
    {
        public static void Main(string[] args)
        {
            var dbContext = new RealEstatesDbContext();

            IPropertiesService propertiesService = new PropertiesService(dbContext);

            string apartments = File.ReadAllText("Apartments.json");

            string houses = File.ReadAllText("Houses.json");

            ImportJsonFiles(propertiesService, apartments, houses);
        }

        private static void ImportJsonFiles(IPropertiesService propertiesService, params string[] strings)
        {
            string jsonInput = CombineStringsAsJson(strings);

            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyInputModel>>(jsonInput);

            propertiesService.AddProperties(properties);
        }

        private static string CombineStringsAsJson(string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Replace("[", "");

                strings[i] = strings[i].Replace("]","");

                if (i < strings.Length - 1)
                {
                    strings[i] = strings[i] + ",";
                }
            }

            string result = "[" + string.Join(Environment.NewLine, strings) + "]";

            return result;
        }
    }
}
