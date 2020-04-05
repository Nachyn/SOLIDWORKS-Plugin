using System;
using System.ComponentModel;
using System.Linq;

namespace Drawer3D.Model.Extensions
{
    /// <summary>
    ///     Методы расширения для перечислений
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Получить атрибут TAttribute с перечисления TEnum
        /// </summary>
        /// <typeparam name="TEnum">Перечисление</typeparam>
        /// <typeparam name="TAttribute">Атрибут</typeparam>
        /// <param name="enumValue">Расширяемое перечисление</param>
        /// <returns></returns>
        public static TAttribute GetAttributeFromEnum<TEnum, TAttribute>(TEnum enumValue)
            where TAttribute : Attribute
        {
            var type = typeof(TEnum);
            var memInfo = type.GetMember(type.GetEnumName(enumValue));
            var attribute = memInfo[0]
                .GetCustomAttributes(typeof(TAttribute), false)
                .FirstOrDefault() as TAttribute;

            return attribute;
        }

        /// <summary>
        ///     Возвращает значение атрибута "Description"
        /// </summary>
        public static string GetEnumDescription<TEnum>(this TEnum enumValue)
        {
            var descriptionAttribute =
                GetAttributeFromEnum<TEnum, DescriptionAttribute>(enumValue);

            return descriptionAttribute.Description ?? enumValue.ToString();
        }
    }
}