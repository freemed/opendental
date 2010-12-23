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
			List<Def> list=Crud.DefCrud.TableToList(table);
			DefC.Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Long[j]=GetForCategory(j,true,list);
			}
			DefC.Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Short[j]=GetForCategory(j,false,list);
			}
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex,bool includeHidden,List<Def> list) {
			//No need to check RemotingRole; no call to db.
			List<Def> retVal=new List<Def>();
			for(int i=0;i<list.Count;i++) {
				if((int)list[i].Category!=catIndex){
					continue;
				}
				if(list[i].IsHidden && !includeHidden){
					continue;
				}
				retVal.Add(list[i]);
			}
			return retVal.ToArray();
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
			return Crud.DefCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.DefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(Def def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.DefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.DefNum;
			}
			return Crud.DefCrud.Insert(def);
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
