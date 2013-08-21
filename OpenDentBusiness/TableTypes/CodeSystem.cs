using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Used for tracking code systems imported to OD. HL7OID used for sending messages.</summary>
	[Serializable()]
	public class CodeSystem:TableBase{
		///<summary>Primary key. Not currently referenced anywhere.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CodeSystemNum;
		///<summary>.</summary>
		public string CodeSystemName;
		///<summary>Only used for display, not actually interpreted. Updated by Code System importer.</summary>
		public string VersionCur;
		///<summary>Only used for display, not actually interpreted. Updated by Convert DB script.</summary>
		public string VersionAvail;
		///<summary>Should never change.</summary>
		public string HL7OID;

		///<summary></summary>
		public CodeSystem Clone() {
			return (CodeSystem)this.MemberwiseClone();
		}

		///<summary></summary>
		public CodeSystem() {
			
		}

		///<summary>Used to generate version specific list.</summary>
		public CodeSystem(string name) {
			CodeSystemName=name;
		}

	}

	
}




