using Newtonsoft.Json;

namespace Flagscript.Gravatar.Integration
{

	[JsonObject]
	internal class PhotoResponse
	{
		
		[JsonProperty("type")]
		internal string Type { get; set; }	

		[JsonProperty("value")]
		internal string Value { get; set; }

	}

}
