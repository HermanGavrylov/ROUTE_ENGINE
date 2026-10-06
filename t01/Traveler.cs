using System.Collections.Generic;
using System;

namespace Travelling
{
    public class Traveler : ICloneable
    {
        protected string name;
        protected string location;
        protected List<string> route = new List<string>();

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
            route.Add(CapitalizeWords(city));
        }

        public string GetRoute()
        {
            return string.Join(" -> ", route);
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

        public bool HasCity(string city)
        {
            city = CapitalizeWords(city);
            return route.Contains(city);
        }

        public void SortRoute()
        {
            route.Sort();
        }

        public bool RemoveCity(string city)
        {
            city = CapitalizeWords(city);
            return route.Remove(city);
        }

        public string GetNextStop()
        {
            if (route.Count > 0)
            {
                return route[0];
            }
            else
            {
                return null;
            }
        }

        public string this[int index]
        {
            get
            {
                    return route[index];
            }
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

        public virtual object Clone()
        {
            Traveler copy = new Traveler(this.name);
            copy.location = this.location;
            copy.route = new List<string>(this.route);
            return copy;
        }

        public virtual string GetTravelSummary()
        {
            return $"Traveler: {name} | Location: {location} | Route: {GetRoute()}";
        }
    }
}