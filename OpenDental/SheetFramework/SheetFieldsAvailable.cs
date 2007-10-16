using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental{
	class SheetFieldsAvailable {
		///<Summary>This isn't actually used yet.  It's just a handy place to store the lists.</Summary>
		public static List<SheetField> GetList(InOutEnum inOut,SheetTypeEnum sheetType){
			if(inOut==InOutEnum.In){
				throw new ApplicationException("In types not supported yet.");
			}
			switch(sheetType){
				case SheetTypeEnum.LabelPatient:
					return OutLabelPatient();
			}
			return new List<SheetField>();
		}

		private static List<SheetField> OutLabelPatient(){
			List<SheetField> list=new List<SheetField>();
			list.Add(new SheetField(InOutEnum.Out,"nameFL"));
			list.Add(new SheetField(InOutEnum.Out,"nameLF"));
			list.Add(new SheetField(InOutEnum.Out,"address"));//includes address2
			list.Add(new SheetField(InOutEnum.Out,"cityStateZip"));
			return list;
		}

	}

}
