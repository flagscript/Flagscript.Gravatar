using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

using Flurl;

namespace Flagscript.Gravatar.TagHelpers
{

	/// <summary>
	/// Tag Helper to insert gravatar image tags. 
	/// </summary>
	public class GravatarTagHelper : TagHelper
	{

		/// <summary>
		/// Fallback Gravatara Base Url.
		/// </summary>
		//private const string GravatarImageBaseUrl = "https://s.gravatar.com/avatar/";

		/// <summary>
		/// The alt text value
		/// </summary>
		/// <remarks>
		/// Can be passed via <gravatar alt="My Gravatar" />.
		/// </remarks>
		/// <value>The alt text value</value>
		public string Alt { get; set; }

		/// <summary>
		/// The CSS class value.
		/// </summary>
		/// <remarks>
		/// Can be passed via <gravatar class="myStyleOne myStyleTwo" />.
		/// </remarks>
		/// <value>The CSS class value.</value>
		public string Class { get; set; }

		/// <summary>
		/// The gravatar request size value.
		/// </summary>
		/// <value>The gravatar request size value.</value>
		public int? Size { get; set; }

		/// <summary>
		/// Logger to log errors and warnings.
		/// </summary>
		/// <value>Logger to log errors and warnings.</value>
		private ILogger Logger { get; set; }

		/// <summary>
		/// Gravatar Library to obtain profiles.
		/// </summary>
		/// <value>Gravatar Library to obtain profiles.</value>
		private GravatarLibrary Library { get; set; }

		/// <summary>
		/// Default Constructor.
		/// </summary>
		public GravatarTagHelper() => Library = new GravatarLibrary();

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarTagHelper"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to log errors and warnings.</param>
		public GravatarTagHelper(ILogger<GravatarTagHelper> logger) => (Library, Logger) = (new GravatarLibrary(), logger);

		/// <summary>
		/// Constructor with a <see cref="GravatarLibrary"/> to obtain profiles.
		/// </summary>
		/// <param name="library">Gravatar Library to obtain profiles.</param>
		public GravatarTagHelper(GravatarLibrary library) => Library = library;

		/// <summary>
		/// Constructor with a <see cref="GravatarLibrary"/> to obtain profiles and
		/// a logging context.
		/// </summary>
		/// <param name="library">Gravatar Library to obtain profiles.</param>
		/// <param name="logger">Logger to log errors and warnings.</param>
		public GravatarTagHelper(GravatarLibrary library, ILogger<GravatarTagHelper> logger) => (Library, Logger) = (library, logger);

		/// <summary>
		/// Asynchronously executes the <see cref="TagHelper"/> with the given <c>context</c> 
		/// and <c>output</c>.
		/// </summary>
		/// <param name="context">Contains information associated with the current HTML tag.</param>
		/// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
		/// <returns>A <see cref="Task"/> that on completion updates the <c>output</c>.</returns>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			
			// make sure we have content
			var content = await output.GetChildContentAsync();
			var gravatarEmail = content.GetContent();
			if (string.IsNullOrWhiteSpace(gravatarEmail))
			{
				Logger?.LogWarning($"Tag helper gravatar email missing content. Suppressing output.");
				output.SuppressOutput();
				return;
			}

			output.TagName = "img";

			// Gravatar image src
			try
			{

				var profile = await Library.GetGravatarProfile(gravatarEmail);
				if (profile == null)
				{
					Logger?.LogWarning($"Unable to retrieve gravatar profile for {gravatarEmail}. Suppressing output.");
					output.SuppressOutput();
					return;
				}
				var gravatarUrl = profile.ThumbnailUrl.SetQueryParam("s", Size);
				output.Attributes.SetAttribute("src", gravatarUrl);

			}
			catch (Exception ex)
			{

				Logger?.LogWarning(ex, $"Exception retrieving gravatar profile for {gravatarEmail}. Suppressing output.");
				output.SuppressOutput();
				return;
			}
			
			// Pass-through attributes
			if (!string.IsNullOrWhiteSpace(Alt))
			{
				output.Attributes.SetAttribute("alt", Alt);
			}
			if (!string.IsNullOrWhiteSpace(Class))
			{
				output.Attributes.SetAttribute("class", Class);
			}					

			// html 5 img tag compliance
			output.TagMode = TagMode.StartTagOnly;			

		}

	}

}
