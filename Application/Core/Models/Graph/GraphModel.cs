using System.Collections.Generic;

namespace DealerServiceSystem.Business.Core.Models.Graph
{
    public class GraphModel
    {
        public List<GraphEdge> Edges { get; set; }

        public List<GraphVertex> Vertices { get; set; }

        public GraphVertex StartVertex { get; set; }

        private static GraphVertex End { get; set; } = new GraphVertex() { Name = "End" };


        private int _initialEquipmentAge { get; set; }
        private Dictionary<int, AtomicEquipmentInfo> _equipmentInformation { get; set; }
        private AtomicEquipmentInfo _equipmentInfoAtStart { get; set; }

        public GraphModel(EquipmentUsageInformation info, int firstYearOfDecision, int initialEquipmentAge)
        {
            _initialEquipmentAge = initialEquipmentAge;
            _equipmentInformation = info.EquipmentInfo;
            _equipmentInfoAtStart = info.EquipmentInfo[0];

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

            var atomicEquipmentInfo = _equipmentInformation[vertex.EquipmentAge];


            var keepCost = atomicEquipmentInfo.Income - atomicEquipmentInfo.UsageExpenses;
            vertex.Keep = new GraphVertex
            {
                EquipmentAge = vertex.EquipmentAge + 1,
            };
            RecursiveFill(vertex.Keep, yearOfDecision + 1);
            Edges.Add(new GraphEdge(vertex,
                vertex.Keep,
                keepCost,
                $"Keep: year {yearOfDecision}, eq age {vertex.EquipmentAge}"));


            var replaceCost = _equipmentInfoAtStart.Income 
                              - _equipmentInfoAtStart.UsageExpenses 
                              - atomicEquipmentInfo.NewEquipmentCost
                              + atomicEquipmentInfo.SellEquipmentCost;
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
