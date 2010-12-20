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
			if(PrefC.RandomKeys){
				Cur.ScreenNum=ReplicationServers.GetKey("screen","ScreenNum");
			}
			string command="INSERT INTO screen (";
			if(PrefC.RandomKeys){
				command+="ScreenNum,";
			}
			command+="ScreenDate,GradeSchool,County,PlaceService,"
				+"ProvNum,ProvName,Gender,Race,GradeLevel,Age,Urgency,HasCaries,NeedsSealants,"
				+"CariesExperience,EarlyChildCaries,ExistingSealants,MissingAllTeeth,Birthdate,"
				+"ScreenGroupNum,ScreenGroupOrder,Comments) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(Cur.ScreenNum)+"', ";
			}
			command+=
				 POut.Date  (Cur.ScreenDate)+", "
				+"'"+POut.String(Cur.GradeSchool)+"', "
				+"'"+POut.String(Cur.County)+"', "
				+"'"+POut.Long   ((int)Cur.PlaceService)+"', "
				+"'"+POut.Long   (Cur.ProvNum)+"', "
				+"'"+POut.String(Cur.ProvName)+"', "
				+"'"+POut.Long   ((int)Cur.Gender)+"', "
				+"'"+POut.Long   ((int)Cur.Race)+"', "
				+"'"+POut.Long   ((int)Cur.GradeLevel)+"', "
				+"'"+POut.Long   (Cur.Age)+"', "
				+"'"+POut.Long   ((int)Cur.Urgency)+"', "
				+"'"+POut.Long   ((int)Cur.HasCaries)+"', "
				+"'"+POut.Long   ((int)Cur.NeedsSealants)+"', "
				+"'"+POut.Long   ((int)Cur.CariesExperience)+"', "
				+"'"+POut.Long   ((int)Cur.EarlyChildCaries)+"', "
				+"'"+POut.Long   ((int)Cur.ExistingSealants)+"', "
				+"'"+POut.Long   ((int)Cur.MissingAllTeeth)+"', "
				+POut.Date  (Cur.Birthdate)+", "
				+"'"+POut.Long   (Cur.ScreenGroupNum)+"', "
				+"'"+POut.Long   (Cur.ScreenGroupOrder)+"', "
				+"'"+POut.String(Cur.Comments)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.ScreenNum=Db.NonQ(command,true);
			}
			return Cur.ScreenNum;
		}

		///<summary></summary>
		public static void Update(OpenDentBusiness.Screen Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE screen SET "
				+"ScreenDate     ="    +POut.Date  (Cur.ScreenDate)
				+",GradeSchool ='"      +POut.String(Cur.GradeSchool)+"'"
				+",County ='"           +POut.String(Cur.County)+"'"
				+",PlaceService ='"     +POut.Long   ((int)Cur.PlaceService)+"'"
				+",ProvNum ='"          +POut.Long   (Cur.ProvNum)+"'"
				+",ProvName ='"         +POut.String(Cur.ProvName)+"'"
				+",Gender ='"           +POut.Long   ((int)Cur.Gender)+"'"
				+",Race ='"             +POut.Long   ((int)Cur.Race)+"'"
				+",GradeLevel ='"       +POut.Long   ((int)Cur.GradeLevel)+"'"
				+",Age ='"              +POut.Long   (Cur.Age)+"'"
				+",Urgency ='"          +POut.Long   ((int)Cur.Urgency)+"'"
				+",HasCaries ='"      +POut.Long   ((int)Cur.HasCaries)+"'"
				+",NeedsSealants ='"    +POut.Long   ((int)Cur.NeedsSealants)+"'"
				+",CariesExperience ='" +POut.Long   ((int)Cur.CariesExperience)+"'"
				+",EarlyChildCaries ='" +POut.Long   ((int)Cur.EarlyChildCaries)+"'"
				+",ExistingSealants ='" +POut.Long   ((int)Cur.ExistingSealants)+"'"
				+",MissingAllTeeth ='"  +POut.Long   ((int)Cur.MissingAllTeeth)+"'"
				+",Birthdate ="        +POut.Date  (Cur.Birthdate)
				+",ScreenGroupNum ='"   +POut.Long   (Cur.ScreenGroupNum)+"'"
				+",ScreenGroupOrder ='" +POut.Long   (Cur.ScreenGroupOrder)+"'"
				+",Comments ='"         +POut.String(Cur.Comments)+"'"
				+" WHERE ScreenNum = '" +POut.Long(Cur.ScreenNum)+"'";
			Db.NonQ(command);
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













