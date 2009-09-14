using System;
using System.Data;
using System.Collections;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RepeatCharges {
		///<summary>Gets a list of all RepeatCharges for a given patient.  Supply 0 to get a list for all patients.</summary>
		public static RepeatCharge[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RepeatCharge[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM repeatcharge";
			if(patNum!=0) {
				command+=" WHERE PatNum = "+POut.PLong(patNum);
			}
			command+=" ORDER BY DateStart";
			DataTable table=Db.GetTable(command);
			RepeatCharge[] List=new RepeatCharge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RepeatCharge();
				List[i].RepeatChargeNum= PIn.PLong(table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PLong(table.Rows[i][1].ToString());
				List[i].ProcCode       = PIn.PString(table.Rows[i][2].ToString());
				List[i].ChargeAmt      = PIn.PDouble(table.Rows[i][3].ToString());
				List[i].DateStart      = PIn.PDate(table.Rows[i][4].ToString());
				List[i].DateStop       = PIn.PDate(table.Rows[i][5].ToString());
				List[i].Note           = PIn.PString(table.Rows[i][6].ToString());
			}
			return List;
		}	

		///<summary></summary>
		public static void Update(RepeatCharge charge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),charge);
				return;
			}
			string command="UPDATE repeatcharge SET " 
				+"PatNum = '"    +POut.PLong   (charge.PatNum)+"'"
				+",ProcCode = '" +POut.PString(charge.ProcCode)+"'"
				+",ChargeAmt = '"+POut.PDouble(charge.ChargeAmt)+"'"
				+",DateStart = "+POut.PDate  (charge.DateStart)
				+",DateStop = " +POut.PDate  (charge.DateStop)
				+",Note = '"     +POut.PString(charge.Note)+"'"
				+" WHERE RepeatChargeNum = '" +POut.PLong(charge.RepeatChargeNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(RepeatCharge charge) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				charge.RepeatChargeNum=Meth.GetInt(MethodBase.GetCurrentMethod(),charge);
				return charge.RepeatChargeNum;
			}
			if(PrefC.RandomKeys){
				charge.RepeatChargeNum=ReplicationServers.GetKey("repeatcharge","RepeatChargeNum");
			}
			string command="INSERT INTO repeatcharge (";
			if(PrefC.RandomKeys){
				command+="RepeatChargeNum,";
			}
			command+="PatNum,ProcCode,ChargeAmt,DateStart,DateStop,Note) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(charge.RepeatChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (charge.PatNum)+"', "
				+"'"+POut.PString(charge.ProcCode)+"', "
				+"'"+POut.PDouble(charge.ChargeAmt)+"', "
				+POut.PDate  (charge.DateStart)+", "
				+POut.PDate  (charge.DateStop)+", "
				+"'"+POut.PString(charge.Note)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				charge.RepeatChargeNum=Db.NonQ(command,true);
			}
			return charge.RepeatChargeNum;
		}

		///<summary>Called from FormRepeatCharge.</summary>
		public static void Delete(RepeatCharge charge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),charge);
				return;
			}
			string command="DELETE FROM repeatcharge WHERE RepeatChargeNum ="+POut.PLong(charge.RepeatChargeNum);
			Db.NonQ(command);
		}

		///<summary>Used in FormRepeatChargesUpdate to get a list of the dates of procedures that have the proccode and patnum specified.</summary>
		public static ArrayList GetDates(long codeNum,long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ArrayList>(MethodBase.GetCurrentMethod(),codeNum,patNum);
			}
			ArrayList retVal=new ArrayList();
			string command="SELECT ProcDate FROM procedurelog "
				+"WHERE PatNum="+POut.PLong(patNum)
				+" AND CodeNum="+POut.PLong(codeNum)
				+" AND ProcStatus=2";//complete
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.PDate(table.Rows[i][0].ToString()));
			}
			return retVal;
		}
		

		

		


	}

	

	


}










