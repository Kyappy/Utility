using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json lazy creator support.
	/// </summary>
	/// <seealso cref="JsonNode" />
	internal class JsonLazyCreator : JsonNode {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.None; } }
		#endregion

		#region JsonNode public properties implementation
		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns>The json node at the given index.</returns>
		public override JsonNode this[int index] {
			get { return new JsonLazyCreator(this); }
			set {
				var tmp = new JsonArray {value};
				Set(tmp);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns>The json node matching the given key.</returns>
		public override JsonNode this[string key] {
			get { return new JsonLazyCreator(this, key); }
			set {
				var tmp = new JsonObject {{key, value}};
				Set(tmp);
			}
		}

		/// <summary>
		/// Gets or sets the value as int.
		/// </summary>
		/// <value>
		/// The value as int.
		/// </value>
		public override int AsInt {
			get {
				var tmp = new JsonNumber(0);
				Set(tmp);
				return 0;
			}
			set {
				var tmp = new JsonNumber(value);
				Set(tmp);
			}
		}

		/// <summary>
		/// Gets or sets tha value as float.
		/// </summary>
		/// <value>
		/// The value as float.
		/// </value>
		public override float AsFloat {
			get {
				var tmp = new JsonNumber(0.0f);
				Set(tmp);
				return 0.0f;
			}
			set {
				var tmp = new JsonNumber(value);
				Set(tmp);
			}
		}

		/// <summary>
		/// Gets or sets the value as double.
		/// </summary>
		/// <value>
		/// The value as double.
		/// </value>
		public override double AsDouble {
			get {
				var tmp = new JsonNumber(0.0);
				Set(tmp);
				return 0.0;
			}
			set {
				var tmp = new JsonNumber(value);
				Set(tmp);
			}
		}

		/// <summary>
		/// Gets or sets the value as boolean.
		/// </summary>
		/// <value>
		/// The value as boolean.
		/// </value>
		public override bool AsBool {
			get {
				var tmp = new JsonBool(false);
				Set(tmp);
				return false;
			}
			set {
				var tmp = new JsonBool(value);
				Set(tmp);
			}
		}
		#endregion

		#region JsonNode public getter implementation
		/// <summary>
		/// Gets tha value as array.
		/// </summary>
		/// <value>
		/// The value as array.
		/// </value>
		public override JsonArray AsArray {
			get {
				var tmp = new JsonArray();
				Set(tmp);
				return tmp;
			}
		}

		/// <summary>
		/// Gets the value as object.
		/// </summary>
		/// <value>
		/// The value as object.
		/// </value>
		public override JsonObject AsObject {
			get {
				var tmp = new JsonObject();
				Set(tmp);
				return tmp;
			}
		}
		#endregion

		#region Private fields
		/// <summary>
		/// The json node.
		/// </summary>
		private JsonNode jsonNode;

		/// <summary>
		/// The key.
		/// </summary>
		private readonly string key;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonLazyCreator"/> class.
		/// </summary>
		/// <param name="jsonNode">The node.</param>
		public JsonLazyCreator(JsonNode jsonNode) {
			this.jsonNode = jsonNode;
			key = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonLazyCreator"/> class.
		/// </summary>
		/// <param name="jsonNode">The json node.</param>
		/// <param name="key">The key.</param>
		public JsonLazyCreator(JsonNode jsonNode, string key) {
			this.jsonNode = jsonNode;
			this.key = key;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Sets the specified value.
		/// </summary>
		/// <param name="value">The value to set.</param>
		private void Set(JsonNode value) {
			if (key == null) jsonNode.Add(value);
			else jsonNode.Add(key, value);
			jsonNode = null;
		}
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Adds the the given item.
		/// </summary>
		/// <param name="item">The item to insert.</param>
		public override void Add(JsonNode item) {
			var tmp = new JsonArray {item};
			Set(tmp);
		}

		/// <summary>
		/// Adds the the given item at the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item to insert.</param>
		public override void Add(string key, JsonNode item) {
			var tmp = new JsonObject {{key, item}};
			Set(tmp);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			return obj == null || ReferenceEquals(this, obj);
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
		#endregion

		#region Operator overload		
		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">The json lazy creator to compare.</param>
		/// <param name="b">The object to compare.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(JsonLazyCreator a, object b) {
			return b == null || ReferenceEquals(a, b);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">The json lazy creator to compare.</param>
		/// <param name="b">The object to compare.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(JsonLazyCreator a, object b) {
			return !(a == b);
		}
		#endregion

		#region JsonNode internal methods implementation
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indentation incrementation value.</param>
		/// <param name="mode">The json text mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode) {
			stringBuilder.Append("null");
		}
		#endregion
	}
}