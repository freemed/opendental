using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class DiseaseDefs {
		private static DiseaseDef[] listLong;
		private static DiseaseDef[] list;

		///<summary>A list of all Diseases.</summary>
		public static DiseaseDef[] ListLong{
			//No need to check RemotingRole; no call to db.
			get {
				if(listLong==null) {
					RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>The list that is typically used. Does not include hidden diseases.</summary>
		public static DiseaseDef[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM diseasedef ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DiseaseDef";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listLong=Crud.DiseaseDefCrud.TableToList(table).ToArray();
			List<DiseaseDef> listshort=new List<DiseaseDef>();
			for(int i=0;i<listLong.Length;i++) {
				if(!listLong[i].IsHidden) {
					listshort.Add(listLong[i]);
				}
			}
			list=listshort.ToArray();
		}

		///<summary></summary>
		public static void Update(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			Crud.DiseaseDefCrud.Update(def);
		}

		///<summary></summary>
		public static long Insert(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				def.DiseaseDefNum=Meth.GetLong(MethodBase.GetCurrentMethod(),def);
				return def.DiseaseDefNum;
			}
			return Crud.DiseaseDefCrud.Insert(def);
		}

		///<summary>Surround with try/catch, because it will throw an exception if any patient is using this def.</summary>
		public static void Delete(DiseaseDef def) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),def);
				return;
			}
			//Validate patient attached
			string command="SELECT LName,FName,patient.PatNum FROM patient,disease WHERE "
				+"patient.PatNum=disease.PatNum "
				+"AND disease.DiseaseDefNum='"+POut.Long(def.DiseaseDefNum)+"' ";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+="GROUP BY patient.PatNum";
			}
			else {//Oracle
				command+="GROUP BY LName,FName,patient.PatNum";
			}
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				string s=Lans.g("DiseaseDef","Not allowed to delete. Already in use by ")+table.Rows.Count.ToString()
					+" "+Lans.g("DiseaseDef","patients, including")+" \r\n";
				for(int i=0;i<table.Rows.Count;i++){
					if(i>5){
						break;
					}
					s+=table.Rows[i][0].ToString()+", "+table.Rows[i][1].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			//Validate edu resource attached
			command="SELECT COUNT(*) FROM eduresource WHERE eduresource.DiseaseDefNum='"+POut.Long(def.DiseaseDefNum)+"'";
			int num=PIn.Int(Db.GetCount(command));
			if(num>0) {
				string s=Lans.g("DiseaseDef","Not allowed to delete.  Already attached to an EHR educational resource.");
				throw new ApplicationException(s);
			}
			//Validate family health history attached
			command="SELECT LName,FName,patient.PatNum FROM patient,familyhealth "
				+"WHERE patient.PatNum=familyhealth.PatNum "
				+"AND familyhealth.DiseaseDefNum='"+POut.Long(def.DiseaseDefNum)+"' ";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+="GROUP BY patient.PatNum";
			}
			else {//Oracle
				command+="GROUP BY LName,FName,patient.PatNum";
			}
			table=Db.GetTable(command);
			if(table.Rows.Count>0) {
				string s=Lans.g("DiseaseDef","Not allowed to delete. Already in use by")+" "+table.Rows.Count.ToString()
					+" "+Lans.g("DiseaseDef","patients' family history, including")+": \r\n";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>5) {
						break;
					}
					s+="#"+table.Rows[i]["PatNum"].ToString()+" "+table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString()+"\r\n";
				}
				throw new ApplicationException(s);
			}
			//End of validation
			command="DELETE FROM diseasedef WHERE DiseaseDefNum ="+POut.Long(def.DiseaseDefNum);
			Db.NonQ(command);
		}

		///<summary>Moves the selected item up in the listLong.</summary>
		public static void MoveUp(int selected){
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==0) {//already at top
				return;
			}
			if(selected>ListLong.Length-1){
				throw new ApplicationException(Lans.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected-1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder-1);
			//Selected-=1;
		}

		///<summary></summary>
		public static void MoveDown(int selected) {
			//No need to check RemotingRole; no call to db.
			if(selected<0) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Please select an item first."));
			}
			if(selected==ListLong.Length-1){//already at bottom
				return;
			}
			if(selected>ListLong.Length-1) {
				throw new ApplicationException(Lans.g("DiseaseDefs","Invalid selection."));
			}
			SetOrder(selected+1,ListLong[selected].ItemOrder);
			SetOrder(selected,ListLong[selected].ItemOrder+1);
			//selected+=1;
		}

		///<summary>Used by MoveUp and MoveDown.</summary>
		private static void SetOrder(int mySelNum,int myItemOrder) {
			//No need to check RemotingRole; no call to db.
			DiseaseDef temp=ListLong[mySelNum];
			temp.ItemOrder=myItemOrder;
			DiseaseDefs.Update(temp);
		}

		///<summary>Returns the order in ListLong, whether hidden or not.</summary>
		public static int GetOrder(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].DiseaseDefNum==diseaseDefNum){
					return ListLong[i].ItemOrder;
				}
			}
			return 0;
		}

		///<summary>Returns the name of the disease, whether hidden or not.</summary>
		public static string GetName(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].DiseaseName;
				}
			}
			return "";
		}

		///<summary>Returns the name of the disease based on SNOMEDCode, then if no match tries ICD9Code, then if no match returns empty string. Used in EHR Patient Lists.</summary>
		public static string GetNameByCode(string SNOMEDorICD9Code) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].SnomedCode==SNOMEDorICD9Code) {
					return ListLong[i].DiseaseName;
				}
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ICD9Code==SNOMEDorICD9Code) {
					return ListLong[i].DiseaseName;
				}
			}
			return "";
		}

		///<summary>Returns the DiseaseDefNum based on SNOMEDCode, then if no match tries ICD9Code, then if no match tries ICD10Code, then if no match returns 0. Used in EHR Patient Lists and when automatically inserting pregnancy Dx from FormVitalsignEdit2014.  Will match hidden diseases.</summary>
		public static long GetNumFromCode(string CodeValue) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].SnomedCode==CodeValue) {
					return ListLong[i].DiseaseDefNum;
				}
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ICD9Code==CodeValue) {
					return ListLong[i].DiseaseDefNum;
				}
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].Icd10Code==CodeValue) {
					return ListLong[i].DiseaseDefNum;
				}
			}
			return 0;
		}

		///<summary>Returns the diseaseDef with the specified num.  Will match hidden diseasedefs.</summary>
		public static DiseaseDef GetItem(long diseaseDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].DiseaseDefNum==diseaseDefNum) {
					return ListLong[i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns the diseaseDefNum that exactly matches the specified string.  Used in import functions when you only have the name to work with.  Can return 0 if no match.  Does not match hidden diseases.</summary>
		public static long GetNumFromName(string diseaseName){
			//No need to check RemotingRole; no call to db.
			return GetNumFromName(diseaseName,false);
		}

		///<summary>Returns the diseaseDefNum that exactly matches the specified string.  Will return 0 if no match.  Set matchHidden to true to match hidden diseasedefs as well.</summary>
		public static long GetNumFromName(string diseaseName,bool matchHidden) {
			//No need to check RemotingRole; no call to db.
			if(matchHidden) {
				for(int i=0;i<ListLong.Length;i++) {
					if(diseaseName==ListLong[i].DiseaseName) {
						return ListLong[i].DiseaseDefNum;
					}
				}
			}
			else {
				for(int i=0;i<List.Length;i++) {
					if(diseaseName==List[i].DiseaseName) {
						return List[i].DiseaseDefNum;
					}
				}
			}
			return 0;
		}

		///<summary>Returns the diseasedef that has a name exactly matching the specified string. Returns null if no match.  Does not match hidden diseases.</summary>
		public static DiseaseDef GetFromName(string diseaseName) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(diseaseName==List[i].DiseaseName) {
					return List[i];
				}
			}
			return null;
		}

		public static List<long> GetChangedSinceDiseaseDefNums(DateTime changedSince) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince);
			}
			string command="SELECT DiseaseDefNum FROM diseasedef WHERE DateTStamp > "+POut.DateT(changedSince);
			DataTable dt=Db.GetTable(command);
			List<long> diseaseDefNums = new List<long>(dt.Rows.Count);
			for(int i=0;i<dt.Rows.Count;i++) {
				diseaseDefNums.Add(PIn.Long(dt.Rows[i]["DiseaseDefNum"].ToString()));
			}
			return diseaseDefNums;
		}

		///<summary>Used along with GetChangedSinceDiseaseDefNums</summary>
		public static List<DiseaseDef> GetMultDiseaseDefs(List<long> diseaseDefNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DiseaseDef>>(MethodBase.GetCurrentMethod(),diseaseDefNums);
			}
			string strDiseaseDefNums="";
			DataTable table;
			if(diseaseDefNums.Count>0) {
				for(int i=0;i<diseaseDefNums.Count;i++) {
					if(i>0) {
						strDiseaseDefNums+="OR ";
					}
					strDiseaseDefNums+="DiseaseDefNum='"+diseaseDefNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM diseasedef WHERE "+strDiseaseDefNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			DiseaseDef[] multDiseaseDefs=Crud.DiseaseDefCrud.TableToList(table).ToArray();
			List<DiseaseDef> diseaseDefList=new List<DiseaseDef>(multDiseaseDefs);
			return diseaseDefList;
		}

		public static bool ContainsSnomed(string snomedCode, long excludeDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].SnomedCode==snomedCode && ListLong[i].DiseaseDefNum!=excludeDefNum) {
					return true;
				}
			}
			return false;
		}

		public static bool ContainsICD9(string icd9Code,long excludeDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].ICD9Code==icd9Code && ListLong[i].DiseaseDefNum!=excludeDefNum) {
					return true;
				}
			}
			return false;
		}

		public static bool ContainsICD10(string icd10Code,long excludeDefNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].Icd10Code==icd10Code && ListLong[i].DiseaseDefNum!=excludeDefNum) {
					return true;
				}
			}
			return false;
		}

		///<summary>Get all diseasedefs that have a pregnancy code that applies to the three CQM measures with pregnancy as an exclusion condition.</summary>
		public static List<DiseaseDef> GetAllPregDiseaseDefs() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DiseaseDef>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT CodeValue,CodeSystem FROM (SELECT CodeValue,CodeSystem,GROUP_CONCAT(DISTINCT MeasureIds) AS Measures "
				+"FROM ehrcode WHERE ValueSetOID IN('2.16.840.1.113883.3.600.1.1623', "//measure 69, smaller list
				+"'2.16.840.1.113883.3.526.3.378') "//measures 155,165, bigger list
				+"GROUP BY CodeValue) A "
				+"WHERE A.Measures LIKE '%69%' AND A.Measures LIKE '%155%' AND A.Measures LIKE '%165%'";
			DataTable tableAllPregCodesForCQMs=Db.GetTable(command);
			List<DiseaseDef> retval=new List<DiseaseDef>();
			for(int i=0;i<ListLong.Length;i++) {
				for(int j=0;j<tableAllPregCodesForCQMs.Rows.Count;j++) {
					if(tableAllPregCodesForCQMs.Rows[j]["CodeSystem"].ToString()=="ICD9CM") {//if preg code is an ICD9CM code, compare against diseasedef.ICD9Code column
						if(ListLong[i].ICD9Code==tableAllPregCodesForCQMs.Rows[j]["CodeValue"].ToString()) {
							retval.Add(ListLong[i]);
						}
					}
					else if(tableAllPregCodesForCQMs.Rows[j]["CodeSystem"].ToString()=="ICD10CM") {
						if(ListLong[i].Icd10Code==tableAllPregCodesForCQMs.Rows[j]["CodeValue"].ToString()) {
							retval.Add(ListLong[i]);
						}
					}
					else if(tableAllPregCodesForCQMs.Rows[j]["CodeSystem"].ToString()=="SNOMEDCT") {
						if(ListLong[i].SnomedCode==tableAllPregCodesForCQMs.Rows[j]["CodeValue"].ToString()) {
							retval.Add(ListLong[i]);
						}
					}
				}
			}
			return retval;
		}

		/*public static DiseaseDef GetByICD9Code(string ICD9Code) {///<summary>Returns the diseasedef that has a name exactly matching the specified string. Returns null if no match.  Does not match hidden diseases.</summary>
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++) {
				if(ICD9Code==List[i].ICD9Code) {
					return List[i];
				}
			}
			return null;
		}*/
	}

		



		
	

	

	


}










