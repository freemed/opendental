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
	public class FormProcTools : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butProcButtons;
		private Label label2;
		private OpenDental.UI.Button butAutocodes;
		private Label label1;
		private OpenDental.UI.Button butDelete;
		private Label label9;
		public bool Changed;

		///<summary></summary>
		public FormProcTools()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcTools));
			this.butClose = new OpenDental.UI.Button();
			this.butProcButtons = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.butAutocodes = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(539,197);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butProcButtons
			// 
			this.butProcButtons.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butProcButtons.Autosize = true;
			this.butProcButtons.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butProcButtons.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butProcButtons.CornerRadius = 4F;
			this.butProcButtons.Location = new System.Drawing.Point(24,115);
			this.butProcButtons.Name = "butProcButtons";
			this.butProcButtons.Size = new System.Drawing.Size(103,26);
			this.butProcButtons.TabIndex = 39;
			this.butProcButtons.Text = "Procedure Buttons";
			this.butProcButtons.Click += new System.EventHandler(this.butProcButtons_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(132,115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(501,36);
			this.label2.TabIndex = 38;
			this.label2.Text = "Deletes all current ProcButtons from the Chart module, and then adds the default " +
    "ProcButtons.  Procedure codes must have already been entered or they cannot be a" +
    "dded as a ProcButton.";
			// 
			// butAutocodes
			// 
			this.butAutocodes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAutocodes.Autosize = true;
			this.butAutocodes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAutocodes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAutocodes.CornerRadius = 4F;
			this.butAutocodes.Location = new System.Drawing.Point(24,68);
			this.butAutocodes.Name = "butAutocodes";
			this.butAutocodes.Size = new System.Drawing.Size(103,26);
			this.butAutocodes.TabIndex = 37;
			this.butAutocodes.Text = "Autocodes";
			this.butAutocodes.Click += new System.EventHandler(this.butAutocodes_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(132,68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(443,36);
			this.label1.TabIndex = 36;
			this.label1.Text = "Deletes all current autocodes and then adds the default autocodes.  Procedure cod" +
    "es must have already been entered or they cannot be added as an autocode.";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(24,21);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(103,26);
			this.butDelete.TabIndex = 35;
			this.butDelete.Text = "Delete Codes";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(132,21);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(443,36);
			this.label9.TabIndex = 34;
			this.label9.Text = "This deletes all unused codes that match the pattern D####.  This needs to be don" +
    "e after converting from another software in order to comply with copyright on co" +
    "des.";
			// 
			// FormProcTools
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(644,247);
			this.Controls.Add(this.butProcButtons);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAutocodes);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcTools";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Code Tools";
			this.Load += new System.EventHandler(this.FormProcTools_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormProcTools_Load(object sender,EventArgs e) {

		}

		private void butDelete_Click(object sender,EventArgs e) {
			int affectedRows=ProcedureCodes.DeleteUnusedCodes();
			MessageBox.Show(affectedRows.ToString()+Lan.g(this," codes deleted."));
			if(affectedRows>0) {
				Changed=true;
			}
		}

		private void butAutocodes_Click(object sender,EventArgs e) {
			AutoCodes.SetToDefault();
			DataValid.SetInvalid(InvalidTypes.AutoCodes);
			MessageBox.Show(Lan.g(this,"Done."));
		}

		private void butProcButtons_Click(object sender,EventArgs e) {
			ProcButtons.SetToDefault();
			DataValid.SetInvalid(InvalidTypes.ProcButtons | InvalidTypes.Defs);
			MessageBox.Show(Lan.g(this,"Done."));
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		


	}
}





















