namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Support class for Json data parser.
	/// </summary>
	public static class Json {
		#region public methods
		/// <summary>
		/// Parses the specified json data.
		/// </summary>
		/// <param name="data">The json data to parse.</param>
		/// <returns>The parsed json.</returns>
		public static JsonNode Parse(string data) {
			return JsonNode.Parse(data);
		}
		#endregion
	}
}