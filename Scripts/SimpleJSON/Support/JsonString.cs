using System.Text;
using JetBrains.Annotations;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json node support.
	/// </summary>
	/// <seealso cref="JsonNode" />
	public class JsonString : JsonNode {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.String; } }

		/// <summary>
		/// Gets a value indicating whether this instance is string.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is string; otherwise, <c>false</c>.
		/// </value>
		public override bool IsString { get { return true; } }
		#endregion

		#region JsonNode public properties implementation
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public override string Value { get { return data; } set { data = value; } }
		#endregion

		#region Private fields
		/// <summary>
		/// The contained data.
		/// </summary>
		private string data;
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Serializes using the the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public override void Serialize([NotNull] System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.String);
			writer.Write(data);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			if (base.Equals(obj)) return true;
			var s = obj as string;
			if (s != null) return data == s;
			var s2 = obj as JsonString;
			if (s2 != null) return data == s2.data;
			return false;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() {
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			return data.GetHashCode();
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonString"/> class.
		/// </summary>
		/// <param name="data">a data.</param>
		public JsonString(string data) {
			this.data = data;
		}
		#endregion

		#region JsonNode internal methods implementation
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indent value.</param>
		/// <param name="indentIncrementation">a indent inc.</param>
		/// <param name="mode">The mode.</param>
		internal override void WriteToStringBuilder([NotNull] StringBuilder stringBuilder, int indent, int indentIncrementation,
			JsonTextMode mode) {
			stringBuilder.Append('\"').Append(Escape(data)).Append('\"');
		}
		#endregion
	}
}