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
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",25,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef LabelPatientLFAddress() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",25,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef LabelPatientLFChartNumber(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF", 25, yPos, 300, sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("ChartNumber",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef LabelPatientLFPatNum() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF", 25, yPos, 300, sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PatNum",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef LabelPatientRadiograph(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			Font smallFont=new Font(FontFamily.GenericSansSerif, 9);
			int rowH=sheet.Font.Height;
			int yPos=30;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "nameLF", 25, yPos, 150, sheet.Font, GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "dateTime.Today", 180, yPos, 100, sheet.Font, GrowthBehaviorEnum.None));
			yPos += rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "birthdate", 25, yPos, 105, smallFont, GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "priProvName", 130, yPos, 150, smallFont, GrowthBehaviorEnum.None));
			return sheet;
		}

		private static SheetDef LabelCarrier(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelCarrier);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("CarrierName",25,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef LabelReferral(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelReferral);
			sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
			sheet.Width=108;
			sheet.Height=346;
			int rowH=sheet.Font.Height;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",25,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
			return sheet;
		}

		private static SheetDef ReferralSlip(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.ReferralSlip);
			sheet.Font=new Font("Microsoft Sans Serif",9f);
			Font fontHeading=new Font("Microsoft Sans Serif",10,FontStyle.Bold);
			Font fontBold=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
			sheet.Width=450;
			sheet.Height=650;
			int rowH=sheet.Font.Height+1;
			int yPos=50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referral to",170,yPos,300,fontHeading));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.nameFL",150,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.address",150,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.cityStateZip",150,yPos,300,sheet.Font));
			yPos+=rowH+30;
			//Patient--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Patient",25,yPos,100,fontBold));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",25,yPos,200,sheet.Font));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("dateTime.Today",350, yPos,100,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Work:",25,yPos,38,sheet.Font));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WkPhone",63,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Home:",25,yPos,42,sheet.Font));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.HmPhone",67,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Wireless:",25,yPos,58,sheet.Font));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WirelessPhone",83,yPos,300,sheet.Font));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.cityStateZip",25,yPos,300,sheet.Font));
			yPos+=rowH+30;
			//Provider--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referred by",25,yPos,300,fontBold));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.provider",25,yPos,300,sheet.Font));
			yPos+=rowH+20;
			//Notes--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Notes",25,yPos,300,fontBold));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("notes",25,yPos,400,275,sheet.Font));
			return sheet;
		}
		

	}
}
