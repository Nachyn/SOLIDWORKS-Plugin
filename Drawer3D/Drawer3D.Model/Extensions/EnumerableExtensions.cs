using System.Collections.Generic;
using System.Linq;

namespace Drawer3D.Model.Extensions
{
    /// <summary>
    ///     Методы расширения для Enumerable коллекций
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Null или пустая коллекция
        /// </summary>
        /// <typeparam name="T">Обобщение</typeparam>
        /// <param name="enumerable">Расширяемая коллеция</param>
        /// <returns></returns>
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