using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Stores the list of insurance filing code subtypes.</summary>
	[DataObject("InsFilingCodeSubtype")]
	public class Tablename : DataObjectBase{
		[DataField("InsFilingCodeSubtypeNum",PrimaryKey=true,AutoNumber=true)]
		private int insFilingCodeSubtypeNum;
		private bool insFilingCodeSubtypeNumChanged;
		///<summary>Primary key.</summary>
		public int InsFilingCodeSubtypeNum{
			get{return insFilingCodeSubtypeNum;}
			set{if(insFilingCodeSubtypeNum!=value){insFilingCodeSubtypeNum=value;MarkDirty();insFilingCodeSubtypeNumChanged=true;}}
		}
		public bool InsFilingCodeSubtypeNumChanged{
			get{return insFilingCodeSubtypeNumChanged;}
		}

		[DataField("InsFilingCodeNum")]
		private int insFilingCodeNum;
		private bool insFilingCodeNumChanged;
		///<summary>FK to insfilingcode.insfilingcodenum</summary>
		public int InsFilingCodeNum{
			get{return insFilingCodeNum;}
			set{if(insFilingCodeNum!=value){insFilingCodeNum=value;MarkDirty();insFilingCodeNumChanged=true;}}
		}
		public bool InsFilingCodeNumChanged{
			get{return insFilingCodeNumChanged;}
		}

		[DataField("Descript")]
		private string descript;
		private bool descriptChanged;
		///<summary>The description of the insurance filing code subtype.</summary>
		public string Descript{
			get{return descript;}
			set{if(descript!=value){descript=value;MarkDirty();descriptChanged=true;}}
		}
		public bool DescriptChanged{
			get{return descriptChanged;}
		}
		
		public Tablename Copy(){
			return (Tablename)Clone();
		}	
	}
}


