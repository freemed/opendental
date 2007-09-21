using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary>This does not correspond to any table in the database.  It works with a variety of tables to calculate aging.</summary>
	public class Ledgers{
		///<summary></summary>
		public static double[] Bal;//30-60-90 for one guarantor
		///<summary></summary>
		public static double InsEst;//for one guarantor
		///<summary></summary>
		public static double BalTotal;//for one guarantor
		private static DateTime AsOfDate;
		///<summary></summary>
		public struct DateValuePair{
			///<summary></summary>
			public DateTime Date;
			///<summary></summary>
			public double Value;
		}

		///<summary></summary>
		public static int[] GetAllGuarantors(){
			string command="SELECT DISTINCT guarantor FROM patient";
			DataTable table=General.GetTable(command);
			int[] AllGuarantors=new int[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				AllGuarantors[i]=PIn.PInt(table.Rows[i][0].ToString());
			}
			return AllGuarantors;
		}

		/*
		///<summary></summary>
		public static DateTime GetClosestFirst(DateTime date){ 
			if(date.Day > 15){
				if(date.Month!=12){
					return new DateTime(date.Year,date.Month+1,1);		
				}
				else{ 
					return new DateTime(date.Year+1,1,1);	
				}
			}
			else{
				return new DateTime(date.Year,date.Month,1);
			}
		}*/

		/*
		///<summary></summary>
		public static void ComputeAging(int guarantor){
			DateTime asOfDate;
			if(DateTime.Today.Day > 15){
				if(DateTime.Today.Month==12){
					asOfDate=new DateTime(DateTime.Today.Year+1,1,1);
				}
				else{
					asOfDate=new DateTime(DateTime.Today.Year,DateTime.Today.Month+1,1);	
				}
			}
			else{
				asOfDate=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
			}
			ComputeAging(guarantor,asOfDate);
			Patients.ResetAging(guarantor);
			Patients.UpdateAging(guarantor,Bal[0],Bal[1],Bal[2],Bal[3],InsEst,BalTotal);
		}*/

		///<summary>Computes aging for entire family.  Gets all info from database.</summary>
		public static void ComputeAging(int guarantor,DateTime asOfDate){
			AsOfDate=asOfDate;
			Bal=new double[4];
			Bal[0]=0;//0_30
			Bal[1]=0;//31_60
			Bal[2]=0;//61_90
			Bal[3]=0;//90plus
			BalTotal=0;
			InsEst=0;
			DateValuePair[] pairs;
			string wherePats="";
			ArrayList ALpatNums=new ArrayList();//used for payplans
			string command="SELECT PatNum FROM patient WHERE guarantor = '"+POut.PInt(guarantor)+"'";
			//MessageBox.Show(command);
			DataTable table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				ALpatNums.Add(PIn.PInt(table.Rows[i][0].ToString()));
				if(i>0) wherePats+=" OR";
				wherePats+=" PatNum = '"+table.Rows[i][0].ToString()+"'";
			}
			//REGULAR PROCEDURES:
			command="SELECT procdate,procfee,unitqty FROM procedurelog"
				+" WHERE procstatus = '2'"//complete
				+" AND ("+wherePats+")";
			table=General.GetTable(command);
			pairs=new DateValuePair[table.Rows.Count];
			double val;
			double qty;
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				val=PIn.PDouble(table.Rows[i][1].ToString());
				qty=PIn.PDouble(table.Rows[i][2].ToString());
				if(qty > 0) {
					val *= qty;
				}
				pairs[i].Value=val;
			}
			for(int i=0;i<pairs.Length;i++){
				Bal[GetAgingType(pairs[i].Date)]+=pairs[i].Value;
			}
			//POSITIVE ADJUSTMENTS:
			command="SELECT adjdate,adjamt FROM adjustment"
				+" WHERE adjamt > 0"
				+" AND ("+wherePats+")";
			table=General.GetTable(command);
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			for(int i=0;i<pairs.Length;i++){
				Bal[GetAgingType(pairs[i].Date)]+=pairs[i].Value;
			}
			//NEGATIVE ADJUSTMENTS:
			command="SELECT adjdate,adjamt FROM adjustment"
				+" WHERE adjamt < 0"
				+" AND ("+wherePats+")"
				+" ORDER BY adjdate";
			table=General.GetTable(command);
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= -PIn.PDouble(table.Rows[i][1].ToString());
			}
			ComputePayments(pairs);
			//CLAIM PAYMENTS AND CAPITATION WRITEOFFS:
			//Always use DateCP rather than ProcDate to calculate the date of a claim payment
			command="SELECT datecp,inspayamt,writeoff FROM claimproc"
				+" WHERE (status = '1' "//received
				+"OR status = '4'"//or supplemental
				+"OR status = '7'"//or CapComplete
				+"OR status = '5'"//or CapClaim
				+")"
				//pending insurance is handled further down
				//ins adjustments do not affect patient balance, but only insurance benefits
				+" AND ("+wherePats+")"
				+" ORDER BY datecp";
			table=General.GetTable(command);
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString())
					+PIn.PDouble(table.Rows[i][2].ToString());
			}
			ComputePayments(pairs);
			//PAYSPLITS:
			command="SELECT procdate,splitamt FROM paysplit"
				+" WHERE"
				+wherePats
				+" ORDER BY procdate";
			table=General.GetTable(command);
			pairs=new DateValuePair[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				pairs[i].Date=  PIn.PDate  (table.Rows[i][0].ToString());
				pairs[i].Value= PIn.PDouble(table.Rows[i][1].ToString());
			}
			ComputePayments(pairs);
			//PAYMENT PLANS:
			string whereGuars="";
			for(int i=0;i<ALpatNums.Count;i++){
				if(i>0)
					whereGuars+=" OR";
				whereGuars+=" Guarantor = '"+((int)ALpatNums[i]).ToString()+"'";
			}
			command="SELECT PatNum,Guarantor,Principal,Interest,ChargeDate FROM payplancharge"
				//"SELECT currentdue,totalamount,patnum,guarantor FROM payplan"
				+" WHERE"
				+wherePats
				+" OR"
				+whereGuars
				+" ORDER BY ChargeDate";
			table=General.GetTable(command);
			pairs=new DateValuePair[1];//always just one single combined entry
			pairs[0].Date=DateTime.Today;
			foreach(int patNum in ALpatNums){
				for(int i=0;i<table.Rows.Count;i++){
					//one or both of these conditions may be met:
					//if is guarantor
					if(PIn.PInt(table.Rows[i][1].ToString())==patNum){
						if(PIn.PDate(table.Rows[i][4].ToString())<=DateTime.Today){
							pairs[0].Value+=PIn.PDouble(table.Rows[i][2].ToString())
								+PIn.PDouble(table.Rows[i][3].ToString());
						}
					}
					//if is patient
					if(PIn.PInt(table.Rows[i][0].ToString())==patNum){
						pairs[0].Value-=PIn.PDouble(table.Rows[i][2].ToString());
					}
				}
			}
			if(pairs[0].Value>0)
				Bal[GetAgingType(pairs[0].Date)]+=pairs[0].Value;
			else if(pairs[0].Value<0){
				pairs[0].Value=-pairs[0].Value;
				ComputePayments(pairs);
			}
			//CLAIM ESTIMATES
			command="SELECT inspayest,writeoff FROM claimproc"
				+" WHERE status = '0'"//not received
				+" AND ("+wherePats+")";
			table=General.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				InsEst+=PIn.PDouble(table.Rows[i][0].ToString())+PIn.PDouble(table.Rows[i][1].ToString());
			}
			//balance is sum of 4 aging periods
			BalTotal=Bal[0]+Bal[1]+Bal[2]+Bal[3];
			//after this, balance will NOT necessarily be the same as the sum of the 4.
			//clean up negative numbers:
			if(Bal[3] < 0){
				Bal[2]+=Bal[3];
				Bal[3]=0;
			}
			if(Bal[2] < 0){
				Bal[1]+=Bal[2];
				Bal[2]=0;
			}
			if(Bal[1] < 0){
				Bal[0]+=Bal[1];
				Bal[1]=0;
			}
			if(Bal[0] < 0){
				Bal[0]=0;
			}
			//must complete by updating patient table. Done from wherever this was called.
		}

		///<summary>Called 4 times from the above function.  Not needed for charges, but only for payments, which are much more complex to place in the correct aging slot.  Hopefully, the complexity will be reduced when we have line item accounting, and every payment will have a proc date as well as a payment date.  Payment date will be used for all reports that show payments.  ProcDate will be used for aging.  This is already mostly in place for claimproc.ProcDate and claimproc.DateCP.</summary>
		private static void ComputePayments(DateValuePair[] pairs){
			//
			for(int i=0;i<pairs.Length;i++){
				switch(GetAgingType(pairs[i].Date)){
					case 3://over 90
						Bal[3]-=pairs[i].Value;//can go negative if patient balance was negative at some point
						break;
					case 2://60 90
						if(Bal[3]>0){//apply to older balance first
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;//apply all to over 90 bal
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];//deduct amount applied
								Bal[3]=0;//apply only part to over 90
							}
						}
						Bal[2]-=pairs[i].Value;//apply whatever is left over to 60 90
						break;
					case 1://30 60
						if(Bal[3]>0){//apply to older balance first
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];
								Bal[3]=0;
							}
						}
						if(Bal[2]>0){
							if(Bal[2]>pairs[i].Value){
								Bal[2]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[2];
								Bal[2]=0;
							}
						}
						Bal[1]-=pairs[i].Value;//apply whatever is left over to 30 60
						break;
					case 0://0 30
						if(Bal[3]>0){
							if(Bal[3]>pairs[i].Value){
								Bal[3]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[3];
								Bal[3]=0;
							}
						}
						if(Bal[2]>0){
							if(Bal[2]>pairs[i].Value){
								Bal[2]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[2];
								Bal[2]=0;
							}
						}
						if(Bal[1]>0){
							if(Bal[1]>pairs[i].Value){
								Bal[1]-=pairs[i].Value;
								pairs[i].Value=0;
							}
							else{
								pairs[i].Value-=Bal[1];
								Bal[1]=0;
							}
						}
						Bal[0]-=pairs[i].Value;//apply whatever is left over to 0 30
						break;
				}//switch
				//MessageBox.Show(pairs[i].Date.ToShortDateString()+","+pairs[i].Value.ToString()+","+Bal[3].ToString());
			}//for
		}

		private static int GetAgingType(DateTime date){
			//MessageBox.Show(AsOfDate.ToShortDateString()+","+date.ToShortDateString());
			int retVal;
			if(date < AsOfDate.AddMonths(-3))
				retVal= 3;
			else if(date < AsOfDate.AddMonths(-2))
				retVal= 2;
			else if(date < AsOfDate.AddMonths(-1))
				retVal= 1;
			else 
				retVal= 0;
			//MessageBox.Show(retVal.ToString());
			return retVal;
		}

	
	}


}













