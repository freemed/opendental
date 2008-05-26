using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormAutoCodeLessIntrusive : System.Windows.Forms.Form{
		private System.Windows.Forms.CheckBox checkLessIntrusive;
		private System.Windows.Forms.Label labelMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butNo;
		private OpenDental.UI.Button butYes;
		///<summary>The text to display in this dialog</summary>
		public string mainText;
		///<summary>The user checked the box.</summary>
		public bool CheckedBox;

		///<summary></summary>
		public FormAutoCodeLessIntrusive()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this,new Control[] {labelMain});
			//labelMain is translated from calling Form (FormProcEdit)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoCodeLessIntrusive));
			this.butNo = new OpenDental.UI.Button();
			this.butYes = new OpenDental.UI.Button();
			this.checkLessIntrusive = new System.Windows.Forms.CheckBox();
			this.labelMain = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butNo
			// 
			this.butNo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNo.Autosize = true;
			this.butNo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNo.CornerRadius = 4F;
			this.butNo.Location = new System.Drawing.Point(406,169);
			this.butNo.Name = "butNo";
			this.butNo.Size = new System.Drawing.Size(75,26);
			this.butNo.TabIndex = 0;
			this.butNo.Text = "&No";
			this.butNo.Click += new System.EventHandler(this.butNo_Click);
			// 
			// butYes
			// 
			this.butYes.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butYes.Autosize = true;
			this.butYes.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butYes.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butYes.CornerRadius = 4F;
			this.butYes.Location = new System.Drawing.Point(406,128);
			this.butYes.Name = "butYes";
			this.butYes.Size = new System.Drawing.Size(75,26);
			this.butYes.TabIndex = 1;
			this.butYes.Text = "&Yes";
			this.butYes.Click += new System.EventHandler(this.butYes_Click);
			// 
			// checkLessIntrusive
			// 
			this.checkLessIntrusive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkLessIntrusive.Location = new System.Drawing.Point(34,155);
			this.checkLessIntrusive.Name = "checkLessIntrusive";
			this.checkLessIntrusive.Size = new System.Drawing.Size(304,44);
			this.checkLessIntrusive.TabIndex = 2;
			this.checkLessIntrusive.Text = "Check this box if you never again want to be prompted to change this type of proc" +
    "edure.";
			// 
			// labelMain
			// 
			this.labelMain.Location = new System.Drawing.Point(35,32);
			this.labelMain.Name = "labelMain";
			this.labelMain.Size = new System.Drawing.Size(438,73);
			this.labelMain.TabIndex = 3;
			this.labelMain.Text = "labelMain";
			this.labelMain.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// FormAutoCodeLessIntrusive
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(511,211);
			this.Controls.Add(this.labelMain);
			this.Controls.Add(this.checkLessIntrusive);
			this.Controls.Add(this.butYes);
			this.Controls.Add(this.butNo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoCodeLessIntrusive";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Change Code?";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormAutoCodeLessIntrusive_Closing);
			this.Load += new System.EventHandler(this.FormAutoCodeLessIntrusive_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAutoCodeLessIntrusive_Load(object sender, System.EventArgs e) {
			labelMain.Text=mainText;
		}

		private void butYes_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butNo_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormAutoCodeLessIntrusive_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(checkLessIntrusive.Checked){
				CheckedBox=true;
			}
		}

		


	}
}





















