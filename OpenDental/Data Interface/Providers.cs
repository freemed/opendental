using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class Providers{
		///<summary>Rarely used. Includes all providers, even if hidden.</summary>
		public static Provider[] ListLong;
		///<summary>This is the list used most often. It does not include hidden providers.</summary>
		public static Provider[] List;

		///<summary>Refreshes List with all providers.</summary>
		public static void Refresh(){
			ArrayList AL=new ArrayList();
			string command="SELECT * FROM provider ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			ListLong=new Provider[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				ListLong[i]=new Provider();
				ListLong[i].ProvNum       = PIn.PInt   (table.Rows[i][0].ToString());
				ListLong[i].Abbr          = PIn.PString(table.Rows[i][1].ToString());
				ListLong[i].ItemOrder     = PIn.PInt   (table.Rows[i][2].ToString());
				ListLong[i].LName         = PIn.PString(table.Rows[i][3].ToString());
				ListLong[i].FName         = PIn.PString(table.Rows[i][4].ToString());
				ListLong[i].MI            = PIn.PString(table.Rows[i][5].ToString());
				ListLong[i].Suffix        = PIn.PString(table.Rows[i][6].ToString());
				ListLong[i].FeeSched      = PIn.PInt   (table.Rows[i][7].ToString());
				ListLong[i].Specialty     =(DentalSpecialty)PIn.PInt (table.Rows[i][8].ToString());
				ListLong[i].SSN           = PIn.PString(table.Rows[i][9].ToString());
				ListLong[i].StateLicense  = PIn.PString(table.Rows[i][10].ToString());
				ListLong[i].DEANum        = PIn.PString(table.Rows[i][11].ToString());
				ListLong[i].IsSecondary   = PIn.PBool  (table.Rows[i][12].ToString());
				ListLong[i].ProvColor     = Color.FromArgb(PIn.PInt(table.Rows[i][13].ToString()));
				ListLong[i].IsHidden      = PIn.PBool  (table.Rows[i][14].ToString());
				ListLong[i].UsingTIN      = PIn.PBool  (table.Rows[i][15].ToString());
				//ListLong[i].BlueCrossID = PIn.PString(table.Rows[i][16].ToString());
				ListLong[i].SigOnFile     = PIn.PBool  (table.Rows[i][17].ToString());
				ListLong[i].MedicaidID    = PIn.PString(table.Rows[i][18].ToString());
				ListLong[i].OutlineColor  = Color.FromArgb(PIn.PInt(table.Rows[i][19].ToString()));
				ListLong[i].SchoolClassNum= PIn.PInt   (table.Rows[i][20].ToString());
				ListLong[i].NationalProvID= PIn.PString(table.Rows[i][21].ToString());
				ListLong[i].CanadianOfficeNum= PIn.PString(table.Rows[i][22].ToString());
				if(!ListLong[i].IsHidden) AL.Add(ListLong[i]);	
			}
			List=new Provider[AL.Count];
			AL.CopyTo(List);
		}
	
		///<summary></summary>
		public static void Update(Provider prov){
			string command="UPDATE provider SET "
				+ "Abbr = '"          +POut.PString(prov.Abbr)+"'"
				+",ItemOrder = '"     +POut.PInt   (prov.ItemOrder)+"'"
				+",LName = '"         +POut.PString(prov.LName)+"'"
				+",FName = '"         +POut.PString(prov.FName)+"'"
				+",MI = '"            +POut.PString(prov.MI)+"'"
				+",Suffix = '"        +POut.PString(prov.Suffix)+"'"
				+",FeeSched = '"      +POut.PInt   (prov.FeeSched)+"'"
				+",Specialty = '"     +POut.PInt   ((int)prov.Specialty)+"'"
				+",SSN = '"           +POut.PString(prov.SSN)+"'"
				+",StateLicense = '"  +POut.PString(prov.StateLicense)+"'"
				+",DEANum = '"        +POut.PString(prov.DEANum)+"'"
				+",IsSecondary = '"   +POut.PBool  (prov.IsSecondary)+"'"
				+",ProvColor = '"     +POut.PInt   (prov.ProvColor.ToArgb())+"'"
				+",IsHidden = '"      +POut.PBool  (prov.IsHidden)+"'"
				+",UsingTIN = '"      +POut.PBool  (prov.UsingTIN)+"'"
				//+",bluecrossid = '" +POut.PString(BlueCrossID)+"'"
				+",SigOnFile = '"     +POut.PBool  (prov.SigOnFile)+"'"
				+",MedicaidID = '"    +POut.PString(prov.MedicaidID)+"'"
				+",OutlineColor = '"  +POut.PInt   (prov.OutlineColor.ToArgb())+"'"
				+",SchoolClassNum = '"+POut.PInt   (prov.SchoolClassNum)+"'"
				+",NationalProvID = '"+POut.PString(prov.NationalProvID)+"'"
				+",CanadianOfficeNum = '"+POut.PString(prov.CanadianOfficeNum)+"'"
				+" WHERE provnum = '" +POut.PInt(prov.ProvNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Provider prov){
			string command= "INSERT INTO provider (Abbr,ItemOrder,LName,FName,MI,Suffix,"
				+"FeeSched,Specialty,SSN,StateLicense,DEANum,IsSecondary,ProvColor,IsHidden,"
				+"UsingTIN,SigOnFile,MedicaidID,OutlineColor,SchoolClassNum,"
				+"NationalProvID,CanadianOfficeNum) VALUES("
				+"'"+POut.PString(prov.Abbr)+"', "
				+"'"+POut.PInt   (prov.ItemOrder)+"', "
				+"'"+POut.PString(prov.LName)+"', "
				+"'"+POut.PString(prov.FName)+"', "
				+"'"+POut.PString(prov.MI)+"', "
				+"'"+POut.PString(prov.Suffix)+"', "
				+"'"+POut.PInt   (prov.FeeSched)+"', "
				+"'"+POut.PInt   ((int)prov.Specialty)+"', "
				+"'"+POut.PString(prov.SSN)+"', "
				+"'"+POut.PString(prov.StateLicense)+"', "
				+"'"+POut.PString(prov.DEANum)+"', "
				+"'"+POut.PBool  (prov.IsSecondary)+"', "
				+"'"+POut.PInt   (prov.ProvColor.ToArgb())+"', "
				+"'"+POut.PBool  (prov.IsHidden)+"', "
				+"'"+POut.PBool  (prov.UsingTIN)+"', "
				//+"'"+POut.PString(BlueCrossID)+"', "
				+"'"+POut.PBool  (prov.SigOnFile)+"', "
				+"'"+POut.PString(prov.MedicaidID)+"', "
				+"'"+POut.PInt   (prov.OutlineColor.ToArgb())+"', "
				+"'"+POut.PInt   (prov.SchoolClassNum)+"', "
				+"'"+POut.PString(prov.NationalProvID)+"', "
				+"'"+POut.PString(prov.CanadianOfficeNum)+"')";
			//MessageBox.Show(string command);
 			prov.ProvNum=General.NonQ(command,true);
		}

		///<summary>Only used from FormProvEdit if user clicks cancel before finishing entering a new provider.</summary>
		public static void Delete(Provider prov){
			string command="DELETE from provider WHERE provnum = '"+prov.ProvNum.ToString()+"'";
 			General.NonQ(command);
		}

		///<summary>Gets table for main provider edit list.  SchoolClass is usually zero to indicate all providers.  IsAlph will sort aphabetically instead of by ItemOrder.</summary>
		public static DataTable Refresh(int schoolClass,bool isAlph){
			string command="SELECT Abbr,LName,FName,provider.IsHidden,provider.ProvNum,GradYear,Descript,UserName "
				+"FROM provider LEFT JOIN schoolclass ON provider.SchoolClassNum=schoolclass.SchoolClassNum "
				+"LEFT JOIN userod ON userod.ProvNum=provider.ProvNum ";
			if(schoolClass!=0){
				command+="WHERE provider.SchoolClassNum="+POut.PInt(schoolClass)+" ";
			}
			if(isAlph){
				command+="ORDER BY GradYear,Descript,LName,FName";
			}
			else {
				command+="ORDER BY ItemOrder";
			}
			return General.GetTable(command);
		}

		///<summary></summary>
		public static string GetAbbr(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return ListLong[i].Abbr;
				}
			}
			return "";
		}

		///<summary>Used in the HouseCalls bridge</summary>
		public static string GetLName(int provNum){
			string retStr="";
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retStr=ListLong[i].LName;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static string GetNameLF(int provNum) {
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ProvNum==provNum) {
					return ListLong[i].LName+", "+ListLong[i].FName;
				}
			}
			return "";
		}

		///<summary></summary>
		public static Color GetColor(int provNum){
			Color retCol=Color.White;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static Color GetOutlineColor(int provNum){
			Color retCol=Color.Black;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retCol=ListLong[i].OutlineColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetIsSec(int provNum){
			bool retVal=false;
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					retVal=ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		///<summary>Gets a provider from the List.  If provnum is not valid, then it returns null.</summary>
		public static Provider GetProv(int provNum) {
			if(provNum==0){
				return null;
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ProvNum==provNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static int GetIndexLong(int provNum){
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return 0;//should NEVER happen, but just in case, the 0 won't crash
		}

		///<summary></summary>
		public static int GetIndex(int provNum){
			//Gets the index of the provider in short list (visible providers)
			for(int i=0;i<List.Length;i++){
				if(List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>There are three different choices for getting the billing provider.  One of the three is to use the treating provider, so supply that as an argument.  It will return a valid provNum unless the supplied treatProv was invalid.</summary>
		public static int GetBillingProvNum(int treatProv){
			if(PrefB.GetInt("InsBillingProv")==0) {//default=0
				return PrefB.GetInt("PracticeDefaultProv");
			}
			else if(PrefB.GetInt("InsBillingProv")==-1) {//treat=-1
				return treatProv;
			}
			else {
				return PrefB.GetInt("InsBillingProv");
			}
		}

		///<summary>Used when adding a provider to get the next available itemOrder.</summary>
		public static int GetNextItemOrder(){
			//Is this valid in Oracle??
			string command="SELECT MAX(ItemOrder) FROM provider";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return 1;
			}
			return PIn.PInt(table.Rows[0][0].ToString())+1;
		}


	}
	
	

}










