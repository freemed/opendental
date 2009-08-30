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
				op.OperatoryNum = PIn.PInt(table.Rows[i][0].ToString());
				op.OpName       = PIn.PString(table.Rows[i][1].ToString());
				op.Abbrev       = PIn.PString(table.Rows[i][2].ToString());
				op.ItemOrder    = PIn.PInt(table.Rows[i][3].ToString());
				op.IsHidden     = PIn.PBool(table.Rows[i][4].ToString());
				op.ProvDentist  = PIn.PInt(table.Rows[i][5].ToString());
				op.ProvHygienist= PIn.PInt(table.Rows[i][6].ToString());
				op.IsHygiene    = PIn.PBool(table.Rows[i][7].ToString());
				op.ClinicNum    = PIn.PInt(table.Rows[i][8].ToString());
				//DateTStamp
				oplist.Add(op);
			}
			return oplist;
		}

		///<summary></summary>
		private static long Insert(Operatory op) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				op.OperatoryNum=Meth.GetInt(MethodBase.GetCurrentMethod(),op);
				return op.OperatoryNum;
			}
			string command= "INSERT INTO operatory (OpName,Abbrev,ItemOrder,IsHidden,ProvDentist,ProvHygienist,"
				+"IsHygiene,ClinicNum"//DateTStamp
				+") VALUES("
				+"'"+POut.PString(op.OpName)+"', "
				+"'"+POut.PString(op.Abbrev)+"', "
				+"'"+POut.PInt   (op.ItemOrder)+"', "
				+"'"+POut.PBool  (op.IsHidden)+"', "
				+"'"+POut.PInt   (op.ProvDentist)+"', "
				+"'"+POut.PInt   (op.ProvHygienist)+"', "
				+"'"+POut.PBool  (op.IsHygiene)+"', "
				+"'"+POut.PInt   (op.ClinicNum)+"')";
 			op.OperatoryNum=Db.NonQ(command,true);
			return op.OperatoryNum;
		}

		///<summary></summary>
		private static void Update(Operatory op){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),op);
				return;
			}
			string command= "UPDATE operatory SET " 
				+ "OpName = '"        +POut.PString(op.OpName)+"'"
				+ ",Abbrev = '"       +POut.PString(op.Abbrev)+"'"
				+ ",ItemOrder = '"    +POut.PInt   (op.ItemOrder)+"'"
				+ ",IsHidden = '"     +POut.PBool  (op.IsHidden)+"'"
				+ ",ProvDentist = '"  +POut.PInt   (op.ProvDentist)+"'"
				+ ",ProvHygienist = '"+POut.PInt   (op.ProvHygienist)+"'"
				+ ",IsHygiene = '"    +POut.PBool  (op.IsHygiene)+"'"
				+ ",ClinicNum = '"    +POut.PInt   (op.ClinicNum)+"'"	
				//DateTStamp
				+" WHERE OperatoryNum = '" +POut.PInt(op.OperatoryNum)+"'";
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

		public static List<Operatory> GetUAppoint(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Operatory>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM operatory WHERE DateTStamp > "+POut.PDateT(changedSince);
			DataTable table=Db.GetTable(command);
			return TableToList(table);
		}

		public static string GetAbbrev(int operatoryNum){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.Listt.Count;i++){
				if(OperatoryC.Listt[i].OperatoryNum==operatoryNum){
					return OperatoryC.Listt[i].Abbrev;
				}
			}
			return "";
		}

		///<summary>Gets the order of the op within ListShort or -1 if not found.</summary>
		public static int GetOrder(int opNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<OperatoryC.ListShort.Count;i++) {
				if(OperatoryC.ListShort[i].OperatoryNum==opNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static Operatory GetOperatory(int operatoryNum) {
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













