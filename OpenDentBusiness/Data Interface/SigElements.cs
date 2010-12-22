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
			return Crud.SigElementCrud.SelectMany(command).ToArray();
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
			return Crud.SigElementCrud.Insert(se);
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




















