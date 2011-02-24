using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormChartViewDateFilter:Form {
		///<summary>Set this date before opening the form.  Also after OK, this date is available to the calling class.</summary>
		public DateTime DateStart;
		///<summary>Set this date before opening the form.  Also after OK, this date is available to the calling class.</summary>
		public DateTime DateEnd;

		public FormChartViewDateFilter() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormChartViewDateFilter_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(ChartViewDates)).Length;i++) {
				listPresetDateRanges.Items.Add(Enum.GetNames(typeof(ChartViewDates))[i]);
			}
			textDateStart.Text=(DateStart==null?"":DateStart.ToString("MM/d/yy"));
			textDateStop.Text=(DateEnd==null?"":DateEnd.ToString("MM/d/yy"));
		}

		private void listPresetDateRanges_MouseClick(object sender,MouseEventArgs e) {
			int selectedI=listPresetDateRanges.IndexFromPoint(e.Location);
			switch((ChartViewDates)selectedI) {
				case ChartViewDates.All:
					DateStart=DateTime.MinValue;
					DateEnd=DateTime.MinValue;//interpreted as empty.  We want to show all future dates.
					break;
				case ChartViewDates.Today:
					DateStart=DateTime.Today;
					DateEnd=DateTime.Today;
					break;
				case ChartViewDates.Yesterday:
					DateStart=DateTime.Today.AddDays(-1);
					DateEnd=DateTime.Today.AddDays(-1);
					break;
				case ChartViewDates.ThisYear:
					DateStart=new DateTime(DateTime.Today.Year,1,1);
					DateEnd=new DateTime(DateTime.Today.Year,12,31);
					break;
				case ChartViewDates.LastYear:
					DateStart=new DateTime(DateTime.Today.Year-1,1,1);
					DateEnd=new DateTime(DateTime.Today.Year-1,12,31);
					break;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				) 
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}



			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	
	}
}