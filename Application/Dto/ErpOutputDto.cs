using System.Collections.Generic;
using DealerServiceSystem.Business.Core.Models.Graph;

namespace DealerServiceSystem.Business.Dto
{
    public class ErpOutputDto
    {
        public List<GraphEdge> OptimalPath { get; set; }

        public List<GraphVertex> GraphVertices { get; set; }

        public List<GraphEdge> GraphEdges { get; set; }
    }
}
