using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
	/// <summary>
	/// Options and tools for Open Dental customers using database replication. Should not be accessible if random primary keys is inactive.
	/// </summary>
	public class FormReplication : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butForceReplication;
		private TextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormReplication()
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
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormReplication));
			this.butCancel=new OpenDental.UI.Button();
			this.butOK=new OpenDental.UI.Button();
			this.butForceReplication=new OpenDental.UI.Button();
			this.textBox1=new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(457,116);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(457,75);
			this.butOK.Name="butOK";
			this.butOK.Size=new System.Drawing.Size(75,26);
			this.butOK.TabIndex=1;
			this.butOK.Text="&OK";
			this.butOK.Click+=new System.EventHandler(this.butOK_Click);
			// 
			// butForceReplication
			// 
			this.butForceReplication.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butForceReplication.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butForceReplication.Autosize=true;
			this.butForceReplication.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butForceReplication.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butForceReplication.CornerRadius=4F;
			this.butForceReplication.Location=new System.Drawing.Point(12,12);
			this.butForceReplication.Name="butForceReplication";
			this.butForceReplication.Size=new System.Drawing.Size(107,26);
			this.butForceReplication.TabIndex=2;
			this.butForceReplication.Text="Force Replication";
			this.butForceReplication.Click+=new System.EventHandler(this.butForceReplication_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor=System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle=System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location=new System.Drawing.Point(125,12);
			this.textBox1.Multiline=true;
			this.textBox1.Name="textBox1";
			this.textBox1.Size=new System.Drawing.Size(306,47);
			this.textBox1.TabIndex=3;
			this.textBox1.Text="Forces MySQL to begin data replication on the local computer. Only computers whic"+
					"h are visible to this computer and setup for MySQL replication will send SQL upd"+
					"ates to this machine.";
			// 
			// FormReplication
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(584,167);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butForceReplication);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormReplication";
			this.ShowInTaskbar=false;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Replication Options and Utilities";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butForceReplication_Click(object sender,EventArgs e) {
			//WE NEEED TO ADD IN HERE A CHECK THAT ALL COMPUTERS IN THE COMPUTER TABLE ARE VISIBLE BEFORE WE TRY TO MERGE DATABASES HERE.
			//Restart the MySQL replication slave on the local computer. This will cause the local machine to accept updates from computers it is currently connected to.
			string command="SLAVE STOP";
			General.NonQ(command);
			command="START SLAVE";
			General.NonQ(command);
		}

	}
}





















