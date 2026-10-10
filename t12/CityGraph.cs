using System;
using System.IO;
using System.Collections.Generic;

namespace Travelling
{
    public class Edge
    {
        public string To { get; set;}
        public int Distance { get; set;}
    }
    public class CityGraph
    {
        private Dictionary<string, List<Edge>> adjacencyList = new Dictionary<string, List<Edge>>();
        private List<string> connections = new List<string>();

        private void AddEdge(string from, string to, int distance)
        {
            if (!adjacencyList.ContainsKey(from))
            {
                adjacencyList[from] = new List<Edge>();
            }
            adjacencyList[from].Add(new Edge { To = to, Distance = distance });
            connections.Add($"{from}-{to},{distance}");
        }

        public static CityGraph LoadFromFile(string filePath)
        {
            CityGraph graph = new CityGraph();
            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                var cities = parts[0].Split('-');
                cities[0] = cities[0].Trim();
                cities[1] = cities[1].Trim();
                int distance = int.Parse(parts[1].Trim());
                graph.AddEdge(cities[0], cities[1], distance);
                graph.AddEdge(cities[1], cities[0], distance);
            }
            return graph;
        }

        public override string ToString()
        {
            return string.Join("\n", connections);
        }

        public List<string> FindShortestPath(string from, string to)
        {
            if (!adjacencyList.ContainsKey(from) || !adjacencyList.ContainsKey(to))
            {
                return new List<string>();
            }

            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var visited = new HashSet<string>();

            foreach (var city in adjacencyList.Keys)
            {
                distances[city] = int.MaxValue;
            }

            distances[from] = 0;

            while (true)
            {
                string currentCity = null;
                int currentDistance = int.MaxValue;
    
                foreach (var city in distances.Keys)
                {
                    if (!visited.Contains(city) && distances[city] < currentDistance)
                    {
                        currentCity = city;
                        currentDistance = distances[city];
                    }
                }

                if (currentCity == null)
                {
                    break;
                }

                visited.Add(currentCity);
                
                foreach (var edge in adjacencyList[currentCity])
                {
                    int newDistance = distances[currentCity] + edge.Distance;

                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        previous[edge.To] = currentCity;
                    }
                }
            }

            if (distances[to] == int.MaxValue)
            {
                return new List<string>();
            }

            var path = new List<string>();
            var step = to;
            while (step != null)
            {
                path.Insert(0, step);
                previous.TryGetValue(step, out step);
            }
            return path;
        }
    }
}