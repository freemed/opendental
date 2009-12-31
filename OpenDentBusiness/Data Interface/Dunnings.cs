using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Dunnings {

		///<summary>Gets a list of all dunnings.</summary>
		public static Dunning[] Refresh() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Dunning[]>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM dunning "
				+"ORDER BY BillingType,AgeAccount,InsIsPending";
			DataTable table=Db.GetTable(command);
			Dunning[] List=new Dunning[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Dunning();
				List[i].DunningNum     = PIn.Long(table.Rows[i][0].ToString());
				List[i].DunMessage     = PIn.String(table.Rows[i][1].ToString());
				List[i].BillingType    = PIn.Long(table.Rows[i][2].ToString());
				List[i].AgeAccount     = PIn.Int(table.Rows[i][3].ToString());
				List[i].InsIsPending   = (YN)PIn.Long(table.Rows[i][4].ToString());
				List[i].MessageBold    = PIn.String(table.Rows[i][5].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static long Insert(Dunning dun) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				dun.DunningNum=Meth.GetLong(MethodBase.GetCurrentMethod(),dun);
				return dun.DunningNum;
			}
			if(PrefC.RandomKeys) {
				dun.DunningNum=ReplicationServers.GetKey("dunning","DunningNum");
			}
			string command="INSERT INTO dunning (";
			if(PrefC.RandomKeys) {
				command+="DunningNum,";
			}
			command+="DunMessage,BillingType,AgeAccount,InsIsPending,"
				+"MessageBold) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(dun.DunningNum)+", ";
			}
			command+=
				 "'"+POut.String(dun.DunMessage)+"', "
				+"'"+POut.Long   (dun.BillingType)+"', "
				+"'"+POut.Long   (dun.AgeAccount)+"', "
				+"'"+POut.Long   ((int)dun.InsIsPending)+"', "
				+"'"+POut.String(dun.MessageBold)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				dun.DunningNum=Db.NonQ(command,true);
			}
			return dun.DunningNum;
		}

		///<summary></summary>
		public static void Update(Dunning dun){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dun);
				return;
			}
			string command= "UPDATE dunning SET " 
				+ "DunMessage = '"       +POut.String(dun.DunMessage)+"'"
				+ ",BillingType = '"     +POut.Long   (dun.BillingType)+"'"
				+ ",AgeAccount = '"      +POut.Long   (dun.AgeAccount)+"'"
				+ ",InsIsPending = '"    +POut.Long   ((int)dun.InsIsPending)+"'"
				+ ",MessageBold = '"     +POut.String(dun.MessageBold)+"'"
				+" WHERE DunningNum = '" +POut.Long   (dun.DunningNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Dunning dun){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dun);
				return;
			}
			string command="DELETE FROM dunning" 
				+" WHERE DunningNum = "+POut.Long(dun.DunningNum);
 			Db.NonQ(command);
		}

		///<summary>Will return null if no dunning matches the given criteria.</summary>
		public static Dunning GetDunning(Dunning[] dunList,long billingType,int ageAccount,YN insIsPending) {
			//No need to check RemotingRole; no call to db.
			//loop backwards through Dunning list and find the first dunning that matches criteria.
			for(int i=dunList.Length-1;i>=0;i--){
				if(dunList[i].BillingType!=0//0 in the list matches all
					&& dunList[i].BillingType!=billingType){
					continue;
				}
				if(ageAccount < dunList[i].AgeAccount){//match if age is >= item in list
					continue;
				}
				if(dunList[i].InsIsPending!=YN.Unknown//unknown in the list matches all
					&& dunList[i].InsIsPending!=insIsPending){
					continue;
				}
				return dunList[i];
			}
			return null;
		}

		

	}
	


}













