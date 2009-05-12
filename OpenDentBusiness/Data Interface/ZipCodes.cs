using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
  ///<summary></summary>
	public class ZipCodes{
		///<summary></summary>
		private static ZipCode[] list;
		///<summary></summary>
		private static ArrayList aLFrequent;
		///<summary>Only used from UI.</summary>
		public static ArrayList ALMatches;
		
		public static ZipCode[] List {
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

		public static ArrayList ALFrequent {
			//No need to check RemotingRole; no call to db.
			get {
				if(aLFrequent==null) {
					RefreshCache();
				}
				return aLFrequent;
			}
			set {
				aLFrequent=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
			"SELECT * from zipcode ORDER BY zipcodedigits";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ZipCode";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			aLFrequent=new ArrayList();
			list=new ZipCode[table.Rows.Count];
			for(int i=0;i<list.Length;i++) {
				list[i]=new ZipCode();
				list[i].ZipCodeNum=PIn.PInt(table.Rows[i][0].ToString());
				list[i].ZipCodeDigits=PIn.PString(table.Rows[i][1].ToString());
				list[i].City=PIn.PString(table.Rows[i][2].ToString());
				list[i].State=PIn.PString(table.Rows[i][3].ToString());
				list[i].IsFrequent=PIn.PBool(table.Rows[i][4].ToString());
				if(list[i].IsFrequent) {
					aLFrequent.Add(list[i]);
				}
			}
		}

		///<summary></summary>
		public static void Insert(ZipCode Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(PrefC.RandomKeys){
				Cur.ZipCodeNum=MiscData.GetKey("zipcode","ZipCodeNum");
			}
			string command="INSERT INTO zipcode (";
			if(PrefC.RandomKeys){
				 command+="ZipCodeNum,";
			}
			command+="zipcodedigits,city,state,isfrequent) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.ZipCodeNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.ZipCodeDigits)+"', "
				+"'"+POut.PString(Cur.City)+"', "
				+"'"+POut.PString(Cur.State)+"', "
				+"'"+POut.PBool  (Cur.IsFrequent)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.ZipCodeNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ZipCode Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE zipcode SET "
				+"zipcodedigits ='"+POut.PString(Cur.ZipCodeDigits)+"'"
				+",city ='"        +POut.PString(Cur.City)+"'"
				+",state ='"       +POut.PString(Cur.State)+"'"
				+",isfrequent ='"  +POut.PBool  (Cur.IsFrequent)+"'"
				+" WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ZipCode Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from zipcode WHERE zipcodenum = '"+POut.PInt(Cur.ZipCodeNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void GetALMatches(string zipCodeDigits){
			//No need to check RemotingRole; no call to db.
			ALMatches=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ZipCodeDigits==zipCodeDigits){
					ALMatches.Add(List[i]);
				}
			}

		}

	}

	

}













