using System.Collections.Generic;
using EquipmentReplacementProblem.Business.Core.Models.Graph;

namespace EquipmentReplacementProblem.Business.Dto
{
    public class ErpOutputDto
    {
        public List<GraphEdge> OptimalPath { get; set; }

        public List<GraphVertex> GraphVertices { get; set; }

        public List<GraphEdge> GraphEdges { get; set; }
    }
}
