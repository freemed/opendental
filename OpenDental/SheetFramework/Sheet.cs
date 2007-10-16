using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental{
	/*A better name for this object would be a Form, but that name is obviously too ambiguous and has been overused.  This internal framework will later be extended to let users customize sheets.  There are two different aspects of the future database tables:
	1. Customization of sheets
	2. Saving data filled in on sheets.
	#2 might very well come before #1, as it would allow archiving many printed documents.
	Sheets will not include reports, which are better handled by the RDL framework.  Examples of what sheets might be used for include statements, tx plans, rx, lab slips, postcards, referral slips, patient registration forms, medical histories, consent forms, and labels.
	The interesting thing about this framework is that it should be able to support incoming data as well as outgoing data using the following elements:
	-background image
	-static text
	-text generated from database
	-user input
	Some of these elements would remain part of the sheet definition, while others would be saved as part of the data for the specific print job.  Therefore, such things as background images and static text would not need to be saved repeatedly with each printout.  But for this to function as a reliable archive, whenever a user "changes" a sheet definition or layout, it must result in the creation of a brand new sheet.  In other words, and sheet that has already been used for any printout is forever locked.  Because of this restriction, we will not be altering our internally defined sheets.  The internally defined sheets must instead be numbered, and with each "change", a brand new sheet created.  The same will hold true once the user is allowed to copy and customize our supplied sheets.  The extra sheets, the garbage, must be elegantly hidden from the user so they will not be tempted to try to alter it.
	Possible future class names:
	Internal: Sheet, SheetField(input or output. this is what gets saved as data), SheetObject(static text, lines, images, boxes, etc.)
	Custom: SheetDef, SheetFieldDef, SheetObjectDef
	Data for both: SheetData, SheetFieldData
	
	Note that we have tried to do similar things before, but not with as much clarity and organization.  See the ReportingOld2 folder for an example of a similar framework that never took off because:
	a) It was overwhelming because it was trying to handle 'reporting' functions as its main purpose.
	b) It did not start with a simpler framework and build iteratively.
	c) It was modeled after Crystal Reports, which was only designed for reports, not forms.
	*/
	///<summary>See comments in the code file above.  Nobody is EVER allowed to alter an internal sheet which has already been released into production.  Instead, more sheets are added.</summary>
	class Sheet {
		///<Summary>Every single sheet must have a type, and only the types listed in the enum will be supported as part of the Sheet framework.</Summary>
		public EnumSheetType SheetType;


	}

	///<Summary>Different types of sheets that can be used.</Summary>
	public enum EnumSheetType{
		///<Summary>0</Summary>
		Label
		/*Statement,
		TxPlan,
		Rx,
		LabSlip,
		Postcard,
		ReferralSlip,
		RegistrationForm,
		MedHistory,
		ConsentForm*/
	}

}
