using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Flagscript.Gravatar.Integration
{

	[JsonObject]
	public class NameResponse
	{

		[JsonProperty("givenName")]
		public string GivenName { get; set; }

		[JsonProperty("familyName")]
		public string FamilyName { get; set; }

		[JsonProperty("formatted")]
		public string Formatted { get; set; }

	}

}
