using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Net;

namespace OpenDental{
	
	///<summary>Miscellaneous database functions.</summary>
	public class MiscData{

		private static int numComputers=0;
		private static int myComputerNum=0;//One-based unique computer number index. Used to decide which key-partition to use for this computer.
		private static int myPartitionStart=0;
		private static int myPartitionEnd=0;

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
			//Calculate the primary key range for this computer if it has not already calculated.
			if(numComputers==0 || myComputerNum==0){
				try {
					string command="SELECT COUNT(*) FROM computer";
					DataTable result=General.GetTable(command);
					numComputers=PIn.PInt(result.Rows[0][0].ToString());
					command="SELECT COUNT(*) FROM computer WHERE ComputerNum<=(SELECT ComputerNum FROM computer AS temp WHERE CompName "+
						"like '"+Dns.GetHostName()+"')";
					result=General.GetTable(command);
					myComputerNum=PIn.PInt(result.Rows[0][0].ToString());
				} catch {
					//This computer has not yet been added to the computer table. Generate any old random number as long as it is unique.
					//This is the first introduction of the computer into the cluster.
					numComputers=1;
					myComputerNum=1;
				}
				int keymaxval=16777215;
				int partitionSize=keymaxval/numComputers;//truncation here is good (to avoid partition overflow).
				myPartitionStart=(myComputerNum-1)*partitionSize+1;
				myPartitionEnd=myPartitionStart+partitionSize-1;
			}
			Random random=new Random();
			int rnd;
			do {
				rnd=random.Next(myPartitionStart,myPartitionEnd);
			} while(rnd==0||KeyInUse(tablename,field,rnd));
			return rnd;
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































