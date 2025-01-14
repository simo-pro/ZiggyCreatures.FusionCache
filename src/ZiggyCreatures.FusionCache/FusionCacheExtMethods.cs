﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZiggyCreatures.FusionCaching
{

	/// <summary>
	/// A set of extension methods that add some commonl used overloads to any instance of a <see cref="IFusionCache"/> object.
	/// </summary>
	public static class FusionCacheExtMethods
	{

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="factory">The function which will be called if the value is not found in the cache.</param>
		/// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
		public static Task<TValue> GetOrSetAsync<TValue>(this IFusionCache cache, string key, Func<CancellationToken, Task<TValue>> factory, TimeSpan duration, CancellationToken token = default)
		{
			return cache.GetOrSetAsync<TValue>(key, factory, cache.DefaultEntryOptions.Duplicate(duration), token);
		}

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="factory">The function which will be called if the value is not found in the cache.</param>
		/// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
		public static TValue GetOrSet<TValue>(this IFusionCache cache, string key, Func<CancellationToken, TValue> factory, TimeSpan duration, CancellationToken token = default)
		{
			return cache.GetOrSet<TValue>(key, factory, cache.DefaultEntryOptions.Duplicate(duration), token);
		}

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="factory">The function which will be called if the value is not found in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
		public static Task<TValue> GetOrSetAsync<TValue>(this IFusionCache cache, string key, Func<CancellationToken, Task<TValue>> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.GetOrSetAsync<TValue>(key, factory, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="factory">The function which will be called if the value is not found in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
		public static TValue GetOrSet<TValue>(this IFusionCache cache, string key, Func<CancellationToken, TValue> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.GetOrSet<TValue>(key, factory, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="defaultValue">The defualt value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
		public static Task<TValue> GetOrDefaultAsync<TValue>(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, TValue defaultValue = default, CancellationToken token = default)
		{
#pragma warning disable CS8604 // Possible null reference argument.
			return cache.GetOrDefaultAsync<TValue>(key, defaultValue, cache.CreateEntryOptions(setupAction), token);
#pragma warning restore CS8604 // Possible null reference argument.
		}

		/// <summary>
		/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="defaultValue">The defualt value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
		public static TValue GetOrDefault<TValue>(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, TValue defaultValue = default, CancellationToken token = default)
		{
#pragma warning disable CS8604 // Possible null reference argument.
			return cache.GetOrDefault<TValue>(key, defaultValue, cache.CreateEntryOptions(setupAction), token);
#pragma warning restore CS8604 // Possible null reference argument.
		}

		/// <summary>
		/// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="TryGetResult{TValue}"/> instance.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>A <see cref="TryGetResult{TValue}"/> instance containing a <see cref="bool"/> indicating if the value was there or not and either the value or a default one.</returns>
		public static Task<TryGetResult<TValue>> TryGetAsync<TValue>(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.TryGetAsync<TValue>(key, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="TryGetResult{TValue}"/> instance.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>A <see cref="TryGetResult{TValue}"/> instance containing a <see cref="bool"/> indicating if the value was there or not and either the value or a default one.</returns>
		public static TryGetResult<TValue> TryGet<TValue>(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.TryGet<TValue>(key, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="duration"/>. If a value is already there it will be overwritten.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="value">The value to put in the cache.</param>
		/// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
		public static Task SetAsync<TValue>(this IFusionCache cache, string key, TValue value, TimeSpan duration, CancellationToken token = default)
		{
			return cache.SetAsync<TValue>(key, value, cache.DefaultEntryOptions.Duplicate(duration), token);
		}

		/// <summary>
		/// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="duration"/>. If a value is already there it will be overwritten.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="value">The value to put in the cache.</param>
		/// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		public static void Set<TValue>(this IFusionCache cache, string key, TValue value, TimeSpan duration, CancellationToken token = default)
		{
			cache.Set<TValue>(key, value, cache.DefaultEntryOptions.Duplicate(duration), token);
		}

		/// <summary>
		/// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda. If a value is already there it will be overwritten.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="value">The value to put in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
		public static Task SetAsync<TValue>(this IFusionCache cache, string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.SetAsync<TValue>(key, value, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda. If a value is already there it will be overwritten.
		/// </summary>
		/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="value">The value to put in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		public static void Set<TValue>(this IFusionCache cache, string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			cache.Set<TValue>(key, value, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Removes the value in the cache for the specified <paramref name="key"/>.
		/// </summary>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		/// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
		public static Task RemoveAsync(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			return cache.RemoveAsync(key, cache.CreateEntryOptions(setupAction), token);
		}

		/// <summary>
		/// Removes the value in the cache for the specified <paramref name="key"/>.
		/// </summary>
		/// <param name="cache">The <see cref="IFusionCache"/> instance.</param>
		/// <param name="key">The cache key which identifies the entry in the cache.</param>
		/// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="FusionCacheOptions.DefaultEntryOptions"/>.</param>
		/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
		public static void Remove(this IFusionCache cache, string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
		{
			cache.Remove(key, cache.CreateEntryOptions(setupAction), token);
		}
	}

}