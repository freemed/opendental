using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RefAttaches{

		///<summary>For one patient</summary>
		public static RefAttach[] Refresh(int patNum) {
			string command=
				"SELECT * FROM refattach"
				+" WHERE PatNum = "+POut.PInt(patNum)
				+" ORDER BY itemorder";
			DataTable table=Db.GetTable(command);
			RefAttach[] List=new RefAttach[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new RefAttach();
				List[i].RefAttachNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].ReferralNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].PatNum      = PIn.PInt(table.Rows[i][2].ToString());
				List[i].ItemOrder   = PIn.PInt(table.Rows[i][3].ToString());
				List[i].RefDate     = PIn.PDate(table.Rows[i][4].ToString());
				List[i].IsFrom      = PIn.PBool(table.Rows[i][5].ToString());
				List[i].RefToStatus = (ReferralToStatus)PIn.PInt(table.Rows[i][6].ToString());
				List[i].Note        = PIn.PString(table.Rows[i][7].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(RefAttach attach){
			string command= "UPDATE refattach SET " 
				+ "ReferralNum = '" +POut.PInt   (attach.ReferralNum)+"'"
				+ ",PatNum = '"     +POut.PInt   (attach.PatNum)+"'"
				+ ",ItemOrder = '"  +POut.PInt   (attach.ItemOrder)+"'"
				+ ",RefDate = "    +POut.PDate  (attach.RefDate)
				+ ",IsFrom = '"     +POut.PBool  (attach.IsFrom)+"'"
				+ ",RefToStatus = '"+POut.PInt   ((int)attach.RefToStatus)+"'"
				+ ",Note = '"       +POut.PString(attach.Note)+"'"
				+" WHERE RefAttachNum = '" +POut.PInt(attach.RefAttachNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(RefAttach attach){
			if(PrefC.RandomKeys){
				attach.RefAttachNum=MiscData.GetKey("refattach","RefAttachNum");
			}
			string command="INSERT INTO refattach (";
			if(PrefC.RandomKeys){
				command+="RefAttachNum,";
			}			
			command+="ReferralNum,PatNum,ItemOrder,RefDate,IsFrom,RefToStatus,Note) VALUES (";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(attach.RefAttachNum)+"', ";
			}
			command+="'"+POut.PInt(attach.ReferralNum)+"', "
				+"'"+POut.PInt(attach.PatNum)+"', "
				+"'"+POut.PInt(attach.ItemOrder)+"', "
				+POut.PDate(attach.RefDate)+", "
				+"'"+POut.PBool(attach.IsFrom)+"', "
				+"'"+POut.PInt ((int)attach.RefToStatus)+"', "
				+"'"+POut.PString(attach.Note)+"')";
 			attach.RefAttachNum=Db.NonQ(command,true);
		}

		///<summary></summary>
		public static void Delete(RefAttach attach){
			string command= "DELETE FROM refattach "
				+"WHERE refattachnum = '"+attach.RefAttachNum+"'";
 			Db.NonQ(command);
		}		

		///<summary></summary>
		public static bool IsReferralAttached(int referralNum){
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
		public static string[] GetPats(int refNum,bool IsFrom){
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