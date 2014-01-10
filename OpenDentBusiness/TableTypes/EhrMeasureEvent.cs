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
		///<summary>Enum:EhrMeasureEventType .</summary>
		public EhrMeasureEventType EventType;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>Only used for some types: EducationProvided, TobaccoCessation, TobaccoUseAssessed.</summary>
		public string MoreInfo;
		///<summary>The code for this event.  Example: TobaccoUseAssessed can be one of three LOINC codes: 11366-2 History of tobacco use Narrative, 68535-4 Have you used tobacco in the last 30 days, and 68536-2 Have you used smokeless tobacco product in the last 30 days.</summary>
		public string CodeValueEvent;
		///<summary>The code system name for the event code.  Examples: LOINC, SNOMEDCT.</summary>
		public string CodeSystemEvent;
		///<summary>The code for this event result.  Example: A TobaccoUseAssessed event type could result in a finding of SNOMED code 8517006 - Ex-smoker (finding).  There are 54 allowed tobacco user/non-user codes, and the user is allowed to select from any SNOMED code if they wish, for a TobaccoUseAssessed event.</summary>
		public string CodeValueResult;
		///<summary>The code system for this event result.  Example: SNOMEDCT, </summary>
		public string CodeSystemResult;
		///<summary>A foreign key to a table associated with the EventType.  0 indicates not in use.  Used to properly count denominators for specific measure types.</summary>
		public long FKey;


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
		TobaccoCessation,
		///<summary>10</summary>
		CurrentMedsDocumented
	}

}