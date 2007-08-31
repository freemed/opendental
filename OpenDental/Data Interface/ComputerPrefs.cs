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
			ComputerPref computerPref=new ComputerPref();
			//OpenGL tooth chart not supported on Unix systems.
			if(Environment.OSVersion.Platform==PlatformID.Unix) {
				computerPref.GraphicsSimple=true;
			}
			//Default sensor values to start
			computerPref.SensorType="D";
			computerPref.SensorPort=0;
			computerPref.SensorExposure=1;
			computerPref.SensorBinned=false;
			try{
				table=General.GetTableEx(command);
			}catch{
				//In case of database error, just use default graphics settings so that it is possible for the program to start.
				return computerPref;
			}			
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
			computerPref.ComputerPrefNum=						PIn.PInt		(table.Rows[0][0].ToString());
			computerPref.ComputerName=							PIn.PString	(table.Rows[0][1].ToString());
			computerPref.GraphicsUseHardware=				PIn.PBool		(table.Rows[0][2].ToString());
			computerPref.GraphicsSimple=						PIn.PBool		(table.Rows[0][3].ToString());
			computerPref.SensorType=								PIn.PString	(table.Rows[0][4].ToString());
			computerPref.SensorBinned=							PIn.PBool		(table.Rows[0][5].ToString());
			computerPref.SensorPort=								PIn.PInt		(table.Rows[0][6].ToString());
			computerPref.SensorExposure=						PIn.PInt		(table.Rows[0][7].ToString());
			computerPref.GraphicsDoubleBuffering=		PIn.PBool		(table.Rows[0][8].ToString());
			computerPref.PreferredPixelFormatNum=		PIn.PInt		(table.Rows[0][9].ToString());
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
			command+="ComputerName,GraphicsUseHardware,GraphicsSimple,SensorType,SensorPort,SensorExposure,SensorBinned,GraphicsDoubleBuffering,PreferredPixelFormatNum) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(computerPref.ComputerPrefNum)+"',";
			}
			command+="'"+POut.PString(computerPref.ComputerName)+"',"
				+"'"+POut.PBool(computerPref.GraphicsUseHardware)+"',"
				+"'"+POut.PBool(computerPref.GraphicsSimple)+"',"
				+"'"+POut.PString(computerPref.SensorType)+"',"
				+"'"+POut.PBool(computerPref.SensorBinned)+"',"
				+"'"+POut.PInt(computerPref.SensorPort)+"',"
				+"'"+POut.PInt(computerPref.SensorExposure)+"',"
				+"'"+POut.PBool(computerPref.GraphicsDoubleBuffering)+"',"
				+"'"+POut.PInt(computerPref.PreferredPixelFormatNum)+"')";
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
				+"GraphicsSimple='"+POut.PBool(computerPref.GraphicsSimple)+"',"
				+"SensorType='"+POut.PString(computerPref.SensorType)+"',"
				+"SensorBinned='"+POut.PBool(computerPref.SensorBinned)+"',"
				+"SensorPort='"+POut.PInt(computerPref.SensorPort)+"',"
				+"SensorExposure='"+POut.PInt(computerPref.SensorExposure)+"',"
				+"GraphicsDoubleBuffering='"+POut.PBool(computerPref.GraphicsDoubleBuffering)+"',"
				+"PreferredPixelFormatNum='"+POut.PInt(computerPref.PreferredPixelFormatNum)+"' "
				+"WHERE ComputerPrefNum='"+POut.PInt(computerPref.ComputerPrefNum)+"'";
			return General.NonQ(command);
		}

	}
}
