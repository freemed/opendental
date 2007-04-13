using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
  ///<summary></summary>
	public class ScreenGroups{
		///<summary></summary>
		public static ScreenGroup[] List;

		///<summary></summary>
		public static void Refresh(DateTime fromDate,DateTime toDate){
			string command =
				"SELECT * from screengroup "
				+"WHERE SGDate >= "+POut.PDateT(fromDate)+" "
				+"AND SGDate <= "+POut.PDateT(toDate.AddDays(1))+" "
				//added one day since it's calculated based on midnight.
				+"ORDER BY SGDate,ScreenGroupNum";
			DataTable table=General.GetTable(command);;
			List=new ScreenGroup[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ScreenGroup();
				List[i].ScreenGroupNum =                  PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description    =                  PIn.PString(table.Rows[i][1].ToString());
				List[i].SGDate         =                  PIn.PDate  (table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		public static void Insert(ScreenGroup Cur){
			if(PrefB.RandomKeys){
				Cur.ScreenGroupNum=MiscData.GetKey("screengroup","ScreenGroupNum");
			}
			string command="INSERT INTO screengroup (";
			if(PrefB.RandomKeys){
				command+="ScreenGroupNum,";
			}
			command+="Description,SGDate) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.ScreenGroupNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.Description)+"', "
				+POut.PDate  (Cur.SGDate)+")";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.ScreenGroupNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ScreenGroup Cur){
			string command = "UPDATE screengroup SET "
				+"Description ='"  +POut.PString(Cur.Description)+"'"
				+",SGDate ="      +POut.PDate  (Cur.SGDate)
				+" WHERE ScreenGroupNum = '" +POut.PInt(Cur.ScreenGroupNum)+"'";
			General.NonQ(command);
		}

		///<summary>This will also delete all screen items, so may need to ask user first.</summary>
		public static void Delete(ScreenGroup Cur){
			string command="DELETE from screen WHERE ScreenGroupNum ='"+POut.PInt(Cur.ScreenGroupNum)+"'";
			General.NonQ(command);
			command="DELETE from screengroup WHERE ScreenGroupNum ='"+POut.PInt(Cur.ScreenGroupNum)+"'";
			General.NonQ(command);
		}


	}

	

}













