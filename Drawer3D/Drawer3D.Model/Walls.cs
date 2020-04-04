using System.Collections.Generic;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Стены
    /// </summary>
    public class Walls
    {
        /// <summary>
        ///     Высота стен
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Точки на эскизе по которым будут построены стены
        /// </summary>
        public List<int> Points { get; set; }
    }
}