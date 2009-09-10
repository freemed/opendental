using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class SchoolClasses {
		///<summary>A list of all classes, ordered by year and descript.</summary>
		private static SchoolClass[] list;

		public static SchoolClass[] List {
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
			string command=
				"SELECT * FROM schoolclass "
				+"ORDER BY GradYear,Descript";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="SchoolClass";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new SchoolClass[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new SchoolClass();
				list[i].SchoolClassNum=PIn.PInt(table.Rows[i][0].ToString());
				list[i].GradYear=PIn.PInt32(table.Rows[i][1].ToString());
				list[i].Descript=PIn.PString(table.Rows[i][2].ToString());
			}
		}


		///<summary></summary>
		private static void Update(SchoolClass sc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sc);
				return;
			}
			string command= "UPDATE schoolclass SET " 
				+"SchoolClassNum = '" +POut.PInt   (sc.SchoolClassNum)+"'"
				+",GradYear = '"      +POut.PInt   (sc.GradYear)+"'"
				+",Descript = '"      +POut.PString(sc.Descript)+"'"
				+" WHERE SchoolClassNum = '"+POut.PInt(sc.SchoolClassNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		private static long Insert(SchoolClass sc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sc.SchoolClassNum=Meth.GetInt(MethodBase.GetCurrentMethod(),sc);
				return sc.SchoolClassNum;
			}
			if(PrefC.RandomKeys){
				sc.SchoolClassNum=ReplicationServers.GetKey("schoolclass","SchoolClassNum");
			}
			string command= "INSERT INTO schoolclass (";
			if(PrefC.RandomKeys){
				command+="SchoolClassNum,";
			}
			command+="GradYear,Descript) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(sc.SchoolClassNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (sc.GradYear)+"', "
				+"'"+POut.PString(sc.Descript)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				sc.SchoolClassNum=Db.NonQ(command,true);
			}
			return sc.SchoolClassNum;
		}

		///<summary></summary>
		public static void InsertOrUpdate(SchoolClass sc, bool isNew){
			//No need to check RemotingRole; no call to db.
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lans.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert(sc);
			}
			else{
				Update(sc);
			}
		}

		///<summary>Surround by a try/catch in case there are dependencies.</summary>
		public static void Delete(long classNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),classNum);
				return;
			}
			//check for attached providers
			string  command="SELECT COUNT(*) FROM provider WHERE SchoolClassNum = '"
				+POut.PInt(classNum)+"'";
			DataTable table=Db.GetTable(command);
			if(PIn.PString(table.Rows[0][0].ToString())!="0"){
				throw new Exception(Lans.g("SchoolClasses","Class already in use by providers."));
			}
			//check for attached reqneededs.
			command="SELECT COUNT(*) FROM reqneeded WHERE SchoolClassNum = '"
				+POut.PInt(classNum)+"'";
			table=Db.GetTable(command);
			if(PIn.PString(table.Rows[0][0].ToString())!="0") {
				throw new Exception(Lans.g("SchoolClasses","Class already in use by 'requirements needed' table."));
			}
			command= "DELETE from schoolclass WHERE SchoolClassNum = '"
				+POut.PInt(classNum)+"'";
 			Db.NonQ(command);
		}

		public static string GetDescript(long SchoolClassNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(List[i].SchoolClassNum==SchoolClassNum){
					return GetDescript(List[i]);
				}
			}
			return "";
		}

		public static string GetDescript(SchoolClass schoolClass) {
			//No need to check RemotingRole; no call to db.
			return schoolClass.GradYear+"-"+schoolClass.Descript;
		}


	
	}

	

	


}




















