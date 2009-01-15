using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRecallSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPostcardsPerSheet;
		private System.Windows.Forms.CheckBox checkReturnAdd;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textConfirmPostcardMessage;
		private TextBox textPostcardFamMsg;
		private Label label11;
		private GroupBox groupBox2;
		private ValidDouble textDown;
		private Label label12;
		private ValidDouble textRight;
		private Label label13;
		private CheckBox checkGroupFamilies;
		private Label label14;
		private Label label15;
		private GroupBox groupBox3;
		private TextBox textEmailSubject;
		private Label label23;
		private Label label25;
		private ComboBox comboStatusMailedRecall;
		private ComboBox comboStatusEmailedRecall;
		private Label label26;
		private ListBox listTypes;
		private Label label1;
		private ValidNumber textDaysPast;
		private ValidNumber textDaysFuture;
		private GroupBox groupBox1;
		private ValidNumber textDaysThirdReminder;
		private Label label4;
		private ValidNumber textDaysSecondReminder;
		private ValidNumber textDaysFirstReminder;
		private Label label2;
		private Label label3;
		private Label label5;
		private GroupBox groupInitialReminder;
		private TextBox textBox3;
		private Label label16;
		private Label label6;
		private TextBox textBox1;
		private TextBox textBox2;
		private Label label9;
		private GroupBox groupBox4;
		private TextBox textBox4;
		private Label label17;
		private Label label18;
		private TextBox textBox5;
		private TextBox textBox6;
		private Label label19;
		private ValidNumber validNumber1;
		private Label label20;
		private System.ComponentModel.Container components = null;

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
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textPostcardsPerSheet = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.checkReturnAdd = new System.Windows.Forms.CheckBox();
			this.textConfirmPostcardMessage = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPostcardFamMsg = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textEmailSubject = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.comboStatusMailedRecall = new System.Windows.Forms.ComboBox();
			this.comboStatusEmailedRecall = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.listTypes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textDaysThirdReminder = new OpenDental.ValidNumber();
			this.textDaysSecondReminder = new OpenDental.ValidNumber();
			this.textDaysFirstReminder = new OpenDental.ValidNumber();
			this.textDaysFuture = new OpenDental.ValidNumber();
			this.textDaysPast = new OpenDental.ValidNumber();
			this.textDown = new OpenDental.ValidDouble();
			this.textRight = new OpenDental.ValidDouble();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupInitialReminder = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.validNumber1 = new OpenDental.ValidNumber();
			this.label20 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupInitialReminder.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(275,462);
			this.textPostcardMessage.MaxLength = 255;
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(305,63);
			this.textPostcardMessage.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(108,462);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167,61);
			this.label7.TabIndex = 17;
			this.label7.Text = "Recall Postcard or E-mail message.  Use ?DueDate wherever you want the due date t" +
    "o be inserted.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(275,642);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34,20);
			this.textPostcardsPerSheet.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(148,645);
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
			this.checkReturnAdd.Location = new System.Drawing.Point(141,672);
			this.checkReturnAdd.Name = "checkReturnAdd";
			this.checkReturnAdd.Size = new System.Drawing.Size(147,19);
			this.checkReturnAdd.TabIndex = 43;
			this.checkReturnAdd.Text = "Show return address";
			this.checkReturnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textConfirmPostcardMessage
			// 
			this.textConfirmPostcardMessage.AcceptsReturn = true;
			this.textConfirmPostcardMessage.Location = new System.Drawing.Point(204,827);
			this.textConfirmPostcardMessage.MaxLength = 255;
			this.textConfirmPostcardMessage.Multiline = true;
			this.textConfirmPostcardMessage.Name = "textConfirmPostcardMessage";
			this.textConfirmPostcardMessage.Size = new System.Drawing.Size(305,63);
			this.textConfirmPostcardMessage.TabIndex = 44;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(34,827);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(170,61);
			this.label10.TabIndex = 45;
			this.label10.Text = "Confirmation Postcard message.  Use ?date  and ?time where you want those values " +
    "to be inserted";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardFamMsg
			// 
			this.textPostcardFamMsg.AcceptsReturn = true;
			this.textPostcardFamMsg.Location = new System.Drawing.Point(275,528);
			this.textPostcardFamMsg.MaxLength = 255;
			this.textPostcardFamMsg.Multiline = true;
			this.textPostcardFamMsg.Name = "textPostcardFamMsg";
			this.textPostcardFamMsg.Size = new System.Drawing.Size(305,63);
			this.textPostcardFamMsg.TabIndex = 46;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(108,528);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(167,61);
			this.label11.TabIndex = 47;
			this.label11.Text = "Recall Postcard message for multiple patients in one family.  Use ?FamilyList whe" +
    "re the list of family members should show.";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textDown);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.textRight);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Location = new System.Drawing.Point(536,598);
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
			this.checkGroupFamilies.Location = new System.Drawing.Point(32,15);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(121,18);
			this.checkGroupFamilies.TabIndex = 49;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(4,32);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(133,20);
			this.label14.TabIndex = 50;
			this.label14.Text = "Days Past (usually blank)";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(39,53);
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
			this.groupBox3.Location = new System.Drawing.Point(536,678);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(205,78);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Recall List Default View";
			// 
			// textEmailSubject
			// 
			this.textEmailSubject.Location = new System.Drawing.Point(275,439);
			this.textEmailSubject.Name = "textEmailSubject";
			this.textEmailSubject.Size = new System.Drawing.Size(305,20);
			this.textEmailSubject.TabIndex = 55;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(108,442);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(167,16);
			this.label23.TabIndex = 56;
			this.label23.Text = "E-mail Subject";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(116,598);
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
			this.comboStatusMailedRecall.Location = new System.Drawing.Point(275,594);
			this.comboStatusMailedRecall.MaxDropDownItems = 20;
			this.comboStatusMailedRecall.Name = "comboStatusMailedRecall";
			this.comboStatusMailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusMailedRecall.TabIndex = 58;
			// 
			// comboStatusEmailedRecall
			// 
			this.comboStatusEmailedRecall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEmailedRecall.FormattingEnabled = true;
			this.comboStatusEmailedRecall.Location = new System.Drawing.Point(275,618);
			this.comboStatusEmailedRecall.MaxDropDownItems = 20;
			this.comboStatusEmailedRecall.Name = "comboStatusEmailedRecall";
			this.comboStatusEmailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusEmailedRecall.TabIndex = 60;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(116,622);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(157,16);
			this.label26.TabIndex = 59;
			this.label26.Text = "Status for e-mailed recall";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listTypes
			// 
			this.listTypes.FormattingEnabled = true;
			this.listTypes.Location = new System.Drawing.Point(275,697);
			this.listTypes.Name = "listTypes";
			this.listTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listTypes.Size = new System.Drawing.Size(120,108);
			this.listTypes.TabIndex = 64;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(118,699);
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
			this.groupBox1.Controls.Add(this.textDaysThirdReminder);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textDaysSecondReminder);
			this.groupBox1.Controls.Add(this.textDaysFirstReminder);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(536,763);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(205,107);
			this.groupBox1.TabIndex = 65;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Also show in list if # of days since";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(36,19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101,20);
			this.label2.TabIndex = 50;
			this.label2.Text = "First Reminder";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(39,41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98,20);
			this.label3.TabIndex = 52;
			this.label3.Text = "Second Reminder";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3,63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134,20);
			this.label4.TabIndex = 67;
			this.label4.Text = "Third (or more) Reminder";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4,85);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(201,17);
			this.label5.TabIndex = 69;
			this.label5.Text = "(a very large number is recommended)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textDaysThirdReminder
			// 
			this.textDaysThirdReminder.Location = new System.Drawing.Point(139,64);
			this.textDaysThirdReminder.MaxVal = 10000;
			this.textDaysThirdReminder.MinVal = 0;
			this.textDaysThirdReminder.Name = "textDaysThirdReminder";
			this.textDaysThirdReminder.Size = new System.Drawing.Size(53,20);
			this.textDaysThirdReminder.TabIndex = 68;
			// 
			// textDaysSecondReminder
			// 
			this.textDaysSecondReminder.Location = new System.Drawing.Point(139,42);
			this.textDaysSecondReminder.MaxVal = 10000;
			this.textDaysSecondReminder.MinVal = 0;
			this.textDaysSecondReminder.Name = "textDaysSecondReminder";
			this.textDaysSecondReminder.Size = new System.Drawing.Size(53,20);
			this.textDaysSecondReminder.TabIndex = 66;
			// 
			// textDaysFirstReminder
			// 
			this.textDaysFirstReminder.Location = new System.Drawing.Point(139,20);
			this.textDaysFirstReminder.MaxVal = 10000;
			this.textDaysFirstReminder.MinVal = 0;
			this.textDaysFirstReminder.Name = "textDaysFirstReminder";
			this.textDaysFirstReminder.Size = new System.Drawing.Size(53,20);
			this.textDaysFirstReminder.TabIndex = 65;
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(139,54);
			this.textDaysFuture.MaxVal = 10000;
			this.textDaysFuture.MinVal = 0;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(53,20);
			this.textDaysFuture.TabIndex = 66;
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(139,32);
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
			this.butOK.Location = new System.Drawing.Point(1141,813);
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
			this.butCancel.Location = new System.Drawing.Point(1141,851);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupInitialReminder
			// 
			this.groupInitialReminder.Controls.Add(this.textBox3);
			this.groupInitialReminder.Controls.Add(this.label16);
			this.groupInitialReminder.Controls.Add(this.label6);
			this.groupInitialReminder.Controls.Add(this.textBox1);
			this.groupInitialReminder.Controls.Add(this.textBox2);
			this.groupInitialReminder.Controls.Add(this.label9);
			this.groupInitialReminder.Location = new System.Drawing.Point(12,12);
			this.groupInitialReminder.Name = "groupInitialReminder";
			this.groupInitialReminder.Size = new System.Drawing.Size(478,174);
			this.groupInitialReminder.TabIndex = 66;
			this.groupInitialReminder.TabStop = false;
			this.groupInitialReminder.Text = "Initial Reminder";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6,20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(167,16);
			this.label6.TabIndex = 60;
			this.label6.Text = "E-mail Subject";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(173,17);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(298,20);
			this.textBox1.TabIndex = 59;
			// 
			// textBox2
			// 
			this.textBox2.AcceptsReturn = true;
			this.textBox2.Location = new System.Drawing.Point(173,40);
			this.textBox2.MaxLength = 255;
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(298,63);
			this.textBox2.TabIndex = 57;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6,40);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(167,61);
			this.label9.TabIndex = 58;
			this.label9.Text = "E-mail message.\r\nUse ?DueDate wherever you want the due date to be inserted.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox3
			// 
			this.textBox3.AcceptsReturn = true;
			this.textBox3.Location = new System.Drawing.Point(173,105);
			this.textBox3.MaxLength = 255;
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(298,63);
			this.textBox3.TabIndex = 61;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6,105);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(167,61);
			this.label16.TabIndex = 62;
			this.label16.Text = "Postcard message.\r\nUse ?DueDate wherever you want the due date to be inserted.";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.validNumber1);
			this.groupBox4.Controls.Add(this.label20);
			this.groupBox4.Controls.Add(this.textBox4);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.textBox5);
			this.groupBox4.Controls.Add(this.textBox6);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Location = new System.Drawing.Point(12,192);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(478,196);
			this.groupBox4.TabIndex = 67;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Second Reminder";
			// 
			// textBox4
			// 
			this.textBox4.AcceptsReturn = true;
			this.textBox4.Location = new System.Drawing.Point(173,127);
			this.textBox4.MaxLength = 255;
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(298,63);
			this.textBox4.TabIndex = 61;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(6,127);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(167,61);
			this.label17.TabIndex = 62;
			this.label17.Text = "Postcard message.\r\nUse ?DueDate wherever you want the due date to be inserted.";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6,42);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(167,16);
			this.label18.TabIndex = 60;
			this.label18.Text = "E-mail Subject";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(173,39);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(298,20);
			this.textBox5.TabIndex = 59;
			// 
			// textBox6
			// 
			this.textBox6.AcceptsReturn = true;
			this.textBox6.Location = new System.Drawing.Point(173,62);
			this.textBox6.MaxLength = 255;
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(298,63);
			this.textBox6.TabIndex = 57;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(6,62);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(167,61);
			this.label19.TabIndex = 58;
			this.label19.Text = "E-mail message.\r\nUse ?DueDate wherever you want the due date to be inserted.";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// validNumber1
			// 
			this.validNumber1.Location = new System.Drawing.Point(418,17);
			this.validNumber1.MaxVal = 10000;
			this.validNumber1.MinVal = 0;
			this.validNumber1.Name = "validNumber1";
			this.validNumber1.Size = new System.Drawing.Size(53,20);
			this.validNumber1.TabIndex = 67;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(107,16);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(309,20);
			this.label20.TabIndex = 66;
			this.label20.Text = "Show in list if # of days since initial reminder";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(1240,892);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupInitialReminder);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listTypes);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboStatusEmailedRecall);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.comboStatusMailedRecall);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.textEmailSubject);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textPostcardFamMsg);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textConfirmPostcardMessage);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.checkReturnAdd);
			this.Controls.Add(this.textPostcardsPerSheet);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textPostcardMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label7);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Recall and Confirmation";
			this.Load += new System.EventHandler(this.FormRecallSetup_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupInitialReminder.ResumeLayout(false);
			this.groupInitialReminder.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRecallSetup_Load(object sender, System.EventArgs e) {
			checkGroupFamilies.Checked = PrefC.GetBool("RecallGroupByFamily");
			textEmailSubject.Text = PrefC.GetString("RecallEmailSubject");
			textPostcardMessage.Text = PrefC.GetString("RecallPostcardMessage");
			textPostcardFamMsg.Text=PrefC.GetString("RecallPostcardFamMsg");
			textConfirmPostcardMessage.Text=PrefC.GetString("ConfirmPostcardMessage");
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
			if(PrefC.GetInt("RecallShowIfDaysThirdReminder")==-1) {
				textDaysThirdReminder.Text="";
			}
			else {
				textDaysThirdReminder.Text=PrefC.GetInt("RecallShowIfDaysThirdReminder").ToString();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRight.errorProvider1.GetError(textRight)!=""
				|| textDown.errorProvider1.GetError(textDown)!=""
				|| textDaysPast.errorProvider1.GetError(textDaysPast)!=""
				|| textDaysFuture.errorProvider1.GetError(textDaysFuture)!=""
				|| textDaysFirstReminder.errorProvider1.GetError(textDaysFirstReminder)!=""
				|| textDaysSecondReminder.errorProvider1.GetError(textDaysSecondReminder)!=""
				|| textDaysThirdReminder.errorProvider1.GetError(textDaysThirdReminder)!="") 
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
			Prefs.UpdateString("RecallEmailSubject",textEmailSubject.Text);
			Prefs.UpdateString("RecallPostcardMessage",textPostcardMessage.Text);		
			Prefs.UpdateString("RecallPostcardFamMsg",textPostcardFamMsg.Text);
			Prefs.UpdateString("ConfirmPostcardMessage",textConfirmPostcardMessage.Text);
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
			if(textDaysThirdReminder.Text=="") {
				Prefs.UpdateInt("RecallShowIfDaysThirdReminder",-1);
			}
			else {
				Prefs.UpdateInt("RecallShowIfDaysThirdReminder",PIn.PInt(textDaysThirdReminder.Text));
			}
			DataValid.SetInvalid(InvalidType.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	


	}
}
