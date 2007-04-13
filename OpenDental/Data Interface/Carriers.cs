using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Carriers{
		///<summary></summary>
		public static Carrier[] List;
		///<summary>A hashtable of all carriers.</summary>
		public static Hashtable HList;

		///<summary>Carriers are not refreshed as local data, but are refreshed as needed. A full refresh is frequently triggered if a carrierNum cannot be found in the HList.  Important retrieval is done directly from the db.</summary>
		public static void Refresh(){
			HList=new Hashtable();
			string command="SELECT * FROM carrier ORDER BY CarrierName";
			DataTable table=General.GetTable(command);
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
				HList.Add(List[i].CarrierNum,List[i]);
			}
		}

		///<summary>Used to get a list of carriers to display in the FormCarriers window.  Only used for Canadian right now.</summary>
		public static DataTable Refresh(bool isCanadian){
			DataTable tableRaw;
			DataTable tableReturn;
			string command;
			if(isCanadian){
				command="SELECT CarrierNum, CarrierName, ElectID, IsPMP, canadiannetwork.Abbrev, CDAnetVersion FROM carrier "
					+"LEFT JOIN canadiannetwork ON canadiannetwork.CanadianNetworkNum=carrier.CanadianNetworkNum "
					+"WHERE IsCDA=1 ORDER BY CarrierName";
				tableRaw=General.GetTable(command);
				tableReturn=new DataTable();
				tableReturn.Columns.Add("CarrierNum");
				tableReturn.Columns.Add("CarrierName");
				tableReturn.Columns.Add("ElectID");
				tableReturn.Columns.Add("PMP");
				tableReturn.Columns.Add("Network");
				tableReturn.Columns.Add("Version");
				tableReturn.Columns.Add("Trans02");
				tableReturn.Columns.Add("Trans03");
				tableReturn.Columns.Add("Trans04");
				tableReturn.Columns.Add("Trans05");
				tableReturn.Columns.Add("Trans06");
				tableReturn.Columns.Add("Trans07");
				tableReturn.Columns.Add("Trans08");
				DataRow row;
				for(int i=0;i<tableRaw.Rows.Count;i++){
					row=tableReturn.NewRow();
					row["CarrierNum"]=tableRaw.Rows[i]["CarrierNum"].ToString();
					row["CarrierName"]=tableRaw.Rows[i]["CarrierName"].ToString();
					row["ElectID"]=tableRaw.Rows[i]["ElectID"].ToString();
					if(PIn.PBool(tableRaw.Rows[i]["IsPMP"].ToString())){
						row["PMP"]="X";
					}
					else{
						row["PMP"]="";
					}
					row["Network"]=tableRaw.Rows[i]["Abbrev"].ToString();
					row["Version"]=tableRaw.Rows[i]["CDAnetVersion"].ToString();
					row["Trans02"]="X";
					row["Trans03"]="X";
					row["Trans04"]="X";
					row["Trans05"]="X";
					row["Trans06"]="X";
					row["Trans07"]="X";
					row["Trans08"]="X";
					tableReturn.Rows.Add(row);
				}
				return tableReturn;
			}
			return null;
		}

		///<summary>Surround with try/catch.</summary>
		public static void Update(Carrier Cur){
			string command;
			DataTable table;
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA") {//en-CA or fr-CA
				if(Cur.IsCDA) {
					if(Cur.ElectID=="") {
						throw new ApplicationException(Lan.g("Carriers","EDI Code required."));
					}
					if(!Regex.IsMatch(Cur.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lan.g("Carriers","EDI Code must be exactly 6 numbers."));
					}
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(Cur.ElectID)+"' "
						+"AND IsCDA=1 "
						+"AND CarrierNum != "+POut.PInt(Cur.CarrierNum);
					table=General.GetTable(command);
					if(table.Rows.Count>0) {//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lan.g("Carriers","EDI Code already in use."));
					}
				}
				//so the edited carrier looks good, but now we need to make sure that the original was allowed to be changed.
				command="SELECT ElectID,IsCDA FROM carrier WHERE CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'";
				table=General.GetTable(command);
				if(PIn.PBool(table.Rows[0][1].ToString())//if original carrier IsCDA
					&& PIn.PString(table.Rows[0][0].ToString()) !=Cur.ElectID)//and the ElectID was changed
				{
					command="SELECT COUNT(*) FROM etrans WHERE CarrierNum= "+POut.PInt(Cur.CarrierNum)
						+" OR CarrierNum2="+POut.PInt(Cur.CarrierNum);
					if(General.GetCount(command)!="0"){
						throw new ApplicationException(Lan.g("Carriers","Not allowed to change EDI Code because it's in use in the claim history."));
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
				+"WHERE CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary>Surround with try/catch if possibly adding a Canadian carrier.</summary>
		public static void Insert(Carrier Cur){
			string command;
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(Cur.IsCDA){
					if(Cur.ElectID==""){
						throw new ApplicationException(Lan.g("Carriers","EDI Code required."));
					}
					if(!Regex.IsMatch(Cur.ElectID,"^[0-9]{6}$")) {
						throw new ApplicationException(Lan.g("Carriers","EDI Code must be exactly 6 numbers."));
					}
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(Cur.ElectID)+"' "
						+"AND IsCDA=1";
					DataTable table=General.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						throw new ApplicationException(Lan.g("Carriers","EDI Code already in use."));
					}
				}
			}
			if(PrefB.RandomKeys){
				Cur.CarrierNum=MiscData.GetKey("carrier","CarrierNum");
			}
			command="INSERT INTO carrier (";
			if(PrefB.RandomKeys){
				command+="CarrierNum,";
			}
			command+="CarrierName,Address,Address2,City,State,Zip,Phone,ElectID,NoSendElect,"
				+"IsCDA,IsPMP,CDAnetVersion,CanadianNetworkNum) VALUES(";
			if(PrefB.RandomKeys){
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
				+"'"+POut.PInt   (Cur.CanadianNetworkNum)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.CarrierNum=General.NonQ(command,true);
			}
			//id used in the conversion process for 2.8
		}

		///<summary>Surround with try/catch.  If there are any dependencies, then this will throw an exception.  This is currently only called from FormCarrierEdit.</summary>
		public static void Delete(Carrier Cur){
			//look for dependencies in insplan table.
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" AND insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			string strInUse;
			if(table.Rows.Count>0){
				strInUse="";//new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0){
						strInUse+=", ";
					}
					strInUse+=PIn.PString(table.Rows[i][0].ToString());
				}
				throw new ApplicationException(Lan.g("Carriers","Not allowed to delete carrier because it is in use.  Subscribers using this carrier include ")+strInUse);
			}
			//look for dependencies in etrans table.
			command="SELECT DateTimeTrans FROM etrans WHERE CarrierNum="+POut.PInt(Cur.CarrierNum)
				+" OR CarrierNum2="+POut.PInt(Cur.CarrierNum);
			table=General.GetTable(command);
			if(table.Rows.Count>0){
				strInUse="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>0) {
						strInUse+=", ";
					}
					strInUse+=PIn.PDateT(table.Rows[i][0].ToString()).ToShortDateString();
				}
				throw new ApplicationException(Lan.g("Carriers","Not allowed to delete carrier because it is in use in the etrans table.  Dates of claim sent history include ")+strInUse);
			}
			command="DELETE from carrier WHERE CarrierNum = "+POut.PInt(Cur.CarrierNum);
			General.NonQ(command);
		}

		///<summary>Returns a list of insplans that are dependent on the Cur carrier. Used to display in carrier edit.</summary>
		public static string[] DependentPlans(Carrier Cur){
			string command="SELECT CONCAT(CONCAT(LName,', '),FName) FROM patient,insplan" 
				+" WHERE patient.PatNum=insplan.Subscriber"
				+" AND insplan.CarrierNum = '"+POut.PInt(Cur.CarrierNum)+"'"
				+" ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			string[] retStr=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				retStr[i]=PIn.PString(table.Rows[i][0].ToString());
			}
			return retStr;
		}

		///<summary>Gets the name of a carrier based on the carrierNum.  This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static string GetName(int carrierNum){
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//if the carrierNum could not be found:
			Refresh();
			if(HList.ContainsKey(carrierNum)){
				return ((Carrier)HList[carrierNum]).CarrierName;
			}
			//this could only happen if corrupted:
			return "";
		}

		///<summary>Replacing GetCur. Gets the specified carrier. This also refreshes the list if necessary, so it will work even if the list has not been refreshed recently.</summary>
		public static Carrier GetCarrier(int carrierNum){
			if(carrierNum==0){
				Carrier retVal=new Carrier();
				retVal.CarrierName="";//instead of null. Helps prevent crash.
				return retVal;
			}
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//if the carrierNum could not be found:
			Refresh();
			if(HList.ContainsKey(carrierNum)){
				return (Carrier)HList[carrierNum];
			}
			//this could only happen if corrupted:
			Carrier retVall=new Carrier();
			retVall.CarrierName="";//instead of null. Helps prevent crash.
			return retVall;
		}

		///<summary>Primarily used when user clicks OK from the InsPlan window.  Gets a carrierNum from the database based on the other supplied carrier data.  Sets Cur.CarrierNum accordingly. If there is no matching carrier, then a new carrier is created.  The end result is that Cur will now always have a valid carrierNum to use.</summary>
		public static void GetCurSame(Carrier Cur){
			if(Cur.CarrierName==""){
				Cur=new Carrier();//should probably be null instead
				return;
			}
			string command="SELECT CarrierNum FROM carrier WHERE " 
				+"CarrierName = '"   +POut.PString(Cur.CarrierName)+"' "
				+"AND Address = '"    +POut.PString(Cur.Address)+"' "
				+"AND Address2 = '"   +POut.PString(Cur.Address2)+"' "
				+"AND City = '"       +POut.PString(Cur.City)+"' "
				+"AND State = '"      +POut.PString(Cur.State)+"' "
				+"AND Zip = '"        +POut.PString(Cur.Zip)+"' "
				+"AND Phone = '"      +POut.PString(Cur.Phone)+"' "
				+"AND ElectID = '"    +POut.PString(Cur.ElectID)+"' "
				+"AND NoSendElect = '"+POut.PBool  (Cur.NoSendElect)+"'";
			DataTable table=General.GetTable(command);
			if(table.Rows.Count>0){
				//A matching carrier was found in the database, so we will use it.
				Cur.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
				return;
			}
			//No match found.  Decide what to do.  Usually add carrier.--------------------------------------------------------------
			//Canada:
			if(CultureInfo.CurrentCulture.Name.Substring(3)=="CA"){//en-CA or fr-CA
				if(Cur.ElectID!=""){
					command="SELECT CarrierNum FROM carrier WHERE "
						+"ElectID = '"+POut.PString(Cur.ElectID)+"' "
						+"AND IsCDA=1";
					table=General.GetTable(command);
					if(table.Rows.Count>0){//if there already exists a Canadian carrier with that ElectID
						Cur.CarrierNum=PIn.PInt(table.Rows[0][0].ToString());
						//set Cur.CarrierNum to the carrier found (all other carrier fields will still be wrong)
						throw new ApplicationException
							(Lan.g("Carriers","The carrier information was changed based on the EDI Code provided."));
					}
				}
				//Notice that if inserting a carrier, it's never possible to create a canadian carrier.
			}
			Insert(Cur);
		}

		///<summary>Returns an arraylist of Carriers with names similar to the supplied string.  Used in dropdown list from carrier field for faster entry.  There is a small chance that the list will not be completely refreshed when this is run, but it won't really matter if one carrier doesn't show in dropdown.</summary>
		public static ArrayList GetSimilarNames(string carrierName){
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				//if(i>0 && List[i].CarrierName==List[i-1].CarrierName){
				//	continue;//ignore all duplicate names
				//}
				//if(Regex.IsMatch(List[i].CarrierName,"^"+carrierName,RegexOptions.IgnoreCase))
				if(List[i].CarrierName.ToUpper().IndexOf(carrierName.ToUpper())==0)
					retVal.Add(List[i]);
			}
			return retVal;
		}

		///<summary>Surround with try/catch Combines all the given carriers into one. The carrier that will be used as the basis of the combination is specified in the pickedCarrier argument. Updates insplan, then deletes all the other carriers.</summary>
		public static void Combine(int[] carrierNums,int pickedCarrierNum){
			if(carrierNums.Length==1){
				return;//nothing to do
			}
			//remove pickedCarrierNum from the carrierNums list to make the queries easier to construct.
			List<int> carrierNumList=new List<int>();
			for(int i=0;i<carrierNums.Length;i++){
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
			DataTable table=General.GetTable(command);
			string ecount=table.Rows[0][0].ToString();
			if(ecount!="0"){
				throw new ApplicationException(Lan.g("Carriers","Not allowed to combine carriers because some are in use in the etrans table.  Number of entries involved: ")+ecount);
			}
			//Now, do the actual combining----------------------------------------------------------------------------------
			for(int i=0;i<carrierNums.Length;i++){
				if(carrierNums[i]==pickedCarrierNum)
					continue;
				command="UPDATE insplan SET CarrierNum = '"+POut.PInt(pickedCarrierNum)
					+"' WHERE CarrierNum = "+POut.PInt(carrierNums[i]);
				General.NonQ(command);
				command="DELETE FROM carrier"
					+" WHERE CarrierNum = '"+carrierNums[i].ToString()+"'";
				General.NonQ(command);
			}
		}

		///<summary>Used in the FormCarrierCombine window.</summary>
		public static List<Carrier> GetCarriers(int[] carrierNums){
			List<Carrier> retVal=new List<Carrier>();
			for(int i=0;i<List.Length;i++){
				for(int j=0;j<carrierNums.Length;j++){
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
			for(int i=0;i<List.Length;i++){
				if(List[i].ElectID==electID){
					return List[i];
				}
			}
			return null;
		}



	}

	
	

}













