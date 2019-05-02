using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using AutoMapper;
using Flurl;
using Flurl.Http;

using Flagscript.Gravatar.Integration;
using Flagscript.Gravatar.Mapping;

namespace Flagscript.Gravatar
{

	/// <summary>
	/// 
	/// </summary>
	public class GravatarLibrary
	{
		
		/// <summary>
		/// Gravatar webservice URL prefix.
		/// </summary>
		private const string GravatarUrlPrefix = "https://www.gravatar.com";

		/// <summary>
		/// Gravatar webservice URL suffix.
		/// </summary>
		private const string GravatarUrlSuffix = ".json";

		/// <summary>
		/// Mapper to map Gravatar response to library response objects.
		/// </summary>
		private static readonly IMapper ResponseMapper = GetMapperConfiguration().CreateMapper();

		/// <summary>
		/// <see cref="MapperConfiguration"/> used to map gravatar responses to library returns.
		/// </summary>
		/// <returns><see cref="MapperConfiguration"/> used to map gravatar responses to library returns.</returns>
		private static MapperConfiguration GetMapperConfiguration()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<GravatarMappingProfile>();
			});
			return config;
		}

		public async Task<GravatarProfile> GetGravatarProfile(string email)
		{

			var profile = new GravatarProfile(email);

			// Generate Gravatar Email Hash.
			var gravatarHash = GenerateEmailHash(profile.Email);

			// Get the profile JSON as an object
			var requestUrl = Url.Combine(GravatarUrlPrefix, $"{gravatarHash}{GravatarUrlSuffix}");
			try 
			{

				var profiles = await requestUrl.WithHeaders(new { User_Agent = "Flagscript.Gravatar" }).GetJsonAsync<GravatarResponse>();

				if (profiles == null)
				{
					throw new FlagscriptException($"Error retrieving Gravatar profile for {email}.");
				}

				// Validate we got a response
				var matchEntry = profiles.Entries.FirstOrDefault(e => e.Hash == gravatarHash);
				if (matchEntry == null)
				{
					throw new FlagscriptException($"Call to Gravatar did not return any entries for {email}.");
				}

				// Map to output object
				return ResponseMapper.Map(matchEntry, profile);

			}
			catch (FlurlParsingException)
			{
				// This can be thrown when a user has not configured either a first, last, or full name
				// in Gravatar. The JSON that comes back in this case is an empty array for the name property
				// [], vs an object if these are set. This could be fixed with a custom serializer, but as this
				// is just for framework use at the moment, we will leave it. 
				throw new FlagscriptException($"Gravatar {email} does not contain any name information.");
			}
			catch (FlurlHttpException fhe)
			{
				if (fhe.Call.HttpStatus == HttpStatusCode.NotFound)
				{
					throw new GravatarNotFoundException($"Gravatar for email {email} does not exist");
				}

				throw;
			}
			

		}

		/// <summary>
		/// Generates the Gravatar email hash.
		/// </summary>
		/// <remarks>
		/// See <a href="https://en.gravatar.com/site/implement/hash/">Creating the Hash</a> for
		/// more information.
		/// </remarks>
		/// <param name="email">The email address to hash</param>
		/// <returns>The Gravatar email hash</returns>
		public string GenerateEmailHash(string email)
		{
			
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new ArgumentNullException(nameof(email));
			}

			var normalizedEmail = email.Trim().ToLower();

			using (MD5 md5Hash = MD5.Create())
			{

				var rawHash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(normalizedEmail));
				var sBuilder = new StringBuilder();
				for (int i = 0; i < rawHash.Length; i++)
				{
					sBuilder.Append(rawHash[i].ToString("x2"));
				}
				return sBuilder.ToString();

			}

		}

	}

}
