using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Microsoft.Extensions.Logging;

using AutoMapper;
using Flurl;
using Flurl.Http;

using Flagscript.Caching.Memory;
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

		/// <summary>
		/// Memory Cache to use for Gravatars.
		/// </summary>
		/// <value> Memory Cache to use for Gravatars.</value>
		private GravatarProfileMemoryCache MemoryCache { get; set; }

		/// <summary>
		/// Logger used for logging.
		/// </summary>
		/// <value>Logger used for logging..</value>
		private ILogger Logger { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarLibrary"/> class.
		/// </summary>
		public GravatarLibrary()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarLibrary"/> class
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to be used for logging.</param>
		public GravatarLibrary(ILogger<GravatarLibrary> logger) => Logger = logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarLibrary"/> class with a 
		/// memory cache.
		/// </summary>
		/// <param name="memoryCache">Memory cache.</param>
		public GravatarLibrary(GravatarProfileMemoryCache memoryCache) => MemoryCache = memoryCache;

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarLibrary"/> class 
		/// with a memory cache and logging context.
		/// </summary>
		/// <param name="memoryCache">Memory cache.</param>
		/// <param name="logger">Logger to be used for logging.</param>
		public GravatarLibrary(GravatarProfileMemoryCache memoryCache, ILogger<GravatarLibrary> logger) => (MemoryCache, Logger) = (memoryCache, logger);


		public async Task<GravatarProfile> GetGravatarProfile(string email)
		{

			if (MemoryCache != null)
			{

				Logger?.LogDebug($"Retrieveing Gravatar {email} from cache.");
				object cacheKey = MemoryCache.GenerateCacheKey(email);
				return await MemoryCache.GetOrCreateAsync(cacheKey, async () =>
				{

					// Need to get the entry into the Get/Create Async method in the future.
					return await GetProfileFromGravatar(email).ConfigureAwait(false);

				});

			}

			Logger?.LogDebug($"Retrieveing Gravatar {email} directly due to no cache.");
			return await GetProfileFromGravatar(email).ConfigureAwait(false);			

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

		/// <summary>
		/// Creates a <see cref="GravatarProfile"/> from the Gravatar web api.
		/// </summary>
		/// <param name="email">Email address of the gravatar profile.</param>
		/// <returns>The <see cref="GravatarProfile"/> from gravatar web api.</returns>
		private async Task<GravatarProfile> GetProfileFromGravatar(string email)
		{


			var profile = new GravatarProfile(email);

			// Generate Gravatar Email Hash.
			var gravatarHash = GenerateEmailHash(profile.Email);

			// Get the profile JSON as an object
			var requestUrl = Url.Combine(GravatarUrlPrefix, $"{gravatarHash}{GravatarUrlSuffix}");
			try
			{

				var profiles = await requestUrl.WithHeaders(new { User_Agent = "Flagscript.Gravatar" }).GetJsonAsync<GravatarResponse>().ConfigureAwait(false);

				if (profiles == null)
				{
					var throwEx = new FlagscriptException($"Gravatar Profile call for {email} returned empty body.");
					Logger?.LogWarning(throwEx, throwEx.Message);
					throw throwEx;
				}

				// Validate we got a response
				var matchEntry = profiles.Entries.FirstOrDefault(e => e.Hash == gravatarHash);
				if (matchEntry == null)
				{
					var throwEx = new FlagscriptException($"Call to Gravatar did not return any entries for {email}.");
					Logger?.LogWarning(throwEx, throwEx.Message);
					throw throwEx;
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
				var throwEx = new FlagscriptException($"Gravatar {email} does not contain any name information.");
				Logger?.LogError(throwEx, throwEx.Message);
				throw throwEx;
			}
			catch (FlurlHttpException fhe)
			{
				if (fhe.Call.HttpStatus == HttpStatusCode.NotFound)
				{
					var throwEx = new GravatarNotFoundException($"Gravatar for email {email} does not exist.");
					Logger?.LogError(throwEx, throwEx.Message);
					throw throwEx;
				}

				throw;
			}

		}

	}

}
