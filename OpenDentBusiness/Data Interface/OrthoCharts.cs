using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class OrthoCharts{

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<OrthoChart> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrthoChart>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM orthochart WHERE PatNum = "+POut.Long(patNum);
			return Crud.OrthoChartCrud.SelectMany(command);
		}

		///<summary>Gets one OrthoChart from the db.</summary>
		public static OrthoChart GetOne(long orthoChartNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<OrthoChart>(MethodBase.GetCurrentMethod(),orthoChartNum);
			}
			return Crud.OrthoChartCrud.SelectOne(orthoChartNum);
		}

		///<summary></summary>
		public static long Insert(OrthoChart orthoChart){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				orthoChart.OrthoChartNum=Meth.GetLong(MethodBase.GetCurrentMethod(),orthoChart);
				return orthoChart.OrthoChartNum;
			}
			return Crud.OrthoChartCrud.Insert(orthoChart);
		}

		///<summary></summary>
		public static void Update(OrthoChart orthoChart){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orthoChart);
				return;
			}
			Crud.OrthoChartCrud.Update(orthoChart);
		}

		///<summary></summary>
		public static void Delete(long orthoChartNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orthoChartNum);
				return;
			}
			string command= "DELETE FROM orthochart WHERE OrthoChartNum = "+POut.Long(orthoChartNum);
			Db.NonQ(command);
		}
		*/



	}
}