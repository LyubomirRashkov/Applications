using RealEstates.Data;
using RealEstates.Data.Messages;
using RealEstates.Services;
using RealEstates.Services.Dtos.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RealEstates.ConsoleUI
{
    public class Engine
    {
        private readonly RealEstatesDbContext dbContext;
        private readonly IBaseService baseService;
        private readonly IPropertiesService propertiesService;
        private readonly ITagService tagService;
        private readonly IDistrictsService districtsService;

        public Engine()
        {
            this.dbContext = new RealEstatesDbContext();
            this.baseService = new BaseService();
            this.propertiesService = new PropertiesService(this.dbContext);
            this.tagService = new TagService(this.dbContext, this.propertiesService);
            this.districtsService = new DistrictsService(this.dbContext);
        }

        public void Run()
        {
            Console.WriteLine(Messages.GreetingMessage);

            Console.WriteLine(Messages.MakeAChoise1);

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            while (consoleKeyInfo.Key != ConsoleKey.N)
            {
                if (consoleKeyInfo.Key == ConsoleKey.Y)
                {
                    CreateAndLoad();

                    break;
                }
                else if (consoleKeyInfo.Key == ConsoleKey.D0)
                {
                    Environment.Exit(0);
                }

                Console.WriteLine();

                Console.WriteLine(Messages.MakeAChoise2);

                consoleKeyInfo = Console.ReadKey();
            }

            UseDatabase();
        }

        private void AveragePricePerSquareMeter()
        {
            decimal averagePrice = this.propertiesService.AveragePricePerSquareMeter();

            Console.WriteLine();

            Console.WriteLine(Messages.AveragePrice, averagePrice);

            Console.WriteLine();

            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }

        private void GetMostExpensiveDistricts()
        {
            while (true)
            {
                Console.WriteLine();

                Console.WriteLine(Messages.EnterNumberOfDistricts);

                int.TryParse(Console.ReadLine(), out int districtsCount);

                if (districtsCount > 0)
                {
                    Console.WriteLine();

                    if (districtsCount == 1)
                    {
                        Console.WriteLine(Messages.OneDistrict);
                    }
                    else
                    {
                        Console.WriteLine(Messages.ManyDistricts, districtsCount);
                    }

                    var districts = this.districtsService.GetTopMostExpensiveDistricts(districtsCount);

                    foreach (var district in districts)
                    {
                        Console.WriteLine();

                        Console.WriteLine(Messages.DistrictInfo, district.Name, district.AveragePricePerSquareMeter, district.PropertiesCount);
                    }

                    Console.WriteLine();

                    break;
                }
                else
                {
                    Console.WriteLine(Messages.InvalidInput);
                }
            }

            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }

        private void PropertySearchByTagName()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine(Messages.MakeAChoise3);

                Console.WriteLine(Messages.Option01);

                Console.WriteLine(Messages.Option02);

                Console.WriteLine(Messages.Option03);

                Console.WriteLine(Messages.Option04);

                Console.WriteLine(Messages.Option05);

                Console.WriteLine(Messages.Option06);

                Console.WriteLine(Messages.Option07);

                Console.WriteLine(Messages.Option08);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                bool isInputValid = ValidateInput(consoleKeyInfo.Key);

                if (isInputValid)
                {
                    var properties = this.propertiesService.GetPropertiesByTag(consoleKeyInfo.Key);

                    foreach (var property in properties)
                    {
                        string outputYear = property.Year == null ? "No info" : property.Year.ToString();

                        Console.WriteLine();

                        Console.WriteLine(Messages.PropertyInfo, property.DistrictName, property.Size, outputYear, property.PropertyType, property.BuildingType, property.Price, property.Tags);
                    }

                    Console.WriteLine();

                    break;
                }

                Console.WriteLine();

                Console.WriteLine(Messages.InvalidInput);

                Console.WriteLine();
            }

            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }

        private bool ValidateInput(ConsoleKey key)
        {
            foreach (var validKey in this.propertiesService.ValidKeys)
            {
                if (validKey == key)
                {
                    return true;
                }
            }

            return false;
        }

        private void PropertySearchByMaxPriceAndMinSize()
        {
            while (true)
            {
                Console.WriteLine();

                Console.WriteLine(Messages.EnterValues);

                Console.Write(Messages.EnterMaxPrice);

                int.TryParse(Console.ReadLine(), out int maxPrice);

                Console.Write(Messages.EnterMinSize);

                int.TryParse(Console.ReadLine(), out int minSize);

                if (maxPrice > 0 && minSize > 0)
                {
                    var properties = this.propertiesService.GetPropertiesWithMaxPriceAndMinSize(maxPrice, minSize);

                    foreach (var property in properties)
                    {
                        string outputYear = property.Year == null ? "No info" : property.Year.ToString();

                        Console.WriteLine();

                        Console.WriteLine(Messages.PropertyInfo, property.DistrictName, property.Size, outputYear, property.PropertyType, property.BuildingType, property.Price, property.Tags);
                    }

                    Console.WriteLine();

                    break;
                }

                Console.WriteLine(Messages.InvalidInput);
            }

            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }

        private void ProcessCommand(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    PropertySearchByMaxPriceAndMinSize();
                    break;

                case ConsoleKey.D2:
                    PropertySearchByTagName();
                    break;

                case ConsoleKey.D3:
                    GetMostExpensiveDistricts();
                    break;

                case ConsoleKey.D4:
                    AveragePricePerSquareMeter();
                    break;

                case ConsoleKey.D0:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine(Messages.InvalidInput);
                    break;
            }
        }

        private void UseDatabase()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(Messages.MakeAChoise3);

                Console.WriteLine(Messages.Option1);

                Console.WriteLine(Messages.Option2);

                Console.WriteLine(Messages.Option3);

                Console.WriteLine(Messages.Option4);

                Console.WriteLine(Messages.OptionExit);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                ProcessCommand(consoleKeyInfo.Key);
            }
        }

        private void AddTagsToProperties()
        {
            this.tagService.AddTagsToPropertiesRelations();
        }

        private void AddTags()
        {
            string jsonInput = File.ReadAllText("../../../../RealEstates.Data/Datasets/Tags.json");

            var tags = JsonSerializer.Deserialize<IEnumerable<TagInputModel>>(jsonInput);

            this.tagService.AddTags(tags);
        }

        private void AddProperties()
        {
            string apartments = File.ReadAllText("../../../../RealEstates.Data/Datasets/Apartments.json");

            string houses = File.ReadAllText("../../../../RealEstates.Data/Datasets/Houses.json");

            ImportJsonFiles(this.propertiesService, apartments, houses);
        }

        private static void ImportJsonFiles(IPropertiesService propertiesService, params string[] strings)
        {
            string jsonInput = CombineSeveralJsonsAsOneJson(strings);

            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyInputModel>>(jsonInput);

            propertiesService.AddProperties(properties);
        }

        private static string CombineSeveralJsonsAsOneJson(string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Replace("[", "");

                strings[i] = strings[i].Replace("]", "");

                if (i < strings.Length - 1)
                {
                    strings[i] = strings[i] + ",";
                }
            }

            string result = "[" + string.Join(Environment.NewLine, strings) + "]";

            return result;
        }

        private void InitialCreate()
        {
            this.baseService.CreateDatabase(this.dbContext);
        }

        private void CreateAndLoad()
        {
            Console.WriteLine();

            Console.WriteLine(Messages.FirstUse);

            Console.WriteLine(Messages.PleaseWait);

            InitialCreate();

            AddProperties();

            AddTags();

            AddTagsToProperties();

            Console.WriteLine(Messages.Ready);

            Console.WriteLine(Messages.PressAnyKey);

            Console.ReadKey();
        }
    }
}
