using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Stores events for EHR that are needed for reporting purposes.</summary>
	[Serializable]
	public class EhrMeasureEvent:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrMeasureEventNum;
		///<summary>Date and time of measure event.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTEvent;
		///<summary>Enum: EhrMeasureEventType. </summary>
		public EhrMeasureEventType EventType;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>Only used for some types: EducationProvided, TobaccoCessation.</summary>
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
		OnlineAccessProvided,
		///<summary>2-not tracked yet.</summary>
		ElectronicCopyRequested,
		///<summary>3</summary>
		ElectronicCopyProvidedToPt,
		///<summary>4, For one office visit.</summary>
		ClinicalSummaryProvidedToPt,
		///<summary>5</summary>
		ReminderSent,
		///<summary>6</summary>
		MedicationReconcile,
		///<summary>7</summary>
		SummaryOfCareProvidedToDr,
		///<summary>8</summary>
		TobaccoUseAssessed,
		///<summary>9</summary>
		TobaccoCessation
	}

}