using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityModule.Pooling.Contracts {
	/// <summary>
	/// Repository base abstract class.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TItem">The type of the item.</typeparam>
	/// <typeparam name="TPool">The type of the pool.</typeparam>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	public abstract class RepositoryBase<TIndex, TItem, TPool>: MonoBehaviour
		where TIndex: struct, IConvertible
		where TItem: MonoBehaviour, IPoolable<TIndex>
		where TPool: PoolBase<TItem, TIndex> {
		#region private fields
		/// <summary>
		/// The registered pools.
		/// </summary>
		[SerializeField] private TPool[] pools;
		#endregion

		#region accessors		
		/// <summary>
		/// Gets the pools.
		/// </summary>
		/// <value>
		/// The pools.
		/// </value>
		public TPool[] Pools { get { return pools; } }

		/// <summary>
		/// Gets the registered pool dictionary.
		/// </summary>
		/// <value>
		/// The registered spells.
		/// </value>
		public static Dictionary<TIndex, TPool> PoolDictionary { get; protected set; }
		#endregion

		#region public methods
		/// <summary>
		/// Gets an instance of pool item.
		/// </summary>
		/// <param name="itemIndex">Index of the item.</param>
		/// <returns>The item matching the index.</returns>
		public virtual TItem GetPoolItem(TIndex itemIndex) {
			return PoolDictionary[itemIndex].Get;
		}

		/// <summary>
		/// Recycles the specified item at in the pool matching the given index.
		/// </summary>
		/// <param name="item">The item to recycle.</param>
		public static void Recycle(TItem item) {
			PoolDictionary[item.Index].Recycle(item);
		}
		#endregion

		#region unity methods		
		/// <summary>
		/// Starts this instance.
		/// </summary>
		protected virtual void Start() {
			PoolDictionary = new Dictionary<TIndex, TPool>();
			foreach (var pool in Pools) PoolDictionary[pool.Poolable.Index] = pool;
		}
		#endregion
	}
}