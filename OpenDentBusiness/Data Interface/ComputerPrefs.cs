using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Reflection;
using CodeBase;

namespace OpenDentBusiness {
	public class ComputerPrefs {

		///<summary>Returns the computer preferences for the computer which this instance of Open Dental is running on.</summary>
		public static ComputerPref GetForLocalComputer(){
			//No need to check RemotingRole; no call to db.
			string computerName=Dns.GetHostName();//local computer name
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
			//Default values to start
			computerPref.AtoZpath="";
			computerPref.TaskKeepListHidden=false; //show docked task list on this computer 
			computerPref.TaskDock=0; //bottom
			computerPref.TaskX=900;
			computerPref.TaskY=625;
			DataTable table=GetPrefsForComputer(computerName);
			if(table==null){
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
			computerPref.ComputerPrefNum=				PIn.PInt		(table.Rows[0][0].ToString());
			computerPref.ComputerName=					PIn.PString		(table.Rows[0][1].ToString());
			computerPref.GraphicsUseHardware=			PIn.PBool		(table.Rows[0][2].ToString());
			computerPref.GraphicsSimple=				PIn.PBool		(table.Rows[0][3].ToString());
			computerPref.SensorType=					PIn.PString		(table.Rows[0][4].ToString());
			computerPref.SensorBinned=					PIn.PBool		(table.Rows[0][5].ToString());
			computerPref.SensorPort=					PIn.PInt		(table.Rows[0][6].ToString());
			computerPref.SensorExposure=				PIn.PInt		(table.Rows[0][7].ToString());
			computerPref.GraphicsDoubleBuffering=		PIn.PBool		(table.Rows[0][8].ToString());
			computerPref.PreferredPixelFormatNum=		PIn.PInt		(table.Rows[0][9].ToString());
			computerPref.AtoZpath=						PIn.PString		(table.Rows[0][10].ToString());
			computerPref.TaskKeepListHidden=			PIn.PBool		(table.Rows[0][11].ToString());
			computerPref.TaskDock=						PIn.PInt		(table.Rows[0][12].ToString());
			computerPref.TaskX=							PIn.PInt		(table.Rows[0][13].ToString());
			computerPref.TaskY=							PIn.PInt		(table.Rows[0][14].ToString());
			return computerPref;
		}

		public static DataTable GetPrefsForComputer(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),computerName);
			}
			string command="SELECT * FROM computerpref WHERE ComputerName='"+POut.PString(computerName)+"'";
			try {
				return Db.GetTable(command);
			} catch {
				return null;
			}
		}

		///<summary>Inserts the given preference and ensures that the primary key is properly set.</summary>
		public static int Insert(ComputerPref computerPref){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				computerPref.ComputerPrefNum=Meth.GetInt(MethodBase.GetCurrentMethod(),computerPref);
				return computerPref.ComputerPrefNum;
			}
			if(PrefC.RandomKeys) {
				computerPref.ComputerPrefNum=MiscData.GetKey("computerpref","ComputerPrefNum");
			}
			string command="INSERT INTO computerpref (";
			if(PrefC.RandomKeys){
				command+="ComputerPrefNum,";
			}			
			command+="ComputerName,GraphicsUseHardware,GraphicsSimple,SensorType,SensorPort,SensorExposure,SensorBinned,"
				+ "GraphicsDoubleBuffering,PreferredPixelFormatNum,AtoZpath,TaskKeepListHidden,TaskDock,TaskX,TaskY) VALUES(";
			if(PrefC.RandomKeys){
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
				+"'"+POut.PInt(computerPref.PreferredPixelFormatNum)+"',"
				+"'"+POut.PString(computerPref.AtoZpath)+"',"
				+"'"+POut.PBool(computerPref.TaskKeepListHidden)+"',"
				+"'"+POut.PInt(computerPref.TaskDock)+"',"
				+"'"+POut.PInt(computerPref.TaskX)+"',"
				+"'"+POut.PInt(computerPref.TaskY)+"')";
			if(PrefC.RandomKeys)
			{
				Db.NonQ(command);
			}else{
				computerPref.ComputerPrefNum=Db.NonQ(command,true);
			}
			return computerPref.ComputerPrefNum;
		}

		public static int Update(ComputerPref computerPref){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),computerPref);
			}
			string command="UPDATE computerpref SET "
				+"ComputerName='"+POut.PString(computerPref.ComputerName)+"',"
				+"GraphicsUseHardware='"+POut.PBool(computerPref.GraphicsUseHardware)+"',"
				+"GraphicsSimple='"+POut.PBool(computerPref.GraphicsSimple)+"',"
				+"SensorType='"+POut.PString(computerPref.SensorType)+"',"
				+"SensorBinned='"+POut.PBool(computerPref.SensorBinned)+"',"
				+"SensorPort='"+POut.PInt(computerPref.SensorPort)+"',"
				+"SensorExposure='"+POut.PInt(computerPref.SensorExposure)+"',"
				+"GraphicsDoubleBuffering='"+POut.PBool(computerPref.GraphicsDoubleBuffering)+"',"
				+"PreferredPixelFormatNum='"+POut.PInt(computerPref.PreferredPixelFormatNum)+"',"
				+"AtoZpath='"+POut.PString(computerPref.AtoZpath)+"',"
				+"TaskKeepListHidden='"+POut.PBool(computerPref.TaskKeepListHidden)+"',"
				+"TaskDock='"+POut.PInt(computerPref.TaskDock)+"',"
				+"TaskX='"+POut.PInt(computerPref.TaskX)+"',"
				+"TaskY='"+POut.PInt(computerPref.TaskY)+"' "
				+"WHERE ComputerPrefNum='"+POut.PInt(computerPref.ComputerPrefNum)+"'";
			return Db.NonQ(command);
		}

		//public static int Update(ComputerPref computerPref) {
		//	return 0;
		//}

	}

	
}
