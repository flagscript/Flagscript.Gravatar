using System;

using Flurl;

namespace Flagscript.Gravatar
{

	/// <summary>
	/// Current Gravatar Profile information exposed to Flagscript applications.
	/// </summary>
    public class GravatarProfile
    {

		/// <summary>
		/// Primary email address of the Gravatar profile.
		/// </summary>
		/// <value>Primary email address of the Gravatar profile.</value>
		public string Email { get; private set; }

		/// <summary>
		/// Profile Url of the Gravatar user.
		/// </summary>
		/// <value>Profile Url of the Gravatar user.</value>
		public string ProfileUrl { get; internal set; }

		/// <summary>
		/// Thumbnail Url of the Gravatar user.
		/// </summary>
		/// <value>Thumbnail Url of the Gravatar user.</value>
		public string ThumbnailUrl { get; internal set; }

		/// <summary>
		/// First name of the profile.
		/// </summary>
		/// <value>First name of the profile.</value>
		public string FirstName { get; internal set; }

		/// <summary>
		/// Last name of the profile.
		/// </summary>
		/// <value>Last name of the profile.</value>
		public string LastName { get; internal set; }

		/// <summary>
		/// Full name of the profile.
		/// </summary>
		/// <value>Full name of the profile.</value>
		public string FullName { get; internal set; }

		/// <summary>
		/// Descriptive information about the profile user.
		/// </summary>
		/// <value>Descriptive information about the profile user.</value>
		public string About { get; internal set; }

		/// <summary>
		/// Creates an instance of <see cref="GravatarProfile"/> initialized to an email address.
		/// </summary>
		/// <param name="email">Email address of the Gravatar profile.</param>
		internal GravatarProfile(string email)
		{
			Email = email?.Trim().ToLower() ?? throw new ArgumentNullException(nameof(email));
		}

    }

}
