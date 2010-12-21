using System;
using System.Collections;
using System.Collections.Generic;
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

		///<summary>Haven't moved this over to the cache pattern because of the parameters.  But when called, it behaves exactly like the cache pattern, refreshing on both client and server.</summary>
		public static DataTable Refresh(string cultureInfoName,string cultureInfoTwoLetterISOLanguageName) {
			//Very unusual.  RemotingRole checked a little further down due to complex situation.
			//culture info won't serialize
			if(cultureInfoName=="en-US") {
				return null;//since DataTable is ignored anyway if on the client, this won't crash.
			}
			//load all translations for the current culture, using other culture of same language if no trans avail.
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture LIKE '"+cultureInfoTwoLetterISOLanguageName+"%' "
				+"ORDER BY Culture";
			DataTable table;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				table=Meth.GetTable(MethodBase.GetCurrentMethod(),cultureInfoName,cultureInfoTwoLetterISOLanguageName);
			}
			else {
				table=Db.GetTable(command);
			}
			hList=new Hashtable();
			//LanguageForeign lanf;
			List<LanguageForeign> list=Crud.LanguageForeignCrud.TableToList(table);
			for(int i=0;i<list.Count;i++) {
				if(list[i].Culture==cultureInfoName) {//if exact culture match
					if(hList.ContainsKey(list[i].ClassType+list[i].English)) {
						hList.Remove(list[i].ClassType+list[i].English);//remove any existing entry
					}
					hList.Add(list[i].ClassType+list[i].English,list[i]);
				}
				else {//or if any other culture of same language
					if(!hList.ContainsKey(list[i].ClassType+list[i].English)) {
						//only add if not already in HList
						hList.Add(list[i].ClassType+list[i].English,list[i]);
					}
				}
			}
			return table;
		}

		///<summary></summary>
		public static long Insert(LanguageForeign lanf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),lanf);
			}
			return Crud.LanguageForeignCrud.Insert(lanf);
		}

		///<summary></summary>
		public static void Update(LanguageForeign lanf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lanf);
				return;
			}
			Crud.LanguageForeignCrud.Update(lanf);
		}

		///<summary></summary>
		public static void Delete(LanguageForeign lanf){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lanf);
				return;
			}
			Crud.LanguageForeignCrud.Delete(lanf.LanguageForeignNum);
		}

		///<summary>Only used during export to get a list of all translations for specified culture only.</summary>
		public static LanguageForeign[] GetListForCulture(CultureInfo cultureInfo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<LanguageForeign[]>(MethodBase.GetCurrentMethod(),cultureInfo);
			}
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE Culture='"+CultureInfo.CurrentCulture.Name+"'";
			return Crud.LanguageForeignCrud.SelectMany(command).ToArray();
		}

		///<summary>Used in FormTranslation to get all translations for all cultures for one classtype</summary>
		public static LanguageForeign[] GetListForType(string classType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<LanguageForeign[]>(MethodBase.GetCurrentMethod(),classType);
			}
			string command=
				"SELECT * FROM languageforeign "
				+"WHERE ClassType='"+POut.String(classType)+"'";
			return Crud.LanguageForeignCrud.SelectMany(command).ToArray();
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













