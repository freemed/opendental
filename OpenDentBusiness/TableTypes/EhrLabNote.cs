using EhrLaboratories;
using System;

namespace OpenDentBusiness {
	///<summary>For EHR module, May either be a note attached to an EhrLab or an EhrLabResult.  NTE.*</summary>
	[Serializable]
	public class EhrLabNote:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrLabNoteNum;
		///<summary>FK to EhrLab.EhrLabNum.  Should never be zero.</summary>
		public long EhrLabNum;
		///<summary>FK to EhrLabResult.EhrLabResult.  May be 0 if this is a Lab Note, will be valued if this is an Ehr Lab Result Note.</summary>
		public long EhrLabResultNum;
		///<summary>Carret delimited list of comments.  Comments must be formatted text and cannot contain the following 6 characters |^&~\#  NTE.*.*</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Comments;


		///<summary></summary>
		public EhrLabNote Copy() {
			return (EhrLabNote)MemberwiseClone();
		}

	}

}