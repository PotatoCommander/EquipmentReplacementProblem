using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentReplacementProblem.Render.Models.EqualityComparers
{
    class CartesianEdgeEqualityComparer : IEqualityComparer<CartesianEdge>
    {
        public bool Equals(CartesianEdge x, CartesianEdge y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            var cartesianPointEqualityComparer = new CartesianPointEqualityComparer();

            return cartesianPointEqualityComparer.Equals(x.From, y.From) && cartesianPointEqualityComparer.Equals(x.To, y.To);
        }

        public int GetHashCode(CartesianEdge obj)
        {
            return HashCode.Combine(obj.From.Y, obj.From.X, obj.To.Y, obj.To.X);
        }
    }
}
