A simple JSON Parser / builder
------------------------------

It mainly has been written as a simple JSON parser. It can build a JSON string
from the node-tree, or generate a node tree from any valid JSON string.

If you want to use compression when saving to file / stream / B64 you have to include
SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ ) in your project and
define "USE_SharpZipLib" at the top of the file

Written by Bunny83 
2012-06-09


Features / attributes:
- provides strongly typed node classes and lists / dictionaries
- provides easy access to class members / array items / data values
- the parser now properly identifies types. So generating JSON with this framework should work.
- only double quotes (") are used for quoting strings.
- provides "casting" properties to easily convert to / from those types:
  int / float / double / bool
- provides a common interface for each node so no explicit casting is required.
- the parser tries to avoid errors, but if malformed JSON is parsed the result is more or less undefined
- It can serialize/deserialize a node tree into/from an experimental compact binary format. It might
  be handy if you want to store things in a file and don't want it to be easily modifiable


2012-12-17 Update:
- Added internal JSONLazyCreator class which simplifies the construction of a JSON tree
  Now you can simple reference any item that doesn't exist yet and it will return a JSONLazyCreator
  The class determines the required type by it's further use, creates the type and removes itself.
- Added binary serialization / deserialization.
- Added support for BZip2 zipped binary format. Requires the SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ )
  The usage of the SharpZipLib library can be disabled by removing or commenting out the USE_SharpZipLib define at the top
- The serializer uses different types when it comes to store the values. Since my data values
  are all of type string, the serializer will "try" which format fits best. The order is: int, float, double, bool, string.
  It's not the most efficient way but for a moderate amount of data it should work on all platforms.
  
2017-03-08 Update:
- Optimised parsing by using a StringBuilder for token. This prevents performance issues when large
  string data fields are contained in the json data.
- Finally refactored the badly named JSONClass into JSONObject.
- Replaced the old JSONData class by distict typed classes ( JSONString, JSONNumber, JSONBool, JSONNull ) this
  allows to propertly convert the node tree back to json without type information loss. The actual value
  parsing now happens at parsing time and not when you actually access one of the casting properties.

2017-04-11 Update:
- Fixed parsing bug where empty string values have been ignored.
- Optimised "ToString" by using a StringBuilder internally. This should heavily improve performance for large files
- Changed the overload of "ToString(string aIndent)" to "ToString(int aIndent)"

The MIT License (MIT)

Copyright (c) 2012-2017 Markus Göbel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.