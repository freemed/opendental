using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPeriods {
		///<summary>A list of all payperiods.</summary>
		private static PayPeriod[] list;

		public static PayPeriod[] List {
			get {
				if(list==null) {
					Refresh();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>Fills List with all payperiods, ordered by startdate.</summary>
		public static void Refresh() {
			string command="SELECT * from payperiod ORDER BY DateStart";
			DataTable table=General.GetTable(command);
			List=new PayPeriod[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new PayPeriod();
				List[i].PayPeriodNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].DateStart    = PIn.PDate(table.Rows[i][1].ToString());
				List[i].DateStop     = PIn.PDate(table.Rows[i][2].ToString());
				List[i].DatePaycheck = PIn.PDate(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(PayPeriod pp) {
			if(PrefC.RandomKeys) {
				pp.PayPeriodNum=MiscData.GetKey("payperiod","PayPeriodNum");
			}
			string command="INSERT INTO payperiod (";
			if(PrefC.RandomKeys) {
				command+="PayPeriodNum,";
			}
			command+="DateStart,DateStop,DatePaycheck) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(pp.PayPeriodNum)+"', ";
			}
			command+=
				 POut.PDate  (pp.DateStart)+", "
				+POut.PDate  (pp.DateStop)+", "
				+POut.PDate  (pp.DatePaycheck)+")";
			if(PrefC.RandomKeys) {
				General.NonQ(command);
			}
			else {
				pp.PayPeriodNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(PayPeriod pp) {
			string command= "UPDATE payperiod SET "
				+"DateStart = "    +POut.PDate  (pp.DateStart)+" "
				+",DateStop = "    +POut.PDate  (pp.DateStop)+" "
				+",DatePaycheck = "+POut.PDate  (pp.DatePaycheck)+" "
				+"WHERE PayPeriodNum = '"+POut.PInt(pp.PayPeriodNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(PayPeriod pp) {
			string command= "DELETE FROM payperiod WHERE PayPeriodNum = "+POut.PInt(pp.PayPeriodNum);
			General.NonQ(command);
		}

		///<summary></summary>
		public static int GetForDate(DateTime date){
			for(int i=0;i<List.Length;i++){
				if(date.Date >= List[i].DateStart.Date && date.Date <= List[i].DateStop.Date){
					return i;
				}
			}
			return List.Length-1;//if we can't find a match, just return the last index
		}
		




	}

	
}




