using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class DefD {
		///<summary>Only used in FormDefinitions</summary>
		public static Def[] GetCatList(int myCat){
			string command=
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			Def[] List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Def();
				List[i].DefNum    = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.PInt   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.PString(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.PBool  (table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(Def def) {
			string command = "UPDATE definition SET "
				+ "Category = '"  +POut.PInt((int)def.Category)+"'"
				+",ItemOrder = '" +POut.PInt(def.ItemOrder)+"'"
				+",ItemName = '"  +POut.PString(def.ItemName)+"'"
				+",ItemValue = '" +POut.PString(def.ItemValue)+"'"
				+",ItemColor = '" +POut.PInt(def.ItemColor.ToArgb())+"'"
				+",IsHidden = '"  +POut.PBool(def.IsHidden)+"'"
				+"WHERE defnum = '"+POut.PInt(def.DefNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Def def) {
			string command= "INSERT INTO definition (Category,ItemOrder,"
				+"ItemName,ItemValue,ItemColor,IsHidden) VALUES("
				+"'"+POut.PInt((int)def.Category)+"', "
				+"'"+POut.PInt(def.ItemOrder)+"', "
				+"'"+POut.PString(def.ItemName)+"', "
				+"'"+POut.PString(def.ItemValue)+"', "
				+"'"+POut.PInt(def.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool(def.IsHidden)+"')";
			def.DefNum=General.NonQ(command,true);//used in conversion
		}

		///<summary>CAUTION.  This does not perform all validations.  It only properly validates for one def type right now.</summary>
		public static void Delete(Def def) {
			if(def.Category!=DefCat.SupplyCats){
				throw new ApplicationException("NOT Allowed to delete this type of def.");
			}
			string command="SELECT COUNT(*) FROM supply WHERE Category="+POut.PInt(def.DefNum);
			if(General.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("Defs","Def is in use.  Not allowed to delete."));
			}
			command="DELETE FROM definition WHERE DefNum="+POut.PInt(def.DefNum);
			General.NonQ(command);
			command="UPDATE definition SET ItemOrder=ItemOrder-1 "
				+"WHERE Category="+POut.PInt((int)def.Category)
				+" AND ItemOrder > "+POut.PInt(def.ItemOrder);
			General.NonQ(command);
		}
	}
}
