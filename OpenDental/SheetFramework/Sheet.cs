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
	Some of these elements would remain part of the sheet definition, while others would be saved as part of the data for the specific print job.  Therefore, such things as background images and static text would not need to be saved repeatedly with each printout.  But for this to function as a reliable archive, whenever a user "changes" a sheet definition or layout, it must result in the creation of a brand new sheet.  In other words, any sheet that has already been used for any printout is forever locked.  Because of this restriction, our internally defined sheets must be clearly named/numbered.  Every time even the smallest change is made to an internal sheet, it will be assigned a new name/number.  This will trigger the database to archive a copy of the new sheet.  The same will hold true once the user is allowed to copy and customize our supplied sheets.  The extra sheets, the garbage, must be elegantly hidden from the user so they will not be tempted to try to alter them. But until we start saving data, it's OK to alter existing sheets.
	Class names:
	Internal: Sheet, SheetParameter, SheetField.
	Class names for Database tables:
	SheetDef, SheetFieldDef (parameterDefs are hardcoded based on type)
	SheetData, SheetParameterData, SheetFieldData
	
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
		///<Summary>A collection of all parameters for this sheet.  There's usually only one parameter.  The first parameter will be a List int if it's a batch.</Summary>
		public List<SheetParameter> Parameters;
		///<Summary></Summary>
		public List<SheetField> SheetFields;
		///<Summary></Summary>
		public Font Font;
		public int Width;
		public int Height;

		private Font fontDefault=new Font(FontFamily.GenericSansSerif,8.5f);
		///<Summary>For the current sheet in the batch.  Reset for each new sheet in a batch.</Summary>
		private bool heightsCalculated;
		///<Summary>For the current sheet in the batch.  Reset for each new sheet in a batch.</Summary>
		private bool valuesFilled;
		///<Summary>Based off of the first parameter.  The list of primary keys representing sheets in a batch.</Summary>
		private List<int> ParamVals;
		///<Summary>If there is only one "sheet" in the batch, then this will be 0.</Summary>
		private int SheetsPrintedInBatch;

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

		///<Summary>Surround with try/catch. If isBatch, then the first parameter is an array.  This same logic can't be constructed externally, because then the print preview and (future) progress indicator would only show one sheet in the batch at a time.</Summary>
		public void Print(){
			Print(false);
		}

		///<Summary>Surround with try/catch. If isBatch, then the first parameter is an array.  This same logic can't be constructed externally, because then the print preview and (future) progress indicator would only show one sheet in the batch at a time.</Summary>
		public void Print(bool isBatch){
			foreach(SheetParameter param in Parameters){
				if(param.IsRequired && param.ParamValue==null){
					throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
				}
			}
			//could validate field names here later.
			if(isBatch){
				ParamVals=(List<int>)Parameters[0].ParamValue;
			}
			else{
				ParamVals=new List<int>();
				ParamVals.Add((int)Parameters[0].ParamValue);
			}
			valuesFilled=false;
			heightsCalculated=false;
			SheetsPrintedInBatch=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(Width>0 && Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",Width,Height);
			}
			PrintSituation sit=PrintSituation.Default;
			switch(this.SheetType){
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelReferral:
					pd.DefaultPageSettings.Landscape=false;
					sit=PrintSituation.LabelSingle;
					break;
			}
			//later: add a check here for print preview.
			#if DEBUG
				pd.DefaultPageSettings.Margins=new Margins(20,20,0,0);
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,ParamVals.Count);
				printPreview.ShowDialog();
			#else
				try {
					if(!Printers.SetPrinter(pd,sit)) {
						return;
					}
					pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
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
					case "PatNum":
						field.FieldValue=pat.PatNum.ToString();
						break;
					case "dateTime.Today":
						field.FieldValue=DateTime.Today.ToShortDateString();
						break;
					case "birthdate":
						field.FieldValue=Lan.g(this,"BD: ")+pat.Birthdate.ToShortDateString();
						break;
					case "priProvName":
						field.FieldValue=Providers.GetLongDesc(pat.PriProv);
						break;
				}
			}
		}

		private void FillFieldsForLabelCarrier(Carrier carrier) {
			foreach(SheetField field in SheetFields) {
				switch(field.FieldName) {
					case "CarrierName":
						field.FieldValue=carrier.CarrierName;
						break;
					case "address":
						field.FieldValue=carrier.Address;
						if(carrier.Address2!="") {
							field.FieldValue+="\r\n"+carrier.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=carrier.City+", "+carrier.State+" "+carrier.Zip;
						break;
				}
			}
		}

		private void FillFieldsForLabelReferral(Referral refer) {
			foreach(SheetField field in SheetFields) {
				switch(field.FieldName) {
					case "nameLF":
						field.FieldValue=Referrals.GetNameFL(refer.ReferralNum);
						break;
					case "address":
						field.FieldValue=refer.Address;
						if(refer.Address2!="") {
							field.FieldValue+="\r\n"+refer.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=refer.City+", "+refer.ST+" "+refer.Zip;
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
			if(!valuesFilled){
				//get the data, and fill the fields.
				switch(this.SheetType) {
					case SheetTypeEnum.LabelPatient:
						Patient pat=Patients.GetPat((int)GetParamByName("PatNum").ParamValue);
						FillFieldsForLabelPatient(pat);
						break;
					case SheetTypeEnum.LabelCarrier:
						Carrier carrier=Carriers.GetCarrier(ParamVals[SheetsPrintedInBatch]);
						FillFieldsForLabelCarrier(carrier);
						break;
					case SheetTypeEnum.LabelReferral:
						Referral refer=Referrals.GetReferral((int)GetParamByName("ReferralNum").ParamValue);
						FillFieldsForLabelReferral(refer);
						break;
				}
				valuesFilled=true;
			}
			Graphics g=e.Graphics;
			if(!heightsCalculated){
				int oldH;
				foreach(SheetField field in SheetFields) {
					field.ResetHeightAndYPosToOriginal();
				}
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
			if(SheetType==SheetTypeEnum.LabelCarrier
				|| SheetType==SheetTypeEnum.LabelPatient
				|| SheetType==SheetTypeEnum.LabelReferral)
			{
				g.TranslateTransform(100,0);
				g.RotateTransform(90);
			}
			foreach(SheetField field in SheetFields){
				g.DrawString(field.FieldValue,field.Font,Brushes.Black,field.BoundsF);
			}
			g.Dispose();
			//no logic yet for multiple pages on one sheet.
			SheetsPrintedInBatch++;
			valuesFilled=false;
			heightsCalculated=false;
			if(SheetsPrintedInBatch<ParamVals.Count){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
				SheetsPrintedInBatch=0;
			}	
		}

	}

	///<Summary>Different types of sheets that can be used.</Summary>
	public enum SheetTypeEnum{
		///<Summary>0-Requires SheetParameter for PatNum.</Summary>
		LabelPatient,
		///<Summary>1-Requires SheetParameter for PatNum.</Summary>
		LabelCarrier,
		///<Summary>2-Requires SheetParameter for PatNum.</Summary>
		LabelReferral,
		///<Summary>3-Requires SheetParameter for PatNum,ReferralNum.</Summary>
		ReferralSlip
		/*Statement,
		TxPlan,
		Rx,
		LabSlip,
		Postcard,
		RegistrationForm,
		MedHistory,
		ConsentForm*/
	}

	

}
