using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormMedical : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butAdd;
		private OpenDental.ODtextBox textMedical;
		private OpenDental.ODtextBox textService;
		private OpenDental.ODtextBox textMedicalComp;
		private OpenDental.ODtextBox textMedUrgNote;
		private IContainer components;
		private OpenDental.UI.Button butAddDisease;// Required designer variable.
		private Patient PatCur;
		private OpenDental.UI.ODGrid gridMeds;
		private OpenDental.UI.ODGrid gridDiseases;
		private CheckBox checkPremed;
		private List<Disease> DiseaseList;
		private CheckBox checkDiscontinued;
		private ODGrid gridAllergies;
		private UI.Button butAddAllergy;
		private PatientNote PatientNoteCur;
		private CheckBox checkShowInactiveAllergies;
		private List<Allergy> allergyList;
		private UI.Button butPrint;
		private List<MedicationPat> medList;
		private int pagesPrinted;
		private PrintDocument pd;
		private bool headingPrinted;
		private UI.Button butPrintMedical;
		private Label label1;
		private CheckBox checkShowInactiveProblems;
		private ImageList imageListInfoButton;
		private ODGrid gridFamilyHealth;
		private UI.Button butAddFamilyHistory;
		private List<FamilyHealth> ListFamHealth;
		private int headingPrintH;
		private GroupBox groupMedsDocumented;
		private RadioButton radioMedsDocumentedNo;
		private RadioButton radioMedsDocumentedYes;
		private long _EhrMeasureEventNum;
		private long _EhrNotPerfNum;



		///<summary></summary>
		public FormMedical(PatientNote patientNoteCur,Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			PatientNoteCur=patientNoteCur;
			Lan.F(this);
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedical));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textMedUrgNote = new OpenDental.ODtextBox();
			this.textService = new OpenDental.ODtextBox();
			this.textMedical = new OpenDental.ODtextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.textMedicalComp = new OpenDental.ODtextBox();
			this.butAddDisease = new OpenDental.UI.Button();
			this.gridMeds = new OpenDental.UI.ODGrid();
			this.gridDiseases = new OpenDental.UI.ODGrid();
			this.checkPremed = new System.Windows.Forms.CheckBox();
			this.checkDiscontinued = new System.Windows.Forms.CheckBox();
			this.gridAllergies = new OpenDental.UI.ODGrid();
			this.butAddAllergy = new OpenDental.UI.Button();
			this.checkShowInactiveAllergies = new System.Windows.Forms.CheckBox();
			this.butPrint = new OpenDental.UI.Button();
			this.butPrintMedical = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.checkShowInactiveProblems = new System.Windows.Forms.CheckBox();
			this.imageListInfoButton = new System.Windows.Forms.ImageList(this.components);
			this.gridFamilyHealth = new OpenDental.UI.ODGrid();
			this.butAddFamilyHistory = new OpenDental.UI.Button();
			this.groupMedsDocumented = new System.Windows.Forms.GroupBox();
			this.radioMedsDocumentedYes = new System.Windows.Forms.RadioButton();
			this.radioMedsDocumentedNo = new System.Windows.Forms.RadioButton();
			this.groupMedsDocumented.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(786, 650);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 0;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(879, 650);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textMedUrgNote
			// 
			this.textMedUrgNote.AcceptsTab = true;
			this.textMedUrgNote.DetectUrls = false;
			this.textMedUrgNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textMedUrgNote.ForeColor = System.Drawing.Color.Red;
			this.textMedUrgNote.Location = new System.Drawing.Point(115, 447);
			this.textMedUrgNote.Name = "textMedUrgNote";
			this.textMedUrgNote.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalUrgent;
			this.textMedUrgNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textMedUrgNote.Size = new System.Drawing.Size(252, 33);
			this.textMedUrgNote.TabIndex = 53;
			this.textMedUrgNote.Text = "";
			// 
			// textService
			// 
			this.textService.AcceptsTab = true;
			this.textService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textService.DetectUrls = false;
			this.textService.Location = new System.Drawing.Point(115, 558);
			this.textService.Name = "textService";
			this.textService.QuickPasteType = OpenDentBusiness.QuickPasteType.ServiceNotes;
			this.textService.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textService.Size = new System.Drawing.Size(252, 83);
			this.textService.TabIndex = 52;
			this.textService.Text = "";
			// 
			// textMedical
			// 
			this.textMedical.AcceptsTab = true;
			this.textMedical.DetectUrls = false;
			this.textMedical.Location = new System.Drawing.Point(115, 482);
			this.textMedical.Name = "textMedical";
			this.textMedical.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalSummary;
			this.textMedical.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textMedical.Size = new System.Drawing.Size(252, 74);
			this.textMedical.TabIndex = 51;
			this.textMedical.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 559);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106, 16);
			this.label3.TabIndex = 50;
			this.label3.Text = "Service Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 445);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 21);
			this.label2.TabIndex = 49;
			this.label2.Text = "Med Urgent";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 483);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(106, 17);
			this.label4.TabIndex = 47;
			this.label4.Text = "Medical Summary";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(371, 407);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(421, 18);
			this.label6.TabIndex = 6;
			this.label6.Text = "Medical History - Complete and Detailed (does not show in chart)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(374, 1);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(123, 23);
			this.butAdd.TabIndex = 51;
			this.butAdd.Text = "&Add Medication";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// textMedicalComp
			// 
			this.textMedicalComp.AcceptsTab = true;
			this.textMedicalComp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textMedicalComp.DetectUrls = false;
			this.textMedicalComp.Location = new System.Drawing.Point(374, 428);
			this.textMedicalComp.Name = "textMedicalComp";
			this.textMedicalComp.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalHistory;
			this.textMedicalComp.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textMedicalComp.Size = new System.Drawing.Size(585, 213);
			this.textMedicalComp.TabIndex = 54;
			this.textMedicalComp.Text = "";
			// 
			// butAddDisease
			// 
			this.butAddDisease.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butAddDisease.Autosize = true;
			this.butAddDisease.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddDisease.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddDisease.CornerRadius = 4F;
			this.butAddDisease.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddDisease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddDisease.Location = new System.Drawing.Point(5, 1);
			this.butAddDisease.Name = "butAddDisease";
			this.butAddDisease.Size = new System.Drawing.Size(98, 23);
			this.butAddDisease.TabIndex = 58;
			this.butAddDisease.Text = "Add Problem";
			this.butAddDisease.Click += new System.EventHandler(this.butAddProblem_Click);
			// 
			// gridMeds
			// 
			this.gridMeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMeds.HScrollVisible = false;
			this.gridMeds.Location = new System.Drawing.Point(374, 26);
			this.gridMeds.Name = "gridMeds";
			this.gridMeds.ScrollValue = 0;
			this.gridMeds.Size = new System.Drawing.Size(585, 216);
			this.gridMeds.TabIndex = 59;
			this.gridMeds.Title = "Medications";
			this.gridMeds.TranslationName = "TableMedications";
			this.gridMeds.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMeds_CellDoubleClick);
			this.gridMeds.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMeds_CellClick);
			// 
			// gridDiseases
			// 
			this.gridDiseases.HScrollVisible = false;
			this.gridDiseases.Location = new System.Drawing.Point(4, 26);
			this.gridDiseases.Name = "gridDiseases";
			this.gridDiseases.ScrollValue = 0;
			this.gridDiseases.Size = new System.Drawing.Size(363, 216);
			this.gridDiseases.TabIndex = 60;
			this.gridDiseases.Title = "Problems";
			this.gridDiseases.TranslationName = "TableDiseases";
			this.gridDiseases.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDiseases_CellDoubleClick);
			this.gridDiseases.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDiseases_CellClick);
			// 
			// checkPremed
			// 
			this.checkPremed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPremed.Location = new System.Drawing.Point(6, 407);
			this.checkPremed.Name = "checkPremed";
			this.checkPremed.Size = new System.Drawing.Size(123, 35);
			this.checkPremed.TabIndex = 61;
			this.checkPremed.Text = "Premedicate (PAC or other)";
			this.checkPremed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPremed.UseVisualStyleBackColor = true;
			// 
			// checkDiscontinued
			// 
			this.checkDiscontinued.Location = new System.Drawing.Point(523, 3);
			this.checkDiscontinued.Name = "checkDiscontinued";
			this.checkDiscontinued.Size = new System.Drawing.Size(201, 23);
			this.checkDiscontinued.TabIndex = 61;
			this.checkDiscontinued.Tag = "";
			this.checkDiscontinued.Text = "Show Discontinued Medications";
			this.checkDiscontinued.UseVisualStyleBackColor = true;
			this.checkDiscontinued.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkDiscontinued_KeyUp);
			this.checkDiscontinued.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkShowDiscontinuedMeds_MouseUp);
			// 
			// gridAllergies
			// 
			this.gridAllergies.HScrollVisible = false;
			this.gridAllergies.Location = new System.Drawing.Point(4, 278);
			this.gridAllergies.Name = "gridAllergies";
			this.gridAllergies.ScrollValue = 0;
			this.gridAllergies.Size = new System.Drawing.Size(363, 123);
			this.gridAllergies.TabIndex = 63;
			this.gridAllergies.Title = "Allergies";
			this.gridAllergies.TranslationName = "TableDiseases";
			this.gridAllergies.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAllergies_CellDoubleClick);
			this.gridAllergies.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAllergies_CellClick);
			// 
			// butAddAllergy
			// 
			this.butAddAllergy.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butAddAllergy.Autosize = true;
			this.butAddAllergy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAllergy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAllergy.CornerRadius = 4F;
			this.butAddAllergy.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAllergy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAllergy.Location = new System.Drawing.Point(5, 249);
			this.butAddAllergy.Name = "butAddAllergy";
			this.butAddAllergy.Size = new System.Drawing.Size(98, 23);
			this.butAddAllergy.TabIndex = 64;
			this.butAddAllergy.Text = "Add Allergy";
			this.butAddAllergy.Click += new System.EventHandler(this.butAddAllergy_Click);
			// 
			// checkShowInactiveAllergies
			// 
			this.checkShowInactiveAllergies.Location = new System.Drawing.Point(129, 250);
			this.checkShowInactiveAllergies.Name = "checkShowInactiveAllergies";
			this.checkShowInactiveAllergies.Size = new System.Drawing.Size(201, 23);
			this.checkShowInactiveAllergies.TabIndex = 65;
			this.checkShowInactiveAllergies.Tag = "";
			this.checkShowInactiveAllergies.Text = "Show Inactive Allergies";
			this.checkShowInactiveAllergies.UseVisualStyleBackColor = true;
			this.checkShowInactiveAllergies.CheckedChanged += new System.EventHandler(this.checkShowInactiveAllergies_CheckedChanged);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(843, 1);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(116, 24);
			this.butPrint.TabIndex = 66;
			this.butPrint.Text = "Print Medications";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butPrintMedical
			// 
			this.butPrintMedical.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPrintMedical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrintMedical.Autosize = true;
			this.butPrintMedical.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrintMedical.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrintMedical.CornerRadius = 4F;
			this.butPrintMedical.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrintMedical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrintMedical.Location = new System.Drawing.Point(9, 651);
			this.butPrintMedical.Name = "butPrintMedical";
			this.butPrintMedical.Size = new System.Drawing.Size(112, 24);
			this.butPrintMedical.TabIndex = 67;
			this.butPrintMedical.Text = "Print Medical";
			this.butPrintMedical.Click += new System.EventHandler(this.butPrintMedical_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(126, 655);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(241, 16);
			this.label1.TabIndex = 68;
			this.label1.Text = "To print medications, use button at UR";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkShowInactiveProblems
			// 
			this.checkShowInactiveProblems.Location = new System.Drawing.Point(129, 3);
			this.checkShowInactiveProblems.Name = "checkShowInactiveProblems";
			this.checkShowInactiveProblems.Size = new System.Drawing.Size(201, 23);
			this.checkShowInactiveProblems.TabIndex = 65;
			this.checkShowInactiveProblems.Tag = "";
			this.checkShowInactiveProblems.Text = "Show Inactive Problems";
			this.checkShowInactiveProblems.UseVisualStyleBackColor = true;
			this.checkShowInactiveProblems.CheckedChanged += new System.EventHandler(this.checkShowInactiveProblems_CheckedChanged);
			// 
			// imageListInfoButton
			// 
			this.imageListInfoButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListInfoButton.ImageStream")));
			this.imageListInfoButton.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListInfoButton.Images.SetKeyName(0, "iButton_16px.png");
			// 
			// gridFamilyHealth
			// 
			this.gridFamilyHealth.HScrollVisible = false;
			this.gridFamilyHealth.Location = new System.Drawing.Point(374, 278);
			this.gridFamilyHealth.Name = "gridFamilyHealth";
			this.gridFamilyHealth.ScrollValue = 0;
			this.gridFamilyHealth.Size = new System.Drawing.Size(418, 123);
			this.gridFamilyHealth.TabIndex = 69;
			this.gridFamilyHealth.Title = "Family Health History";
			this.gridFamilyHealth.TranslationName = "TableDiseases";
			this.gridFamilyHealth.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFamilyHealth_CellDoubleClick);
			// 
			// butAddFamilyHistory
			// 
			this.butAddFamilyHistory.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butAddFamilyHistory.Autosize = true;
			this.butAddFamilyHistory.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddFamilyHistory.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddFamilyHistory.CornerRadius = 4F;
			this.butAddFamilyHistory.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddFamilyHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddFamilyHistory.Location = new System.Drawing.Point(374, 249);
			this.butAddFamilyHistory.Name = "butAddFamilyHistory";
			this.butAddFamilyHistory.Size = new System.Drawing.Size(137, 23);
			this.butAddFamilyHistory.TabIndex = 70;
			this.butAddFamilyHistory.Text = "Add Family History";
			this.butAddFamilyHistory.Click += new System.EventHandler(this.butAddFamilyHistory_Click);
			// 
			// groupMedsDocumented
			// 
			this.groupMedsDocumented.Controls.Add(this.radioMedsDocumentedNo);
			this.groupMedsDocumented.Controls.Add(this.radioMedsDocumentedYes);
			this.groupMedsDocumented.Location = new System.Drawing.Point(800, 248);
			this.groupMedsDocumented.Name = "groupMedsDocumented";
			this.groupMedsDocumented.Size = new System.Drawing.Size(159, 43);
			this.groupMedsDocumented.TabIndex = 72;
			this.groupMedsDocumented.TabStop = false;
			this.groupMedsDocumented.Text = "Current Meds Documented";
			// 
			// radioMedsDocumentedYes
			// 
			this.radioMedsDocumentedYes.Location = new System.Drawing.Point(23, 19);
			this.radioMedsDocumentedYes.Name = "radioMedsDocumentedYes";
			this.radioMedsDocumentedYes.Size = new System.Drawing.Size(66, 17);
			this.radioMedsDocumentedYes.TabIndex = 0;
			this.radioMedsDocumentedYes.TabStop = true;
			this.radioMedsDocumentedYes.Text = "Yes";
			this.radioMedsDocumentedYes.UseVisualStyleBackColor = true;
			// 
			// radioMedsDocumentedNo
			// 
			this.radioMedsDocumentedNo.Location = new System.Drawing.Point(93, 19);
			this.radioMedsDocumentedNo.Name = "radioMedsDocumentedNo";
			this.radioMedsDocumentedNo.Size = new System.Drawing.Size(60, 17);
			this.radioMedsDocumentedNo.TabIndex = 1;
			this.radioMedsDocumentedNo.TabStop = true;
			this.radioMedsDocumentedNo.Text = "No";
			this.radioMedsDocumentedNo.UseVisualStyleBackColor = true;
			// 
			// FormMedical
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(964, 683);
			this.Controls.Add(this.groupMedsDocumented);
			this.Controls.Add(this.butAddFamilyHistory);
			this.Controls.Add(this.gridFamilyHealth);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butPrintMedical);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.checkShowInactiveProblems);
			this.Controls.Add(this.checkShowInactiveAllergies);
			this.Controls.Add(this.butAddAllergy);
			this.Controls.Add(this.gridAllergies);
			this.Controls.Add(this.checkDiscontinued);
			this.Controls.Add(this.checkPremed);
			this.Controls.Add(this.gridDiseases);
			this.Controls.Add(this.gridMeds);
			this.Controls.Add(this.butAddDisease);
			this.Controls.Add(this.textMedUrgNote);
			this.Controls.Add(this.textService);
			this.Controls.Add(this.textMedicalComp);
			this.Controls.Add(this.textMedical);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedical";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medical";
			this.Load += new System.EventHandler(this.FormMedical_Load);
			this.groupMedsDocumented.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedical_Load(object sender, System.EventArgs e){
			SecurityLogs.MakeLogEntry(Permissions.MedicalInfoViewed,PatCur.PatNum,"Patient medical information viewed");
			checkPremed.Checked=PatCur.Premed;
			textMedUrgNote.Text=PatCur.MedUrgNote;
			textMedical.Text=PatientNoteCur.Medical;
			textMedicalComp.Text=PatientNoteCur.MedicalComp;
			textService.Text=PatientNoteCur.Service;
			FillMeds();
			FillProblems();
			FillAllergies();
			FillFamilyHealth();
			List<EhrMeasureEvent> listDocumentedMedEvents=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.CurrentMedsDocumented);
			_EhrMeasureEventNum=0;
			for(int i=0;i<listDocumentedMedEvents.Count;i++) {
				if(listDocumentedMedEvents[i].DateTEvent.Date==DateTime.Today) {
					radioMedsDocumentedYes.Checked=true;
					_EhrMeasureEventNum=listDocumentedMedEvents[i].EhrMeasureEventNum;
					break;
				}
			}
			_EhrNotPerfNum=0;
			List<EhrNotPerformed> listNotPerfs=EhrNotPerformeds.Refresh(PatCur.PatNum);
			for(int i=0;i<listNotPerfs.Count;i++) {
				if(listNotPerfs[i].CodeValue!="428191000124101") {//this is the only allowed code for Current Meds Documented procedure
					continue;
				}
				if(listNotPerfs[i].DateEntry.Date==DateTime.Today) {
					radioMedsDocumentedNo.Checked=!radioMedsDocumentedYes.Checked;//only check the No radio button if the Yes radio button is not already set
					_EhrNotPerfNum=listNotPerfs[i].EhrNotPerformedNum;
					break;
				}
			}
		}

		private void FillMeds() {
			Medications.Refresh();
			medList=MedicationPats.Refresh(PatCur.PatNum,checkDiscontinued.Checked);
			gridMeds.BeginUpdate();
			gridMeds.Columns.Clear();
			ODGridColumn col;
			if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton){//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				col=new ODGridColumn("",18);//infoButton
				col.ImageList=imageListInfoButton;
				gridMeds.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableMedications","Medication"),120);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Notes"),190);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Notes for Patient"),190);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Status"),60,HorizontalAlignment.Center);
			gridMeds.Columns.Add(col);
			gridMeds.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<medList.Count;i++) {
				row=new ODGridRow();
				if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
					row.Cells.Add("0");//index of infobutton
				}
				if(medList[i].MedicationNum==0) {
					row.Cells.Add(medList[i].MedDescript);
					row.Cells.Add("");//generic notes
				}
				else {
					Medication generic=Medications.GetGeneric(medList[i].MedicationNum);
					string medName=Medications.GetMedication(medList[i].MedicationNum).MedName;
					if(generic.MedicationNum!=medList[i].MedicationNum) {//not generic
						medName+=" ("+generic.MedName+")";
					}
					row.Cells.Add(medName);
					row.Cells.Add(Medications.GetGeneric(medList[i].MedicationNum).Notes);
				}
				row.Cells.Add(medList[i].PatNote);
				if(MedicationPats.IsMedActive(medList[i])) {
					row.Cells.Add("Active");
				}
				else {
					row.Cells.Add("Inactive");
				}
				gridMeds.Rows.Add(row);
			}
			gridMeds.EndUpdate();
		}

		private void gridMeds_CellClick(object sender,ODGridClickEventArgs e) {
			if(!CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				return;
			}
			if(e.Col!=0) {
				return;
			}
			FormInfobutton FormIB = new FormInfobutton();
			FormIB.PatCur=PatCur;
			//FormInfoButton allows MedicationCur to be null, so this will still work for medication orders returned from NewCrop (because MedicationNum will be 0).
			FormIB.ListObjects.Add(medList[e.Row]);//TODO: verify that this is what we need to get.
			//FormIB.ListObjects.Add(Medications.GetMedicationFromDb(medList[e.Row].MedicationNum));//TODO: verify that this is what we need to get.
			FormIB.ShowDialog();
			//Nothing to do with Dialog Result yet.
		}

		private void gridMeds_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=medList[e.Row];
			FormMP.ShowDialog();
			if(FormMP.DialogResult==DialogResult.OK 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).MedicationCDS) 
			{
				FormCDSIntervention FormCDSI=new FormCDSIntervention();
				FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(Medications.GetMedication(FormMP.MedicationPatCur.MedicationNum),PatCur);
				FormCDSI.ShowIfRequired(false);
			}
			FillMeds();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			//select medication from list.  Additional meds can be added to the list from within that dlg
			FormMedications FormM=new FormMedications();
			FormM.IsSelectionMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK){
				return;
			} 
			if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS && CDSPermissions.GetForUser(Security.CurUser.UserNum).MedicationCDS) {
				FormCDSIntervention FormCDSI=new FormCDSIntervention();
				FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(Medications.GetMedication(FormM.SelectedMedicationNum),PatCur);
				FormCDSI.ShowIfRequired();
				if(FormCDSI.DialogResult==DialogResult.Abort) {
					return;//do not add medication
				}
			}
			MedicationPat MedicationPatCur=new MedicationPat();
			MedicationPatCur.PatNum=PatCur.PatNum;
			MedicationPatCur.MedicationNum=FormM.SelectedMedicationNum;
			MedicationPatCur.RxCui=Medications.GetMedication(FormM.SelectedMedicationNum).RxCui;
			MedicationPatCur.ProvNum=PatCur.PriProv;
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=MedicationPatCur;
			FormMP.IsNew=true;
			FormMP.ShowDialog();
			if(FormMP.DialogResult!=DialogResult.OK){
				return;
			}
			FillMeds();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			//pd.OriginAtMargins=true;
			//pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
