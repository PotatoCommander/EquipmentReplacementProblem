namespace EquipmentReplacementProblem.Business.Core.Models.Graph
{
    public class GraphVertex
    {
        public GraphVertex Keep { get; set; }

        public GraphVertex Replace { get; set; }

        public int EquipmentAge { get; set; }

        public int YearOfDecision { get; set; }

        public string Name { get; set; }
    }
}
