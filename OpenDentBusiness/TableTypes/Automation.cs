using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>A trigger event causes one or more actions.</summary>
	[Serializable()]
	public class Automation:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AutomationNum;
		///<summary>.</summary>
		public string Description;
		///<summary>Enum:AutomationTrigger What triggers this automation</summary>
		public AutomationTrigger Autotrigger;
		///<summary>If this has a CompleteProcedure trigger, this is a comma-delimited list of codes that will trigger the action.</summary>
		public string ProcCodes;
		///<summary>The action taken as a result of the trigger.  To get more than one action, create multiple automation entries.</summary>
		public AutomationAction AutoAction;
		///<summary>FK to sheetdef.SheetDefNum.  If the action is to print a sheet, then this tells which sheet to print.  So it must be a custom sheet.  Also, not that this organization does not allow passing parameters to the sheet such as which procedures were completed, or which appt was broken.</summary>
		public long SheetDefNum;
		///<summary>FK to definition.DefNum. Only used if action is CreateCommlog.</summary>
		public long CommType;
		///<summary>If a commlog action, then this is the text that goes in the commlog.  If this is a ShowStatementNoteBold action, then this is the NoteBold. Might later be expanded to work with email or to use variables.</summary>
		public string MessageContent;

		public Automation Copy() {
			return (Automation)MemberwiseClone();
		}

	}



	///<summary></summary>
	public enum AutomationTrigger {
		///<summary></summary>
		CompleteProcedure,
		///<summary></summary>
		BreakAppointment,
		///<summary></summary>
		CreateApptNewPat,
		///<summary>Regardless of module.  Usually only used with conditions.</summary>
		OpenPatient
		//<summary>Either a single statement or as part of the billing process.  Either print or </summary>
		//CreateStatement
	}

	///<summary></summary>
	public enum AutomationAction {
		///<summary></summary>
		PrintPatientLetter,
		///<summary></summary>
		CreateCommlog,
		///<summary>If a referral does not exist for this patient, then notify user instead.</summary>
		PrintReferralLetter,
		///<summary></summary>
		ShowExamSheet,
		///<summary></summary>
		PopUp
		//<summary></summary>
		//AddStatementNoteBold
	}
	


}









