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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"nameFL",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "nameLF", 25, yPos, 300, sheet.Font, GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"ChartNumber",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "nameLF", 25, yPos, 300, sheet.Font, GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"PatNum",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "nameLF", 25, yPos, 150, sheet.Font, GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "dateTime.Today", 180, yPos, 100, sheet.Font, GrowthBehaviorEnum.None));
				yPos += rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "birthdate", 25, yPos, 105, smallFont, GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText, "priProvName", 130, yPos, 150, smallFont, GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"CarrierName",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
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
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet ReferralSlip {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.ReferralSlip);
				sheet.Font=new Font(FontFamily.GenericSansSerif,9f);
				Font fontHeading=new Font(FontFamily.GenericSansSerif,10,FontStyle.Bold);
				Font fontBold=new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold);
				sheet.Width=450;
				sheet.Height=650;
				int rowH=sheet.Font.Height;
				int yPos=50;
				sheet.SheetFields.Add(new SheetField("Referral to",175,yPos,300,fontHeading,GrowthBehaviorEnum.None));
				yPos+=rowH+15;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"referral.nameLF",200,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"referral.address",200,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"referral.cityStateZip",200,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				//Patient--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(new SheetField("Patient",25,yPos,300,fontBold,GrowthBehaviorEnum.None));
				yPos+=rowH+5;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"dateTime.Today",25, yPos,110,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField("Work:",25,yPos,50,sheet.Font,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.WkPhone",75,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField("Home:",25,yPos,50,sheet.Font,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.HmPhone",175,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField("Wireless:",25,yPos,70,sheet.Font,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.WirelessPhone",95,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH+20;
				//Patient--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(new SheetField("Referred by",25,yPos,300,fontBold,GrowthBehaviorEnum.None));
				yPos+=rowH+5;
				sheet.SheetFields.Add(new SheetField(SheetFieldType.OutputText,"patient.provider",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH+20;
				//Notes--------------------------------------------------------------------------------------------
				sheet.SheetFields.Add(new SheetField("Notes",25,yPos,300,fontBold,GrowthBehaviorEnum.None));
				yPos+=rowH+5;
				sheet.SheetFields.Add(new SheetField("notes",25,yPos,400,300,sheet.Font));
				return sheet;
			}
		}
		

	}
}
