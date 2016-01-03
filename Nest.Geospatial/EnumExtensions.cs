using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Nest.Geospatial
{
    public static class EnumExtensions
    {
        internal static string GetStringValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var info = type.GetField(enumValue.ToString());
            var attribute = info.GetCustomAttribute<EnumMemberAttribute>();

            return attribute != null 
                ? attribute.Value 
                : Enum.GetName(type, enumValue);
        }
    }
}