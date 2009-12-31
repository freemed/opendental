using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SigElements {

		///<summary>Gets all SigElements for a set of Signals, ordered by type: user,extras, message.</summary>
		public static SigElement[] GetElements(List <Signal> signalList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<SigElement[]>(MethodBase.GetCurrentMethod(),signalList);
			}
			if(signalList.Count==0) {
				return new SigElement[0];
			}
			string command="SELECT sigelement.* FROM sigelement,sigelementdef WHERE "
				+"sigelement.SigElementDefNum=sigelementdef.SigElementDefNum AND (";
			for(int i=0;i<signalList.Count;i++) {
				if(i>0) {
					command+=" OR ";
				}
				command+="SignalNum="+POut.Long(signalList[i].SignalNum);
			}
			command+=") ORDER BY sigelementdef.SigElementType";
			DataTable table=Db.GetTable(command);
			SigElement[] List=new SigElement[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigElement();
				List[i].SigElementNum   = PIn.Long(table.Rows[i][0].ToString());
				List[i].SigElementDefNum= PIn.Long(table.Rows[i][1].ToString());
				List[i].SignalNum       = PIn.Long(table.Rows[i][2].ToString());
			}
			//Array.Sort(List);
			return List;
		}

		/*
		///<summary>This will never happen</summary>
		public void Update(){
			string command= "UPDATE sigelement SET " 
				+"FromUser = '"    +POut.PString(FromUser)+"'"
				+",ITypes = '"     +POut.PInt   ((int)ITypes)+"'"
				+",DateViewing = '"+POut.PDate  (DateViewing)+"'"
				+",SigType = '"    +POut.PInt   ((int)SigType)+"'"
				+" WHERE SigElementNum = '"+POut.PInt(SigElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		///<summary></summary>
		public static long Insert(SigElement se) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				se.SigElementNum=Meth.GetLong(MethodBase.GetCurrentMethod(),se);
				return se.SigElementNum;
			}
			if(PrefC.RandomKeys){
				se.SigElementNum=ReplicationServers.GetKey("sigelement","SigElementNum");
			}
			string command= "INSERT INTO sigelement (";
			if(PrefC.RandomKeys){
				command+="SigElementNum,";
			}
			command+="SigElementDefNum,SignalNum"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(se.SigElementNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (se.SigElementDefNum)+"', "
				+"'"+POut.Long   (se.SignalNum)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				se.SigElementNum=Db.NonQ(command,true);
			}
			return se.SigElementNum;
		}

		//<summary>There's no such thing as deleting a SigElement</summary>
		/*public void Delete(){
			string command= "DELETE from SigElement WHERE SigElementNum = '"
				+POut.PInt(SigElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		///<summary>Loops through the supplied sigElement list and pulls out all elements for one specific signal.</summary>
		public static SigElement[] GetForSig(SigElement[] elementList,long signalNum) {
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<elementList.Length;i++){
				if(elementList[i].SignalNum==signalNum){
					AL.Add(elementList[i].Copy());
				}
			}
			SigElement[] retVal=new SigElement[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		

	
	}

	

	


}




















