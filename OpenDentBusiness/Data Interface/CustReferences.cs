using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CustReferences{

		///<summary>Gets one CustReference from the db.</summary>
		public static CustReference GetOne(long custReferenceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CustReference>(MethodBase.GetCurrentMethod(),custReferenceNum);
			}
			return Crud.CustReferenceCrud.SelectOne(custReferenceNum);
		}

		///<summary></summary>
		public static long Insert(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				custReference.CustReferenceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),custReference);
				return custReference.CustReferenceNum;
			}
			return Crud.CustReferenceCrud.Insert(custReference);
		}

		///<summary></summary>
		public static void Update(CustReference custReference){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReference);
				return;
			}
			Crud.CustReferenceCrud.Update(custReference);
		}

		///<summary>Might not be used.  Might implement when a patient is deleted but doesn't happen often if ever.</summary>
		public static void Delete(long custReferenceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custReferenceNum);
				return;
			}
			string command= "DELETE FROM custreference WHERE CustReferenceNum = "+POut.Long(custReferenceNum);
			Db.NonQ(command);
		}

		///<summary>Used only from FormReferenceSelect to get the list of references.</summary>
		public static DataTable GetReferenceTable(bool limit,bool showBadRefs,bool showUsed,string city,string state,string zip,
			string areaCode,string specialty,int superFam,string lname,string fname,string patnum,int age) 
		{
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),limit,showBadRefs,showUsed,city,state,zip,areaCode,specialty,superFam,lname,fname,patnum,age);
			}
			string phonedigits="";
			for(int i=0;i<areaCode.Length;i++){
				if(Regex.IsMatch(areaCode[i].ToString(),"[0-9]")){
					phonedigits=phonedigits+areaCode[i];
				}
			}
			string regexp="";
			for(int i=0;i<phonedigits.Length;i++){
				if(i!=0){
					regexp+="[^0-9]*";//zero or more intervening digits that are not numbers
				}
				regexp+=phonedigits[i];
			}
			DataTable table=new DataTable();
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("CustReferenceNum");
			table.Columns.Add("PatNum");
			table.Columns.Add("FName");
			table.Columns.Add("LName");
			table.Columns.Add("HmPhone");
			table.Columns.Add("State");
			table.Columns.Add("City");
			table.Columns.Add("Zip");
			table.Columns.Add("specialty");
			table.Columns.Add("age");
			table.Columns.Add("superFam");//Number of super family members.
			table.Columns.Add("DateMostRecent");
			table.Columns.Add("timesUsed");
			table.Columns.Add("IsBadRef");
			List<DataRow> rows=new List<DataRow>();
			string command=" WHERE CustReferenceNum<>0 ";//Just something to always have in the where clause.
			if(age > 0) {
				command+="AND Birthdate <"+POut.Date(DateTime.Now.AddYears(-age))+" ";
			}
			if(regexp!="") {
				command+="AND (HmPhone REGEXP '"+POut.String(regexp)+"' "
					+"OR WkPhone REGEXP '"+POut.String(regexp)+"' "
					+"OR WirelessPhone REGEXP '"+POut.String(regexp)+"') ";
			}
			command+=(lname.Length>0?"AND (LName LIKE '"+POut.String(lname)+"%' OR Preferred LIKE '"+POut.String(lname)+"%') ":"")
					+(fname.Length>0?"AND (FName LIKE '"+POut.String(fname)+"%' OR Preferred LIKE '"+POut.String(fname)+"%') ":"")
					+(city.Length>0?"AND City LIKE '"+POut.String(city)+"%' ":"")
					+(state.Length>0?"AND State LIKE '"+POut.String(state)+"%' ":"")
					+(zip.Length>0?"AND Zip LIKE '"+POut.String(zip)+"%' ":"")
					+(patnum.Length>0?"AND PatNum LIKE '"+POut.String(patnum)+"%' ":"");
			if(superFam>0) {
				command+="";//do something for super fam.
			}
			if(specialty.Length>0) {
				command+="";//do something for specialty.
			}
			if(showUsed) {
				command+="";//do something for count>0
			}
			if(limit){
				command=DbHelper.LimitOrderBy(command,40);//Might need to be more than 40.
			}
			DataTable rawtable=Db.GetTable(command);
			for(int i=0;i<rawtable.Rows.Count;i++) {
				row=table.NewRow();
				row["CustReferenceNum"]=rawtable.Rows[i]["CustReferenceNum"].ToString();
				row["PatNum"]=rawtable.Rows[i]["PatNum"].ToString();
				row["FName"]=rawtable.Rows[i]["FName"].ToString();
				row["LName"]=rawtable.Rows[i]["LName"].ToString();
				row["HmPhone"]=rawtable.Rows[i]["HmPhone"].ToString();
				row["State"]=rawtable.Rows[i]["State"].ToString();
				row["City"]=rawtable.Rows[i]["City"].ToString();
				row["Zip"]=rawtable.Rows[i]["Zip"].ToString();
				row["specialty"]="";//Figure out the specialty.
				row["age"]=Patients.DateToAge(PIn.Date(table.Rows[i]["Birthdate"].ToString())).ToString();
				row["superFam"]="";//Figure out the superFam
				row["DateMostRecent"]=PIn.DateT(rawtable.Rows[i]["DateMostRecent"].ToString()).ToShortDateString();
				row["timesUsed"]="";//Figure out the timesUsed
				row["IsBadRef"]="";
				if(showBadRefs) {
					row["IsBadRef"]=rawtable.Rows[i]["IsBadRef"].ToString();
				}
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}



	}
}