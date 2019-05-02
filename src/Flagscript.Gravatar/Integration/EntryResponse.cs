using System.Collections.Generic;

using Newtonsoft.Json;

namespace Flagscript.Gravatar.Integration
{

	[JsonObject]
	internal class EntryResponse
	{

		[JsonProperty("id")]
		internal string Id { get; set; }

		[JsonProperty("hash")]
		internal string Hash { get; set; }

		[JsonProperty("requestHash")]
		internal string RequestHash { get; set; }

		[JsonProperty("profileUrl")]
		internal string ProfileUrl { get; set; }

		[JsonProperty("preferredUsername")]
		internal string PreferredUsername { get; set; }

		[JsonProperty("thumbnailUrl")]
		internal string ThumbnailUrl { get; set; }

		[JsonProperty("photos")]
		internal IEnumerable<PhotoResponse> Photos { get; set; }

		[JsonProperty("name")]
		internal NameResponse Name { get; set; }

		[JsonProperty("displayName")]
		internal string DisplayName { get; set; }

		[JsonProperty("aboutMe")]
		internal string AboutMe { get; set; }

		[JsonProperty("currentLocation")]
		internal string CurrentLocation { get; set; }

		[JsonProperty("urls")]
		internal IEnumerable<UrlResponse> Urls { get; set; }

	}

}
