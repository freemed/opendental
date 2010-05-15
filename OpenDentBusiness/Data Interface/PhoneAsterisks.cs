using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary>This entire class is only used at Open Dental, Inc HQ.  So for that special environment, many things are hard-coded.</summary>
	public class PhoneAsterisks {

		public static void SetToDefaultRingGroups(int extension,long employeeNum) {
			//query asterisk(user opendental, pass secret), db asterisk, table ringgroups
			DataConnection dcon=new DataConnection("asterisk","asterisk","opendental","secret",DatabaseType.MySql);

		}

		public static void BackupRingGroupOnly(int extension,long employeeNum) {
			
		}

		public static void RemoveFromRingGroups(int extension,long employeeNum) {
			
		}

		///<summary>For a given date, gets a list of dateTimes of missed calls.  Gets directly from the Asterisk database, hard-coded.</summary>
		public static List<DateTime> GetMissedCalls(DateTime date) {
			DataConnection dcon=new DataConnection("asterisk","asteriskcdrdb","opendental","secret",DatabaseType.MySql);
			string command="SELECT calldate FROM cdr WHERE DATE(calldate) = "+POut.Date(date)+" "
				+"AND (dcontext='ext-group' OR dcontext='ext-local') AND dst='vmu998'";
			List<DateTime> retVal=new List<DateTime>();
			DataTable table=dcon.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(PIn.DateT(table.Rows[i][0].ToString()));
			}
			return retVal;
		}
		


	}



}



