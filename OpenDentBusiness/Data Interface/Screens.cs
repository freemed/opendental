using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
  ///<summary></summary>
	public class Screens{
	
		///<summary></summary>
		public static Screen[] Refresh(long screenGroupNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Screen[]>(MethodBase.GetCurrentMethod(),screenGroupNum);
			}
			string command =
				"SELECT * from screen "
				+"WHERE ScreenGroupNum = '"+POut.Long(screenGroupNum)+"' "
				+"ORDER BY ScreenGroupOrder";
			DataTable table=Db.GetTable(command);
			Screen[] List=new OpenDentBusiness.Screen[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new OpenDentBusiness.Screen();
				List[i].ScreenNum       =                  PIn.Long   (table.Rows[i][0].ToString());
				List[i].ScreenDate      =                  PIn.Date  (table.Rows[i][1].ToString());
				List[i].GradeSchool     =                  PIn.String(table.Rows[i][2].ToString());
				List[i].County          =                  PIn.String(table.Rows[i][3].ToString());
				List[i].PlaceService    =(PlaceOfService)  PIn.Long   (table.Rows[i][4].ToString());
				List[i].ProvNum         =                  PIn.Long   (table.Rows[i][5].ToString());
				List[i].ProvName        =                  PIn.String(table.Rows[i][6].ToString());
				List[i].Gender          =(PatientGender)   PIn.Long   (table.Rows[i][7].ToString());
				List[i].Race            =(PatientRace)     PIn.Long   (table.Rows[i][8].ToString());
				List[i].GradeLevel      =(PatientGrade)    PIn.Long   (table.Rows[i][9].ToString());
				List[i].Age             =                  PIn.Byte   (table.Rows[i][10].ToString());
				List[i].Urgency         =(TreatmentUrgency)PIn.Long   (table.Rows[i][11].ToString());
				List[i].HasCaries       =(YN)              PIn.Long   (table.Rows[i][12].ToString());
				List[i].NeedsSealants   =(YN)              PIn.Long   (table.Rows[i][13].ToString());
				List[i].CariesExperience=(YN)              PIn.Long   (table.Rows[i][14].ToString());
				List[i].EarlyChildCaries=(YN)              PIn.Long   (table.Rows[i][15].ToString());
				List[i].ExistingSealants=(YN)              PIn.Long   (table.Rows[i][16].ToString());
				List[i].MissingAllTeeth =(YN)              PIn.Long   (table.Rows[i][17].ToString());
				List[i].Birthdate       =                  PIn.Date  (table.Rows[i][18].ToString());
				List[i].ScreenGroupNum  =                  PIn.Long   (table.Rows[i][19].ToString());
				List[i].ScreenGroupOrder=                  PIn.Int   (table.Rows[i][20].ToString());
				List[i].Comments        =                  PIn.String(table.Rows[i][21].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static long Insert(OpenDentBusiness.Screen Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ScreenNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ScreenNum;
			}
			return Crud.ScreenCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Update(OpenDentBusiness.Screen Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.ScreenCrud.Update(Cur);
		}

		///<summary>Updates all screens for a group with the date,prov, and location info of the current group.</summary>
		public static void UpdateForGroup(ScreenGroup ScreenGroupCur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ScreenGroupCur);
				return;
			}
			string command = "UPDATE screen SET "
				+"ScreenDate     ="    +POut.Date  (ScreenGroupCur.SGDate)
				+",GradeSchool ='"      +POut.String(ScreenGroupCur.GradeSchool)+"'"
				+",County ='"           +POut.String(ScreenGroupCur.County)+"'"
				+",PlaceService ='"     +POut.Long   ((int)ScreenGroupCur.PlaceService)+"'"
				+",ProvNum ='"          +POut.Long   (ScreenGroupCur.ProvNum)+"'"
				+",ProvName ='"         +POut.String(ScreenGroupCur.ProvName)+"'"
				+" WHERE ScreenGroupNum = '" +ScreenGroupCur.ScreenGroupNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(OpenDentBusiness.Screen Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from screen WHERE ScreenNum = '"+POut.Long(Cur.ScreenNum)+"'";
			Db.NonQ(command);
		}


	}

	

}













