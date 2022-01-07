using ConsoleApp.DAL;
using ConsoleApp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
namespace ConsoleApp
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            PrintWeather();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void PrintWeather()
        {
            var weatherDAL = new WeatherDAL(_iconfiguration);
            var listWeatherModel = weatherDAL.GetList();
            bool loop = true;
            while (loop)
            {
                int rememberMinute = 2;
                Console.WriteLine("Press 1 to sort by date");
                Console.WriteLine("Press 2 to sort by temprature");
                Console.WriteLine("Press 3 to sort by humidity");
                Console.WriteLine("Press 4 to sort by moldrisk and show database");
                Console.WriteLine("Press space to show database");
                Console.WriteLine("Press enter to exit program");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        SortByDatum sortByDatum = new SortByDatum();
                        listWeatherModel.Sort(sortByDatum);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        SortByTemperatur sortByTemperatur = new SortByTemperatur();
                        listWeatherModel.Sort(sortByTemperatur);
                        Console.WriteLine("Sorted temperatures");
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        SortByLuftfuktighet sortByLuftfuktighet = new SortByLuftfuktighet();
                        listWeatherModel.Sort(sortByLuftfuktighet);
                        Console.WriteLine("Sorted humidity");
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        SortByMogel sortByMogel = new SortByMogel();
                        listWeatherModel.Sort(sortByMogel);
                        listWeatherModel.ForEach(item =>
                        {
                            if (item.Datum.Minute % 20 == 0 && item.Datum.Minute != rememberMinute)
                            {
                            }
                            if (item.Luftfuktighet >= 75 && item.Plats == "Inne")
                                Console.WriteLine(item.Datum + "    " + item.Plats + "  " + item.Temperatur + "C " + item.Luftfuktighet);
                            else if (item.Luftfuktighet >= 85 && item.Plats == "Ute")
                                Console.WriteLine(item.Datum + "    " + item.Plats + "  " + item.Temperatur + "C " + item.Luftfuktighet);
                        });
                        Console.WriteLine("Sorted mold risk low to high and wrote database");
                        Console.WriteLine("Please use another sorting method before showing database. Otherwise don't blame me for the weird order");
                        break;
                    case ConsoleKey.Spacebar:
                        Console.Clear();
                        listWeatherModel.ForEach(item =>
                        {
                            if (item.Datum.Minute % 20 == 0 && item.Datum.Minute != rememberMinute)
                            {
                                rememberMinute = item.Datum.Minute;
                                Console.WriteLine("Date   Place   Temp   Air");
                            }
                            Console.WriteLine(item.Datum + "    " + item.Plats + "  " + item.Temperatur + "C " + item.Luftfuktighet);
                        });
                        Console.WriteLine("Sorted dates");
                        break;
                    case ConsoleKey.Enter:
                        loop = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("That input is not registered to any action");
                        break;
                }
            }
        }
    }
}