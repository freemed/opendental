using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are the definitions for the custom patient fields added and managed by the user.</summary>
	[Serializable]
	public class PatFieldDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatFieldDefNum;
		///<summary>The name of the field that the user will be allowed to fill in the patient info window.</summary>
		public string FieldName;
		///<summary>Enum:PatFieldType Text=0,PickList=1,Date=2,Checkbox=3,Currency=4</summary>
		public PatFieldType FieldType;
		/// <summary>The text that contains pick list values.</summary>
		public string PickList;
		//<summary>Enum:PatFieldMapping Certain reports such as Medicaid make use of patient fields that are explicitly mapped.</summary>
		//public PatFieldMapping FieldMapping;

		///<summary></summary>
		public PatFieldDef Copy() {
			return (PatFieldDef)this.MemberwiseClone();
		}

		
	}


	public enum PatFieldType {
		///<summary>0</summary>
		Text,
		///<summary>1</summary>
		PickList,
		///<summary>2-Stored in db as entered, already localized.  For example, it could be 2/04/11, 2/4/11, 2/4/2011, or any other variant.  This makes it harder to create queries that filter by date, but easier to display dates as part of results.</summary>
		Date,
		///<summary>3-If checked, value stored as "1".  If unchecked, row deleted.</summary>
		Checkbox,
		///<summary>4-This seems to have been added without implementing.  Not sure what will happen if someone tries to use it.</summary>
		Currency
	}

	//public enum PatFieldMapping{
		//<summary>0</summary>
		//None,
		//<summary>1</summary>
		//IncomeForPoverty	
	//}


}










