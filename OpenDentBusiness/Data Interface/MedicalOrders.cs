using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class MedicalOrders{
		///<summary></summary>
		public static List<MedicalOrder> Refresh(long patNum,bool includeDiscontinued){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicalOrder>>(MethodBase.GetCurrentMethod(),patNum,includeDiscontinued);
			}
			string command="SELECT * FROM medicalorder WHERE PatNum = "+POut.Long(patNum);
			if(!includeDiscontinued) {
				command+=" AND IsDiscontinued=0";
			}
			return Crud.MedicalOrderCrud.SelectMany(command);
		}

		///<summary></summary>
		public static int GetCountMedical(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetInt(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT COUNT(*) FROM medicalorder WHERE MedOrderType="+POut.Int((int)MedicalOrderType.Medication)+" "
				+"AND PatNUm="+POut.Long(patNum);
			return PIn.Int(Db.GetCount(command));
		}

		///<summary></summary>
		public static List<MedicalOrder> GetPendingLabs() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicalOrder>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM medicalorder WHERE MedOrderType="+POut.Int((int)MedicalOrderType.Laboratory)+" "
				+"AND IsLabPending = 1";
			//NOT EXISTS(SELECT * FROM labpanel WHERE labpanel.MedicalOrderNum=medicalorder.MedicalOrderNum)";
			return Crud.MedicalOrderCrud.SelectMany(command);
		}

		///<summary>Gets one MedicalOrder from the db.</summary>
		public static MedicalOrder GetOne(long medicalOrderNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<MedicalOrder>(MethodBase.GetCurrentMethod(),medicalOrderNum);
			}
			return Crud.MedicalOrderCrud.SelectOne(medicalOrderNum);
		}

		///<summary></summary>
		public static long Insert(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				medicalOrder.MedicalOrderNum=Meth.GetLong(MethodBase.GetCurrentMethod(),medicalOrder);
				return medicalOrder.MedicalOrderNum;
			}
			return Crud.MedicalOrderCrud.Insert(medicalOrder);
		}

		///<summary></summary>
		public static void Update(MedicalOrder medicalOrder){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrder);
				return;
			}
			Crud.MedicalOrderCrud.Update(medicalOrder);
		}

		///<summary></summary>
		public static void Delete(long medicalOrderNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicalOrderNum);
				return;
			}
			string command= "DELETE FROM medicalorder WHERE MedicalOrderNum = "+POut.Long(medicalOrderNum);
			Db.NonQ(command);
		}

		



	}
}