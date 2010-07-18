using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ApptFields{


		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ApptField> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ApptField>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM apptfield WHERE PatNum = "+POut.Long(patNum);
			return Crud.ApptFieldCrud.SelectMany(command);
		}

		///<summary>Gets one ApptField from the db.</summary>
		public static ApptField GetOne(long apptFieldNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ApptField>(MethodBase.GetCurrentMethod(),apptFieldNum);
			}
			return Crud.ApptFieldCrud.SelectOne(apptFieldNum);
		}

		///<summary></summary>
		public static long Insert(ApptField apptField){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				apptField.ApptFieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),apptField);
				return apptField.ApptFieldNum;
			}
			return Crud.ApptFieldCrud.Insert(apptField);
		}

		///<summary></summary>
		public static void Update(ApptField apptField){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptField);
				return;
			}
			Crud.ApptFieldCrud.Update(apptField);
		}

		///<summary></summary>
		public static void Delete(long apptFieldNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),apptFieldNum);
				return;
			}
			string command= "DELETE FROM apptfield WHERE ApptFieldNum = "+POut.Long(apptFieldNum);
			Db.NonQ(command);
		}
		*/



	}
}