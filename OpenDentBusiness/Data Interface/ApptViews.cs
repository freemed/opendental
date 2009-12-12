using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews{

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM apptview ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="ApptView";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ApptViewC.List=new ApptView[table.Rows.Count];
			for(int i=0;i<ApptViewC.List.Length;i++){
				ApptViewC.List[i]=new ApptView();
				ApptViewC.List[i].ApptViewNum = PIn.PLong   (table.Rows[i][0].ToString());
				ApptViewC.List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ApptViewC.List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				ApptViewC.List[i].RowsPerIncr = PIn.PInt   (table.Rows[i][3].ToString());
				ApptViewC.List[i].OnlyScheduledProvs = PIn.PBool(table.Rows[i][4].ToString());
				ApptViewC.List[i].OnlySchedBeforeTime= PIn.PTimeSpan(table.Rows[i][5].ToString());
				ApptViewC.List[i].OnlySchedAfterTime = PIn.PTimeSpan(table.Rows[i][6].ToString());	
			}
		}

		///<summary></summary>
		public static long Insert(ApptView Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ApptViewNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ApptViewNum;
			}
			if(PrefC.RandomKeys) {
				Cur.ApptViewNum=ReplicationServers.GetKey("apptview","ApptViewNum");
			}
			string command="INSERT INTO apptview (";
			if(PrefC.RandomKeys) {
				command+="ApptViewNum,";
			}
			command+="Description,ItemOrder,RowsPerIncr,OnlyScheduledProvs,OnlySchedBeforeTime,OnlySchedAfterTime) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(Cur.ApptViewNum)+", ";
			}
			command+=
				 "'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PLong   (Cur.ItemOrder)+"', "
				+"'"+POut.PLong   (Cur.RowsPerIncr)+"', "
				+"'"+POut.PBool(Cur.OnlyScheduledProvs)+"', "
				+POut.PTimeSpan(Cur.OnlySchedBeforeTime)+", "
				+POut.PTimeSpan(Cur.OnlySchedAfterTime)+")";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.ApptViewNum=Db.NonQ(command,true);
			}
			return Cur.ApptViewNum;
		}

		///<summary></summary>
		public static void Update(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE apptview SET "
				+"Description='"   +POut.PString(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.PLong   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.PLong   (Cur.RowsPerIncr)+"'"
				+",OnlyScheduledProvs = '"+POut.PBool(Cur.OnlyScheduledProvs)+"'"
				+",OnlySchedBeforeTime = "+POut.PTimeSpan(Cur.OnlySchedBeforeTime)
				+",OnlySchedAfterTime = "+POut.PTimeSpan(Cur.OnlySchedAfterTime)
				+" WHERE ApptViewNum = '"+POut.PLong(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE FROM apptview WHERE ApptViewNum = '"
				+POut.PLong(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

	

	


	}

	


}









