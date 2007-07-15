using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class Referrals{
		///<summary>All referrals for all patients</summary>
		public static Referral[] List;
		//should later add a faster refresh sequence.
		//private static Hashtable HList;

		///<summary>Refreshes all referrals for all patients.  Need to rework at some point so less memory is consumed.  Also refreshes dynamically, so no need to invalidate local data.</summary>
		public static void Refresh(){
			string command=
				"SELECT * from referral "
				+"ORDER BY lname";
 			DataTable table=General.GetTable(command);
			List=new Referral[table.Rows.Count];
			//HList=new Hashtable();
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Referral();
				List[i].ReferralNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].LName      = PIn.PString(table.Rows[i][1].ToString());
				List[i].FName      = PIn.PString(table.Rows[i][2].ToString());
				List[i].MName      = PIn.PString(table.Rows[i][3].ToString());
				List[i].SSN        = PIn.PString(table.Rows[i][4].ToString());
				List[i].UsingTIN   = PIn.PBool  (table.Rows[i][5].ToString());
				List[i].Specialty  = (DentalSpecialty)PIn.PInt(table.Rows[i][6].ToString());
				List[i].ST         = PIn.PString(table.Rows[i][7].ToString());
				List[i].Telephone  = PIn.PString(table.Rows[i][8].ToString());
				List[i].Address    = PIn.PString(table.Rows[i][9].ToString());
				List[i].Address2   = PIn.PString(table.Rows[i][10].ToString());
				List[i].City       = PIn.PString(table.Rows[i][11].ToString());
				List[i].Zip        = PIn.PString(table.Rows[i][12].ToString());
				List[i].Note       = PIn.PString(table.Rows[i][13].ToString());
				List[i].Phone2     = PIn.PString(table.Rows[i][14].ToString());
				List[i].IsHidden   = PIn.PBool  (table.Rows[i][15].ToString());
				List[i].NotPerson  = PIn.PBool  (table.Rows[i][16].ToString());
				List[i].Title      = PIn.PString(table.Rows[i][17].ToString());
				List[i].EMail      = PIn.PString(table.Rows[i][18].ToString());
				List[i].PatNum     = PIn.PInt   (table.Rows[i][19].ToString());
				List[i].NationalProvID     = PIn.PString   (table.Rows[i][20].ToString());
				//HList.Add(List[i].ReferralNum,List[i].Copy());
			}
		}

		///<summary></summary>
		public static void Update(Referral refer) {
			string command = "UPDATE referral SET " 
				+ "LName = '"      +POut.PString(refer.LName)+"'"
				+ ",FName = '"     +POut.PString(refer.FName)+"'"
				+ ",MName = '"     +POut.PString(refer.MName)+"'"
				+ ",SSN = '"       +POut.PString(refer.SSN)+"'"
				+ ",UsingTIN = '"  +POut.PBool(refer.UsingTIN)+"'"
				+ ",Specialty = '" +POut.PInt((int)refer.Specialty)+"'"
				+ ",ST = '"        +POut.PString(refer.ST)+"'"
				+ ",Telephone = '" +POut.PString(refer.Telephone)+"'"
				+ ",Address = '"   +POut.PString(refer.Address)+"'"
				+ ",Address2 = '"  +POut.PString(refer.Address2)+"'"
				+ ",City = '"      +POut.PString(refer.City)+"'"
				+ ",Zip = '"       +POut.PString(refer.Zip)+"'"
				+ ",Note = '"      +POut.PString(refer.Note)+"'"
				+ ",Phone2 = '"    +POut.PString(refer.Phone2)+"'"
				+ ",IsHidden = '"  +POut.PBool(refer.IsHidden)+"'"
				+ ",NotPerson = '" +POut.PBool(refer.NotPerson)+"'"
				+ ",Title = '"     +POut.PString(refer.Title)+"'"
				+ ",EMail = '"     +POut.PString(refer.EMail)+"'"
				+ ",PatNum = '"    +POut.PInt(refer.PatNum)+"'"
				+ ",NationalProvID = '"    +POut.PString(refer.NationalProvID)+"'"
				+" WHERE ReferralNum = '" +POut.PInt(refer.ReferralNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Referral refer) {
			if(PrefB.RandomKeys) {
				refer.ReferralNum=MiscData.GetKey("referral","ReferralNum");
			}
			string command= "INSERT INTO referral (";
			if(PrefB.RandomKeys) {
				command+="ReferralNum,";
			}
			command+="LName,FName,MName,SSN,UsingTIN,Specialty,ST,"
				+"Telephone,Address,Address2,City,Zip,Note,Phone2,IsHidden,NotPerson,Title,Email,PatNum,NationalProvID) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(refer.ReferralNum)+"', ";
			}
			command+=
				 "'"+POut.PString(refer.LName)+"', "
				+"'"+POut.PString(refer.FName)+"', "
				+"'"+POut.PString(refer.MName)+"', "
				+"'"+POut.PString(refer.SSN)+"', "
				+"'"+POut.PBool(refer.UsingTIN)+"', "
				+"'"+POut.PInt((int)refer.Specialty)+"', "
				+"'"+POut.PString(refer.ST)+"', "
				+"'"+POut.PString(refer.Telephone)+"', "    
				+"'"+POut.PString(refer.Address)+"', "
				+"'"+POut.PString(refer.Address2)+"', "
				+"'"+POut.PString(refer.City)+"', "
				+"'"+POut.PString(refer.Zip)+"', "
				+"'"+POut.PString(refer.Note)+"', "
				+"'"+POut.PString(refer.Phone2)+"', "
				+"'"+POut.PBool(refer.IsHidden)+"', "
				+"'"+POut.PBool(refer.NotPerson)+"', "
				+"'"+POut.PString(refer.Title)+"', "
				+"'"+POut.PString(refer.EMail)+"', "
				+"'"+POut.PInt(refer.PatNum)+"', "
				+"'"+POut.PString(refer.NationalProvID)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				refer.ReferralNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(Referral refer) {
			string command= "DELETE FROM referral "
				+"WHERE referralnum = '"+refer.ReferralNum+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static string GetNameLF(int referralNum) {
			if(referralNum==0)
				return "";
			for(int i=0;i<List.Length;i++){
				if(List[i].ReferralNum==referralNum){
					//Referral refer=List[i];
					string retVal=List[i].LName;
					if(List[i].FName!="") {
						retVal+=", "+List[i].FName+" "+List[i].MName;
					}
					return retVal;
				}
			}
			return "";
		}

		///<summary></summary>
		public static string GetNameFL(int referralNum) {
			if(referralNum==0)
				return "";
			for(int i=0;i<List.Length;i++) {
				if(List[i].ReferralNum==referralNum) {
					//Referral refer=List[i];
					string retVal="";
					if(List[i].FName!="") {
						retVal+=List[i].FName+" "+List[i].MName;
					}
					retVal+=List[i].LName;
					if(List[i].Title!="") {
						retVal+=", "+List[i].Title;
					}
					return retVal;
				}
			}
			return "";
		}

		///<summary></summary>
		public static string GetPhone(int referralNum) {
			if(referralNum==0)
				return "";
			for(int i=0;i<List.Length;i++) {
				if(List[i].ReferralNum==referralNum) {
					//Referral refer=(Referral)HList[referralNum];
					if(List[i].Telephone.Length==10) {
						return List[i].Telephone.Substring(0,3)+"-"+List[i].Telephone.Substring(3,3)+"-"+List[i].Telephone.Substring(6);
					}
					return List[i].Telephone;
				}
			}
			return "";
		}
	
		///<summary>Gets Referral info from memory. Does not make a call to the database unless needed.</summary>
		public static Referral GetReferral(int referralNum){
			if(referralNum==0){
				return null;
			}
			for(int i=0;i<List.Length;i++) {
				if(List[i].ReferralNum==referralNum) {
					return List[i].Copy();
				}
			}
			Refresh();//must be outdated
			for(int i=0;i<List.Length;i++) {
				if(List[i].ReferralNum==referralNum) {
					return List[i].Copy();
				}
			}
			MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
			return null;
		}

		///<summary>Gets the first referral "from" for the given patient.  Will return null if no "from" found for patient.</summary>
		public static Referral GetReferralForPat(int patNum){
			RefAttach[] RefAttachList=RefAttaches.Refresh(patNum);
			for(int i=0;i<RefAttachList.Length;i++) {
				if(RefAttachList[i].IsFrom) {
					return GetReferral(RefAttachList[i].ReferralNum);
				}
			}
			return null;
		}


	}

	

	


}













