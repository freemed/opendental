using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutoCodeItems{
		///<summary></summary>
		public static AutoCodeItem[] ListForCode;//all items for a specific AutoCode

		///<summary></summary>
		public static DataTable RefreshCache(){
			string command="SELECT * FROM autocodeitem";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),command);
			table.TableName="AutoCodeItem";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			AutoCodeItemC.HList=new Hashtable();
			AutoCodeItemC.List=new AutoCodeItem[table.Rows.Count];
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				AutoCodeItemC.List[i]=new AutoCodeItem();
				AutoCodeItemC.List[i].AutoCodeItemNum= PIn.PInt   (table.Rows[i][0].ToString());
				AutoCodeItemC.List[i].AutoCodeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				//List[i].OldCode      = PIn.PString(table.Rows[i][2].ToString());
				AutoCodeItemC.List[i].CodeNum        = PIn.PInt   (table.Rows[i][3].ToString());
				if(!AutoCodeItemC.HList.ContainsKey(AutoCodeItemC.List[i].CodeNum)){
					AutoCodeItemC.HList.Add(AutoCodeItemC.List[i].CodeNum,AutoCodeItemC.List[i].AutoCodeNum);
				}
			}
		}

		///<summary></summary>
		public static void Insert(AutoCodeItem Cur){
			string command= "INSERT INTO autocodeitem (autocodenum,OldCode,CodeNum) "
				+"VALUES ("
				+"'"+POut.PInt   (Cur.AutoCodeNum)+"', "
				+"'"+POut.PString(Cur.OldCode)+"', "
				+"'"+POut.PInt   (Cur.CodeNum)+"')";
			//MessageBox.Show(string command);
			Cur.AutoCodeItemNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(AutoCodeItem Cur){
			string command= "UPDATE autocodeitem SET "
				+"AutoCodeNum='"+POut.PInt   (Cur.AutoCodeNum)+"'"
				//+",Oldcode ='"  +POut.PString(Cur.OldCode)+"'"
				+",CodeNum ='"  +POut.PInt   (Cur.CodeNum)+"'"
				+" WHERE AutoCodeItemNum = '"+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AutoCodeItem Cur){
			string command= "DELETE FROM autocodeitem WHERE AutoCodeItemNum = '"
				+POut.PInt(Cur.AutoCodeItemNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(int autoCodeNum){
			string command= "DELETE FROM autocodeitem WHERE AutoCodeNum = '"
				+POut.PInt(autoCodeNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void GetListForCode(int autoCodeNum){
			//loop through AutoCodeItems.List to fill ListForCode
			ArrayList ALtemp=new ArrayList();
			for(int i=0;i<AutoCodeItemC.List.Length;i++){
				if(AutoCodeItemC.List[i].AutoCodeNum==autoCodeNum){
					ALtemp.Add(AutoCodeItemC.List[i]);
				} 
			}
			ListForCode=new AutoCodeItem[ALtemp.Count];
			if(ALtemp.Count>0){
				ALtemp.CopyTo(ListForCode);
			}     
		}

		

		



	}

	
	


}









