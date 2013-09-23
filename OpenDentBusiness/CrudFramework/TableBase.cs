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
		///<summary>Not a db column.  Always false by default.  Will only be true if explicitly set to true by programmer.  When CRUD grabs a table from db, it is naturally set to False.  Once set, this value is not used by the CRUD in any manner.  Just used by the programmer for making decisions about whether to Insert or Update.</summary>
		public bool IsNew {
			get { return isNew; }
			set { isNew = value; }
		}

	
		

	}
}
