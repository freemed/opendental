using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>For a given subscriber, this list all their plans.  User then selects one plan from the list or creates a blank plan.</summary>
	public class FormInsSelectSubscr : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listPlans;
		private OpenDental.UI.Button butNew;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int Subscriber;
		private InsPlan[] PlanList;
		///<summary>When dialogResult=OK, this will contain the PlanNum of the selected plan.  If this is 0, then user has selected the 'New' option.</summary>
		public int SelectedPlanNum;

		///<summary></summary>
		public FormInsSelectSubscr(int subscriber)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Subscriber=subscriber;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsSelectSubscr));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listPlans = new System.Windows.Forms.ListBox();
			this.butNew = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(317,211);
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
			this.butOK.Location = new System.Drawing.Point(226,211);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listPlans
			// 
			this.listPlans.Location = new System.Drawing.Point(24,21);
			this.listPlans.Name = "listPlans";
			this.listPlans.Size = new System.Drawing.Size(271,134);
			this.listPlans.TabIndex = 2;
			this.listPlans.DoubleClick += new System.EventHandler(this.listPlans_DoubleClick);
			// 
			// butNew
			// 
			this.butNew.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butNew.Autosize = true;
			this.butNew.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNew.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNew.CornerRadius = 4F;
			this.butNew.Location = new System.Drawing.Point(26,211);
			this.butNew.Name = "butNew";
			this.butNew.Size = new System.Drawing.Size(85,26);
			this.butNew.TabIndex = 3;
			this.butNew.Text = "New Plan";
			this.butNew.Click += new System.EventHandler(this.butNew_Click);
			// 
			// FormInsSelectSubscr
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(420,263);
			this.Controls.Add(this.butNew);
			this.Controls.Add(this.listPlans);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsSelectSubscr";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Insurance Plan";
			this.Load += new System.EventHandler(this.FormInsSelectSubscr_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsSelectSubscr_Load(object sender, System.EventArgs e) {
			PlanList=InsPlans.GetListForSubscriber(Subscriber);
			for(int i=0;i<PlanList.Length;i++){
				listPlans.Items.Add(InsPlans.GetCarrierName(PlanList[i].PlanNum,PlanList));
			}
		}

		private void listPlans_DoubleClick(object sender, System.EventArgs e) {
			if(listPlans.SelectedIndex==-1){
				return;
			}
			SelectedPlanNum=PlanList[listPlans.SelectedIndex].PlanNum;
			DialogResult=DialogResult.OK;
		}

		private void butNew_Click(object sender, System.EventArgs e) {
			SelectedPlanNum=0;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listPlans.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a plan first.");
				return;
			}
			SelectedPlanNum=PlanList[listPlans.SelectedIndex].PlanNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















