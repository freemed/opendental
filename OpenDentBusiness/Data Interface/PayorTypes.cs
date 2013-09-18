using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayorTypes{		
		///<summary></summary>
		public static List<PayorType> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PayorType>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM payortype WHERE PatNum = "+POut.Long(patNum)+" ORDER BY DateStart";
			return Crud.PayorTypeCrud.SelectMany(command);
		}

		///<summary>Gets most recent PayorType entry.</summary>
		public static string GetCurrentDescription(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=DbHelper.LimitOrderBy("SELECT * FROM payortype WHERE PatNum = "+POut.Long(patNum)+" ORDER BY DateStart DESC",1);
			PayorType payorType=Crud.PayorTypeCrud.SelectOne(command);
			if(payorType==null) {
				return "";
			}
			return Sops.GetRecentPayorTypeDescription(payorType.SopCode);
		}

		///<summary>Gets one PayorType from the db.</summary>
		public static PayorType GetOne(long payorTypeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PayorType>(MethodBase.GetCurrentMethod(),payorTypeNum);
			}
			return Crud.PayorTypeCrud.SelectOne(payorTypeNum);
		}

		///<summary></summary>
		public static long Insert(PayorType payorType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				payorType.PayorTypeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),payorType);
				return payorType.PayorTypeNum;
			}
			return Crud.PayorTypeCrud.Insert(payorType);
		}

		///<summary></summary>
		public static void Update(PayorType payorType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payorType);
				return;
			}
			Crud.PayorTypeCrud.Update(payorType);
		}

		///<summary></summary>
		public static void Delete(long payorTypeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),payorTypeNum);
				return;
			}
			string command= "DELETE FROM payortype WHERE PayorTypeNum = "+POut.Long(payorTypeNum);
			Db.NonQ(command);
		}



	}
}