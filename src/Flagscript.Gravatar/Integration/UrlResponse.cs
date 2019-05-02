using Newtonsoft.Json;

namespace Flagscript.Gravatar.Integration
{

	[JsonObject]
	internal class UrlResponse
	{
		
		[JsonProperty("title")]
		public string Title { get; set; }


		[JsonProperty("value")]
		public string Value { get; set; }

	}

}
