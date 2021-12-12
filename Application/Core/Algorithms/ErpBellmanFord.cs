using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentReplacementProblem.Business.Core.Models.Graph;
using EquipmentReplacementProblem.Business.Dto;
using QuikGraph;
using QuikGraph.Algorithms;

namespace EquipmentReplacementProblem.Business.Core.Algorithms
{
    internal class ErpBellmanFord
    {
        private readonly AdjacencyGraph<GraphVertex, GraphEdge> _graph;
        private readonly Dictionary<GraphEdge, double> _costs;
        private readonly GraphVertex _startVertex;
        private readonly GraphVertex _endVertex;
        private readonly EquipmentReplacementProblemGraph _erpGraph;

        public ErpBellmanFord(ErpInputDto erpInputDto)
        {
            _graph = new AdjacencyGraph<GraphVertex, GraphEdge>();
            _costs = new Dictionary<GraphEdge, double>();

            _erpGraph = new EquipmentReplacementProblemGraph(
                erpInputDto.EquipmentServiceInformation,
                erpInputDto.YearStart,
                erpInputDto.EquipmentAgeAtStart);

            _startVertex = _erpGraph.StartVertex;
            _endVertex = EquipmentReplacementProblemGraph.End;

            foreach (var edge in _erpGraph.Edges)
            {
                _graph.AddVerticesAndEdge(edge);
                _costs.Add(edge, -edge.Cost);
            }
        }

        public void PrintShortestPath()
        {
            var edgeCost = AlgorithmExtensions.GetIndexer(_costs);
            var tryGetPath = _graph.ShortestPathsBellmanFord(edgeCost, _startVertex, out _);

            if (tryGetPath(_endVertex, out var path))
            {
                PrintPath(_startVertex, _endVertex, path);
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

        public ErpOutputDto GetSolutionData()
        {
            var edgeCost = AlgorithmExtensions.GetIndexer(_costs);
            var tryGetPath = _graph.ShortestPathsBellmanFord(edgeCost, _startVertex, out _);

            if (tryGetPath(_endVertex, out var path))
            {
                return new ErpOutputDto()
                {
                    GraphEdges = _erpGraph.Edges,
                    GraphVertices = _erpGraph.Vertices,
                    OptimalPath = path.ToList(),
                };
            }
            else
            {
                Console.WriteLine("No path found from {0} to {1}.");
                return null;
            }
        }
    }
}
