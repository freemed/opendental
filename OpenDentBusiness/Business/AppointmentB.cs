using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{
	public class AppointmentB {
		///<Summary>Parameters: 1:AptNum</Summary>
		public static DataSet GetApptEdit(string[] parameters){
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetApptTable(PIn.PInt(parameters[0])));
			return retVal;
		}

		private static DataTable GetApptTable(int patNum){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("Appt");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("IsNewPatient");
			row=table.NewRow();
			row["IsNewPatient"]="0";
			table.Rows.Add(row);
			return table;
		}
	}
}
