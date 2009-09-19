using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class ReportSimpleGrid{
		///<summary></summary>
		public DataTable TableQ;
		///<summary></summary>
		public DataTable TableTemp;
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
		///<summary>This is a quick hack to allow printing account numbers on deposit slips in a different font.  It will typically be null.</summary>
		public string SummaryFont;


		///<summary></summary>
		public void SubmitQuery(){
			string command = Query;
			DataTable table=Reports.GetTable(command);
			TableQ=table.Copy();
		}

		///<summary>Runs the query and fills TableTemp with the result.</summary>
		public void SubmitTemp(){
			string command = Query;
			DataTable table=Reports.GetTable(command);
			TableTemp=table.Copy();
		}

	}


}













