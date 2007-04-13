using System;
using System.Drawing;
using System.Collections;
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
		private OpenDental.UI.Button butNewPatient;
		private Label label4;
		private OpenDental.UI.Button butUpdatePatient;
		private Timer timer1;
		private ContextMenu contextMenuMain;
		private MenuItem menuItemStandby;
		private MenuItem menuItemPatientInfo;
		private MenuItem menuItemMedical;
		private MenuItem menuItemUpdateOnly;
		private GroupBox groupBox1;
		private TerminalActive[] TerminalList;

		///<summary></summary>
		public FormTerminalManager()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			gridMain.ContextMenu=contextMenuMain;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTerminalManager));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuMain = new System.Windows.Forms.ContextMenu();
			this.menuItemStandby = new System.Windows.Forms.MenuItem();
			this.menuItemPatientInfo = new System.Windows.Forms.MenuItem();
			this.menuItemMedical = new System.Windows.Forms.MenuItem();
			this.menuItemUpdateOnly = new System.Windows.Forms.MenuItem();
			this.butUpdatePatient = new OpenDental.UI.Button();
			this.butNewPatient = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(437,47);
			this.label1.TabIndex = 3;
			this.label1.Text = "Activate each terminal by going to that computer and selecting Tools-Terminal.  O" +
    "nce the terminals are activated, you control them from here.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30,285);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(327,31);
			this.label2.TabIndex = 4;
			this.label2.Text = "To close a terminal, go to that computer and click close.  You will need to enter" +
    " this password:";
			// 
			// textPassword
			// 
			this.textPassword.Location = new System.Drawing.Point(33,319);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(129,20);
			this.textPassword.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(233,28);
			this.label4.TabIndex = 10;
			this.label4.Text = "Diseases and questions will not be available.";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 4000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// contextMenuMain
			// 
			this.contextMenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStandby,
            this.menuItemPatientInfo,
            this.menuItemMedical,
            this.menuItemUpdateOnly});
			// 
			// menuItemStandby
			// 
			this.menuItemStandby.Index = 0;
			this.menuItemStandby.Text = "Standby";
			this.menuItemStandby.Click += new System.EventHandler(this.menuItemStandby_Click);
			// 
			// menuItemPatientInfo
			// 
			this.menuItemPatientInfo.Index = 1;
			this.menuItemPatientInfo.Text = "Patient Info";
			this.menuItemPatientInfo.Click += new System.EventHandler(this.menuItemPatientInfo_Click);
			// 
			// menuItemMedical
			// 
			this.menuItemMedical.Index = 2;
			this.menuItemMedical.Text = "Medical";
			this.menuItemMedical.Click += new System.EventHandler(this.menuItemMedical_Click);
			// 
			// menuItemUpdateOnly
			// 
			this.menuItemUpdateOnly.Index = 3;
			this.menuItemUpdateOnly.Text = "Update Only";
			this.menuItemUpdateOnly.Click += new System.EventHandler(this.menuItemUpdateOnly_Click);
			// 
			// butUpdatePatient
			// 
			this.butUpdatePatient.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUpdatePatient.Autosize = true;
			this.butUpdatePatient.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUpdatePatient.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUpdatePatient.CornerRadius = 4F;
			this.butUpdatePatient.Location = new System.Drawing.Point(9,81);
			this.butUpdatePatient.Name = "butUpdatePatient";
			this.butUpdatePatient.Size = new System.Drawing.Size(88,25);
			this.butUpdatePatient.TabIndex = 9;
			this.butUpdatePatient.Text = "Update Patient";
			this.butUpdatePatient.UseVisualStyleBackColor = true;
			this.butUpdatePatient.Click += new System.EventHandler(this.butUpdatePatient_Click);
			// 
			// butNewPatient
			// 
			this.butNewPatient.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNewPatient.Autosize = true;
			this.butNewPatient.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNewPatient.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNewPatient.CornerRadius = 4F;
			this.butNewPatient.Location = new System.Drawing.Point(9,24);
			this.butNewPatient.Name = "butNewPatient";
			this.butNewPatient.Size = new System.Drawing.Size(88,25);
			this.butNewPatient.TabIndex = 7;
			this.butNewPatient.Text = "New Patient";
			this.butNewPatient.UseVisualStyleBackColor = true;
			this.butNewPatient.Click += new System.EventHandler(this.butNewPatient_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(33,345);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(97,25);
			this.butSave.TabIndex = 6;
			this.butSave.Text = "Save Password";
			this.butSave.UseVisualStyleBackColor = true;
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(33,63);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(421,206);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Active Terminals";
			this.gridMain.TranslationName = "TableTerminals";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butNewPatient);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.butUpdatePatient);
			this.groupBox1.Location = new System.Drawing.Point(467,57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(251,143);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Load Current Patient As";
			// 
			// FormTerminalManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(733,386);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTerminalManager";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Terminal Manager";
			this.Load += new System.EventHandler(this.FormTerminalManager_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormTerminalManager_Load(object sender,EventArgs e) {
			textPassword.Text=PrefB.GetString("TerminalClosePassword");
			FillGrid();
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
			col=new ODGridColumn(Lan.g("TableTerminals","Status"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableTerminals","Patient"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TerminalList.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(TerminalList[i].ComputerName);
				row.Cells.Add(Lan.g("TerminalStatusEnum",TerminalList[i].TerminalStatus.ToString()));
				/*switch (TerminalList[i].TerminalStatus){
					case TerminalStatusEnum.Standby:
						row.Cells.Add("");
						break;
				}*/
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

		private void butNewPatient_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus!=TerminalStatusEnum.Standby){
				if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?"))
				{
					return;
				}
			}
			if(FormOpenDental.CurPatNum==0) {
				MsgBox.Show(this,"Please select a patient in the main window first.");
				return;
			}
			int patNum=FormOpenDental.CurPatNum;
			//See if the selected patient already has diseases attached
			Disease[] DiseaseList=Diseases.Refresh(patNum);
			if(DiseaseList.Length>0){
				MsgBox.Show(this,"This patient already has diseases attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				return;
			}
			//See if the selected patient already has questions attached
			/*if(Questions.PatHasQuest(patNum)){
				MsgBox.Show(this,"This patient already has questions attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				return;
			}*/
			terminal.PatNum=patNum;
			terminal.TerminalStatus=TerminalStatusEnum.PatientInfo;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		private void butUpdatePatient_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus!=TerminalStatusEnum.Standby) {
				if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
					return;
				}
			}
			if(FormOpenDental.CurPatNum==0){
				MsgBox.Show(this,"Please select a patient in the main window first.");
				return;
			}
			//FormPatientSelect FormP=new FormPatientSelect();
			//FormP.ShowDialog();
			//if(FormP.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			//int patNum=
			terminal.PatNum=FormOpenDental.CurPatNum;
			terminal.TerminalStatus=TerminalStatusEnum.UpdateOnly;
			TerminalActives.Update(terminal);
			FillGrid();
		}

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
			/*if(Questions.PatHasQuest(terminal.PatNum)) {
				MsgBox.Show(this,"This patient already has questions attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				return;
			}*/
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.TerminalStatus=TerminalStatusEnum.Medical;
			TerminalActives.Update(terminal);
			FillGrid();
		}

		/*private void menuItemQuestions_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a terminal first.");
				return;
			}
			TerminalActive terminal=TerminalList[gridMain.GetSelectedIndex()].Copy();
			if(terminal.TerminalStatus==TerminalStatusEnum.Standby) {
				MsgBox.Show(this,"Please load a patient onto this terminal first.");
				return;
			}
			//See if the selected patient already has questions attached
			if(Questions.PatHasQuest(terminal.PatNum)) {
				MsgBox.Show(this,"This patient already has questions attached.  This function is only intended for new patients.  Patient cannot be loaded.");
				return;
			}
			if(!MsgBox.Show(this,true,"A patient is currently using the terminal.  If you continue, they will lose the information that is on their screen.  Continue anyway?")) {
				return;
			}
			terminal.TerminalStatus=TerminalStatusEnum.Questions;
			terminal.Update();
			FillGrid();
		}*/

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
		}

		private void timer1_Tick(object sender,EventArgs e) {
			//happens every 4 seconds to refresh list from db.
			FillGrid();
		}

		private void butSave_Click(object sender,EventArgs e) {
			if(Prefs.UpdateString("TerminalClosePassword",textPassword.Text)){
				DataValid.SetInvalid(InvalidTypes.Prefs);
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





















