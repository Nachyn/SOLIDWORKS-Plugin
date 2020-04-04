using System;
using System.Resources;

namespace Drawer3D.Model.Extensions
{
    /// <summary>
    ///     Методы расширения для менеджера ресурсов
    /// </summary>
    public static class ResourceManagerExtensions
    {
        /// <summary>
        ///     Получить форматированную строку по ключу и параметрам
        /// </summary>
        /// <param name="resourceManager"></param>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetFormattedString(this ResourceManager resourceManager
            , string key
            , params object[] args)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return string.Format(resourceManager.GetString(key) ?? key, args);
        }
    }
}