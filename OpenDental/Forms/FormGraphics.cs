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
	public class FormGraphics : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private CheckBox checkHardwareAccel;
		private CheckBox checkSimpleChart;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormGraphics(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraphics));
			this.checkHardwareAccel = new System.Windows.Forms.CheckBox();
			this.checkSimpleChart = new System.Windows.Forms.CheckBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// checkHardwareAccel
			// 
			this.checkHardwareAccel.Location = new System.Drawing.Point(47,34);
			this.checkHardwareAccel.Name = "checkHardwareAccel";
			this.checkHardwareAccel.Size = new System.Drawing.Size(176,18);
			this.checkHardwareAccel.TabIndex = 2;
			this.checkHardwareAccel.Text = "Hardware Acceleration";
			this.checkHardwareAccel.UseVisualStyleBackColor = true;
			// 
			// checkSimpleChart
			// 
			this.checkSimpleChart.Location = new System.Drawing.Point(47,62);
			this.checkSimpleChart.Name = "checkSimpleChart";
			this.checkSimpleChart.Size = new System.Drawing.Size(176,18);
			this.checkSimpleChart.TabIndex = 3;
			this.checkSimpleChart.Text = "Use simple tooth chart";
			this.checkSimpleChart.UseVisualStyleBackColor = true;
			this.checkSimpleChart.CheckedChanged += new System.EventHandler(this.checkSimpleChart_CheckedChanged);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(133,124);
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
			this.butCancel.Location = new System.Drawing.Point(235,124);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormGraphics
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(347,175);
			this.Controls.Add(this.checkSimpleChart);
			this.Controls.Add(this.checkHardwareAccel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormGraphics";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Graphics Preferences";
			this.Load += new System.EventHandler(this.FormGraphics_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormGraphics_Load(object sender,EventArgs e) {
			ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
			checkHardwareAccel.Checked=computerPrefs.GraphicsUseHardware;
			checkSimpleChart.Checked=computerPrefs.GraphicsSimple;//Must be after checkHardwareAccel is set. Sets initial visibility.
		}

		private void checkSimpleChart_CheckedChanged(object sender,EventArgs e) {
			if(Environment.OSVersion.Platform==PlatformID.Unix){//Only allow simple mode on Unix systems.
				checkSimpleChart.Checked=true;
				checkSimpleChart.Enabled=false;
			}
			if(checkSimpleChart.Checked) {
				checkHardwareAccel.Checked=false;
				checkHardwareAccel.Enabled=false;
			}else{
				checkHardwareAccel.Enabled=true;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			ComputerPref computerPrefs=ComputerPrefs.GetForLocalComputer();
			computerPrefs.GraphicsUseHardware=checkHardwareAccel.Checked;
			computerPrefs.GraphicsSimple=checkSimpleChart.Checked;
			ComputerPrefs.Update(computerPrefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















