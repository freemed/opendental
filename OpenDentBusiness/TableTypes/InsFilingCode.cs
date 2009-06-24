using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Description of table</summary>
	[DataObject("InsFilingCode")]
	public class InsFilingCode : DataObjectBase{

		[DataField("InsFilingCodeNum",PrimaryKey=true,AutoNumber=true)]
		private int insFilingCodeNum;
		private bool insFilingCodeNumChanged;
		///<summary>Primary key.</summary>
		public int InsFilingCodeNum{
			get { return insFilingCodeNum;}
			set { if(insFilingCodeNum!=value) { insFilingCodeNum=value; MarkDirty(); insFilingCodeNumChanged=true; } }
		}
		public bool InsFilingCodeNumChanged{
			get { return insFilingCodeNumChanged; }
		}

		[DataField("Descript")]
		private string descript;
		private bool descriptChanged;
		///<summary>Description of the insurance filing code.</summary>
		public string Descript {
			get { return descript; }
			set { if(descript!=value) { descript=value; MarkDirty(); descriptChanged=true; } }
		}
		public bool DescriptChanged {
			get { return descriptChanged; }
		}

		[DataField("EclaimCode")]
		private string eclaimCode;
		private bool eclaimCodeChanged;
		///<summary>Code for electronic claim.</summary>
		public string EclaimCode {
			get { return eclaimCode; }
			set { if(eclaimCode!=value) { eclaimCode=value; MarkDirty(); eclaimCodeChanged=true; } }
		}
		public bool EclaimCodeChanged {
			get { return eclaimCodeChanged; }
		}

		[DataField("ItemOrder")]
		private int itemOrder;
		private bool itemOrderChanged;
		///<summary>Display order for this filing code within the UI.</summary>
		public int ItemOrder {
			get { return itemOrder; }
			set { if(itemOrder!=value) { itemOrder=value; MarkDirty(); itemOrderChanged=true; } }
		}
		public bool ItemOrderChanged {
			get { return itemOrderChanged; }
		}
		
		public InsFilingCode Copy(){
			return (InsFilingCode)Clone();
		}	

	}
}



