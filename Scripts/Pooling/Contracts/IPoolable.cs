using System;

namespace Utility.Pooling.Contracts {
	/// <summary>
	/// Poolable interface.
	/// </summary>
	/// <typeparam name="T">Poolable index type.</typeparam>
	public interface IPoolable<out T> where T : struct, IConvertible {
		/// <summary>
		/// Gets the poolable item index.
		/// </summary>
		/// <value>
		/// The poolable item index.
		/// </value>
		T Index { get; }
	}
}