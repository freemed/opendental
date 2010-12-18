using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using OpenDentBusiness;

namespace WebHostSynch {
	public class Db {

		private  DataConnection dcon;

		public void setConn(string connectStr) {
			dcon = new DataConnection(connectStr,true);
		}

		public DataTable GetTable(string command) {
			DataTable table=dcon.GetTable(command);
			return table;//retVal;
		}
		public long NonQ(string command,bool getInsertID) {
			long rowsChanged=dcon.NonQ(command,getInsertID);
			if(getInsertID) {
				return (long)dcon.InsertID;
			}
			else {
				return rowsChanged;
			}
		}

		public long NonQ(string command) {
			return NonQ(command,false);
		}

	}
}