using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class DefC {
		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		private static Def[][] shortt;
		///<summary>Stores all defs in a 2D array.</summary>
		private static Def[][] longg;

		public static Def[][] Long {
			get {
				if(longg==null) {
					Defs.RefreshCache();
				}
				return longg;
			}
			set {
				longg=value;
			}
		}

		public static Def[][] Short {
			get {
				if(shortt==null) {
					Defs.RefreshCache();
				}
				return shortt;
			}
			set {
				shortt=value;
			}
		}

		public static bool DefShortIsNull {
			get {
				if(shortt==null) {
					return true;
				}
				return false;
			}
		}

		///<summary>Gets a list of defs for one category.</summary>
		public static Def[] GetList(DefCat defCat) {
			return Short[(int)defCat];
		}

		///<summary>Get one def from Long.  Returns null if not found.  Only used for very limited situations.  Other Get functions tend to be much more useful since they don't return null.  There is also BIG potential for silent bugs if you use this.ItemOrder instead of GetOrder().</summary>
		public static Def GetDef(DefCat myCat,long myDefNum) {
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].Copy();
				}
			}
			return null;
		}

		///<summary></summary>
		public static string GetName(DefCat myCat,long myDefNum) {
			if(myDefNum==0){
				return "";
			}
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].ItemName;
				}
			}
			return "";
		}

		///<summary>Returns 0 if it can't find the named def.  If the name is blank, then it returns the first def in the category.</summary>
		public static long GetByExactName(DefCat myCat,string itemName) {
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

		///<summary>Returns the named def.  If it can't find the name, then it returns the first def in the category.</summary>
		public static long GetByExactNameNeverZero(DefCat myCat,string itemName) {
			if(itemName=="") {
				return DefC.Long[(int)myCat][0].DefNum;//return the first one in the list
			}
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].ItemName==itemName) {
					return DefC.Long[(int)myCat][i].DefNum;
				}
			}
			if(DefC.Long[(int)myCat].Length==0) {
				Def def=new Def();
				def.Category=myCat;
				def.ItemOrder=0;
				def.ItemName=itemName;
				Defs.Insert(def);
				Defs.RefreshCache();
			}
			return DefC.Long[(int)myCat][0].DefNum;//return the first one in the list
		}

		///<summary>Gets the order of the def within Short or -1 if not found.</summary>
		public static int GetOrder(DefCat myCat,long myDefNum) {
			//gets the index in the list of unhidden (the Short list).
			for(int i=0;i<DefC.Short[(int)myCat].GetLength(0);i++) {
				if(DefC.Short[(int)myCat][i].DefNum==myDefNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat,long myDefNum) {
			string retStr="";
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					retStr=DefC.Long[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat,long myDefNum) {
			Color retCol=Color.White;
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					retCol=DefC.Long[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetHidden(DefCat myCat,long myDefNum) {
			for(int i=0;i<DefC.Long[(int)myCat].GetLength(0);i++) {
				if(DefC.Long[(int)myCat][i].DefNum==myDefNum) {
					return DefC.Long[(int)myCat][i].IsHidden;
				}
			}
			return false;
		}

		/*//<summary>Allowed types are blank, C, or A.  Only used in FormInsPlan.</summary>
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
		}*/

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

		///<summary>Returns a DefNum for the special image category specified.  Returns 0 if no match found.</summary>
		public static long GetImageCat(ImageCategorySpecial specialCat) {
			Def[] defs=DefC.GetList(DefCat.ImageCats);
			for(int i=0;i<defs.Length;i++) {
				if(defs[i].ItemValue.Contains(specialCat.ToString())) {
					return defs[i].DefNum;
				}
			}
			return 0;
		}

	}

	///<summary></summary>
	public enum ImageCategorySpecial {
		///<summary>Show in Chart module.</summary>
		X,
		///<summary>Show in patient forms.</summary>
		F,
		///<summary>Patient picture (only one)</summary>
		P,
		///<summary>Statements (only one)</summary>
		S,
		///<summary>Graphical tooth charts and perio charts (only one)</summary>
		T
	}
}
