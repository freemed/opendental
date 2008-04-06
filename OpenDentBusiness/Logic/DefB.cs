using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class DefB {

		///<summary>If using remoting, then the calling program is responsible for filling the arrays on the client since the automated part only happens on the server.  So there are TWO sets of arrays in a server situation, but only one in a small office that connects directly to the database.</summary>
		public static DataSet Refresh(){
			string command="SELECT * FROM definition ORDER BY Category,ItemOrder";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			DataSet retVal=new DataSet();
			retVal.Tables.Add(table);
			FillArrays(table);
			return retVal;
		}

		public static void FillArrays(DataTable table){
			DefC.Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Long[j]=GetForCategory(j,true,table);
			}
			DefC.Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				DefC.Short[j]=GetForCategory(j,false,table);
			}
		}

		///<summary>Used by the refresh method above.</summary>
		private static Def[] GetForCategory(int catIndex,bool includeHidden,DataTable table) {
			List<Def> list=new List<Def>();
			Def def;
			for(int i=0;i<table.Rows.Count;i++) {
				if(PIn.PInt(table.Rows[i][1].ToString())!=catIndex) {
					continue;
				}
				if(PIn.PBool(table.Rows[i][6].ToString())//if is hidden
					&& !includeHidden)//and we don't want to include hidden
				{
					continue;
				}
				def=new Def();
				def.DefNum    = PIn.PInt(table.Rows[i][0].ToString());
				def.Category  = (DefCat)PIn.PInt(table.Rows[i][1].ToString());
				def.ItemOrder = PIn.PInt(table.Rows[i][2].ToString());
				def.ItemName  = PIn.PString(table.Rows[i][3].ToString());
				def.ItemValue = PIn.PString(table.Rows[i][4].ToString());
				def.ItemColor = Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				def.IsHidden  = PIn.PBool(table.Rows[i][6].ToString());
				list.Add(def);
			}
			return list.ToArray();
		}

		///<summary>Get one def from Long.  Returns null if not found.  Only used for very limited situations.  Other Get functions tend to be much more useful since they don't return null.  There is also BIG potential for silent bugs if you use this.ItemOrder instead of GetOrder().</summary>
		public static Def GetDef(DefCat myCat,int myDefNum) {
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static string GetName(DefCat myCat,int myDefNum) {
			if(myDefNum==0){
				return "";
			}
			if(DefC.Long==null){
				Refresh();
			}
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].ItemName;
				}
			}
			return "";
		}

		///<summary>Returns 0 if it can't find the named def.  If the name is blank, then it returns the first def in the category.</summary>
		public static int GetByExactName(DefCat myCat, string itemName) {
			if(itemName=="") {
				return DefC.Long[(int)myCat][0].DefNum;//return the first one in the list
			}
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].ItemName==itemName) {
					return DefC.Long[(int)myCat][i].DefNum;
				}
			}
			return 0;
		}

		///<summary>Gets the order of the def within Short or -1 if not found.</summary>
		public static int GetOrder(DefCat myCat,int myDefNum) {
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<DefC.Short[(int)myCat].GetLength(0);i++) {
				if(DefC.Short[(int)myCat][i].DefNum==myDefNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat,int myDefNum) {
			string retStr="";
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					retStr=DefC.Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat,int myDefNum) {
			Color retCol=Color.White;
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					retCol=DefC.Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetHidden(DefCat myCat,int myDefNum) {
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].IsHidden;
				}
			}
			return false;
		}

		///<summary>Allowed types are blank, C, or A.  Only used in FormInsPlan.</summary>
		public static Def[] GetFeeSchedList(string type) {
			ArrayList AL=new ArrayList();
			for(int i=0;i<DefC.Short[(int)DefCat.FeeSchedNames].Length;i++) {
				if(DefC.Short[(int)DefCat.FeeSchedNames][i].ItemValue==type) {
					AL.Add(DefC.Short[(int)DefCat.FeeSchedNames][i]);
				}
			}
			Def[] retVal=new Def[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		///<summary></summary>
		public static List<Def> GetPositiveAdjTypes() {
			List<Def> retVal=new List<Def>();
			for(int i=0;i<DefC.Short[(int)DefCat.AdjTypes].Length;i++) {
				if(DefC.Short[(int)DefCat.AdjTypes][i].ItemValue=="+") {
					retVal.Add(DefC.Short[(int)DefCat.AdjTypes][i]);
				}
			}
			return retVal;
		}
		

	}
}
