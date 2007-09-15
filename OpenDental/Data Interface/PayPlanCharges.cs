using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PayPlanCharges {
		///<summary>Gets all PayPlanCharges for a guarantor or patient, ordered by date.</summary>
		public static PayPlanCharge[] Refresh(int patNum) {
			string command=
				"SELECT * FROM payplancharge "
				+"WHERE Guarantor='"+POut.PInt(patNum)+"' "
				+"OR PatNum='"+POut.PInt(patNum)+"' "
				+"ORDER BY ChargeDate";
			DataTable table=General.GetTable(command);
			PayPlanCharge[] List=new PayPlanCharge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PayPlanCharge();
				List[i].PayPlanChargeNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PayPlanNum      = PIn.PInt(table.Rows[i][1].ToString());
				List[i].Guarantor       = PIn.PInt(table.Rows[i][2].ToString());
				List[i].PatNum          = PIn.PInt(table.Rows[i][3].ToString());
				List[i].ChargeDate      = PIn.PDate(table.Rows[i][4].ToString());
				List[i].Principal       = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].Interest        = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].Note            = PIn.PString(table.Rows[i][7].ToString());
				List[i].ProvNum         = PIn.PInt(table.Rows[i][8].ToString());
			}
			return List;
		}

		///<summary></summary>
		private static void Update(PayPlanCharge charge){
			string command= "UPDATE payplancharge SET " 
				+"PayPlanChargeNum = '"+POut.PInt   (charge.PayPlanChargeNum)+"'"
				+",PayPlanNum = '"     +POut.PInt   (charge.PayPlanNum)+"'"
				+",Guarantor = '"      +POut.PInt   (charge.Guarantor)+"'"
				+",PatNum = '"         +POut.PInt   (charge.PatNum)+"'"
				+",ChargeDate = "     +POut.PDate  (charge.ChargeDate)
				+",Principal = '"      +POut.PDouble(charge.Principal)+"'"
				+",Interest = '"       +POut.PDouble(charge.Interest)+"'"
				+",Note = '"           +POut.PString(charge.Note)+"'"
				+",ProvNum = '"        +POut.PInt   (charge.ProvNum)+"'"
				+" WHERE PayPlanChargeNum = '"+POut.PInt(charge.PayPlanChargeNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(PayPlanCharge charge){
			if(PrefB.RandomKeys){
				charge.PayPlanChargeNum=MiscData.GetKey("payplancharge","PayPlanChargeNum");
			}
			string command= "INSERT INTO payplancharge (";
			if(PrefB.RandomKeys){
				command+="PayPlanChargeNum,";
			}
			command+="PayPlanNum,Guarantor,PatNum,ChargeDate,Principal,Interest,Note,ProvNum) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(charge.PayPlanChargeNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (charge.PayPlanNum)+"', "
				+"'"+POut.PInt   (charge.Guarantor)+"', "
				+"'"+POut.PInt   (charge.PatNum)+"', "
				+POut.PDate  (charge.ChargeDate)+", "
				+"'"+POut.PDouble(charge.Principal)+"', "
				+"'"+POut.PDouble(charge.Interest)+"', "
				+"'"+POut.PString(charge.Note)+"', "
				+"'"+POut.PInt   (charge.ProvNum)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				charge.PayPlanChargeNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(PayPlanCharge charge, bool isNew){
			//if(){
			//	throw new ApplicationException(Lan.g(this,""));
			//}
			if(isNew){
				Insert(charge);
			}
			else{
				Update(charge);
			}
		}

		///<summary></summary>
		public static void Delete(PayPlanCharge charge){
			string command= "DELETE from payplancharge WHERE PayPlanChargeNum = '"
				+POut.PInt(charge.PayPlanChargeNum)+"'";
 			General.NonQ(command);
		}

		///<summary>Must pass in a list of charges for the guarantor.  The ones for this particular payplan will be returned.</summary>
		public static PayPlanCharge[] GetForPayPlan(int payPlanNum,PayPlanCharge[] ChargeList){
			ArrayList AL=new ArrayList();
			for(int i=0;i<ChargeList.Length;i++){
				if(ChargeList[i].PayPlanNum==payPlanNum){
					AL.Add(ChargeList[i]);
				}
			}
			PayPlanCharge[] retVal=new PayPlanCharge[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary></summary>
		public static void DeleteAllInPlan(int payPlanNum){
			string command="DELETE FROM payplancharge WHERE PayPlanNum="+payPlanNum.ToString();
			General.NonQ(command);
		}

	
	}

	

	


}




















