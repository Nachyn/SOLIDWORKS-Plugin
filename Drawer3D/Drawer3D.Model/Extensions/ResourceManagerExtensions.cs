using System;
using System.Resources;

namespace Drawer3D.Model.Extensions
{
    public static class ResourceManagerExtensions
    {
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