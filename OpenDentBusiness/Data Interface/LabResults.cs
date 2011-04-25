using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LabResults{
	
		public static List<LabResult> GetForPanel(long labPanelNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabResult>>(MethodBase.GetCurrentMethod(),labPanelNum);
			}
			string command="SELECT * FROM labresult WHERE LabPanelNum = "+POut.Long(labPanelNum);
			return Crud.LabResultCrud.SelectMany(command);
		}

		///<summary></summary>
		public static void Delete(long labResultNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labResultNum);
				return;
			}
			string command= "DELETE FROM labresult WHERE LabResultNum = "+POut.Long(labResultNum);
			Db.NonQ(command);
		}

		///<summary>Deletes all Lab Results associated with Lab Panel.</summary>
		public static void DeleteForPanel(long labPanelNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labPanelNum);
				return;
			}
			string command= "DELETE FROM labresult WHERE LabPanelNum = "+POut.Long(labPanelNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<LabResult> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabResult>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM labresult WHERE PatNum = "+POut.Long(patNum);
			return Crud.LabResultCrud.SelectMany(command);
		}

		///<summary>Gets one LabResult from the db.</summary>
		public static LabResult GetOne(long labResultNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<LabResult>(MethodBase.GetCurrentMethod(),labResultNum);
			}
			return Crud.LabResultCrud.SelectOne(labResultNum);
		}

		///<summary></summary>
		public static long Insert(LabResult labResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				labResult.LabResultNum=Meth.GetLong(MethodBase.GetCurrentMethod(),labResult);
				return labResult.LabResultNum;
			}
			return Crud.LabResultCrud.Insert(labResult);
		}

		///<summary></summary>
		public static void Update(LabResult labResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labResult);
				return;
			}
			Crud.LabResultCrud.Update(labResult);
		}

		*/



	}
}