using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class Defs {
		///<summary>If using remoting, then the calling program is responsible for filling the arrays on the client since the automated part only happens on the server.  So there are TWO sets of arrays in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM definition ORDER BY Category,ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Def";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			DefC.Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Long[j]=GetForCategory(j,true,table);
			}
			DefC.Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Short[j]=GetForCategory(j,false,table);
			}
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex,bool includeHidden,DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Def> list=new List<Def>();
			Def def;
			for(int i=0;i<table.Rows.Count;i++) {
				if(PIn.Long(table.Rows[i][1].ToString())!=catIndex) {
					continue;
				}
				if(PIn.Bool(table.Rows[i][6].ToString())//if is hidden
					&& !includeHidden)//and we don't want to include hidden
				{
					continue;
				}
				def=new Def();
				def.DefNum    = PIn.Long(table.Rows[i][0].ToString());
				def.Category  = (DefCat)PIn.Long(table.Rows[i][1].ToString());
				def.ItemOrder = PIn.Int(table.Rows[i][2].ToString());
				def.ItemName  = PIn.String(table.Rows[i][3].ToString());
				def.ItemValue = PIn.String(table.Rows[i][4].ToString());
				def.ItemColor = Color.FromArgb(PIn.Int(table.Rows[i][5].ToString()));
				def.IsHidden  = PIn.Bool(table.Rows[i][6].ToString());
				list.Add(def);
			}
			return list.ToArray();
		}

		///<summary>Only used in FormDefinitions</summary>
		public static Def[] GetCatList(int myCat){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Def[]>(MethodBase.GetCurrentMethod(),myCat);
			}
			string command=
				"SELECT * from definition"
				+" WHERE category = '"+myCat+"'"
				+" ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			Def[] List=new Def[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Def();
				List[i].DefNum    = PIn.Long   (table.Rows[i][0].ToString());
				List[i].Category  = (DefCat)PIn.Long   (table.Rows[i][1].ToString());
				List[i].ItemOrder = PIn.Int   (table.Rows[i][2].ToString());
				List[i].ItemName  = PIn.String(table.Rows[i][3].ToString());
				List[i].ItemValue = PIn.String(table.Rows[i][4].ToString());
				List[i].ItemColor = Color.FromArgb(PIn.Int(table.Rows[i][5].ToString()));
				List[i].IsHidden  = PIn.Bool  (table.Rows[i][6].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			string command = "UPDATE definition SET "
				+ "Category = '"  +POut.Long((int)def.Category)+"'"
				+",ItemOrder = '" +POut.Long(def.ItemOrder)+"'"
				+",ItemName = '"  +POut.String(def.ItemName)+"'"
				+",ItemValue = '" +POut.String(def.ItemValue)+"'"
				+",ItemColor = '" +POut.Long(def.ItemColor.ToArgb())+"'"
				+",IsHidden = '"  +POut.Bool(def.IsHidden)+"'"
				+"WHERE defnum = '"+POut.Long(def.DefNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.DefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.DefNum;
			}
			if(PrefC.RandomKeys) {
				def.DefNum=ReplicationServers.GetKey("definition","DefNum");
			}
			string command="INSERT INTO definition (";
			if(PrefC.RandomKeys) {
				command+="DefNum,";
			}
			command+="Category,ItemOrder,"
				+"ItemName,ItemValue,ItemColor,IsHidden) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(def.DefNum)+", ";
			}
			command+=
				 "'"+POut.Long((int)def.Category)+"', "
				+"'"+POut.Long(def.ItemOrder)+"', "
				+"'"+POut.String(def.ItemName)+"', "
				+"'"+POut.String(def.ItemValue)+"', "
				+"'"+POut.Long(def.ItemColor.ToArgb())+"', "
				+"'"+POut.Bool(def.IsHidden)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				def.DefNum=Db.NonQ(command,true);//used in conversion
			}
			return def.DefNum;
		}

		///<summary>CAUTION.  This does not perform all validations.  It only properly validates for one def type right now.</summary>
		public static void Delete(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			if(def.Category!=DefCat.SupplyCats){
				throw new ApplicationException("NOT Allowed to delete this type of def.");
			}
			string command="SELECT COUNT(*) FROM supply WHERE Category="+POut.Long(def.DefNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("Defs","Def is in use.  Not allowed to delete."));
			}
			command="DELETE FROM definition WHERE DefNum="+POut.Long(def.DefNum);
			Db.NonQ(command);
			command="UPDATE definition SET ItemOrder=ItemOrder-1 "
				+"WHERE Category="+POut.Long((int)def.Category)
				+" AND ItemOrder > "+POut.Long(def.ItemOrder);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void HideDef(Def def) {

			//No need to check RemotingRole; no call to db.
			def.IsHidden=true;
			Defs.Update(def);
		}

		///<summary></summary>
		public static void SetOrder(int mySelNum,int myItemOrder,Def[] list) {
			//No need to check RemotingRole; no call to db.
			Def def=list[mySelNum];
			def.ItemOrder=myItemOrder;
			//Cur=temp;
			Defs.Update(def);
		}

	}
}
