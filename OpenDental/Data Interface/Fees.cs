using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Fees {
		///<summary>An array of hashtables, one for each non-hidden fee schedule.  For each hashtable, key is adacode, value is Fee object.</summary>
		private static Hashtable[] HList;

		///<summary>Refreshes all fees and loads them into HList array.  </summary>
		public static void Refresh() {
			HList=new Hashtable[DefB.Short[(int)DefCat.FeeSchedNames].Length];
			for(int i=0;i<HList.Length;i++) {
				HList[i]=new Hashtable();
			}
			Fee fee;
			string command= 
				"SELECT * from fee";
			DataTable table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				fee=new Fee();
				fee.FeeNum       =PIn.PInt(table.Rows[i][0].ToString());
				fee.Amount       =PIn.PDouble(table.Rows[i][1].ToString());
				fee.ADACode      =PIn.PString(table.Rows[i][2].ToString());
				fee.FeeSched     =PIn.PInt(table.Rows[i][3].ToString());
				fee.UseDefaultFee=PIn.PBool(table.Rows[i][4].ToString());
				fee.UseDefaultCov=PIn.PBool(table.Rows[i][5].ToString());
				if(DefB.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)!=-1) {//if fee sched is visible
					if(HList[DefB.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)].ContainsKey(fee.ADACode)) {
						//if fee was already loaded for this adacode, delete this duplicate.
						command="DELETE FROM fee WHERE feenum = '"+fee.FeeNum+"'";
						General.NonQ(command);
					}
					else {
						HList[DefB.GetOrder(DefCat.FeeSchedNames,fee.FeeSched)].Add(fee.ADACode,fee);
					}
				}
			}
		}

		///<summary></summary>
		public static void Update(Fee fee){
			string command= "UPDATE fee SET " 
				+ "amount = '"        +POut.PDouble(fee.Amount)+"'"
				+ ",adacode = '"      +POut.PString(fee.ADACode)+"'"
				+ ",feesched = '"     +POut.PInt   (fee.FeeSched)+"'"
				+ ",usedefaultfee = '"+POut.PBool  (fee.UseDefaultFee)+"'"
				+ ",usedefaultcov = '"+POut.PBool  (fee.UseDefaultCov)+"'"
				+" WHERE feenum = '"  +POut.PInt   (fee.FeeNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Fee fee){
			string command= "INSERT INTO fee (amount,adacode,"
				+"feesched,usedefaultfee,usedefaultcov) VALUES("
				+"'"+POut.PDouble(fee.Amount)+"', "
				+"'"+POut.PString(fee.ADACode)+"', "
				+"'"+POut.PInt   (fee.FeeSched)+"', "
				+"'"+POut.PBool  (fee.UseDefaultFee)+"', "
				+"'"+POut.PBool  (fee.UseDefaultCov)+"')";
 			fee.FeeNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(Fee fee){
			string command="DELETE FROM fee WHERE FeeNum="+fee.FeeNum;
			General.NonQ(command);
		}

		///<summary>Used in FormProcCodeEdit,FormProcedures, and FormClaimProc to get Fees for display and for editing. Returns null if no matching fee found.</summary>
		public static Fee GetFeeByOrder(string adacode, int order){
			if(adacode==null)
				return null;
			if(HList[order].Contains(adacode)){
				return (Fee)HList[order][adacode];
			}
			return null;
			//else{
				//MessageBox.Show("code not found: "+myADA);
			//	return new Fee();
			//}
		}

		///<summary>Returns an amount if a fee has been entered.  Otherwise returns -1.  Not usually used directly.</summary>
		public static double GetAmount(string adacode, int feeSched){
			if(adacode==null)
				return -1;
			if(feeSched==0)
				return -1;
			int i=DefB.GetOrder(DefCat.FeeSchedNames,feeSched);
			if(i==-1){
				return -1;//you cannot obtain fees for hidden fee schedules
			}
			if(HList[i].Contains(adacode)){
				return ((Fee)HList[i][adacode]).Amount;
			}
			return -1;//code not found
		}

		///<summary>Almost the same as GetAmount.  But never returns -1;  Returns an amount if a fee has been entered.  Returns 0 if code can't be found.</summary>
		public static double GetAmount0(string adacode, int feeSched){
			double retVal=GetAmount(adacode,feeSched);
			if(retVal==-1){
				return 0;
			}
			return retVal;															 
		}

		///<summary>Gets the fee schedule from the priinsplan, the patient, or the provider in that order.  Either returns a fee schedule (fk to definition.DefNum) or 0.</summary>
		public static int GetFeeSched(Patient pat,InsPlan[] PlanList,PatPlan[] patPlans){
			//there's not really a good place to put this function, so it's here.
			int retVal=0;
			if(PatPlans.GetPlanNum(patPlans,1)!=0){
				InsPlan PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(patPlans,1),PlanList);
				if(PlanCur==null){
					retVal=0;
				}
				else{
					retVal=PlanCur.FeeSched;
				}
			}
			if(retVal==0){
				if(pat.FeeSched!=0){
					retVal=pat.FeeSched;
				}
				else{
					if(pat.PriProv==0){
						retVal=Providers.List[0].FeeSched;
					}
					else{
            //MessageBox.Show(Providers.GetIndex(Patients.Cur.PriProv).ToString());   
						retVal=Providers.ListLong[Providers.GetIndexLong(pat.PriProv)].FeeSched;
					}
				}
			}
			return retVal;
		}

		///<summary>A simpler version of the same function above.  The required numbers can be obtained in a fairly simple query.  Might return a 0 if the primary provider does not have a fee schedule set.</summary>
		public static int GetFeeSched(int priPlanFeeSched, int patFeeSched, int patPriProvNum){
			if(priPlanFeeSched!=0){
				return priPlanFeeSched;
			}
			if(patFeeSched!=0){
				return patFeeSched;
			}
			return Providers.ListLong[Providers.GetIndexLong(patPriProvNum)].FeeSched;
		}

		///<summary>Clears all fees from one fee schedule.  Supply the DefNum of the feeSchedule.</summary>
		public static void ClearFeeSched(int schedNum){
			string command="DELETE FROM fee WHERE FeeSched="+schedNum;
			General.NonQ(command);
		}

		///<summary>Copies any fee objects over to the new fee schedule.  Usually run ClearFeeSched first.  Be careful exactly which int's you supply.</summary>
		public static void CopyFees(int fromSchedI,int toSchedNum){
			//Fee fee;
			Fee[] feeArray=new Fee[HList[fromSchedI].Values.Count];
			HList[fromSchedI].Values.CopyTo(feeArray,0);
			for(int i=0;i<feeArray.Length;i++){
				//fee=((Fee)HList[fromSchedI].Values  [i]).Copy();
				feeArray[i].FeeSched=toSchedNum;
				Fees.Insert(feeArray[i]);
			}
		}

		///<summary>Increases the fee schedule by percent.  Round should be the number of decimal places, either 0,1,or 2.</summary>
		public static void Increase(int schedI,int percent,int round){
			Fee[] feeArray=new Fee[HList[schedI].Values.Count];
			HList[schedI].Values.CopyTo(feeArray,0);
			double newVal;
			for(int i=0;i<feeArray.Length;i++){
				if(feeArray[i].Amount==0){
					continue;
				}
				newVal=(double)feeArray[i].Amount*(1+(double)percent/100);
				feeArray[i].Amount=Math.Round(newVal,round);
				Fees.Update(feeArray[i]);
			}
		}

		///<summary>schedI is the currently displayed index of the fee schedule to save to.  Empty fees never even make it this far and should be skipped earlier in the process.</summary>
		public static void Import(string adaCode,double amt,int schedI){
			if(!ProcedureCodes.IsValidCode(adaCode)){
				return;//skip for now. Possibly insert adaCode in a future version.
			}
			Fee fee=GetFeeByOrder(adaCode,schedI);
			if(fee!=null){
				Delete(fee);
			}
			fee=new Fee();
			fee.ADACode=adaCode;
			fee.Amount=amt;
			fee.FeeSched=DefB.Short[(int)DefCat.FeeSchedNames][schedI].DefNum;
			Insert(fee);
		}




	}

	

}













