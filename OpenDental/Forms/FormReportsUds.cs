using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormReportsUds:Form {
		private DateTime DateFrom;
		private DateTime DateTo;

		public FormReportsUds() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReportsUds_Load(object sender,EventArgs e) {
			textFrom.Text=DateTime.Now.AddYears(-1).ToShortDateString();
			textTo.Text=DateTime.Now.ToShortDateString();
		}

		private void butPatByZip_Click(object sender,EventArgs e) {
			if(!DateIsValid()) {
				return;
			}
			ReportSimpleGrid report=new ReportSimpleGrid();
			report.Query="SELECT SUBSTR(Zip,1,5) 'Zip Code',COUNT(*) 'Patients' "//Column headings "Zip Code" and "Patients" are provided by the USD 2010 Manual.
				+"FROM patient pat "
				+"JOIN procedurelog proc "
				+"ON pat.PatNum=proc.PatNum "
				+"WHERE "+DbHelper.Regexp("Zip","^[0-9]{5}")+" "//Starts with five numbers
				+"AND proc.ProcStatus="+POut.Int((int)ProcStat.C)+" "
				+"AND proc.DateEntryC >= "+POut.Date(DateFrom)+" "
				+"AND proc.DateEntryC <= "+POut.Date(DateTo)+" "
				+"GROUP BY Zip "
				+"HAVING COUNT(*) >= 10 "//Has more than 10 patients in that zip code for the given time frame.
				+"ORDER BY Zip";
			FormQuery FormQ=new FormQuery(report);
			FormQ.IsReport=true;
			FormQ.SubmitQuery();
      FormQ.textQuery.Text=report.Query;
			report.Title="Patients By ZIP CODE";
			report.SubTitle.Add("From "+DateFrom.ToShortDateString()+" to "+DateTo.ToShortDateString());
			report.Summary.Add("Other Zip Codes: "+Patients.GetZipOther(DateFrom,DateTo));
			report.Summary.Add("Unknown Residence: "+Patients.GetZipUnknown(DateFrom,DateTo));
			report.Summary.Add("TOTAL: "+Patients.GetPatCount(DateFrom,DateTo));
			FormQ.ShowDialog();
		}

		private void but3A_Click(object sender,EventArgs e) {

		}

		private bool DateIsValid() {
			DateFrom=PIn.Date(textFrom.Text);
			DateTo=PIn.Date(textTo.Text);
			if(DateFrom==DateTime.MinValue || DateTo==DateTime.MinValue) {
				MsgBox.Show(this,"Please enter valid To and From dates.");
				return false;
			}
			return true;
		}
		
		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}







	}
}