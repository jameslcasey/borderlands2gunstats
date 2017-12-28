using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Borderlands2Guns.Models;
using System.Text.RegularExpressions;

namespace Borderlands2Guns
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    try
            //    {
            //        // Requires using MvcMovie.Models;
            //        SeedData.Initialize(services);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred seeding the DB.");
            //    }
            //}

            host.Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();



        //public static void Main()
        //{
        //    Console.WriteLine("Enter any text, followed by <Enter>:\n");
        //    String s = Console.ReadLine();
        //    ShowWords(s);
        //    Console.Write("\nPress any key to continue... ");
        //    Console.ReadKey();
        //}

        //private static void ShowWords(String s)
        //{
        //    String pattern = @"\w+";
        //    var matches = Regex.Matches(s, pattern);
        //    if (matches.Count == 0)
        //    {
        //        Console.WriteLine("\nNo words were identified in your input.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"\nThere are {matches.Count} words in your string:");
        //        for (int ctr = 0; ctr < matches.Count; ctr++)
        //        {
        //            Console.WriteLine($"   #{ctr,2}: '{matches[ctr].Value}' at position {matches[ctr].Index}");
        //        }
        //    }
        //}






    }
}



//namespace Applications.ConsoleApps
//{
//    public class ConsoleParser
//    {
//        public static void Main()
//        {
//            Console.WriteLine("Enter any text, followed by <Enter>:\n");
//            String s = Console.ReadLine();
//            ShowWords(s);
//            Console.Write("\nPress any key to continue... ");
//            Console.ReadKey();
//        }

//        private static void ShowWords(String s)
//        {
//            String pattern = @"\w+";
//            var matches = Regex.Matches(s, pattern);
//            if (matches.Count == 0)
//            {
//                Console.WriteLine("\nNo words were identified in your input.");
//            }
//            else
//            {
//                Console.WriteLine($"\nThere are {matches.Count} words in your string:");
//                for (int ctr = 0; ctr < matches.Count; ctr++)
//                {
//                    Console.WriteLine($"   #{ctr,2}: '{matches[ctr].Value}' at position {matches[ctr].Index}");
//                }
//            }
//        }
//    }
//}
