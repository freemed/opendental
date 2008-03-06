using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental.Reporting.Allocators.MyAllocator1
{
	public partial class FormInstallAllocator_Provider : Form
	{
		public FormInstallAllocator_Provider()
		{
			InitializeComponent();
		}

		

		private void butGuarantorDetailReport_Click(object sender, EventArgs e)
		{
			FormReport_GuarantorAllocationCheck fgac = new FormReport_GuarantorAllocationCheck();
			fgac.ShowDialog();
		}
		private void FormInstallAllocator_Provider_Load(object sender, EventArgs e)
		{
			this.richTextBox1.Text = "";
		}

		private void butProviderIncomeReport_Click(object sender, EventArgs e)
		{
			1.ToString();
			//DateTime dtFrom = new DateTime(2007,1,1);
			DateTime dtFrom = new DateTime(2007, 12, 31);
			DateTime dtTo = new DateTime(2007, 12, 31);
			//DateTime dtNow = DateTime.Now;
			//DateTime dtTo	 = new DateTime(dtNow.Year,dtNow.Month,dtNow.Day);
			FormReport_ProviderIncomeReport fpir = new FormReport_ProviderIncomeReport(dtFrom, dtTo);
			fpir.ShowDialog();
		}

		private void butUneardedIncomeReport_Click(object sender, EventArgs e)
		{

		}

		private void butRunAllocatorTool_Click(object sender, EventArgs e)
		{

			if (MessageBox.Show("Do you want to run the batch allocation?", "Please Respond", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Reporting.Allocators.MyAllocator1_ProviderPayment allocator1 = new OpenDental.Reporting.Allocators.MyAllocator1_ProviderPayment();
				allocator1.StartBatchAllocation();
			}
		}

		private void rbutIHaveRead_CheckedChanged(object sender, EventArgs e)
		{
			this.butRunAllocatorTool.Enabled = this.rbutIHaveRead.Checked;
		}

		
	}
}