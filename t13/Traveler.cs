using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Travelling
{
    public class Traveler
    {
        protected string name;
        protected string location;
        protected RouteManager<string> route = new RouteManager<string>();

        private string CapitalizeWords(string input)
        {
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }

        public Traveler(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }

        public void SetLocation(string location)
        {
            this.location = CapitalizeWords(location);
        }

        public string GetLocation()
        {
            return location;
        }
        public void AddCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City name cannot be null or empty.", nameof(city));
            }
            route.AddItem(CapitalizeWords(city));
        }

        public string GetRoute()
        {
            return route.ToString();
        }

        public override string ToString()
        {
            return "Traveler: " + name + " | " + "Location: " + location + " | " + "Route: " + GetRoute();
        }

        public void ClearRoute()
        {
            route.Clear();
        }

        public int GetStopCount()
        {
            return route.Count;
        }

        public override bool Equals(object obj)
        {
            if (obj is Traveler other)
            {
                return name == other.name && location == other.location;
            }   
            return false;
        }

        public override int GetHashCode()
        {
            return (name + location).GetHashCode();
        }

        public static bool operator ==(Traveler a, Traveler b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            return a.Equals(b);
            }

        public static bool operator !=(Traveler a, Traveler b)
        {
            return !(a == b);
        }

        public virtual string GetTravelSummary()
        {
            return $"Traveler: {name} | Location: {location} | Route: {GetRoute()}";
        }

        public void SaveToFile(string filePath)
        {
            string[] cities = new string[0];

            if (GetStopCount() != 0)
            {
                cities = GetRoute().Split(" -> ");
            }
            var data = new
            {
                name = name,
                currentLocation = location,
                route = cities
            };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
        }

        public static Traveler LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File doesn’t exist");
            }

            string json = File.ReadAllText(filePath);
            TravelerData data;

            try
            {
                data = JsonSerializer.Deserialize<TravelerData>(json);
            }

            catch (JsonException)
            {
                throw new FileLoadException("Invalid travel data");
            }

            if (data == null)
            {
                throw new FileLoadException("Invalid travel data");
            }

            Traveler traveler = new Traveler(data.name);
            traveler.SetLocation(data.currentLocation);

            foreach (var city in data.route)
            {
                traveler.AddCity(city);
            }

            return traveler;
        }

        public bool PlanRouteTo(string destination, CityGraph map)
        {
            string start;

            if (!string.IsNullOrWhiteSpace(location))
            {
                start = GetLocation();
            }
            else if (GetStopCount() > 0)
            {
                start = GetRoute().Split(" -> ")[0];
            }
            else
            {
                Console.WriteLine("No route!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("No route!");
                return false;
            }
            else
            {
                destination = CapitalizeWords(destination);
            }

            List<string> path = map.FindShortestPath(start, destination);

            if (path.Count == 0)
            {
                Console.WriteLine("No route!");
                return false;
            }

            ClearRoute();
            foreach (var city in path)
            {
                AddCity(city);
            }
            return true;
        }
    }

    public class TravelerData
    {
        public string name { get; set; }
        public string currentLocation { get; set; }
        public string[] route { get; set; }
    }
}