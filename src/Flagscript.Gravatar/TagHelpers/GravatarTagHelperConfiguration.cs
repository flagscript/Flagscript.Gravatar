using Microsoft.Extensions.Logging;

namespace Flagscript.Gravatar.TagHelpers
{

	/// <summary>
	/// Dependancy Injection Configuration for <see cref="GravatarTagHelper"/>.
	/// </summary>
	public class GravatarTagHelperConfiguration
	{

		/// <summary>
		/// Gravatar Library to obtain profiles.
		/// </summary>
		/// <value>Gravatar Library to obtain profiles.</value>
		public GravatarLibrary Library { get; private set; }

		/// <summary>
		/// Logger to log errors and warnings.
		/// </summary>
		/// <value>Logger to log errors and warnings.</value>
		public ILogger Logger { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarTagHelperConfiguration"/> class
		/// with a default library.
		/// </summary>
		public GravatarTagHelperConfiguration() => Library = new GravatarLibrary();

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarTagHelperConfiguration"/> class
		/// with a specified library.
		/// </summary>
		/// <param name="library">Gravatar Library to obtain profiles.</param>
		public GravatarTagHelperConfiguration(GravatarLibrary library) => Library = library;

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarTagHelperConfiguration"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to log errors and warnings.</param>
		public GravatarTagHelperConfiguration(ILogger<GravatarTagHelper> logger) => Logger = logger;

		/// <summary>
		/// Constructor with a <see cref="GravatarLibrary"/> to obtain profiles and
		/// a logging context.
		/// </summary>
		/// <param name="library">Gravatar Library to obtain profiles.</param>
		/// <param name="logger">Logger to log errors and warnings.</param>
		public GravatarTagHelperConfiguration(GravatarLibrary library, ILogger<GravatarTagHelper> logger)
		{
			Library = library ?? new GravatarLibrary();
			Logger = logger;
		}

	}

}
