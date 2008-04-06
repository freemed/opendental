using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class DefC {
		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;

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
				DefD.Refresh();
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
