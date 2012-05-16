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
	public class FormPhoneEmpDefaultEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private Label label1;
		private Label label2;
		private OpenDental.UI.Button butDelete;
		private ListBox listRingGroup;
		private CheckBox checkNoGraph;
		private Label label5;
		private CheckBox checkNoColor;
		private TextBox textEmpName;
		private Label label6;
		private TextBox textNotes;
		private Label label3;
		private TextBox textComputerName;
		private Label label4;
		private CheckBox checkIsPrivateScreen;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private Label label14;
		private Label label15;
		private Label label16;
		private ValidNum textEmployeeNum;
		private ValidNum textPhoneExt;
		private ListBox listStatusOverride;
		private Label label17;
		private CheckBox checkIsTriageOperator;
		public PhoneEmpDefault PedCur;
		///<summary>Will always be the override status upon load.</summary>
		private PhoneEmpStatusOverride StatusOld;

		///<summary></summary>
		public FormPhoneEmpDefaultEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPhoneEmpDefaultEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listRingGroup = new System.Windows.Forms.ListBox();
			this.checkNoGraph = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkNoColor = new System.Windows.Forms.CheckBox();
			this.textEmpName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textComputerName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkIsPrivateScreen = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.listStatusOverride = new System.Windows.Forms.ListBox();
			this.label17 = new System.Windows.Forms.Label();
			this.checkIsTriageOperator = new System.Windows.Forms.CheckBox();
			this.textPhoneExt = new OpenDental.ValidNum();
			this.textEmployeeNum = new OpenDental.ValidNum();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 11;
			this.label1.Text = "EmployeeNum";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(1, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(139, 20);
			this.label2.TabIndex = 13;
			this.label2.Text = "Default Ring Group";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listRingGroup
			// 
			this.listRingGroup.FormattingEnabled = true;
			this.listRingGroup.Location = new System.Drawing.Point(144, 144);
			this.listRingGroup.Name = "listRingGroup";
			this.listRingGroup.Size = new System.Drawing.Size(120, 43);
			this.listRingGroup.TabIndex = 4;
			// 
			// checkNoGraph
			// 
			this.checkNoGraph.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoGraph.Location = new System.Drawing.Point(3, 87);
			this.checkNoGraph.Name = "checkNoGraph";
			this.checkNoGraph.Size = new System.Drawing.Size(155, 20);
			this.checkNoGraph.TabIndex = 2;
			this.checkNoGraph.Text = "No Graph";
			this.checkNoGraph.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoGraph.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(40, 200);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 20);
			this.label5.TabIndex = 23;
			this.label5.Text = "Extension";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkNoColor
			// 
			this.checkNoColor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoColor.Location = new System.Drawing.Point(3, 113);
			this.checkNoColor.Name = "checkNoColor";
			this.checkNoColor.Size = new System.Drawing.Size(155, 20);
			this.checkNoColor.TabIndex = 3;
			this.checkNoColor.Text = "No Color";
			this.checkNoColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkNoColor.UseVisualStyleBackColor = true;
			// 
			// textEmpName
			// 
			this.textEmpName.Location = new System.Drawing.Point(144, 56);
			this.textEmpName.Name = "textEmpName";
			this.textEmpName.Size = new System.Drawing.Size(170, 20);
			this.textEmpName.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(137, 20);
			this.label6.TabIndex = 26;
			this.label6.Text = "Employee First Name";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNotes
			// 
			this.textNotes.Location = new System.Drawing.Point(144, 292);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(352, 51);
			this.textNotes.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 291);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 20);
			this.label3.TabIndex = 29;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textComputerName
			// 
			this.textComputerName.Location = new System.Drawing.Point(144, 358);
			this.textComputerName.Name = "textComputerName";
			this.textComputerName.Size = new System.Drawing.Size(213, 20);
			this.textComputerName.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 357);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 20);
			this.label4.TabIndex = 31;
			this.label4.Text = "Computer Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsPrivateScreen
			// 
			this.checkIsPrivateScreen.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPrivateScreen.Location = new System.Drawing.Point(3, 389);
			this.checkIsPrivateScreen.Name = "checkIsPrivateScreen";
			this.checkIsPrivateScreen.Size = new System.Drawing.Size(155, 20);
			this.checkIsPrivateScreen.TabIndex = 9;
			this.checkIsPrivateScreen.Text = "Private Screen";
			this.checkIsPrivateScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsPrivateScreen.UseVisualStyleBackColor = true;
			this.checkIsPrivateScreen.Click += new System.EventHandler(this.checkIsPrivateScreen_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(200, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(366, 20);
			this.label7.TabIndex = 34;
			this.label7.Text = "This number must be looked up in the employee table";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(161, 85);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(366, 20);
			this.label8.TabIndex = 35;
			this.label8.Text = "Do not show this employee on the employee time graph";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(161, 112);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(414, 20);
			this.label9.TabIndex = 36;
			this.label9.Text = "Do not show the red and green phone status colors in the phone panel";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(267, 143);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(359, 47);
			this.label10.TabIndex = 37;
			this.label10.Text = "The normal ring group for this employee when clocked in.  If you change this valu" +
    "e, the change will not immediately show on each workstation, but will instead re" +
    "quire a restart of OD.";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(207, 193);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(498, 37);
			this.label11.TabIndex = 38;
			this.label11.Text = "The current phone extension for this employee.  Can change from day to day.  If t" +
    "his employee is not working today, and you need to use their regular extension, " +
    " set this value to 400+";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(315, 56);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(366, 20);
			this.label12.TabIndex = 39;
			this.label12.Text = "This is the name that will show in the phone panel.";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(268, 235);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(321, 20);
			this.label13.TabIndex = 40;
			this.label13.Text = "Mark yourself unavailable only if approved by manager";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(502, 291);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(157, 35);
			this.label14.TabIndex = 41;
			this.label14.Text = "Why unavailable?\r\nWhy offline assist?";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(362, 355);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(350, 46);
			this.label15.TabIndex = 42;
			this.label15.Text = "If your computer IP matches your phone extension, do not set this value.  This is" +
    " mostly used by remote users.  Not usually needed for floaters because your IP w" +
    "ill match your extension.";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(161, 386);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(194, 47);
			this.label16.TabIndex = 43;
			this.label16.Text = "Halts screen captures.  Only used/allowed by managers. ";
			// 
			// listStatusOverride
			// 
			this.listStatusOverride.FormattingEnabled = true;
			this.listStatusOverride.Location = new System.Drawing.Point(144, 235);
			this.listStatusOverride.Name = "listStatusOverride";
			this.listStatusOverride.Size = new System.Drawing.Size(120, 43);
			this.listStatusOverride.TabIndex = 6;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(2, 237);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(139, 20);
			this.label17.TabIndex = 46;
			this.label17.Text = "StatusOverride";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsTriageOperator
			// 
			this.checkIsTriageOperator.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsTriageOperator.Location = new System.Drawing.Point(3, 415);
			this.checkIsTriageOperator.Name = "checkIsTriageOperator";
			this.checkIsTriageOperator.Size = new System.Drawing.Size(155, 20);
			this.checkIsTriageOperator.TabIndex = 10;
			this.checkIsTriageOperator.Text = "Triage Operator";
			this.checkIsTriageOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsTriageOperator.UseVisualStyleBackColor = true;
			// 
			// textPhoneExt
			// 
			this.textPhoneExt.Location = new System.Drawing.Point(144, 201);
			this.textPhoneExt.MaxVal = 1000;
			this.textPhoneExt.MinVal = 0;
			this.textPhoneExt.Name = "textPhoneExt";
			this.textPhoneExt.Size = new System.Drawing.Size(54, 20);
			this.textPhoneExt.TabIndex = 5;
			// 
			// textEmployeeNum
			// 
			this.textEmployeeNum.Location = new System.Drawing.Point(144, 24);
			this.textEmployeeNum.MaxVal = 255;
			this.textEmployeeNum.MinVal = 0;
			this.textEmployeeNum.Name = "textEmployeeNum";
			this.textEmployeeNum.Size = new System.Drawing.Size(54, 20);
			this.textEmployeeNum.TabIndex = 0;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(28, 467);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84, 24);
			this.butDelete.TabIndex = 13;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(539, 467);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 11;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(632, 467);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 12;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPhoneEmpDefaultEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(724, 504);
			this.Controls.Add(this.checkIsTriageOperator);
			this.Controls.Add(this.listStatusOverride);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textPhoneExt);
			this.Controls.Add(this.textEmployeeNum);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.checkIsPrivateScreen);
			this.Controls.Add(this.textComputerName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textEmpName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.checkNoColor);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkNoGraph);
			this.Controls.Add(this.listRingGroup);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPhoneEmpDefaultEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Employee Setting";
			this.Load += new System.EventHandler(this.FormPhoneEmpDefaultEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPhoneEmpDefaultEdit_Load(object sender, System.EventArgs e) {
			StatusOld=PedCur.StatusOverride;//We use this for testing when user clicks OK.
			if(!IsNew){
				textEmployeeNum.ReadOnly=true;
			}
			textEmployeeNum.Text=PedCur.EmployeeNum.ToString();
			textEmpName.Text=PedCur.EmpName;
			checkNoGraph.Checked=PedCur.NoGraph;
			checkNoColor.Checked=PedCur.NoColor;
			for(int i=0;i<Enum.GetNames(typeof(AsteriskRingGroups)).Length;i++){
				listRingGroup.Items.Add(Enum.GetNames(typeof(AsteriskRingGroups))[i]);
			}
			listRingGroup.SelectedIndex=(int)PedCur.RingGroups;
			textPhoneExt.Text=PedCur.PhoneExt.ToString();
			for(int i=0;i<Enum.GetNames(typeof(PhoneEmpStatusOverride)).Length;i++) {
				listStatusOverride.Items.Add(Enum.GetNames(typeof(PhoneEmpStatusOverride))[i]);
			}
			listStatusOverride.SelectedIndex=(int)PedCur.StatusOverride;
			textNotes.Text=PedCur.Notes;
			textComputerName.Text=PedCur.ComputerName;
			checkIsPrivateScreen.Checked=PedCur.IsPrivateScreen;
			checkIsTriageOperator.Checked=PedCur.IsTriageOperator;
		}

		private void checkIsPrivateScreen_Click(object sender,EventArgs e) {
			if(Security.CurUser.EmployeeNum!=10			//Debbie
				&& Security.CurUser.EmployeeNum!=13		//Shannon
				&& Security.CurUser.EmployeeNum!=17		//Nathan
				&& Security.CurUser.EmployeeNum!=22)	//Jordan
			{
				//Put the checkbox back the way it was before user clicked on it.
				if(checkIsPrivateScreen.Checked) {
					checkIsPrivateScreen.Checked=false;
				}
				else {
					checkIsPrivateScreen.Checked=true;
				}
				MsgBox.Show(this,"You do not have permission to halt screen captures.");
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")){
				return;
			}
			PhoneEmpDefaults.Delete(PedCur.EmployeeNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//Using a switch statement in case we want special functionality for the other statuses later on.
			switch((PhoneEmpStatusOverride)listStatusOverride.SelectedIndex) {
				case PhoneEmpStatusOverride.None:
					if(StatusOld==PhoneEmpStatusOverride.Unavailable) {
						MsgBox.Show(this,"Change your status from unavailable by using the small phone panel.");
						return;
					}
					break;
				case PhoneEmpStatusOverride.OfflineAssist:
					if(StatusOld==PhoneEmpStatusOverride.Unavailable) {
						MsgBox.Show(this,"Change your status from unavailable by using the small phone panel.");
						return;
					}
					break;
				case PhoneEmpStatusOverride.Unavailable:
					//We set ourselves unavailable from this window because we require an explination.
					//This is the only status that will synch with the phone table, all others should be handled by the small phone panel.
					Phones.SetPhoneStatus(ClockStatusEnum.Unavailable,PedCur.PhoneExt);
					break;
			}
			if(IsNew){
				if(textEmployeeNum.Text==""){
					MsgBox.Show(this,"Unique EmployeeNum is required.");
					return;
				}
				if(textEmpName.Text==""){
					MsgBox.Show(this,"Employee name is required.");
					return;
				}
				PedCur.EmployeeNum=PIn.Long(textEmployeeNum.Text);
			}
			PedCur.EmpName=textEmpName.Text;
			PedCur.NoGraph=checkNoGraph.Checked;
			PedCur.NoColor=checkNoColor.Checked;
			PedCur.RingGroups=(AsteriskRingGroups)listRingGroup.SelectedIndex;
			PedCur.PhoneExt=PIn.Int(textPhoneExt.Text);
			PedCur.StatusOverride=(PhoneEmpStatusOverride)listStatusOverride.SelectedIndex;
			PedCur.Notes=textNotes.Text;
			PedCur.ComputerName=textComputerName.Text;
			PedCur.IsPrivateScreen=checkIsPrivateScreen.Checked;
			PedCur.IsTriageOperator=checkIsTriageOperator.Checked;
			if(IsNew){
				PhoneEmpDefaults.Insert(PedCur);
			}
			else{
				PhoneEmpDefaults.Update(PedCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
		

	

	

		

		

		


	}
}





















