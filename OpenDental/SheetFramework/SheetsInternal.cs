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
		

	}
}
