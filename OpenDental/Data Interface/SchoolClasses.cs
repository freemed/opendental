using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class SchoolClasses {
		///<summary></summary>
		public static SchoolClass[] List;

		///<summary>Refreshes all SchoolClasses.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM schoolclass "
				+"ORDER BY GradYear,Descript";
			DataTable table=General.GetTable(command);
			List=new SchoolClass[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new SchoolClass();
				List[i].SchoolClassNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].GradYear      = PIn.PInt(table.Rows[i][1].ToString());
				List[i].Descript      = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		private static void Update(SchoolClass sc){
			string command= "UPDATE schoolclass SET " 
				+"SchoolClassNum = '" +POut.PInt   (sc.SchoolClassNum)+"'"
				+",GradYear = '"      +POut.PInt   (sc.GradYear)+"'"
				+",Descript = '"      +POut.PString(sc.Descript)+"'"
				+" WHERE SchoolClassNum = '"+POut.PInt(sc.SchoolClassNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(SchoolClass sc){
			if(PrefB.RandomKeys){
				sc.SchoolClassNum=MiscData.GetKey("schoolclass","SchoolClassNum");
			}
			string command= "INSERT INTO schoolclass (";
			if(PrefB.RandomKeys){
				command+="SchoolClassNum,";
			}
			command+="GradYear,Descript) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(sc.SchoolClassNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (sc.GradYear)+"', "
				+"'"+POut.PString(sc.Descript)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				sc.SchoolClassNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(SchoolClass sc, bool isNew){
			//if(IsRepeating && DateTask.Year>1880){
			//	throw new Exception(Lan.g(this,"Task cannot be tagged repeating and also have a date."));
			//}
			if(isNew){
				Insert(sc);
			}
			else{
				Update(sc);
			}
		}

		///<summary>Surround by a try/catch in case there are dependencies.</summary>
		public static void Delete(SchoolClass sc){
			string  command="SELECT COUNT(*) FROM provider WHERE SchoolClassNum = '"
				+POut.PInt(sc.SchoolClassNum)+"'";
			DataTable table=General.GetTable(command);
			if(PIn.PString(table.Rows[0][0].ToString())!="0"){
				throw new Exception(Lan.g("SchoolClasses","Class alread in use by providers."));
			}
			command= "DELETE from schoolclass WHERE SchoolClassNum = '"
				+POut.PInt(sc.SchoolClassNum)+"'";
 			General.NonQ(command);
		}

		public static string GetDescript(int SchoolClassNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].SchoolClassNum==SchoolClassNum){
					return List[i].GradYear+"-"+List[i].Descript;
				}
			}
			return "";
		}




	
	}

	

	


}




















