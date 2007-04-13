//using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SigElements {

		///<summary>Gets all SigElements for a set of Signals, ordered by type: user,extras, message.</summary>
		public static SigElement[] GetElements(Signal[] signalList) {
			if(signalList.Length==0) {
				return new SigElement[0];
			}
			string command="SELECT sigelement.* FROM sigelement,sigelementdef WHERE "
				+"sigelement.SigElementDefNum=sigelementdef.SigElementDefNum AND (";
			for(int i=0;i<signalList.Length;i++) {
				if(i>0) {
					command+=" OR ";
				}
				command+="SignalNum="+POut.PInt(signalList[i].SignalNum);
			}
			command+=") ORDER BY sigelementdef.SigElementType";
			DataTable table=General.GetTable(command);
			SigElement[] List=new SigElement[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SigElement();
				List[i].SigElementNum   = PIn.PInt(table.Rows[i][0].ToString());
				List[i].SigElementDefNum= PIn.PInt(table.Rows[i][1].ToString());
				List[i].SignalNum       = PIn.PInt(table.Rows[i][2].ToString());
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
 			General.NonQ(command);
		}*/

		///<summary></summary>
		public static void Insert(SigElement se){
			if(PrefB.RandomKeys){
				se.SigElementNum=MiscData.GetKey("sigelement","SigElementNum");
			}
			string command= "INSERT INTO sigelement (";
			if(PrefB.RandomKeys){
				command+="SigElementNum,";
			}
			command+="SigElementDefNum,SignalNum"
				+") VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(se.SigElementNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (se.SigElementDefNum)+"', "
				+"'"+POut.PInt   (se.SignalNum)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				se.SigElementNum=General.NonQ(command,true);
			}
		}

		//<summary>There's no such thing as deleting a SigElement</summary>
		/*public void Delete(){
			string command= "DELETE from SigElement WHERE SigElementNum = '"
				+POut.PInt(SigElementNum)+"'";
			DataConnection dcon=new DataConnection();
 			General.NonQ(command);
		}*/

		///<summary>Loops through the supplied sigElement list and pulls out all elements for one specific signal.</summary>
		public static SigElement[] GetForSig(SigElement[] elementList,int signalNum){
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




