#if DEBUG
        FormRpPrintPreview pView = new FormRpPrintPreview();
        pView.printPreviewControl2.Document=pd;
        pView.ShowDialog();
#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default,PatCur.PatNum,"Medications printed")) {
						pd.Print();
					}
#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Medications List For ")+PatCur.FName+" "+PatCur.LName;
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				text=Lan.g(this,"Created ")+DateTime.Now.ToString();
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			yPos=gridMeds.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		/// <summary>This report is a brute force, one page medical history report. It is not designed to handle more than one page. It does not print service notes or medications.</summary>
		private void butPrintMedical_Click(object sender,EventArgs e) {
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPageMedical);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			pd.OriginAtMargins=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			try {
#if DEBUG
        FormRpPrintPreview pView = new FormRpPrintPreview();
        pView.printPreviewControl2.Document=pd;
        pView.ShowDialog();
#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default,PatCur.PatNum,"Medical information printed")) {
						pd.Print();
					}
#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPageMedical(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			Font bodyFont=new Font(FontFamily.GenericSansSerif,10);
			StringFormat format=new StringFormat();
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			int textHeight;
			RectangleF textRect;
			text=Lan.g(this,"Medical History For ")+PatCur.FName+" "+PatCur.LName;
			g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
			yPos+=(int)g.MeasureString(text,headingFont).Height;
			text=Lan.g(this,"Birthdate: ")+PatCur.Birthdate.ToShortDateString();
			g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
			yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			text=Lan.g(this,"Created ")+DateTime.Now.ToString();
			g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
			yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			yPos+=25;
			if(gridDiseases.Rows.Count>0) {
				text=Lan.g(this,"Problems");
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				yPos+=2;
				yPos=gridDiseases.PrintPage(g,0,bounds,yPos);
				yPos+=25;
			}
			if(gridAllergies.Rows.Count>0) {
				text=Lan.g(this,"Allergies");
				g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				yPos+=2;
				yPos=gridAllergies.PrintPage(g,0,bounds,yPos);
				yPos+=25;
			}
			text=Lan.g(this,"Premedicate (PAC or other): ")+(checkPremed.Checked?"Y":"N");
			textHeight=(int)g.MeasureString(text,bodyFont,bounds.Width).Height;
			textRect=new Rectangle(bounds.Left,yPos,bounds.Width,textHeight);
			g.DrawString(text,subHeadingFont,Brushes.Black,textRect);
			yPos+=textHeight;
			yPos+=10;
			text=Lan.g(this,"Medical Urgent Note");
			g.DrawString(text,subHeadingFont,Brushes.Black,bounds.Left,yPos);
			yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			text=textMedUrgNote.Text;
			textHeight=(int)g.MeasureString(text,bodyFont,bounds.Width).Height;
			textRect=new Rectangle(bounds.Left,yPos,bounds.Width,textHeight);
			g.DrawString(text,bodyFont,Brushes.Black,textRect);//maybe red?
			yPos+=textHeight;
			yPos+=10;
			text=Lan.g(this,"Medical Summary");
			g.DrawString(text,subHeadingFont,Brushes.Black,bounds.Left,yPos);
			yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			text=textMedical.Text;
			textHeight=(int)g.MeasureString(text,bodyFont,bounds.Width).Height;
			textRect=new Rectangle(bounds.Left,yPos,bounds.Width,textHeight);
			g.DrawString(text,bodyFont,Brushes.Black,textRect);
			yPos+=textHeight;
			yPos+=10;
			text=Lan.g(this,"Medical History - Complete and Detailed");
			g.DrawString(text,subHeadingFont,Brushes.Black,bounds.Left,yPos);
			yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			text=textMedicalComp.Text;
			textHeight=(int)g.MeasureString(text,bodyFont,bounds.Width).Height;
			textRect=new Rectangle(bounds.Left,yPos,bounds.Width,textHeight);
			g.DrawString(text,bodyFont,Brushes.Black,textRect);
			yPos+=textHeight;
			g.Dispose();
		}

		private void FillFamilyHealth() {
			ListFamHealth=FamilyHealths.GetFamilyHealthForPat(PatCur.PatNum);
			gridFamilyHealth.BeginUpdate();
			gridFamilyHealth.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableFamilyHealth","Relationship"),90,HorizontalAlignment.Center);
			gridFamilyHealth.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableFamilyHealth","Name"),150);
			gridFamilyHealth.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableFamilyHealth","Problem"),180);
			gridFamilyHealth.Columns.Add(col);
			gridFamilyHealth.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListFamHealth.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Lan.g("enumFamilyRelationship",ListFamHealth[i].Relationship.ToString()));
				row.Cells.Add(ListFamHealth[i].PersonName);
				row.Cells.Add(DiseaseDefs.GetName(ListFamHealth[i].DiseaseDefNum));
				gridFamilyHealth.Rows.Add(row);
			}
			gridFamilyHealth.EndUpdate();
		}

		private void FillProblems(){
			DiseaseList=Diseases.Refresh(checkShowInactiveProblems.Checked,PatCur.PatNum);
			gridDiseases.BeginUpdate();
			gridDiseases.Columns.Clear();
			ODGridColumn col;
			if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				col=new ODGridColumn("",18);//infoButton
				col.ImageList=imageListInfoButton;
				gridDiseases.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableDiseases","Name"),140);//total is about 325
			gridDiseases.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDiseases","Patient Note"),145);
			gridDiseases.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDisease","Status"),40);
			gridDiseases.Columns.Add(col);
			gridDiseases.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DiseaseList.Count;i++){
				row=new ODGridRow();
				if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
					row.Cells.Add("0");//index of infobutton
				}
				if(DiseaseList[i].DiseaseDefNum!=0) {
					row.Cells.Add(DiseaseDefs.GetName(DiseaseList[i].DiseaseDefNum));
				}
				else {
					row.Cells.Add(DiseaseDefs.GetName(DiseaseList[i].DiseaseDefNum));
				}
				row.Cells.Add(DiseaseList[i].PatNote);
				row.Cells.Add(DiseaseList[i].ProbStatus.ToString());
				gridDiseases.Rows.Add(row);
			}
			gridDiseases.EndUpdate();
		}

		private void FillAllergies() {
			allergyList=Allergies.GetAll(PatCur.PatNum,checkShowInactiveAllergies.Checked);
			gridAllergies.BeginUpdate();
			gridAllergies.Columns.Clear();
			ODGridColumn col;
			if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				col=new ODGridColumn("",18);//infoButton
				col.ImageList=imageListInfoButton;
				gridAllergies.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TableAllergies","Allergy"),100);
			gridAllergies.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAllergies","Reaction"),180);
			gridAllergies.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAllergies","Status"),60,HorizontalAlignment.Center);
			gridAllergies.Columns.Add(col);
			gridAllergies.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<allergyList.Count;i++){
				row=new ODGridRow();
				if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
					row.Cells.Add("0");//index of infobutton
				}
				AllergyDef allergyDef=AllergyDefs.GetOne(allergyList[i].AllergyDefNum);
				row.Cells.Add(allergyDef.Description);
				if(allergyList[i].DateAdverseReaction<DateTime.Parse("1-1-1800")) {
					row.Cells.Add(allergyList[i].Reaction);
				}
				else {
					row.Cells.Add(allergyList[i].Reaction+" "+allergyList[i].DateAdverseReaction.ToShortDateString());
				}
				if(allergyList[i].StatusIsActive) {
					row.Cells.Add("Active");
				}
				else {
					row.Cells.Add("Inactive");
				}
				gridAllergies.Rows.Add(row);
			}
			gridAllergies.EndUpdate();
		}

		private void butAddProblem_Click(object sender,EventArgs e) {
			FormDiseaseDefs formDD=new FormDiseaseDefs();
			formDD.IsSelectionMode=true;
			formDD.IsMultiSelect=true;
			formDD.ShowDialog();
			if(formDD.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<formDD.SelectedDiseaseDefNums.Count;i++) {
				if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS && CDSPermissions.GetForUser(Security.CurUser.UserNum).ProblemCDS){
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(DiseaseDefs.GetItem(formDD.SelectedDiseaseDefNums[i]),PatCur);
					FormCDSI.ShowIfRequired();
					if(FormCDSI.DialogResult==DialogResult.Abort) {
						continue;//cancel 
					}
				}
				SecurityLogs.MakeLogEntry(Permissions.PatProblemListEdit,PatCur.PatNum,DiseaseDefs.GetName(formDD.SelectedDiseaseDefNums[i])+" added"); //Audit log made outside form because the form is just a list of problems and is called from many places.
				Disease disease=new Disease();
				disease.PatNum=PatCur.PatNum;
				disease.DiseaseDefNum=formDD.SelectedDiseaseDefNums[i];
				Diseases.Insert(disease);
			}
			FillProblems();
		}

		/*private void butIcd9_Click(object sender,EventArgs e) {
			FormIcd9s formI=new FormIcd9s();
			formI.IsSelectionMode=true;
			formI.ShowDialog();
			if(formI.DialogResult!=DialogResult.OK) {
				return;
			}
			Disease disease=new Disease();
			disease.PatNum=PatCur.PatNum;
			disease.ICD9Num=formI.SelectedIcd9Num;
			Diseases.Insert(disease);
			FillProblems();
		}*/

		private void gridDiseases_CellClick(object sender,ODGridClickEventArgs e) {
			if(!CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				return;
			}
			if(e.Col!=0) {
				return;
			}
			FormInfobutton FormIB=new FormInfobutton();
			FormIB.PatCur=PatCur;
			FormIB.ListObjects.Add(DiseaseDefs.GetItem(DiseaseList[e.Row].DiseaseDefNum));//TODO: verify that this is what we need to get.
			FormIB.ShowDialog();
			//Nothing to do with Dialog Result yet.
		}

		private void gridDiseases_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormDiseaseEdit FormD=new FormDiseaseEdit(DiseaseList[e.Row]);
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.OK 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).ProblemCDS) 
			{
				FormCDSIntervention FormCDSI=new FormCDSIntervention();
				FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(DiseaseDefs.GetItem(DiseaseList[e.Row].DiseaseDefNum),PatCur);
				FormCDSI.ShowIfRequired(false);
			}
			FillProblems();
		}

		private void checkShowInactiveProblems_CheckedChanged(object sender,EventArgs e) {
			FillProblems();
		}

		private void gridFamilyHealth_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormFamilyHealthEdit FormFHE=new FormFamilyHealthEdit();
			FormFHE.FamilyHealthCur=ListFamHealth[e.Row];
			FormFHE.ShowDialog();
			FillFamilyHealth();
		}

		private void butAddFamilyHistory_Click(object sender,EventArgs e) {
			FormFamilyHealthEdit FormFHE=new FormFamilyHealthEdit();
			FamilyHealth famH=new FamilyHealth();
			famH.PatNum=PatCur.PatNum;
			famH.IsNew=true;
			FormFHE.FamilyHealthCur=famH;
			FormFHE.ShowDialog();
			if(FormFHE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillFamilyHealth();
		}

		private void gridAllergies_CellClick(object sender,ODGridClickEventArgs e) {
			if(!CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowInfobutton) {//Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				return;
			}
			if(e.Col!=0) {
				return;
			}
			FormInfobutton FormIB=new FormInfobutton();
			FormIB.PatCur=PatCur;
			//TODO: get right object and pass it in.
			//FormIB. = Medications.GetMedicationFromDb(medList[e.Row].MedicationNum);//TODO: verify that this is what we need to get.
			FormIB.ShowDialog();
			//Nothing to do with Dialog Result yet.
		}

		private void gridAllergies_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAllergyEdit FAE=new FormAllergyEdit();
			FAE.AllergyCur=allergyList[gridAllergies.GetSelectedIndex()];
			FAE.ShowDialog();
			if(FAE.DialogResult==DialogResult.OK 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS 
				&& CDSPermissions.GetForUser(Security.CurUser.UserNum).AllergyCDS) 
			{
				FormCDSIntervention FormCDSI=new FormCDSIntervention();
				FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(AllergyDefs.GetOne(FAE.AllergyCur.AllergyDefNum),PatCur);
				FormCDSI.ShowIfRequired(false);
			}
			FillAllergies();
		}
		
		private void checkShowInactiveAllergies_CheckedChanged(object sender,EventArgs e) {
			FillAllergies();
		}

		/*
		private void butQuestions_Click(object sender,EventArgs e) {
			FormQuestionnaire FormQ=new FormQuestionnaire(PatCur.PatNum);
			FormQ.ShowDialog();
			if(Questions.PatHasQuest(PatCur.PatNum)) {
				butQuestions.Text=Lan.g(this,"Edit Questionnaire");
			}
			else {
				butQuestions.Text=Lan.g(this,"New Questionnaire");
			}
		}*/

		private void checkShowDiscontinuedMeds_MouseUp(object sender,MouseEventArgs e) {
			FillMeds();
		}

		private void checkDiscontinued_KeyUp(object sender,KeyEventArgs e) {
			FillMeds();
		}

		private void butMedicationReconcile_Click(object sender,EventArgs e) {
			FormMedicationReconcile FormMR=new FormMedicationReconcile();
			FormMR.PatCur=PatCur;
			FormMR.ShowDialog();
			FillMeds();
		}

		private void butAddAllergy_Click(object sender,EventArgs e) {
			FormAllergyEdit formA=new FormAllergyEdit();
			formA.AllergyCur=new Allergy();
			formA.AllergyCur.StatusIsActive=true;
			formA.AllergyCur.PatNum=PatCur.PatNum;
			formA.AllergyCur.IsNew=true;
			formA.ShowDialog();
			if(formA.DialogResult!=DialogResult.OK) {
				return;
			}
			if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS && CDSPermissions.GetForUser(Security.CurUser.UserNum).AllergyCDS) {
				FormCDSIntervention FormCDSI=new FormCDSIntervention();
				FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(AllergyDefs.GetOne(formA.AllergyCur.AllergyDefNum),PatCur);
				FormCDSI.ShowIfRequired(false);
			}
			FillAllergies();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.Premed=checkPremed.Checked;
			PatCur.MedUrgNote=textMedUrgNote.Text;
			Patients.Update(PatCur,PatOld);
			PatientNoteCur.Medical=textMedical.Text;
			PatientNoteCur.Service=textService.Text;
			PatientNoteCur.MedicalComp=textMedicalComp.Text;
			PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
			//Insert an ehrmeasureevent for CurrentMedsDocumented if user selected Yes and there isn't one with today's date
			if(radioMedsDocumentedYes.Checked && _EhrMeasureEventNum==0) {
				EhrMeasureEvent ehrMeasureEventCur=new EhrMeasureEvent();
				ehrMeasureEventCur.PatNum=PatCur.PatNum;
				ehrMeasureEventCur.DateTEvent=DateTime.Now;
				ehrMeasureEventCur.EventType=EhrMeasureEventType.CurrentMedsDocumented;
				ehrMeasureEventCur.CodeValueEvent="428191000124101";//SNOMEDCT code for document current meds procedure
				ehrMeasureEventCur.CodeSystemEvent="SNOMEDCT";
				EhrMeasureEvents.Insert(ehrMeasureEventCur);
			}
			//No is selected, if no EhrNotPerformed item for current meds documented, launch not performed edit window to allow user to select valid reason.
			if(radioMedsDocumentedNo.Checked) {
				if(_EhrNotPerfNum==0) {
					FormEhrNotPerformedEdit FormNP=new FormEhrNotPerformedEdit();
					FormNP.EhrNotPerfCur=new EhrNotPerformed();
					FormNP.EhrNotPerfCur.IsNew=true;
					FormNP.EhrNotPerfCur.PatNum=PatCur.PatNum;
					FormNP.EhrNotPerfCur.ProvNum=PatCur.PriProv;
					FormNP.SelectedItemIndex=(int)EhrNotPerformedItem.DocumentCurrentMeds;
					FormNP.EhrNotPerfCur.DateEntry=DateTime.Today;
					FormNP.IsDateReadOnly=true;
					FormNP.ShowDialog();
					if(FormNP.DialogResult==DialogResult.OK) {//if they just inserted a not performed item, set the private class-wide variable for the next if statement
						_EhrNotPerfNum=FormNP.EhrNotPerfCur.EhrNotPerformedNum;
					}
				}
				if(_EhrNotPerfNum>0 && _EhrMeasureEventNum>0) {//if not performed item is entered with today's date, delete existing performed item
					EhrMeasureEvents.Delete(_EhrMeasureEventNum);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		

	

		

		

		

	

		

	}
}
