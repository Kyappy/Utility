namespace UtilityModule.Command.Contracts {
	/// <summary>
	/// Command interface.
	/// </summary>
	/// <typeparam name="T">Type of the command target.</typeparam>
	public interface ICommand<in T> {
		/// <summary>
		/// Executes the command on the specified target.
		/// </summary>
		/// <param name="target">The target on which to execute the command.</param>
		void Execute(T target);
	}
}