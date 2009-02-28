 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Providers{
		
		///<summary>Does not get called directly from the UI.</summary>
		public static DataTable RefreshCache(){
			string command="SELECT * FROM provider ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			table.TableName="Provider";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			ArrayList AL=new ArrayList();
			ProviderC.ListLong=new Provider[table.Rows.Count];
			List<Provider> provList=TableToList(table);
			for(int i=0;i<provList.Count;i++){
				ProviderC.ListLong[i]=provList[i];
				if(!ProviderC.ListLong[i].IsHidden){
					AL.Add(ProviderC.ListLong[i]);	
				}
			}
			ProviderC.List=new Provider[AL.Count];
			AL.CopyTo(ProviderC.List);
		}

		private static List<Provider> TableToList(DataTable table){
			List<Provider> retVal=new List<Provider>();
			Provider prov;
			for(int i=0;i<table.Rows.Count;i++){
				prov=new Provider();
				prov.ProvNum       = PIn.PInt   (table.Rows[i][0].ToString());
				prov.Abbr          = PIn.PString(table.Rows[i][1].ToString());
				prov.ItemOrder     = PIn.PInt   (table.Rows[i][2].ToString());
				prov.LName         = PIn.PString(table.Rows[i][3].ToString());
				prov.FName         = PIn.PString(table.Rows[i][4].ToString());
				prov.MI            = PIn.PString(table.Rows[i][5].ToString());
				prov.Suffix        = PIn.PString(table.Rows[i][6].ToString());
				prov.FeeSched      = PIn.PInt   (table.Rows[i][7].ToString());
				prov.Specialty     =(DentalSpecialty)PIn.PInt (table.Rows[i][8].ToString());
				prov.SSN           = PIn.PString(table.Rows[i][9].ToString());
				prov.StateLicense  = PIn.PString(table.Rows[i][10].ToString());
				prov.DEANum        = PIn.PString(table.Rows[i][11].ToString());
				prov.IsSecondary   = PIn.PBool  (table.Rows[i][12].ToString());
				prov.ProvColor     = Color.FromArgb(PIn.PInt(table.Rows[i][13].ToString()));
				prov.IsHidden      = PIn.PBool  (table.Rows[i][14].ToString());
				prov.UsingTIN      = PIn.PBool  (table.Rows[i][15].ToString());
				//prov.BlueCrossID = PIn.PString(table.Rows[i][16].ToString());
				prov.SigOnFile     = PIn.PBool  (table.Rows[i][17].ToString());
				prov.MedicaidID    = PIn.PString(table.Rows[i][18].ToString());
				prov.OutlineColor  = Color.FromArgb(PIn.PInt(table.Rows[i][19].ToString()));
				prov.SchoolClassNum= PIn.PInt   (table.Rows[i][20].ToString());
				prov.NationalProvID= PIn.PString(table.Rows[i][21].ToString());
				prov.CanadianOfficeNum= PIn.PString(table.Rows[i][22].ToString());
				//DateTStamp
				retVal.Add(prov);
			}
			return retVal;
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
				//DateTStamp
				+" WHERE provnum = '" +POut.PInt(prov.ProvNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Provider prov){
			string command= "INSERT INTO provider (Abbr,ItemOrder,LName,FName,MI,Suffix,"
				+"FeeSched,Specialty,SSN,StateLicense,DEANum,IsSecondary,ProvColor,IsHidden,"
				+"UsingTIN,SigOnFile,MedicaidID,OutlineColor,SchoolClassNum,"
				+"NationalProvID,CanadianOfficeNum"//DateTStamp
				+") VALUES("
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
				//DateTStamp
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

		public static List<Provider> GetUAppoint(DateTime changedSince){
			string command="SELECT * FROM provider WHERE DateTStamp > "+POut.PDateT(changedSince);
			DataTable table=General.GetTable(command);
			return TableToList(table);
		}

		///<summary></summary>
		public static string GetAbbr(int provNum){
			if(ProviderC.ListLong==null){
				RefreshCache();
			}
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					return ProviderC.ListLong[i].Abbr;
				}
			}
			return "";
		}

		///<summary>Used in the HouseCalls bridge</summary>
		public static string GetLName(int provNum){
			string retStr="";
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retStr=ProviderC.ListLong[i].LName;
				}
			}
			return retStr;
		}

		///<summary>First Last, Suffix</summary>
		public static string GetFormalName(int provNum){
			string retStr="";
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retStr=ProviderC.ListLong[i].FName+" "
						+ProviderC.ListLong[i].LName;
					if(ProviderC.ListLong[i].Suffix != ""){
						retStr+=", "+ProviderC.ListLong[i].Suffix;
					}
				}
			}
			return retStr;
		}

		///<summary>Abbr - LName, FName (hidden).</summary>
		public static string GetLongDesc(int provNum) {
			for(int i=0;i<ProviderC.ListLong.Length;i++) {
				if(ProviderC.ListLong[i].ProvNum==provNum) {
					return ProviderC.ListLong[i].GetLongDesc();
				}
			}
			return "";
		}

		///<summary></summary>
		public static Color GetColor(int provNum){
			Color retCol=Color.White;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retCol=ProviderC.ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static Color GetOutlineColor(int provNum){
			Color retCol=Color.Black;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retCol=ProviderC.ListLong[i].OutlineColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetIsSec(int provNum){
			bool retVal=false;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retVal=ProviderC.ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		///<summary>Gets a provider from the List.  If provnum is not valid, then it returns null.</summary>
		public static Provider GetProv(int provNum) {
			if(provNum==0){
				return null;
			}
			if(ProviderC.ListLong==null) {
				RefreshCache();
			}
			for(int i=0;i<ProviderC.ListLong.Length;i++) {
				if(ProviderC.ListLong[i].ProvNum==provNum) {
					return ProviderC.ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Gets a provider from the List.  If abbr is not found, then it returns null.</summary>
		public static Provider GetProvByAbbr(string abbr) {
			if(abbr=="") {
				return null;
			}
			if(ProviderC.ListLong==null) {
				RefreshCache();
			}
			for(int i=0;i<ProviderC.ListLong.Length;i++) {
				if(ProviderC.ListLong[i].Abbr==abbr) {
					return ProviderC.ListLong[i].Copy();
				}
			}
			return null;
		}


		///<summary></summary>
		public static int GetIndexLong(int provNum){
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return 0;//should NEVER happen, but just in case, the 0 won't crash
		}

		///<summary></summary>
		public static int GetIndex(int provNum){
			//Gets the index of the provider in short list (visible providers)
			for(int i=0;i<ProviderC.List.Length;i++){
				if(ProviderC.List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>If useClinic, then clinicInsBillingProv will be used.  Otherwise, the pref for the practice.  Either way, there are three different choices for getting the billing provider.  One of the three is to use the treating provider, so supply that as an argument.  It will return a valid provNum unless the supplied treatProv was invalid.</summary>
		public static int GetBillingProvNum(int treatProv,bool useClinic,int clinicInsBillingProv){
			if(useClinic){
				if(clinicInsBillingProv==0) {//default=0
					return PrefC.GetInt("PracticeDefaultProv");
				}
				else if(clinicInsBillingProv==-1) {//treat=-1
					return treatProv;
				}
				else {
					return clinicInsBillingProv;
				}
			}
			else{
				if(PrefC.GetInt("InsBillingProv")==0) {//default=0
					return PrefC.GetInt("PracticeDefaultProv");
				}
				else if(PrefC.GetInt("InsBillingProv")==-1) {//treat=-1
					return treatProv;
				}
				else {
					return PrefC.GetInt("InsBillingProv");
				}
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

		///<Summary>Used once in the Provider Select window to warn user of duplicate Abbrs.</Summary>
		public static string GetDuplicateAbbrs(){
			string command="SELECT Abbr FROM provider p1 WHERE EXISTS"
				+"(SELECT * FROM provider p2 WHERE p1.ProvNum!=p2.ProvNum AND p1.Abbr=p2.Abbr) GROUP BY Abbr";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0) {
				return "";
			}
			string retVal="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					retVal+=",";
				}
				retVal+=table.Rows[i][0].ToString();
			}
			return retVal;
		}


	}
	
	

}










