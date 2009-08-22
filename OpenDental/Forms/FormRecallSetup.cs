using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormRecallSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPostcardsPerSheet;
		private System.Windows.Forms.CheckBox checkReturnAdd;
		private GroupBox groupBox2;
		private ValidDouble textDown;
		private Label label12;
		private ValidDouble textRight;
		private Label label13;
		private CheckBox checkGroupFamilies;
		private Label label14;
		private Label label15;
		private GroupBox groupBox3;
		private Label label25;
		private ComboBox comboStatusMailedRecall;
		private ComboBox comboStatusEmailedRecall;
		private Label label26;
		private ListBox listTypes;
		private Label label1;
		private ValidNumber textDaysPast;
		private ValidNumber textDaysFuture;
		private GroupBox groupBox1;
		private ValidNumber textDaysSecondReminder;
		private ValidNumber textDaysFirstReminder;
		private Label label2;
		private Label label3;
		private Label label5;
		private OpenDental.UI.ODGrid gridMain;
		private System.ComponentModel.Container components = null;
		private bool changed;

		///<summary></summary>
		public FormRecallSetup(){
			InitializeComponent();
			Lan.F(this);
			//Lan.C(this, new System.Windows.Forms.Control[] {
				//textBox1,
				//textBox6
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecallSetup));
			this.textPostcardsPerSheet = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.checkReturnAdd = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label25 = new System.Windows.Forms.Label();
			this.comboStatusMailedRecall = new System.Windows.Forms.ComboBox();
			this.comboStatusEmailedRecall = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.listTypes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textDaysSecondReminder = new OpenDental.ValidNumber();
			this.textDaysFirstReminder = new OpenDental.ValidNumber();
			this.textDaysFuture = new OpenDental.ValidNumber();
			this.textDaysPast = new OpenDental.ValidNumber();
			this.textDown = new OpenDental.ValidDouble();
			this.textRight = new OpenDental.ValidDouble();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(176,475);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34,20);
			this.textPostcardsPerSheet.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(49,478);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,31);
			this.label8.TabIndex = 19;
			this.label8.Text = "Postcards per sheet (1,3,or 4)";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkReturnAdd
			// 
			this.checkReturnAdd.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReturnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReturnAdd.Location = new System.Drawing.Point(42,505);
			this.checkReturnAdd.Name = "checkReturnAdd";
			this.checkReturnAdd.Size = new System.Drawing.Size(147,19);
			this.checkReturnAdd.TabIndex = 43;
			this.checkReturnAdd.Text = "Show return address";
			this.checkReturnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textDown);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.textRight);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Location = new System.Drawing.Point(657,442);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(205,74);
			this.groupBox2.TabIndex = 48;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Adjust Postcard Position in Inches";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(57,45);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60,20);
			this.label12.TabIndex = 5;
			this.label12.Text = "Down";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(57,20);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(60,20);
			this.label13.TabIndex = 4;
			this.label13.Text = "Right";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGroupFamilies
			// 
			this.checkGroupFamilies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.Location = new System.Drawing.Point(44,15);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(121,18);
			this.checkGroupFamilies.TabIndex = 49;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(16,32);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133,20);
			this.label14.TabIndex = 50;
			this.label14.Text = "Days Past (usually blank)";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(51,53);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(98,20);
			this.label15.TabIndex = 52;
			this.label15.Text = "Days Future";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textDaysFuture);
			this.groupBox3.Controls.Add(this.textDaysPast);
			this.groupBox3.Controls.Add(this.checkGroupFamilies);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Location = new System.Drawing.Point(415,442);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(213,78);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Recall List Default View";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(17,431);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(157,16);
			this.label25.TabIndex = 57;
			this.label25.Text = "Status for mailed recall";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboStatusMailedRecall
			// 
			this.comboStatusMailedRecall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusMailedRecall.FormattingEnabled = true;
			this.comboStatusMailedRecall.Location = new System.Drawing.Point(176,427);
			this.comboStatusMailedRecall.MaxDropDownItems = 20;
			this.comboStatusMailedRecall.Name = "comboStatusMailedRecall";
			this.comboStatusMailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusMailedRecall.TabIndex = 58;
			// 
			// comboStatusEmailedRecall
			// 
			this.comboStatusEmailedRecall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEmailedRecall.FormattingEnabled = true;
			this.comboStatusEmailedRecall.Location = new System.Drawing.Point(176,451);
			this.comboStatusEmailedRecall.MaxDropDownItems = 20;
			this.comboStatusEmailedRecall.Name = "comboStatusEmailedRecall";
			this.comboStatusEmailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusEmailedRecall.TabIndex = 60;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(17,455);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(157,16);
			this.label26.TabIndex = 59;
			this.label26.Text = "Status for e-mailed recall";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listTypes
			// 
			this.listTypes.FormattingEnabled = true;
			this.listTypes.Location = new System.Drawing.Point(176,530);
			this.listTypes.Name = "listTypes";
			this.listTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listTypes.Size = new System.Drawing.Size(120,108);
			this.listTypes.TabIndex = 64;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,532);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157,65);
			this.label1.TabIndex = 63;
			this.label1.Text = "Types to show in recall list (typically just prophy, perio, and user-added types)" +
    "";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textDaysSecondReminder);
			this.groupBox1.Controls.Add(this.textDaysFirstReminder);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(415,543);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(213,91);
			this.groupBox1.TabIndex = 65;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Also show in list if # of days since";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7,67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(201,17);
			this.label5.TabIndex = 69;
			this.label5.Text = "(a very large number is recommended)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(48,19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101,20);
			this.label2.TabIndex = 50;
			this.label2.Text = "Initial Reminder";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(146,20);
			this.label3.TabIndex = 52;
			this.label3.Text = "Second (or more) Reminder";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(9,8);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(869,411);
			this.gridMain.TabIndex = 67;
			this.gridMain.Title = "Messages";
			this.gridMain.TranslationName = "TableRecallMsgs";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// textDaysSecondReminder
			// 
			this.textDaysSecondReminder.Location = new System.Drawing.Point(151,42);
			this.textDaysSecondReminder.MaxVal = 10000;
			this.textDaysSecondReminder.MinVal = 0;
			this.textDaysSecondReminder.Name = "textDaysSecondReminder";
			this.textDaysSecondReminder.Size = new System.Drawing.Size(53,20);
			this.textDaysSecondReminder.TabIndex = 66;
			// 
			// textDaysFirstReminder
			// 
			this.textDaysFirstReminder.Location = new System.Drawing.Point(151,20);
			this.textDaysFirstReminder.MaxVal = 10000;
			this.textDaysFirstReminder.MinVal = 0;
			this.textDaysFirstReminder.Name = "textDaysFirstReminder";
			this.textDaysFirstReminder.Size = new System.Drawing.Size(53,20);
			this.textDaysFirstReminder.TabIndex = 65;
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(151,54);
			this.textDaysFuture.MaxVal = 10000;
			this.textDaysFuture.MinVal = 0;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(53,20);
			this.textDaysFuture.TabIndex = 66;
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(151,32);
			this.textDaysPast.MaxVal = 10000;
			this.textDaysPast.MinVal = 0;
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(53,20);
			this.textDaysPast.TabIndex = 65;
			// 
			// textDown
			// 
			this.textDown.Location = new System.Drawing.Point(119,46);
			this.textDown.Name = "textDown";
			this.textDown.Size = new System.Drawing.Size(73,20);
			this.textDown.TabIndex = 6;
			// 
			// textRight
			// 
			this.textRight.Location = new System.Drawing.Point(119,21);
			this.textRight.Name = "textRight";
			this.textRight.Size = new System.Drawing.Size(73,20);
			this.textRight.TabIndex = 4;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(787,570);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(787,608);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(886,649);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listTypes);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboStatusEmailedRecall);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.comboStatusMailedRecall);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.checkReturnAdd);
			this.Controls.Add(this.textPostcardsPerSheet);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Recall and Confirmation";
			this.Load += new System.EventHandler(this.FormRecallSetup_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRecallSetup_FormClosing);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRecallSetup_Load(object sender, System.EventArgs e) {
			checkGroupFamilies.Checked = PrefC.GetBool("RecallGroupByFamily");
			textPostcardsPerSheet.Text=PrefC.GetInt("RecallPostcardsPerSheet").ToString();
			checkReturnAdd.Checked=PrefC.GetBool("RecallCardsShowReturnAdd");
			checkGroupFamilies.Checked=PrefC.GetBool("RecallGroupByFamily");
			if(PrefC.GetInt("RecallDaysPast")==-1) {
				textDaysPast.Text="";
			}
			else {
				textDaysPast.Text=PrefC.GetInt("RecallDaysPast").ToString();
			}
			if(PrefC.GetInt("RecallDaysFuture")==-1) {
				textDaysFuture.Text="";
			}
			else {
				textDaysFuture.Text=PrefC.GetInt("RecallDaysFuture").ToString();
			}
			textRight.Text=PrefC.GetDouble("RecallAdjustRight").ToString();
			textDown.Text=PrefC.GetDouble("RecallAdjustDown").ToString();
			//comboStatusMailedRecall.Items.Clear();
			for(int i=0;i<DefC.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatusMailedRecall.Items.Add(DefC.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				comboStatusEmailedRecall.Items.Add(DefC.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefC.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==PrefC.GetInt("RecallStatusMailed")){
					comboStatusMailedRecall.SelectedIndex=i;
				}
				if(DefC.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==PrefC.GetInt("RecallStatusEmailed")){
					comboStatusEmailedRecall.SelectedIndex=i;
				}
			}
			List<int> recalltypes=new List<int>();
			string[] typearray=PrefC.GetString("RecallTypesShowingInList").Split(',');
			if(typearray.Length>0){
				for(int i=0;i<typearray.Length;i++){
					recalltypes.Add(PIn.PInt(typearray[i]));
				}
			}
			for(int i=0;i<RecallTypeC.Listt.Count;i++){
				listTypes.Items.Add(RecallTypeC.Listt[i].Description);
				if(recalltypes.Contains(RecallTypeC.Listt[i].RecallTypeNum)){
					listTypes.SetSelected(i,true);
				}
			}
			if(PrefC.GetInt("RecallShowIfDaysFirstReminder")==-1) {
				textDaysFirstReminder.Text="";
			}
			else {
				textDaysFirstReminder.Text=PrefC.GetInt("RecallShowIfDaysFirstReminder").ToString();
			}
			if(PrefC.GetInt("RecallShowIfDaysSecondReminder")==-1) {
				textDaysSecondReminder.Text="";
			}
			else {
				textDaysSecondReminder.Text=PrefC.GetInt("RecallShowIfDaysSecondReminder").ToString();
			}
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g("TableRecallMsgs","Remind#"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallMsgs","Mode"),50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("",300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecallMsgs","Message"),500);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			//
			row=new ODGridRow();
			row.Cells.Add("1");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Subject line"));
			row.Cells.Add(PrefC.GetString("RecallEmailSubject"));//old
			row.Tag="RecallEmailSubject";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("1");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Available variables: [DueDate], [NameFL], [NameF]."));
			row.Cells.Add(PrefC.GetString("RecallEmailMessage"));
			row.Tag="RecallEmailMessage";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("1");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList] where the list of family members should show."));
			row.Cells.Add(PrefC.GetString("RecallEmailFamMsg"));
			row.Tag="RecallEmailFamMsg";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("1");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"Use [DueDate] wherever you want the due date to be inserted."));
			row.Cells.Add(PrefC.GetString("RecallPostcardMessage"));//old
			row.Tag="RecallPostcardMessage";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("1");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList] where the list of family members should show."));
			row.Cells.Add(PrefC.GetString("RecallPostcardFamMsg"));//old
			row.Tag="RecallPostcardFamMsg";
			gridMain.Rows.Add(row);
			//2---------------------------------------------------------------------------------------------
			//
			row=new ODGridRow();
			row.Cells.Add("2");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Subject line"));
			row.Cells.Add(PrefC.GetString("RecallEmailSubject2"));
			row.Tag="RecallEmailSubject2";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("2");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Available variables: [DueDate], [NameFL], [NameF]."));
			row.Cells.Add(PrefC.GetString("RecallEmailMessage2"));
			row.Tag="RecallEmailMessage2";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("2");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList]."));
			row.Cells.Add(PrefC.GetString("RecallEmailFamMsg2"));
			row.Tag="RecallEmailFamMsg2";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("2");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"Use [DueDate]."));
			row.Cells.Add(PrefC.GetString("RecallPostcardMessage2"));
			row.Tag="RecallPostcardMessage2";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("2");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList]."));
			row.Cells.Add(PrefC.GetString("RecallPostcardFamMsg2"));
			row.Tag="RecallPostcardFamMsg2";
			gridMain.Rows.Add(row);
			//3---------------------------------------------------------------------------------------------
			//
			row=new ODGridRow();
			row.Cells.Add("3");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Subject line"));
			row.Cells.Add(PrefC.GetString("RecallEmailSubject3"));
			row.Tag="RecallEmailSubject3";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("3");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"Available variables: [DueDate], [NameFL], [NameF]."));
			row.Cells.Add(PrefC.GetString("RecallEmailMessage3"));
			row.Tag="RecallEmailMessage3";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("3");
			row.Cells.Add(Lan.g(this,"E-mail"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList]."));
			row.Cells.Add(PrefC.GetString("RecallEmailFamMsg3"));
			row.Tag="RecallEmailFamMsg3";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("3");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"Use [DueDate]."));
			row.Cells.Add(PrefC.GetString("RecallPostcardMessage3"));
			row.Tag="RecallPostcardMessage3";
			gridMain.Rows.Add(row);
			//
			row=new ODGridRow();
			row.Cells.Add("3");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"For multiple patients in one family.  Use [FamilyList]."));
			row.Cells.Add(PrefC.GetString("RecallPostcardFamMsg3"));
			row.Tag="RecallPostcardFamMsg3";
			gridMain.Rows.Add(row);
			//Confirmation---------------------------------------------------------------------------------------------
			row=new ODGridRow();
			row.Cells.Add("");
			row.Cells.Add(Lan.g(this,"Postcard"));
			row.Cells.Add(Lan.g(this,"Confirmation message.  Use [date]  and [time] where you want those values to be inserted"));
			row.Cells.Add(PrefC.GetString("ConfirmPostcardMessage"));//old
			row.Tag="ConfirmPostcardMessage";
			gridMain.Rows.Add(row);
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			string prefName=gridMain.Rows[e.Row].Tag.ToString();
			FormRecallMessageEdit FormR=new FormRecallMessageEdit();
			FormR.MessageVal=PrefC.GetString(prefName);
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK) {
				return;
			}
			Prefs.UpdateString(prefName,FormR.MessageVal);
			//Prefs.RefreshCache();//above line handles it.
			FillGrid();
			changed=true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRight.errorProvider1.GetError(textRight)!=""
				|| textDown.errorProvider1.GetError(textDown)!=""
				|| textDaysPast.errorProvider1.GetError(textDaysPast)!=""
				|| textDaysFuture.errorProvider1.GetError(textDaysFuture)!=""
				|| textDaysFirstReminder.errorProvider1.GetError(textDaysFirstReminder)!=""
				|| textDaysSecondReminder.errorProvider1.GetError(textDaysSecondReminder)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textPostcardsPerSheet.Text!="1"
				&& textPostcardsPerSheet.Text!="3"
				&& textPostcardsPerSheet.Text!="4")
			{
				MsgBox.Show(this,"The value in postcards per sheet must be 1, 3, or 4");
				return;
			}
			if(comboStatusMailedRecall.SelectedIndex==-1 || comboStatusMailedRecall.SelectedIndex==-1){
				MsgBox.Show(this,"Both status options at the bottom must be set.");
				return; 
			}
			if(textPostcardsPerSheet.Text=="1"){
				MsgBox.Show(this,"If using 1 postcard per sheet, you must adjust the position, and also the preview will not work");
			}
			Prefs.UpdateString("RecallPostcardsPerSheet",textPostcardsPerSheet.Text);
			Prefs.UpdateBool("RecallCardsShowReturnAdd",checkReturnAdd.Checked);
			Prefs.UpdateBool("RecallGroupByFamily",checkGroupFamilies.Checked);
			if(textDaysPast.Text=="") {
				Prefs.UpdateInt("RecallDaysPast",-1);
			}
			else {
				Prefs.UpdateInt("RecallDaysPast",PIn.PInt(textDaysPast.Text));
			}
			if(textDaysFuture.Text=="") {
				Prefs.UpdateInt("RecallDaysFuture",-1);
			}
			else {
				Prefs.UpdateInt("RecallDaysFuture",PIn.PInt(textDaysFuture.Text));
			}
			Prefs.UpdateDouble("RecallAdjustRight",PIn.PDouble(textRight.Text));
			Prefs.UpdateDouble("RecallAdjustDown",PIn.PDouble(textDown.Text));
			if(comboStatusEmailedRecall.SelectedIndex==-1){
				Prefs.UpdateInt("RecallStatusEmailed",0);
			}
			else{
				Prefs.UpdateInt("RecallStatusEmailed",DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatusEmailedRecall.SelectedIndex].DefNum);
			}
			if(comboStatusMailedRecall.SelectedIndex==-1){
				Prefs.UpdateInt("RecallStatusMailed",0);
			}
			else{
				Prefs.UpdateInt("RecallStatusMailed",DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatusMailedRecall.SelectedIndex].DefNum);
			}
			string recalltypes="";
			for(int i=0;i<listTypes.SelectedIndices.Count;i++){
				if(i>0){
					recalltypes+=",";
				}
				recalltypes+=RecallTypeC.Listt[listTypes.SelectedIndices[i]].RecallTypeNum.ToString();
			}
			Prefs.UpdateString("RecallTypesShowingInList",recalltypes);
			if(textDaysFirstReminder.Text=="") {
				Prefs.UpdateInt("RecallShowIfDaysFirstReminder",-1);
			}
			else {
				Prefs.UpdateInt("RecallShowIfDaysFirstReminder",PIn.PInt(textDaysFirstReminder.Text));
			}
			if(textDaysSecondReminder.Text=="") {
				Prefs.UpdateInt("RecallShowIfDaysSecondReminder",-1);
			}
			else {
				Prefs.UpdateInt("RecallShowIfDaysSecondReminder",PIn.PInt(textDaysSecondReminder.Text));
			}
			changed=true;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormRecallSetup_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
		}

	

	


	}
}
