using EhrLaboratories;
using System;

namespace OpenDentBusiness {
	///<summary>For EHR module, May either be a note attached to an EhrLab or an EhrLabResult.  NTE.*</summary>
	[Serializable]
	public class EhrLabNote:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrLabNoteNum;
		///<summary>FK to EhrLab.EhrLabNum.  May be 0.</summary>
		public long EhrLabNum;
		///<summary>FK to EhrLabResult.EhrLabResult.  May be 0.</summary>
		public long EhrLabResultNum;
		///<summary>Carret delimited list of comments.  Comments must be formatted text and cannot contain the following 6 characters |^&~\#  NTE.*.*</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string comments;


		///<summary></summary>
		public EhrLabNote Copy() {
			return (EhrLabNote)MemberwiseClone();
		}

	}

}