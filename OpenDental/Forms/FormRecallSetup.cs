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
		private ValidNum textDaysPast;
		private ValidNum textDaysFuture;
		private Label label15;
		private GroupBox groupBox3;
		private TextBox textEmailSubject;
		private Label label23;
		private Label label25;
		private ComboBox comboStatusMailedRecall;
		private ComboBox comboStatusEmailedRecall;
		private Label label26;
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
			this.textDaysFuture = new OpenDental.ValidNum();
			this.textDaysPast = new OpenDental.ValidNum();
			this.textDown = new OpenDental.ValidDouble();
			this.textRight = new OpenDental.ValidDouble();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(174,35);
			this.textPostcardMessage.MaxLength = 255;
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(466,63);
			this.textPostcardMessage.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(7,35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167,61);
			this.label7.TabIndex = 17;
			this.label7.Text = "Recall Postcard or E-mail message.  Use ?DueDate wherever you want the due date t" +
    "o be inserted.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(174,283);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34,20);
			this.textPostcardsPerSheet.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(47,286);
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
			this.checkReturnAdd.Location = new System.Drawing.Point(40,313);
			this.checkReturnAdd.Name = "checkReturnAdd";
			this.checkReturnAdd.Size = new System.Drawing.Size(147,19);
			this.checkReturnAdd.TabIndex = 43;
			this.checkReturnAdd.Text = "Show return address";
			this.checkReturnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textConfirmPostcardMessage
			// 
			this.textConfirmPostcardMessage.AcceptsReturn = true;
			this.textConfirmPostcardMessage.Location = new System.Drawing.Point(174,167);
			this.textConfirmPostcardMessage.MaxLength = 255;
			this.textConfirmPostcardMessage.Multiline = true;
			this.textConfirmPostcardMessage.Name = "textConfirmPostcardMessage";
			this.textConfirmPostcardMessage.Size = new System.Drawing.Size(466,63);
			this.textConfirmPostcardMessage.TabIndex = 44;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(4,167);
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
			this.textPostcardFamMsg.Location = new System.Drawing.Point(174,101);
			this.textPostcardFamMsg.MaxLength = 255;
			this.textPostcardFamMsg.Multiline = true;
			this.textPostcardFamMsg.Name = "textPostcardFamMsg";
			this.textPostcardFamMsg.Size = new System.Drawing.Size(466,63);
			this.textPostcardFamMsg.TabIndex = 46;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(7,101);
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
			this.groupBox2.Location = new System.Drawing.Point(450,236);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(190,74);
			this.groupBox2.TabIndex = 48;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Adjust Postcard Position in Inches";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(34,46);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60,20);
			this.label12.TabIndex = 5;
			this.label12.Text = "Down";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(34,21);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(60,20);
			this.label13.TabIndex = 4;
			this.label13.Text = "Right";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.label14.Location = new System.Drawing.Point(6,32);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(101,20);
			this.label14.TabIndex = 50;
			this.label14.Text = "Days Past";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(9,53);
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
			this.groupBox3.Location = new System.Drawing.Point(61,338);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(242,78);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Recall List Default View";
			// 
			// textEmailSubject
			// 
			this.textEmailSubject.Location = new System.Drawing.Point(174,12);
			this.textEmailSubject.Name = "textEmailSubject";
			this.textEmailSubject.Size = new System.Drawing.Size(466,20);
			this.textEmailSubject.TabIndex = 55;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(7,15);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(167,16);
			this.label23.TabIndex = 56;
			this.label23.Text = "E-mail Subject";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(15,239);
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
			this.comboStatusMailedRecall.Location = new System.Drawing.Point(174,235);
			this.comboStatusMailedRecall.MaxDropDownItems = 20;
			this.comboStatusMailedRecall.Name = "comboStatusMailedRecall";
			this.comboStatusMailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusMailedRecall.TabIndex = 58;
			// 
			// comboStatusEmailedRecall
			// 
			this.comboStatusEmailedRecall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatusEmailedRecall.FormattingEnabled = true;
			this.comboStatusEmailedRecall.Location = new System.Drawing.Point(174,259);
			this.comboStatusEmailedRecall.MaxDropDownItems = 20;
			this.comboStatusEmailedRecall.Name = "comboStatusEmailedRecall";
			this.comboStatusEmailedRecall.Size = new System.Drawing.Size(206,21);
			this.comboStatusEmailedRecall.TabIndex = 60;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(15,263);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(157,16);
			this.label26.TabIndex = 59;
			this.label26.Text = "Status for e-mailed recall";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(113,54);
			this.textDaysFuture.MaxVal = 10000;
			this.textDaysFuture.MinVal = -10000;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(53,20);
			this.textDaysFuture.TabIndex = 53;
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(113,33);
			this.textDaysPast.MaxVal = 10000;
			this.textDaysPast.MinVal = -10000;
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(53,20);
			this.textDaysPast.TabIndex = 51;
			// 
			// textDown
			// 
			this.textDown.Location = new System.Drawing.Point(98,47);
			this.textDown.Name = "textDown";
			this.textDown.Size = new System.Drawing.Size(73,20);
			this.textDown.TabIndex = 6;
			// 
			// textRight
			// 
			this.textRight.Location = new System.Drawing.Point(98,22);
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
			this.butOK.Location = new System.Drawing.Point(638,369);
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
			this.butCancel.Location = new System.Drawing.Point(638,407);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(737,448);
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
			textDaysPast.Text=PrefC.GetInt("RecallDaysPast").ToString();
			textDaysFuture.Text=PrefC.GetInt("RecallDaysFuture").ToString();
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
			if(comboStatusMailedRecall.SelectedIndex==-1 || comboStatusMailedRecall.SelectedIndex==-1){
				MsgBox.Show(this,"Both status options at the bottom must be set.");
				return; 
			}

			Prefs.UpdateString("RecallEmailSubject",textEmailSubject.Text);
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

			Prefs.UpdateInt("RecallStatusEmailed",DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatusEmailedRecall.SelectedIndex].DefNum);
			Prefs.UpdateInt("RecallStatusMailed",DefC.Short[(int)DefCat.RecallUnschedStatus][comboStatusMailedRecall.SelectedIndex].DefNum);
			
			DataValid.SetInvalid(InvalidType.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	


	}
}
