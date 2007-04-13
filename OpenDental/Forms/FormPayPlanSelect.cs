using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>Lets the user choose which payment plan to attach a payment to if there are more than one available.</summary>
	public class FormPayPlanSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>A list of plans passed to this form which are to be displayed.</summary>
		private PayPlan[] ValidPlans;
		/// <summary>A list of payPlanCharges passed to this form used to calculate princ for each payplan.</summary>
		private PayPlanCharge[] ChargeList;
		private System.Windows.Forms.ListBox listPayPlans;
		/// <summary>The index of the plan selected.</summary>
		public int IndexSelected;

		///<summary></summary>
		public FormPayPlanSelect(PayPlan[] validPlans,PayPlanCharge[] chargeList)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			ValidPlans=validPlans;
			ChargeList=chargeList;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayPlanSelect));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listPayPlans = new System.Windows.Forms.ListBox();
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(256,185);
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
			this.butOK.Location = new System.Drawing.Point(160,185);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listPayPlans
			// 
			this.listPayPlans.Location = new System.Drawing.Point(32,51);
			this.listPayPlans.Name = "listPayPlans";
			this.listPayPlans.Size = new System.Drawing.Size(300,95);
			this.listPayPlans.TabIndex = 2;
			this.listPayPlans.DoubleClick += new System.EventHandler(this.listPayPlans_DoubleClick);
			// 
			// FormPayPlanSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(356,232);
			this.Controls.Add(this.listPayPlans);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPayPlanSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Payment Plan";
			this.Load += new System.EventHandler(this.FormPayPlanSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormPayPlanSelect_Load(object sender, System.EventArgs e) {
			for(int i=0;i<ValidPlans.Length;i++){
				listPayPlans.Items.Add(ValidPlans[i].PayPlanDate.ToShortDateString()
					+"  "+PayPlans.GetTotalPrinc(ValidPlans[i].PayPlanNum,ChargeList).ToString("F")
					+"  "+Patients.GetPat(ValidPlans[i].PatNum).GetNameFL());
			}
		}

		private void listPayPlans_DoubleClick(object sender, System.EventArgs e) {
			if(listPayPlans.SelectedIndex==-1){
				return;
			}
			IndexSelected=listPayPlans.SelectedIndex;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listPayPlans.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a payment plan first."));
				return;
			}
			IndexSelected=listPayPlans.SelectedIndex;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	


	}
}




































