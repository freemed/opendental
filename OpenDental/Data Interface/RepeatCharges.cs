using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class RepeatCharges {
		///<summary>Gets a list of all RepeatCharges for a given patient.  Supply 0 to get a list for all patients.</summary>
		public static RepeatCharge[] Refresh(int patNum) {
			string command="SELECT * FROM repeatcharge";
			if(patNum!=0) {
				command+=" WHERE PatNum = "+POut.PInt(patNum);
			}
			command+=" ORDER BY DateStart";
			DataTable table=General.GetTable(command);
			RepeatCharge[] List=new RepeatCharge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RepeatCharge();
				List[i].RepeatChargeNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum         = PIn.PInt(table.Rows[i][1].ToString());
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
			string command="UPDATE repeatcharge SET " 
				+"PatNum = '"    +POut.PInt   (charge.PatNum)+"'"
				+",ProcCode = '" +POut.PString(charge.ProcCode)+"'"
				+",ChargeAmt = '"+POut.PDouble(charge.ChargeAmt)+"'"
				+",DateStart = "+POut.PDate  (charge.DateStart)
				+",DateStop = " +POut.PDate  (charge.DateStop)
				+",Note = '"     +POut.PString(charge.Note)+"'"
				+" WHERE RepeatChargeNum = '" +POut.PInt(charge.RepeatChargeNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RepeatCharge charge){
			if(PrefB.RandomKeys){
				charge.RepeatChargeNum=MiscData.GetKey("repeatcharge","RepeatChargeNum");
			}
			string command="INSERT INTO repeatcharge (";
			if(PrefB.RandomKeys){
				command+="RepeatChargeNum,";
			}
			command+="PatNum,ProcCode,ChargeAmt,DateStart,DateStop,Note) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(charge.RepeatChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (charge.PatNum)+"', "
				+"'"+POut.PString(charge.ProcCode)+"', "
				+"'"+POut.PDouble(charge.ChargeAmt)+"', "
				+POut.PDate  (charge.DateStart)+", "
				+POut.PDate  (charge.DateStop)+", "
				+"'"+POut.PString(charge.Note)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				charge.RepeatChargeNum=General.NonQ(command,true);
			}
		}

		///<summary>Called from FormRepeatCharge.</summary>
		public static void Delete(RepeatCharge charge){
			string command="DELETE FROM repeatcharge WHERE RepeatChargeNum ="+POut.PInt(charge.RepeatChargeNum);
			General.NonQ(command);
		}



	

		///<summary>Used in FormRepeatChargesUpdate to get a list of the dates of procedures that have the proccode and patnum specified.</summary>
		public static ArrayList GetDates(int codeNum,int patNum){
			ArrayList retVal=new ArrayList();
			string command="SELECT ProcDate FROM procedurelog "
				+"WHERE PatNum="+POut.PInt(patNum)
				+" AND CodeNum="+POut.PInt(codeNum)
				+" AND ProcStatus=2";//complete
			DataTable table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.PDate(table.Rows[i][0].ToString()));
			}
			return retVal;
		}
		

		

		


	}

	

	


}










