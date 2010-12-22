using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
  ///<summary></summary>
	public class ScreenGroups{

		///<summary></summary>
		public static List<ScreenGroup> Refresh(DateTime fromDate,DateTime toDate){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ScreenGroup>>(MethodBase.GetCurrentMethod(),fromDate,toDate);
			}
			string command =
				"SELECT * from screengroup "
				+"WHERE SGDate >= "+POut.DateT(fromDate)+" "
				+"AND SGDate <= "+POut.DateT(toDate.AddDays(1))+" "
				//added one day since it's calculated based on midnight.
				+"ORDER BY SGDate,ScreenGroupNum";
			//return Crud.ScreenGroupCrud.SelectMany(command);
			DataTable table=Db.GetTable(command);;
			List<ScreenGroup> list=new List<ScreenGroup>();
			ScreenGroup sg;
			for(int i=0;i<table.Rows.Count;i++){
				sg=new ScreenGroup();
				sg.ScreenGroupNum =                  PIn.Long(table.Rows[i][0].ToString());
				sg.Description    =                  PIn.String(table.Rows[i][1].ToString());
				sg.SGDate         =                  PIn.Date(table.Rows[i][2].ToString());
				list.Add(sg);
			}
			return list;
		}

		///<summary></summary>
		public static long Insert(ScreenGroup Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ScreenGroupNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ScreenGroupNum;
			}
			return Crud.ScreenGroupCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Update(ScreenGroup Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.ScreenGroupCrud.Update(Cur);
		}

		///<summary>This will also delete all screen items, so may need to ask user first.</summary>
		public static void Delete(ScreenGroup Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from screen WHERE ScreenGroupNum ='"+POut.Long(Cur.ScreenGroupNum)+"'";
			Db.NonQ(command);
			command="DELETE from screengroup WHERE ScreenGroupNum ='"+POut.Long(Cur.ScreenGroupNum)+"'";
			Db.NonQ(command);
		}


	}

	

}













