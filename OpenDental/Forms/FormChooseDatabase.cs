using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using OpenDentBusiness;
using CodeBase;
using OpenDental.DataAccess;

namespace OpenDental{
	///<summary></summary>
	public class FormChooseDatabase : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUser;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupDirect;
		private System.ComponentModel.Container components = null;
		//private string mysqlInstalled;
		private System.Windows.Forms.ComboBox comboComputerName;
		private System.Windows.Forms.ComboBox comboDatabase;
		//private bool mysqlIsStarted;
		private CheckBox checkNoShow;
		private GroupBox groupServer;
		private ComboBox comboServerName2;
		private Label label9;
		private Label label6;
		private ComboBox comboDatabase2;
		private Label label12;
		private TextBox textUser2;
		private TextBox textPassword2;
		private Label label10;
		private Label label11;
		private CheckBox checkConnectServer;
		private Label label5;
		private ValidNumber textPort;
		//<summary></summary>
		public bool NoShow;
		private Label label7;
		private ListBox listType;
		private Label label8;
		private TextBox textConnectionString;
		public static DatabaseType DBtype;

		///<summary></summary>
		public FormChooseDatabase(){
			InitializeComponent();
			//textPort.MaxVal=System.Int32.MaxValue;
			Lan.F(this);
			//Lan.C(this, new System.Windows.Forms.Control[] {
			//	textNotInstalled
			//});
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChooseDatabase));
			this.label1 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textUser = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupDirect = new System.Windows.Forms.GroupBox();
			this.comboDatabase = new System.Windows.Forms.ComboBox();
			this.checkNoShow = new System.Windows.Forms.CheckBox();
			this.comboComputerName = new System.Windows.Forms.ComboBox();
			this.groupServer = new System.Windows.Forms.GroupBox();
			this.textPort = new OpenDental.ValidNumber();
			this.label5 = new System.Windows.Forms.Label();
			this.comboDatabase2 = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textUser2 = new System.Windows.Forms.TextBox();
			this.textPassword2 = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.comboServerName2 = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.checkConnectServer = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textConnectionString = new System.Windows.Forms.TextBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.groupDirect.SuspendLayout();
			this.groupServer.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(582,38);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name: The name of the computer where the MySQL server and database are loc" +
    "ated.  If you are running this program on a single computer only, then the compu" +
    "ter name may be localhost.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(21,181);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(280,20);
			this.textPassword.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19,162);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(509,18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Password: For new installations, the password will be blank.  You probably don\'t " +
    "need to change this.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(21,140);
			this.textUser.Name = "textUser";
			this.textUser.Size = new System.Drawing.Size(280,20);
			this.textUser.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(19,121);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(480,18);
			this.label3.TabIndex = 4;
			this.label3.Text = "User: When MySQL is first installed, the user is root.  You probably don\'t need t" +
    "o change this.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(19,79);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(582,18);
			this.label4.TabIndex = 6;
			this.label4.Text = "DataBase: usually opendental unless you changed the name.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupDirect
			// 
			this.groupDirect.Controls.Add(this.textUser);
			this.groupDirect.Controls.Add(this.comboDatabase);
			this.groupDirect.Controls.Add(this.checkNoShow);
			this.groupDirect.Controls.Add(this.comboComputerName);
			this.groupDirect.Controls.Add(this.label1);
			this.groupDirect.Controls.Add(this.textPassword);
			this.groupDirect.Controls.Add(this.label2);
			this.groupDirect.Controls.Add(this.label3);
			this.groupDirect.Controls.Add(this.label4);
			this.groupDirect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupDirect.Location = new System.Drawing.Point(16,16);
			this.groupDirect.Name = "groupDirect";
			this.groupDirect.Size = new System.Drawing.Size(660,244);
			this.groupDirect.TabIndex = 0;
			this.groupDirect.TabStop = false;
			this.groupDirect.Text = "Connection Settings - These values will only be used on this computer.  They have" +
    " to be set on each computer";
			// 
			// comboDatabase
			// 
			this.comboDatabase.Location = new System.Drawing.Point(21,98);
			this.comboDatabase.MaxDropDownItems = 100;
			this.comboDatabase.Name = "comboDatabase";
			this.comboDatabase.Size = new System.Drawing.Size(280,21);
			this.comboDatabase.TabIndex = 9;
			// 
			// checkNoShow
			// 
			this.checkNoShow.AutoSize = true;
			this.checkNoShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoShow.Location = new System.Drawing.Point(21,211);
			this.checkNoShow.Name = "checkNoShow";
			this.checkNoShow.Size = new System.Drawing.Size(294,18);
			this.checkNoShow.TabIndex = 2;
			this.checkNoShow.Text = "Do not show this window on startup (this computer only)";
			this.checkNoShow.UseVisualStyleBackColor = true;
			// 
			// comboComputerName
			// 
			this.comboComputerName.Location = new System.Drawing.Point(21,56);
			this.comboComputerName.MaxDropDownItems = 100;
			this.comboComputerName.Name = "comboComputerName";
			this.comboComputerName.Size = new System.Drawing.Size(280,21);
			this.comboComputerName.TabIndex = 8;
			this.comboComputerName.Leave += new System.EventHandler(this.comboComputerName_Leave);
			this.comboComputerName.SelectionChangeCommitted += new System.EventHandler(this.comboComputerName_SelectionChangeCommitted);
			// 
			// groupServer
			// 
			this.groupServer.Controls.Add(this.textPort);
			this.groupServer.Controls.Add(this.label5);
			this.groupServer.Controls.Add(this.comboDatabase2);
			this.groupServer.Controls.Add(this.label12);
			this.groupServer.Controls.Add(this.textUser2);
			this.groupServer.Controls.Add(this.textPassword2);
			this.groupServer.Controls.Add(this.label10);
			this.groupServer.Controls.Add(this.label11);
			this.groupServer.Controls.Add(this.comboServerName2);
			this.groupServer.Controls.Add(this.label9);
			this.groupServer.Controls.Add(this.label6);
			this.groupServer.Location = new System.Drawing.Point(16,305);
			this.groupServer.Name = "groupServer";
			this.groupServer.Size = new System.Drawing.Size(376,273);
			this.groupServer.TabIndex = 2;
			this.groupServer.TabStop = false;
			this.groupServer.Text = "Connect to Server Component - Only for advanced users";
			// 
			// textPort
			// 
			this.textPort.Location = new System.Drawing.Point(22,112);
			this.textPort.MaxVal = 999999;
			this.textPort.MinVal = 0;
			this.textPort.Name = "textPort";
			this.textPort.Size = new System.Drawing.Size(100,20);
			this.textPort.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(20,93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(308,18);
			this.label5.TabIndex = 18;
			this.label5.Text = "Port";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboDatabase2
			// 
			this.comboDatabase2.Location = new System.Drawing.Point(22,153);
			this.comboDatabase2.MaxDropDownItems = 100;
			this.comboDatabase2.Name = "comboDatabase2";
			this.comboDatabase2.Size = new System.Drawing.Size(280,21);
			this.comboDatabase2.TabIndex = 1;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(19,133);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(296,18);
			this.label12.TabIndex = 15;
			this.label12.Text = "DataBase";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textUser2
			// 
			this.textUser2.Location = new System.Drawing.Point(22,195);
			this.textUser2.Name = "textUser2";
			this.textUser2.Size = new System.Drawing.Size(280,20);
			this.textUser2.TabIndex = 2;
			// 
			// textPassword2
			// 
			this.textPassword2.Location = new System.Drawing.Point(22,236);
			this.textPassword2.Name = "textPassword2";
			this.textPassword2.PasswordChar = '*';
			this.textPassword2.Size = new System.Drawing.Size(280,20);
			this.textPassword2.TabIndex = 3;
			this.textPassword2.UseSystemPasswordChar = true;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(20,217);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(281,18);
			this.label10.TabIndex = 11;
			this.label10.Text = "Password";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(20,176);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(295,18);
			this.label11.TabIndex = 14;
			this.label11.Text = "Open Dental User (not MySQL user)";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboServerName2
			// 
			this.comboServerName2.Location = new System.Drawing.Point(22,70);
			this.comboServerName2.MaxDropDownItems = 100;
			this.comboServerName2.Name = "comboServerName2";
			this.comboServerName2.Size = new System.Drawing.Size(280,21);
			this.comboServerName2.TabIndex = 10;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(20,50);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(310,18);
			this.label9.TabIndex = 9;
			this.label9.Text = "Server Name";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(19,23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(311,32);
			this.label6.TabIndex = 0;
			this.label6.Text = "Read the manual to learn how to install the server component.";
			// 
			// checkConnectServer
			// 
			this.checkConnectServer.AutoSize = true;
			this.checkConnectServer.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkConnectServer.Location = new System.Drawing.Point(16,277);
			this.checkConnectServer.Name = "checkConnectServer";
			this.checkConnectServer.Size = new System.Drawing.Size(212,18);
			this.checkConnectServer.TabIndex = 1;
			this.checkConnectServer.Text = "Connect to Server Component instead";
			this.checkConnectServer.UseVisualStyleBackColor = true;
			this.checkConnectServer.Click += new System.EventHandler(this.checkConnectServer_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(411,289);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(223,18);
			this.label7.TabIndex = 19;
			this.label7.Text = "Database Type";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listType
			// 
			this.listType.FormattingEnabled = true;
			this.listType.Location = new System.Drawing.Point(414,311);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(99,30);
			this.listType.TabIndex = 20;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(412,357);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(165,13);
			this.label8.TabIndex = 21;
			this.label8.Text = "Advanced: Use connection string";
			// 
			// textConnectionString
			// 
			this.textConnectionString.Location = new System.Drawing.Point(414,375);
			this.textConnectionString.Multiline = true;
			this.textConnectionString.Name = "textConnectionString";
			this.textConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textConnectionString.Size = new System.Drawing.Size(262,202);
			this.textConnectionString.TabIndex = 22;
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
			this.butCancel.Location = new System.Drawing.Point(602,632);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 6;
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
			this.butOK.Location = new System.Drawing.Point(509,632);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormChooseDatabase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(733,672);
			this.Controls.Add(this.textConnectionString);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.checkConnectServer);
			this.Controls.Add(this.groupServer);
			this.Controls.Add(this.groupDirect);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChooseDatabase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Choose Database";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormConfig_Closing);
			this.Load += new System.EventHandler(this.FormConfig_Load);
			this.groupDirect.ResumeLayout(false);
			this.groupDirect.PerformLayout();
			this.groupServer.ResumeLayout(false);
			this.groupServer.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormConfig_Load(object sender, System.EventArgs e) {
			listType.Items.Add("MySql");
			listType.Items.Add("Oracle");
			listType.SelectedIndex=0;
			GetConfig();
			FillCombosComputerNames();
			FillComboDatabases();
		}

		///<summary>Gets a list of all computer names on the network (this is not easy)</summary>
		private string[] GetComputerNames(){
			if(Environment.OSVersion.Platform==PlatformID.Unix){
				return new string[0];
			}
			try{
				ArrayList retList=new ArrayList();
				//string myAdd=Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();//obsolete
				string myAdd=Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
				ProcessStartInfo psi=new ProcessStartInfo();
				psi.FileName=@"C:\WINDOWS\system32\cmd.exe";//Path for the cmd prompt
				psi.Arguments="/c net view > tempCompNames.txt";//Arguments for the command prompt
				//"/c" tells it to run the following command which is "net view > tempCompNames.txt"
				//"net view" lists all the computers on the network
				//" > tempCompNames.txt" tells dos to put the results in a file called tempCompNames.txt
				psi.WindowStyle=ProcessWindowStyle.Hidden;//Hide the window
				Process.Start(psi);
				StreamReader sr=null;
				string filename=ODFileUtils.CombinePaths(Application.StartupPath,"tempCompNames.txt");
				Thread.Sleep(200);//sleep for 1/5 second
				if(!File.Exists(filename)) {
					return new string[0];
				}
				try {
					sr=new StreamReader(filename);
				}
				catch {
				}
				while(!sr.ReadLine().StartsWith("--")){
					//The line just before the data looks like: --------------------------
				}
				string line="";
				retList.Add("localhost");
				while(true){
					line=sr.ReadLine();
					if(line.StartsWith("The"))//cycle until we reach,"The command completed successfully."
						break;
					line=line.Split(char.Parse(" "))[0];// Split the line after the first space
					// Normally, in the file it lists it like this
					// \\MyComputer                 My Computer's Description
					// Take off the slashes, "\\MyComputer" to "MyComputer"
					retList.Add(line.Substring(2,line.Length-2));
				}
				sr.Close();
				File.Delete(ODFileUtils.CombinePaths(Application.StartupPath,"tempCompNames.txt"));
				string[] retArray=new string[retList.Count];
				retList.CopyTo(retArray);
				return retArray;
			}
			catch{//it will always fail if not WinXP
				return new string[0];
			}
		}

		///<summary></summary>
		private string[] GetDatabases(){
			if(comboComputerName.Text==""){
				return new string[0];
			}
			if(listType.SelectedIndex!=0){
				return new string[0];//because SHOW DATABASES won't work
			}
			try{
				OpenDentBusiness.DataConnection dcon;
				//use the one table that we know exists
				if(textUser.Text==""){
					dcon=new OpenDentBusiness.DataConnection(comboComputerName.Text,"mysql","root",textPassword.Text,
						DatabaseType.MySql);
				}
				else{
					dcon=new OpenDentBusiness.DataConnection(comboComputerName.Text,"mysql",textUser.Text,textPassword.Text,
						DatabaseType.MySql);
				}
				string command="SHOW DATABASES";
				//if this next step fails, table will simply have 0 rows
				DataTable table=dcon.GetTable(command);
				string[] dbNames=new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					dbNames[i]=table.Rows[i][0].ToString();
				}
				return dbNames;
			}
			catch{
				return new string[0];
			}
		}

		///<summary>Only runs on startup.</summary>
		private void FillCombosComputerNames(){
			string[] computerNames=GetComputerNames();
			comboComputerName.Items.Clear();
			comboComputerName.Items.AddRange(computerNames);
			comboServerName2.Items.Clear();
			comboServerName2.Items.AddRange(computerNames);
		}

		private void FillComboDatabases(){
			comboDatabase.Items.Clear();
			comboDatabase.Items.AddRange(GetDatabases());
		}

		private void comboComputerName_SelectionChangeCommitted(object sender, System.EventArgs e) {
			FillComboDatabases();
		}

		private void comboComputerName_Leave(object sender, System.EventArgs e) {
			FillComboDatabases();
		}

		private void checkConnectServer_Click(object sender,EventArgs e) {
			if(checkConnectServer.Checked){
				groupServer.Enabled=true;
				groupDirect.Enabled=false;
			}
			else{
				groupServer.Enabled=false;
				groupDirect.Enabled=true;
			}
		}
		
		///<summary>Gets the settings from the config file, and fills the form with those values.  Gets run twice at startup.</summary>
		public void GetConfig(){
			//Improvement should be made here to avoid requiring admin priv.
			//Search path should be something like this:
			//1. /home/username/.opendental/config.xml (or corresponding user path in Windows)
			//2. /etc/opendental/config.xml (or corresponding machine path in Windows) (should be default for new installs) 
			//3. Application Directory/FreeDentalConfig.xml (read-only if user is not admin)
			if(!File.Exists("FreeDentalConfig.xml")){
				FileStream fs=File.Create("FreeDentalConfig.xml");
				fs.Close();
				comboComputerName.Text="localhost";
				#if(TRIALONLY)
					comboDatabase.Text="demo";
				#else
					comboDatabase.Text="opendental";
				#endif
				textUser.Text="root";
				if(listType.Items.Count>0) {//not true on startup
					listType.SelectedIndex=0;
				}
				return;
			}
			
			XmlDocument document=new XmlDocument();
			try{
				
				document.Load("FreeDentalConfig.xml");
				XPathNavigator Navigator=document.CreateNavigator();
				XPathNavigator nav;
				//Database type
				nav=Navigator.SelectSingleNode("//DatabaseType");
				if(listType.Items.Count>0) {//not true on startup
					listType.SelectedIndex=0;
				}
				DBtype=DatabaseType.MySql;	
				if(nav!=null && nav.Value=="Oracle" && listType.Items.Count>0){
					listType.SelectedIndex=1;
					DBtype=DatabaseType.Oracle;
				}
				//See if there's a DatabaseConnection
				nav=Navigator.SelectSingleNode("//DatabaseConnection");
				if(nav!=null) {
					//If there is a DatabaseConnection, then use it.
					groupServer.Enabled=false;
					comboComputerName.Text=nav.SelectSingleNode("ComputerName").Value;
					comboDatabase.Text=nav.SelectSingleNode("Database").Value;
					textUser.Text=nav.SelectSingleNode("User").Value;
					textPassword.Text=nav.SelectSingleNode("Password").Value;
					XPathNavigator noshownav=nav.SelectSingleNode("NoShowOnStartup");
					if(noshownav!=null){
						string noshow=noshownav.Value;
						if(noshow=="True"){
							NoShow=true;
							checkNoShow.Checked=true;
						}
					}
					return;
				}
				nav=Navigator.SelectSingleNode("//ServerConnection");
				if(nav!=null) {
					//If there is a ServerConnection, then use it.
					checkConnectServer.Checked=true;
					groupDirect.Enabled=false;
					comboServerName2.Text=nav.SelectSingleNode("ComputerName").Value;
					XPathNavigator navport=nav.SelectSingleNode("ServerPort");
					if(navport!=null) {
						textPort.Text=navport.Value;
					}
					comboDatabase2.Text=nav.SelectSingleNode("Database").Value;
					textUser2.Select();
					return;
				}
				
			}catch(Exception) {
				//Common error: root element is missing
				//MessageBox.Show(e.Message);
			}
			comboComputerName.Text="localhost";
			comboDatabase.Text="opendental";
			textUser.Text="root";
			checkConnectServer.Checked=false;
			groupDirect.Enabled=true;
			groupServer.Enabled=false;
			if(listType.Items.Count>1){
				listType.SelectedIndex=0;
			}
			DBtype=DatabaseType.MySql;
		}

		///<summary>Only called at startup if this dialog is not supposed to be shown.  Must call GetConfig first.</summary>
		public bool TryToConnect(){
			OpenDentBusiness.DataConnection dcon=new OpenDentBusiness.DataConnection();
			//Try to connect to the database directly
			try {
				if(textConnectionString.Text.Length>0){
						dcon.SetDb(textConnectionString.Text,"",DBtype);
				}else{
					dcon.SetDb(comboComputerName.Text,comboDatabase.Text,textUser.Text,textPassword.Text,"","",DBtype);
				}
				//a direct connection does not utilize lower privileges.
				RemotingClient.OpenDentBusinessIsLocal=true;
				return true;
			}
			catch {//(Exception ex){
				return false;
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(checkConnectServer.Checked){
				if(textPort.errorProvider1.GetError(textPort)!="") {
					MsgBox.Show(this,"Please fix data entry errors first.");
					return;
				}
				string originalServer=RemotingClient.ServerName;
				int originalPort=RemotingClient.ServerPort;
				RemotingClient.ServerName=comboServerName2.Text;
				RemotingClient.ServerPort=PIn.PInt(textPort.Text);
				DtoLogin dto=new DtoLogin();
				dto.Database=comboDatabase2.Text;
				dto.OdUser=textUser2.Text;
				dto.OdPassHash=UserodB.EncryptPassword(textPassword2.Text);
				try{
					RemotingClient.ProcessCommand(dto);
					RemotingClient.OpenDentBusinessIsLocal=false;
				}
				catch(Exception ex){
					RemotingClient.ServerName=originalServer;
					RemotingClient.ServerPort=originalPort;
					MessageBox.Show(ex.Message);
					return;
				}
			}
			else{
				OpenDentBusiness.DataConnection dcon;
				//Try to connect to the database directly
				try {
					DBtype=DatabaseType.MySql;
					if(listType.SelectedIndex==1) {
						DBtype=DatabaseType.Oracle;
					}
					DataSettings.DbType = DBtype;
					dcon=new OpenDentBusiness.DataConnection(DBtype);
					if(textConnectionString.Text.Length>0){
						DataSettings.ConnectionString = textConnectionString.Text; 
						dcon.SetDb(textConnectionString.Text,"",DBtype);
					}else{
						DataSettings.CreateConnectionString(comboComputerName.Text, comboDatabase.Text, textUser.Text, textPassword.Text);
						dcon.SetDb(comboComputerName.Text,comboDatabase.Text,textUser.Text,textPassword.Text,"","",DBtype);
					}
					//a direct connection does not utilize lower privileges.
				}
				catch(Exception ex){
					MessageBox.Show(//Lan.g(this,"Could not establish connection to database."));
						ex.Message);
					return;
				}
				RemotingClient.OpenDentBusinessIsLocal=true;
			}
			try{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("    ");
				using(XmlWriter writer=XmlWriter.Create("FreeDentalConfig.xml",settings)) {
					writer.WriteStartElement("ConnectionSettings");
					if(RemotingClient.OpenDentBusinessIsLocal){
						writer.WriteStartElement("DatabaseConnection");
						writer.WriteStartElement("ComputerName");
						writer.WriteString(comboComputerName.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("Database");
						writer.WriteString(comboDatabase.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("User");
						writer.WriteString(textUser.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("Password");
						writer.WriteString(textPassword.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("NoShowOnStartup");
						if(checkNoShow.Checked) {
							writer.WriteString("True");
						}
						else {
							writer.WriteString("False");
						}
						writer.WriteEndElement();
						writer.WriteEndElement();
					}
					else{
						writer.WriteStartElement("ServerConnection");
						writer.WriteStartElement("ComputerName");
						writer.WriteString(comboServerName2.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("ServerPort");
						writer.WriteString(textPort.Text);
						writer.WriteEndElement();
						writer.WriteStartElement("Database");
						writer.WriteString(comboDatabase2.Text);
						writer.WriteEndElement();
						writer.WriteEndElement();
					}
					writer.WriteStartElement("DatabaseType");
					if(listType.SelectedIndex==0){
						writer.WriteString("MySql");
					}
					else{
						writer.WriteString("Oracle");
					}
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.Flush();
				}//using writer
			}
			catch{
				//data not saved.
			}
			//fyiReporting.RDL.DataSource.SetOpenDentalConnectionString(
			//	"Server="+ComputerName+";Database="+Database+";User ID="+DbUser+";Password="+Password+";CharSet=utf8");
			DialogResult=DialogResult.OK;
    }

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormConfig_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			//if(DialogResult==DialogResult.Cancel){
			//	ResetToOriginal();
			//}
		}

		

		

		

		

		

		

		

		

		
		
  }
}
