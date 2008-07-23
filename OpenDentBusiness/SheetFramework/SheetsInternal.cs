using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness{
	public class SheetsInternal {

		public static SheetDef GetSheetDef(SheetInternalType internalType){
			switch(internalType){
				case SheetInternalType.LabelPatientMail:
					return LabelPatientMail();
				case SheetInternalType.LabelPatientLFAddress:
					return LabelPatientLFAddress();
				case SheetInternalType.LabelPatientLFChartNumber:
					return LabelPatientLFChartNumber();
				case SheetInternalType.LabelPatientLFPatNum:
					return LabelPatientLFPatNum();
				case SheetInternalType.LabelPatientRadiograph:
					return LabelPatientRadiograph();
				case SheetInternalType.LabelCarrier:
					return LabelCarrier();
				case SheetInternalType.LabelReferral:
					return LabelReferral();
				case SheetInternalType.ReferralSlip:
					return ReferralSlip();
				case SheetInternalType.LabelAppointment:
					return LabelAppointment();
				default:
					throw new ApplicationException("Invalid SheetInternalType.");
			}
		}

		public static List<SheetDef> GetAllInternal(){
			List<SheetDef> list=new List<SheetDef>();
			for(int i=0;i<Enum.GetValues(typeof(SheetInternalType)).Length;i++){
				list.Add(GetSheetDef((SheetInternalType)i));
			}
			return list;
		}
		
		private static SheetDef LabelPatientMail(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			//Font font=new Font(sheet.FontName,sheet.FontSize);
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFAddress() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFChartNumber(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("ChartNumber",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFPatNum() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PatNum",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientRadiograph(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,150,rowH,GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "dateTime.Today",sheet.FontSize,sheet.FontName,false,180,yPos,100,rowH,GrowthBehaviorEnum.None));
			yPos += rowH;
			//smallfont:
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "birthdate",9,sheet.FontName,false,25,yPos,105,rowH, GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "priProvName",9,sheet.FontName,false,130,yPos,150,rowH, GrowthBehaviorEnum.None));
			return sheet;
		}

		private static SheetDef LabelCarrier(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelCarrier);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("CarrierName",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelReferral(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelReferral);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef ReferralSlip(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.ReferralSlip);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=9f;
			sheet.Width=450;
			sheet.Height=650;
			int rowH=14;
			int yPos=50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referral to",10,sheet.FontName,true,170,yPos,200,19));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.nameFL",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.address",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.cityStateZip",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH));
			yPos+=rowH+30;
			//Patient--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Patient",9,sheet.FontName,true,25,yPos,100,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,200,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("dateTime.Today",sheet.FontSize,sheet.FontName,false,300, yPos,100,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Work:",sheet.FontSize,sheet.FontName,false,25,yPos,38,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WkPhone",sheet.FontSize,sheet.FontName,false,63,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Home:",sheet.FontSize,sheet.FontName,false,25,yPos,42,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.HmPhone",sheet.FontSize,sheet.FontName,false,67,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Wireless:",sheet.FontSize,sheet.FontName,false,25,yPos,58,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WirelessPhone",sheet.FontSize,sheet.FontName,false,83,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH+30;
			//Provider--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referred by",9,sheet.FontName,true,25,yPos,300,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.provider",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH+20;
			//Notes--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Notes",9,sheet.FontName,true,25,yPos,300,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("notes",sheet.FontSize,sheet.FontName,false,25,yPos,400,275));
			return sheet;
		}

		private static SheetDef LabelAppointment() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelAppointment);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=15;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Your appointment is scheduled for:",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("weekdayDateTime",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Appointment length:",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("length",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}
		

	}
}
