using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRecallListEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textWkPhone;
		private System.Windows.Forms.TextBox textWirelessPhone;
		private System.Windows.Forms.TextBox textAddrNotes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textHmPhone;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private OpenDental.ContrAccount contrAccount3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textCreditType;
		private System.Windows.Forms.TextBox textPriIns;
		private PatientNotes PatientNotes=new PatientNotes();
		///<summary>If Pin clicked, this allows FormRecall to know about it.</summary>
		public bool PinClicked=false;
		private System.Windows.Forms.TextBox textBillingType;
		private OpenDental.UI.Button butPin;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView listFamily;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private Procedure[] ProcList;
		private System.Windows.Forms.ComboBox comboStatus;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butEditRecall;
		///<summary>Only used if PinClicked=true</summary>
		public int AptSelected;
		private Recall RecallCur;
		private Patient PatCur;
		private Family FamCur;
		private OpenDental.ODtextBox textNote;
		private ComboBox comboRecallMethod;
		private Label label25;
		private TextBox textEmail;
		private Label label6;
		private InsPlan[] PlanList;

		///<summary></summary>
		public FormRecallListEdit(Recall recallCur){
			InitializeComponent();
			RecallCur=recallCur;
			Lan.F(this);
			for(int i=0;i<listFamily.Columns.Count;i++){
				listFamily.Columns[i].Text=Lan.g(this,listFamily.Columns[i].Text);
			}
		}

		///<summary></summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecallListEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.textWkPhone = new System.Windows.Forms.TextBox();
			this.textWirelessPhone = new System.Windows.Forms.TextBox();
			this.textAddrNotes = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textHmPhone = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.contrAccount3 = new OpenDental.ContrAccount();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textEmail = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.listFamily = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.textPriIns = new System.Windows.Forms.TextBox();
			this.comboRecallMethod = new System.Windows.Forms.ComboBox();
			this.label25 = new System.Windows.Forms.Label();
			this.textBillingType = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textCreditType = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.butPin = new OpenDental.UI.Button();
			this.comboStatus = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textNote = new OpenDental.ODtextBox();
			this.butEditRecall = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(852,644);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
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
			this.butOK.Location = new System.Drawing.Point(768,644);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(3,13);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(135,17);
			this.label10.TabIndex = 46;
			this.label10.Text = "Status";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textWkPhone
			// 
			this.textWkPhone.BackColor = System.Drawing.Color.White;
			this.textWkPhone.Location = new System.Drawing.Point(501,33);
			this.textWkPhone.Name = "textWkPhone";
			this.textWkPhone.Size = new System.Drawing.Size(100,20);
			this.textWkPhone.TabIndex = 41;
			this.textWkPhone.TextChanged += new System.EventHandler(this.textWkPhone_TextChanged);
			// 
			// textWirelessPhone
			// 
			this.textWirelessPhone.BackColor = System.Drawing.Color.White;
			this.textWirelessPhone.Location = new System.Drawing.Point(501,53);
			this.textWirelessPhone.Name = "textWirelessPhone";
			this.textWirelessPhone.Size = new System.Drawing.Size(100,20);
			this.textWirelessPhone.TabIndex = 40;
			this.textWirelessPhone.TextChanged += new System.EventHandler(this.textWirelessPhone_TextChanged);
			// 
			// textAddrNotes
			// 
			this.textAddrNotes.BackColor = System.Drawing.Color.White;
			this.textAddrNotes.Location = new System.Drawing.Point(501,114);
			this.textAddrNotes.Multiline = true;
			this.textAddrNotes.Name = "textAddrNotes";
			this.textAddrNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAddrNotes.Size = new System.Drawing.Size(240,48);
			this.textAddrNotes.TabIndex = 39;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(400,35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100,16);
			this.label7.TabIndex = 38;
			this.label7.Text = "Work Phone";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(398,55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Wireless Phone";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textHmPhone
			// 
			this.textHmPhone.BackColor = System.Drawing.Color.White;
			this.textHmPhone.Location = new System.Drawing.Point(501,13);
			this.textHmPhone.Name = "textHmPhone";
			this.textHmPhone.Size = new System.Drawing.Size(100,20);
			this.textHmPhone.TabIndex = 36;
			this.textHmPhone.TextChanged += new System.EventHandler(this.textHmPhone_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(396,15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(105,16);
			this.label5.TabIndex = 35;
			this.label5.Text = "Home Phone";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(405,115);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96,44);
			this.label4.TabIndex = 34;
			this.label4.Text = "Address/Phone Notes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// contrAccount3
			// 
			this.contrAccount3.Location = new System.Drawing.Point(4,164);
			this.contrAccount3.Name = "contrAccount3";
			this.contrAccount3.Size = new System.Drawing.Size(931,477);
			this.contrAccount3.TabIndex = 53;
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(6,162);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(929,32);
			this.panel1.TabIndex = 54;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textEmail);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.listFamily);
			this.groupBox1.Controls.Add(this.textPriIns);
			this.groupBox1.Controls.Add(this.comboRecallMethod);
			this.groupBox1.Controls.Add(this.label25);
			this.groupBox1.Controls.Add(this.textBillingType);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textAddrNotes);
			this.groupBox1.Controls.Add(this.textCreditType);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textWirelessPhone);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textHmPhone);
			this.groupBox1.Controls.Add(this.textWkPhone);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(179,3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(752,187);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patient Information";
			// 
			// textEmail
			// 
			this.textEmail.BackColor = System.Drawing.Color.White;
			this.textEmail.Location = new System.Drawing.Point(501,73);
			this.textEmail.Name = "textEmail";
			this.textEmail.Size = new System.Drawing.Size(240,20);
			this.textEmail.TabIndex = 105;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(398,75);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,16);
			this.label6.TabIndex = 104;
			this.label6.Text = "Email";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listFamily
			// 
			this.listFamily.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5});
			this.listFamily.GridLines = true;
			this.listFamily.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listFamily.Location = new System.Drawing.Point(7,24);
			this.listFamily.Name = "listFamily";
			this.listFamily.Size = new System.Drawing.Size(384,97);
			this.listFamily.TabIndex = 55;
			this.listFamily.UseCompatibleStateImageBehavior = false;
			this.listFamily.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Family Member";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Age";
			this.columnHeader2.Width = 40;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Gender";
			this.columnHeader4.Width = 50;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Due Date";
			this.columnHeader3.Width = 74;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Scheduled";
			this.columnHeader5.Width = 74;
			// 
			// textPriIns
			// 
			this.textPriIns.BackColor = System.Drawing.SystemColors.Control;
			this.textPriIns.Location = new System.Drawing.Point(121,153);
			this.textPriIns.Name = "textPriIns";
			this.textPriIns.ReadOnly = true;
			this.textPriIns.Size = new System.Drawing.Size(247,20);
			this.textPriIns.TabIndex = 50;
			// 
			// comboRecallMethod
			// 
			this.comboRecallMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRecallMethod.FormattingEnabled = true;
			this.comboRecallMethod.Location = new System.Drawing.Point(501,93);
			this.comboRecallMethod.MaxDropDownItems = 30;
			this.comboRecallMethod.Name = "comboRecallMethod";
			this.comboRecallMethod.Size = new System.Drawing.Size(174,21);
			this.comboRecallMethod.TabIndex = 103;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(368,95);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(132,16);
			this.label25.TabIndex = 102;
			this.label25.Text = "Prefer Recall Method";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBillingType
			// 
			this.textBillingType.BackColor = System.Drawing.SystemColors.Control;
			this.textBillingType.Location = new System.Drawing.Point(121,133);
			this.textBillingType.Name = "textBillingType";
			this.textBillingType.ReadOnly = true;
			this.textBillingType.Size = new System.Drawing.Size(120,20);
			this.textBillingType.TabIndex = 54;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(21,136);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(98,16);
			this.label14.TabIndex = 53;
			this.label14.Text = "Billing Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(20,156);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,16);
			this.label11.TabIndex = 49;
			this.label11.Text = "Primary Insurance";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCreditType
			// 
			this.textCreditType.BackColor = System.Drawing.SystemColors.Control;
			this.textCreditType.Location = new System.Drawing.Point(501,162);
			this.textCreditType.Name = "textCreditType";
			this.textCreditType.ReadOnly = true;
			this.textCreditType.Size = new System.Drawing.Size(23,20);
			this.textCreditType.TabIndex = 46;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(392,164);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107,16);
			this.label1.TabIndex = 42;
			this.label1.Text = "Credit Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.White;
			this.label13.Location = new System.Drawing.Point(474,231);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(203,15);
			this.label13.TabIndex = 56;
			this.label13.Text = "(view only mode - no editing allowed)";
			// 
			// butPin
			// 
			this.butPin.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPin.Autosize = true;
			this.butPin.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPin.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPin.CornerRadius = 4F;
			this.butPin.Image = global::OpenDental.Properties.Resources.butPin;
			this.butPin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPin.Location = new System.Drawing.Point(574,644);
			this.butPin.Name = "butPin";
			this.butPin.Size = new System.Drawing.Size(98,26);
			this.butPin.TabIndex = 57;
			this.butPin.Text = "&Pinboard";
			this.butPin.Click += new System.EventHandler(this.butPin_Click);
			// 
			// comboStatus
			// 
			this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboStatus.Location = new System.Drawing.Point(4,30);
			this.comboStatus.MaxDropDownItems = 50;
			this.comboStatus.Name = "comboStatus";
			this.comboStatus.Size = new System.Drawing.Size(166,21);
			this.comboStatus.TabIndex = 58;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textNote);
			this.groupBox2.Controls.Add(this.butEditRecall);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.comboStatus);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Location = new System.Drawing.Point(4,4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(173,199);
			this.groupBox2.TabIndex = 59;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Recall";
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(4,69);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Recall;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(165,102);
			this.textNote.TabIndex = 62;
			// 
			// butEditRecall
			// 
			this.butEditRecall.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEditRecall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEditRecall.Autosize = true;
			this.butEditRecall.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEditRecall.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEditRecall.CornerRadius = 4F;
			this.butEditRecall.Location = new System.Drawing.Point(4,172);
			this.butEditRecall.Name = "butEditRecall";
			this.butEditRecall.Size = new System.Drawing.Size(82,24);
			this.butEditRecall.TabIndex = 61;
			this.butEditRecall.Text = "Edit Recall";
			this.butEditRecall.Click += new System.EventHandler(this.butEditRecall_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2,52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(135,17);
			this.label3.TabIndex = 59;
			this.label3.Text = "Note";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormRecallListEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(937,677);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butPin);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.contrAccount3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRecallListEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Recall Status";
			this.Load += new System.EventHandler(this.FormRecallListEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRecallListEdit_Load(object sender, System.EventArgs e) {
			contrAccount3.ViewingInRecall=true;
			contrAccount3.InitializeOnStartup();
			contrAccount3.ModuleSelected(RecallCur.PatNum);
				//also refreshes these internal classes:family,patients,procedures,insplans
				//and these global classes:claims,adjustments
				//paysplits,covpats and patientnotes.
			FamCur=Patients.GetFamily(RecallCur.PatNum);
			PatCur=FamCur.GetPatient(RecallCur.PatNum);
			PlanList=InsPlans.Refresh(FamCur);
			ProcList=Procedures.Refresh(PatCur.PatNum);
			Text="Recall for "+PatCur.GetNameLF();
			textCreditType.Text=PatCur.CreditType;
			textBillingType.Text=DefB.GetName(DefCat.BillingTypes,PatCur.BillingType);
			//textPriIns.Text=InsPlans.GetDescript(PatCur.PriPlanNum,FamCur,PlanList);
      textHmPhone.Text=PatCur.HmPhone;
			textWkPhone.Text=PatCur.WkPhone;
			textWirelessPhone.Text=PatCur.WirelessPhone;
			textEmail.Text=PatCur.Email;
			textAddrNotes.Text=PatCur.AddrNote;
			for(int i=0;i<Enum.GetNames(typeof(ContactMethod)).Length;i++) {
				comboRecallMethod.Items.Add(Lan.g("enumContactMethod",Enum.GetNames(typeof(ContactMethod))[i]));
			}
			comboRecallMethod.SelectedIndex=(int)PatCur.PreferRecallMethod;
			FillRecall();
		}

		private void FillRecall(){
			comboStatus.Items.Clear();
			comboStatus.Items.Add(Lan.g(this,"None"));
			comboStatus.SelectedIndex=0;
			for(int i=0;i<DefB.Short[(int)DefCat.RecallUnschedStatus].Length;i++){
				comboStatus.Items.Add(DefB.Short[(int)DefCat.RecallUnschedStatus][i].ItemName);
				if(DefB.Short[(int)DefCat.RecallUnschedStatus][i].DefNum==RecallCur.RecallStatus)
					comboStatus.SelectedIndex=i+1;
			}
			textNote.Text=RecallCur.Note;
			//Now, the family list:
			listFamily.Items.Clear();
			Appointment[] aptsOnePat;
			ListViewItem item;
			Recall[] recallList=Recalls.GetList(FamCur.List);
			DateTime dateDue;
			for(int i=0;i<FamCur.List.Length;i++){
				item=new ListViewItem(FamCur.GetNameInFamFLI(i));
				if(FamCur.List[i].PatNum==PatCur.PatNum){
					item.BackColor=Color.Silver;
				}
				item.SubItems.Add(Shared.AgeToString(FamCur.List[i].Age));
				item.SubItems.Add(FamCur.List[i].Gender.ToString());
				dateDue=DateTime.MinValue;
				for(int j=0;j<recallList.Length;j++){
					if(recallList[j].PatNum==FamCur.List[i].PatNum){
						dateDue=recallList[j].DateDue;
					}
				}
				if(dateDue.Year<1880){
					item.SubItems.Add("");
				}
				else{
					item.SubItems.Add(dateDue.ToShortDateString());
				}
				if(dateDue<=DateTime.Today){
					item.ForeColor=Color.Red;
				}
				aptsOnePat=Appointments.GetForPat(FamCur.List[i].PatNum);
				for(int a=0;a<aptsOnePat.Length;a++){
					if(aptsOnePat[a].AptDateTime.Date<=DateTime.Today){
						continue;//disregard old appts.
					}
					item.SubItems.Add(aptsOnePat[a].AptDateTime.ToShortDateString());
					break;//we only want one appt
					//could add condition here to add blank subitem if no date found
				}
				listFamily.Items.Add(item);
				//if(Patients.FamilyList[i].PatNum==Patients.Cur.PatNum){
				//	listFamily.Items[i].Selected=true;//doesn't work
				//}
			}
		}

		private void textWirelessPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textWirelessPhone.SelectionStart;
			int length=textWirelessPhone.Text.Length;
			textWirelessPhone.Text=TelephoneNumbers.AutoFormat(textWirelessPhone.Text);
			if(textWirelessPhone.Text.Length>length)
				cursor++;
			textWirelessPhone.SelectionStart=cursor;
		}

		private void textWkPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;
		}

		private void textHmPhone_TextChanged(object sender,System.EventArgs e) {
			int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length)
				cursor++;
			textHmPhone.SelectionStart=cursor;
		}

		private void butEditRecall_Click(object sender, System.EventArgs e) {
			FormRecallEdit FormRE=new FormRecallEdit(PatCur);
			FormRE.RecallCur=RecallCur;
			FormRE.ShowDialog();
			FillRecall();
		}

		///<summary>Called from Pin_Click and OK_Click.</summary>
		private void SaveRecall(){
			int newStatus;
			if(comboStatus.SelectedIndex==0){
				newStatus=0;
			}
			else{
				newStatus=DefB.Short[(int)DefCat.RecallUnschedStatus][comboStatus.SelectedIndex-1].DefNum;
			}
			if(newStatus!=RecallCur.RecallStatus//if the status has changed
				|| (RecallCur.Note=="" && textNote.Text!=""))//or a note was added
			{
				//make a commlog entry
				//unless there is an existing recall commlog entry for today
				bool recallEntryToday=false;
				Commlog[] CommlogList=Commlogs.Refresh(PatCur.PatNum);
				for(int i=0;i<CommlogList.Length;i++){
					if(CommlogList[i].CommDateTime.Date==DateTime.Today	
						&& CommlogList[i].CommType==Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL))
					{
						recallEntryToday=true;
					}
				}
				if(!recallEntryToday){
					Commlog CommlogCur=new Commlog();
					CommlogCur.CommDateTime=DateTime.Now;
					CommlogCur.CommType=Commlogs.GetTypeAuto(CommItemTypeAuto.RECALL);
					CommlogCur.PatNum=PatCur.PatNum;
					if(newStatus!=RecallCur.RecallStatus){
						//Commlogs.Cur.Note+=Lan.g(this,"Status changed to")+" ";
						if(newStatus==0)
							CommlogCur.Note+=Lan.g(this,"Status None");
						else
							CommlogCur.Note+=DefB.GetName(DefCat.RecallUnschedStatus,newStatus);
						if(RecallCur.Note=="" && textNote.Text!="")
							CommlogCur.Note+=", ";
					}
					if(RecallCur.Note=="" && textNote.Text!=""){
						CommlogCur.Note+=textNote.Text;
					}
					CommlogCur.Note+=".  ";
					FormCommItem FormCI=new FormCommItem(CommlogCur);
					FormCI.IsNew=true;
					//forces user to at least consider a commlog entry
					FormCI.ShowDialog();//typically saved in this window.
				}
			}
			RecallCur.RecallStatus=newStatus;
			RecallCur.Note=textNote.Text;
			Recalls.Update(RecallCur);
			Patient PatOld=PatCur.Copy();
			PatCur.WkPhone=textWkPhone.Text;
			PatCur.WirelessPhone=textWirelessPhone.Text;
			PatCur.Email=textEmail.Text;
			PatCur.PreferRecallMethod=(ContactMethod)comboRecallMethod.SelectedIndex;
			Patients.Update(PatCur,PatOld);
			Patients.UpdatePhoneAndNoteIfNeeded(textHmPhone.Text,textAddrNotes.Text,PatCur.PatNum);
		}

		private void butPin_Click(object sender, System.EventArgs e) {
			SaveRecall();
			PinClicked=true;
			Appointment apt=Appointments.CreateRecallApt(PatCur,ProcList,RecallCur,PlanList);
			AptSelected=apt.AptNum;
			DialogResult=DialogResult.OK;
		}

		/*
		// <summary>Creates appointment and appropriate procedures, and places data in ContrAppt.CurInfo so it will display on pinboard.</summary>
		private void CreateCurInfo() {
			MessageBox.Show("Not functional");
			ContrAppt.CurInfo=new InfoApt();
			Appointment AptCur=Appointments.CreateRecallApt(PatCur,ProcList,RecallCur,PlanList);
			ContrAppt.CurInfo.MyApt=AptCur.Copy();
			Procedure[] procs=Procedures.GetProcsForSingle(AptCur.AptNum,false);
			ContrAppt.CurInfo.Procs=procs;
			ContrAppt.CurInfo.Production=Procedures.GetProductionOneApt(AptCur.AptNum,procs,false);
			ContrAppt.CurInfo.MyPatient=PatCur;
		}*/

		private void butOK_Click(object sender, System.EventArgs e){
			SaveRecall();
			//?Patients.PatIsLoaded=false;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

	






	}
}
