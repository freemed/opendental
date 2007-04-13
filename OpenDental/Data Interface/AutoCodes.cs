using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class AutoCodes{
		///<summary></summary>
		public static AutoCode[] List;
		///<summary></summary>
		public static AutoCode[] ListShort;
		///<summary>key=AutoCodeNum, value=AutoCode</summary>
		public static Hashtable HList; 

		///<summary></summary>
		public static void Refresh(){
			string command="SELECT * from autocode";
			DataTable table=General.GetTable(command);
			HList=new Hashtable();
			List=new AutoCode[table.Rows.Count];
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<List.Length;i++){
				List[i]=new AutoCode();
				List[i].AutoCodeNum  = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description  = PIn.PString(table.Rows[i][1].ToString());
				List[i].IsHidden     = PIn.PBool  (table.Rows[i][2].ToString());	
				List[i].LessIntrusive= PIn.PBool  (table.Rows[i][3].ToString());	
				HList.Add(List[i].AutoCodeNum,List[i]);
				if(!List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				ListShort[i]=List[(int)ALshort[i]];
			}
		}

		///<summary></summary>
		public static void Insert(AutoCode Cur){
			string command= "INSERT INTO autocode (Description,IsHidden,LessIntrusive) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PBool  (Cur.IsHidden)+"', "
				+"'"+POut.PBool  (Cur.LessIntrusive)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCode Cur){
			string command= "UPDATE autocode SET "
				+"Description='"      +POut.PString(Cur.Description)+"'"
				+",IsHidden = '"      +POut.PBool  (Cur.IsHidden)+"'"
				+",LessIntrusive = '" +POut.PBool  (Cur.LessIntrusive)+"'"
				+" WHERE autocodenum = '"+POut.PInt (Cur.AutoCodeNum)+"'";
			General.NonQ(command);
		}

		///<summary>This could be improved since it does not delete any autocode items.</summary>
		public static void Delete(AutoCode Cur){
			string command= "DELETE from autocode WHERE autocodenum = '"+POut.PInt(Cur.AutoCodeNum)+"'";
			General.NonQ(command);
		}

		


	}

	


}









