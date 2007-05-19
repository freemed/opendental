using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class ReqNeededs{

		public static DataTable Refresh(int schoolClass,int schoolCourse){
			string command="SELECT * FROM reqneeded WHERE SchoolClassNum="+POut.PInt(schoolClass)
				+" AND SchoolCourseNum="+POut.PInt(schoolCourse)
				+" ORDER BY Descript";
			return General.GetTable(command);
		}

		public static ReqNeeded GetReq(int reqNeededNum){
			string command="SELECT * FROM reqneeded WHERE ReqNeededNum="+POut.PInt(reqNeededNum);
 			DataTable table=General.GetTable(command);
			ReqNeeded req=new ReqNeeded();
			//for(int i=0;i<table.Rows.Count;i++){
			req.ReqNeededNum   = PIn.PInt   (table.Rows[0][0].ToString());
			req.Descript       = PIn.PString(table.Rows[0][1].ToString());
			req.SchoolCourseNum= PIn.PInt   (table.Rows[0][2].ToString());
			req.SchoolClassNum = PIn.PInt   (table.Rows[0][3].ToString());
			return req;
		}

		///<summary></summary>
		public static void Update(ReqNeeded req) {
			string command = "UPDATE reqneeded SET " 
				+ "Descript = '"        +POut.PString(req.Descript)+"'"
				+ ",SchoolCourseNum = '"+POut.PInt   (req.SchoolCourseNum)+"'"
				+ ",SchoolClassNum = '" +POut.PInt   (req.SchoolClassNum)+"'"   
				+" WHERE ReqNeededNum = '" +POut.PInt(req.ReqNeededNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(ReqNeeded req) {
			if(PrefB.RandomKeys) {
				req.ReqNeededNum=MiscData.GetKey("reqneeded","ReqNeededNum");
			}
			string command= "INSERT INTO reqneeded (";
			if(PrefB.RandomKeys) {
				command+="ReqNeededNum,";
			}
			command+="Descript,SchoolCourseNum,SchoolClassNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(req.ReqNeededNum)+"', ";
			}
			command+=
				 "'"+POut.PString(req.Descript)+"', "
				+"'"+POut.PInt   (req.SchoolCourseNum)+"', "
				+"'"+POut.PInt   (req.SchoolClassNum)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				req.ReqNeededNum=General.NonQ(command,true);
			}
		}

		///<summary>Surround with try/catch.</summary>
		public static void Delete(int reqNeededNum) {
			//still need to validate
			string command= "DELETE FROM reqneeded "
				+"WHERE ReqNeededNum = "+POut.PInt(reqNeededNum);
			General.NonQ(command);
		}


		




	}

	

	


}













