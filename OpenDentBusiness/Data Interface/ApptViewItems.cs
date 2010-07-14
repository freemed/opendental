using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptviewitem table in the database.</summary>
	public class ApptViewItems{
		
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from apptviewitem ORDER BY ElementOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ApptViewItem";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ApptViewItemC.List=Crud.ApptViewItemCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(ApptViewItem apptViewItem){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				apptViewItem.ApptViewItemNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptViewItem);
				return apptViewItem.ApptViewItemNum;
			}
			return Crud.ApptViewItemCrud.Insert(apptViewItem);
		}

		///<summary></summary>
		public static void Update(ApptViewItem apptViewItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptViewItem);
				return;
			}
			Crud.ApptViewItemCrud.Update(apptViewItem);
		}

		///<summary></summary>
		public static void Delete(ApptViewItem apptViewItem) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptViewItem);
				return;
			}
			string command="DELETE from apptviewitem WHERE ApptViewItemNum = '"
				+POut.Long(apptViewItem.ApptViewItemNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Deletes all apptviewitems for the current apptView.</summary>
		public static void DeleteAllForView(ApptView view){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),view);
				return;
			}
			string command="DELETE from apptviewitem WHERE ApptViewNum = '"
				+POut.Long(view.ApptViewNum)+"'";
			Db.NonQ(command);
		}

		public static List<long> GetOpsForView(long apptViewNum) {
			//No need to check RemotingRole; no call to db.
			//ArrayList AL=new ArrayList();
			List<long> retVal=new List<long>();
			for(int i=0;i<ApptViewItemC.List.Length;i++){
				if(ApptViewItemC.List[i].ApptViewNum==apptViewNum && ApptViewItemC.List[i].OpNum!=0){
					retVal.Add(ApptViewItemC.List[i].OpNum);
				}
			}
			//int[] retVal=new int[AL.Count]();
			return retVal;//(int[])AL.ToArray(typeof(int));
		}

		

	}

	


}









