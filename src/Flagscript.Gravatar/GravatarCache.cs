using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using Flagscript.Caching.Memory;

namespace Flagscript.Gravatar
{

	/// <summary>
	/// Simple In-Memory Gravatar profile memory cache.
	/// </summary>
	public class GravatarProfileMemoryCache : SimpleAsyncMemoryCacheBase<GravatarProfile>
	{

		/// <summary>
		/// The default size of the item cache. 
		/// </summary>
		/// <remarks>
		/// Just an item count for now. 
		/// </remarks>
		public static readonly long DefaultItemCacheSize = 1;

		/// <summary>
		/// Default time in minutes to cache an item. 
		/// </summary>
		public static readonly int DefaultCacheMinutes = 180;

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarProfileMemoryCache"/> class.
		/// </summary>
		public GravatarProfileMemoryCache()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarProfileMemoryCache"/> class with
		/// with a logging context.
		/// </summary>
		/// <param name="logger">Logger to be used for logging.</param>
		public GravatarProfileMemoryCache(ILogger logger) : base(logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarProfileMemoryCache"/> class
		/// with specified cache options.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		public GravatarProfileMemoryCache(MemoryCacheOptions memoryCacheOptions) : base (memoryCacheOptions)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GravatarProfileMemoryCache"/> class
		/// with specified cache options and logging context.
		/// </summary>
		/// <param name="memoryCacheOptions">Memory cache options.</param>
		/// <param name="logger">Logger to be used for logging.</param>
		public GravatarProfileMemoryCache(MemoryCacheOptions memoryCacheOptions, ILogger logger) : base(memoryCacheOptions, logger)
		{
		}

		/// <summary>
		/// Use email address as cache key.
		/// </summary>
		/// <returns>The cache key.</returns>
		/// <param name="identifier">Email address of the Gravatar.</param>
		public override object GenerateCacheKey(object identifier) => identifier;

	}

}
