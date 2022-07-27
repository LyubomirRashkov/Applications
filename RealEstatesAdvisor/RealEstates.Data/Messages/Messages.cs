namespace RealEstates.Data.Messages
{
    public static class Messages
    {
        public const string Title = "Real Estate Advisor";

        public const string GreetingMessage = "Welcome to Real Estate Advisor!";

        public const string MakeAChoise1 = "Are you starting the application for the first time? (y/n)";

        public const string MakeAChoise2 = "Please press 'Y' for Yes or 'N' for No (press '0' for exit)";

        public const string MakeAChoise3 = "Please choose an option:";

        public const string FirstUse = "OK! It will take a few seconds to create the database and load all the information.";

        public const string PleaseWait = "Please, wait a while...";

        public const string Ready = "Everything is ready";

        public const string PressAnyKey = "Press any key to continue...";

        public const string Option1 = "   1. Property search by max price and min size";

        public const string Option2 = "   2. Property search by tag";

        public const string Option3 = "   3. See the most expensive districts";

        public const string Option4 = "   4. See the average price per square meter";

        public const string OptionExit = "   0. Exit";

        public const string Option01 = "   1. Скъп имот";

        public const string Option02 = "   2. Евтин имот";

        public const string Option03 = "   3. Ново строителство";

        public const string Option04 = "   4. Старо строителство";

        public const string Option05 = "   5. Голям имот";

        public const string Option06 = "   6. Малък имот";

        public const string Option07 = "   7. Първи етаж";

        public const string Option08 = "   8. Последен етаж";

        public const string InvalidInput = "Invalid input!";

        public const string EnterValues = "Please enter the required values (they must be integers):";

        public const string EnterMaxPrice = "   Max price: ";

        public const string EnterMinSize = "   Min size: ";

        public const string EnterNumberOfDistricts = "Please enter the number of top districts you want to see (it must be a positive integer):";

        public const string OneDistrict = "Top 1 most expensive district is:";

        public const string ManyDistricts = "Top {0} most expensive districts are:";

        public const string PropertyInfo = "District - {0}; Size - {1}m²; Year - {2}; Type - {3}; BuildingType - {4}; Price - {5}€; Tags - {6}";

        public const string DistrictInfo = "District - {0}; Average price per square meter - {1:F2}€/m²; Count of properties - {2}";

        public const string AveragePrice = "The average price per square meter is {0:F2}€/m²";
    }
}
