using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class ClaimValCodeLog {

		public static double GetValAmountTotal(Claim Cur, string Code){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),Cur,Code);
			}
			double total = 0;
			string command="SELECT * FROM claimvalcodelog WHERE ClaimNum='" + POut.PInt(Cur.ClaimNum) + "' AND ValCode='" + POut.PString(Code) + "'";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				total+=PIn.PDouble(table.Rows[i][4].ToString());
			}
			return total;
		}

		public static ArrayList GetValCodes(Claim Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ArrayList>(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT * FROM claimvalcodelog WHERE ClaimNum='" + Cur.ClaimNum + "'";
			DataTable table=Db.GetTable(command);
			ArrayList List=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				ClaimValCode vc = new ClaimValCode();
				vc.ClaimValCodeLogNum=PIn.PInt(table.Rows[i][0].ToString());
				vc.ClaimNum=PIn.PInt(table.Rows[i][1].ToString());
				vc.ClaimField=PIn.PString(table.Rows[i][2].ToString());
				vc.ValCode=PIn.PString(table.Rows[i][3].ToString());
				vc.ValAmount=PIn.PDouble(table.Rows[i][4].ToString());
				vc.Ordinal=PIn.PInt(table.Rows[i][5].ToString());
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
				ClaimValCode vc = (ClaimValCode)vCodes[i];
				if(vc.ClaimValCodeLogNum==0){
					string command="INSERT INTO claimvalcodelog (ClaimNum,ClaimField,ValCode,ValAmount,Ordinal) VALUES("
						+"'"+POut.PInt(vc.ClaimNum)+"', "
						+"'"+POut.PString(vc.ClaimField)+"', "
						+"'"+POut.PString(vc.ValCode)+"', "
						+"'"+POut.PDouble(vc.ValAmount)+"', "
						+"'"+POut.PInt(vc.Ordinal)+"')";
 					Db.NonQ(command);
				} else {
				  string command="UPDATE claimvalcodelog SET "
				    +"ClaimNum='" + POut.PInt(vc.ClaimNum) + "',"
				    +"ValCode='" + POut.PString(vc.ValCode) + "',"
				    +"ValAmount='" + POut.PDouble(vc.ValAmount) + "' "
				    +"WHERE ClaimValCodeLogNum='" + POut.PInt(vc.ClaimValCodeLogNum) + "'";
				  Db.NonQ(command);
				}
			}
		} //Update
	}
} 