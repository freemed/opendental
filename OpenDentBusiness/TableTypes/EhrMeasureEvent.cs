using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class EhrMeasureEvent:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrMeasureEventNum;
		///<summary></summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTEvent;
		///<summary>Enum: EhrMeasureEventType. </summary>
		public EhrMeasureEventType EventType;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary></summary>
		public string MoreInfo;

		///<summary></summary>
		public EhrMeasureEvent Copy() {
			return (EhrMeasureEvent)this.MemberwiseClone();
		}
	}

	public enum EhrMeasureEventType {
		///<summary>0</summary>
		EducationProvided,
		///<summary>1</summary>
		TimelyAccessProvided,
		///<summary>2</summary>
		CopyHealthRequested,
		///<summary>3</summary>
		CopyHealthProvided,
		///<summary>4, For one office visit.</summary>
		ClinicalSummaryProvided,
		///<summary>5</summary>
		ReminderSent,
		///<summary>6</summary>
		TransitionCareIn,
		///<summary>7, Includes referrals.</summary>
		TransitionCareOut
	}

}