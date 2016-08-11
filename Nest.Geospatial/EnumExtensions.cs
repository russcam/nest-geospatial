using System;
using System.Runtime.Serialization;

namespace Nest.Geospatial
{
	/// <summary>
	/// Extension methods for enums
	/// </summary>
    internal static class EnumExtensions
    {
        internal static string GetStringValue(this Enum enumValue)
        {
			var type = enumValue.GetType();
			var info = type.GetField(enumValue.ToString());
			var attributes = (EnumMemberAttribute[])info.GetCustomAttributes(typeof(EnumMemberAttribute), false);

			return attributes.Length > 0 
				? attributes[0].Value 
				: Enum.GetName(type, enumValue);
        }
    }
}