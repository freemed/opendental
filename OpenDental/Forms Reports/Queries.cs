using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace OpenDental{
	
	


	/*=========================================================================================
		=================================== class Queries ==========================================*/
///<summary>This is a very old class that will be replaced by the new reporting classes.</summary>
	public class Queries{
		//public static string queryString;
		///<summary></summary>
		public static DataTable TableQ;
		///<summary></summary>
		public static DataTable TableTemp;
		///<summary></summary>
		public static ReportOld CurReport;

		///<summary></summary>
		public static void SubmitCur(){
			string command = CurReport.Query;
			DataTable table=General.GetTableLow(command);
			TableQ=table.Copy();
		}

		///<summary>Runs the query and fills TableTemp with the result.</summary>
		public static void SubmitTemp(){
			string command = CurReport.Query;
			DataTable table=General.GetTable(command);
			TableTemp=table.Copy();
		}

		///<summary></summary>
		public static void SubmitNonQ(){
			string command = CurReport.Query;
			General.NonQ(command);
		}
	}

	///<summary>Not a database table.</summary>
	public struct ReportOld{
		///<summary></summary>
		public string Query;
		///<summary></summary>
		public string Title;
		///<summary></summary>
		public string[] SubTitle;
		///<summary>Always 1 extra for right boundary of right col</summary>
		public int[] ColPos;
		///<summary></summary>
		public string[] ColCaption;
		///<summary></summary>
		public HorizontalAlignment[] ColAlign;
		///<summary></summary>
		public double[] ColTotal;
		///<summary></summary>
		public int[] ColWidth;
		///<summary></summary>
		public string[] Summary;
	}


}













