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