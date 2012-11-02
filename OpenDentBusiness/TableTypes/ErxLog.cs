using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class ErxLog:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ErxLogNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Holds up to 16MB.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string MsgText;
		///<summary>Automatically updated by MySQL every time a row is added or changed.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public ErxLog Clone() {
			return (ErxLog)this.MemberwiseClone();
		}

	}
}
