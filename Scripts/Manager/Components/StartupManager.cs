using System.Collections;
using System.Linq;
using UnityEngine;
using UtilityModule.Load.Components;
using UtilityModule.Manager.Contracts;

namespace UtilityModule.Manager.Components {
	/// <summary>
	/// Startup manager component.
	/// </summary>
	[RequireComponent(typeof(DontDestroyOnLoad))]
	public class StartupManager : MonoBehaviour {
		#region private fields
		/// <summary>
		/// The children managers.
		/// </summary>
		private ManagerBase[] managers;
		#endregion

		#region unity methods
		/// <summary>
		/// Awakes this instance.
		/// </summary>
		private void Awake() {
			managers = GetComponentsInChildren<ManagerBase>();
		}
		 
		private IEnumerator Start() {
			var allManagersReady = false;
			while (!allManagersReady) {
				allManagersReady = managers.Aggregate(true, (current, manager) => current & manager.Ready);
				yield return null;
			}

			// todo: throw managers ready event
		}
		#endregion
	}
}