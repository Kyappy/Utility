using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json boolean support.
	/// </summary>
	/// <seealso cref="JsonNode" />
	public class JsonBool : JsonNode {
		#region JsonNode public getters implementation		
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.Boolean; } }

		/// <summary>
		/// Gets a value indicating whether this instance is boolean.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is boolean; otherwise, <c>false</c>.
		/// </value>
		public override bool IsBoolean { get { return true; } }
		#endregion

		#region JsonNode public properties implementation		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public sealed override string Value {
			get { return data.ToString(); }
			set {
				bool v;
				if (bool.TryParse(value, out v)) data = v;
			}
		}

		/// <summary>
		/// Gets or sets the value as bool.
		/// </summary>
		/// <value>
		/// The value as bool.
		/// </value>
		public override bool AsBool { get { return data; } set { data = value; } }
		#endregion

		#region Private fields		
		/// <summary>
		/// The data.
		/// </summary>
		private bool data;
		#endregion

		#region JsonNode public methods implementation		
		/// <summary>
		/// Serializes the node.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public override void Serialize(System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.Boolean);
			writer.Write(data);
		}

		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indentation incrementation value.</param>
		/// <param name="mode">The mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation,
			JsonTextMode mode) {
			stringBuilder.Append(data ? "true" : "false");
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			return data == obj as bool?;
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
		/// Initializes a new instance of the <see cref="JsonBool"/> class.
		/// </summary>
		/// <param name="data">if set to <c>true</c> [a data].</param>
		public JsonBool(bool data) {
			this.data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonBool"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public JsonBool(string data) {
			Value = data;
		}
		#endregion
	}
}