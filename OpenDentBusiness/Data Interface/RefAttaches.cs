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
				+" WHERE PatNum = "+POut.Long(patNum)
				+" ORDER BY itemorder";
			return Crud.RefAttachCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(RefAttach attach){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),attach);
				return;
			}
			Crud.RefAttachCrud.Update(attach);
		}

		///<summary></summary>
		public static long Insert(RefAttach attach) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				attach.RefAttachNum=Meth.GetLong(MethodBase.GetCurrentMethod(),attach);
				return attach.RefAttachNum;
			}
			return Crud.RefAttachCrud.Insert(attach);
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
				+"AND refattach.IsFrom="+POut.Bool(IsFrom)
				+" AND referral.ReferralNum="+refNum.ToString();
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				retStr[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		/// <summary>Gets the referral number for this patient.  If multiple, it returns the first one.  If none, it returns 0.  Does not consider referred To.</summary>
		public static long GetReferralNum(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT ReferralNum "
				+"FROM refattach " 
				+"WHERE refattach.PatNum ="+POut.Long(patNum)+" "
				+"AND refattach.IsFrom=1 "
				+"ORDER BY ItemOrder ";
			command=DbHelper.LimitOrderBy(command,1);
			return PIn.Long(Db.GetScalar(command));
		}

	}
}