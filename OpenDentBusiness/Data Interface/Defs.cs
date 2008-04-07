using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class Defs {
		///<summary>If using remoting, then the calling program is responsible for filling the arrays on the client since the automated part only happens on the server.  So there are TWO sets of arrays in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataSet Refresh(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp){
				throw new ApplicationException("Defs.Refresh incorrect RemotingRole.");
			}
			string command="SELECT * FROM definition ORDER BY Category,ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			retVal.Tables.Add(table);
			FillCache(table);
			return retVal;
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex,bool includeHidden,DataTable table) {
			List<Def> list=new List<Def>();
			Def def;
			for(int i=0;i<table.Rows.Count;i++) {
				if(PIn.PInt(table.Rows[i][1].ToString())!=catIndex) {
					continue;
				}
				if(PIn.PBool(table.Rows[i][6].ToString())//if is hidden
					&& !includeHidden)//and we don't want to include hidden
				{
					continue;
				}
				def=new Def();
				def.DefNum    = PIn.PInt(table.Rows[i][0].ToString());
				def.Category  = (DefCat)PIn.PInt(table.Rows[i][1].ToString());
				def.ItemOrder = PIn.PInt(table.Rows[i][2].ToString());
				def.ItemName  = PIn.PString(table.Rows[i][3].ToString());
				def.ItemValue = PIn.PString(table.Rows[i][4].ToString());
				def.ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				def.IsHidden  = PIn.PBool(table.Rows[i][6].ToString());
				list.Add(def);
			}
			return list.ToArray();
		}

		public static void FillCache(DataTable table){
			DefC.Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Long[j]=GetForCategory(j,true,table);
			}
			DefC.Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Short[j]=GetForCategory(j,false,table);
			}
		}

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
