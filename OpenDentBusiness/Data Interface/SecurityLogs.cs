using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SecurityLogs {

		///<summary>Used when viewing securityLog from the security admin window.  PermTypes can be length 0 to get all types.</summary>
		public static SecurityLog[] Refresh(DateTime dateFrom,DateTime dateTo,Permissions permType,long patNum,long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SecurityLog[]>(MethodBase.GetCurrentMethod(),dateFrom,dateTo,permType,patNum,userNum);
			}
			string command="SELECT securitylog.*,LName,FName,Preferred,MiddleI,LogHash FROM securitylog "
				+"LEFT JOIN patient ON patient.PatNum=securitylog.PatNum "
				+"LEFT JOIN securityloghash ON securityloghash.SecurityLogNum=securitylog.SecurityLogNum "
				+"WHERE LogDateTime >= "+POut.Date(dateFrom)+" "
				+"AND LogDateTime <= "+POut.Date(dateTo.AddDays(1));
			if(patNum !=0) {
				command+=" AND securitylog.PatNum= '"+POut.Long(patNum)+"'";
			}
			if(permType!=Permissions.None) {
				command+=" AND PermType="+POut.Long((int)permType);
			}
			if(userNum!=0) {
				command+=" AND UserNum="+POut.Long(userNum);
			}
			command+=" ORDER BY LogDateTime";
			DataTable table=Db.GetTable(command);
			List<SecurityLog> list=Crud.SecurityLogCrud.TableToList(table);
			for(int i=0;i<list.Count;i++) {
				if(table.Rows[i]["PatNum"].ToString()=="0") {
					list[i].PatientName="";
				}
				else {
					list[i].PatientName=table.Rows[i]["PatNum"].ToString()+"-"
						+Patients.GetNameLF(table.Rows[i]["LName"].ToString()
						,table.Rows[i]["FName"].ToString()
						,table.Rows[i]["Preferred"].ToString()
						,table.Rows[i]["MiddleI"].ToString());
				}
				list[i].LogHash=table.Rows[i]["LogHash"].ToString();
			}
			return list.ToArray();
		}

		///<summary></summary>
		public static long Insert(SecurityLog log){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				log.SecurityLogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),log);
				return log.SecurityLogNum;
			}
			return Crud.SecurityLogCrud.Insert(log);
		}

		//there are no methods for deleting or changing log entries because that will never be allowed.

		///<summary>Used when viewing various audit trails of specific types.  Only implemented Appointments,ProcFeeEdit,InsPlanChangeCarrierName so far. patNum only used for Appointments.  The other two are zero.</summary>
		public static SecurityLog[] Refresh(long patNum,List<Permissions> permTypes,long fKey) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SecurityLog[]>(MethodBase.GetCurrentMethod(),patNum,permTypes,fKey);
			}
			string types="";
			for(int i=0;i<permTypes.Count;i++){
				if(i>0){
					types+=" OR";
				}
				types+=" PermType="+POut.Long((int)permTypes[i]);
			}
			string command="SELECT * FROM securitylog "
				+"WHERE ("+types+") "
				+"AND FKey="+POut.Long(fKey)+" ";
			if(patNum!=0) {//appointments
				command+=" AND PatNum="+POut.Long(patNum)+" ";
			}
			command+="ORDER BY LogDateTime";
			return Crud.SecurityLogCrud.SelectMany(command).ToArray();
			/*
			DataTable table=Db.GetTable(command);
			SecurityLog[] List=new SecurityLog[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new SecurityLog();
				List[i].SecurityLogNum= PIn.Long   (table.Rows[i]["SecurityLogNum"].ToString());
				List[i].PermType      = (Permissions)PIn.Long(table.Rows[i]["PermType"].ToString());
				List[i].UserNum       = PIn.Long   (table.Rows[i]["UserNum"].ToString());
				List[i].LogDateTime   = PIn.DateT (table.Rows[i]["LogDateTime"].ToString());	
				List[i].LogText       = PIn.String(table.Rows[i]["LogText"].ToString());
				List[i].PatNum        = PIn.Long   (table.Rows[i]["PatNum"].ToString());
				List[i].FKey					= PIn.Long   (table.Rows[i]["FKey"].ToString());
			}
			return List;*/
		}

		///<summary>Returns one SecurityLog from the db.  Called from SecurityLogHashs.CreateSecurityLogHash()</summary>
		public static SecurityLog GetOne(long securityLogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SecurityLog>(MethodBase.GetCurrentMethod(),securityLogNum);
			}
			return Crud.SecurityLogCrud.SelectOne(securityLogNum);
		}

		///<summary>PatNum can be 0.</summary>
		public static void MakeLogEntry(Permissions permType,long patNum,string logText) {
			//No need to check RemotingRole; no call to db.
			MakeLogEntry(permType,patNum,logText,0);
		}	

		///<summary>Takes fKey, which is a key to a table associated with that permType. Right now only used for AptNum. PatNum can be 0.</summary>
		public static void MakeLogEntry(Permissions permType,long patNum,string logText,long fKey) {
			//No need to check RemotingRole; no call to db.
			SecurityLog securityLog=new SecurityLog();
			securityLog.PermType=permType;
			securityLog.UserNum=Security.CurUser.UserNum;
			securityLog.LogText=logText;//"From: "+Environment.MachineName+" - "+logText;
			securityLog.CompName=Environment.MachineName;
			securityLog.PatNum=patNum;
			securityLog.FKey=fKey;
			securityLog.SecurityLogNum=SecurityLogs.Insert(securityLog);
			//Create a hash of the security log.
			SecurityLogHashes.InsertSecurityLogHash(securityLog.SecurityLogNum);//uses db date/time
		}


	}
}