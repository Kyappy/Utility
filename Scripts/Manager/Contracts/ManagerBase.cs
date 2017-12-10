using UnityEngine;

namespace UtilityModule.Manager.Contracts {
	/// <summary>
	/// Manager base abstract class.
	/// </summary>
	public abstract class ManagerBase: MonoBehaviour {
		#region public accessors
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ManagerBase"/> is ready.
		/// </summary>
		/// <value>
		///   <c>true</c> if ready; otherwise, <c>false</c>.
		/// </value>
		public bool Ready { get; protected set; }
		#endregion
	}
}