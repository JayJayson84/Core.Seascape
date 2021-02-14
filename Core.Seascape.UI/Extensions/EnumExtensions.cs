using Core.Seascape.UI.Helpers.Attributes;
using System;
using System.Linq;

namespace Core.Seascape.UI.Extensions
{
    public static class EnumExtensions
    {

        /// <summary>
        /// Gets the <see cref="string"/> value of the <see cref="EnumTextAttribute"/> decorating the <see cref="Enum"/> member.
        /// </summary>
        /// <param name="value">Instance type to extend.</param>
        /// <returns>The <see cref="string"/> value of the <see cref="EnumTextAttribute"/> decorating the <see cref="Enum"/> member.</returns>
        public static string GetText(this Enum value)
        {
            return (value?
                .GetType()
                .GetField(value
                    .ToString())?
                    .GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[])
                    .FirstOrDefault()?
                    .StringValue;
        }

        /// <summary>
        /// Gets the <see cref="Enum"/> member that contains a matching <see cref="EnumTextAttribute"/> value.
        /// </summary>
        /// <param name="value">Instance type to extend.</param>
        /// <returns>The <see cref="Enum"/> member that contains the matching <see cref="EnumTextAttribute"/> value.</returns>
        public static T GetEnum<T>(this string value) where T : Enum
        {
            Enum.TryParse(
                typeof(T),
                typeof(T)
                    .GetFields()
                    .FirstOrDefault(x => (x
                        .GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[])
                        .FirstOrDefault()?
                        .StringValue == value)?
                    .Name,
                out var result);

            return result is T
                ? (T)Convert.ChangeType(result, typeof(T))
                : (T)Enum.ToObject(typeof(T), default);
        }

        /// <summary>
        /// Gets the <see cref="Int32"/> value of the <see cref="Enum"/> member that contains a matching <see cref="EnumTextAttribute"/> value.
        /// </summary>
        /// <param name="value">Instance type to extend.</param>
        /// <returns>The <see cref="Int32"/> value of the <see cref="Enum"/> member that contains the matching <see cref="EnumTextAttribute"/> value.</returns>
        public static int GetEnumValue<T>(this string value) where T : Enum
        {
            Enum.TryParse(
                typeof(T),
                typeof(T)
                    .GetFields()
                    .FirstOrDefault(x => (x
                        .GetCustomAttributes(typeof(EnumTextAttribute), false) as EnumTextAttribute[])
                        .FirstOrDefault()?
                        .StringValue == value)?
                    .Name,
                out var result);

            return (int)(result ?? 0);
        }

    }
}
