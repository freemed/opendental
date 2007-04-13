using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary>Miscellaneous database functions.</summary>
	public class MiscData{

		///<summary>This throws an exception if it fails for any reason.</summary>
		public static void MakeABackup() {
			//try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					MiscDataB.MakeABackup();
				}
				else {
					DtoMiscDataMakeABackup dto=new DtoMiscDataMakeABackup();
					RemotingClient.ProcessCommand(dto);
				}
			//}
			//catch(Exception e) {
			//	MessageBox.Show(e.Message);
			//}
		}

		///<summary>Gets the current date/Time direcly from the server.  Mostly used to prevent uesr from altering the workstation date to bypass security.</summary>
		public static DateTime GetNowDateTime(){
			string command="SELECT NOW()";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				command="SELECT CURRENT_TIMESTAMP FROM DUAL";
			}
			DataTable table=General.GetTable(command);
			return PIn.PDateT(table.Rows[0][0].ToString());
		}

		///<summary>Generates a random primary key.  Tests to see if that key already exists before returning it for use.  Currently, the range of returned values is greater than 0, and less than or equal to 16777215, the limit for mysql medium int.  This will eventually change to a max of 18446744073709551615.  Then, the return value would have to be a ulong and the mysql type would have to be bigint.</summary>
		public static int GetKey(string tablename,string field){
			Random random=new Random();
			double rnd=random.NextDouble();
			while(rnd==0 || KeyInUse(tablename,field,(int)(rnd*16777215))){
				rnd=random.NextDouble();
			}
			return (int)(rnd*16777215);
		}

		private static bool KeyInUse(string tablename,string field,int keynum){
			string command="SELECT COUNT(*) FROM "+tablename+" WHERE "+field+"="+keynum.ToString();
			if(General.GetCount(command)=="0"){
				return false;
			}
			return true;//already in use
		}

		public static string GetCurrentDatabase() {
			string command="SELECT database()";
			DataTable table=General.GetTable(command);
			return PIn.PString(table.Rows[0][0].ToString());
		}

		
	}
 
	

	
}































