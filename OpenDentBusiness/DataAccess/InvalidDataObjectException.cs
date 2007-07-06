using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental.DataAccess
{

	[global::System.Serializable]
	public class InvalidDataObjectException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public InvalidDataObjectException() { }
		public InvalidDataObjectException(string message) : base(message) { }
		public InvalidDataObjectException(string message, Exception inner) : base(message, inner) { }
		protected InvalidDataObjectException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
