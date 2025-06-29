using System;
using System.ServiceModel;

namespace GTTestConsol
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost service = new ServiceHost(typeof(GTPriceImporterService.GTPriceImporterWCF));

            service.Open();
            Console.WriteLine("Connected");

            Console.WriteLine("Press For Exit");
            Console.ReadKey();

            service.Close();
            Console.WriteLine("Closed");

            Console.ReadKey();
        }
    }
}