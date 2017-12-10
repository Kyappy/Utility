using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json array support.
	/// </summary>
	public class JsonArray : JsonNode, IEnumerable {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.Array; } }

		/// <summary>
		/// Gets a value indicating whether this instance is array.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is array; otherwise, <c>false</c>.
		/// </value>
		public override bool IsArray { get { return true; } }

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public override IEnumerable<JsonNode> Children { get { return jsonNodes; } }

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public override int Count { get { return jsonNodes.Count; } }
		#endregion

		#region JsonNode public properties implementation
		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns>The json node at the specified index.</returns>
		public override JsonNode this[int index] {
			get {
				if (index < 0 || index >= jsonNodes.Count) return new JsonLazyCreator(this);
				return jsonNodes[index];
			}
			set {
				if (value == null) value = new JsonNull();
				if (index < 0 || index >= jsonNodes.Count) jsonNodes.Add(value);
				else jsonNodes[index] = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns>The json node matching the specified key.</returns>
		public override JsonNode this[string key] {
			get { return new JsonLazyCreator(this); }
			set {
				if (value == null) value = new JsonNull();
				jsonNodes.Add(value);
			}
		}
		#endregion

		#region Public members
		/// <summary>
		/// The inline state.
		/// </summary>
		public bool Inline = false;
		#endregion

		#region Private fields		
		/// <summary>
		/// The json nodes.
		/// </summary>
		private readonly List<JsonNode> jsonNodes = new List<JsonNode>();
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Adds the the given item at the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item to insert.</param>
		public override void Add(string key, JsonNode item) {
			if (item == null) item = new JsonNull();
			jsonNodes.Add(item);
		}

		/// <summary>
		/// Removes the json node at the specified index.
		/// </summary>
		/// <param name="index">The index of the node to remove.</param>
		/// <returns>The removed json node.</returns>
		public override JsonNode Remove(int index) {
			if (index < 0 || index >= jsonNodes.Count) return null;
			var tmp = jsonNodes[index];
			jsonNodes.RemoveAt(index);
			return tmp;
		}

		/// <summary>
		/// Removes the specified node.
		/// </summary>
		/// <param name="node">The node to remove.</param>
		/// <returns>The removed node.</returns>
		public override JsonNode Remove(JsonNode node) {
			jsonNodes.Remove(node);
			return node;
		}

		/// <summary>
		/// Serializes the json node.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public override void Serialize(System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.Array);
			writer.Write(jsonNodes.Count);
			foreach (var node in jsonNodes) { node.Serialize(writer); }
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator GetEnumerator() {
			return jsonNodes.GetEnumerator();
		}
		#endregion

		#region JsonNode internal methods implementation		
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indent value.</param>
		/// <param name="indentIncrementation">The indent incrementation value.</param>
		/// <param name="mode">The json text mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode) {
			stringBuilder.Append('[');
			var count = jsonNodes.Count;
			if (Inline) mode = JsonTextMode.Compact;
			for (var i = 0; i < count; i++) {
				if (i > 0) stringBuilder.Append(',');
				if (mode == JsonTextMode.Indent) stringBuilder.AppendLine();

				if (mode == JsonTextMode.Indent) stringBuilder.Append(' ', indent + indentIncrementation);
				jsonNodes[i].WriteToStringBuilder(stringBuilder, indent + indentIncrementation, indentIncrementation, mode);
			}
			if (mode == JsonTextMode.Indent) stringBuilder.AppendLine().Append(' ', indent);
			stringBuilder.Append(']');
		}
		#endregion
	}
}