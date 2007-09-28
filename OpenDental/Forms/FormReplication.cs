using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Options and tools for Open Dental customers using database replication. Should not be accessible if random primary keys is inactive.
	/// </summary>
	public class FormReplication : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid odGrid1;
		private Label label1;
		private OpenDental.UI.Button butSelectAll;
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
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.odGrid1=new OpenDental.UI.ODGrid();
			this.label1=new System.Windows.Forms.Label();
			this.butSelectAll=new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(562,387);
			this.butOK.Name="butOK";
			this.butOK.Size=new System.Drawing.Size(75,26);
			this.butOK.TabIndex=1;
			this.butOK.Text="&OK";
			this.butOK.Click+=new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(562,428);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// odGrid1
			// 
			this.odGrid1.HScrollVisible=false;
			this.odGrid1.Location=new System.Drawing.Point(50,40);
			this.odGrid1.Name="odGrid1";
			this.odGrid1.ScrollValue=0;
			this.odGrid1.Size=new System.Drawing.Size(587,317);
			this.odGrid1.TabIndex=2;
			this.odGrid1.Title=null;
			this.odGrid1.TranslationName=null;
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(47,24);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(407,13);
			this.label1.TabIndex=3;
			this.label1.Text="Select the complete list of known replication servers to merge with from the list"+
					" below:";
			// 
			// butSelectAll
			// 
			this.butSelectAll.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butSelectAll.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butSelectAll.Autosize=true;
			this.butSelectAll.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectAll.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectAll.CornerRadius=4F;
			this.butSelectAll.Location=new System.Drawing.Point(50,363);
			this.butSelectAll.Name="butSelectAll";
			this.butSelectAll.Size=new System.Drawing.Size(75,26);
			this.butSelectAll.TabIndex=4;
			this.butSelectAll.Text="Select All";
			// 
			// FormReplication
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(689,479);
			this.Controls.Add(this.butSelectAll);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.odGrid1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormReplication";
			this.ShowInTaskbar=false;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Merge Replicating Databases";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		private void butOK_Click(object sender, System.EventArgs e) {
			//This tool can only be used for MySQL.
			if(FormChooseDatabase.DBtype!=DatabaseType.MySql){
				MessageBox.Show(Lan.g(this,"This tool can only be used on MySQL databases."),"");
			}

			//A server is considered a replication server if it is in the current list of computers for Open Dental, and it is possible to connect to mysql into a database by the current database name (use mysql function) using the Open Dental replication user.

			//







			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

/*		private void butForceReplication_Click(object sender,EventArgs e) {
			//WE NEEED TO ADD IN HERE A CHECK THAT ALL COMPUTERS IN THE COMPUTER TABLE ARE VISIBLE BEFORE WE TRY TO MERGE DATABASES HERE.
			//Restart the MySQL replication slave on the local computer. This will cause the local machine to accept updates from computers it is currently connected to.
			string command="SLAVE STOP";
			General.NonQ(command);
			command="START SLAVE";
			General.NonQ(command);
		}*/

	}
}





















