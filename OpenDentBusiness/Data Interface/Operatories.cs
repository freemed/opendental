using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Operatories {

		///<summary>Refresh all operatories</summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM operatory "
				+"ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Operatory";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			OperatoryC.Listt=TableToList(table);
			OperatoryC.ListShort=new List<Operatory>();
			for(int i=0;i<OperatoryC.Listt.Count;i++) {
				if(!OperatoryC.Listt[i].IsHidden) {
					OperatoryC.ListShort.Add(OperatoryC.Listt[i]);
				}
			}
		}

		private static List<Operatory> TableToList(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<Operatory> oplist=new List<Operatory>();
			Operatory op;
			for(int i=0;i<table.Rows.Count;i++) {
				op=new Operatory();
				op.OperatoryNum = PIn.Long(table.Rows[i][0].ToString());
				op.OpName       = PIn.String(table.Rows[i][1].ToString());
				op.Abbrev       = PIn.String(table.Rows[i][2].ToString());
				op.ItemOrder    = PIn.Long(table.Rows[i][3].ToString());
				op.IsHidden     = PIn.Bool(table.Rows[i][4].ToString());
				op.ProvDentist  = PIn.Long(table.Rows[i][5].ToString());
				op.ProvHygienist= PIn.Long(table.Rows[i][6].ToString());
				op.IsHygiene    = PIn.Bool(table.Rows[i][7].ToString());
				op.ClinicNum    = PIn.Long(table.Rows[i][8].ToString());
				//DateTStamp
				oplist.Add(op);
			}
			return oplist;
		}

		///<summary></summary>
		public static long Insert(Operatory op) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				op.OperatoryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),op);
				return op.OperatoryNum;
			}
			if(PrefC.RandomKeys) {
				op.OperatoryNum=ReplicationServers.GetKey("operatory","OperatoryNum");
			}
			string command="INSERT INTO operatory (";
			if(PrefC.RandomKeys) {
				command+="OperatoryNum,";
			}
			command+="OpName,Abbrev,ItemOrder,IsHidden,ProvDentist,ProvHygienist,"
				+"IsHygiene,ClinicNum"//DateTStamp
				+") VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(op.OperatoryNum)+", ";
			}
			command+=
				 "'"+POut.String(op.OpName)+"', "
				+"'"+POut.String(op.Abbrev)+"', "
				+"'"+POut.Long   (op.ItemOrder)+"', "
				+"'"+POut.Bool  (op.IsHidden)+"', "
				+"'"+POut.Long   (op.ProvDentist)+"', "
				+"'"+POut.Long   (op.ProvHygienist)+"', "
				+"'"+POut.Bool  (op.IsHygiene)+"', "
				+"'"+POut.Long   (op.ClinicNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				op.OperatoryNum=Db.NonQ(command,true);
			}
			return op.OperatoryNum;
		}

		///<summary></summary>
		public static void Update(Operatory op){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),op);
				return;
			}
			string command= "UPDATE operatory SET " 
				+ "OpName = '"        +POut.String(op.OpName)+"'"
				+ ",Abbrev = '"       +POut.String(op.Abbrev)+"'"
				+ ",ItemOrder = '"    +POut.Long   (op.ItemOrder)+"'"
				+ ",IsHidden = '"     +POut.Bool  (op.IsHidden)+"'"
				+ ",ProvDentist = '"  +POut.Long   (op.ProvDentist)+"'"
				+ ",ProvHygienist = '"+POut.Long   (op.ProvHygienist)+"'"
				+ ",IsHygiene = '"    +POut.Bool  (op.IsHygiene)+"'"
				+ ",ClinicNum = '"    +POut.Long   (op.ClinicNum)+"'"	
				//DateTStamp
				+" WHERE OperatoryNum = '" +POut.Long(op.OperatoryNum)+"'";
			//MessageBox.Show(string command);
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(Operatory op, bool IsNew){
			//No need to check RemotingRole; no call to db.
			//if(){
				//throw new ApplicationException(Lans.g(this,""));
			//}
			if(IsNew){
				Insert(op);
			}
			else{
				Update(op);
			}
		}

		//<summary>Checks dependencies first.  Throws exception if can't delete.</summary>
		//public void Delete(){//no such thing as delete.  Hide instead
		//}

		public static List<Operatory> GetChangedSince(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Operatory>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM operatory WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable table=Db.GetTable(command);
			return TableToList(table);
		}

		public static string GetAbbrev(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.Listt.Count;i++){
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum){
					return OperatoryC.Listt[i].Abbrev;
				}
			}
			return "";
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(long opNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.ListShort.Count;i++) {
				if(OperatoryC.ListShort[i].OperatoryNum==opNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static Operatory GetOperatory(long operatoryNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.Listt.Count;i++) {
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum) {
					return OperatoryC.Listt[i].Copy();
				}
			}
			return null;
		}
		
	
	}
	


}













