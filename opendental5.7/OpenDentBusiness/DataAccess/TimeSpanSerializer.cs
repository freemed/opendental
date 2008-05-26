using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OpenDental.DataAccess {
	/// <summary>
	/// The <see cref="TimeSpan"/> class cannot be serialized using the standard <see cref="XmlSerializer"/> class,
	/// see for example <see href="http://www.google.com/search?q=TimeSpan+XmlSerializer"/>. Therefore, we implement
	/// our own XML Serializer for the <see cref="TimeSpan"/> class.
	/// </summary>
	public class TimeSpanSerializer {
		private const string NodeName = "TimeSpan";

		public void Serialize(XmlWriter writer, TimeSpan value) {
			writer.WriteStartElement(NodeName);
			writer.WriteValue(value.Ticks);
			writer.WriteEndElement();
		}

		public TimeSpan Deserialize(XmlReader reader) {
			if (reader.Name != NodeName)
				throw new InvalidOperationException();

			// Read the content
			long ticks = reader.ReadElementContentAsLong();
			
			return new TimeSpan(ticks);
		}
	}
}
