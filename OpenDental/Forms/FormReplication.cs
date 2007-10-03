using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

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
			this.folderBrowserMySQL=new System.Windows.Forms.FolderBrowserDialog();
			this.butSelectServers=new OpenDental.UI.Button();
			this.gridReplicationComputers=new OpenDental.UI.ODGrid();
			this.butOK=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(47,9);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(407,13);
			this.label1.TabIndex=3;
			this.label1.Text="Select the complete list of known replication servers to merge with from the list"+
					" below:";
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
			this.gridReplicationComputers.Location=new System.Drawing.Point(50,33);
			this.gridReplicationComputers.Name="gridReplicationComputers";
			this.gridReplicationComputers.ScrollValue=0;
			this.gridReplicationComputers.SelectionMode=OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridReplicationComputers.Size=new System.Drawing.Size(587,324);
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
			string command="SELECT DATABASE()";
			string currentDatabaseName=PIn.PString(General.GetTable(command).Rows[0][0].ToString());
			//Loop through each of the selected computers and restart the slave service to force replication to start, then wait until replication is completed.
			for(int i=0;i<gridReplicationComputers.SelectedIndices.Length;i++) {
				string compName=gridReplicationComputers.Rows[gridReplicationComputers.SelectedIndices[i]].Cells[0].Text;
				DataConnection dc=new DataConnection();
				try {
					try {
						dc.SetDb(compName,currentDatabaseName,"repl","od1234","","",DataConnection.DBtype);
					} catch(MySqlException ex) {
						if(ex.Number==1042) {//The error 1042 is issued when the connection could not be made. 
							throw ex;//Pass the exception along.
						}
						dc.cmd.Connection.Close();
					}
					//Connection is considered to be successfull at this point. Now restart the slave process to force replication.
					command="SLAVE STOP; START SLAVE; SHOW SLAVE STATUS;";
					DataTable slaveStatus=dc.GetTable(command);
					//Wait for the slave process to become active again.
					for(int j=0;j<40 && slaveStatus.Rows[0]["Slave_IO_Running"].ToString().ToLower()!="yes";j++){
						Thread.Sleep(1000);
						command="SHOW SLAVE STATUS";
						slaveStatus=dc.GetTable(command);
					}
					if(slaveStatus.Rows[0]["Slave_IO_Running"].ToString().ToLower()!="yes") {
						throw new Exception("Slave IO is not running on computer "+compName);
					}
					if(slaveStatus.Rows[0]["Slave_SQL_Running"].ToString().ToLower()!="yes") {
						throw new Exception("Slave SQL is not running on computer "+compName);
					}
					//Wait for replication to complete.
					while(slaveStatus.Rows[0]["Slave_IO_State"].ToString().ToLower()!="waiting for master to send event" || 
						slaveStatus.Rows[0]["Seconds_Behind_Master"].ToString()!="0") {
						slaveStatus=dc.GetTable(command);
					}
				} catch(Exception ex) {
					MessageBox.Show(Lan.g(this,"Error forcing replication on computer")+" "+compName+": "+ex.Message);
					return;//Cancel operation.
				}
			}
			MessageBox.Show(Lan.g(this,"Database merge completed successfully"));
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butSelectServers_Click(object sender,EventArgs e) {
			//A server is considered a replication server if it is selected in the server list, and it is possible to connect to mysql into a database by the current database name using the Open Dental replication user.
			string command="SELECT DATABASE()";
			string currentDatabaseName=PIn.PString(General.GetTable(command).Rows[0][0].ToString());
			gridReplicationComputers.SetSelected(false);//Un-select all computers to start.
			for(int i=0;i<gridReplicationComputers.Rows.Count;i++){
				try{
					string connectStr="Server="+gridReplicationComputers.Rows[i].Cells[0].Text
					+";Database="+currentDatabaseName
					+";Port=3306;Connect Timeout=1;User ID=repl;Password=od1234;CharSet=utf8";
					MySqlConnection con=new MySqlConnection(connectStr);
					con.Open();
					//Select the computer in the list if a successfull connection was made.
					gridReplicationComputers.SetSelected(i,true);
				}catch{
				}
			}			
		}		

	}
}





















