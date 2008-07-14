using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenDentBusiness;


namespace OpenDental{
	partial class SheetsInternal {
		
		public static Sheet LabelPatientMail{
			get{
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(SheetField.NewOutput("nameFL",25,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFAddress {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(SheetField.NewOutput("nameLF",25,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFChartNumber {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=30;
				if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
				sheet.SheetFields.Add(SheetField.NewOutput("nameLF", 25, yPos, 300, sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("ChartNumber",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFPatNum {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=30;
				if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
				sheet.SheetFields.Add(SheetField.NewOutput("nameLF", 25, yPos, 300, sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("PatNum",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet LabelPatientRadiograph {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				Font smallFont=new Font(FontFamily.GenericSansSerif, 9);
				int rowH=sheet.Font.Height;
				int yPos=30;
				if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
				sheet.SheetFields.Add(SheetField.NewOutput( "nameLF", 25, yPos, 150, sheet.Font, GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(SheetField.NewOutput( "dateTime.Today", 180, yPos, 100, sheet.Font, GrowthBehaviorEnum.None));
				yPos += rowH;
				sheet.SheetFields.Add(SheetField.NewOutput( "birthdate", 25, yPos, 105, smallFont, GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(SheetField.NewOutput( "priProvName", 130, yPos, 150, smallFont, GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelCarrier {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelCarrier);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(SheetField.NewOutput("CarrierName",25,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet LabelReferral{
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelReferral);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				sheet.Width=108;
				sheet.Height=346;
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(SheetField.NewOutput("nameFL",25,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("cityStateZip",25,yPos,300,sheet.Font));
				return sheet;
			}
		}

		public static Sheet ReferralSlip {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.ReferralSlip);
				sheet.Font=new Font("Microsoft Sans Serif",9f);
				Font fontHeading=new Font("Microsoft Sans Serif",10,FontStyle.Bold);
				Font fontBold=new Font("Microsoft Sans Serif",9,FontStyle.Bold);
				sheet.Width=450;
				sheet.Height=650;
				int rowH=sheet.Font.Height+1;
				int yPos=50;
				sheet.SheetFields.Add(SheetField.NewStaticText("Referral to",170,yPos,300,fontHeading));
				yPos+=rowH+5;
				sheet.SheetFields.Add(SheetField.NewOutput("referral.nameFL",150,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("referral.address",150,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("referral.cityStateZip",150,yPos,300,sheet.Font));
				yPos+=rowH+30;
				//Patient--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(SheetField.NewStaticText("Patient",25,yPos,100,fontBold));
				yPos+=rowH+5;
				sheet.SheetFields.Add(SheetField.NewOutput("patient.nameFL",25,yPos,200,sheet.Font));
				sheet.SheetFields.Add(SheetField.NewOutput("dateTime.Today",350, yPos,100,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewStaticText("Work:",25,yPos,38,sheet.Font));
				sheet.SheetFields.Add(SheetField.NewOutput("patient.WkPhone",63,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewStaticText("Home:",25,yPos,42,sheet.Font));
				sheet.SheetFields.Add(SheetField.NewOutput("patient.HmPhone",67,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewStaticText("Wireless:",25,yPos,58,sheet.Font));
				sheet.SheetFields.Add(SheetField.NewOutput("patient.WirelessPhone",83,yPos,300,sheet.Font));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("patient.address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(SheetField.NewOutput("patient.cityStateZip",25,yPos,300,sheet.Font));
				yPos+=rowH+30;
				//Provider--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(SheetField.NewStaticText("Referred by",25,yPos,300,fontBold));
				yPos+=rowH+5;
				sheet.SheetFields.Add(SheetField.NewOutput("patient.provider",25,yPos,300,sheet.Font));
				yPos+=rowH+20;
				//Notes--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(SheetField.NewStaticText("Notes",25,yPos,300,fontBold));
				yPos+=rowH+5;
				sheet.SheetFields.Add(SheetField.NewInput("notes",25,yPos,400,275,sheet.Font));
				return sheet;
			}
		}
		

	}
}
