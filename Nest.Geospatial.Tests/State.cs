using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest.Geospatial.Tests
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum State
	{
		NSW = 1,

		VIC = 2,

		QLD = 3,

		SA = 4,

		WA = 5,

		TAS = 6,

		NT = 7,

		ACT = 8,

		OT = 9
	}
}