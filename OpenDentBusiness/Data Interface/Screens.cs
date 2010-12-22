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
			return Crud.ScreenCrud.SelectMany(command).ToArray();
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













