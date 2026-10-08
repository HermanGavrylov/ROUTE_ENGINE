using System;
using System.IO;

namespace Travelling
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter filename: ");
            string fileName = Console.ReadLine();
            try
            {
                Traveler traveler = Traveler.LoadFromFile(fileName);
                Console.WriteLine(traveler.GetTravelSummary());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File doesn’t exist");
            }

        }
    }
}
