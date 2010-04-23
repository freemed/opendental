using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDentBusiness{
/*A better name for this object would be a Form, but that name is obviously too ambiguous and has been overused.  There are two different aspects of the database tables:
	1. Customization of sheets.
	2. Saving data filled in on sheets.
	Sheets do not include reports, which are better handled by the RDL framework or something even simpler.  Examples of what sheets might be used for include statements, tx plans, rx, lab slips, postcards, referral slips, patient registration forms, medical histories, consent forms, and labels.
	The interesting thing about this framework is that it is able to support incoming data as well as outgoing data using the following elements:
	-background image
	-static text
	-text generated from database
	-user input
	Class names:
	Data: Sheet, SheetField. (Parameters are saved as part of fields, except PatNum is part of Sheet)
	Defs:	SheetDef, SheetFieldDef (SheetParameters are hardcoded based on type)
  SheetImage (handling this with files for now)
	
	Note that we have tried to do similar things before, but not with as much clarity and organization.  See the ReportingOld2 folder for an example of a similar framework that never took off because:
	a) It was overwhelming because it was trying to handle 'reporting' functions as its main purpose.
	b) It did not start with a simpler framework and build iteratively.
	c) It was modeled after Crystal Reports, which was only designed for reports, not forms.
	d) We did not have generics.
	*/
	///<summary>One sheet for one patient.</summary>
	[Serializable()]
	public class Sheet:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SheetNum;
		///<summary>Enum:SheetTypeEnum</summary>
		public SheetTypeEnum SheetType;
		///<summary>FK to patient.PatNum.  A saved sheet is always attached to a patient.  There are a few sheets that are so minor that they don't get saved, such as a Carrier label.</summary>
		public long PatNum;
		///<summary>The date and time of the sheet as it will be displayed in the commlog.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.DateT)]
		public DateTime DateTimeSheet;
		///<summary>The default fontSize for the sheet.  The actual font must still be saved with each sheetField.</summary>
		public float FontSize;
		///<summary>The default fontName for the sheet.  The actual font must still be saved with each sheetField.</summary>
		public string FontName;
		///<summary>Width of the sheet in pixels, 100 pixels per inch.</summary>
		public int Width;
		///<summary>Height of the sheet in pixels, 100 pixels per inch.</summary>
		public int Height;
		///<summary></summary>
		public bool IsLandscape;
		///<summary>An internal note for the use of the office staff regarding the sheet.  Not to be printed on the sheet in any way.</summary>
		public string InternalNote;
		///<summary>Copied from the SheetDef description.</summary>
		public string Description;
		///<summary>The order that this sheet will show in the patient terminal for the patient to fill out.  Or zero if not set.</summary>
		public byte ShowInTerminal;
		
		public Sheet Copy(){
			return (Sheet)this.MemberwiseClone();
		}	

		///<Summary>A collection of all parameters for this sheetdef.  There's usually only one parameter.  The first parameter will be a List long if it's a batch.  If a sheet has already been filled, saved to the database, and printed, then there is no longer any need for the parameters in order to fill the data.  So a retrieved sheet will have no parameters, signalling a skip in the fill phase.  There will still be parameters tucked away in the Field data in the database, but they won't become part of the sheet.</Summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<SheetField> SheetFields;	











	}
}


