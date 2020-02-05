using System.Collections.Generic;
using System.Linq;

namespace Drawer3D.Model.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            return !enumerable.Any();
        }
    }
}