using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class TrojanQueries {

		public static DataTable GetMaxProcedureDate(int PatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT MAX(ProcDate) FROM procedurelog,patient
				WHERE patient.PatNum=procedurelog.PatNum
				AND patient.Guarantor="+POut.PInt(PatNum);
			return Db.GetTable(command);
		}

		public static DataTable GetMaxPaymentDate(int PatNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT MAX(DatePay) FROM paysplit,patient
				WHERE patient.PatNum=paysplit.PatNum
				AND patient.Guarantor="+POut.PInt(PatNum);
			return Db.GetTable(command);
		}

		public static int GetUniqueFileNum(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT ValueString FROM preference WHERE PrefName='TrojanExpressCollectPreviousFileNumber'";
			DataTable table=Db.GetTable(command);
			int previousNum=PIn.PInt(table.Rows[0][0].ToString());
			int thisNum=previousNum+1;
			command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+
				"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
				+" AND ValueString='"+POut.PInt(previousNum)+"'";
			int result=Db.NonQ(command);
			while(result!=1) {//someone else sent one at the same time
				previousNum++;
				thisNum++;
				command="UPDATE preference SET ValueString='"+POut.PInt(thisNum)+
					"' WHERE PrefName='TrojanExpressCollectPreviousFileNumber'"
					+" AND ValueString='"+POut.PInt(previousNum)+"'";
				result=Db.NonQ(command);
			}
			return thisNum;
		}

	}
}
