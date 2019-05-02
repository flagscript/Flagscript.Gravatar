using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Flagscript.Gravatar.Integration
{

	[JsonObject]
	internal class GravatarResponse
	{ 

		[JsonProperty("entry")]
		internal IEnumerable<EntryResponse> Entries { get; set; }

	}

}
