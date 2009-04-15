using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutoCodes{

		///<summary></summary>
		public static DataTable RefreshCache(){
			string command="SELECT * from autocode";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),command);
			table.TableName="AutoCode";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			AutoCodeC.HList=new Hashtable();
			AutoCodeC.List=new AutoCode[table.Rows.Count];
			ArrayList ALshort=new ArrayList();//int of indexes of short list
			for(int i = 0;i<AutoCodeC.List.Length;i++){
				AutoCodeC.List[i]=new AutoCode();
				AutoCodeC.List[i].AutoCodeNum  = PIn.PInt   (table.Rows[i][0].ToString());
				AutoCodeC.List[i].Description  = PIn.PString(table.Rows[i][1].ToString());
				AutoCodeC.List[i].IsHidden     = PIn.PBool  (table.Rows[i][2].ToString());	
				AutoCodeC.List[i].LessIntrusive= PIn.PBool  (table.Rows[i][3].ToString());	
				AutoCodeC.HList.Add(AutoCodeC.List[i].AutoCodeNum,AutoCodeC.List[i]);
				if(!AutoCodeC.List[i].IsHidden){
					ALshort.Add(i);
				}
			}
			AutoCodeC.ListShort=new AutoCode[ALshort.Count];
			for(int i=0;i<ALshort.Count;i++){
				AutoCodeC.ListShort[i]=AutoCodeC.List[(int)ALshort[i]];
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

		///<summary>Used in ProcButtons.SetToDefault.  Returns 0 if the given autocode does not exist.</summary>
		public static int GetNumFromDescript(string descript) {
			for(int i=0;i<AutoCodeC.ListShort.Length;i++) {
				if(AutoCodeC.ListShort[i].Description==descript) {
					return AutoCodeC.ListShort[i].AutoCodeNum;
				}
			}
			return 0;
		}

		


	}

	


}









