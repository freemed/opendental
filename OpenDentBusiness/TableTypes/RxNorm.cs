using System;

namespace OpenDentBusiness {
	///<summary>RxNorm created from a zip file.</summary>
	[Serializable]
	public class RxNorm:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RxNormNum;
		///<summary>RxNorm Concept universal ID.</summary>
		public string RxCui;
		///<summary>Multum code.  Only used for import/export with electronic Rx program.  User cannot see multum codes.</summary>
		public string MmslCode;
		///<summary>Only used for RxNorms, not Multums.</summary>
		public string Description;

		///<summary></summary>
		public RxNorm Copy() {
			return (RxNorm)this.MemberwiseClone();
		}

	}

}













