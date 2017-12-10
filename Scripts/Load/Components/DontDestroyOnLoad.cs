using UnityEngine;

namespace UtilityModule.Load.Components {
	/// <summary>
	/// Don't destroy on load component.
	/// </summary>
	public class DontDestroyOnLoad : MonoBehaviour {
		#region unity methods
		/// <summary>
		/// Awakes this instance.
		/// </summary>
		private void Awake() {
			DontDestroyOnLoad(gameObject);
		}
		#endregion

	}
}