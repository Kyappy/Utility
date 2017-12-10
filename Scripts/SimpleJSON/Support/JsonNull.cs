using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json null support.
	/// </summary>
	/// <seealso cref="JsonNode" />
	public class JsonNull : JsonNode {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.NullValue; } }

		/// <summary>
		/// Gets a value indicating whether this instance is null.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is null; otherwise, <c>false</c>.
		/// </value>
		public override bool IsNull { get { return true; } }
		#endregion

		#region JsonNode public properties implementation
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public override string Value { get { return "null"; } set {} }

		/// <summary>
		/// Gets or sets a value indicating whether [as bool].
		/// </summary>
		/// <value>
		///   <c>true</c> if [as bool]; otherwise, <c>false</c>.
		/// </value>
		public override bool AsBool { get { return false; } set {} }
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			if(ReferenceEquals(this, obj)) return true;
			return (obj is JsonNull);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() {
			return 0;
		}

		/// <summary>
		/// Serializes the data using the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public override void Serialize(System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.NullValue);
		}
		#endregion

		#region JsonNode internal methods implementation
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indent incrementation.</param>
		/// <param name="mode">The mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode) {
			stringBuilder.Append("null");
		}
		#endregion
	}
}