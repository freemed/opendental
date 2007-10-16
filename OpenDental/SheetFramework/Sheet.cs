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
			try {
				pd.Print();
			}
			catch(Exception ex){
				throw ex;
				//MessageBox.Show(Lan.g("Sheet","Printer not available"));
			}
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
				}
			}
		}

		///<Summary>Supply the field that we are testing.  All other fields which intersect with it will be moved down.  Each time one is moved down, this method is called recursively.  The end result should be no intersections among fields near to the original field that grew.</Summary>
		private void MoveAllDownWhichIntersect(SheetField field){
			foreach(SheetField field2 in SheetFields) {
				if(field2==field){
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

	///<Summary></Summary>
	class SheetParameter{
		///<Summary></Summary>
		public bool IsRequired;
		///<Summary>Usually, a columnName.</Summary>
		public string ParamName;
		///<Summary>This is the value which must be set in order to obtain data from the database. It is usually an int primary key.</Summary>
		public object ParamValue;

		public SheetParameter(bool isRequired,string paramName){
			IsRequired=isRequired;
			ParamName=paramName;
		}

		///<Summary>Every sheet has at least one required parameter, usually the primary key of an imporant table.</Summary>
		public static List<SheetParameter> GetForType(SheetTypeEnum sheetType){
			List<SheetParameter> list=new List<SheetParameter>();
			if(sheetType==SheetTypeEnum.LabelPatient){
				list.Add(new SheetParameter(true,"PatNum"));
			}
			return list;
		}
	}

	///<Summary></Summary>
	class SheetField{
		///<Summary>An Out field is pulled from the database to be printed on the sheet.  An In field (not supported yet) is for user input.</Summary>
		public InOutEnum InOut;
		///<Summary>Each sheet typically has a main datatable type.  For Out types, FieldName is usually the string representation of the database column for the main table.  For other tables, it can be of the form table.Column.  There may also be extra fields available that are not strictly pulled from the database.  Extra fields will start with lowercase to indicate that they are not pure database fields.  The list of available fields for each type in SheetFieldsAvailable.  Users could pick from that list.  Likewise, In types are internally tied to actions to persist the data.  So they are also hard coded and are available in SheetFieldsAvailable.</Summary>
		public string FieldName;
		///<Summary>Overrides sheet font.</Summary>
		public Font Font;
		///<Summary>In pixels.</Summary>
		public int XPos;
		///<Summary>In pixels.</Summary>
		public int YPos;
		///<Summary>The field will be constrained horizontally to this size.  Not allowed to be zero.</Summary>
		public int Width;
		///<Summary>The Sheet constructor makes sure that if this is 0, then it will default to the size dictated by the font.  Once we build a sheet designer, the designer will handle the default size.  So it's not allowed to be zero so that it will be visible on the designer.</Summary>
		public int Height;
		///<Summary></Summary>
		public GrowthBehaviorEnum GrowthBehavior;
		///<Summary>For Out types, this value is set during printing.  This is the data obtained from the database and ready to print.</Summary>
		public string FieldValue;

		public SheetField(InOutEnum inOut,string fieldName){
			InOut=inOut;
			FieldName=fieldName;
		}

		public SheetField(InOutEnum inOut,string fieldName,int xPos,int yPos,int width,Font font,GrowthBehaviorEnum growthBehavior){
			InOut=inOut;
			FieldName=fieldName;
			Font=font;
			XPos=xPos;
			YPos=yPos;
			Width=width;
			Height=font.Height;
			GrowthBehavior=growthBehavior;
		}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public Rectangle Bounds{
			get{
				return new Rectangle(XPos,YPos,Width,Height);
			}
		}

		///<Summary>Should only be called after FieldValue has been set, due to GrowthBehavior.</Summary>
		public RectangleF BoundsF {
			get {
				return new RectangleF(XPos,YPos,Width,Height);
			}
		}
	}

	class SheetObject{

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

	public enum InOutEnum{
		In,
		Out
	}

	public enum GrowthBehaviorEnum{
		///<Summary>Not allowed to grow.  Max size would be height of one row of text, and Width.</Summary>
		None,
		///<Summary>Can grow down if needed, and will push nearby objects out of the way so that there is no overlap.</Summary>
		DownLocal,
		///<Summary>Can grow down, and will push down all objects on the sheet that are below it.  Mostly used when drawing grids.</Summary>
		DownGlobal
	}

}
