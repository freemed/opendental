using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LanguageForeigns {
		///<summary>just translations for the culture currently being used.  If a translation is missing, it tries to use a translation from another culture with the same language. Key=ClassType+English. Value =LanguageForeign object.  When support for multiple simultaneous languages is added, there will still be a current culture, but then we will add a supplemental way to extract translations for alternate cultures.</summary>
		private static Hashtable hList;

		public static Hashtable HList {
			//No need to check RemotingRole; no call to db.
			get {
				if(hList==null) {
					Refresh(CultureInfo.CurrentCulture.Name,CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

		///<summary>Called once when the program first starts up.  Then only if user downloads new translations or adds their own.</summary>
		public static void Refresh(string cultureInfoName,string cultureInfoTwoLetterISOLanguageName) {
			//culture info won't serialize
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cultureInfoName,cultureInfoTwoLetterISOLanguageName);
				return;
			}
			HList=new Hashtable();
			if(cultureInfoName=="en-US") {
				return;
			}
			//load all translations for the current culture, using other culture of same language if no trans avail.
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture LIKE '"+cultureInfoTwoLetterISOLanguageName+"%' "
				+"ORDER BY Culture";
			DataTable table=Db.GetTable(command);
			LanguageForeign lf;
			for(int i=0;i<table.Rows.Count;i++) {
				lf=new LanguageForeign();
				lf.ClassType  = PIn.PString(table.Rows[i][0].ToString());
				lf.English    = PIn.PString(table.Rows[i][1].ToString());
				lf.Culture    = PIn.PString(table.Rows[i][2].ToString());
				lf.Translation= PIn.PString(table.Rows[i][3].ToString());
				lf.Comments   = PIn.PString(table.Rows[i][4].ToString());
				if(lf.Culture==cultureInfoName) {//if exact culture match
					if(HList.ContainsKey(lf.ClassType+lf.English)) {
						HList.Remove(lf.ClassType+lf.English);//remove any existing entry
					}
					HList.Add(lf.ClassType+lf.English,lf);
				}
				else {//or if any other culture of same language
					if(!HList.ContainsKey(lf.ClassType+lf.English)) {
						//only add if not already in HList
						HList.Add(lf.ClassType+lf.English,lf);
					}
				}
			}
		}

		///<summary></summary>
		public static void Insert(LanguageForeign lf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lf);
				return;
			}
			string command= "INSERT INTO languageforeign(ClassType,English,Culture"
				+",Translation,Comments) "
				+"VALUES("
				+"'"+POut.PString(lf.ClassType)+"', "
				+"'"+POut.PString(lf.English)+"', "
				+"'"+POut.PString(lf.Culture)+"', "
				+"'"+POut.PString(lf.Translation)+"', "
				+"'"+POut.PString(lf.Comments)+"')";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Update(LanguageForeign lf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lf);
				return;
			}
			string command="UPDATE languageforeign SET " 
				+"Translation	= '"+POut.PString(lf.Translation)+"'"
				+",Comments = '"  +POut.PString(lf.Comments)+"'" 
				+" WHERE ClassType= BINARY '"+POut.PString(lf.ClassType)+"'" 
				+" AND English= BINARY '"+POut.PString(lf.English)+"'"
				+" AND Culture= '"+CultureInfo.CurrentCulture.Name+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(LanguageForeign lf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lf);
				return;
			}
			string command= "DELETE from languageforeign "
				+"WHERE ClassType=BINARY '"+POut.PString(lf.ClassType)+"' "
				+"AND English=BINARY '"    +POut.PString(lf.English)+"' "
				+"AND Culture='"+CultureInfo.CurrentCulture.Name+"'";
			Db.NonQ(command);
		}

		///<summary>Only used during export to get a list of all translations for specified culture only.</summary>
		public static LanguageForeign[] GetListForCulture(CultureInfo cultureInfo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<LanguageForeign[]>(MethodBase.GetCurrentMethod(),cultureInfo);
			}
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture='"+CultureInfo.CurrentCulture.Name+"'";
			DataTable table=Db.GetTable(command);
			LanguageForeign[] List=new LanguageForeign[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LanguageForeign();
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}

		///<summary>Used in FormTranslation to get all translations for all cultures for one classtype</summary>
		public static LanguageForeign[] GetListForType(string classType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<LanguageForeign[]>(MethodBase.GetCurrentMethod(),classType);
			}
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE ClassType='"+POut.PString(classType)+"'";
			DataTable table=Db.GetTable(command);
			LanguageForeign[] List=new LanguageForeign[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new LanguageForeign();
				List[i].ClassType  = PIn.PString(table.Rows[i][0].ToString());
				List[i].English    = PIn.PString(table.Rows[i][1].ToString());
				List[i].Culture    = PIn.PString(table.Rows[i][2].ToString());
				List[i].Translation= PIn.PString(table.Rows[i][3].ToString());
				List[i].Comments   = PIn.PString(table.Rows[i][4].ToString());
			}
			return List;
		}
		
		///<summary>Used in FormTranslation to get a single entry for the specified culture.  The culture match must be extact.  If no translation entries, then it returns null.</summary>
		public static LanguageForeign GetForCulture(LanguageForeign[] listForType,string english,string cultureName){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listForType.Length;i++){
				if(english!=listForType[i].English){
					continue;
				}
				if(cultureName!=listForType[i].Culture){
					continue;
				}
				return listForType[i];
			}
			return null;
		}

		///<summary>Used in FormTranslation to get a single entry with the same language as the specified culture, but only for a different culture.  For instance, if culture is es-PR (Spanish-PuertoRico), then it will return any spanish translation that is NOT from Puerto Rico.  If no other translation entries, then it returns null.</summary>
		public static LanguageForeign GetOther(LanguageForeign[] listForType,string english,string cultureName){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listForType.Length;i++){
				if(english!=listForType[i].English){
					continue;
				}
				if(cultureName==listForType[i].Culture){
					continue;
				}
				if(cultureName.Substring(0,2)!=listForType[i].Culture.Substring(0,2)){
					continue;
				}
				return listForType[i];
			}
			return null;
		}

		

	}

	

	

}













