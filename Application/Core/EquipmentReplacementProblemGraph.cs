using System.Collections.Generic;
using EquipmentReplacementProblem.Business.Core.Models.Graph;
using EquipmentReplacementProblem.Business.Dto;

namespace EquipmentReplacementProblem.Business.Core
{
    public class EquipmentReplacementProblemGraph
    {
        public List<GraphEdge> Edges { get; set; }

        public List<GraphVertex> Vertices { get; set; }

        public GraphVertex StartVertex { get; set; }

        public static GraphVertex End { get; set; } = new GraphVertex() { Name = "End" };


        private int _initialEquipmentAge { get; set; }
        private Dictionary<int, EquipmentServiceInformation> _equipmentInformation { get; set; }
        private EquipmentServiceInformation EquipmentUsageInfoAtStart { get; set; }

        public EquipmentReplacementProblemGraph(Dictionary<int, EquipmentServiceInformation> info, int firstYearOfDecision, int initialEquipmentAge)
        {
            _initialEquipmentAge = initialEquipmentAge;
            _equipmentInformation = info;
            EquipmentUsageInfoAtStart = info[0];

            Edges = new List<GraphEdge>();
            Vertices = new List<GraphVertex>();

            StartVertex = new GraphVertex()
            {
                EquipmentAge = initialEquipmentAge,
            };
            Vertices.Add(StartVertex);

            RecursiveFill(StartVertex, firstYearOfDecision);
        }

        private void RecursiveFill(GraphVertex vertex, int yearOfDecision)
        {
            vertex.YearOfDecision = yearOfDecision;
            vertex.Name = $"[Dec {yearOfDecision} EqAge {vertex.EquipmentAge}]";

            if (yearOfDecision + _initialEquipmentAge > _equipmentInformation.Count)
            {
                vertex.Keep = End;
                vertex.Replace = End;

                Edges.Add(new GraphEdge(vertex,
                    vertex.Replace, 
                    _equipmentInformation[vertex.EquipmentAge - 1].SellEquipmentCost,
                    $"Sell"));

                Edges.Add(new GraphEdge(vertex,
                    vertex.Keep,
                    _equipmentInformation[vertex.EquipmentAge - 1].SellEquipmentCost,
                    $"Sell"));

                return;
            }

            var equipmentServiceInformation = _equipmentInformation[vertex.EquipmentAge];


            var keepCost = equipmentServiceInformation.Income - equipmentServiceInformation.ServiceExpenses;
            vertex.Keep = new GraphVertex
            {
                EquipmentAge = vertex.EquipmentAge + 1,
            };
            RecursiveFill(vertex.Keep, yearOfDecision + 1);
            Edges.Add(new GraphEdge(vertex,
                vertex.Keep,
                keepCost,
                $"Keep: year {yearOfDecision}, eq age {vertex.EquipmentAge}"));


            var replaceCost = EquipmentUsageInfoAtStart.Income 
                              - EquipmentUsageInfoAtStart.ServiceExpenses 
                              - equipmentServiceInformation.NewEquipmentCost
                              + equipmentServiceInformation.SellEquipmentCost;
            vertex.Replace = new GraphVertex
            {
                EquipmentAge = 1
            };
            RecursiveFill(vertex.Replace, yearOfDecision + 1);
            Edges.Add(new GraphEdge(vertex,
                vertex.Replace,
                replaceCost,
                $"Replace: year {yearOfDecision}, eq age {vertex.EquipmentAge}"
            ));
        }
    }
}
