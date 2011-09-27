using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are custom fields added and managed by the user.</summary>
	[Serializable]
	public class PatField:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatFieldNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to patfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window.</summary>
		public string FieldName;
		///<summary>Any text that the user types in.  For picklists, this will contain the picked text.  For dates, this is stored as the user typed it, after validating that it could be parsed.  So queries that involve dates won't work very well.  If we want better handling of date fields, we should add a column to this table.  Checkbox will either have a value of 1, or else the row will be deleted from the db.  Currency is handled in a culture neutral way, just like other currency in the db.</summary>
		public string FieldValue;
		//<summary>The last date that this field was updated.  Useful for certain reports.  User controls.  Loosely automated.</summary>
		//public DateTime DateLastUpdated;


		///<summary></summary>
		public PatField Copy() {
			return (PatField)this.MemberwiseClone();
		}

	}

		



		
	

	

	


}










