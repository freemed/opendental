using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTrojanCollectSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label1;
		private TextBox textExportFolder;
		private Label label2;
		private ComboBox comboBillType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormTrojanCollectSetup()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrojanCollectSetup));
			this.label1 = new System.Windows.Forms.Label();
			this.textExportFolder = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBillType = new System.Windows.Forms.ComboBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(477,16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Export Folder.  Should be a shared network folder, and must be the same for all c" +
    "omputers.";
			// 
			// textExportFolder
			// 
			this.textExportFolder.Location = new System.Drawing.Point(15,40);
			this.textExportFolder.Name = "textExportFolder";
			this.textExportFolder.Size = new System.Drawing.Size(406,20);
			this.textExportFolder.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12,91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(294,16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Billing type for patients sent to collections";
			// 
			// comboBillType
			// 
			this.comboBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBillType.FormattingEnabled = true;
			this.comboBillType.Location = new System.Drawing.Point(15,110);
			this.comboBillType.MaxDropDownItems = 50;
			this.comboBillType.Name = "comboBillType";
			this.comboBillType.Size = new System.Drawing.Size(198,21);
			this.comboBillType.TabIndex = 7;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(264,166);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(364,166);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormTrojanCollectSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(491,217);
			this.Controls.Add(this.comboBillType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textExportFolder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTrojanCollectSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Trojan Express Collect";
			this.Load += new System.EventHandler(this.FormTrojanCollectSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTrojanCollectSetup_Load(object sender,EventArgs e) {
			textExportFolder.Text=PrefB.GetString("TrojanExpressCollectPath");
			int billtype=PrefB.GetInt("TrojanExpressCollectBillingType");
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				comboBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
				if(DefB.Short[(int)DefCat.BillingTypes][i].DefNum==billtype){
					comboBillType.SelectedIndex=i;
				}
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			if(!Directory.Exists(textExportFolder.Text)){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Export folder does not exist.");
				return;
			}
			if(comboBillType.SelectedIndex==-1){
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Please select a billing type.");
				return;
			}
			int billtype=DefB.Short[(int)DefCat.BillingTypes][comboBillType.SelectedIndex].DefNum;
			if( Prefs.UpdateString("TrojanExpressCollectPath",textExportFolder.Text)
				| Prefs.UpdateInt   ("TrojanExpressCollectBillingType",billtype))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















