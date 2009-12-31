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
				ApptViewC.List[i].ApptViewNum = PIn.Long   (table.Rows[i][0].ToString());
				ApptViewC.List[i].Description = PIn.String(table.Rows[i][1].ToString());
				ApptViewC.List[i].ItemOrder   = PIn.Int   (table.Rows[i][2].ToString());
				ApptViewC.List[i].RowsPerIncr = PIn.Int   (table.Rows[i][3].ToString());
				ApptViewC.List[i].OnlyScheduledProvs = PIn.Bool(table.Rows[i][4].ToString());
				ApptViewC.List[i].OnlySchedBeforeTime= PIn.TimeSpan(table.Rows[i][5].ToString());
				ApptViewC.List[i].OnlySchedAfterTime = PIn.TimeSpan(table.Rows[i][6].ToString());	
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
				command+=POut.Long(Cur.ApptViewNum)+", ";
			}
			command+=
				 "'"+POut.String(Cur.Description)+"', "
				+"'"+POut.Long   (Cur.ItemOrder)+"', "
				+"'"+POut.Long   (Cur.RowsPerIncr)+"', "
				+"'"+POut.Bool(Cur.OnlyScheduledProvs)+"', "
				+POut.TimeSpan(Cur.OnlySchedBeforeTime)+", "
				+POut.TimeSpan(Cur.OnlySchedAfterTime)+")";
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
				+"Description='"   +POut.String(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.Long   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.Long   (Cur.RowsPerIncr)+"'"
				+",OnlyScheduledProvs = '"+POut.Bool(Cur.OnlyScheduledProvs)+"'"
				+",OnlySchedBeforeTime = "+POut.TimeSpan(Cur.OnlySchedBeforeTime)
				+",OnlySchedAfterTime = "+POut.TimeSpan(Cur.OnlySchedAfterTime)
				+" WHERE ApptViewNum = '"+POut.Long(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE FROM apptview WHERE ApptViewNum = '"
				+POut.Long(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

	

	


	}

	


}









