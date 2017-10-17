using UnityEngine;
using Utility.Command.Contracts;
using Utility.Data.Supports;

namespace Utility.Data.Contracts {
	/// <summary>
	/// Modify amount command base class.
	/// </summary>
	/// <typeparam name="T">Type of the command target.</typeparam>
	/// <seealso cref="ICommand{T}" />
	public abstract class ModifyAmountCommandBase<T> : ICommand<T> {
		#region private fields		
		/// <summary>
		/// The value to modify.
		/// </summary>
		[SerializeField] protected float value;
		/// <summary>
		/// TThe amount operation to apply.
		/// </summary>
		[SerializeField] protected AmountOperation amountOperation;
		#endregion

		#region public constructors		
		/// <summary>
		/// Initializes a new instance of the <see cref="ModifyAmountCommandBase{T}"/> class.
		/// </summary>
		/// <param name="value">The value to modify.</param>
		/// <param name="amountOperation">The amount operation to apply.</param>
		protected ModifyAmountCommandBase(float value, AmountOperation amountOperation) {
			this.value = value;
			this.amountOperation = amountOperation;
		}
		#endregion

		#region public methods		
		/// <summary>
		/// Executes the command on the specified target.
		/// </summary>
		/// <param name="target">The target on which to execute the command.</param>
		public abstract void Execute(T target);
		#endregion
	}
}