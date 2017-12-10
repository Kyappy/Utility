using System;
using System.Globalization;
using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json number support.
	/// </summary>
	/// <seealso cref="JsonNode" />
	public class JsonNumber : JsonNode {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.Number; } }

		/// <summary>
		/// Gets a value indicating whether this instance is number.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is number; otherwise, <c>false</c>.
		/// </value>
		public override bool IsNumber { get { return true; } }
		#endregion

		#region JsonNode public properties implementation
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public sealed override string Value {
			get { return data.ToString(CultureInfo.InvariantCulture); }
			set {
				double v;
				if(double.TryParse(value, out v)) data = v;
			}
		}

		/// <summary>
		/// Gets or sets the value as double.
		/// </summary>
		/// <value>
		/// The value as double.
		/// </value>
		public override double AsDouble { get { return data; } set { data = value; } }
		#endregion

		#region Private fields
		/// <summary>
		/// The number data.
		/// </summary>
		private double data;
		#endregion

		#region Public methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonNumber"/> class.
		/// </summary>
		/// <param name="data">The json number data.</param>
		public JsonNumber(double data) {
			this.data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonNumber"/> class.
		/// </summary>
		/// <param name="data">The json number data.</param>
		public JsonNumber(string data) {
			Value = data;
		}

		/// <summary>
		/// Determines whether the specified value is numeric.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsNumeric(object value) {
			return value is int || value is uint || value is float || value is double || value is decimal || value is long || value is ulong || value is short || value is ushort || value is sbyte || value is byte;
		}
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Serializes the data using the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public override void Serialize(System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.Number);
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
			if (obj == null) return false;
			if (base.Equals(obj)) return true;
			var s2 = obj as JsonNumber;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (s2 != null) return data == s2.data;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (IsNumeric(obj)) return Convert.ToDouble(obj) == data;
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

		#region JsonNode internal methods implementation
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indent incrementation.</param>
		/// <param name="mode">The mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode) {
			stringBuilder.Append(data);
		}
		#endregion
	}
}