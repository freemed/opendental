using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RefAttaches{

		///<summary>For one patient</summary>
		public static RefAttach[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RefAttach[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM refattach"
				+" WHERE PatNum = "+POut.PLong(patNum)
				+" ORDER BY itemorder";
			DataTable table=Db.GetTable(command);
			RefAttach[] List=new RefAttach[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RefAttach();
				List[i].RefAttachNum= PIn.PLong(table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PLong(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PLong(table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt(table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate(table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool(table.Rows[i][5].ToString());
				List[i].RefToStatus = (ReferralToStatus)PIn.PLong(table.Rows[i][6].ToString());
				List[i].Note        = PIn.PString(table.Rows[i][7].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(RefAttach attach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),attach);
				return;
			}
			string command= "UPDATE refattach SET " 
				+ "ReferralNum = '" +POut.PLong   (attach.ReferralNum)+"'"
				+ ",PatNum = '"     +POut.PLong   (attach.PatNum)+"'"
				+ ",ItemOrder = '"  +POut.PLong   (attach.ItemOrder)+"'"
				+ ",RefDate = "    +POut.PDate  (attach.RefDate)
				+ ",IsFrom = '"     +POut.PBool  (attach.IsFrom)+"'"
				+ ",RefToStatus = '"+POut.PLong   ((int)attach.RefToStatus)+"'"
				+ ",Note = '"       +POut.PString(attach.Note)+"'"
				+" WHERE RefAttachNum = '" +POut.PLong(attach.RefAttachNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(RefAttach attach) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				attach.RefAttachNum=Meth.GetInt(MethodBase.GetCurrentMethod(),attach);
				return attach.RefAttachNum;
			}
			if(PrefC.RandomKeys){
				attach.RefAttachNum=ReplicationServers.GetKey("refattach","RefAttachNum");
			}
			string command="INSERT INTO refattach (";
			if(PrefC.RandomKeys){
				command+="RefAttachNum,";
			}			
			command+="ReferralNum,PatNum,ItemOrder,RefDate,IsFrom,RefToStatus,Note) VALUES (";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(attach.RefAttachNum)+"', ";
			}
			command+="'"+POut.PLong(attach.ReferralNum)+"', "
				+"'"+POut.PLong(attach.PatNum)+"', "
				+"'"+POut.PLong(attach.ItemOrder)+"', "
				+POut.PDate(attach.RefDate)+", "
				+"'"+POut.PBool(attach.IsFrom)+"', "
				+"'"+POut.PLong ((int)attach.RefToStatus)+"', "
				+"'"+POut.PString(attach.Note)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				attach.RefAttachNum=Db.NonQ(command,true);
			}
			return attach.RefAttachNum;
		}

		///<summary></summary>
		public static void Delete(RefAttach attach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),attach);
				return;
			}
			string command= "DELETE FROM refattach "
				+"WHERE refattachnum = '"+attach.RefAttachNum+"'";
 			Db.NonQ(command);
		}		

		///<summary></summary>
		public static bool IsReferralAttached(long referralNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),referralNum);
			}
			string command =
				"SELECT * FROM refattach"
				+" WHERE ReferralNum = '"+referralNum+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count > 0){
				return true;
			}
			else{
				return false;
			}
		}

		///<summary>Returns a list of patient names that are attached to this referral. Used to display in the referral edit window.</summary>
		public static string[] GetPats(long refNum,bool IsFrom) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),refNum,IsFrom);
			}
			string command="SELECT CONCAT(CONCAT(patient.LName,', '),patient.FName) "
				+"FROM patient,refattach,referral " 
				+"WHERE patient.PatNum=refattach.PatNum "
				+"AND refattach.ReferralNum=referral.ReferralNum "
				+"AND refattach.IsFrom="+POut.PBool(IsFrom)
				+" AND referral.ReferralNum="+refNum.ToString();
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}		

	}
}