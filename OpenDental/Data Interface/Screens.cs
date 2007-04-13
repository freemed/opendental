using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
  ///<summary></summary>
	public class Screens{
		///<summary></summary>
		public static OpenDentBusiness.Screen[] List;

		///<summary></summary>
		public static void Refresh(int screenGroupNum){
			string command =
				"SELECT * from screen "
				+"WHERE ScreenGroupNum = '"+POut.PInt(screenGroupNum)+"' "
				+"ORDER BY ScreenGroupOrder";
			DataTable table=General.GetTable(command);
			List=new OpenDentBusiness.Screen[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new OpenDentBusiness.Screen();
				List[i].ScreenNum       =                  PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ScreenDate      =                  PIn.PDate  (table.Rows[i][1].ToString());
				List[i].GradeSchool     =                  PIn.PString(table.Rows[i][2].ToString());
				List[i].County          =                  PIn.PString(table.Rows[i][3].ToString());
				List[i].PlaceService    =(PlaceOfService)  PIn.PInt   (table.Rows[i][4].ToString());
				List[i].ProvNum         =                  PIn.PInt   (table.Rows[i][5].ToString());
				List[i].ProvName        =                  PIn.PString(table.Rows[i][6].ToString());
				List[i].Gender          =(PatientGender)   PIn.PInt   (table.Rows[i][7].ToString());
				List[i].Race            =(PatientRace)     PIn.PInt   (table.Rows[i][8].ToString());
				List[i].GradeLevel      =(PatientGrade)    PIn.PInt   (table.Rows[i][9].ToString());
				List[i].Age             =                  PIn.PInt   (table.Rows[i][10].ToString());
				List[i].Urgency         =(TreatmentUrgency)PIn.PInt   (table.Rows[i][11].ToString());
				List[i].HasCaries       =(YN)              PIn.PInt   (table.Rows[i][12].ToString());
				List[i].NeedsSealants   =(YN)              PIn.PInt   (table.Rows[i][13].ToString());
				List[i].CariesExperience=(YN)              PIn.PInt   (table.Rows[i][14].ToString());
				List[i].EarlyChildCaries=(YN)              PIn.PInt   (table.Rows[i][15].ToString());
				List[i].ExistingSealants=(YN)              PIn.PInt   (table.Rows[i][16].ToString());
				List[i].MissingAllTeeth =(YN)              PIn.PInt   (table.Rows[i][17].ToString());
				List[i].Birthdate       =                  PIn.PDate  (table.Rows[i][18].ToString());
				List[i].ScreenGroupNum  =                  PIn.PInt   (table.Rows[i][19].ToString());
				List[i].ScreenGroupOrder=                  PIn.PInt   (table.Rows[i][20].ToString());
				List[i].Comments        =                  PIn.PString(table.Rows[i][21].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(OpenDentBusiness.Screen Cur){
			if(PrefB.RandomKeys){
				Cur.ScreenNum=MiscData.GetKey("screen","ScreenNum");
			}
			string command="INSERT INTO screen (";
			if(PrefB.RandomKeys){
				command+="ScreenNum,";
			}
			command+="ScreenDate,GradeSchool,County,PlaceService,"
				+"ProvNum,ProvName,Gender,Race,GradeLevel,Age,Urgency,HasCaries,NeedsSealants,"
				+"CariesExperience,EarlyChildCaries,ExistingSealants,MissingAllTeeth,Birthdate,"
				+"ScreenGroupNum,ScreenGroupOrder,Comments) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.ScreenNum)+"', ";
			}
			command+=
				 POut.PDate  (Cur.ScreenDate)+", "
				+"'"+POut.PString(Cur.GradeSchool)+"', "
				+"'"+POut.PString(Cur.County)+"', "
				+"'"+POut.PInt   ((int)Cur.PlaceService)+"', "
				+"'"+POut.PInt   (Cur.ProvNum)+"', "
				+"'"+POut.PString(Cur.ProvName)+"', "
				+"'"+POut.PInt   ((int)Cur.Gender)+"', "
				+"'"+POut.PInt   ((int)Cur.Race)+"', "
				+"'"+POut.PInt   ((int)Cur.GradeLevel)+"', "
				+"'"+POut.PInt   (Cur.Age)+"', "
				+"'"+POut.PInt   ((int)Cur.Urgency)+"', "
				+"'"+POut.PInt   ((int)Cur.HasCaries)+"', "
				+"'"+POut.PInt   ((int)Cur.NeedsSealants)+"', "
				+"'"+POut.PInt   ((int)Cur.CariesExperience)+"', "
				+"'"+POut.PInt   ((int)Cur.EarlyChildCaries)+"', "
				+"'"+POut.PInt   ((int)Cur.ExistingSealants)+"', "
				+"'"+POut.PInt   ((int)Cur.MissingAllTeeth)+"', "
				+POut.PDate  (Cur.Birthdate)+", "
				+"'"+POut.PInt   (Cur.ScreenGroupNum)+"', "
				+"'"+POut.PInt   (Cur.ScreenGroupOrder)+"', "
				+"'"+POut.PString(Cur.Comments)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.ScreenNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(OpenDentBusiness.Screen Cur){
			string command = "UPDATE screen SET "
				+"ScreenDate     ="    +POut.PDate  (Cur.ScreenDate)
				+",GradeSchool ='"      +POut.PString(Cur.GradeSchool)+"'"
				+",County ='"           +POut.PString(Cur.County)+"'"
				+",PlaceService ='"     +POut.PInt   ((int)Cur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PInt   (Cur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(Cur.ProvName)+"'"
				+",Gender ='"           +POut.PInt   ((int)Cur.Gender)+"'"
				+",Race ='"             +POut.PInt   ((int)Cur.Race)+"'"
				+",GradeLevel ='"       +POut.PInt   ((int)Cur.GradeLevel)+"'"
				+",Age ='"              +POut.PInt   (Cur.Age)+"'"
				+",Urgency ='"          +POut.PInt   ((int)Cur.Urgency)+"'"
				+",HasCaries ='"      +POut.PInt   ((int)Cur.HasCaries)+"'"
				+",NeedsSealants ='"    +POut.PInt   ((int)Cur.NeedsSealants)+"'"
				+",CariesExperience ='" +POut.PInt   ((int)Cur.CariesExperience)+"'"
				+",EarlyChildCaries ='" +POut.PInt   ((int)Cur.EarlyChildCaries)+"'"
				+",ExistingSealants ='" +POut.PInt   ((int)Cur.ExistingSealants)+"'"
				+",MissingAllTeeth ='"  +POut.PInt   ((int)Cur.MissingAllTeeth)+"'"
				+",Birthdate ="        +POut.PDate  (Cur.Birthdate)
				+",ScreenGroupNum ='"   +POut.PInt   (Cur.ScreenGroupNum)+"'"
				+",ScreenGroupOrder ='" +POut.PInt   (Cur.ScreenGroupOrder)+"'"
				+",Comments ='"         +POut.PString(Cur.Comments)+"'"
				+" WHERE ScreenNum = '" +POut.PInt(Cur.ScreenNum)+"'";
			General.NonQ(command);
		}

		///<summary>Updates all screens for a group with the date,prov, and location info of the current group.</summary>
		public static void UpdateForGroup(ScreenGroup ScreenGroupCur){
			string command = "UPDATE screen SET "
				+"ScreenDate     ="    +POut.PDate  (ScreenGroupCur.SGDate)
				+",GradeSchool ='"      +POut.PString(ScreenGroupCur.GradeSchool)+"'"
				+",County ='"           +POut.PString(ScreenGroupCur.County)+"'"
				+",PlaceService ='"     +POut.PInt   ((int)ScreenGroupCur.PlaceService)+"'"
				+",ProvNum ='"          +POut.PInt   (ScreenGroupCur.ProvNum)+"'"
				+",ProvName ='"         +POut.PString(ScreenGroupCur.ProvName)+"'"
				+" WHERE ScreenGroupNum = '" +ScreenGroupCur.ScreenGroupNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(OpenDentBusiness.Screen Cur){
			string command = "DELETE from screen WHERE ScreenNum = '"+POut.PInt(Cur.ScreenNum)+"'";
			General.NonQ(command);
		}


	}

	

}













