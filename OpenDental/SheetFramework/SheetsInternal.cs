using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDental{
	partial class SheetsInternal {
		
		public static Sheet LabelPatientMail{
			get{
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameFL",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFAddress {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFChartNumber {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=30;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"ChartNumber",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelPatientLFPatNum {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=30;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"PatNum",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelPatientRadiograph {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelPatient);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				Font smallFont=new Font(FontFamily.GenericSansSerif, 9);
				int rowH=sheet.Font.Height;
				int yPos=30;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameLF",10,yPos,150,sheet.Font,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"dateTime.Today",165,yPos,100,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"birthdate",10,yPos,105,smallFont,GrowthBehaviorEnum.None));
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"priProvName",115,yPos,150,smallFont,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelCarrier {
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelCarrier);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"CarrierName",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}

		public static Sheet LabelReferral{
			get {
				Sheet sheet=new Sheet(SheetTypeEnum.LabelReferral);
				sheet.Font=new Font(FontFamily.GenericSansSerif,12f);
				int rowH=sheet.Font.Height;
				int yPos=10;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"nameLF",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"address",25,yPos,300,sheet.Font,GrowthBehaviorEnum.DownLocal));
				yPos+=rowH;
				sheet.SheetFields.Add(new SheetField(InOutEnum.Out,"cityStateZip",25,yPos,300,sheet.Font,GrowthBehaviorEnum.None));
				return sheet;
			}
		}
		

	}
}
