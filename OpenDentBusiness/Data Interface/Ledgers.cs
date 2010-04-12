using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDentBusiness{

	///<summary>This does not correspond to any table in the database.  It works with a variety of tables to calculate aging.</summary>
	public class Ledgers{

		///<summary>This runs aging for all patients.  If using monthly aging, it always just runs the aging as of the last date again.  If using daily aging, it runs it as of today.  This logic used to be in FormAging, but is now centralized.</summary>
		public static void RunAging() {
			//No need to check RemotingRole; no call to db.
			if(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)) {
				ComputeAging(0,PrefC.GetDate(PrefName.DateLastAging),false);
			}
			else {
				ComputeAging(0,DateTime.Today,false);
				if(PrefC.GetDate(PrefName.DateLastAging) != DateTime.Today) {
					Prefs.UpdateString(PrefName.DateLastAging,POut.Date(DateTime.Today,false));
					//Since this is always called from UI, the above line works fine to keep the prefs cache current.
				}
			}
		}

		///<summary>Computes aging for the family specified. Specify guarantor=0 in order to calculate aging for all families.  
		///Gets all info from database. 
		///The aging calculation will use the following rules within each family: 
		///1) The aging "buckets" (0 to 30, 31 to 60, 61 to 90 and Over 90) ONLY include account activity on or
		///before AsOfDate.
		///2) BalTotal will always include all account activity, even future entries, except when in historical
		///mode, where BalTotal will exclude account activity after AsOfDate.
		///3) InsEst will always include all insurance estimates, even future estimates, except when in
		///historical mode where InsEst excludes insurance estimates after AsOfDate.
		///4) PayPlanDue will always include all payment plan charges minus credits, except when in
		///historical mode where PayPlanDue excludes payment plan charges and payments after AsOfDate.</summary>
		public static void ComputeAging(long guarantor,DateTime AsOfDate,bool historic) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),guarantor,AsOfDate,historic);
				return;
			}
			//Zero out either entire database or entire family.
			//Need to zero everything out first to catch former guarantors.
			string command="UPDATE patient SET "
				+"Bal_0_30   = 0"
				+",Bal_31_60 = 0"
				+",Bal_61_90 = 0"
				+",BalOver90 = 0"
				+",InsEst    = 0"
				+",BalTotal  = 0"
				+",PayPlanDue= 0";
			if(guarantor!=0) {
				command+=" WHERE Guarantor="+POut.Long(guarantor);
			}
			Db.NonQ(command);
			if(AsOfDate.Year<1880){
				AsOfDate=DateTime.Today;
			}
			string asOfDate=POut.Date(AsOfDate);
			string billInAdvanceDate=POut.Date(AsOfDate.AddDays(PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays)));
			if(historic){
				billInAdvanceDate=POut.Date(DateTime.Today.AddDays(PrefC.GetLong(PrefName.PayPlansBillInAdvanceDays)));
			}
			string thirtyDaysAgo=POut.Date(AsOfDate.AddDays(-30));
			string sixtyDaysAgo=POut.Date(AsOfDate.AddDays(-60));
			string ninetyDaysAgo=POut.Date(AsOfDate.AddDays(-90));
			string familyPatNums="";
			Collection <string> familyPatNumList=new Collection<string> ();
			if(guarantor!=0) {
				familyPatNums="(";
				command="SELECT p.PatNum FROM patient p WHERE p.Guarantor="+guarantor+";";
				DataTable tFamilyPatNums=Db.GetTable(command);
				for(int i=0;i<tFamilyPatNums.Rows.Count;i++) {
					if(i>0) {
						familyPatNums+=",";
					}
					string patNum=tFamilyPatNums.Rows[i][0].ToString();
					familyPatNums+=patNum;
					familyPatNumList.Add(patNum);
				}
				familyPatNums+=")";
			}
			//We use temporary tables using the "CREATE TEMPORARY TABLE" syntax here so that any temporary
			//tables created are specific to the current MySQL connection and no actual files are created
			//in the database. This will prevent rogue files from collecting in the live database, and will
			//prevent aging calculations on one computer from affecting the aging calculations on another computer.
			//Unfortunately, this has one side effect, which is that our MySQL connector reopens the MySQL
			//connection every time a command is run, so the temporary tables only last for a single MySQL
			//command. To get around this issue, we run the aging script as a single command/script.
			//Unfortunately, the "CREATE TEMPORARY TABLE" syntax gets replicated if MySQL replication is enabled,
			//which becomes a problem becauase the command is then no longer connection specific. Therefore,
			//to accomodate to the few offices using database replication, when creating the temporary aging tables,
			//we append a random string to the temporary table names so the possibility to temporary table
			//name collision is practically zero.
			//Create a temporary table to calculate aging into temporarily, so that the patient table is 
			//not being changed by multiple threads if more than one user is calculating aging.
			//Since a temporary table is dropped automatically only when the connection is closed,
			//and since we use connection pooling, drop them before using.
			string tempTableSuffix=CodeBase.MiscUtils.CreateRandomAlphaNumericString(32);
			string tempAgingTableName="tempaging"+tempTableSuffix;
			string tempOdAgingTransTableName="tempodagingtrans"+tempTableSuffix;
			command="DROP TEMPORARY TABLE IF EXISTS "+tempAgingTableName+", "+tempOdAgingTransTableName;
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS "+tempAgingTableName+", "+tempOdAgingTransTableName;
			Db.NonQ(command);
			command="CREATE TEMPORARY TABLE "+tempAgingTableName+" ("+
				"PatNum bigint,"+
				"Guarantor bigint,"+
				"Charges_0_30 DOUBLE DEFAULT 0,"+
				"Charges_31_60 DOUBLE DEFAULT 0,"+
				"Charges_61_90 DOUBLE DEFAULT 0,"+
				"ChargesOver90 DOUBLE DEFAULT 0,"+
				"TotalCredits DOUBLE DEFAULT 0,"+
				"InsEst DOUBLE DEFAULT 0,"+
				"PayPlanDue DOUBLE DEFAULT 0,"+
				"BalTotal DOUBLE DEFAULT 0"+
			");";
			if(guarantor==0) {
				//We insert all of the patient numbers and guarantor numbers only when we are running aging for everyone,
				//since we do not want MySQL to examine every patient record when running aging for a single family.
				command+="INSERT INTO "+tempAgingTableName+" (PatNum,Guarantor) "+
				"SELECT p.PatNum,p.Guarantor "+
					"FROM patient p;";
				//When there is only one patient that aging is being calculated for, then the indexes actually
				//slow the calculation down, but they significantly improve the speed when aging is being 
				//calculated for all familes.
				command+="ALTER TABLE "+tempAgingTableName+" ADD INDEX IDX_"+tempAgingTableName.ToUpper()+"_PATNUM (PatNum);";
				command+="ALTER TABLE "+tempAgingTableName+" ADD INDEX IDX_"+tempAgingTableName.ToUpper()+"_Guarantor (Guarantor);";
			}else{
				//Manually create insert statements to avoid having MySQL visit every patient record again.
				//In my testing, this saves about 0.25 seconds on an individual family aging calculation on my machine.
				command+="INSERT INTO "+tempAgingTableName+" (PatNum,Guarantor) VALUES ";
				for(int i=0;i<familyPatNumList.Count;i++){
					if(i>0){
						command+=",";
					}
					command+="("+familyPatNumList[i]+","+guarantor+")";
				}
				command+=";";
			}
			//Create another temporary table which holds a very concise summary of the entire office transaction history, 
			//so that all transactions can be treated as either a general credit or a general charge in the aging calculation.
			//Since we are recreating a temporary table with the same name as last time aging was run,
			//the old temporary table gets wiped out.
			command+="CREATE TEMPORARY TABLE "+tempOdAgingTransTableName+" ("+
					"PatNum bigint,"+
					"TranDate DATE DEFAULT '0001-01-01',"+
					"TranAmount DOUBLE DEFAULT 0"+
				");";
			//Get the completed procedure dates and charges for the entire office history.
			command+="INSERT INTO "+tempOdAgingTransTableName+" (PatNum,TranDate,TranAmount) "+
				"SELECT pl.PatNum PatNum,"+
						"pl.ProcDate TranDate,"+
						"pl.ProcFee*(pl.UnitQty+pl.BaseUnits) TranAmount "+
					"FROM procedurelog pl "+
					"WHERE pl.ProcStatus=2 "+
						(guarantor==0?"":(" AND pl.PatNum IN "+familyPatNums))+";";
			//Paysplits for the entire office history.
			command+="INSERT INTO "+tempOdAgingTransTableName+" (PatNum,TranDate,TranAmount) "+
				"SELECT ps.PatNum PatNum,"+
						"ps.ProcDate TranDate,"+
						"-ps.SplitAmt TranAmount "+
					"FROM paysplit ps "+
					"WHERE ps.PayPlanNum=0 "+//Only splits not attached to payment plans.
						(guarantor==0?"":(" AND ps.PatNum IN "+familyPatNums))+";";
			//Get the adjustment dates and amounts for the entire office history.
			command+="INSERT INTO "+tempOdAgingTransTableName+" (PatNum,TranDate,TranAmount) "+
				"SELECT a.PatNum PatNum,"+
						"a.AdjDate TranDate,"+
						"a.AdjAmt TranAmount "+
					"FROM adjustment a "+
					"WHERE a.AdjAmt<>0 "+
					(guarantor==0?"":(" AND a.PatNum IN "+familyPatNums))+";";
			//Claim payments and capitation writeoffs for the entire office history.
			command+="INSERT INTO "+tempOdAgingTransTableName+" (PatNum,TranDate,TranAmount) "+
				"SELECT cp.PatNum PatNum,"+
						"cp.DateCp TranDate,"+//Always use DateCP rather than ProcDate to calculate the date of a claim payment.
						"-cp.InsPayAmt-cp.Writeoff TranAmount "+
					"FROM claimproc cp "+
					"WHERE cp.status IN (1,4,5,7) "+//received, supplemental, CapClaim or CapComplete.
						(guarantor==0?"":(" AND cp.PatNum IN "+familyPatNums))+";";
			//Payment plan principal for the entire office history.
			command+="INSERT INTO "+tempOdAgingTransTableName+" (PatNum,TranDate,TranAmount) "+
				"SELECT pp.PatNum PatNum,"+
						"pp.PayPlanDate TranDate,"+
						"-pp.CompletedAmt TranAmount "+
					"FROM payplan pp "+
					"WHERE pp.CompletedAmt<>0 "+
					(guarantor==0?"":(" AND pp.PatNum IN "+familyPatNums))+";";
			//Now that we have all of the pertinent transaction history, we will calculate all of the charges for
			//the associated patients. 
			//Calculate over 90 day charges for all specified families.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total charges for each patient during this time period and 
				//place the results into memory table 'chargesOver90'.
				"(SELECT t.PatNum,SUM(t.TranAmount) TotalCharges FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount>0 AND t.TranDate<DATE("+ninetyDaysAgo+") GROUP BY t.PatNum) chargesOver90 "+
				//Update the tempaging table with the caculated charges for the time period.
				"SET a.ChargesOver90=chargesOver90.TotalCharges "+
				"WHERE a.PatNum=chargesOver90.PatNum;";
			//Calculate 61 to 90 day charges for all specified families.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total charges for each patient during this time period and 
				//place the results into memory table 'charges_61_90'.
				"(SELECT t.PatNum,SUM(t.TranAmount) TotalCharges FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount>0 AND t.TranDate<DATE("+sixtyDaysAgo+") AND "+
					"t.TranDate>=DATE("+ninetyDaysAgo+") GROUP BY t.PatNum) charges_61_90 "+
				//Update the tempaging table with the caculated charges for the time period.
				"SET a.Charges_61_90=charges_61_90.TotalCharges "+
				"WHERE a.PatNum=charges_61_90.PatNum;";
			//Calculate 31 to 60 day charges for all specified families.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total charges for each patient during this time period and 
				//place the results into memory table 'charges_31_60'.
				"(SELECT t.PatNum,SUM(t.TranAmount) TotalCharges FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount>0 AND t.TranDate<DATE("+thirtyDaysAgo+") AND "+
					"t.TranDate>=DATE("+sixtyDaysAgo+") GROUP BY t.PatNum) charges_31_60 "+
				//Update the tempaging table with the caculated charges for the time period.
				"SET a.Charges_31_60=charges_31_60.TotalCharges "+
				"WHERE a.PatNum=charges_31_60.PatNum;";
			//Calculate 0 to 30 day charges for all specified families.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total charges for each patient during this time period and 
				//place the results into memory table 'charges_0_30'.
				"(SELECT t.PatNum,SUM(t.TranAmount) TotalCharges FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount>0 AND t.TranDate<=DATE("+asOfDate+") AND "+
					"t.TranDate>=DATE("+thirtyDaysAgo+") GROUP BY t.PatNum) charges_0_30 "+
				//Update the tempaging table with the caculated charges for the time period.
				"SET a.Charges_0_30=charges_0_30.TotalCharges "+
				"WHERE a.PatNum=charges_0_30.PatNum;";
			//Calculate the total credits each patient has ever received so we can apply the credits to the aged charges below.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total credits for each patient and store the results in memory table 'credits'.
				"(SELECT t.PatNum,-SUM(t.TranAmount) TotalCredits FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount<0 AND t.TranDate<=DATE("+asOfDate+") GROUP BY t.PatNum) credits "+
				//Update the total credit for each patient into the tempaging table.
				"SET a.TotalCredits=credits.TotalCredits "+
				"WHERE a.PatNum=credits.PatNum;";
			//Calculate claim estimates for each patient individually on or before the specified date.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the insurance estimates for each patient and store the results into
				//memory table 't'.
				"(SELECT cp.PatNum,SUM(cp.InsPayEst+cp.Writeoff) InsEst "+
						"FROM claimproc cp "+
						"WHERE cp.PatNum<>0 "+
						(historic?(" AND ((cp.Status=0 AND cp.ProcDate<=DATE("+asOfDate+")) OR "+
							"(cp.Status=1 AND cp.DateCP>DATE("+asOfDate+"))) AND cp.ProcDate<=DATE("+asOfDate+") "):" AND cp.Status=0 ")+
							(guarantor==0?"":(" AND cp.PatNum IN "+familyPatNums+" "))+
						"GROUP BY cp.PatNum) t "+//not received claims.
				//Update the tempaging table with the insurance estimates for each patient.
				"SET a.InsEst=t.InsEst "+
				"WHERE a.PatNum=t.PatNum;";
			//Calculate the payment plan amount remaining to be paid for each patient 
			//as the amount owed due to payment plans, minus the payments made to payment plans.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Get payment plan patient amount due for the entire office history 
				//on or before the specified date (also considering the PayPlansBillInAdvanceDays setting)
				//and place the results in memory table 'c'.
				"(SELECT ppc.PatNum PatNum,"+
					"IFNULL(SUM(ppc.Principal+ppc.Interest),0) PayPlanCharges "+
					"FROM payplancharge ppc "+
					"WHERE ppc.PatNum<>0 "+
						(guarantor==0?"":(" AND ppc.PatNum IN "+familyPatNums+" "))+
						(historic?(" AND ppc.ChargeDate<=DATE("+billInAdvanceDate+") "):"")+
					"GROUP BY ppc.PatNum) c,"+
				//Get the payments made to payment plans for each patient
				//on or before the specified date and store the results in memory table 'p'.
				"(SELECT ps.PatNum PatNum,"+
					"IFNULL(SUM(ps.SplitAmt),0) PayPlanPayments "+
					"FROM paysplit ps "+
					"WHERE ps.PayPlanNum<>0 "+//only payments attached to payment plans.
						(guarantor==0?"":(" AND ps.PatNum IN "+familyPatNums+" "))+
						(historic?(" AND ps.ProcDate<=DATE("+asOfDate+") "):"")+
					"GROUP BY ps.PatNum "+
					"UNION "+
					//We have to include patients which have not made payments and represent
					//them with zero amounts so that the join below between tables a,c, and p
					//will work properly.
					"SELECT DISTINCT ppc2.PatNum,0 "+
					"FROM payplancharge ppc2 "+
					"LEFT JOIN paysplit ps2 ON ps2.PatNum=ppc2.patnum "+
					"WHERE ISNULL(ps2.PatNum) "+
					") p "+
				//Now using the tables tempaging, c and p, update the payment plan remaining
				//amount to be paid within the tempaging table for each patient.
				"SET a.PayPlanDue=IFNULL(c.PayPlanCharges-p.PayPlanPayments,0) "+
						"WHERE c.PatNum=a.PatNum AND p.PatNum=a.PatNum;";
			//Calculate the total balance for each patient.
			//In historical mode, only transactions on or before AsOfDate will be included.
			command+="UPDATE "+tempAgingTableName+" a,"+
				//Calculate the total balance for each patient and
				//place the results into memory table 'totals'.
				"(SELECT t.PatNum,SUM(t.TranAmount) BalTotal FROM "+tempOdAgingTransTableName+" t "+
					"WHERE t.TranAmount<>0 "+(historic?(" AND t.TranDate<=DATE("+asOfDate+")"):"")+" GROUP BY t.PatNum) totals "+
				//Update the tempaging table with the caculated charges for the time period.
				"SET a.BalTotal=totals.BalTotal "+
				"WHERE a.PatNum=totals.PatNum;";
			//Update the family aged balances onto the guarantor rows of the patient table 
			//by placing credits on oldest charges first, then on younger charges.
			command+="UPDATE patient p,"+
					//Sum each colum within each family group inside of the tempaging table so that we are now
					//using family amounts instead of individual patient amounts, and store the result into
					//memory table 'f'.
					"(SELECT a.Guarantor,SUM(a.Charges_0_30) Charges_0_30,SUM(a.Charges_31_60) Charges_31_60,"+
						"SUM(a.Charges_61_90) Charges_61_90,SUM(a.ChargesOver90) ChargesOver90,"+
						"SUM(TotalCredits) TotalCredits,SUM(InsEst) InsEst,SUM(PayPlanDue) PayPlanDue,"+
						"SUM(BalTotal) BalTotal "+
						"FROM "+tempAgingTableName+" a "+
						"GROUP BY a.Guarantor) f "+
					//Perform the update of the patient table based on the family amounts summed into table 'f', and
					//distribute the payments into the oldest balances first.
					"SET "+
					"p.BalOver90=CASE "+
						//over 90 balance paid in full.
						"WHEN f.TotalCredits>=f.ChargesOver90 THEN 0 "+
						//over 90 balance partially paid or unpaid.
						"ELSE f.ChargesOver90-f.TotalCredits END,"+			
					"p.Bal_61_90=CASE "+
						//61 to 90 day balance unpaid.
						"WHEN f.TotalCredits<=f.ChargesOver90 THEN f.Charges_61_90 "+
						//61 to 90 day balance paid in full.
						"WHEN f.ChargesOver90+f.Charges_61_90<=f.TotalCredits THEN 0 "+
						//61 to 90 day balance partially paid.
						"ELSE f.ChargesOver90+f.Charges_61_90-f.TotalCredits END,"+
					"p.Bal_31_60=CASE "+
						//31 to 60 day balance unpaid.
						"WHEN f.TotalCredits<f.ChargesOver90+f.Charges_61_90 THEN f.Charges_31_60 "+
						//31 to 60 day balance paid in full.
						"WHEN f.ChargesOver90+f.Charges_61_90+f.Charges_31_60<=f.TotalCredits THEN 0 "+
						//31 to 60 day balance partially paid.
						"ELSE f.ChargesOver90+f.Charges_61_90+f.Charges_31_60-f.TotalCredits END,"+
					"p.Bal_0_30=CASE "+
						//0 to 30 day balance unpaid.
						"WHEN f.TotalCredits<f.ChargesOver90+f.Charges_61_90+f.Charges_31_60 THEN f.Charges_0_30 "+
						//0 to 30 day balance paid in full.
						"WHEN f.ChargesOver90+f.Charges_61_90+f.Charges_31_60+f.Charges_0_30<=f.TotalCredits THEN 0 "+
						//0 to 30 day balance partially paid.
						"ELSE f.ChargesOver90+f.Charges_61_90+f.Charges_31_60+f.Charges_0_30-f.TotalCredits END,"+
					"p.BalTotal=f.BalTotal,"+
					"p.InsEst=f.InsEst,"+
					"p.PayPlanDue=f.PayPlanDue "+
				"WHERE p.PatNum=f.Guarantor;";//Aging calculations only apply to guarantors.
			Db.NonQ(command);
			command="DROP TEMPORARY TABLE IF EXISTS "+tempAgingTableName+", "+tempOdAgingTransTableName;
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS "+tempAgingTableName+", "+tempOdAgingTransTableName;
			Db.NonQ(command);
		}
	}

}