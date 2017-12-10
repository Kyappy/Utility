using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UtilityModule.SimpleJSON.Support {
	/// <summary>
	/// Json node support.
	/// </summary>
	public abstract class JsonNode {
		#region Public getters
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public virtual int Count { get { return 0; } }

		/// <summary>
		/// Gets a value indicating whether this instance is number.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is number; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsNumber { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether this instance is string.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is string; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsString { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether this instance is boolean.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is boolean; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsBoolean { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether this instance is null.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is null; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsNull { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether this instance is array.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is array; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsArray { get { return false; } }

		/// <summary>
		/// Gets a value indicating whether this instance is object.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is object; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsObject { get { return false; } }

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public virtual IEnumerable<JsonNode> Children { get { yield break; } }

		/// <summary>
		/// Gets the deep children.
		/// </summary>
		/// <value>
		/// The deep children.
		/// </value>
		public IEnumerable<JsonNode> DeepChildren { get { return Children.SelectMany(child => child.DeepChildren); } }

		/// <summary>
		/// Gets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public abstract JsonNodeType Tag { get; }

		/// <summary>
		/// Gets the value as array.
		/// </summary>
		/// <value>
		/// The value as array.
		/// </value>
		public virtual JsonArray AsArray { get { return this as JsonArray; } }

		/// <summary>
		/// Gets the value as object.
		/// </summary>
		/// <value>
		/// The value as object.
		/// </value>
		public virtual JsonObject AsObject { get { return this as JsonObject; } }
		#endregion

		#region Public properties
		// ReSharper disable once ValueParameterNotUsed		
		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns>The json node.</returns>
		public virtual JsonNode this[int index] { get { return null; } set { } }

		// ReSharper disable once ValueParameterNotUsed		
		/// <summary>
		/// Gets or sets the <see cref="JsonNode"/> with the specified key.
		/// </summary>
		/// <value>
		/// The <see cref="JsonNode"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns>The json node.</returns>
		public virtual JsonNode this[string key] { get { return null; } set { } }

		// ReSharper disable once ValueParameterNotUsed		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public virtual string Value { get { return ""; } set { } }

		/// <summary>
		/// Gets or sets the value as double.
		/// </summary>
		/// <value>
		/// The value as double.
		/// </value>
		public virtual double AsDouble {
			get {
				double value;
				return double.TryParse(Value, out value) ? value : 0.0;
			}
			set { Value = value.ToString(CultureInfo.InvariantCulture); }
		}

		/// <summary>
		/// Gets or sets as the value int.
		/// </summary>
		/// <value>
		/// The value as int.
		/// </value>
		public virtual int AsInt { get { return (int) AsDouble; } set { AsDouble = value; } }

		/// <summary>
		/// Gets or sets the value as float.
		/// </summary>
		/// <value>
		/// The value as float.
		/// </value>
		public virtual float AsFloat { get { return (float) AsDouble; } set { AsDouble = value; } }

		/// <summary>
		/// Gets or sets the value as boolean.
		/// </summary>
		/// <value>
		/// The value as boolean.
		/// </value>
		public virtual bool AsBool {
			get {
				bool value;
				if (bool.TryParse(Value, out value)) return value;
				return !string.IsNullOrEmpty(Value);
			}
			set { Value = (value) ? "true" : "false"; }
		}
		#endregion

		#region Internal members		
		/// <summary>
		/// The escape builder.
		/// </summary>
		internal static StringBuilder EscapeBuilder = new StringBuilder();
		#endregion

		#region Public methods
		/// <summary>
		/// Adds the the given item at the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="item">The item to insert.</param>
		public virtual void Add(string key, JsonNode item) { }

		/// <summary>
		/// Adds the specified item.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public virtual void Add(JsonNode item) {
			Add("", item);
		}

		/// <summary>
		/// Removes the node at the specified key.
		/// </summary>
		/// <param name="key">The key of the node to remove.</param>
		/// <returns></returns>
		public virtual JsonNode Remove(string key) {
			return null;
		}

		/// <summary>
		/// Removes the specified index.
		/// </summary>
		/// <param name="index">The index of the node to remove.</param>
		/// <returns></returns>
		public virtual JsonNode Remove(int index) {
			return null;
		}

		/// <summary>
		/// Removes the specified node.
		/// </summary>
		/// <param name="node">The node to remove.</param>
		/// <returns></returns>
		public virtual JsonNode Remove(JsonNode node) {
			return node;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString() {
			var stringBuilder = new StringBuilder();
			WriteToStringBuilder(stringBuilder, 0, 0, JsonTextMode.Compact);
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <param name="indent">The indentation value.</param>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public virtual string ToString(int indent) {
			var stringBuilder = new StringBuilder();
			WriteToStringBuilder(stringBuilder, 0, indent, JsonTextMode.Indent);
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			return ReferenceEquals(this, obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() {
			// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
			return base.GetHashCode();
		}

		/// <summary>
		/// Casts the specified json node.
		/// </summary>
		/// <param name="jsonNode">The json node to cast.</param>
		/// <param name="key">The key.</param>
		/// <returns>The cast JsonNode</returns>
		public static JsonNode Cast(JsonNode jsonNode, string key) {
			var result = jsonNode[key];
			if (result.IsObject) return result.AsObject;
			return result.IsArray ? result.AsArray : result;
		}

		/// <summary>
		/// Parses the specified json string.
		/// </summary>
		/// <param name="json">The json string to parse.</param>
		/// <returns>The json node resulting of the parsing.</returns>
		/// <exception cref="Exception">
		/// JSON Parse: Too many closing brackets
		/// or
		/// JSON Parse: Quotation marks seems to be messed up.
		/// </exception>
		public static JsonNode Parse(string json) {
			var stack = new Stack<JsonNode>();
			JsonNode ctx = null;
			var i = 0;
			var token = new StringBuilder();
			var tokenName = "";
			var quoteMode = false;
			var tokenIsQuoted = false;
			while (i < json.Length) {
				switch (json[i]) {
					case '{':
						if (quoteMode) {
							token.Append(json[i]);
							break;
						}
						stack.Push(new JsonObject());
						if (ctx != null) { ctx.Add(tokenName, stack.Peek()); }
						tokenName = "";
						token.Length = 0;
						ctx = stack.Peek();
						break;

					case '[':
						if (quoteMode) {
							token.Append(json[i]);
							break;
						}

						stack.Push(new JsonArray());
						if (ctx != null) { ctx.Add(tokenName, stack.Peek()); }
						tokenName = "";
						token.Length = 0;
						ctx = stack.Peek();
						break;

					case '}':
					case ']':
						if (quoteMode) {
							token.Append(json[i]);
							break;
						}
						if (stack.Count == 0) throw new Exception("JSON Parse: Too many closing brackets");

						stack.Pop();
						if (token.Length > 0 || tokenIsQuoted) {
							ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
							tokenIsQuoted = false;
						}
						tokenName = "";
						token.Length = 0;
						if (stack.Count > 0) ctx = stack.Peek();
						break;

					case ':':
						if (quoteMode) {
							token.Append(json[i]);
							break;
						}
						tokenName = token.ToString();
						token.Length = 0;
						tokenIsQuoted = false;
						break;

					case '"':
						quoteMode ^= true;
						tokenIsQuoted |= quoteMode;
						break;

					case ',':
						if (quoteMode) {
							token.Append(json[i]);
							break;
						}
						if (token.Length > 0 || tokenIsQuoted) {
							ParseElement(ctx, token.ToString(), tokenName, tokenIsQuoted);
						}
						tokenName = "";
						token.Length = 0;
						break;

					case '\r':
					case '\n': break;

					case ' ':
					case '\t':
						if (quoteMode) token.Append(json[i]);
						break;

					case '\\':
						++i;
						if (quoteMode) {
							var character = json[i];
							switch (character) {
								case 't':
									token.Append('\t');
									break;
								case 'r':
									token.Append('\r');
									break;
								case 'n':
									token.Append('\n');
									break;
								case 'b':
									token.Append('\b');
									break;
								case 'f':
									token.Append('\f');
									break;
								case 'u': {
										var s = json.Substring(i + 1, 4);
										token.Append(
											(char)int.Parse(s, NumberStyles.AllowHexSpecifier));
										i += 4;
										break;
									}
								default:
									token.Append(character);
									break;
							}
						}
						break;

					default:
						token.Append(json[i]);
						break;
				}
				++i;
			}
			if (quoteMode) { throw new Exception("JSON Parse: Quotation marks seems to be messed up."); }
			return ctx;
		}

		/// <summary>
		/// Serialize the node using the specified writer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public virtual void Serialize(System.IO.BinaryWriter writer) { }

		/// <summary>
		/// Saves to stream.
		/// </summary>
		/// <param name="data">The data to save.</param>
		public void SaveToStream(System.IO.Stream data) {
			var writer = new System.IO.BinaryWriter(data);
			Serialize(writer);
		}

		/// <summary>
		/// Saves to file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="Exception">Can't use File IO stuff in the web player</exception>
		public void SaveToFile(string fileName) {
			#if USE_FileIO
			var directoryInfo = new System.IO.FileInfo(fileName).Directory;
			if (directoryInfo != null) System.IO.Directory.CreateDirectory(directoryInfo.FullName);
			using (var f = System.IO.File.OpenWrite(fileName)) SaveToStream(f);
			#else
			throw new Exception("Can't use File IO stuff in the web player");
			#endif
		}

		/// <summary>
		/// Saves to base64.
		/// </summary>
		/// <returns>The base 64 string.</returns>
		public string SaveToBase64() {
			using (var stream = new System.IO.MemoryStream()) {
				SaveToStream(stream);
				stream.Position = 0;
				return Convert.ToBase64String(stream.ToArray());
			}
		}

		/// <summary>
		/// Deserializes using the specified reader.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns>The deserialized json node</returns>
		/// <exception cref="Exception">Error deserializing JSON. Unknown tag: " + type</exception>
		public static JsonNode Deserialize(System.IO.BinaryReader reader) {
			var type = (JsonNodeType)reader.ReadByte();
			switch (type) {
				case JsonNodeType.Array: {
						var count = reader.ReadInt32();
						var tmp = new JsonArray();
						for (var i = 0; i < count; i++) tmp.Add(Deserialize(reader));
						return tmp;
					}
				case JsonNodeType.Object: {
						var count = reader.ReadInt32();
						var tmp = new JsonObject();
						for (var i = 0; i < count; i++) {
							var key = reader.ReadString();
							var val = Deserialize(reader);
							tmp.Add(key, val);
						}
						return tmp;
					}
				case JsonNodeType.String: return new JsonString(reader.ReadString());
				case JsonNodeType.Number: return new JsonNumber(reader.ReadDouble());
				case JsonNodeType.Boolean: return new JsonBool(reader.ReadBoolean());
				case JsonNodeType.NullValue: return new JsonNull();
				case JsonNodeType.None: return null;
				default: throw new Exception("Error deserializing JSON. Unknown tag: " + type);
			}
		}

		/// <summary>
		/// Loads from stream.
		/// </summary>
		/// <param name="data">The data stream.</param>
		/// <returns>The loaded json node</returns>
		public static JsonNode LoadFromStream(System.IO.Stream data) {
			using (var reader = new System.IO.BinaryReader(data)) { return Deserialize(reader); }
		}

		/// <summary>
		/// Loads from file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The loaded json node.</returns>
		/// <exception cref="Exception">Can't use File IO stuff in the web player</exception>
		public static JsonNode LoadFromFile(string fileName) {
			#if USE_FileIO
			using(var f = System.IO.File.OpenRead(fileName)) return LoadFromStream(f);
			#else
			throw new Exception("Can't use File IO stuff in the web player");
			#endif
		}

		/// <summary>
		/// Loads from base64.
		/// </summary>
		/// <param name="base64">The base64.</param>
		/// <returns>The loaded json node.</returns>
		public static JsonNode LoadFromBase64(string base64) {
			var tmp = Convert.FromBase64String(base64);
			var stream = new System.IO.MemoryStream(tmp) { Position = 0 };
			return LoadFromStream(stream);
		}
		#endregion

		#region Internal methods		
		/// <summary>
		/// Escapes the specified text.
		/// </summary>
		/// <param name="text">The text to escape.</param>
		/// <returns>The escaped text.</returns>
		internal static string Escape(string text) {
			EscapeBuilder.Length = 0;
			if (EscapeBuilder.Capacity < text.Length + text.Length / 10)
				EscapeBuilder.Capacity = text.Length + text.Length / 10;
			foreach (var c in text) {
				switch (c) {
					case '\\':
						EscapeBuilder.Append("\\\\");
						break;
					case '\"':
						EscapeBuilder.Append("\\\"");
						break;
					case '\n':
						EscapeBuilder.Append("\\n");
						break;
					case '\r':
						EscapeBuilder.Append("\\r");
						break;
					case '\t':
						EscapeBuilder.Append("\\t");
						break;
					case '\b':
						EscapeBuilder.Append("\\b");
						break;
					case '\f':
						EscapeBuilder.Append("\\f");
						break;
					default:
						EscapeBuilder.Append(c);
						break;
				}
			}
			var result = EscapeBuilder.ToString();
			EscapeBuilder.Length = 0;
			return result;
		}

		/// <summary>
		/// Writes to string builder.
		/// </summary>
		/// <param name="stringBuilder">The string builder.</param>
		/// <param name="indent">The indentation value.</param>
		/// <param name="indentIncrementation">The indentation incrementation value.</param>
		/// <param name="mode">The writing mode.</param>
		internal abstract void WriteToStringBuilder(StringBuilder stringBuilder, int indent, int indentIncrementation, JsonTextMode mode);
		#endregion

		#region Private methods		
		/// <summary>
		/// Parses the element.
		/// </summary>
		/// <param name="ctx">The context.</param>
		/// <param name="token">The token.</param>
		/// <param name="tokenName">Name of the token.</param>
		/// <param name="quoted">if set to <c>true</c> [quoted].</param>
		private static void ParseElement(JsonNode ctx, string token, string tokenName, bool quoted) {
			if (quoted) {
				ctx.Add(tokenName, token);
				return;
			}
			var tmp = token.ToLower();
			if (tmp == "false" || tmp == "true") ctx.Add(tokenName, tmp == "true");
			else if (tmp == "null") ctx.Add(tokenName, null);
			else {
				double val;
				if (double.TryParse(token, out val)) ctx.Add(tokenName, val);
				else ctx.Add(tokenName, token);
			}
		}
		#endregion

		#region Operators		
		/// <summary>
		/// Performs an implicit conversion from <see cref="string"/> to <see cref="JsonNode"/>.
		/// </summary>
		/// <param name="s">The string to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator JsonNode(string s) {
			return new JsonString(s);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="JsonNode"/> to <see cref="string"/>.
		/// </summary>
		/// <param name="d">The Node to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator string(JsonNode d) {
			return d == null ? null : d.Value;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="double"/> to <see cref="JsonNode"/>.
		/// </summary>
		/// <param name="n">The number to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator JsonNode(double n) {
			return new JsonNumber(n);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="JsonNode"/> to <see cref="double"/>.
		/// </summary>
		/// <param name="d">The node to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator double(JsonNode d) {
			return d == null ? 0 : d.AsDouble;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="float"/> to <see cref="JsonNode"/>.
		/// </summary>
		/// <param name="n">The number to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator JsonNode(float n) {
			return new JsonNumber(n);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="JsonNode"/> to <see cref="float"/>.
		/// </summary>
		/// <param name="d">The node to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator float(JsonNode d) {
			return d == null ? 0 : d.AsFloat;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="int"/> to <see cref="JsonNode"/>.
		/// </summary>
		/// <param name="n">The number to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator JsonNode(int n) {
			return new JsonNumber(n);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="JsonNode"/> to <see cref="int"/>.
		/// </summary>
		/// <param name="d">The node to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator int(JsonNode d) {
			return d == null ? 0 : d.AsInt;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="bool"/> to <see cref="JsonNode"/>.
		/// </summary>
		/// <param name="b">The boolean value to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator JsonNode(bool b) {
			return new JsonBool(b);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="JsonNode"/> to <see cref="bool"/>.
		/// </summary>
		/// <param name="d">The node to convert.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator bool(JsonNode d) {
			return d != null && d.AsBool;
		}

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="a">The node to compare.</param>
		/// <param name="b">The object to compare.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator ==(JsonNode a, object b) {
			if(ReferenceEquals(a, b)) return true;
			var aIsNull = a is JsonNull || ReferenceEquals(a, null) || a is JsonLazyCreator;
			var bIsNull = b is JsonNull || ReferenceEquals(b, null) || b is JsonLazyCreator;
			if(aIsNull && bIsNull) return true;
			if(aIsNull || bIsNull) return false;
			return a.Equals(b);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="a">The node to compare.</param>
		/// <param name="b">The object to compare.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(JsonNode a, object b) {
			return !(a == b);
		}
		#endregion operators

		#region Sharp zip lib
		#if USE_SharpZipLib
		/// <summary>
		/// Saves to compressed stream.
		/// </summary>
		/// <param name="data">The data.</param>
		public void SaveToCompressedStream(System.IO.Stream data) {
			using (var gZipOut = new ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream(data)) {
				gZipOut.IsStreamOwner = false;
				SaveToStream(gZipOut);
				gZipOut.Close();
			}
		}

		/// <summary>
		/// Saves to compressed file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="Exception">Can't use File IO stuff in the webplayer</exception>
		public void SaveToCompressedFile(string fileName) {
			#if USE_FileIO
			System.IO.Directory.CreateDirectory(new System.IO.FileInfo(fileName).Directory.FullName);
			using (var f = System.IO.File.OpenWrite(fileName)) SaveToCompressedStream(f);

			#else
			throw new Exception("Can't use File IO stuff in the webplayer");
			#endif
		}

		/// <summary>
		/// Saves to compressed base64.
		/// </summary>
		/// <returns>The compressed base 64 string.</returns>
		public string SaveToCompressedBase64() {
			using (var stream = new System.IO.MemoryStream()) {
				SaveToCompressedStream(stream);
				stream.Position = 0;
				return Convert.ToBase64String(stream.ToArray());
			}
		}

				/// <summary>
		/// Loads from compressed stream.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns>The loaded json node.</returns>
		public static JsonNode LoadFromCompressedStream(System.IO.Stream data) {
			var zin = new ICSharpCode.SharpZipLib.BZip2.BZip2InputStream(data);
			return LoadFromStream(zin);
		}

		/// <summary>
		/// Loads from compressed file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The loaded json node.</returns>
		/// <exception cref="Exception">Can't use File IO stuff in the webplayer</exception>
		public static JsonNode LoadFromCompressedFile(string fileName) {
			#if USE_FileIO
			using (var f = System.IO.File.OpenRead(fileName)) {
				return LoadFromCompressedStream(f);
			}
			#else
			throw new Exception("Can't use File IO stuff in the webplayer");
			#endif
		}

		/// <summary>
		/// Loads from compressed base64.
		/// </summary>
		/// <param name="base64">The compressed base64 string to load.</param>
		/// <returns>The loaded json node.</returns>
		public static JsonNode LoadFromCompressedBase64(string base64) {
			var tmp = System.Convert.FromBase64String(base64);
			var stream = new System.IO.MemoryStream(tmp) {Position = 0};
			return LoadFromCompressedStream(stream);
		}

		#else
		/// <summary>
		/// Saves to compressed stream.
		/// </summary>
		/// <param name="data">The data to save.</param>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public void SaveToCompressedStream(System.IO.Stream data) {
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		/// <summary>
		/// Saves to compressed file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public void SaveToCompressedFile(string fileName) {
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		/// <summary>
		/// Saves to compressed base64.
		/// </summary>
		/// <returns>The compressed base 64 string.</returns>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public string SaveToCompressedBase64() {
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		/// <summary>
		/// Loads from compressed file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The loaded json node.</returns>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public static JsonNode LoadFromCompressedFile(string fileName) {
			throw new Exception(
				"Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		/// <summary>
		/// Loads from compressed stream.
		/// </summary>
		/// <param name="data">The data to load.</param>
		/// <returns>The loaded json node.</returns>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public static JsonNode LoadFromCompressedStream(System.IO.Stream data) {
			throw new Exception(
				"Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		/// <summary>
		/// Loads from compressed base64.
		/// </summary>
		/// <param name="base64">The base64 string to load.</param>
		/// <returns>The loaded json node.</returns>
		/// <exception cref="Exception">Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON</exception>
		public static JsonNode LoadFromCompressedBase64(string base64) {
			throw new Exception(
				"Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}
		#endif
		#endregion
	}
}