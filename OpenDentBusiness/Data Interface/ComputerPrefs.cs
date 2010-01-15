using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Reflection;
using CodeBase;

namespace OpenDentBusiness {
	public class ComputerPrefs {
		//public static ComputerPref ForLocalComputer;//js Maybe do this later for speed.

		///<summary>Returns the computer preferences for the computer which this instance of Open Dental is running on.</summary>
		public static ComputerPref GetForLocalComputer(){
			//No need to check RemotingRole; no call to db.
			string computerName=Dns.GetHostName();//local computer name
			ComputerPref computerPref=new ComputerPref();
			//OpenGL tooth chart not supported on Unix systems.
			if(Environment.OSVersion.Platform==PlatformID.Unix) {
				computerPref.GraphicsSimple=DrawingMode.Simple2D;
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
					+POut.String(computerName)+"' is a primary key in multiple records. Please run the "
					+"database maintenance tool, then call us for help if you still get this message.",
					Logger.Severity.WARNING);
			}
			computerPref.ComputerPrefNum=				PIn.Long		(table.Rows[0][0].ToString());
			computerPref.ComputerName=					PIn.String		(table.Rows[0][1].ToString());
			computerPref.GraphicsUseHardware=			PIn.Bool		(table.Rows[0][2].ToString());
			computerPref.GraphicsSimple=(DrawingMode)PIn.Int  (table.Rows[0][3].ToString());
			computerPref.SensorType=					PIn.String		(table.Rows[0][4].ToString());
			computerPref.SensorBinned=					PIn.Bool		(table.Rows[0][5].ToString());
			computerPref.SensorPort=					PIn.Int		(table.Rows[0][6].ToString());
			computerPref.SensorExposure=				PIn.Int		(table.Rows[0][7].ToString());
			computerPref.GraphicsDoubleBuffering=		PIn.Bool		(table.Rows[0][8].ToString());
			computerPref.PreferredPixelFormatNum=		PIn.Int		(table.Rows[0][9].ToString());
			computerPref.AtoZpath=						PIn.String		(table.Rows[0][10].ToString());
			computerPref.TaskKeepListHidden=			PIn.Bool		(table.Rows[0][11].ToString());
			computerPref.TaskDock=						PIn.Int		(table.Rows[0][12].ToString());
			computerPref.TaskX=							PIn.Int		(table.Rows[0][13].ToString());
			computerPref.TaskY=							PIn.Int		(table.Rows[0][14].ToString());
			computerPref.DirectXFormat=			PIn.String(table.Rows[0][15].ToString());
			return computerPref;
		}

		public static DataTable GetPrefsForComputer(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),computerName);
			}
			string command="SELECT * FROM computerpref WHERE ComputerName='"+POut.String(computerName)+"'";
			try {
				return Db.GetTable(command);
			} catch {
				return null;
			}
		}

		///<summary>Inserts the given preference and ensures that the primary key is properly set.</summary>
		public static long Insert(ComputerPref computerPref) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				computerPref.ComputerPrefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),computerPref);
				return computerPref.ComputerPrefNum;
			}
			if(PrefC.RandomKeys) {
				computerPref.ComputerPrefNum=ReplicationServers.GetKey("computerpref","ComputerPrefNum");
			}
			string command="INSERT INTO computerpref (";
			if(PrefC.RandomKeys){
				command+="ComputerPrefNum,";
			}			
			command+="ComputerName,GraphicsUseHardware,GraphicsSimple,SensorType,SensorPort,SensorExposure,SensorBinned,"
				+ "GraphicsDoubleBuffering,PreferredPixelFormatNum,AtoZpath,TaskKeepListHidden,TaskDock,TaskX,TaskY,DirectXFormat) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(computerPref.ComputerPrefNum)+"',";
			}
			command+="'"+POut.String(computerPref.ComputerName)+"',"
				+"'"+POut.Bool(computerPref.GraphicsUseHardware)+"',"
				+"'"+POut.Int((int)computerPref.GraphicsSimple)+"',"
				+"'"+POut.String(computerPref.SensorType)+"',"
				+"'"+POut.Bool(computerPref.SensorBinned)+"',"
				+"'"+POut.Long(computerPref.SensorPort)+"',"
				+"'"+POut.Long(computerPref.SensorExposure)+"',"
				+"'"+POut.Bool(computerPref.GraphicsDoubleBuffering)+"',"
				+"'"+POut.Long(computerPref.PreferredPixelFormatNum)+"',"
				+"'"+POut.String(computerPref.AtoZpath)+"',"
				+"'"+POut.Bool(computerPref.TaskKeepListHidden)+"',"
				+"'"+POut.Long(computerPref.TaskDock)+"',"
				+"'"+POut.Long(computerPref.TaskX)+"',"
				+"'"+POut.Long(computerPref.TaskY)+"',"
				+"'"+POut.String(computerPref.DirectXFormat)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				computerPref.ComputerPrefNum=Db.NonQ(command,true);
			}
			return computerPref.ComputerPrefNum;
		}

		public static long Update(ComputerPref computerPref) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),computerPref);
			}
			string command="UPDATE computerpref SET "
				+"ComputerName='"+POut.String(computerPref.ComputerName)+"',"
				+"GraphicsUseHardware='"+POut.Bool(computerPref.GraphicsUseHardware)+"',"
				+"GraphicsSimple='"+POut.Int((int)computerPref.GraphicsSimple)+"',"
				+"SensorType='"+POut.String(computerPref.SensorType)+"',"
				+"SensorBinned='"+POut.Bool(computerPref.SensorBinned)+"',"
				+"SensorPort='"+POut.Long(computerPref.SensorPort)+"',"
				+"SensorExposure='"+POut.Long(computerPref.SensorExposure)+"',"
				+"GraphicsDoubleBuffering='"+POut.Bool(computerPref.GraphicsDoubleBuffering)+"',"
				+"PreferredPixelFormatNum='"+POut.Long(computerPref.PreferredPixelFormatNum)+"',"
				+"AtoZpath='"+POut.String(computerPref.AtoZpath)+"',"
				+"TaskKeepListHidden='"+POut.Bool(computerPref.TaskKeepListHidden)+"',"
				+"TaskDock='"+POut.Long(computerPref.TaskDock)+"',"
				+"TaskX='"+POut.Long(computerPref.TaskX)+"',"
				+"TaskY='"+POut.Long(computerPref.TaskY)+"',"
				+"DirectXFormat='"+POut.String(computerPref.DirectXFormat)+"' "
				+"WHERE ComputerPrefNum='"+POut.Long(computerPref.ComputerPrefNum)+"'";
			return Db.NonQ(command);
		}

		//public static int Update(ComputerPref computerPref) {
		//	return 0;
		//}

	}

	
}
