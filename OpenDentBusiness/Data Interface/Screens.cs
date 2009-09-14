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
				+"WHERE ScreenGroupNum = '"+POut.PLong(screenGroupNum)+"' "
				+"ORDER BY ScreenGroupOrder";
			DataTable table=Db.GetTable(command);
			Screen[] List=new OpenDentBusiness.Screen[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new OpenDentBusiness.Screen();
				List[i].ScreenNum       =                  PIn.PLong   (table.Rows[i][0].ToString());
				List[i].ScreenDate      =                  PIn.PDate  (table.Rows[i][1].ToString());
				List[i].GradeSchool     =                  PIn.PString(table.Rows[i][2].ToString());
				List[i].County          =                  PIn.PString(table.Rows[i][3].ToString());
				List[i].PlaceService    =(PlaceOfService)  PIn.PLong   (table.Rows[i][4].ToString());
				List[i].ProvNum         =                  PIn.PLong   (table.Rows[i][5].ToString());
				List[i].ProvName        =                  PIn.PString(table.Rows[i][6].ToString());
				List[i].Gender          =(PatientGender)   PIn.PLong   (table.Rows[i][7].ToString());
				List[i].Race            =(PatientRace)     PIn.PLong   (table.Rows[i][8].ToString());
				List[i].GradeLevel      =(PatientGrade)    PIn.PLong   (table.Rows[i][9].ToString());
				List[i].Age             =                  PIn.PInt   (table.Rows[i][10].ToString());
				List[i].Urgency         =(TreatmentUrgency)PIn.PLong   (table.Rows[i][11].ToString());
				List[i].HasCaries       =(YN)              PIn.PLong   (table.Rows[i][12].ToString());
				List[i].NeedsSealants   =(YN)              PIn.PLong   (table.Rows[i][13].ToString());
				List[i].CariesExperience=(YN)              PIn.PLong   (table.Rows[i][14].ToString());
				List[i].EarlyChildCaries=(YN)              PIn.PLong   (table.Rows[i][15].ToString());
				List[i].ExistingSealants=(YN)              PIn.PLong   (table.Rows[i][16].ToString());
				List[i].MissingAllTeeth =(YN)              PIn.PLong   (table.Rows[i][17].ToString());
				List[i].Birthdate       =                  PIn.PDate  (table.Rows[i][18].ToString());
				List[i].ScreenGroupNum  =                  PIn.PLong   (table.Rows[i][19].ToString());
				List[i].ScreenGroupOrder=                  PIn.PInt   (table.Rows[i][20].ToString());
				List[i].Comments        =                  PIn.PString(table.Rows[i][21].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static long Insert(OpenDentBusiness.Screen Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ScreenNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
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
				command+="'"+POut.PLong(Cur.ScreenNum)+"', ";
			}
			command+=
				 POut.PDate  (Cur.ScreenDate)+", "
				+"'"+POut.PString(Cur.GradeSchool)+"', "
				+"'"+POut.PString(Cur.County)+"', "
				+"'"+POut.PLong   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PLong   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.ProvName)+"', "
				+"'"+POut.PLong   ((int)Cur.Gender)+"', "
				+"'"+POut.PLong   ((int)Cur.Race)+"', "
				+"'"+POut.PLong   ((int)Cur.GradeLevel)+"', "
				+"'"+POut.PLong   (Cur.Age)+"', "
				+"'"+POut.PLong   ((int)Cur.Urgency)+"', "
				+"'"+POut.PLong   ((int)Cur.HasCaries)+"', "
				+"'"+POut.PLong   ((int)Cur.NeedsSealants)+"', "
				+"'"+POut.PLong   ((int)Cur.CariesExperience)+"', "
				+"'"+POut.PLong   ((int)Cur.EarlyChildCaries)+"', "
				+"'"+POut.PLong   ((int)Cur.ExistingSealants)+"', "
				+"'"+POut.PLong   ((int)Cur.MissingAllTeeth)+"', "
				+POut.PDate  (Cur.Birthdate)+", "
				+"'"+POut.PLong   (Cur.ScreenGroupNum)+"', "
				+"'"+POut.PLong   (Cur.ScreenGroupOrder)+"', "
				+"'"+POut.PString(Cur.Comments)+"')";
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
				+"ScreenDate     ="    +POut.PDate  (Cur.ScreenDate)
				+",GradeSchool ='"      +POut.PString(Cur.GradeSchool)+"'"
				+",County ='"           +POut.PString(Cur.County)+"'"
				+",PlaceService ='"     +POut.PLong   ((int)Cur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PLong   (Cur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(Cur.ProvName)+"'"
				+",Gender ='"           +POut.PLong   ((int)Cur.Gender)+"'"
				+",Race ='"             +POut.PLong   ((int)Cur.Race)+"'"
				+",GradeLevel ='"       +POut.PLong   ((int)Cur.GradeLevel)+"'"
				+",Age ='"              +POut.PLong   (Cur.Age)+"'"
				+",Urgency ='"          +POut.PLong   ((int)Cur.Urgency)+"'"
				+",HasCaries ='"      +POut.PLong   ((int)Cur.HasCaries)+"'"
				+",NeedsSealants ='"    +POut.PLong   ((int)Cur.NeedsSealants)+"'"
				+",CariesExperience ='" +POut.PLong   ((int)Cur.CariesExperience)+"'"
				+",EarlyChildCaries ='" +POut.PLong   ((int)Cur.EarlyChildCaries)+"'"
				+",ExistingSealants ='" +POut.PLong   ((int)Cur.ExistingSealants)+"'"
				+",MissingAllTeeth ='"  +POut.PLong   ((int)Cur.MissingAllTeeth)+"'"
				+",Birthdate ="        +POut.PDate  (Cur.Birthdate)
				+",ScreenGroupNum ='"   +POut.PLong   (Cur.ScreenGroupNum)+"'"
				+",ScreenGroupOrder ='" +POut.PLong   (Cur.ScreenGroupOrder)+"'"
				+",Comments ='"         +POut.PString(Cur.Comments)+"'"
				+" WHERE ScreenNum = '" +POut.PLong(Cur.ScreenNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Updates all screens for a group with the date,prov, and location info of the current group.</summary>
		public static void UpdateForGroup(ScreenGroup ScreenGroupCur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ScreenGroupCur);
				return;
			}
			string command = "UPDATE screen SET "
				+"ScreenDate     ="    +POut.PDate  (ScreenGroupCur.SGDate)
				+",GradeSchool ='"      +POut.PString(ScreenGroupCur.GradeSchool)+"'"
				+",County ='"           +POut.PString(ScreenGroupCur.County)+"'"
				+",PlaceService ='"     +POut.PLong   ((int)ScreenGroupCur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PLong   (ScreenGroupCur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(ScreenGroupCur.ProvName)+"'"
				+" WHERE ScreenGroupNum = '" +ScreenGroupCur.ScreenGroupNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(OpenDentBusiness.Screen Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from screen WHERE ScreenNum = '"+POut.PLong(Cur.ScreenNum)+"'";
			Db.NonQ(command);
		}


	}

	

}













