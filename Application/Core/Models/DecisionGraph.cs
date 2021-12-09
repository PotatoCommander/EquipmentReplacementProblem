using System;
using System.Collections.Generic;
using DealerServiceSystem.Business.Core.Models.Graph;
using QuikGraph;
using QuikGraph.Algorithms;

namespace DealerServiceSystem.Business.Core.Models
{
    public class DecisionGraph
    {
        private AdjacencyGraph<GraphVertex, GraphEdge> _graph;
        private Dictionary<GraphEdge, double> _costs;
        private GraphModel _graphModel;

        public DecisionGraph(GraphModel graphModel)
        {
            _graph = new AdjacencyGraph<GraphVertex, GraphEdge>();
            _costs = new Dictionary<GraphEdge, double>();
            _graphModel = graphModel;

            foreach (var edge in _graphModel.Edges)
            {
                _graph.AddVerticesAndEdge(edge);
                _costs.Add(edge, -edge.Cost);
            }
        }

        public void PrintShortestPath(GraphVertex @from, GraphVertex to)
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

        private static void PrintPath(GraphVertex @from, GraphVertex to, IEnumerable<GraphEdge> path)
        {
            Console.Write("Path found from {0} to {1}: {0}", @from.Name, to.Name);
            foreach (var e in path)
            {
                Console.Write(" > {0}", e.Name);
            }

            Console.WriteLine();
        }
    }
}
