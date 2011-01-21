using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	/// <summary>The base class for classes that correspond to a table in the database.  Make sure to mark each derived class [Serializable].</summary>
	abstract public class TableBase {
		private bool isNew;
		///<summary>Always false by default.  Will only be true if explicitly set to true by user.</summary>
		public bool IsNew {
			get { return isNew; }
			set { isNew = value; }
		}

	
		

	}
}
