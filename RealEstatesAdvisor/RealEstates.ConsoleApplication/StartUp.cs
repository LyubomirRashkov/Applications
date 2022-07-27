using RealEstates.ConsoleUI;
using RealEstates.Data.Messages;
using System;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Console.Title = Messages.Title;

            Console.OutputEncoding = Encoding.Unicode;

            Engine engine = new Engine();

            engine.Run();
        }
    }
}
