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
    }
}