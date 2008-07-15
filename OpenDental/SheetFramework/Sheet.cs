using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	/*A better name for this object would be a Form, but that name is obviously too ambiguous and has been overused.  This internal framework will later be extended to let users customize sheets.  There are two different aspects of the future database tables:
	1. Customization of sheets
	2. Saving data filled in on sheets.
	#2 might very well come before #1, as it would allow archiving many printed documents.
	Sheets will not include reports, which are better handled by the RDL framework or something even simpler.  Examples of what sheets might be used for include statements, tx plans, rx, lab slips, postcards, referral slips, patient registration forms, medical histories, consent forms, and labels.
	The interesting thing about this framework is that it should be able to support incoming data as well as outgoing data using the following elements:
	-background image
	-static text
	-text generated from database
	-user input
	Some of these elements would remain part of the sheet definition, while others would be saved as part of the data for the specific print job.  Therefore, such things as background images and static text would not need to be saved repeatedly with each printout.  But for this to function as a reliable archive, whenever a user "changes" a sheet definition or layout, it must result in the creation of a brand new sheetdef.  In other words, any sheetdef that has already been used for any printout is forever locked.  Because of this restriction, our internally defined sheetdefs must be clearly named/numbered.  Every time even the smallest change is made to an internal sheet, it will be assigned a new name/number.  This will trigger the database to archive a copy of the new sheet.  The same will hold true once the user is allowed to copy and customize our supplied sheets.  The extra sheets, the garbage, must be elegantly hidden from the user so they will not be tempted to try to alter them. But until we start saving data, it's OK to alter existing sheets.  And some sheets like labels never get saved, so it's ok to alter those internal defs.
	Class names:
	Internal: Sheet, SheetParameter, SheetField.
	Class names for Database tables:
	SheetDef, SheetFieldDef (parameterDefs are hardcoded based on type)
	SheetData, SheetFieldData(won't need parameterData, because all saved sheets will have patnum.  Other parameters will be saved as adapted SheetFieldData)
	
	Note that we have tried to do similar things before, but not with as much clarity and organization.  See the ReportingOld2 folder for an example of a similar framework that never took off because:
	a) It was overwhelming because it was trying to handle 'reporting' functions as its main purpose.
	b) It did not start with a simpler framework and build iteratively.
	c) It was modeled after Crystal Reports, which was only designed for reports, not forms.
	d) We did not have generics.
	*/
	///<summary>See comments in the code file above.</summary>
	public class Sheet {
		///<Summary>Every single sheet must have a type, and only the types listed in the enum will be supported as part of the Sheet framework.</Summary>
		public SheetTypeEnum SheetType;
		///<Summary>A collection of all parameters for this sheet.  There's usually only one parameter.  The first parameter will be a List int if it's a batch.  If a sheet has already been filled, saved to the database, and printed, then there is no longer any need for the parameters in order to fill the data.  So a retrieved sheet will have no parameters, signalling a skip in the fill phase.  There will still be parameters tucked away in the Field data in the database, but they won't become part of the sheet.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		public List<SheetField> SheetFields;
		///<Summary></Summary>
		public Font Font;
		public int Width;
		public int Height;

		private Font fontDefault=new Font(FontFamily.GenericSansSerif,8.5f);

		public Sheet(SheetTypeEnum sheetType){
			SheetType=sheetType;
			Parameters=SheetParameter.GetForType(sheetType);
			SheetFields=new List<SheetField>();
			Font=fontDefault;
		}

		public Sheet Copy(){
			Sheet sheet=(Sheet)this.MemberwiseClone();
			//do I need to copy the lists?
			return sheet;
		}

		public void SetParameter(string paramName,object paramValue){
			SheetParameter param=GetParamByName(paramName);
			if(param==null){
				throw new ApplicationException(Lan.g("Sheet","Parameter not found: ")+paramName);
			}
			param.ParamValue=paramValue;
		}

		private SheetParameter GetParamByName(string paramName){
			foreach(SheetParameter param in Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		

	}

	

	

}
