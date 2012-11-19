using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace OpenDentalWebService {
	///<summary>Packages any object with a TypeName so that it can be serialized and deserialized better.</summary>
	public class DtoObject {
		///<summary>Object type.  Examples: int, Patient, Patient[], List&lt;Patient&gt;.</summary>
		public string TypeName;
		///<summary>The actual object.</summary>
		public object Obj;

	}
}
