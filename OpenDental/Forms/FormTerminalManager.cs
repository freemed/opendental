using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormTerminalManager:System.Windows.Forms.Form {
		private OpenDental.UI.ODGrid gridMain;
		private Label label1;
		private Label label2;
		private TextBox textPassword;
		private OpenDental.UI.Button butSave;
		private IContainer components;
		private OpenDental.UI.Button butLoad;
		private Timer timer1;
		private GroupBox groupBox1;
		private ListBox listSheets;
		private Label labelSheets;
		private Label labelPatient;
		private GroupBox groupBox2;
		private OpenDental.UI.Button butClear;
		private TerminalActive[] TerminalList;
		//private int counterActivated;
		//private bool isAdvanced;

		///<summary></summary>
		public FormTerminalManager()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//gridMain.ContextMenu=contextMenuMain;
			Lan.F(this);
			//#if DEBUG
			//label4.Visible=true;
			//#endif
			//isAdvanced=false;
			Width=239;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTerminalManager));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelSheets = new System.Windows.Forms.Label();
			this.labelPatient = new System.Windows.Forms.Label();
			this.listSheets = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butClear = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.butLoad = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18,13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(437,49);
			this.label1.TabIndex = 3;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7,16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(327,31);
			this.label2.TabIndex = 4;
			this.label2.Text = "To close a terminal, go to that computer and click close.  You will need to enter" +
    " this password:";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(10,50);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(129,20);
			this.textPassword.TabIndex = 5;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 4000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelSheets);
			this.groupBox1.Controls.Add(this.labelPatient);
			this.groupBox1.Controls.Add(this.listSheets);
			this.groupBox1.Controls.Add(this.butLoad);
			this.groupBox1.Location = new System.Drawing.Point(475,60);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168,252);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Current Patient";
			// 
			// labelSheets
			// 
			this.labelSheets.Location = new System.Drawing.Point(11,37);
			this.labelSheets.Name = "labelSheets";
			this.labelSheets.Size = new System.Drawing.Size(123,18);
			this.labelSheets.TabIndex = 10;
			this.labelSheets.Text = "Forms for Terminal";
			this.labelSheets.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(11,19);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(147,18);
			this.labelPatient.TabIndex = 9;
			this.labelPatient.Text = "Fname Lname";
			// 
			// listSheets
			// 
			this.listSheets.FormattingEnabled = true;
			this.listSheets.Location = new System.Drawing.Point(14,59);
			this.listSheets.Name = "listSheets";
			this.listSheets.Size = new System.Drawing.Size(120,147);
			this.listSheets.TabIndex = 8;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textPassword);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.butSave);
			this.groupBox2.Location = new System.Drawing.Point(21,315);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(344,80);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Password";
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(21,67);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(421,206);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Active Terminals";
			this.gridMain.TranslationName = "TableTerminals";
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Location = new System.Drawing.Point(349,279);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(93,24);
			this.butClear.TabIndex = 13;
			this.butClear.Text = "Clear Terminal";
			this.butClear.UseVisualStyleBackColor = true;
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(145,48);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(97,24);
			this.butSave.TabIndex = 6;
			this.butSave.Text = "Save Password";
			this.butSave.UseVisualStyleBackColor = true;
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// butLoad
			// 
			this.butLoad.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butLoad.Autosize = true;
			this.butLoad.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLoad.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLoad.CornerRadius = 4F;
			this.butLoad.Location = new System.Drawing.Point(14,219);
			this.butLoad.Name = "butLoad";
			this.butLoad.Size = new System.Drawing.Size(93,24);
			this.butLoad.TabIndex = 7;
			this.butLoad.Text = "Load Patient";
			this.butLoad.UseVisualStyleBackColor = true;
			this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
			// 
			// FormTerminalManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(665,410);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTerminalManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Terminal Manager";
			this.Load += new System.EventHandler(this.FormTerminalManager_Load);
			this.Activated += new System.EventHandler(this.FormTerminalManager_Activated);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormTerminalManager_Load(object sender,EventArgs e) {
			//MessageBox.Show("load");
			textPassword.Text=PrefC.GetString(PrefName.TerminalClosePassword);
			FillGrid();
		}

		private void FormTerminalManager_Activated(object sender,EventArgs e) {
			//MessageBox.Show("activated");
			//counterActivated++;
			//label4.Text=counterActivated.ToString();
			FillPat();
		}

		private void FillGrid(){
			try{
				TerminalList=TerminalActives.Refresh();
			}
			catch{//SocketException if db connection gets lost.
				return;
			}
			int selected=gridMain.GetSelectedIndex();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableTerminals","Computer Name"),150);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g("TableTerminals","Status"),100);
			//gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTerminals","Patient"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TerminalList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(TerminalList[i].ComputerName);
				//row.Cells.Add(Lan.g("TerminalStatusEnum",TerminalList[i].TerminalStatus.ToString()));
				if(TerminalList[i].PatNum==0){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Patients.GetLim(TerminalList[i].PatNum).GetNameLF());
				}			  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(selected,true);
			if(gridMain.GetSelectedIndex()==-1 && gridMain.Rows.Count>0){
				gridMain.SetSelected(0,true);
			}
		}

		private void FillPat() {
			if(FormOpenDental.CurPatNum==0) {
				labelPatient.Text=Lan.g(this,"none");
				labelSheets.Visible=false;
				listSheets.Visible=false;
				butLoad.Visible=false;
			}
			else {
				Patient pat=Patients.GetLim(FormOpenDental.CurPatNum);
				labelPatient.Text=pat.GetNameFL();
				labelSheets.Visible=true;
				listSheets.Visible=true;
				butLoad.Visible=true;
				listSheets.Items.Clear();
				List<Sheet> sheetList=Sheets.GetForTerminal(FormOpenDental.CurPatNum);
				for(int i=0;i<sheetList.Count;i++) {
					listSheets.Items.Add(sheetList[i].Description);
				}
			}
		}

		/*
		private void butAdvanced_Click(object sender,EventArgs e) {
			if(isAdvanced) {
				isAdvanced=false;
				Width=239;
			}
			else {
				isAdvanced=true;
				Width=749;
			}
		}*/

		private void butClear_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.PatNum==0) {
				return;
			}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.PatNum=0;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		private void butLoad_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.PatNum!=0) {
				if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
					return;
				}
			}
			long patNum=FormOpenDental.CurPatNum;
			terminal.PatNum=patNum;
			TerminalActives.Update(terminal);
			FillGrid();
			/*
			else {
				//just load up a terminal on this computer in a modal window.  The terminal window itself will handle clearing it from the db when done.
				TerminalActives.DeleteAllForComputer(Environment.MachineName);
				TerminalActive terminal=new TerminalActive();
				terminal.ComputerName=Environment.MachineName;
				terminal.PatNum=FormOpenDental.CurPatNum;
				TerminalActives.Insert(terminal);
				//Still need to start the modal window
			}*/
		}

		/*
		private void menuItemStandby_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus==TerminalStatusEnum.Standby) {
				//MsgBox.Show(this,"Please load a patient onto this terminal first.");
				return;
			}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.PatNum=0;
			terminal.TerminalStatus=TerminalStatusEnum.Standby;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		private void menuItemPatientInfo_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus==TerminalStatusEnum.Standby) {
				MsgBox.Show(this,"Please load a patient onto this terminal first.");
				return;
			}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.TerminalStatus=TerminalStatusEnum.PatientInfo;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		private void menuItemMedical_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus==TerminalStatusEnum.Standby) {
				MsgBox.Show(this,"Please load a patient onto this terminal first.");
				return;
			}
			//See if the selected patient already has diseases attached
			Disease[] DiseaseList=Diseases.Refresh(terminal.PatNum);
			if(DiseaseList.Length>0) {
				MsgBox.Show(this,"This patient already has diseases attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				return;
			}
			//See if the selected patient already has questions attached
			//if(Questions.PatHasQuest(terminal.PatNum)) {
				//MsgBox.Show(this,"This patient already has questions attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				//return;
			//}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.TerminalStatus=TerminalStatusEnum.Medical;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		private void menuItemUpdateOnly_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus==TerminalStatusEnum.Standby) {
				MsgBox.Show(this,"Please load a patient onto this terminal first.");
				return;
			}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.TerminalStatus=TerminalStatusEnum.UpdateOnly;
			TerminalActives.Update(terminal);
			FillGrid();
		}*/

		private void timer1_Tick(object sender,EventArgs e) {
			//happens every 4 seconds to refresh list from db.
			FillGrid();
		}

		private void butSave_Click(object sender,EventArgs e) {
			if(Prefs.UpdateString(PrefName.TerminalClosePassword,textPassword.Text)){
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			MsgBox.Show(this,"Done.");
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		


	}
}





















