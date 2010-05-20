using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	///<summary>These are only run from the Unit Testing framework</summary>
	public class WebServiceTests {
		public static string GetString(string str){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),str);
			}
			return str+"-Processed";
		}

		public static string GetStringNull(string str){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),str);
			}
			return null;
		}

		public static int GetInt(int intVal){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),intVal);
			}
			return 2;
		}

		public static long GetLong(long longVal){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),longVal);
			}
			return 2;
		}

		public static void GetVoid(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			return;
		}

		public static bool GetBool(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			return true;
		}

		public static Patient GetObjectPat(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod());
			}
			Patient pat=new Patient();
			pat.LName="Smith";
			pat.FName=null;
			return pat;
		}

		public static DataTable GetTable(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command="SELECT 'cell00'";
			DataTable table=Db.GetTable(command);
			return table;
		}

		public static DataSet GetDataSet(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod());
			}
			string command="SELECT 'cell00'";
			DataSet ds=Db.GetDataSet(command);
			ds.Tables[0].TableName="table0";
			return ds;
		}

		public static List<int> GetListInt(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<int>>(MethodBase.GetCurrentMethod());
			}
			List<int> listInt=new List<int>();
			listInt.Add(2);
			return listInt;
		}

		public static Patient[] GetArrayPatient(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient[]>(MethodBase.GetCurrentMethod());
			}
			Patient[] retVal=new Patient[2];
			retVal[0]=new Patient();
			retVal[0].LName="Jones";
			retVal[1]=null;
			return retVal;
		}

		public static string SendNullParam(string str){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),str);
			}
			if(str==null){
				return "nullOK";
			}
			else{
				return "null not found";
			}
		}

		public static Patient GetObjectNull(){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod());
			}
			return null;
		}

		public static Color SendColorParam(Color color){ 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Color>(MethodBase.GetCurrentMethod(),color);
			}
			if(color.ToArgb()==Color.Fuchsia.ToArgb()) {
				return Color.Green;
			}
			return Color.Red;//indicates error
		}
		


	}
}
