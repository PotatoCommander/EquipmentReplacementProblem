using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentReplacementProblem.Render.Models
{
    public class GraphRenderModel
    {
        public List<CartesianEdge> Lines { get; set; }

        public List<CartesianPoint> Points { get; set; }

        public List<CartesianEdge> Path { get; set; }
    }
}
