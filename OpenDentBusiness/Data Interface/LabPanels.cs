using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LabPanels{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all LabPanels.</summary>
		private static List<LabPanel> listt;

		///<summary>A list of all LabPanels.</summary>
		public static List<LabPanel> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM labpanel ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="LabPanel";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.LabPanelCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<LabPanel> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabPanel>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM labpanel WHERE PatNum = "+POut.Long(patNum);
			return Crud.LabPanelCrud.SelectMany(command);
		}

		///<summary>Gets one LabPanel from the db.</summary>
		public static LabPanel GetOne(long labPanelNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<LabPanel>(MethodBase.GetCurrentMethod(),labPanelNum);
			}
			return Crud.LabPanelCrud.SelectOne(labPanelNum);
		}

		///<summary></summary>
		public static long Insert(LabPanel labPanel){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				labPanel.LabPanelNum=Meth.GetLong(MethodBase.GetCurrentMethod(),labPanel);
				return labPanel.LabPanelNum;
			}
			return Crud.LabPanelCrud.Insert(labPanel);
		}

		///<summary></summary>
		public static void Update(LabPanel labPanel){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labPanel);
				return;
			}
			Crud.LabPanelCrud.Update(labPanel);
		}

		///<summary></summary>
		public static void Delete(long labPanelNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labPanelNum);
				return;
			}
			string command= "DELETE FROM labpanel WHERE LabPanelNum = "+POut.Long(labPanelNum);
			Db.NonQ(command);
		}
		*/



	}
}