using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Carriers{
		private static Carrier[] listt;
		private static Hashtable hList;

		public static Carrier[] Listt {
			//No need to check RemotingRole; no call to db.
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary>A hashtable of all carriers.</summary>
		public static Hashtable HList {
			//No need to check RemotingRole; no call to db.
			get {
				if(hList==null) {
					RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}
	
		///<summary>Carriers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if a carrierNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM carrier ORDER BY CarrierName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodInfo.GetCurrentMethod(),command);
			table.TableName="Carrier";
			FillCache(table);
			return table;
		}
		
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=Crud.CarrierCrud.TableToList(table).ToArray();
			HList=new Hashtable();
			for(int i=0;i<Listt.Length;i++){
				HList.Add(Listt[i].CarrierNum,Listt[i]);
			}
		}

		///<summary>Used to get a list of carriers to display in the FormCarriers window.</summary>
		public static DataTable GetBigList(bool isCanadian,bool showHidden,string carrierName){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),isCanadian,showHidden,carrierName);
			}
			DataTable tableRaw;
			DataTable table;
			string command;
			//if(isCanadian){
			command="SELECT Address,Address2,canadiannetwork.Abbrev,carrier.CarrierNum,"
				+"CarrierName,CDAnetVersion,City,ElectID,"
				+"COUNT(insplan.PlanNum) insPlanCount,IsCDA,"
				+"carrier.IsHidden,IsPMP,Phone,State,Zip "
				+"FROM carrier "
				+"LEFT JOIN canadiannetwork ON canadiannetwork.CanadianNetworkNum=carrier.CanadianNetworkNum "
				+"LEFT JOIN insplan ON insplan.CarrierNum=carrier.CarrierNum "
				+"WHERE "
				+"CarrierName LIKE '%"+POut.String(carrierName)+"%' ";
			if(isCanadian){
				command+="AND IsCDA=1 ";
			}
			if(!showHidden){
				command+="AND carrier.IsHidden=0 ";
			}
			command+="GROUP BY carrier.CarrierNum "
				+"ORDER BY CarrierName";
			tableRaw=Db.GetTable(command);
			table=new DataTable();
			table.Columns.Add("Address");
			table.Columns.Add("Address2");
			table.Columns.Add("CarrierNum");
			table.Columns.Add("CarrierName");
			table.Columns.Add("City");
			table.Columns.Add("ElectID");
			table.Columns.Add("insPlanCount");
			table.Columns.Add("isCDA");
			table.Columns.Add("isHidden");
			table.Columns.Add("Phone");
			//table.Columns.Add("pMP");
			//table.Columns.Add("network");
			table.Columns.Add("State");
			//table.Columns.Add("version");
			table.Columns.Add("Zip");
			DataRow row;
			for(int i=0;i<tableRaw.Rows.Count;i++){
				row=table.NewRow();
				row["Address"]=tableRaw.Rows[i]["Address"].ToString();
				row["Address2"]=tableRaw.Rows[i]["Address2"].ToString();
				row["CarrierNum"]=tableRaw.Rows[i]["CarrierNum"].ToString();
				row["CarrierName"]=tableRaw.Rows[i]["CarrierName"].ToString();
				row["City"]=tableRaw.Rows[i]["City"].ToString();
				row["ElectID"]=tableRaw.Rows[i]["ElectID"].ToString();
				if(PIn.Bool(tableRaw.Rows[i]["IsCDA"].ToString())) {
					row["isCDA"]="X";
				}
				else {
					row["isCDA"]="";
				}
				if(PIn.Bool(tableRaw.Rows[i]["IsHidden"].ToString())){
					row["isHidden"]="X";
				}
				else{
					row["isHidden"]="";
				}
				row["insPlanCount"]=tableRaw.Rows[i]["insPlanCount"].ToString();
				row["Phone"]=tableRaw.Rows[i]["Phone"].ToString();
				//if(PIn.Bool(tableRaw.Rows[i]["IsPMP"].ToString())){
				//	row["pMP"]="X";
				//}
				//else{
				//	row["pMP"]="";
				//}
				//row["network"]=tableRaw.Rows[i]["Abbrev"].ToString();
				row["State"]=tableRaw.Rows[i]["State"].ToString();
				//row["version"]=tableRaw.Rows[i]["CDAnetVersion"].ToString();
				row["Zip"]=tableRaw.Rows[i]["Zip"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Surround with try/catch.</summary>
		public static void Update(Carrier carrier){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),carrier);
				return;
			}
			string command;
			DataTable table;
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//en-CA or fr-CA
				if(carrier.IsCDA) {
					if(carrier.ElectID=="") {
						throw new ApplicationException(Lans.g("Carriers","Carrier Identification Number required."));
					}
					if(!Regex.IsMatch(carrier.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lans.g("Carriers","Carrier Identification Number must be exactly 6 numbers."));
					}
					/*Duplication is allowed
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.String(Cur.ElectID)+"' "
						+"AND IsCDA=1 "
						+"AND CarrierNum != "+POut.Long(Cur.CarrierNum);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lans.g("Carriers","EDI Code already in use."));
					}
					*/
				}
				//so the edited carrier looks good, but now we need to make sure that the original was allowed to be changed.
				command="SELECT ElectID,IsCDA FROM carrier WHERE CarrierNum = '"+POut.Long(carrier.CarrierNum)+"'";
				table=Db.GetTable(command);
				if(PIn.Bool(table.Rows[0][1].ToString())//if original carrier IsCDA
					&& PIn.String(table.Rows[0][0].ToString()) !=carrier.ElectID)//and the ElectID was changed
				{
					command="SELECT COUNT(*) FROM etrans WHERE CarrierNum= "+POut.Long(carrier.CarrierNum)
						+" OR CarrierNum2="+POut.Long(carrier.CarrierNum);
					if(Db.GetCount(command)!="0"){
						throw new ApplicationException(Lans.g("Carriers","Not allowed to change Carrier Identification Number because it's in use in the claim history."));
					}
				}
			}
			Crud.CarrierCrud.Update(carrier);
		}

		///<summary>Surround with try/catch if possibly adding a Canadian carrier.</summary>
		public static long Insert(Carrier carrier){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				carrier.CarrierNum=Meth.GetLong(MethodBase.GetCurrentMethod(),carrier);
				return carrier.CarrierNum;
			}
			string command;
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")){//en-CA or fr-CA
				if(carrier.IsCDA){
					if(carrier.ElectID==""){
						throw new ApplicationException(Lans.g("Carriers","Carrier Identification Number required."));
					}
					if(!Regex.IsMatch(carrier.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lans.g("Carriers","Carrier Identification Number must be exactly 6 numbers."));
					}
					/*Duplication actually seems to be allowed
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.String(Cur.ElectID)+"' ";
						//+"AND IsCDA=1";//no duplication allowed regardless.
					DataTable table=Db.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lans.g("Carriers","EDI Code already in use."));
					}
					*/
				}
			}
			return Crud.CarrierCrud.Insert(carrier);
		}

		///<summary>Surround with try/catch.  If there are any dependencies, then this will throw an exception.  This is currently only called from FormCarrierEdit.</summary>
		public static void Delete(Carrier Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			//look for dependencies in insplan table.
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" AND insplan.CarrierNum = '"+POut.Long(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string strInUse;
			if(table.Rows.Count>0){
				strInUse="";//new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0){
						strInUse+=", ";
					}
					strInUse+=PIn.String(table.Rows[i][0].ToString());
				}
				throw new ApplicationException(Lans.g("Carriers","Not allowed to delete carrier because it is in use.  Subscribers using this carrier include ")+strInUse);
			}
			//look for dependencies in etrans table.
			command="SELECT DateTimeTrans FROM etrans WHERE CarrierNum="+POut.Long(Cur.CarrierNum)
				+" OR CarrierNum2="+POut.Long(Cur.CarrierNum);
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				strInUse="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0) {
						strInUse+=", ";
					}
					strInUse+=PIn.DateT(table.Rows[i][0].ToString()).ToShortDateString();
				}
				throw new ApplicationException(Lans.g("Carriers","Not allowed to delete carrier because it is in use in the etrans table.  Dates of claim sent history include ")+strInUse);
			}
			command="DELETE from carrier WHERE CarrierNum = "+POut.Long(Cur.CarrierNum);
			Db.NonQ(command);
		}

		///<summary>Returns a list of insplans that are dependent on the Cur carrier. Used to display in carrier edit.</summary>
		public static string[] DependentPlans(Carrier Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" AND insplan.CarrierNum = '"+POut.Long(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retStr[i]=PIn.String(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of a carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(long carrierNum) {
			//No need to check RemotingRole; no call to db.
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//if the carrierNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//this could only happen if corrupted:
			return "";
		}

		///<summary>Gets the specified carrier from Cache. This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static Carrier GetCarrier(long carrierNum) {
			//No need to check RemotingRole; no call to db.
			if(carrierNum==0){
				Carrier retVal=new Carrier();
				retVal.CarrierName="";//instead of null. Helps prevent crash.
				return retVal;
			}
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//if the carrierNum could not be found:
			RefreshCache();
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//this could only happen if corrupted:
			Carrier retVall=new Carrier();
			retVall.CarrierName="";//instead of null. Helps prevent crash.
			return retVall;
		}

		///<summary>Primarily used when user clicks OK from the InsPlan window.  Gets a carrierNum from the database based on the other supplied carrier data.  Sets the CarrierNum accordingly. If there is no matching carrier, then a new carrier is created.  The end result is a valid carrierNum to use.</summary>
		public static Carrier GetIndentical(Carrier carrier){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Carrier>(MethodBase.GetCurrentMethod(),carrier);
			}
			if(carrier.CarrierName=="") {
				return new Carrier();//should probably be null instead
			}
			Carrier retVal=carrier.Copy();
			string command="SELECT CarrierNum FROM carrier WHERE " 
				+"CarrierName = '"    +POut.String(carrier.CarrierName)+"' "
				+"AND Address = '"    +POut.String(carrier.Address)+"' "
				+"AND Address2 = '"   +POut.String(carrier.Address2)+"' "
				+"AND City = '"       +POut.String(carrier.City)+"' "
				+"AND State = '"      +POut.String(carrier.State)+"' "
				+"AND Zip = '"        +POut.String(carrier.Zip)+"' "
				+"AND Phone = '"      +POut.String(carrier.Phone)+"' "
				+"AND ElectID = '"    +POut.String(carrier.ElectID)+"' "
				+"AND NoSendElect = '"+POut.Bool  (carrier.NoSendElect)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				//A matching carrier was found in the database, so we will use it.
				retVal.CarrierNum=PIn.Long(table.Rows[0][0].ToString());
				return retVal;
			}
			//No match found.  Decide what to do.  Usually add carrier.--------------------------------------------------------------
			//Canada:
			if(CultureInfo.CurrentCulture.Name.EndsWith("CA")){//en-CA or fr-CA
				throw new ApplicationException(Lans.g("Carriers","Carrier not found."));
				/*if(carrier.ElectID!=""){
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.String(carrier.ElectID)+"' "
						+"AND IsCDA=1";
					table=Db.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						retVal.CarrierNum=PIn.Long(table.Rows[0][0].ToString());
						//set carrier.CarrierNum to the carrier found (all other carrier fields will still be wrong)
						//throw new ApplicationException(Lans.g("Carriers","The carrier information was changed based on the EDI Code provided."));
						return retVal;
					}
				}*/
				//Notice that if inserting a carrier, it's never possible to create a canadian carrier.
			}
			Insert(carrier);
			retVal.CarrierNum=carrier.CarrierNum;
			return retVal;
		}

		///<summary>Returns an arraylist of Carriers with names similar to the supplied string.  Used in dropdown list from carrier field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one carrier doesn't show in dropdown.</summary>
		public static List<Carrier> GetSimilarNames(string carrierName){
			//No need to check RemotingRole; no call to db.
			List<Carrier> retVal=new List<Carrier>();
			for(int i=0;i<Listt.Length;i++){
				//if(i>0 && List[i].CarrierName==List[i-1].CarrierName){
				//	continue;//ignore all duplicate names
				//}
				//if(Regex.IsMatch(List[i].CarrierName,"^"+carrierName,RegexOptions.IgnoreCase))
				if(Listt[i].IsHidden){
					continue;
				}
				if(Listt[i].CarrierName.ToUpper().IndexOf(carrierName.ToUpper())==0){
					retVal.Add(Listt[i]);
				}
			}
			return retVal;
		}

		///<summary>Surround with try/catch Combines all the given carriers into one. The carrier that will be used as the basis of the combination is specified in the pickedCarrier argument. Updates insplan, then deletes all the other carriers.</summary>
		public static void Combine(List<long> carrierNums,long pickedCarrierNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),carrierNums,pickedCarrierNum);
				return;
			}
			if(carrierNums.Count==1){
				return;//nothing to do
			}
			//remove pickedCarrierNum from the carrierNums list to make the queries easier to construct.
			List<long> carrierNumList=new List<long>();
			for(int i=0;i<carrierNums.Count;i++){
				if(carrierNums[i]==pickedCarrierNum)
					continue;
				carrierNumList.Add(carrierNums[i]);
			}
			//Make sure that none of the carrierNums are in use in the etrans table
			string command="SELECT COUNT(*) FROM etrans WHERE";
			for(int i=0;i<carrierNumList.Count;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" (CarrierNum="+carrierNumList[i].ToString()+" AND CarrierTransCounter>0)";
			}
			for(int i=0;i<carrierNumList.Count;i++) {
				command+=" OR (CarrierNum2="+carrierNumList[i].ToString()+" AND CarrierTransCounter2>0)";
			}
			DataTable table=Db.GetTable(command);
			string ecount=table.Rows[0][0].ToString();
			if(ecount!="0"){
				throw new ApplicationException(Lans.g("Carriers","Not allowed to combine carriers because some are in use in the etrans table.  Number of entries involved: ")+ecount);
			}
			//Now, do the actual combining----------------------------------------------------------------------------------
			for(int i=0;i<carrierNums.Count;i++){
				if(carrierNums[i]==pickedCarrierNum)
					continue;
				command="UPDATE insplan SET CarrierNum = '"+POut.Long(pickedCarrierNum)
					+"' WHERE CarrierNum = "+POut.Long(carrierNums[i]);
				Db.NonQ(command);
				command="DELETE FROM carrier"
					+" WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				Db.NonQ(command);
			}
		}

		///<summary>Used in the FormCarrierCombine window.</summary>
		public static List<Carrier> GetCarriers(List<long> carrierNums) {
			//No need to check RemotingRole; no call to db.
			List<Carrier> retVal=new List<Carrier>();
			for(int i=0;i<Listt.Length;i++){
				for(int j=0;j<carrierNums.Count;j++){
					if(Listt[i].CarrierNum==carrierNums[j]){
						retVal.Add(Listt[i]);
						break;
					}
				}
			}
			return retVal;
		}

		///<summary>Used in FormInsPlan to check whether another carrier is already using this id.  That way, it won't tell the user that this might be an invalid id.</summary>
		public static bool ElectIdInUse(string electID){
			//No need to check RemotingRole; no call to db.
			if(electID==""){
				return true;
			}
			for(int i=0;i<Listt.Length;i++){
				if(Listt[i].ElectID==electID){
					return true;
				}
			}
			return false;
		}

		///<summary>Used from insplan window when requesting benefits.  Gets carrier based on electID.</summary>
		public static Carrier GetCanadian(string electID){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Length;i++){
				if(Listt[i].ElectID==electID){
					return Listt[i];
				}
			}
			return null;
		}

		public static Carrier GetByNameAndPhone(string carrierName,string phone){
			//No need to check RemotingRole; no call to db.
			if(carrierName==""){
				throw new ApplicationException("Carrier cannot be blank");
			}
			for(int i=0;i<Listt.Length;i++){
				if(carrierName==Listt[i].CarrierName && phone==Listt[i].Phone){
					return Listt[i].Copy();
				}
			}
			Carrier carrier=new Carrier();
			carrier.CarrierName=carrierName;
			carrier.Phone=phone;
			Insert(carrier);
			return carrier;
		}

		///<summary>Gets a dictionary of carrier names for the supplied patient list.</summary>
		public static Dictionary<long,string> GetCarrierNames(List<Patient> patients){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Dictionary<long,string>>(MethodBase.GetCurrentMethod(),patients);
			}
			if(patients.Count==0){
				return new Dictionary<long,string>();
			}
			string command="SELECT patient.PatNum,carrier.CarrierName "
				+"FROM patient "
				+"LEFT JOIN patplan ON patient.PatNum=patplan.PatNum "
				+"LEFT JOIN insplan ON patplan.PlanNum=insplan.PlanNum "
				+"LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum "
				+"WHERE";
			for(int i=0;i<patients.Count;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" patient.PatNum="+POut.Long(patients[i].PatNum);
			}
			command+=" GROUP BY patient.PatNum";
			DataTable table=Db.GetTable(command);
			Dictionary<long,string> retVal=new Dictionary<long,string>();
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.Long(table.Rows[i]["PatNum"].ToString()),table.Rows[i]["CarrierName"].ToString());
			}
			return retVal;
		}



	}

	
	

}













