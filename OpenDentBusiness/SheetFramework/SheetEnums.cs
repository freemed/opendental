using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<Summary>Different types of sheets that can be used.</Summary>
	public enum SheetTypeEnum{
		///<Summary>0-Requires SheetParameter for PatNum.</Summary>
		LabelPatient,
		///<Summary>1-Requires SheetParameter for CarrierNum.</Summary>
		LabelCarrier,
		///<Summary>2-Requires SheetParameter for ReferralNum.</Summary>
		LabelReferral,
		///<Summary>3-Requires SheetParameter for PatNum,ReferralNum.</Summary>
		ReferralSlip,
		///<Summary>4-Requires SheetParameter for AptNum. Does not get saved to db.</Summary>
		LabelAppointment,
		///<Summary>5-Requires SheetParameter for RxNum.</Summary>
		Rx,
		///<summary>6-Requires SheetParameter for PatNum.</summary>
		Consent
		//<Summary>6-Requires SheetParameter for PatNum.</Summary>
		//PatientRegistration
		/*StatementHeader,
		TxPlanHeader,
		LabSlip,
		Postcard,
		ConsentForm*/
	}

	///<summary>For sheetFields</summary>
	public enum GrowthBehaviorEnum {
		///<Summary>Not allowed to grow.  Max size would be Height(generated automatically for now) and Width.</Summary>
		None,
		///<Summary>Can grow down if needed, and will push nearby objects out of the way so that there is no overlap.</Summary>
		DownLocal,
		///<Summary>Can grow down, and will push down all objects on the sheet that are below it.  Mostly used when drawing grids.</Summary>
		DownGlobal
	}

	public enum SheetFieldType {
		///<Summary>Pulled from the database to be printed on the sheet.  Or also possibly just generated at runtime even though not pulled from the database.   User still allowed to change the output text as they are filling out the sheet so that it can different from what was initially generated.</Summary>
		OutputText,
		///<Summary>A blank box that the user is supposed to fill in.</Summary>
		InputField,
		///<Summary>This is text that is defined as part of the sheet and will never change from sheet to sheet.  </Summary>
		StaticText,
		///<summary>Stores a parameter other than the PatNum.  Not meant to be seen on the sheet.  Only used for SheetField, not SheetFieldDef.</summary>
		Parameter,
		///<Summary>Any image of any size, typically a background image for a form.</Summary>
		Image,
		///<summary>One sequence of dots that makes a line.  Continuous without any breaks.  Each time the pen is picked up, it creates a new field row in the database.</summary>
		Drawing,
		///<Summary>A simple line drawn from x,y to x+width,y+height.  So for these types, we must allow width and height to be negative or zero.</Summary>
		Line,
		///<Summary>A simple rectangle outline.</Summary>
		Rectangle,
		///<summary>A clickable area on the screen.  It's a form of input, so treated similarly to an InputField.  The X will go from corner to corner of the rectangle specified.</summary>
		CheckBox,
		///<summary>A signature box, either Topaz pad or directly on the screen with stylus/mouse.  The signature is encrypted based an a hash of other data (yet to be determined).</summary>
		SigBox
		//<summary></summary>
		//RadioButton
		
		//<Summary>Not yet supported.  This might be redundant, and we might use border element instead as the preferred way of drawing a box.</Summary>
		//Box
	}

	public enum SheetInternalType{
		LabelPatientMail,
		LabelPatientLFAddress,
		LabelPatientLFChartNumber,
		LabelPatientLFPatNum,
		LabelPatientRadiograph,
		LabelCarrier,
		LabelReferral,
		ReferralSlip,
		LabelAppointment,
		Rx,
		Consent
		//PatientRegistration
	}

	public enum OutInCheck{
		Out,
		In,
		Check
	}

}
