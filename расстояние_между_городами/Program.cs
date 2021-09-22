using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace расстояние_между_городами
{
    class Program
    {
        class City
        {
            public readonly string Name;
            public List<CityDistanceInfo> Distances = new List<CityDistanceInfo>();

            public City(string name)
            {
                Name = name;
            }
        }

        class CityDistanceInfo
        {
            public readonly City First;
            public readonly City Second;
            public readonly int Distance;

            public CityDistanceInfo(City first, City second, int distance)
            {
                First = first;
                Second = second;
                Distance = distance;
            }
        }


        static void Main(string[] args)
        {
            var cities = new List<string> { "Москва", "Екатеринбург", "Казань", "Питер", "Тюмень", "Курган", "Новгород" };

            CreateFile(cities);

            var start = AskUser("начальный", cities);
            var finish = AskUser("конечный", cities);


            bool[] visited = new bool[7] { false, false, false, false, false, false, false };
            int[] distance = new int[7] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, };
            Console.WriteLine($"путь между {start} и {finish} равен {FindWay(ReadingFile(), start, finish, visited, distance)} км");
        }

        public static void CreateFile(List<string> cities)
        {
            var distance = new List<string> { "100", "200", "300", "400", "600", "500", "700", "800", "1200", "1100", "1500" };

            var rnd = new Random();
            int num;
            var file = new StreamWriter("cities.txt");
            for (int i = 0; i < 100; i++)
            {
                var array = new string[3];

                for (int j = 0; j < 2; j++)
                {
                    num = rnd.Next(cities.Count);
                    array[j] = cities[num];
                }
                num = rnd.Next(distance.Count);
                array[2] = distance[num];
                file.WriteLine($"{array[0]};{array[1]};{array[2]}");
            }
            file.Close();
        }

        public static string AskUser(string x, List<string> cities)
        {
            string city;
            do
            {
                Console.WriteLine($"введите {x} город: ");
                city = Console.ReadLine();
                if (!cities.Contains(city))
                    Console.WriteLine("error");
            }
            while (cities.Contains(city) != true);

            return city;
        }

        private static List<City> ReadingFile()
        {
            var Cities = new List<City>();
            var newFile = new StreamReader("cities.txt");

            while (!newFile.EndOfStream)
            {
                var line = newFile.ReadLine().Split(';');

                var foundCityFirst = FindCities(Cities, line, 0);
                var foundCitySecond = FindCities(Cities, line, 1);

                foundCityFirst.Distances.Add(new CityDistanceInfo(foundCityFirst, foundCitySecond, Convert.ToInt32(line[2])));
                foundCitySecond.Distances.Add(new CityDistanceInfo(foundCitySecond, foundCityFirst, Convert.ToInt32(line[2])));
            }
            return Cities;
        }

        private static City FindCities(List<City> Cities, string[] line, int index)
        {
            var foundCity = Cities.Find((city) => city.Name == line[index]);

            if (foundCity == null)
            {
                foundCity = new City(line[index]);
                Cities.Add(foundCity);
            }
            return foundCity;
        }

        public static int FindWay(List<City> Cities, string start, string finish, bool[] visited, int[] distance)
        {
            //if (start == finish)
            //    return 0;

            //foreach (var city in Cities)
            //{
            //    visited[city.Name] = true;
            //}

            //int km = 1501;
            //foreach(var e in Cities)
            //{
            //    if (e[0] == start)
            //    {
            //        if ((e[1] == finish) && (Convert.ToInt32(e[2]) < km))
            //            km = Convert.ToInt32(e[2]);
            //    }
            //}

            //if (km == 1501)
            //    return 0;
            //else 
            //    return km;
        }
    }
}
