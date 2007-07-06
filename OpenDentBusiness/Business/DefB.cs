using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary></summary>
	public class DefB {
		///<summary>This gets filled automatically when Refresh is called.  </summary>
		//public static DataTable RawData;
		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;

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
			Long=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				Long[j]=GetForCategory(j,true,table);
			}
			Short=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			for(int j=0;j<Enum.GetValues(typeof(DefCat)).Length;j++) {
				Short[j]=GetForCategory(j,false,table);
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

		///<summary>Returns the new DefNum</summary>
		public static int Insert(Def def){
			string command= "INSERT INTO definition (category,itemorder,"
				+"itemname,itemvalue,itemcolor,ishidden) VALUES("
				+"'"+POut.PInt((int)def.Category)+"', "
				+"'"+POut.PInt(def.ItemOrder)+"', "
				+"'"+POut.PString(def.ItemName)+"', "
				+"'"+POut.PString(def.ItemValue)+"', "
				+"'"+POut.PInt(def.ItemColor.ToArgb())+"', "
				+"'"+POut.PBool(def.IsHidden)+"')";
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command,true);
			int defNum=dcon.InsertID;//used in conversion
			return defNum;
		}

		///<summary></summary>
		public static int Update(Def def) {
			string command = "UPDATE definition SET "
				+ "category = '"  +POut.PInt((int)def.Category)+"'"
				+",itemorder = '" +POut.PInt(def.ItemOrder)+"'"
				+",itemname = '"  +POut.PString(def.ItemName)+"'"
				+",itemvalue = '" +POut.PString(def.ItemValue)+"'"
				+",itemcolor = '" +POut.PInt(def.ItemColor.ToArgb())+"'"
				+",ishidden = '"  +POut.PBool(def.IsHidden)+"'"
				+"WHERE defnum = '"+POut.PInt(def.DefNum)+"'";
			DataConnection dcon=new DataConnection();
			int rowsChanged=dcon.NonQ(command);
			return rowsChanged;
		}

		///<summary>Get one def from Long.  Returns null if not found.  Only used for very limited situations.  Other Get functions tend to be much more useful since they don't return null.  There is also BIG potential for silent bugs if you use this.ItemOrder instead of GetOrder().</summary>
		public static Def GetDef(DefCat myCat,int myDefNum) {
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					return Long[(int)myCat][i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static string GetName(DefCat myCat,int myDefNum) {
			if(myDefNum==0){
				return "";
			}
			if(Long==null){
				Refresh();
			}
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					return Long[(int)myCat][i].ItemName;
				}
			}
			return "";
		}

		///<summary>Returns 0 if it can't find the named def.  If the name is blank, then it returns the first def in the category.</summary>
		public static int GetByExactName(DefCat myCat, string itemName) {
			if(itemName=="") {
				return Long[(int)myCat][0].DefNum;//return the first one in the list
			}
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].ItemName==itemName) {
					return Long[(int)myCat][i].DefNum;
				}
			}
			return 0;
		}

		///<summary>Gets the order of the def within Short or -1 if not found.</summary>
		public static int GetOrder(DefCat myCat,int myDefNum) {
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<Short[(int)myCat].GetLength(0);i++) {
				if(Short[(int)myCat][i].DefNum==myDefNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat,int myDefNum) {
			string retStr="";
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					retStr=Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat,int myDefNum) {
			Color retCol=Color.White;
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					retCol=Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetHidden(DefCat myCat,int myDefNum) {
			for(int i=0;i<Long[(int)myCat].GetLength(0);i++) {
				if(Long[(int)myCat][i].DefNum==myDefNum) {
					return Long[(int)myCat][i].IsHidden;
				}
			}
			return false;
		}

		///<summary>Allowed types are blank, C, or A.  Only used in FormInsPlan.</summary>
		public static Def[] GetFeeSchedList(string type) {
			ArrayList AL=new ArrayList();
			for(int i=0;i<Short[(int)DefCat.FeeSchedNames].Length;i++) {
				if(Short[(int)DefCat.FeeSchedNames][i].ItemValue==type) {
					AL.Add(Short[(int)DefCat.FeeSchedNames][i]);
				}
			}
			Def[] retVal=new Def[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}
		

	}
}
