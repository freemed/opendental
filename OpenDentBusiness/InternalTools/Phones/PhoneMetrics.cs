using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PhoneMetrics{

		///<summary></summary>
		public static long Insert(PhoneMetric phoneMetric){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				phoneMetric.PhoneMetricNum=Meth.GetLong(MethodBase.GetCurrentMethod(),phoneMetric);
				return phoneMetric.PhoneMetricNum;
			}
			return Crud.PhoneMetricCrud.Insert(phoneMetric);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<PhoneMetric> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PhoneMetric>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM phonemetric WHERE PatNum = "+POut.Long(patNum);
			return Crud.PhoneMetricCrud.SelectMany(command);
		}

		///<summary>Gets one PhoneMetric from the db.</summary>
		public static PhoneMetric GetOne(long phoneMetricNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<PhoneMetric>(MethodBase.GetCurrentMethod(),phoneMetricNum);
			}
			return Crud.PhoneMetricCrud.SelectOne(phoneMetricNum);
		}

		///<summary></summary>
		public static void Update(PhoneMetric phoneMetric){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneMetric);
				return;
			}
			Crud.PhoneMetricCrud.Update(phoneMetric);
		}

		///<summary></summary>
		public static void Delete(long phoneMetricNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),phoneMetricNum);
				return;
			}
			string command= "DELETE FROM phonemetric WHERE PhoneMetricNum = "+POut.Long(phoneMetricNum);
			Db.NonQ(command);
		}
		*/



	}
}