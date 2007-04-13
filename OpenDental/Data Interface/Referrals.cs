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
		private static Hashtable HList;

		///<summary>Refreshes all referrals for all patients.  Need to rework at some point so less memory is consumed.  Also refreshes dynamically, so no need to invalidate local data.</summary>
		public static void Refresh(){
			string command=
				"SELECT * from referral "
				+"ORDER BY lname";
 			DataTable table=General.GetTable(command);
			List=new Referral[table.Rows.Count];
			HList=new Hashtable();
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
				HList.Add(List[i].ReferralNum,List[i]);
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
				+"Telephone,Address,Address2,City,Zip,Note,Phone2,IsHidden,NotPerson,Title,Email,PatNum) VALUES(";
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
				+"'"+POut.PInt(refer.PatNum)+"')";
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
		public static string GetName(int referralNum) {
			if(referralNum==0)
				return "";
			if(!HList.ContainsKey(referralNum)) {
				return "";
			}
			Referral refer=(Referral)HList[referralNum];
			string retVal=refer.LName;
			if(refer.FName!="") {
				retVal+=", "+refer.FName+" "+refer.MName;
			}
			return retVal;
		}

		///<summary></summary>
		public static string GetPhone(int referralNum) {
			if(referralNum==0)
				return "";
			if(!HList.ContainsKey(referralNum)) {
				return "";
			}
			Referral refer=(Referral)HList[referralNum];
			if(refer.Telephone.Length==10){
				return refer.Telephone.Substring(0,3)+"-"+refer.Telephone.Substring(3,3)+"-"+refer.Telephone.Substring(6);
			}
			return refer.Telephone;
		}
	
		///<summary>Gets Referral info from memory (HList). Does not make a call to the database.</summary>
		public static Referral GetReferral(int referralNum){
			if(referralNum==0){
				return null;
			}
			if(HList.ContainsKey(referralNum)){
				return (Referral)HList[referralNum];
			}
			Refresh();//must be outdated
			if(!HList.ContainsKey(referralNum)){
				MessageBox.Show("Error.  Referral not found: "+referralNum.ToString());
				return null;
			}
			return (Referral)HList[referralNum];
		}


	}

	

	


}













