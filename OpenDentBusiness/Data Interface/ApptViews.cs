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
			ApptViewC.List=Crud.ApptViewCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(ApptView apptView) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				apptView.ApptViewNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptView);
				return apptView.ApptViewNum;
			}
			return Crud.ApptViewCrud.Insert(apptView);
		}

		///<summary></summary>
		public static void Update(ApptView apptView){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptView);
				return;
			}
			Crud.ApptViewCrud.Update(apptView);
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









