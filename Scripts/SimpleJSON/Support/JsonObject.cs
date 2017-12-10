using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json object support.
	/// </summary>
	/// <seealso cref="T:UtilityModule.SimpleJSON.Support.JsonNode" />
	/// <seealso cref="T:System.Collections.IEnumerable" />
	public class JsonObject : JsonNode, IEnumerable {
		#region JsonNode public getters implementation
		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public override JsonNodeType Tag { get { return JsonNodeType.Object; } }

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public override IEnumerable<JsonNode> Children { get { return jsonNodes.Select(node => node.Value); } }

		/// <summary>
		/// Gets a value indicating whether this instance is object.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is object; otherwise, <c>false</c>.
		/// </value>
		public override bool IsObject { get { return true; } }
		#endregion

		#region IEnumerable public getters implementation
		/// <summary>
		/// Gets the children node count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public override int Count { get { return jsonNodes.Count; } }
		#endregion

		#region public getters
		/// <summary>
		/// Gets the children keys.
		/// </summary>
		/// <value>
		/// The keys.
		/// </value>
		public Dictionary<string, JsonNode>.KeyCollection Keys { get { return jsonNodes.Keys; } }
		#endregion

		#region JsonNode public operators implementation
		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns>The node matching the key.</returns>
		public override JsonNode this[string key] {
			get { return jsonNodes.ContainsKey(key) ? jsonNodes[key] : new JsonLazyCreator(this, key); }
			set {
				if(value == null) value = new JsonNull();
				if(jsonNodes.ContainsKey(key)) jsonNodes[key] = value;
				else jsonNodes.Add(key, value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns>The node matching the index.</returns>
		public override JsonNode this[int index] {
			get {
				if(index < 0 || index >= jsonNodes.Count) return null;
				return jsonNodes.ElementAt(index).Value;
			}
			set {
				if(value == null) value = new JsonNull();
				if(index < 0 || index >= jsonNodes.Count) return;
				var key = jsonNodes.ElementAt(index).Key;
				jsonNodes[key] = value;
			}
		}
		#endregion

		#region public members		
		/// <summary>
		/// Indicates if the object is inline.
		/// </summary>
		public bool Inline = false;
		#endregion

		#region private fields
		/// <summary>
		/// The children json nodes.
		/// </summary>
		private readonly Dictionary<string, JsonNode> jsonNodes = new Dictionary<string, JsonNode>();
		#endregion

		#region JsonNode public methods implementation
		/// <summary>
		/// Adds the specified item at the given key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item to add.</param>
		public override void Add(string key, JsonNode item) {
			if (item == null) item = new JsonNull();

			if (!string.IsNullOrEmpty(key)) {
				if (jsonNodes.ContainsKey(key)) jsonNodes[key] = item;
				else jsonNodes.Add(key, item);
			}
			else jsonNodes.Add(Guid.NewGuid().ToString(), item);
		}

		/// <summary>
		/// Removes the node at the specified key.
		/// </summary>
		/// <param name="key">The key of the node to remove.</param>
		/// <returns>The node removed.</returns>
		public override JsonNode Remove(string key) {
			if (!jsonNodes.ContainsKey(key)) return null;
			var tmp = jsonNodes[key];
			jsonNodes.Remove(key);
			return tmp;
		}

		/// <summary>
		/// Removes the node at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The value or null.</returns>
		public override JsonNode Remove(int index) {
			if (index < 0 || index >= jsonNodes.Count) return null;
			var item = jsonNodes.ElementAt(index);
			jsonNodes.Remove(item.Key);
			return item.Value;
		}

		/// <summary>
		/// Removes the specified node.
		/// </summary>
		/// <param name="node">The node to remove.</param>
		/// <returns>The removed node or null.</returns>
		public override JsonNode Remove(JsonNode node) {
			try {
				var item = jsonNodes.First(k => k.Value == node);
				jsonNodes.Remove(item.Key);
				return node;
			}
			catch { return null; }
		}

		/// <summary>
		/// Serializes the specified writer.
		/// </summary>
		/// <param name="writer">The writer to serialize.</param>
		public override void Serialize(System.IO.BinaryWriter writer) {
			writer.Write((byte) JsonNodeType.Object);
			writer.Write(jsonNodes.Count);
			foreach (var key in jsonNodes.Keys) {
				writer.Write(key);
				jsonNodes[key].Serialize(writer);
			}
		}
		#endregion

		#region public methods		
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerated nodes.</returns>
		public IEnumerator GetEnumerator() {
			return jsonNodes.GetEnumerator();
		}
		#endregion

		#region JsonNode internal methods implementation
		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indent incrementation.</param>
		/// <param name="mode">The text mode.</param>
		internal override void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode) {
			stringBuilder.Append('{');
			var first = true;
			if(Inline) mode = JsonTextMode.Compact;
			foreach(var k in jsonNodes) {
				if(!first) stringBuilder.Append(',');
				first = false;
				if(mode == JsonTextMode.Indent) stringBuilder.AppendLine();
				if(mode == JsonTextMode.Indent) stringBuilder.Append(' ', indent + indentIncrementation);
				stringBuilder.Append('\"').Append(Escape(k.Key)).Append('\"');
				if(mode == JsonTextMode.Compact) stringBuilder.Append(':');
				else stringBuilder.Append(" : ");
				k.Value.WriteToStringBuilder(stringBuilder, indent + indentIncrementation, indentIncrementation, mode);
			}
			if(mode == JsonTextMode.Indent) stringBuilder.AppendLine().Append(' ', indent);
			stringBuilder.Append('}');
		}
		#endregion
	}
}