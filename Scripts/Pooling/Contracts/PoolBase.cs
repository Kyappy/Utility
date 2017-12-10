using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityModule.Pooling.Contracts {
	/// <summary>
	/// Pool base abstract class.
	/// </summary>
	/// <typeparam name="T1">The item managed by the pool.</typeparam>
	/// <typeparam name="T2">The index type of the item managed by the pool.</typeparam>
	/// <seealso cref="MonoBehaviour" />
	public abstract class PoolBase<T1, T2>: MonoBehaviour where T1: MonoBehaviour, IPoolable<T2> where T2: struct, IConvertible{
		#region public accessors		
		/// <summary>
		/// Gets the pool initial length.
		/// </summary>
		/// <value>
		/// The pool initial length.
		/// </value>
		public ushort InitialLength { get { return initialLength; } }
		/// <summary>
		/// Gets the pool minimal length.
		/// </summary>
		/// <value>
		/// The pool minimal length.
		/// </value>
		public ushort MinimalLength { get { return minimalLength; } }
		/// <summary>
		/// Gets the size of the pool chunk.
		/// </summary>
		/// <value>
		/// The size of the pool chunk.
		/// </value>
		public ushort ChunkSize { get { return chunkSize; } }
		/// <summary>
		/// Gets the initial poolable item.
		/// </summary>
		/// <value>
		/// The initial poolable item.
		/// </value>
		public T1 Poolable { get { return poolable; } }
		/// <summary>
		/// Gets the pooled object.
		/// </summary>
		/// <value>
		/// The pooled object.
		/// </value>
		public T1 Get {
			get {
				if (pool.Count <= minimalLength) ExpandPool(chunkSize);
				if (pool.Count == 0) return null;
				var item = pool[0];
				item.gameObject.SetActive(true);
				pool.RemoveAt(0);
				return item;
			}
		}
		#endregion

		#region private fields
		/// <summary>
		/// The items contained in the pool.
		/// </summary>
		private List<T1> pool = new List<T1>();
		/// <summary>
		/// The initial poolable object.
		/// </summary>
		[SerializeField] private T1 poolable;
		/// <summary>
		/// The pool length.
		/// </summary>
		[SerializeField] private ushort initialLength;
		/// <summary>
		/// The pool minimal length.
		/// </summary>
		[SerializeField] private ushort minimalLength;
		/// <summary>
		/// The pool chunk size.
		/// </summary>
		[SerializeField] private ushort chunkSize;
		/// <summary>
		/// The pool transform
		/// </summary>
		[SerializeField] private Transform poolTransform;
		#endregion

		#region public methods
		/// <summary>
		/// Adds a poolable object to the pool.
		/// </summary>
		public void Add(T1 poolableItem) {
			poolableItem = Instantiate(poolableItem, poolTransform, true);
			poolableItem.gameObject.SetActive(false);
			pool.Add(poolableItem);
		}

		/// <summary>
		/// Recycle the pooled element.
		/// </summary>
		/// <param name="poolableItem"></param>
		public void Recycle(T1 poolableItem) {
			if (pool.Contains(poolableItem)) return;
			poolableItem.transform.SetParent(poolTransform);
			poolableItem.gameObject.SetActive(false);
			pool.Add(poolableItem);
		}

		/// <summary>
		/// Clears the pool.
		/// </summary>
		public void ClearPool() {
			for (var i = pool.Count - 1; i > 0; i--) {
				var item = pool[i];
				pool.RemoveAt(i);
				Destroy(item);
			}
			pool = new List<T1>();
		}

		/// <summary>
		/// Expands the pool with the given length.
		/// </summary>
		/// <param name="length">Length of the poolable items to add.</param>
		public void ExpandPool(ushort length) {
			for (var i = 0; i < length; i++) Add(poolable);
		}
		#endregion

		#region unity methods		
		/// <summary>
		/// Starts this instance.
		/// </summary>
		private void Start() {
			ExpandPool(initialLength);
		}
		#endregion
	}
}