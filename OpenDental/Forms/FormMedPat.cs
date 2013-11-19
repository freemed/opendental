using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormMedPat : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelGenericName;
		private System.Windows.Forms.Label labelGenericNotes;
		private System.Windows.Forms.Label labelPatNote;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelEdit;
		private System.Windows.Forms.TextBox textMedName;
		private System.Windows.Forms.TextBox textGenericName;
		private System.Windows.Forms.TextBox textMedNote;
		private OpenDental.UI.Button butRemove;
		private OpenDental.UI.Button butEdit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.ODtextBox textPatNote;
		///<summary></summary>
		public bool IsNew;
		private GroupBox groupOrder;
		private ValidDate textDateStop;
		private Label label7;
		private ValidDate textDateStart;
		private Label label4;
		private UI.Button butTodayStop;
		private UI.Button butTodayStart;
		private Label label8;
		private ComboBox comboProv;
		public MedicationPat MedicationPatCur;
		private TextBox textRxNormDesc;
		private Label label9;
		private UI.Button butRxNormSelect;
		///<summary>Helps enforce the note and start date that must be present for a med order to be valid.</summary>
		public bool IsNewMedOrder;

		///<summary></summary>
		public FormMedPat()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedPat));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textMedName = new System.Windows.Forms.TextBox();
			this.textGenericName = new System.Windows.Forms.TextBox();
			this.labelGenericName = new System.Windows.Forms.Label();
			this.textMedNote = new System.Windows.Forms.TextBox();
			this.labelGenericNotes = new System.Windows.Forms.Label();
			this.labelPatNote = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butRxNormSelect = new OpenDental.UI.Button();
			this.textRxNormDesc = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.labelEdit = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
			this.butRemove = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.textPatNote = new OpenDental.ODtextBox();
			this.groupOrder = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.butTodayStop = new OpenDental.UI.Button();
			this.butTodayStart = new OpenDental.UI.Button();
			this.textDateStop = new OpenDental.ValidDate();
			this.label7 = new System.Windows.Forms.Label();
			this.textDateStart = new OpenDental.ValidDate();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupOrder.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(563, 457);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(461, 457);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(38, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Drug Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textMedName
			// 
			this.textMedName.Location = new System.Drawing.Point(183, 33);
			this.textMedName.Name = "textMedName";
			this.textMedName.ReadOnly = true;
			this.textMedName.Size = new System.Drawing.Size(348, 20);
			this.textMedName.TabIndex = 3;
			// 
			// textGenericName
			// 
			this.textGenericName.Location = new System.Drawing.Point(183, 55);
			this.textGenericName.Name = "textGenericName";
			this.textGenericName.ReadOnly = true;
			this.textGenericName.Size = new System.Drawing.Size(348, 20);
			this.textGenericName.TabIndex = 5;
			// 
			// labelGenericName
			// 
			this.labelGenericName.Location = new System.Drawing.Point(19, 58);
			this.labelGenericName.Name = "labelGenericName";
			this.labelGenericName.Size = new System.Drawing.Size(163, 17);
			this.labelGenericName.TabIndex = 4;
			this.labelGenericName.Text = "Generic Name";
			this.labelGenericName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textMedNote
			// 
			this.textMedNote.Location = new System.Drawing.Point(183, 77);
			this.textMedNote.Multiline = true;
			this.textMedNote.Name = "textMedNote";
			this.textMedNote.ReadOnly = true;
			this.textMedNote.Size = new System.Drawing.Size(348, 87);
			this.textMedNote.TabIndex = 7;
			// 
			// labelGenericNotes
			// 
			this.labelGenericNotes.Location = new System.Drawing.Point(8, 81);
			this.labelGenericNotes.Name = "labelGenericNotes";
			this.labelGenericNotes.Size = new System.Drawing.Size(174, 17);
			this.labelGenericNotes.TabIndex = 6;
			this.labelGenericNotes.Text = "Medication Notes";
			this.labelGenericNotes.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPatNote
			// 
			this.labelPatNote.Location = new System.Drawing.Point(6, 44);
			this.labelPatNote.Name = "labelPatNote";
			this.labelPatNote.Size = new System.Drawing.Size(175, 43);
			this.labelPatNote.TabIndex = 8;
			this.labelPatNote.Text = "Notes for this Patient";
			this.labelPatNote.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butRxNormSelect);
			this.groupBox1.Controls.Add(this.textRxNormDesc);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.labelEdit);
			this.groupBox1.Controls.Add(this.butEdit);
			this.groupBox1.Controls.Add(this.textMedNote);
			this.groupBox1.Controls.Add(this.textMedName);
			this.groupBox1.Controls.Add(this.textGenericName);
			this.groupBox1.Controls.Add(this.labelGenericName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.labelGenericNotes);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(36, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(565, 198);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Medication";
			// 
			// butRxNormSelect
			// 
			this.butRxNormSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRxNormSelect.Autosize = true;
			this.butRxNormSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRxNormSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRxNormSelect.CornerRadius = 4F;
			this.butRxNormSelect.Location = new System.Drawing.Point(533, 11);
			this.butRxNormSelect.Name = "butRxNormSelect";
			this.butRxNormSelect.Size = new System.Drawing.Size(22, 22);
			this.butRxNormSelect.TabIndex = 65;
			this.butRxNormSelect.Text = "...";
			this.butRxNormSelect.Visible = false;
			this.butRxNormSelect.Click += new System.EventHandler(this.butRxNormSelect_Click);
			// 
			// textRxNormDesc
			// 
			this.textRxNormDesc.Location = new System.Drawing.Point(183, 12);
			this.textRxNormDesc.Name = "textRxNormDesc";
			this.textRxNormDesc.ReadOnly = true;
			this.textRxNormDesc.Size = new System.Drawing.Size(348, 20);
			this.textRxNormDesc.TabIndex = 12;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(55, 15);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(127, 17);
			this.label9.TabIndex = 13;
			this.label9.Text = "RxNorm";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelEdit
			// 
			this.labelEdit.Location = new System.Drawing.Point(266, 167);
			this.labelEdit.Name = "labelEdit";
			this.labelEdit.Size = new System.Drawing.Size(128, 28);
			this.labelEdit.TabIndex = 11;
			this.labelEdit.Text = "(edit this medication for all patients)";
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Location = new System.Drawing.Point(183, 167);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(75, 24);
			this.butEdit.TabIndex = 9;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// butRemove
			// 
			this.butRemove.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butRemove.Autosize = true;
			this.butRemove.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemove.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemove.CornerRadius = 4F;
			this.butRemove.Location = new System.Drawing.Point(49, 457);
			this.butRemove.Name = "butRemove";
			this.butRemove.Size = new System.Drawing.Size(75, 24);
			this.butRemove.TabIndex = 8;
			this.butRemove.Text = "&Remove";
			this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Location = new System.Drawing.Point(20, 485);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(127, 43);
			this.label5.TabIndex = 10;
			this.label5.Text = "(remove this medication from this patient)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textPatNote
			// 
			this.textPatNote.AcceptsTab = true;
			this.textPatNote.DetectUrls = false;
			this.textPatNote.Location = new System.Drawing.Point(183, 44);
			this.textPatNote.Name = "textPatNote";
			this.textPatNote.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationPat;
			this.textPatNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textPatNote.Size = new System.Drawing.Size(348, 111);
			this.textPatNote.TabIndex = 11;
			this.textPatNote.Text = "";
			// 
			// groupOrder
			// 
			this.groupOrder.Controls.Add(this.label8);
			this.groupOrder.Controls.Add(this.comboProv);
			this.groupOrder.Controls.Add(this.butTodayStop);
			this.groupOrder.Controls.Add(this.butTodayStart);
			this.groupOrder.Controls.Add(this.textDateStop);
			this.groupOrder.Controls.Add(this.label7);
			this.groupOrder.Controls.Add(this.textDateStart);
			this.groupOrder.Controls.Add(this.label4);
			this.groupOrder.Controls.Add(this.labelPatNote);
			this.groupOrder.Controls.Add(this.textPatNote);
			this.groupOrder.Location = new System.Drawing.Point(36, 217);
			this.groupOrder.Name = "groupOrder";
			this.groupOrder.Size = new System.Drawing.Size(565, 215);
			this.groupOrder.TabIndex = 64;
			this.groupOrder.TabStop = false;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(65, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(116, 17);
			this.label8.TabIndex = 32;
			this.label8.Text = "Provider";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProv
			// 
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.FormattingEnabled = true;
			this.comboProv.Location = new System.Drawing.Point(183, 19);
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(207, 21);
			this.comboProv.TabIndex = 31;
			// 
			// butTodayStop
			// 
			this.butTodayStop.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTodayStop.Autosize = true;
			this.butTodayStop.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayStop.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTodayStop.CornerRadius = 4F;
			this.butTodayStop.Location = new System.Drawing.Point(288, 183);
			this.butTodayStop.Name = "butTodayStop";
			this.butTodayStop.Size = new System.Drawing.Size(64, 23);
			this.butTodayStop.TabIndex = 17;
			this.butTodayStop.Text = "Today";
			this.butTodayStop.Click += new System.EventHandler(this.butTodayStop_Click);
			// 
			// butTodayStart
			// 
			this.butTodayStart.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butTodayStart.Autosize = true;
			this.butTodayStart.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butTodayStart.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butTodayStart.CornerRadius = 4F;
			this.butTodayStart.Location = new System.Drawing.Point(288, 159);
			this.butTodayStart.Name = "butTodayStart";
			this.butTodayStart.Size = new System.Drawing.Size(64, 23);
			this.butTodayStart.TabIndex = 16;
			this.butTodayStart.Text = "Today";
			this.butTodayStart.Click += new System.EventHandler(this.butTodayStart_Click);
			// 
			// textDateStop
			// 
			this.textDateStop.Location = new System.Drawing.Point(183, 185);
			this.textDateStop.Name = "textDateStop";
			this.textDateStop.Size = new System.Drawing.Size(100, 20);
			this.textDateStop.TabIndex = 15;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(18, 186);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(163, 17);
			this.label7.TabIndex = 14;
			this.label7.Text = "Date Stop";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDateStart
			// 
			this.textDateStart.Location = new System.Drawing.Point(183, 160);
			this.textDateStart.Name = "textDateStart";
			this.textDateStart.Size = new System.Drawing.Size(100, 20);
			this.textDateStart.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18, 161);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(163, 17);
			this.label4.TabIndex = 12;
			this.label4.Text = "Date Start";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormMedPat
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(685, 523);
			this.Controls.Add(this.groupOrder);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butRemove);
			this.Controls.Add(this.label5);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedPat";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medication for Patient";
			this.Load += new System.EventHandler(this.FormMedPat_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupOrder.ResumeLayout(false);
			this.groupOrder.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedPat_Load(object sender, System.EventArgs e) {
			if(PrefC.GetBool(PrefName.ShowFeatureEhr)) {
				labelPatNote.Text="Count, Instructions, and Refills";
				groupOrder.Text="Medication Order";
			}
			//Formulary checks now handled in NewCrop
			//else {
			//	butFormulary.Visible=false;
			//}
			textRxNormDesc.Text=RxNorms.GetDescByRxCui(MedicationPatCur.RxCui.ToString());
			if(MedicationPatCur.MedicationNum==0) {//Probably a medical order created during NewCrop prescription fetching, but not necessarily.
				textMedName.Text=MedicationPatCur.MedDescript;
				labelGenericNotes.Visible=false;
				labelGenericName.Visible=false;
				textGenericName.Visible=false;
				textMedNote.Visible=false;
				butEdit.Visible=false;
				labelEdit.Visible=false;
			}
			else {
				textMedName.Text=Medications.GetMedication(MedicationPatCur.MedicationNum).MedName;
				textGenericName.Text=Medications.GetGeneric(MedicationPatCur.MedicationNum).MedName;
				textMedNote.Text=Medications.GetGeneric(MedicationPatCur.MedicationNum).Notes;
			}
			if(!String.IsNullOrEmpty(MedicationPatCur.NewCropGuid)) {//This is a medical order that was automatically created when fetching prescriptions from NewCrop.
				//We allow the user to change the RxCui on the medical order, because NewCrop does not always supply an RxCui.
				//Also the MedicaitonNum on the order will always be 0, so there is no link between the order and the medication table.
				//If MedicationOrder was non-zero and we changed the RxCui on the order, then the automatic sync between medicationpat.RxCui and medication.RxCui would be tainted.
				butRxNormSelect.Visible=true;
			}
			comboProv.Items.Add(Lan.g(this,"none"));
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProv.Items.Add(ProviderC.ListShort[i].GetLongDesc());
			}
			if(MedicationPatCur.ProvNum==0){
				comboProv.SelectedIndex=0;
			}
			else{
				for(int i=0;i<ProviderC.ListShort.Count;i++) {
					if(MedicationPatCur.ProvNum==ProviderC.ListShort[i].ProvNum) {
						comboProv.SelectedIndex=i+1;
					}
				}
				//if a provider was subsequently hidden, then the combobox may now be -1.
			}
			if(MedicationPatCur.IsCpoe) {
				//We cannot allow the user to change the provider, because our EHR reports use the provider in combination with the IsCpoe flag to report CPOE.
				comboProv.Enabled=false;
			}
			textPatNote.Text=MedicationPatCur.PatNote;
			if(MedicationPatCur.DateStart.Year>1880) {
				textDateStart.Text=MedicationPatCur.DateStart.ToShortDateString();
			}
			if(MedicationPatCur.DateStop.Year>1880) {
				textDateStop.Text=MedicationPatCur.DateStop.ToShortDateString();
			}
		}

		private void butTodayStart_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
		}

		private void butTodayStop_Click(object sender,EventArgs e) {
			textDateStop.Text=DateTime.Today.ToShortDateString();
		}

		//New Crop now handles formulary checks
		/*private void butFormulary_Click(object sender,EventArgs e) {
			FormFormularies FormF=new FormFormularies();
			FormF.IsSelectionMode=true;
			FormF.ShowDialog();
			Cursor=Cursors.WaitCursor;
			if(FormF.DialogResult!=DialogResult.OK) {
				Cursor=Cursors.Default;
				return;
			}
			List<FormularyMed> ListMeds=FormularyMeds.GetMedsForFormulary(FormF.SelectedFormularyNum);
			bool medIsInFormulary=false;
			for(int i=0;i<ListMeds.Count;i++) {
				if(ListMeds[i].MedicationNum==MedicationPatCur.MedicationNum) {
					medIsInFormulary=true;
				}
			}
			Cursor=Cursors.Default;
			if(medIsInFormulary){
				MsgBox.Show(this,"This medication is in the selected formulary.");
			}
			else {
				MsgBox.Show(this,"This medication is not in the selected forumulary.");
			}
		}*/

		private void butRxNormSelect_Click(object sender,EventArgs e) {
			FormRxNorms FormRN=new FormRxNorms();
			FormRN.IsSelectionMode=true;
			FormRN.ShowDialog();
			if(FormRN.DialogResult!=DialogResult.OK) {
				return;
			}
			MedicationPatCur.RxCui=PIn.Long(FormRN.SelectedRxNorm.RxCui);
			textRxNormDesc.Text=RxNorms.GetDescByRxCui(MedicationPatCur.RxCui.ToString());
			if(IsNew) {
				textMedName.Text=RxNorms.GetDescByRxCui(MedicationPatCur.RxCui.ToString());
			}
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			Medications.Refresh();
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.MedicationCur=Medications.GetMedication(MedicationPatCur.MedicationNum);//The edit button is not visible if MedicationNum=0.
			FormME.ShowDialog();
			if(FormME.DialogResult!=DialogResult.OK){
				return;
			}
			MedicationPatCur.RxCui=FormME.MedicationCur.RxCui;
			textMedName.Text=Medications.GetMedication(MedicationPatCur.MedicationNum).MedName;//The edit button is not visible if MedicationNum=0.
			textGenericName.Text=Medications.GetGeneric(MedicationPatCur.MedicationNum).MedName;//The edit button is not visible if MedicationNum=0.
			textMedNote.Text=Medications.GetGeneric(MedicationPatCur.MedicationNum).Notes;//The edit button is not visible if MedicationNum=0.
			textRxNormDesc.Text=RxNorms.GetDescByRxCui(MedicationPatCur.RxCui.ToString());
		}

		private void butRemove_Click(object sender, System.EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Remove this medication from this patient?  Patient notes will be lost.")){
				return;
			}
			MedicationPats.Delete(MedicationPatCur);
			if(MedicationPatCur.MedicationNum==0) {
				SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,MedicationPatCur.MedDescript+" deleted");
			}
			else {
				SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,Medications.GetMedication(MedicationPatCur.MedicationNum).MedName+" deleted");
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(IsNewMedOrder) {
				if(textPatNote.Text=="" || textDateStart.Text=="") {
					MsgBox.Show(this,"For a new medical order, instructions and a start date are required.");
					return;
				}
			}
			//MedicationPatCur.MedicationNum is already set before entering this window, or else changed up above.
			if(comboProv.SelectedIndex==-1) {
				//don't make any changes to provnum.  ProvNum is a hidden prov.
			}
			else if(comboProv.SelectedIndex==0){
				MedicationPatCur.ProvNum=0;
			}
			else {
				MedicationPatCur.ProvNum=ProviderC.ListShort[comboProv.SelectedIndex-1].ProvNum;
			}
			MedicationPatCur.PatNote=textPatNote.Text;
			MedicationPatCur.DateStart=PIn.Date(textDateStart.Text);
			MedicationPatCur.DateStop=PIn.Date(textDateStop.Text);
			if(IsNew){
				MedicationPats.Insert(MedicationPatCur);
				if(MedicationPatCur.MedicationNum==0) {
					SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,MedicationPatCur.MedDescript+" added");
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,Medications.GetMedication(MedicationPatCur.MedicationNum).MedName+" added");
				}
			}
			else{
				MedicationPats.Update(MedicationPatCur);
				if(MedicationPatCur.MedicationNum==0) {
					SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,MedicationPatCur.MedDescript+" edited");
				}
				else {
					SecurityLogs.MakeLogEntry(Permissions.PatMedicationListEdit,MedicationPatCur.PatNum,Medications.GetMedication(MedicationPatCur.MedicationNum).MedName+" edited");
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















