using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormRepeatChargesUpdate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRepeatChargesUpdate()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRepeatChargesUpdate));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(393,238);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(393,197);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(43,28);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(426,146);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// FormRepeatChargesUpdate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(520,289);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRepeatChargesUpdate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update Repeating Charges";
			this.Load += new System.EventHandler(this.FormRepeatChargesUpdate_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRepeatChargesUpdate_Load(object sender, System.EventArgs e) {
		
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			RepeatCharge[] chargeList=RepeatCharges.Refresh(0);
			int countAdded=0;
			DateTime startDate;
			Procedure proc;
			for(int i=0;i<chargeList.Length;i++){
				if(chargeList[i].DateStart>DateTime.Today){//not started yet
					continue;
				}
				//if(chargeList[i].DateStop.Year>1880//not blank
				//	&& chargeList[i].DateStop<DateTime.Today)//but already ended
				//{
				//	continue;
				//}
				//get a list dates of all completed procedures with this Code and patNum
				ArrayList ALdates=RepeatCharges.GetDates(ProcedureCodes.GetCodeNum(chargeList[i].ProcCode),chargeList[i].PatNum);
				startDate=chargeList[i].DateStart;
				//This is the repeating date using the old methodology.  It is necessary for checking if the repeating procedure was already added using the old methodology.
				DateTime possibleDateOld=startDate;
				//This is a more accurate repeating date which will allow procedures to be added on the 28th and later.
				DateTime possibleDateNew=startDate;
				int countMonths=0;
				//start looping through possible dates, beginning with the start date of the repeating charge
				while(possibleDateNew<=DateTime.Today){
					if(possibleDateNew<DateTime.Today.AddMonths(-3)){
						possibleDateOld=possibleDateOld.AddMonths(1);
						countMonths++;
						possibleDateNew=startDate.AddMonths(countMonths);
						continue;//don't go back more than three months
					}
					//check to see if the possible date is present in the list
					if(ALdates.Contains(possibleDateNew)
						|| ALdates.Contains(possibleDateOld))
					{
						possibleDateOld=possibleDateOld.AddMonths(1);
						countMonths++;
						possibleDateNew=startDate.AddMonths(countMonths);
						continue;
					}
					if(chargeList[i].DateStop.Year>1880//not blank
						&& chargeList[i].DateStop < possibleDateNew)//but already ended
					{
						break;
					}
					//otherwise, insert a procedure to db
					proc=new Procedure();
					proc.CodeNum=ProcedureCodes.GetCodeNum(chargeList[i].ProcCode);
					proc.DateEntryC=DateTime.Today;
					proc.PatNum=chargeList[i].PatNum;
					proc.ProcDate=possibleDateNew;
					proc.DateTP=possibleDateNew;
					proc.ProcFee=chargeList[i].ChargeAmt;
					proc.ProcStatus=ProcStat.C;
					proc.ProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
					proc.MedicalCode=ProcedureCodes.GetProcCode(proc.CodeNum).MedicalCode;
					proc.BaseUnits = ProcedureCodes.GetProcCode(proc.CodeNum).BaseUnits;
					proc.DiagnosticCode=PrefC.GetString(PrefName.ICD9DefaultForNewProcs);
					Procedures.Insert(proc);//no recall synch needed because dental offices don't use this feature
					countAdded++;
					possibleDateOld=possibleDateOld.AddMonths(1);
					countMonths++;
					possibleDateNew=startDate.AddMonths(countMonths);
				}
			}
			MessageBox.Show(countAdded.ToString()+" "+Lan.g(this,"procedures added."));
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















