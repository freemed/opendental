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
		private static Carrier[] list;
		private static Hashtable hList;

		public static Carrier[] List {
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
			HList=new Hashtable();
			string command="SELECT * FROM carrier ORDER BY CarrierName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodInfo.GetCurrentMethod(),command);
			table.TableName="Carrier";
			FillCache(table);
			return table;
		}
		
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			List=new Carrier[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Carrier();
				List[i].CarrierNum  =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].CarrierName =PIn.PString(table.Rows[i][1].ToString());
				List[i].Address     =PIn.PString(table.Rows[i][2].ToString());
				List[i].Address2    =PIn.PString(table.Rows[i][3].ToString());
				List[i].City        =PIn.PString(table.Rows[i][4].ToString());
				List[i].State       =PIn.PString(table.Rows[i][5].ToString());
				List[i].Zip         =PIn.PString(table.Rows[i][6].ToString());
				List[i].Phone       =PIn.PString(table.Rows[i][7].ToString());
				List[i].ElectID     =PIn.PString(table.Rows[i][8].ToString());
				List[i].NoSendElect =PIn.PBool  (table.Rows[i][9].ToString());
				List[i].IsCDA       =PIn.PBool  (table.Rows[i][10].ToString());
				List[i].IsPMP       =PIn.PBool(table.Rows[i][11].ToString());
				List[i].CDAnetVersion=PIn.PString(table.Rows[i][12].ToString());
				List[i].CanadianNetworkNum=PIn.PInt(table.Rows[i][13].ToString());
				List[i].IsHidden     =PIn.PBool(table.Rows[i][14].ToString());
				HList.Add(List[i].CarrierNum,List[i]);
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
				+"COUNT(insplan.PlanNum) insPlanCount,"
				+"carrier.IsHidden,IsPMP,Phone,State,Zip "
				+"FROM carrier "
				+"LEFT JOIN canadiannetwork ON canadiannetwork.CanadianNetworkNum=carrier.CanadianNetworkNum "
				+"LEFT JOIN insplan ON insplan.CarrierNum=carrier.CarrierNum "
				+"WHERE "
				+"CarrierName LIKE '%"+POut.PString(carrierName)+"%' ";
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
			table.Columns.Add("isHidden");
			table.Columns.Add("Phone");
			table.Columns.Add("pMP");
			table.Columns.Add("network");
			table.Columns.Add("State");
			table.Columns.Add("trans02");
			table.Columns.Add("trans03");
			table.Columns.Add("trans04");
			table.Columns.Add("trans05");
			table.Columns.Add("trans06");
			table.Columns.Add("trans07");
			table.Columns.Add("trans08");
			table.Columns.Add("version");
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
				if(PIn.PBool(tableRaw.Rows[i]["IsHidden"].ToString())){
					row["isHidden"]="X";
				}
				else{
					row["isHidden"]="";
				}
				row["insPlanCount"]=tableRaw.Rows[i]["insPlanCount"].ToString();
				row["Phone"]=tableRaw.Rows[i]["Phone"].ToString();
				if(PIn.PBool(tableRaw.Rows[i]["IsPMP"].ToString())){
					row["pMP"]="X";
				}
				else{
					row["pMP"]="";
				}
				row["network"]=tableRaw.Rows[i]["Abbrev"].ToString();
				row["State"]=tableRaw.Rows[i]["State"].ToString();
				row["trans02"]="X";
				row["trans03"]="X";
				row["trans04"]="X";
				row["trans05"]="X";
				row["trans06"]="X";
				row["trans07"]="X";
				row["trans08"]="X";
				row["version"]=tableRaw.Rows[i]["CDAnetVersion"].ToString();
				row["Zip"]=tableRaw.Rows[i]["Zip"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

		///<summary>Surround with try/catch.</summary>
		public static void Update(Carrier Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command;
			DataTable table;
			if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {//en-CA or fr-CA
				if(Cur.IsCDA) {
					if(Cur.ElectID=="") {
						throw new ApplicationException(Lans.g("Carriers","EDI Code required."));
					}
					if(!Regex.IsMatch(Cur.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lans.g("Carriers","EDI Code must be exactly 6 numbers."));
					}
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(Cur.ElectID)+"' "
						+"AND IsCDA=1 "
						+"AND CarrierNum != "+POut.PInt(Cur.CarrierNum);
					table=Db.GetTable(command);
					if(table.Rows.Count>0) {//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lans.g("Carriers","EDI Code already in use."));
					}
				}
				//so the edited carrier looks good, but now we need to make sure that the original was allowed to be changed.
				command="SELECT ElectID,IsCDA FROM carrier WHERE CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'";
				table=Db.GetTable(command);
				if(PIn.PBool(table.Rows[0][1].ToString())//if original carrier IsCDA
					&& PIn.PString(table.Rows[0][0].ToString()) !=Cur.ElectID)//and the ElectID was changed
				{
					command="SELECT COUNT(*) FROM etrans WHERE CarrierNum= "+POut.PInt(Cur.CarrierNum)
						+" OR CarrierNum2="+POut.PInt(Cur.CarrierNum);
					if(Db.GetCount(command)!="0"){
						throw new ApplicationException(Lans.g("Carriers","Not allowed to change EDI Code because it's in use in the claim history."));
					}
				}
			}
			command="UPDATE carrier SET "
				+ "CarrierName= '" +POut.PString(Cur.CarrierName)+"' "
				+ ",Address= '"    +POut.PString(Cur.Address)+"' "
				+ ",Address2= '"   +POut.PString(Cur.Address2)+"' "
				+ ",City= '"       +POut.PString(Cur.City)+"' "
				+ ",State= '"      +POut.PString(Cur.State)+"' "
				+ ",Zip= '"        +POut.PString(Cur.Zip)+"' "
				+ ",Phone= '"      +POut.PString(Cur.Phone)+"' "
				+ ",ElectID= '"    +POut.PString(Cur.ElectID)+"' "
				+ ",NoSendElect= '"+POut.PBool  (Cur.NoSendElect)+"' "
				+ ",IsCDA= '"      +POut.PBool  (Cur.IsCDA)+"' "
				+ ",IsPMP= '"      +POut.PBool  (Cur.IsPMP)+"' "
				+ ",CDAnetVersion= '"+POut.PString(Cur.CDAnetVersion)+"' "
				+ ",CanadianNetworkNum= '"+POut.PInt(Cur.CanadianNetworkNum)+"' "
				+ ",IsHidden= '"     +POut.PBool(Cur.IsHidden)+"' "
				+"WHERE CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'";
			//MessageBox.Show(string command);
			Db.NonQ(command);
		}

		///<summary>Surround with try/catch if possibly adding a Canadian carrier.</summary>
		public static int Insert(Carrier Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.CarrierNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.CarrierNum;
			}
			string command;
			if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(Cur.IsCDA){
					if(Cur.ElectID==""){
						throw new ApplicationException(Lans.g("Carriers","EDI Code required."));
					}
					if(!Regex.IsMatch(Cur.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lans.g("Carriers","EDI Code must be exactly 6 numbers."));
					}
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(Cur.ElectID)+"' "
						+"AND IsCDA=1";
					DataTable table=Db.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lans.g("Carriers","EDI Code already in use."));
					}
				}
			}
			if(PrefC.RandomKeys){
				Cur.CarrierNum=MiscData.GetKey("carrier","CarrierNum");
			}
			command="INSERT INTO carrier (";
			if(PrefC.RandomKeys){
				command+="CarrierNum,";
			}
			command+="CarrierName,Address,Address2,City,State,Zip,Phone,ElectID,NoSendElect,"
				+"IsCDA,IsPMP,CDAnetVersion,CanadianNetworkNum,IsHidden) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.CarrierNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.CarrierName)+"', "
				+"'"+POut.PString(Cur.Address)+"', "
				+"'"+POut.PString(Cur.Address2)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PString(Cur.Zip)+"', "
				+"'"+POut.PString(Cur.Phone)+"', "
				+"'"+POut.PString(Cur.ElectID)+"', "
				+"'"+POut.PBool  (Cur.NoSendElect)+"', "
				+"'"+POut.PBool  (Cur.IsCDA)+"', "
				+"'"+POut.PBool  (Cur.IsPMP)+"', "
				+"'"+POut.PString(Cur.CDAnetVersion)+"', "
				+"'"+POut.PInt   (Cur.CanadianNetworkNum)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.CarrierNum=Db.NonQ(command,true);
			}
			return Cur.CarrierNum;
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
				+" AND insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string strInUse;
			if(table.Rows.Count>0){
				strInUse="";//new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0){
						strInUse+=", ";
					}
					strInUse+=PIn.PString(table.Rows[i][0].ToString());
				}
				throw new ApplicationException(Lans.g("Carriers","Not allowed to delete carrier because it is in use.  Subscribers using this carrier include ")+strInUse);
			}
			//look for dependencies in etrans table.
			command="SELECT DateTimeTrans FROM etrans WHERE CarrierNum="+POut.PInt(Cur.CarrierNum)
				+" OR CarrierNum2="+POut.PInt(Cur.CarrierNum);
			table=Db.GetTable(command);
			if(table.Rows.Count>0){
				strInUse="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0) {
						strInUse+=", ";
					}
					strInUse+=PIn.PDateT(table.Rows[i][0].ToString()).ToShortDateString();
				}
				throw new ApplicationException(Lans.g("Carriers","Not allowed to delete carrier because it is in use in the etrans table.  Dates of claim sent history include ")+strInUse);
			}
			command="DELETE from carrier WHERE CarrierNum = "+POut.PInt(Cur.CarrierNum);
			Db.NonQ(command);
		}

		///<summary>Returns a list of insplans that are dependent on the Cur carrier. Used to display in carrier edit.</summary>
		public static string[] DependentPlans(Carrier Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<string[]>(MethodBase.GetCurrentMethod(),Cur);
			}
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" AND insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=Db.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of a carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(int carrierNum){
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

		///<summary>Replacing GetCur. Gets the specified carrier. This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static Carrier GetCarrier(int carrierNum){
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
				+"CarrierName = '"    +POut.PString(carrier.CarrierName)+"' "
				+"AND Address = '"    +POut.PString(carrier.Address)+"' "
				+"AND Address2 = '"   +POut.PString(carrier.Address2)+"' "
				+"AND City = '"       +POut.PString(carrier.City)+"' "
				+"AND State = '"      +POut.PString(carrier.State)+"' "
				+"AND Zip = '"        +POut.PString(carrier.Zip)+"' "
				+"AND Phone = '"      +POut.PString(carrier.Phone)+"' "
				+"AND ElectID = '"    +POut.PString(carrier.ElectID)+"' "
				+"AND NoSendElect = '"+POut.PBool  (carrier.NoSendElect)+"'";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count>0){
				//A matching carrier was found in the database, so we will use it.
				retVal.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
				return retVal;
			}
			//No match found.  Decide what to do.  Usually add carrier.--------------------------------------------------------------
			//Canada:
			if(CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(carrier.ElectID!=""){
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(carrier.ElectID)+"' "
						+"AND IsCDA=1";
					table=Db.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						retVal.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
						//set carrier.CarrierNum to the carrier found (all other carrier fields will still be wrong)
						//throw new ApplicationException(Lans.g("Carriers","The carrier information was changed based on the EDI Code provided."));
						return retVal;
					}
				}
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
			for(int i=0;i<List.Length;i++){
				//if(i>0 && List[i].CarrierName==List[i-1].CarrierName){
				//	continue;//ignore all duplicate names
				//}
				//if(Regex.IsMatch(List[i].CarrierName,"^"+carrierName,RegexOptions.IgnoreCase))
				if(List[i].IsHidden){
					continue;
				}
				if(List[i].CarrierName.ToUpper().IndexOf(carrierName.ToUpper())==0){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Surround with try/catch Combines all the given carriers into one. The carrier that will be used as the basis of the combination is specified in the pickedCarrier argument. Updates insplan, then deletes all the other carriers.</summary>
		public static void Combine(List <int> carrierNums,int pickedCarrierNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),carrierNums,pickedCarrierNum);
				return;
			}
			if(carrierNums.Count==1){
				return;//nothing to do
			}
			//remove pickedCarrierNum from the carrierNums list to make the queries easier to construct.
			List<int> carrierNumList=new List<int>();
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
				command="UPDATE insplan SET CarrierNum = '"+POut.PInt(pickedCarrierNum)
					+"' WHERE CarrierNum = "+POut.PInt(carrierNums[i]);
				Db.NonQ(command);
				command="DELETE FROM carrier"
					+" WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				Db.NonQ(command);
			}
		}

		///<summary>Used in the FormCarrierCombine window.</summary>
		public static List<Carrier> GetCarriers(List <int> carrierNums){
			//No need to check RemotingRole; no call to db.
			List<Carrier> retVal=new List<Carrier>();
			for(int i=0;i<List.Length;i++){
				for(int j=0;j<carrierNums.Count;j++){
					if(List[i].CarrierNum==carrierNums[j]){
						retVal.Add(List[i]);
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
			for(int i=0;i<List.Length;i++){
				if(List[i].ElectID==electID){
					return true;
				}
			}
			return false;
		}

		///<summary>Used from insplan window when requesting benefits.  Gets carrier based on electID.</summary>
		public static Carrier GetCanadian(string electID){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].ElectID==electID){
					return List[i];
				}
			}
			return null;
		}

		public static Carrier GetByNameAndPhone(string carrierName,string phone){
			//No need to check RemotingRole; no call to db.
			if(carrierName==""){
				throw new ApplicationException("Carrier cannot be blank");
			}
			for(int i=0;i<List.Length;i++){
				if(carrierName==List[i].CarrierName && phone==List[i].Phone){
					return List[i].Copy();
				}
			}
			Carrier carrier=new Carrier();
			carrier.CarrierName=carrierName;
			carrier.Phone=phone;
			Insert(carrier);
			return carrier;
		}

		///<summary>Gets a dictionary of carrier names for the supplied patient list.</summary>
		public static Dictionary<int,string> GetCarrierNames(List<Patient> patients){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Dictionary<int,string>>(MethodBase.GetCurrentMethod(),patients);
			}
			if(patients.Count==0){
				return new Dictionary<int,string>();
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
				command+=" patient.PatNum="+POut.PInt(patients[i].PatNum);
			}
			command+=" GROUP BY patient.PatNum";
			DataTable table=Db.GetTable(command);
			Dictionary<int,string> retVal=new Dictionary<int,string>();
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.PInt(table.Rows[i]["PatNum"].ToString()),table.Rows[i]["CarrierName"].ToString());
			}
			return retVal;
		}



	}

	
	

}













