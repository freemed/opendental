using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class ClaimValCodeLogs {

		public static double GetValAmountTotal(Claim Cur, string Code){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),Cur,Code);
			}
			double total = 0;
			string command="SELECT * FROM claimvalcodelog WHERE ClaimNum='" + POut.Long(Cur.ClaimNum) + "' AND ValCode='" + POut.String(Code) + "'";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				total+=PIn.Double(table.Rows[i][4].ToString());
			}
			return total;
		}

		public static ArrayList GetValCodes(Claim Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ArrayList>(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT * FROM claimvalcodelog WHERE ClaimNum='" + Cur.ClaimNum + "'";
			DataTable table=Db.GetTable(command);
			ArrayList List=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				ClaimValCodeLog vc = new ClaimValCodeLog();
				vc.ClaimValCodeLogNum=PIn.Long(table.Rows[i][0].ToString());
				vc.ClaimNum=PIn.Long(table.Rows[i][1].ToString());
				vc.ClaimField=PIn.String(table.Rows[i][2].ToString());
				vc.ValCode=PIn.String(table.Rows[i][3].ToString());
				vc.ValAmount=PIn.Double(table.Rows[i][4].ToString());
				vc.Ordinal=PIn.Int(table.Rows[i][5].ToString());
				List.Add(vc);
			}
			return List;
		}

		public static void Update(ArrayList vCodes){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),vCodes);
				return;
			}
			for(int i=0;i<vCodes.Count;i++){
				ClaimValCodeLog vc = (ClaimValCodeLog)vCodes[i];
				if(vc.ClaimValCodeLogNum==0){
					string command="INSERT INTO claimvalcodelog (ClaimNum,ClaimField,ValCode,ValAmount,Ordinal) VALUES("
						+"'"+POut.Long(vc.ClaimNum)+"', "
						+"'"+POut.String(vc.ClaimField)+"', "
						+"'"+POut.String(vc.ValCode)+"', "
						+"'"+POut.Double(vc.ValAmount)+"', "
						+"'"+POut.Long(vc.Ordinal)+"')";
 					Db.NonQ(command);
				} 
				else {
				  string command="UPDATE claimvalcodelog SET "
				    +"ClaimNum='" + POut.Long(vc.ClaimNum) + "',"
				    +"ValCode='" + POut.String(vc.ValCode) + "',"
				    +"ValAmount='" + POut.Double(vc.ValAmount) + "' "
				    +"WHERE ClaimValCodeLogNum='" + POut.Long(vc.ClaimValCodeLogNum) + "'";
				  Db.NonQ(command);
				}
			}
		} //Update
	}
} 