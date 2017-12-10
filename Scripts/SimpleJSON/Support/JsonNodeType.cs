namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json node type.
	/// </summary>
	public enum JsonNodeType {
		/// <summary>
		/// The array json node type.
		/// </summary>
		Array = 1,

		/// <summary>
		/// The object json node type.
		/// </summary>
		Object = 2,

		/// <summary>
		/// The string json node type.
		/// </summary>
		String = 3,

		/// <summary>
		/// The number json node type.
		/// </summary>
		Number = 4,

		/// <summary>
		/// The null value json node type.
		/// </summary>
		NullValue = 5,

		/// <summary>
		/// The boolean json node type.
		/// </summary>
		Boolean = 6,

		/// <summary>
		/// The none json node type.
		/// </summary>
		None = 7,
	}
}