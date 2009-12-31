using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Programs{

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM program ORDER BY ProgDesc";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Program";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			ProgramC.HList=new Hashtable();
			Program prog = new Program();
			ProgramC.Listt=new List<Program>();
			for (int i=0;i<table.Rows.Count;i++){
				prog=new Program();
				prog.ProgramNum =PIn.Long(table.Rows[i][0].ToString());
				prog.ProgName   =PIn.String(table.Rows[i][1].ToString());
				prog.ProgDesc   =PIn.String(table.Rows[i][2].ToString());
				prog.Enabled    =PIn.Bool(table.Rows[i][3].ToString());
				prog.Path       =PIn.String(table.Rows[i][4].ToString());
				prog.CommandLine=PIn.String(table.Rows[i][5].ToString());
				prog.Note       =PIn.String(table.Rows[i][6].ToString());
				prog.PluginDllName=PIn.String(table.Rows[i][7].ToString());
				ProgramC.Listt.Add(prog);
				if(!ProgramC.HList.ContainsKey(prog.ProgName)) {
					ProgramC.HList.Add(prog.ProgName,prog);
				}
			}
		}

		///<summary></summary>
		public static void Update(Program Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE program SET "
				+"ProgName = '"     +POut.String(Cur.ProgName)+"'"
				+",ProgDesc  = '"   +POut.String(Cur.ProgDesc)+"'"
				+",Enabled  = '"    +POut.Bool  (Cur.Enabled)+"'"
				+",Path = '"        +POut.String(Cur.Path)+"'"
				+",CommandLine  = '"+POut.String(Cur.CommandLine)+"'"
				+",Note  = '"       +POut.String(Cur.Note)+"'"
				+",PluginDllName  = '"+POut.String(Cur.PluginDllName)+"'"
				+" WHERE programnum = '"+POut.Long(Cur.ProgramNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Program Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ProgramNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ProgramNum;
			}
			if(PrefC.RandomKeys) {
				Cur.ProgramNum=ReplicationServers.GetKey("program","ProgramNum");
			}
			string command="INSERT INTO program (";
			if(PrefC.RandomKeys) {
				command+="ProgramNum,";
			}
			command+="ProgName,ProgDesc,Enabled,Path,CommandLine,Note,PluginDllName) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(Cur.ProgramNum)+", ";
			}
			command+=
				 "'"+POut.String(Cur.ProgName)+"', "
				+"'"+POut.String(Cur.ProgDesc)+"', "
				+"'"+POut.Bool  (Cur.Enabled)+"', "
				+"'"+POut.String(Cur.Path)+"', "
				+"'"+POut.String(Cur.CommandLine)+"', "
				+"'"+POut.String(Cur.Note)+"', "
				+"'"+POut.String(Cur.PluginDllName)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				Cur.ProgramNum=Db.NonQ(command, true);
			}
			return Cur.ProgramNum;
		}

		///<summary>This can only be called by the user if it is a program link that they created. Included program links cannot be deleted.  If calling this from ClassConversion, must delete any dependent ProgramProperties first.  It will delete ToolButItems for you.</summary>
		public static void Delete(Program prog){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prog);
				return;
			}
			string command = "DELETE from toolbutitem WHERE ProgramNum = "+POut.Long(prog.ProgramNum);
			Db.NonQ(command);
			command = "DELETE from program WHERE ProgramNum = '"+prog.ProgramNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>Returns true if a Program link with the given name or number exists and is enabled.</summary>
		public static bool IsEnabled(string progName){
			//No need to check RemotingRole; no call to db.
			if(ProgramC.HList==null) {
				Programs.RefreshCache();
			}
			if(ProgramC.HList.ContainsKey(progName) && ((Program)ProgramC.HList[progName]).Enabled) {
				return true;
			}
			return false;
		}

		///<summary></summary>
		public static bool IsEnabled(long programNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgramNum==programNum && ProgramC.Listt[i].Enabled) {
					return true;
				}
			}
			return false;
		}

		///<summary>Supply a valid program Name, and this will set Cur to be the corresponding Program object.</summary>
		public static Program GetCur(string progName){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgName==progName) {
					return ProgramC.Listt[i];
				}
			}
			return null;//to signify that the program could not be located. (user deleted it in an older version)
		}

		///<summary>Supply a valid program Name.  Will return 0 if not found.</summary>
		public static long GetProgramNum(string progName) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProgramC.Listt.Count;i++) {
				if(ProgramC.Listt[i].ProgName==progName) {
					return ProgramC.Listt[i].ProgramNum;
				}
			}
			return 0;
		}

		




	}

	

	


}










