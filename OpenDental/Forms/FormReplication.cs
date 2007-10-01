using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Diagnostics;

namespace OpenDental{
	/// <summary>
	/// Options and tools for Open Dental customers using database replication. Should not be accessible if random primary keys is inactive.
	/// </summary>
	public class FormReplication : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridReplicationComputers;
		private Label label1;
		private OpenDental.UI.Button butSelectServers;
		private TextBox textMySQLPath;
		private OpenDental.UI.Button butBrowse;
		private TextBox textBox1;
		private FolderBrowserDialog folderBrowserMySQL;
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
			this.label1=new System.Windows.Forms.Label();
			this.textMySQLPath=new System.Windows.Forms.TextBox();
			this.textBox1=new System.Windows.Forms.TextBox();
			this.folderBrowserMySQL=new System.Windows.Forms.FolderBrowserDialog();
			this.butBrowse=new OpenDental.UI.Button();
			this.butSelectServers=new OpenDental.UI.Button();
			this.gridReplicationComputers=new OpenDental.UI.ODGrid();
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(47,63);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(407,13);
			this.label1.TabIndex=3;
			this.label1.Text="Select the complete list of known replication servers to merge with from the list"+
					" below:";
			// 
			// textMySQLPath
			// 
			this.textMySQLPath.Location=new System.Drawing.Point(50,38);
			this.textMySQLPath.Name="textMySQLPath";
			this.textMySQLPath.Size=new System.Drawing.Size(506,20);
			this.textMySQLPath.TabIndex=5;
			this.textMySQLPath.Text="C:\\Program Files\\MySQL\\MySQL Server 5.0\\bin\\";
			// 
			// textBox1
			// 
			this.textBox1.BackColor=System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle=System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location=new System.Drawing.Point(50,2);
			this.textBox1.Multiline=true;
			this.textBox1.Name="textBox1";
			this.textBox1.Size=new System.Drawing.Size(506,33);
			this.textBox1.TabIndex=8;
			this.textBox1.Text="Path to local \'mysql\' program. It might help to add this path to your Operating S"+
					"ystem path and just type mysql in the box below.";
			// 
			// butBrowse
			// 
			this.butBrowse.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butBrowse.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butBrowse.Autosize=true;
			this.butBrowse.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowse.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowse.CornerRadius=4F;
			this.butBrowse.Location=new System.Drawing.Point(562,38);
			this.butBrowse.Name="butBrowse";
			this.butBrowse.Size=new System.Drawing.Size(75,20);
			this.butBrowse.TabIndex=7;
			this.butBrowse.Text="Browse";
			this.butBrowse.Click+=new System.EventHandler(this.butBrowse_Click);
			// 
			// butSelectServers
			// 
			this.butSelectServers.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butSelectServers.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butSelectServers.Autosize=true;
			this.butSelectServers.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectServers.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectServers.CornerRadius=4F;
			this.butSelectServers.Location=new System.Drawing.Point(50,363);
			this.butSelectServers.Name="butSelectServers";
			this.butSelectServers.Size=new System.Drawing.Size(142,26);
			this.butSelectServers.TabIndex=4;
			this.butSelectServers.Text="Select Replication Servers";
			this.butSelectServers.Click+=new System.EventHandler(this.butSelectServers_Click);
			// 
			// gridReplicationComputers
			// 
			this.gridReplicationComputers.HScrollVisible=false;
			this.gridReplicationComputers.Location=new System.Drawing.Point(50,80);
			this.gridReplicationComputers.Name="gridReplicationComputers";
			this.gridReplicationComputers.ScrollValue=0;
			this.gridReplicationComputers.Size=new System.Drawing.Size(587,277);
			this.gridReplicationComputers.TabIndex=2;
			this.gridReplicationComputers.Title=null;
			this.gridReplicationComputers.TranslationName=null;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(562,365);
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
			this.butCancel.Location=new System.Drawing.Point(562,406);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=0;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// FormReplication
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(689,457);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.butBrowse);
			this.Controls.Add(this.textMySQLPath);
			this.Controls.Add(this.butSelectServers);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridReplicationComputers);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormReplication";
			this.ShowInTaskbar=false;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Merge Replicating Databases";
			this.Load+=new System.EventHandler(this.FormReplication_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormReplication_Load(object sender,EventArgs e) {
			if(FormChooseDatabase.DBtype!=DatabaseType.MySql) {
				MessageBox.Show(Lan.g(this,"This tool can only be used on MySQL databases."),"");
				Close();
			}
			if(MessageBox.Show(Lan.g(this,"This feature is for advanced users only. Please do not use this feature if you do not know what it is for. Hit OK if you would like to continue, or hit Cancel to exit."),"",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				Close();//exit the form.
			}
			gridReplicationComputers.BeginUpdate();
			gridReplicationComputers.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"ComputerName"),80);
			gridReplicationComputers.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,""),60);
			//gridReplicationComputers.Columns.Add(col);
			gridReplicationComputers.EndUpdate();
			FillServerGrid();
		}

		private void FillServerGrid() {
			Computer[] computers=Computers.GetList();
			gridReplicationComputers.BeginUpdate();
			gridReplicationComputers.Rows.Clear();
			for(int i=0;i<computers.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(computers[i].CompName);
				//row.Cells.Add();
				gridReplicationComputers.Rows.Add(row);
			}
			gridReplicationComputers.EndUpdate();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			
			
			







			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(folderBrowserMySQL.ShowDialog()==DialogResult.OK){
				textMySQLPath.Text=folderBrowserMySQL.SelectedPath;
			}
		}

		private void butSelectServers_Click(object sender,EventArgs e) {
			//A server is considered a replication server if it is selected in the server list, and it is possible to connect to mysql into a database by the current database name using the Open Dental replication user.
			string command="SELECT DATABASE()";
			string currentDatabaseName=PIn.PString(General.GetTable(command).Rows[0][0].ToString());
			gridReplicationComputers.SetSelected(false);//Un-select all computers to start.
			for(int i=0;i<gridReplicationComputers.Rows.Count;i++){
				string mysqlArgs=" -h "+gridReplicationComputers.Rows[i].Cells[0].Text
					+" -u repl --password=od1234 -b -D "+currentDatabaseName+" -e \"exit\"";
				ProcessStartInfo psi=new ProcessStartInfo(textMySQLPath.Text+"mysql",mysqlArgs);
				psi.CreateNoWindow=true;
				psi.WindowStyle=ProcessWindowStyle.Hidden;
				Process othermysql=Process.Start(psi);
				try{
					othermysql.WaitForExit(5000);
					if(othermysql.ExitCode==0) {//The connection was a success.
						gridReplicationComputers.SetSelected(i,true);
					}
				}catch{
					//The connection failed, so even when waiting for 5 seconds, the mysql process does not exit, and the call to othermysql.ExitCode throws and exception because the process had not exited.
				}
			}			
		}		

	}
}





















