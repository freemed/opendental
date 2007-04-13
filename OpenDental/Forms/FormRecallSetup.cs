using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRecallSetup : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPostcardsPerSheet;
		private System.Windows.Forms.ListBox listProcs;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBW;
		private System.Windows.Forms.TextBox textProcs;
		private System.Windows.Forms.TextBox textPattern;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label9;
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
		private ValidNum textDaysPast;
		private ValidNum textDaysFuture;
		private Label label15;
		private GroupBox groupBox3;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRecallSetup(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[] {
				textBox1,
				textBox6
			});
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textPostcardsPerSheet = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listProcs = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBW = new System.Windows.Forms.TextBox();
			this.textProcs = new System.Windows.Forms.TextBox();
			this.textPattern = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.checkReturnAdd = new System.Windows.Forms.CheckBox();
			this.textConfirmPostcardMessage = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textPostcardFamMsg = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textDown = new OpenDental.ValidDouble();
			this.label12 = new System.Windows.Forms.Label();
			this.textRight = new OpenDental.ValidDouble();
			this.label13 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textDaysPast = new OpenDental.ValidNum();
			this.textDaysFuture = new OpenDental.ValidNum();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(47,21);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(500,30);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "The following information is used to automate the process of creating recall appo" +
    "intments from the recall list.  You can make changes to the appointment after it" +
    " has been created.";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.SystemColors.Control;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(23,161);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(672,20);
			this.textBox6.TabIndex = 15;
			this.textBox6.Text = "For now, children under 12 do not have their procedures automatically attached.  " +
    "Their appointments are created blank.";
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(176,212);
			this.textPostcardMessage.MaxLength = 255;
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(466,70);
			this.textPostcardMessage.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8,215);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167,61);
			this.label7.TabIndex = 17;
			this.label7.Text = "Recall Postcard message.  Use ?DueDate wherever you want the due date to be inser" +
    "ted.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(176,434);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34,20);
			this.textPostcardsPerSheet.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(49,437);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127,31);
			this.label8.TabIndex = 19;
			this.label8.Text = "Postcards per sheet (1,3,or 4)";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProcs
			// 
			this.listProcs.BackColor = System.Drawing.SystemColors.Control;
			this.listProcs.Location = new System.Drawing.Point(176,519);
			this.listProcs.Name = "listProcs";
			this.listProcs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listProcs.Size = new System.Drawing.Size(130,82);
			this.listProcs.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBW);
			this.groupBox1.Controls.Add(this.textProcs);
			this.groupBox1.Controls.Add(this.textPattern);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.textBox6);
			this.groupBox1.Location = new System.Drawing.Point(16,13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(737,193);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			// 
			// textBW
			// 
			this.textBW.Location = new System.Drawing.Point(159,128);
			this.textBW.Name = "textBW";
			this.textBW.Size = new System.Drawing.Size(100,20);
			this.textBW.TabIndex = 15;
			// 
			// textProcs
			// 
			this.textProcs.Location = new System.Drawing.Point(159,84);
			this.textProcs.Multiline = true;
			this.textProcs.Name = "textProcs";
			this.textProcs.Size = new System.Drawing.Size(336,42);
			this.textProcs.TabIndex = 13;
			// 
			// textPattern
			// 
			this.textPattern.Location = new System.Drawing.Point(159,62);
			this.textPattern.Name = "textPattern";
			this.textPattern.Size = new System.Drawing.Size(170,20);
			this.textPattern.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(266,129);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(445,15);
			this.label6.TabIndex = 20;
			this.label6.Text = "(leave blank to disable automated BW\'s)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(499,85);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(216,34);
			this.label5.TabIndex = 19;
			this.label5.Text = "(valid codes separated by commas)";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(340,64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(255,19);
			this.label4.TabIndex = 18;
			this.label4.Text = "(must contain only /\'s and X\'s)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1,131);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156,16);
			this.label3.TabIndex = 17;
			this.label3.Text = "BiteWings code every year";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14,87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(143,16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Procedures";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140,16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Time Pattern";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(20,518);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(151,83);
			this.label9.TabIndex = 22;
			this.label9.Text = "Procedures that Trigger Recall - You can change these in procedure code setup";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkReturnAdd
			// 
			this.checkReturnAdd.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReturnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReturnAdd.Location = new System.Drawing.Point(6,467);
			this.checkReturnAdd.Name = "checkReturnAdd";
			this.checkReturnAdd.Size = new System.Drawing.Size(184,19);
			this.checkReturnAdd.TabIndex = 43;
			this.checkReturnAdd.Text = "Show return address";
			this.checkReturnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textConfirmPostcardMessage
			// 
			this.textConfirmPostcardMessage.AcceptsReturn = true;
			this.textConfirmPostcardMessage.Location = new System.Drawing.Point(176,358);
			this.textConfirmPostcardMessage.MaxLength = 255;
			this.textConfirmPostcardMessage.Multiline = true;
			this.textConfirmPostcardMessage.Name = "textConfirmPostcardMessage";
			this.textConfirmPostcardMessage.Size = new System.Drawing.Size(466,70);
			this.textConfirmPostcardMessage.TabIndex = 44;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4,361);
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
			this.textPostcardFamMsg.Location = new System.Drawing.Point(176,285);
			this.textPostcardFamMsg.MaxLength = 255;
			this.textPostcardFamMsg.Multiline = true;
			this.textPostcardFamMsg.Name = "textPostcardFamMsg";
			this.textPostcardFamMsg.Size = new System.Drawing.Size(466,70);
			this.textPostcardFamMsg.TabIndex = 46;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8,288);
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
			this.groupBox2.Location = new System.Drawing.Point(344,432);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(204,74);
			this.groupBox2.TabIndex = 48;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Adjust Postcard Position in Inches";
			// 
			// textDown
			// 
			this.textDown.Location = new System.Drawing.Point(113,47);
			this.textDown.Name = "textDown";
			this.textDown.Size = new System.Drawing.Size(73,20);
			this.textDown.TabIndex = 6;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(49,46);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60,20);
			this.label12.TabIndex = 5;
			this.label12.Text = "Down";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRight
			// 
			this.textRight.Location = new System.Drawing.Point(113,22);
			this.textRight.Name = "textRight";
			this.textRight.Size = new System.Drawing.Size(73,20);
			this.textRight.TabIndex = 4;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(49,21);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(60,20);
			this.label13.TabIndex = 4;
			this.label13.Text = "Right";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(678,538);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
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
			this.butCancel.Location = new System.Drawing.Point(678,576);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkGroupFamilies
			// 
			this.checkGroupFamilies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.Location = new System.Drawing.Point(6,15);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(121,18);
			this.checkGroupFamilies.TabIndex = 49;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6,34);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(101,20);
			this.label14.TabIndex = 50;
			this.label14.Text = "Days Past";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(113,35);
			this.textDaysPast.MaxVal = 10000;
			this.textDaysPast.MinVal = -10000;
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(53,20);
			this.textDaysPast.TabIndex = 51;
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(113,57);
			this.textDaysFuture.MaxVal = 10000;
			this.textDaysFuture.MinVal = -10000;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(53,20);
			this.textDaysFuture.TabIndex = 53;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(9,56);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(98,20);
			this.label15.TabIndex = 52;
			this.label15.Text = "Days Future";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textDaysFuture);
			this.groupBox3.Controls.Add(this.checkGroupFamilies);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.textDaysPast);
			this.groupBox3.Location = new System.Drawing.Point(344,514);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(204,87);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Recall List Default View";
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(777,617);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.textPostcardFamMsg);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textConfirmPostcardMessage);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.checkReturnAdd);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textPostcardsPerSheet);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textPostcardMessage);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listProcs);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setup Recall and Confirmation";
			this.Load += new System.EventHandler(this.FormRecallSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRecallSetup_Load(object sender, System.EventArgs e) {
			textPattern.Text=PrefB.GetString("RecallPattern");
			textProcs.Text=((Pref)PrefB.HList["RecallProcedures"]).ValueString;
			textBW.Text=((Pref)PrefB.HList["RecallBW"]).ValueString;
			textPostcardMessage.Text=PrefB.GetString("RecallPostcardMessage");
			textPostcardFamMsg.Text=PrefB.GetString("RecallPostcardFamMsg");
			textConfirmPostcardMessage.Text=PrefB.GetString("ConfirmPostcardMessage");
			textPostcardsPerSheet.Text=PrefB.GetInt("RecallPostcardsPerSheet").ToString();
			checkReturnAdd.Checked=PrefB.GetBool("RecallCardsShowReturnAdd");
			checkGroupFamilies.Checked=PrefB.GetBool("RecallGroupByFamily");
			textDaysPast.Text=PrefB.GetInt("RecallDaysPast").ToString();
			textDaysFuture.Text=PrefB.GetInt("RecallDaysFuture").ToString();
			textRight.Text=PrefB.GetDouble("RecallAdjustRight").ToString();
			textDown.Text=PrefB.GetDouble("RecallAdjustDown").ToString();
			listProcs.Items.Clear();
			for(int i=0;i<ProcedureCodes.RecallAL.Count;i++){
				listProcs.Items.Add(((ProcedureCode)ProcedureCodes.RecallAL[i]).Descript);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textRight.errorProvider1.GetError(textRight)!=""
				|| textDown.errorProvider1.GetError(textDown)!=""
				|| textDaysPast.errorProvider1.GetError(textDaysPast)!=""
				|| textDaysFuture.errorProvider1.GetError(textDaysFuture)!="") 
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

			Prefs.UpdateString("RecallPattern",textPattern.Text);
			
			Prefs.UpdateString("RecallProcedures",textProcs.Text);

			Prefs.UpdateString("RecallBW",textBW.Text);

			Prefs.UpdateString("RecallPostcardMessage",textPostcardMessage.Text);
			
			Prefs.UpdateString("RecallPostcardFamMsg",textPostcardFamMsg.Text);

			Prefs.UpdateString("ConfirmPostcardMessage",textConfirmPostcardMessage.Text);

			Prefs.UpdateString("RecallPostcardsPerSheet",textPostcardsPerSheet.Text);

			Prefs.UpdateBool("RecallCardsShowReturnAdd",checkReturnAdd.Checked);

			Prefs.UpdateBool("RecallGroupByFamily",checkGroupFamilies.Checked);

			Prefs.UpdateInt("RecallDaysPast",PIn.PInt(textDaysPast.Text));
			Prefs.UpdateInt("RecallDaysFuture",PIn.PInt(textDaysFuture.Text));

			Prefs.UpdateDouble("RecallAdjustRight",PIn.PDouble(textRight.Text));
			Prefs.UpdateDouble("RecallAdjustDown",PIn.PDouble(textDown.Text));
			
			DataValid.SetInvalid(InvalidTypes.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
