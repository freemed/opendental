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
	Sheets will not include reports, which are better handled by the RDL framework.  Examples of what sheets might be used for include statements, tx plans, rx, lab slips, postcards, referral slips, patient registration forms, medical histories, consent forms, and labels.
	The interesting thing about this framework is that it should be able to support incoming data as well as outgoing data using the following elements:
	-background image
	-static text
	-text generated from database
	-user input
	Some of these elements would remain part of the sheet definition, while others would be saved as part of the data for the specific print job.  Therefore, such things as background images and static text would not need to be saved repeatedly with each printout.  But for this to function as a reliable archive, whenever a user "changes" a sheet definition or layout, it must result in the creation of a brand new sheet.  In other words, and sheet that has already been used for any printout is forever locked.  Because of this restriction, we will not be altering our internally defined sheets.  The internally defined sheets must instead be numbered, and with each "change", a brand new sheet created.  The same will hold true once the user is allowed to copy and customize our supplied sheets.  The extra sheets, the garbage, must be elegantly hidden from the user so they will not be tempted to try to alter it. But until we start saving data, it's OK to alter existing sheets.
	Possible future class names:
	Internal: Sheet, SheetParameter, SheetField(input or output. this is what gets saved as data), SheetObject(static text, lines, images, boxes, etc.)
	Custom: SheetDef, SheetParameterDef, SheetFieldDef, SheetObjectDef
	Data for both: SheetData, SheetParameterData, SheetFieldData
	
	Note that we have tried to do similar things before, but not with as much clarity and organization.  See the ReportingOld2 folder for an example of a similar framework that never took off because:
	a) It was overwhelming because it was trying to handle 'reporting' functions as its main purpose.
	b) It did not start with a simpler framework and build iteratively.
	c) It was modeled after Crystal Reports, which was only designed for reports, not forms.
	d) We did not have generics.
	*/
	///<summary>See comments in the code file above.</summary>
	class Sheet {
		///<Summary>Every single sheet must have a type, and only the types listed in the enum will be supported as part of the Sheet framework.</Summary>
		public SheetTypeEnum SheetType;
		///<Summary>A collection of all parameters for this sheet.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		public List<SheetField> SheetFields;
		///<Summary></Summary>
		public Font Font;
		private Font fontDefault=new Font(FontFamily.GenericSansSerif,8.5f);
		private bool heightsCalculated;

		public Sheet(SheetTypeEnum sheetType){
			SheetType=sheetType;
			Parameters=SheetParameter.GetForType(sheetType);
			SheetFields=new List<SheetField>();
			Font=fontDefault;
		}

		private SheetParameter GetParamByName(string paramName){
			foreach(SheetParameter param in Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		public void SetParameter(string paramName,object paramValue){
			SheetParameter param=GetParamByName(paramName);
			if(param==null){
				throw new ApplicationException(Lan.g("Sheet","Parameter not found: ")+paramName);
			}
			param.ParamValue=paramValue;
		}

		///<Summary>Surround with try/catch.</Summary>
		public void Print(){
			foreach(SheetField field in SheetFields) {
				if(field.Font==null) {
					field.Font=Font;
				}
				if(field.Height==0) {
					field.Height=field.Font.Height;
				}
			}
			foreach(SheetParameter param in Parameters){
				if(param.IsRequired && param.ParamValue==null){
					throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
				}
			}
			//could validate field names here later.
			//get the data, and fill the fields.
			switch(this.SheetType){
				case SheetTypeEnum.LabelPatient:
					Patient pat=Patients.GetPat((int)GetParamByName("PatNum").ParamValue);
					FillFieldsForLabelPatient(pat);
					break;
			}
			heightsCalculated=false;	
			PrintDocument pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			PrintSituation sit=PrintSituation.Default;
			switch(this.SheetType){
				case SheetTypeEnum.LabelPatient:
					sit=PrintSituation.LabelSingle;
					break;
			}
			if(!Printers.SetPrinter(pd,sit)) {
				return;
			}
			#if DEBUG
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,1);
				printPreview.ShowDialog();
			#else
				try {
					pd.Print();
				}
				catch(Exception ex){
					throw ex;
					//MessageBox.Show(Lan.g("Sheet","Printer not available"));
				}
			#endif
		}

		private void FillFieldsForLabelPatient(Patient pat){
			foreach(SheetField field in SheetFields){
				switch(field.FieldName){
					case "nameFL":
						field.FieldValue=pat.GetNameFLFormal();
						break;
					case "nameLF":
						field.FieldValue=pat.GetNameLF();
						break;
					case "address":
						field.FieldValue=pat.Address;
						if(pat.Address2!=""){
							field.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "ChartNumber":
						field.FieldValue=pat.ChartNumber;
						break;
				}
			}
		}

		///<Summary>Supply the field that we are testing.  All other fields which intersect with it will be moved down.  Each time one is moved down, this method is called recursively.  The end result should be no intersections among fields near to the original field that grew.</Summary>
		private void MoveAllDownWhichIntersect(SheetField field){
			foreach(SheetField field2 in SheetFields) {
				if(field2==field){
					continue;
				}
				if(field2.YPos<field.YPos){//only fields where are below this one
					continue;
				}
				if(field.Bounds.IntersectsWith(field2.Bounds)) {
					field2.YPos=field.Bounds.Bottom;
					MoveAllDownWhichIntersect(field2);
				}
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			if(!heightsCalculated){
				int oldH;
				foreach(SheetField field in SheetFields) {
					oldH=field.Height;
					field.Height=(int)g.MeasureString(field.FieldValue,field.Font).Height;
					if(field.Height>oldH){
						if(field.GrowthBehavior==GrowthBehaviorEnum.DownLocal){
							MoveAllDownWhichIntersect(field);
						}
						else if(field.GrowthBehavior==GrowthBehaviorEnum.DownGlobal){
							int amountOfGrowth=field.Height-oldH;
							foreach(SheetField field2 in SheetFields) {
								if(field2.YPos>field.YPos) {//for all fields that are below this one
									field2.YPos+=amountOfGrowth;//bump down by amount that this one grew
								}
							}
						}
					}
				}
				heightsCalculated=true;
			}
			g.TranslateTransform(100,0);
			g.RotateTransform(90);
			foreach(SheetField field in SheetFields){
				g.DrawString(field.FieldValue,field.Font,Brushes.Black,field.BoundsF);
			}
			g.Dispose();
			//no logic yet for multiple pages
			//e.HasMorePages=true;
		}



	}

	///<Summary>Different types of sheets that can be used.</Summary>
	public enum SheetTypeEnum{
		///<Summary>0-Requires SheetParameter for PatNum.</Summary>
		LabelPatient,
		LabelCarrier,
		LabelReferral
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
