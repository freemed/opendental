 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{

	///<summary></summary>
	public class Providers{
		
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM provider ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Provider";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ProviderC.ListLong=Crud.ProviderCrud.TableToList(table).ToArray();
			List<Provider> listShort=new List<Provider>();
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(!ProviderC.ListLong[i].IsHidden){
					listShort.Add(ProviderC.ListLong[i]);	
				}
			}
			ProviderC.List=listShort.ToArray();
		}

		///<summary></summary>
		public static void Update(Provider provider){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),provider);
				return;
			}
			Crud.ProviderCrud.Update(provider);
		}

		///<summary></summary>
		public static long Insert(Provider provider){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				provider.ProvNum=Meth.GetLong(MethodBase.GetCurrentMethod(),provider);
				return provider.ProvNum;
			}
			return Crud.ProviderCrud.Insert(provider);
		}

		///<summary>Only used from FormProvEdit if user clicks cancel before finishing entering a new provider.</summary>
		public static void Delete(Provider prov){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),prov);
				return;
			}
			string command="DELETE from provider WHERE provnum = '"+prov.ProvNum.ToString()+"'";
 			Db.NonQ(command);
		}

		///<summary>Gets table for main provider edit list.  SchoolClass is usually zero to indicate all providers.  IsAlph will sort aphabetically instead of by ItemOrder.</summary>
		public static DataTable Refresh(long schoolClass,bool isAlph){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),schoolClass,isAlph);
			}
			string command="SELECT Abbr,LName,FName,provider.IsHidden,provider.ItemOrder,provider.ProvNum,GradYear,Descript,UserName "
				+"FROM provider LEFT JOIN schoolclass ON provider.SchoolClassNum=schoolclass.SchoolClassNum "
				+"LEFT JOIN userod ON userod.ProvNum=provider.ProvNum ";
			if(schoolClass!=0){
				command+="WHERE provider.SchoolClassNum="+POut.Long(schoolClass)+" ";
			}
			command+="GROUP BY provider.ProvNum ";
			if(isAlph){
				command+="ORDER BY GradYear,Descript,LName,FName";
			}
			else {
				command+="ORDER BY ItemOrder";
			}
			return Db.GetTable(command);
		}

		public static List<Provider> GetUAppoint(DateTime changedSince){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Provider>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT * FROM provider WHERE DateTStamp > "+POut.DateT(changedSince);
			//DataTable table=Db.GetTable(command);
			//return TableToList(table);
			return Crud.ProviderCrud.SelectMany(command);
		}

		///<summary></summary>
		public static string GetAbbr(long provNum){
			//No need to check RemotingRole; no call to db.
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
		public static string GetLName(long provNum){
			//No need to check RemotingRole; no call to db.
			string retStr="";
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retStr=ProviderC.ListLong[i].LName;
				}
			}
			return retStr;
		}

		///<summary>First Last, Suffix</summary>
		public static string GetFormalName(long provNum){
			//No need to check RemotingRole; no call to db.
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
		public static string GetLongDesc(long provNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProviderC.ListLong.Length;i++) {
				if(ProviderC.ListLong[i].ProvNum==provNum) {
					return ProviderC.ListLong[i].GetLongDesc();
				}
			}
			return "";
		}

		///<summary></summary>
		public static Color GetColor(long provNum) {
			//No need to check RemotingRole; no call to db.
			Color retCol=Color.White;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retCol=ProviderC.ListLong[i].ProvColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static Color GetOutlineColor(long provNum){
			//No need to check RemotingRole; no call to db.
			Color retCol=Color.Black;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retCol=ProviderC.ListLong[i].OutlineColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetIsSec(long provNum){
			//No need to check RemotingRole; no call to db.
			bool retVal=false;
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					retVal=ProviderC.ListLong[i].IsSecondary;
				}
			}
			return retVal;
		}

		///<summary>Gets a provider from the List.  If provnum is not valid, then it returns null.</summary>
		public static Provider GetProv(long provNum) {
			//No need to check RemotingRole; no call to db.
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
			//No need to check RemotingRole; no call to db.
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
			//If using eCW, a provider might have been added from the business layer.
			//The UI layer won't know about the addition.
			//So we need to refresh if we can't initially find the prov.
			RefreshCache();
			//and try again
			for(int i=0;i<ProviderC.ListLong.Length;i++) {
				if(ProviderC.ListLong[i].Abbr==abbr) {
					return ProviderC.ListLong[i].Copy();
				}
			}
			return null;
		}


		///<summary></summary>
		public static int GetIndexLong(long provNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProviderC.ListLong.Length;i++){
				if(ProviderC.ListLong[i].ProvNum==provNum){
					return i;
				}
			}
			return 0;//should NEVER happen, but just in case, the 0 won't crash
		}

		///<summary>Within the regular list of visible providers.  Will return -1 if the specified provider is not in the list.</summary>
		public static int GetIndex(long provNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ProviderC.List.Length;i++){
				if(ProviderC.List[i].ProvNum==provNum){
					return i;
				}
			}
			return -1;
		}

		///<summary>If useClinic, then clinicInsBillingProv will be used.  Otherwise, the pref for the practice.  Either way, there are three different choices for getting the billing provider.  One of the three is to use the treating provider, so supply that as an argument.  It will return a valid provNum unless the supplied treatProv was invalid.</summary>
		public static long GetBillingProvNum(long treatProv,long clinicNum) {//,bool useClinic,int clinicInsBillingProv){
			//No need to check RemotingRole; no call to db.
			long clinicInsBillingProv=0;
			bool useClinic=false;
			if(clinicNum>0) {
				useClinic=true;
				clinicInsBillingProv=Clinics.GetClinic(clinicNum).InsBillingProv;
			}
			if(useClinic){
				if(clinicInsBillingProv==0) {//default=0
					return PrefC.GetLong(PrefName.PracticeDefaultProv);
				}
				else if(clinicInsBillingProv==-1) {//treat=-1
					return treatProv;
				}
				else {
					return clinicInsBillingProv;
				}
			}
			else{
				if(PrefC.GetLong(PrefName.InsBillingProv)==0) {//default=0
					return PrefC.GetLong(PrefName.PracticeDefaultProv);
				}
				else if(PrefC.GetLong(PrefName.InsBillingProv)==-1) {//treat=-1
					return treatProv;
				}
				else {
					return PrefC.GetLong(PrefName.InsBillingProv);
				}
			}
		}

		///<summary>Used when adding a provider to get the next available itemOrder.</summary>
		public static int GetNextItemOrder(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			//Is this valid in Oracle??
			string command="SELECT MAX(ItemOrder) FROM provider";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return 0;
			}
			return PIn.Int(table.Rows[0][0].ToString())+1;
		}

		///<Summary>Used once in the Provider Select window to warn user of duplicate Abbrs.</Summary>
		public static string GetDuplicateAbbrs(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SELECT Abbr FROM provider p1 WHERE EXISTS"
				+"(SELECT * FROM provider p2 WHERE p1.ProvNum!=p2.ProvNum AND p1.Abbr=p2.Abbr) GROUP BY Abbr";
			DataTable table=Db.GetTable(command);
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

		public static DataTable GetDefaultPracticeProvider(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT FName,LName,Suffix,StateLicense
				FROM provider
        WHERE provnum="+PrefC.GetString(PrefName.PracticeDefaultProv);
			return Db.GetTable(command);
		}

		///<summary>We should merge these results with GetDefaultPracticeProvider(), but
		///that would require restructuring indexes in different places in the code and this is
		///faster to do as we are just moving the queries down in to the business layer for now.</summary>
		public static DataTable GetDefaultPracticeProvider2() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT FName,LName,Specialty "+
				"FROM provider WHERE provnum="+
				POut.Long(PrefC.GetLong(PrefName.PracticeDefaultProv));
				//Convert.ToInt32(((Pref)PrefC.HList["PracticeDefaultProv"]).ValueString);
			return Db.GetTable(command);
		}

		///<summary>We should merge these results with GetDefaultPracticeProvider(), but
		///that would require restructuring indexes in different places in the code and this is
		///faster to do as we are just moving the queries down in to the business layer for now.</summary>
		public static DataTable GetDefaultPracticeProvider3() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT NationalProvID "+
				"FROM provider WHERE provnum="+
				POut.Long(PrefC.GetLong(PrefName.PracticeDefaultProv));
			return Db.GetTable(command);
		}

		public static DataTable GetPrimaryProviders(long PatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum);
			}
			string command=@"SELECT Fname,Lname from provider
                        WHERE provnum in (select priprov from 
                        patient where patnum = "+PatNum+")";
			return Db.GetTable(command);
		}


	}
	
	

}










