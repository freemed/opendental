using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CovSpans {

		///<summary></summary>
		public static DataTable RefreshCache() {
			string command=
				"SELECT * FROM covspan"
				+" ORDER BY FromCode";
			//+" ORDER BY CovCatNum";
			DataTable table=Gen.GetTable(MethodInfo.GetCurrentMethod(),command);
			table.TableName="CovSpan";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			CovSpanC.List=new CovSpan[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				CovSpanC.List[i]=new CovSpan();
				CovSpanC.List[i].CovSpanNum  = PIn.PInt(table.Rows[i][0].ToString());
				CovSpanC.List[i].CovCatNum   = PIn.PInt(table.Rows[i][1].ToString());
				CovSpanC.List[i].FromCode    = PIn.PString(table.Rows[i][2].ToString());
				CovSpanC.List[i].ToCode      = PIn.PString(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		private static void Update(CovSpan span) {
			string command="UPDATE covspan SET "
				+"CovCatNum = '"+POut.PInt   (span.CovCatNum)+"'"
				+",FromCode = '"+POut.PString(span.FromCode)+"'"
				+",ToCode = '"  +POut.PString(span.ToCode)+"'"
				+" WHERE CovSpanNum = '"+POut.PInt(span.CovSpanNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(CovSpan span) {
			string command="INSERT INTO covspan (CovCatNum,"
				+"FromCode,ToCode) VALUES("
				+"'"+POut.PInt   (span.CovCatNum)+"', "
				+"'"+POut.PString(span.FromCode)+"', "
				+"'"+POut.PString(span.ToCode)+"')";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void InsertOrUpdate(CovSpan span, bool IsNew){
			if(span.FromCode=="" || span.ToCode=="") {
				throw new ApplicationException(Lan.g("FormInsSpanEdit","Codes not allowed to be blank."));
			}
			if(String.Compare(span.ToCode,span.FromCode)<0){
				throw new ApplicationException(Lan.g("FormInsSpanEdit","From Code must be less than To Code.  Remember that the comparison is alphabetical, not numeric.  For instance, 100 would come before 2, but after 02."));
			}
			if(IsNew){
				Insert(span);
			}
			else{
				Update(span);
			}
		}

		///<summary></summary>
		public static void Delete(CovSpan span) {
			string command="DELETE FROM covspan"
				+" WHERE CovSpanNum = '"+POut.PInt(span.CovSpanNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static int GetCat(string myCode){
			int retVal=0;
			for(int i=0;i<CovSpanC.List.Length;i++){
				if(String.Compare(myCode,CovSpanC.List[i].FromCode)>=0
					&& String.Compare(myCode,CovSpanC.List[i].ToCode)<=0){
					retVal=CovSpanC.List[i].CovCatNum;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static CovSpan[] GetForCat(int catNum){
			ArrayList AL=new ArrayList();
			for(int i=0;i<CovSpanC.List.Length;i++){
				if(CovSpanC.List[i].CovCatNum==catNum){
					AL.Add(CovSpanC.List[i].Copy());
				}
			}
			CovSpan[] retVal=new CovSpan[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

	}

	


}









