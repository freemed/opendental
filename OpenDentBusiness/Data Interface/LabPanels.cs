using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LabPanels{

		///<summary></summary>
		public static List<LabPanel> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabPanel>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM labpanel WHERE PatNum="+POut.Long(patNum);
			return Crud.LabPanelCrud.SelectMany(command);
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

		public static List<long> GetChangedSinceLabPanelNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT LabPanelNum FROM labpanel WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> labpanelNums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				labpanelNums.Add(PIn.Long(dt.Rows[i]["LabPanelNum"].ToString()));
			}
			return labpanelNums;
		}

		///<summary>Used along with GetChangedSinceLabPanelNums</summary>
		public static List<LabPanel> GetMultLabPanels(List<long> labpanelNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabPanel>>(MethodBase.GetCurrentMethod(),labpanelNums);
			}
			string strLabPanelNums="";
			DataTable table;
			if(labpanelNums.Count>0) {
				for(int i=0;i<labpanelNums.Count;i++) {
					if(i>0) {
						strLabPanelNums+="OR ";
					}
					strLabPanelNums+="LabPanelNum='"+labpanelNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM labpanel WHERE "+strLabPanelNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			LabPanel[] multLabPanels=Crud.LabPanelCrud.TableToList(table).ToArray();
			List<LabPanel> LabPanelList=new List<LabPanel>(multLabPanels);
			return LabPanelList;
		}

		///<summary></summary>
		public static long Insert(LabPanel labPanel) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				labPanel.LabPanelNum=Meth.GetLong(MethodBase.GetCurrentMethod(),labPanel);
				return labPanel.LabPanelNum;
			}
			return Crud.LabPanelCrud.Insert(labPanel);
		}

		///<summary></summary>
		public static void Update(LabPanel labPanel) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labPanel);
				return;
			}
			Crud.LabPanelCrud.Update(labPanel);
		}
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.



		///<summary>Gets one LabPanel from the db.</summary>
		public static LabPanel GetOne(long labPanelNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<LabPanel>(MethodBase.GetCurrentMethod(),labPanelNum);
			}
			return Crud.LabPanelCrud.SelectOne(labPanelNum);
		}
		 
				///<summary></summary>
		public static List<LabPanel> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabPanel>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM labpanel WHERE PatNum = "+POut.Long(patNum);
			return Crud.LabPanelCrud.SelectMany(command);
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