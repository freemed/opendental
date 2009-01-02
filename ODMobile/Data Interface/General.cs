using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentMobile {
	public class General {
		public static void NonQ(string command){
			//DataConnection dcon=new DataConnection();
			Dcon.NonQ(command);
		}

		public static DataTable GetTable(string command){
			//DataConnection dcon=new DataConnection();
			return Dcon.GetTable(command);
		}

	}
}
