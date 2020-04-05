namespace Drawer3D.Model.Extensions
{
    /// <summary>
    ///     Методы расширения для численных типов данных
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        ///     Конвертация в милли
        /// </summary>
        /// <param name="number">Перечисляемое значение</param>
        /// <returns>Конвертированное значение</returns>
        public static double ToMilli(this int number)
        {
            return number / (double) 1000;
        }


        /// <summary>
        ///     Конвертация в милли
        /// </summary>
        /// <param name="number">Перечисляемое значение</param>
        /// <returns>Конвертированное значение</returns>
        public static double ToMilli(this double number)
        {
            return number / 1000;
        }
    }
}