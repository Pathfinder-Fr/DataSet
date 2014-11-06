// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace Xml2Json
{
    using System;
    using System.Linq;

    internal static class Extensions
    {
        public static string EmptyAsNull(this string @this)
        {
            if (@this == null)
                return null;

            if (@this.Length == 0)
                return null;

            return @this;
        }

        public static string ToCamelCase(this string @this)
        {
            if (char.IsLower(@this[0]))
                return @this;

            var chars = @this.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }

        public static string[] ToFlagsArray(this Enum @this)
        {
            var enumType = @this.GetType();

            if (Convert.ToInt32(@this) == 0)
                return null;

            return Enum.GetValues(enumType).Cast<Enum>().Where(x => Convert.ToInt32(x) != 0 && @this.HasFlag(x)).Select(x => x.ToString().ToCamelCase()).ToArray();
        }

        public static string ToCamelCase(this Enum @this)
        {
            return @this.ToString().ToCamelCase();
        }
    }
}