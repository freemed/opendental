using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Commlogs {

		///<summary>Gets all items for the current patient ordered by date.</summary>
		public static List<Commlog> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Commlog>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM commlog"
				+" WHERE PatNum = '"+patNum+"'"
				+" ORDER BY CommDateTime";
			return Crud.CommlogCrud.SelectMany(command);
		}

		///<summary>Gets one commlog item from database.</summary>
		public static Commlog GetOne(long commlogNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Commlog>(MethodBase.GetCurrentMethod(),commlogNum);
			}
			return Crud.CommlogCrud.SelectOne(commlogNum);
		}

		///<summary></summary>
		public static long Insert(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				comm.CommlogNum=Meth.GetLong(MethodBase.GetCurrentMethod(),comm);
				return comm.CommlogNum;
			}
			return Crud.CommlogCrud.Insert(comm);
		}

		///<summary></summary>
		public static void Update(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comm);
				return;
			}
			Crud.CommlogCrud.Update(comm);
		}

		///<summary></summary>
		public static void Delete(Commlog comm) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),comm);
				return;
			}
			Crud.CommlogCrud.Delete(comm.CommlogNum);
		}

		///<summary>Used when printing or emailing recall to make a commlog entry without any display.</summary>
		public static void InsertForRecall(long patNum,CommItemMode _mode,int numberOfReminders,long defNumNewStatus) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum,_mode,numberOfReminders,defNumNewStatus);
				return;
			}
			long recallType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
			string command;
			if(recallType!=0){
				command="SELECT COUNT(*) FROM commlog WHERE ";
				if(DataConnection.DBtype==DatabaseType.Oracle){
					command+="TO_DATE(CommDateTime) = "+POut.Date(MiscData.GetNowDateTime());
				}
				else{//MySQL
					command+=DbHelper.DateColumn("CommDateTime")+" = CURDATE()";
				}
				command+=" AND PatNum="+POut.Long(patNum)+" AND CommType="+POut.Long(recallType)
					+" AND Mode_="+POut.Long((int)_mode)
					+" AND SentOrReceived=1";
				if(Db.GetCount(command)!="0"){
					return;
				}
			}
			Commlog com=new Commlog();
			com.PatNum=patNum;
			com.CommDateTime=DateTime.Now;
			com.CommType=recallType;
			com.Mode_=_mode;
			com.SentOrReceived=CommSentOrReceived.Sent;
			com.Note="";
			if(numberOfReminders==0){
				com.Note=Lans.g("FormRecallList","Recall reminder.");
			}
			else if(numberOfReminders==1) {
				com.Note=Lans.g("FormRecallList","Second recall reminder.");
			}
			else if(numberOfReminders==2) {
				com.Note=Lans.g("FormRecallList","Third recall reminder.");
			}
			else {
				com.Note=Lans.g("FormRecallList","Recall reminder:")+" "+(numberOfReminders+1).ToString();
			}
			if(defNumNewStatus==0) {
				com.Note+="  "+Lans.g("Commlogs","Status None");
			}
			else {
				com.Note+="  "+DefC.GetName(DefCat.RecallUnschedStatus,defNumNewStatus);
			}
			com.UserNum=Security.CurUser.UserNum;
			Insert(com);
		}

		///<Summary>Returns a defnum.  If no match, then it returns the first one in the list in that category.</Summary>
		public static long GetTypeAuto(CommItemTypeAuto typeauto) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<DefC.Long[(int)DefCat.CommLogTypes].Length;i++){
				if(DefC.Long[(int)DefCat.CommLogTypes][i].ItemValue==typeauto.ToString()){
					return DefC.Long[(int)DefCat.CommLogTypes][i].DefNum;
				}
			}
			if(DefC.Long[(int)DefCat.CommLogTypes].Length>0){
				return DefC.Long[(int)DefCat.CommLogTypes][0].DefNum;
			}
			return 0;
		}

		public static int GetRecallUndoCount(DateTime date) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),date);
			}
			string command="SELECT COUNT(*) FROM commlog "
				+"WHERE "+DbHelper.DateColumn("CommDateTime")+" = "+POut.Date(date)+" "
				+"AND (SELECT ItemValue FROM definition WHERE definition.DefNum=commlog.CommType) ='"+CommItemTypeAuto.RECALL.ToString()+"'";
			return PIn.Int(Db.GetScalar(command));
		}

		
		public static void RecallUndo(DateTime date) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),date);
				return;
			}
			string command="DELETE FROM commlog "
				+"WHERE "+DbHelper.DateColumn("CommDateTime")+" = "+POut.Date(date)+" "
				+"AND (SELECT ItemValue FROM definition WHERE definition.DefNum=commlog.CommType) ='"+CommItemTypeAuto.RECALL.ToString()+"'";
			Db.NonQ(command);
		}

	}

	///<summary></summary>
	public enum CommItemTypeAuto {
		APPT,
		FIN,
		RECALL,
		MISC
	}




}

















