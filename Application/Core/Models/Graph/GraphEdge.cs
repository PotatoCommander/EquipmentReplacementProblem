using QuikGraph;

namespace EquipmentReplacementProblem.Business.Core.Models.Graph
{
    public class GraphEdge : IEdge<GraphVertex>
    {
        public GraphVertex Source { get; }

        public GraphVertex Target { get; }

        public int Cost { get; private set; }

        public string Name { get; private set; }

        public GraphEdge(GraphVertex source, GraphVertex target, int cost, string name)
        {
            Source = source;
            Target = target;
            Cost = cost;
            Name = name;
        }
    }
}
