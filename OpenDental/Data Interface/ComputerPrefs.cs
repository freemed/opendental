using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;
using CodeBase;
using System.Net;

namespace OpenDental {
	public class ComputerPrefs {

		///<summary>Returns the computer preferences for the computer which this instance of Open Dental is running on.</summary>
		public static ComputerPref GetForLocalComputer(){
			string computerName=Dns.GetHostName();//local computer name
			string command="SELECT * FROM computerpref WHERE ComputerName='"+POut.PString(computerName)+"'";
			DataTable table;
			try{
				table=General.GetTableEx(command);
			}catch{
				//In case of database error, just use default graphics settings so that it is possible for the program to start.
				return new ComputerPref();
			}
			ComputerPref computerPref=new ComputerPref();
			if(table.Rows.Count==0){//Computer prefs do not exist yet.
				computerPref.ComputerName=computerName;
				Insert(computerPref);//Create default prefs for the specified computer. Also sets primary key in our computerPref object.
				return computerPref;
			}
			if(table.Rows.Count>1){
				//Should never happen (would only happen if the primary key showed up more than once).
				Logger.openlog.LogMB("Corrupt computerpref table in database. The computer name '"
					+POut.PString(computerName)+"' is a primary key in multiple records. Please run the "
					+"database maintenance tool, then call us for help if you still get this message.",
					Logger.Severity.WARNING);
			}
			computerPref.ComputerPrefNum=			PIn.PInt		(table.Rows[0][0].ToString());
			computerPref.ComputerName=				PIn.PString	(table.Rows[0][1].ToString());
			computerPref.GraphicsUseHardware=	PIn.PBool		(table.Rows[0][2].ToString());
			computerPref.GraphicsSimple=			PIn.PBool		(table.Rows[0][3].ToString());
			return computerPref;
		}

		///<summary>Inserts the given preference and ensures that the primary key is properly set.</summary>
		public static void Insert(ComputerPref computerPref){
			if(PrefB.RandomKeys) {
				computerPref.ComputerPrefNum=MiscData.GetKey("computerpref","ComputerPrefNum");
			}
			string command="INSERT INTO computerpref (";
			if(PrefB.RandomKeys){
				command+="ComputerPrefNum,";
			}			
			command+="ComputerName,GraphicsUseHardware,GraphicsSimple) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(computerPref.ComputerPrefNum)+"',";
			}
			command+="'"+POut.PString(computerPref.ComputerName)+"',"
				+"'"+POut.PBool(computerPref.GraphicsUseHardware)+"',"
				+"'"+POut.PBool(computerPref.GraphicsSimple)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}else{
				computerPref.ComputerPrefNum=General.NonQ(command,true);
			}
		}

		public static int Update(ComputerPref computerPref){
			string command="UPDATE computerpref SET "
				+"ComputerName='"+POut.PString(computerPref.ComputerName)+"',"
				+"GraphicsUseHardware='"+POut.PBool(computerPref.GraphicsUseHardware)+"',"
				+"GraphicsSimple='"+POut.PBool(computerPref.GraphicsSimple)+"' "
				+"WHERE ComputerPrefNum='"+POut.PInt(computerPref.ComputerPrefNum)+"'";
			return General.NonQ(command);
		}

	}
}
