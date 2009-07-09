using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class HL7Msgs{
		/*
		///<summary></summary>
		public static DataTable RefreshCache(){
			string c="SELECT * from HL7Msg ORDER BY Description";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),command);
			table.TableName="HL7Msg";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			HL7MsgC.List=new HL7Msg[table.Rows.Count];
			for(int i=0;i<HL7MsgC.List.Length;i++){
				HL7MsgC.List[i]=new HL7Msg();
				HL7MsgC.List[i].IsNew=false;
				HL7MsgC.List[i].HL7MsgNum    = PIn.PInt   (table.Rows[i][0].ToString());
				HL7MsgC.List[i].Description= PIn.PString(table.Rows[i][1].ToString());
				HL7MsgC.List[i].Note       = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<Summary>Gets one HL7Msg from the database.</Summary>
		public static HL7Msg CreateObject(int HL7MsgNum){
			return DataObjectFactory<HL7Msg>.CreateObject(HL7MsgNum);
		}*/

		public static List<HL7Msg> GetAllPending(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<HL7Msg>>(MethodBase.GetCurrentMethod());
			}
			//diagnosticMsg=DataConnection.GetCurrentConnectionString();
			string command="SELECT * FROM hl7msg WHERE HL7Status="+POut.PInt((int)HL7MessageStatus.OutPending);
			//diagnosticMsg+=".   "+command;
			//diagnosticMsg="";
			Collection<HL7Msg> collection=DataObjectFactory<HL7Msg>.CreateObjects(command);
			return new List<HL7Msg>(collection);		
		}

		///<summary></summary>
		public static int WriteObject(HL7Msg hL7Msg){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				hL7Msg.HL7MsgNum=Meth.GetInt(MethodBase.GetCurrentMethod(),hL7Msg);
				return hL7Msg.HL7MsgNum;
			}
			DataObjectFactory<HL7Msg>.WriteObject(hL7Msg);
			return hL7Msg.HL7MsgNum;
		}

		///<summary></summary>
		public static bool MessageWasSent(int aptNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),aptNum);
			}
			string command="SELECT COUNT(*) FROM hl7msg WHERE AptNum="+POut.PInt(aptNum);
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;
		}

		/*
		public static string GetHL7FolderOut() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT PropertyValue FROM programproperty,program
				WHERE programproperty.ProgramNum=program.ProgramNum
				AND program.ProgName='eClinicalWorks'
				AND programproperty.PropertyDesc='HL7FolderOut'";
			return Db.GetScalar(command);
		}

		public static string GetHL7FolderIn() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT PropertyValue FROM programproperty,program
				WHERE programproperty.ProgramNum=program.ProgramNum
				AND program.ProgName='eClinicalWorks'
				AND programproperty.PropertyDesc='HL7FolderIn'";
			return Db.GetScalar(command);
		}*/

		/*
		///<summary></summary>
		public static void DeleteObject(int HL7MsgNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE HL7MsgNum="+POut.PInt(HL7MsgNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("HL7Msgs","HL7Msg is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<HL7Msg>.DeleteObject(HL7MsgNum);
		}

		//public static void DeleteObject(int HL7MsgNum){
		//	DataObjectFactory<HL7Msg>.DeleteObject(HL7MsgNum);
		//}

		public static string GetDescription(int HL7MsgNum){
			if(HL7MsgNum==0){
				return "";
			}
			for(int i=0;i<HL7MsgC.List.Length;i++){
				if(HL7MsgC.List[i].HL7MsgNum==HL7MsgNum){
					return HL7MsgC.List[i].Description;
				}
			}
			return "";
		}

		public static List<HL7Msg> GetListFiltered(string snippet) {
			List<HL7Msg> retVal=new List<HL7Msg>();
			if(snippet=="") {
				return retVal;
			}
			for(int i=0;i<HL7MsgC.List.Length;i++) {
				if(HL7MsgC.List[i].Description.ToLower().Contains(snippet.ToLower())) {
					retVal.Add(HL7MsgC.List[i]);
				}
			}
			return retVal;
		}

		///<summary>Will return -1 if no match.</summary>
		public static int FindMatchHL7MsgNum(string description) {
			if(description=="") {
				return 0;
			}
			for(int i=0;i<HL7MsgC.List.Length;i++) {
				if(HL7MsgC.List[i].Description.ToLower()==description.ToLower()) {
					return HL7MsgC.List[i].HL7MsgNum;
				}
			}
			return -1;
		}
		*/

	}
}