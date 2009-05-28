using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using OpenDentBusiness;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EtransMessageTexts {

		///<summary></summary>
		public static int Insert(EtransMessageText etransMessageText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				etransMessageText.EtransMessageTextNum=Meth.GetInt(MethodBase.GetCurrentMethod(),etransMessageText);
				return etransMessageText.EtransMessageTextNum;
			}
			if(PrefC.RandomKeys) {
				etransMessageText.EtransMessageTextNum=MiscData.GetKey("etransmessagetext","EtransMessageTextNum");
			}
			string command="INSERT INTO etransmessagetext (";
			if(PrefC.RandomKeys) {
				command+="EtransMessageTextNum,";
			}
			command+="MessageText) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(etransMessageText.EtransMessageTextNum)+"', ";
			}
			command+="'"+POut.PString(etransMessageText.MessageText)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				etransMessageText.EtransMessageTextNum=Db.NonQ(command,true);
			}
			return etransMessageText.EtransMessageTextNum;
		}

		public static string GetMessageText(int etransMessageTextNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),etransMessageTextNum);
			}
			if(etransMessageTextNum==0) {
				return "";
			}
			string command="SELECT MessageText FROM etransmessagetext WHERE EtransMessageTextNum="+POut.PInt(etransMessageTextNum);
			return Db.GetScalar(command);
		}

		/*
		///<summary></summary>
		public static void Update(EtransMessageText EtransMessageText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),EtransMessageText);
				return;
			}
			string command= "UPDATE EtransMessageText SET "
				+"ClearinghouseNum = '"   +POut.PInt   (EtransMessageText.ClearinghouseNum)+"', "
				+"Etype= '"               +POut.PInt   ((int)EtransMessageText.Etype)+"', "
				+"Note= '"                +POut.PString(EtransMessageText.Note)+"', "
				+"EtransMessageTextMessageTextNum= '"+POut.PInt   (EtransMessageText.EtransMessageTextMessageTextNum)+"' "
				+"WHERE EtransMessageTextNum = "+POut.PInt(EtransMessageText.EtransMessageTextNum);
			Db.NonQ(command);
		}

		///<summary>Deletes the EtransMessageText entry.  Only used when the EtransMessageText entry was created, but then the communication with the clearinghouse failed.  So this is just a rollback function.  Will throw exception if there's a message attached to the EtransMessageText or if the EtransMessageText does not exist.</summary>
		public static void Delete(int EtransMessageTextNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),EtransMessageTextNum);
				return;
			}
			//see if there's a message
			string command="SELECT MessageText FROM EtransMessageText WHERE EtransMessageTextNum="+POut.PInt(EtransMessageTextNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!=""){//this throws exception if 0 rows.
				throw new ApplicationException("Error. EtransMessageText must not have messagetext attached yet.");
			}
			command="DELETE FROM EtransMessageText WHERE EtransMessageTextNum="+POut.PInt(EtransMessageTextNum);
			Db.NonQ(command);
		}*/

		



		
	}
}