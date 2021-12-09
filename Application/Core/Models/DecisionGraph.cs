using System;
using System.Collections.Generic;
using DealerServiceSystem.Business.Core.Models.Graph;
using QuikGraph;
using QuikGraph.Algorithms;

namespace DealerServiceSystem.Business.Core.Models
{
    public class DecisionGraph
    {
        private AdjacencyGraph<string, Edge<string>> _graph;
        private Dictionary<Edge<string>, double> _costs;
        private GraphModel _graphModel;

        public DecisionGraph(GraphModel graphModel)
        {
            _graph = new AdjacencyGraph<string, Edge<string>>();
            _costs = new Dictionary<Edge<string>, double>();
            _graphModel = graphModel;

            foreach (var edge in _graphModel.Edges)
            {
                AddEdgeWithCosts(edge.Source.Name, edge.Target.Name, edge.Cost);
            }
        }

        private void AddEdgeWithCosts(string source, string target, int cost)
        {
            var edge = new Edge<string>(source, target);
            _graph.AddVerticesAndEdge(edge);
            _costs.Add(edge, -cost);
        }

        public void PrintShortestPath(string @from, string to)
        {
            var edgeCost = AlgorithmExtensions.GetIndexer(_costs);
            var tryGetPath = _graph.ShortestPathsBellmanFord(edgeCost, @from, out _);

            if (tryGetPath(to, out var path))
            {
                PrintPath(@from, to, path);
            }
            else
            {
                Console.WriteLine("No path found from {0} to {1}.");
            }
        }

        private static void PrintPath(string @from, string to, IEnumerable<Edge<string>> path)
        {
            Console.Write("Path found from {0} to {1}: {0}", @from, to);
            foreach (var e in path)
                Console.Write(" > {0}", e.Target);
            Console.WriteLine();
        }
    }
}
