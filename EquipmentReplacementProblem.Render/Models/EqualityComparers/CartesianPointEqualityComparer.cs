using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentReplacementProblem.Render.Models.EqualityComparers
{
    class CartesianPointEqualityComparer : IEqualityComparer<CartesianPoint>
    {
        public bool Equals(CartesianPoint x, CartesianPoint y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(CartesianPoint obj)
        {
            return HashCode.Combine(obj.X, obj.Y);
        }
    }
}
