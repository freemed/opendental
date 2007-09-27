using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormProcCodeEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox listTreatArea;
		private System.Windows.Forms.ListBox listCategory;
		private System.ComponentModel.Container components = null;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.TextBox textProcCode;
		private System.Windows.Forms.TextBox textAbbrev;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.CheckBox checkSetRecall;
		private System.Windows.Forms.CheckBox checkNoBillIns;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button butSlider;
		private OpenDental.TableTimeBar tbTime;
		private System.Windows.Forms.TextBox textTime2;
		private bool mouseIsDown;
		private Point	mouseOrigin;
		private Point sliderOrigin;
		private System.Windows.Forms.Label label11;
		private StringBuilder strBTime;
		private System.Windows.Forms.CheckBox checkIsHygiene;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textAlternateCode1;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.CheckBox checkIsProsth;
		private System.Windows.Forms.Label label14;
		private bool FeeChanged;
		private System.Windows.Forms.TextBox textMedicalCode;
		private OpenDental.UI.ODGrid gridFees;
		private Label label15;
		private ListBox listPaintType;
		private Label labelColor;
		private System.Windows.Forms.Button butColor;
		private OpenDental.UI.Button butColorClear;
		private TextBox textLaymanTerm;
		private Label label2;
		private CheckBox checkIsCanadianLab;
		private Label label16;
		private TextBox textBaseUnits;
		private Label label17;
		private Label label18;
		private TextBox textSubstitutionCode;
		private ODGrid gridNotes;
		private OpenDental.UI.Button butAddNote;
		private ProcedureCode ProcCode;
		private Label label19;
		private ComboBox comboSubstOnlyIf;
		private List<ProcCodeNote> NoteList;

		///<summary>The procedure code must have already been insterted into the database.</summary>
		public FormProcCodeEdit(ProcedureCode procCode){
			InitializeComponent();// Required for Windows Form Designer support
			tbTime.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbTime_CellClicked);
			Lan.F(this);
			ProcCode=procCode;
		}

		///<summary></summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcCodeEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.textProcCode = new System.Windows.Forms.TextBox();
			this.textAbbrev = new System.Windows.Forms.TextBox();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.listTreatArea = new System.Windows.Forms.ListBox();
			this.checkSetRecall = new System.Windows.Forms.CheckBox();
			this.checkNoBillIns = new System.Windows.Forms.CheckBox();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.butSlider = new System.Windows.Forms.Button();
			this.textTime2 = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.textAlternateCode1 = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.checkIsProsth = new System.Windows.Forms.CheckBox();
			this.textMedicalCode = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.listPaintType = new System.Windows.Forms.ListBox();
			this.labelColor = new System.Windows.Forms.Label();
			this.butColor = new System.Windows.Forms.Button();
			this.textLaymanTerm = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkIsCanadianLab = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBaseUnits = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.textSubstitutionCode = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.comboSubstOnlyIf = new System.Windows.Forms.ComboBox();
			this.butAddNote = new OpenDental.UI.Button();
			this.gridNotes = new OpenDental.UI.ODGrid();
			this.tbTime = new OpenDental.TableTimeBar();
			this.butColorClear = new OpenDental.UI.Button();
			this.gridFees = new OpenDental.UI.ODGrid();
			this.textNote = new OpenDental.ODtextBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(97,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82,14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Proc Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(474,234);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,14);
			this.label4.TabIndex = 3;
			this.label4.Text = "Treatment Area";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(600,13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,14);
			this.label5.TabIndex = 4;
			this.label5.Text = "Category";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(2,58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53,39);
			this.label6.TabIndex = 5;
			this.label6.Text = "Time Pattern";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(83,118);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(94,16);
			this.label7.TabIndex = 6;
			this.label7.Text = "Abbreviation";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(83,98);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(94,14);
			this.label8.TabIndex = 7;
			this.label8.Text = "Description";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(43,354);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(148,14);
			this.label10.TabIndex = 9;
			this.label10.Text = "Default Note";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textProcCode
			// 
			this.textProcCode.Location = new System.Drawing.Point(179,12);
			this.textProcCode.Name = "textProcCode";
			this.textProcCode.ReadOnly = true;
			this.textProcCode.Size = new System.Drawing.Size(100,20);
			this.textProcCode.TabIndex = 14;
			// 
			// textAbbrev
			// 
			this.textAbbrev.Location = new System.Drawing.Point(179,117);
			this.textAbbrev.MaxLength = 20;
			this.textAbbrev.Name = "textAbbrev";
			this.textAbbrev.Size = new System.Drawing.Size(100,20);
			this.textAbbrev.TabIndex = 1;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(179,96);
			this.textDescription.MaxLength = 255;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(287,20);
			this.textDescription.TabIndex = 0;
			// 
			// listTreatArea
			// 
			this.listTreatArea.Items.AddRange(new object[] {
            "Surface",
            "Tooth",
            "Mouth",
            "Quadrant",
            "Sextant",
            "Arch",
            "Tooth Range"});
			this.listTreatArea.Location = new System.Drawing.Point(476,252);
			this.listTreatArea.Name = "listTreatArea";
			this.listTreatArea.Size = new System.Drawing.Size(118,95);
			this.listTreatArea.TabIndex = 2;
			// 
			// checkSetRecall
			// 
			this.checkSetRecall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkSetRecall.Location = new System.Drawing.Point(45,251);
			this.checkSetRecall.Name = "checkSetRecall";
			this.checkSetRecall.Size = new System.Drawing.Size(284,18);
			this.checkSetRecall.TabIndex = 5;
			this.checkSetRecall.Text = "Triggers Recall";
			// 
			// checkNoBillIns
			// 
			this.checkNoBillIns.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkNoBillIns.Location = new System.Drawing.Point(45,271);
			this.checkNoBillIns.Name = "checkNoBillIns";
			this.checkNoBillIns.Size = new System.Drawing.Size(284,18);
			this.checkNoBillIns.TabIndex = 6;
			this.checkNoBillIns.Text = "Do not usually bill to insurance";
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(600,31);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(120,238);
			this.listCategory.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(184,669);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(388,29);
			this.label3.TabIndex = 28;
			this.label3.Text = "There is no way to delete a code once created because it might have been used som" +
    "eplace.  Instead, move it to a category like \"obsolete\"";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(750,670);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(178,28);
			this.label9.TabIndex = 29;
			this.label9.Text = "Even if you press cancel, changes to fees will not be undone.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butSlider
			// 
			this.butSlider.BackColor = System.Drawing.SystemColors.ControlDark;
			this.butSlider.Location = new System.Drawing.Point(12,113);
			this.butSlider.Name = "butSlider";
			this.butSlider.Size = new System.Drawing.Size(12,15);
			this.butSlider.TabIndex = 31;
			this.butSlider.UseVisualStyleBackColor = false;
			this.butSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseDown);
			this.butSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseMove);
			this.butSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.butSlider_MouseUp);
			// 
			// textTime2
			// 
			this.textTime2.Location = new System.Drawing.Point(10,674);
			this.textTime2.Name = "textTime2";
			this.textTime2.Size = new System.Drawing.Size(60,20);
			this.textTime2.TabIndex = 32;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(76,678);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(102,16);
			this.label11.TabIndex = 33;
			this.label11.Text = "Minutes";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHygiene.Location = new System.Drawing.Point(45,291);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(284,18);
			this.checkIsHygiene.TabIndex = 7;
			this.checkIsHygiene.Text = "Is Hygiene procedure";
			// 
			// textAlternateCode1
			// 
			this.textAlternateCode1.Location = new System.Drawing.Point(179,33);
			this.textAlternateCode1.MaxLength = 15;
			this.textAlternateCode1.Name = "textAlternateCode1";
			this.textAlternateCode1.Size = new System.Drawing.Size(100,20);
			this.textAlternateCode1.TabIndex = 38;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(100,35);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(79,14);
			this.label12.TabIndex = 37;
			this.label12.Text = "Alt Code";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(285,35);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(161,19);
			this.label13.TabIndex = 39;
			this.label13.Text = "(For some Medicaid)";
			// 
			// checkIsProsth
			// 
			this.checkIsProsth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsProsth.Location = new System.Drawing.Point(45,311);
			this.checkIsProsth.Name = "checkIsProsth";
			this.checkIsProsth.Size = new System.Drawing.Size(284,18);
			this.checkIsProsth.TabIndex = 41;
			this.checkIsProsth.Text = "Is Prosthesis (Crown,Bridge,Denture,RPD)";
			// 
			// textMedicalCode
			// 
			this.textMedicalCode.Location = new System.Drawing.Point(179,54);
			this.textMedicalCode.MaxLength = 15;
			this.textMedicalCode.Name = "textMedicalCode";
			this.textMedicalCode.Size = new System.Drawing.Size(100,20);
			this.textMedicalCode.TabIndex = 43;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(100,56);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(79,14);
			this.label14.TabIndex = 42;
			this.label14.Text = "Medical Code";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(474,10);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100,18);
			this.label15.TabIndex = 46;
			this.label15.Text = "Paint Type";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listPaintType
			// 
			this.listPaintType.Location = new System.Drawing.Point(476,31);
			this.listPaintType.Name = "listPaintType";
			this.listPaintType.Size = new System.Drawing.Size(118,199);
			this.listPaintType.TabIndex = 45;
			// 
			// labelColor
			// 
			this.labelColor.Location = new System.Drawing.Point(75,197);
			this.labelColor.Name = "labelColor";
			this.labelColor.Size = new System.Drawing.Size(293,16);
			this.labelColor.TabIndex = 48;
			this.labelColor.Text = "Color override instead of using status colors.";
			this.labelColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// butColor
			// 
			this.butColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColor.Location = new System.Drawing.Point(44,194);
			this.butColor.Name = "butColor";
			this.butColor.Size = new System.Drawing.Size(30,20);
			this.butColor.TabIndex = 47;
			this.butColor.Click += new System.EventHandler(this.butColor_Click);
			// 
			// textLaymanTerm
			// 
			this.textLaymanTerm.Location = new System.Drawing.Point(179,138);
			this.textLaymanTerm.MaxLength = 255;
			this.textLaymanTerm.Name = "textLaymanTerm";
			this.textLaymanTerm.Size = new System.Drawing.Size(178,20);
			this.textLaymanTerm.TabIndex = 50;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(53,139);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(124,16);
			this.label2.TabIndex = 51;
			this.label2.Text = "Layman\'s Term";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkIsCanadianLab
			// 
			this.checkIsCanadianLab.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsCanadianLab.Location = new System.Drawing.Point(45,331);
			this.checkIsCanadianLab.Name = "checkIsCanadianLab";
			this.checkIsCanadianLab.Size = new System.Drawing.Size(284,18);
			this.checkIsCanadianLab.TabIndex = 52;
			this.checkIsCanadianLab.Text = "Is Lab Fee";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(74,162);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(103,13);
			this.label16.TabIndex = 53;
			this.label16.Text = "Base Units";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBaseUnits
			// 
			this.textBaseUnits.Location = new System.Drawing.Point(179,159);
			this.textBaseUnits.Name = "textBaseUnits";
			this.textBaseUnits.Size = new System.Drawing.Size(30,20);
			this.textBaseUnits.TabIndex = 54;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(215,162);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(202,19);
			this.label17.TabIndex = 55;
			this.label17.Text = "(For some medical claims)";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(56,78);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(121,13);
			this.label18.TabIndex = 56;
			this.label18.Text = "Ins. Subst Code";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubstitutionCode
			// 
			this.textSubstitutionCode.Location = new System.Drawing.Point(179,75);
			this.textSubstitutionCode.MaxLength = 255;
			this.textSubstitutionCode.Name = "textSubstitutionCode";
			this.textSubstitutionCode.Size = new System.Drawing.Size(100,20);
			this.textSubstitutionCode.TabIndex = 57;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(280,76);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(46,18);
			this.label19.TabIndex = 58;
			this.label19.Text = "Only if";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboSubstOnlyIf
			// 
			this.comboSubstOnlyIf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSubstOnlyIf.FormattingEnabled = true;
			this.comboSubstOnlyIf.Location = new System.Drawing.Point(321,74);
			this.comboSubstOnlyIf.Name = "comboSubstOnlyIf";
			this.comboSubstOnlyIf.Size = new System.Drawing.Size(145,21);
			this.comboSubstOnlyIf.TabIndex = 61;
			// 
			// butAddNote
			// 
			this.butAddNote.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddNote.Autosize = true;
			this.butAddNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddNote.CornerRadius = 4F;
			this.butAddNote.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddNote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddNote.Location = new System.Drawing.Point(600,450);
			this.butAddNote.Name = "butAddNote";
			this.butAddNote.Size = new System.Drawing.Size(88,26);
			this.butAddNote.TabIndex = 60;
			this.butAddNote.Text = "Add Note";
			this.butAddNote.Click += new System.EventHandler(this.butAddNote_Click);
			// 
			// gridNotes
			// 
			this.gridNotes.HScrollVisible = false;
			this.gridNotes.Location = new System.Drawing.Point(44,482);
			this.gridNotes.Name = "gridNotes";
			this.gridNotes.ScrollValue = 0;
			this.gridNotes.Size = new System.Drawing.Size(676,180);
			this.gridNotes.TabIndex = 59;
			this.gridNotes.Title = "Notes and Times for Specific Providers";
			this.gridNotes.TranslationName = "TableProcedureNotes";
			this.gridNotes.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridNotes_CellDoubleClick);
			// 
			// tbTime
			// 
			this.tbTime.BackColor = System.Drawing.SystemColors.Window;
			this.tbTime.Location = new System.Drawing.Point(10,108);
			this.tbTime.Name = "tbTime";
			this.tbTime.ScrollValue = 150;
			this.tbTime.SelectedIndices = new int[0];
			this.tbTime.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbTime.Size = new System.Drawing.Size(15,561);
			this.tbTime.TabIndex = 30;
			// 
			// butColorClear
			// 
			this.butColorClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butColorClear.Autosize = true;
			this.butColorClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butColorClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butColorClear.CornerRadius = 4F;
			this.butColorClear.Location = new System.Drawing.Point(44,220);
			this.butColorClear.Name = "butColorClear";
			this.butColorClear.Size = new System.Drawing.Size(75,20);
			this.butColorClear.TabIndex = 49;
			this.butColorClear.Text = "Clear Color";
			this.butColorClear.Click += new System.EventHandler(this.butColorClear_Click);
			// 
			// gridFees
			// 
			this.gridFees.HScrollVisible = false;
			this.gridFees.Location = new System.Drawing.Point(726,31);
			this.gridFees.Name = "gridFees";
			this.gridFees.ScrollValue = 0;
			this.gridFees.Size = new System.Drawing.Size(199,445);
			this.gridFees.TabIndex = 44;
			this.gridFees.Title = "Fees";
			this.gridFees.TranslationName = "TableProcFee";
			this.gridFees.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridFees_CellDoubleClick);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(44,372);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(550,104);
			this.textNote.TabIndex = 40;
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
			this.butCancel.Location = new System.Drawing.Point(850,636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 11;
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
			this.butOK.Location = new System.Drawing.Point(850,600);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 10;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormProcCodeEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(941,707);
			this.Controls.Add(this.comboSubstOnlyIf);
			this.Controls.Add(this.butAddNote);
			this.Controls.Add(this.gridNotes);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.textSubstitutionCode);
			this.Controls.Add(this.butSlider);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.textBaseUnits);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.checkIsCanadianLab);
			this.Controls.Add(this.textLaymanTerm);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butColorClear);
			this.Controls.Add(this.labelColor);
			this.Controls.Add(this.butColor);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.listPaintType);
			this.Controls.Add(this.gridFees);
			this.Controls.Add(this.textMedicalCode);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.checkIsProsth);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.textAlternateCode1);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.checkIsHygiene);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.textTime2);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textAbbrev);
			this.Controls.Add(this.textProcCode);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.checkNoBillIns);
			this.Controls.Add(this.checkSetRecall);
			this.Controls.Add(this.listTreatArea);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcCodeEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Procedure Code";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProcCodeEdit_Closing);
			this.Load += new System.EventHandler(this.FormProcCodeEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProcCodeEdit_Load(object sender, System.EventArgs e) {
			List<ProcedureCode> listCodes=CDT.Class1.GetADAcodes();
			if(listCodes.Count>0 && ProcCode.ProcCode.Length==5 && ProcCode.ProcCode.Substring(0,1)=="D") {
				for(int i=0;i<listCodes.Count;i++) {
					if(listCodes[i].ProcCode==ProcCode.ProcCode) {
						textDescription.ReadOnly=true;
					}
				}
			}
			textProcCode.Text=ProcCode.ProcCode;
			textAlternateCode1.Text=ProcCode.AlternateCode1;
			textMedicalCode.Text=ProcCode.MedicalCode;
			textSubstitutionCode.Text=ProcCode.SubstitutionCode;
			for(int i=0;i<Enum.GetNames(typeof(SubstitutionCondition)).Length;i++) {
				comboSubstOnlyIf.Items.Add(Lan.g("enumSubstitutionCondition",Enum.GetNames(typeof(SubstitutionCondition))[i]));
			}
			comboSubstOnlyIf.SelectedIndex=(int)ProcCode.SubstOnlyIf;
			textDescription.Text=ProcCode.Descript;
			textAbbrev.Text=ProcCode.AbbrDesc;
			textLaymanTerm.Text=ProcCode.LaymanTerm;
			strBTime=new StringBuilder(ProcCode.ProcTime);
			butColor.BackColor=ProcCode.GraphicColor;
			checkSetRecall.Checked=ProcCode.SetRecall;
			checkNoBillIns.Checked=ProcCode.NoBillIns;
			checkIsHygiene.Checked=ProcCode.IsHygiene;
			checkIsProsth.Checked=ProcCode.IsProsth;
			textBaseUnits.Text=ProcCode.BaseUnits.ToString();
			if(CultureInfo.CurrentCulture.Name.Substring(3)!="CA"){//Canada
				checkIsCanadianLab.Visible=false;
			}
			checkIsCanadianLab.Checked=ProcCode.IsCanadianLab;
			textNote.Text=ProcCode.DefaultNote;
			listTreatArea.Items.Clear();
			for(int i=1;i<Enum.GetNames(typeof(TreatmentArea)).Length;i++){
				listTreatArea.Items.Add(Lan.g("enumTreatmentArea",Enum.GetNames(typeof(TreatmentArea))[i]));
			}
			listTreatArea.SelectedIndex=(int)ProcCode.TreatArea-1;
			if(listTreatArea.SelectedIndex==-1) listTreatArea.SelectedIndex=2;
			for(int i=0;i<Enum.GetNames(typeof(ToothPaintingType)).Length;i++){
				listPaintType.Items.Add(Enum.GetNames(typeof(ToothPaintingType))[i]);
				if((int)ProcCode.PaintType==i){
					listPaintType.SelectedIndex=i;
				}
			}
			for(int i=0;i<DefB.Short[(int)DefCat.ProcCodeCats].Length;i++){
				listCategory.Items.Add(DefB.Short[(int)DefCat.ProcCodeCats][i].ItemName);
				if(DefB.Short[(int)DefCat.ProcCodeCats][i].DefNum==ProcCode.ProcCat)
					listCategory.SelectedIndex=i;
			}
			if(listCategory.SelectedIndex==-1)
				listCategory.SelectedIndex=0;
			FillTime();
			FillFees();
			FillNotes();
		}

		private void FillTime(){
			for (int i=0;i<strBTime.Length;i++){
				tbTime.Cell[0,i]=strBTime.ToString(i,1);
				tbTime.BackGColor[0,i]=Color.White;
			}
			for (int i=strBTime.Length;i<tbTime.MaxRows;i++){
				tbTime.Cell[0,i]="";
				tbTime.BackGColor[0,i]=Color.FromName("Control");
			}
			tbTime.Refresh();
			butSlider.Location=new Point(tbTime.Location.X+2
				,(tbTime.Location.Y+strBTime.Length*14+1));
			textTime2.Text=(strBTime.Length*ContrApptSheet.MinPerIncr).ToString();
		}

		private void FillFees(){
			//This line will be added later for speed:
			//DataTable feeList=Fees.GetListForCode(ProcCode.Code);
			gridFees.BeginUpdate();
			gridFees.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcFee","Sched"),120);
			gridFees.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcFee","Amount"),60,HorizontalAlignment.Right);
			gridFees.Columns.Add(col); 
			gridFees.Rows.Clear();
			ODGridRow row;
			Fee fee;
			for(int i=0;i<DefB.Short[(int)DefCat.FeeSchedNames].Length;i++){
				fee=Fees.GetFeeByOrder(ProcCode.CodeNum,i);
				row=new ODGridRow();
				row.Cells.Add(DefB.Short[(int)DefCat.FeeSchedNames][i].ItemName);
				if(fee==null){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(fee.Amount.ToString("n"));
				}
				gridFees.Rows.Add(row);
			}
			gridFees.EndUpdate();
		}

		private void gridFees_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			Fee FeeCur=Fees.GetFeeByOrder(ProcCode.CodeNum,e.Row);
			//tbFees.SelectedRow=e.Row;
			//tbFees.ColorRow(e.Row,Color.LightGray);
			FormFeeEdit FormFE=new FormFeeEdit();
			if(FeeCur==null) {
				FeeCur=new Fee();
				FeeCur.FeeSched=DefB.Short[(int)DefCat.FeeSchedNames][e.Row].DefNum;
				FeeCur.CodeNum=ProcCode.CodeNum;
				Fees.Insert(FeeCur);
				FormFE.IsNew=true;
			}
			FormFE.FeeCur=FeeCur;
			FormFE.ShowDialog();
			if(FormFE.DialogResult==DialogResult.OK) {
				FeeChanged=true;
			}
			Fees.Refresh();
			//tbFees.SelectedRow=-1;
			FillFees();
		}

		private void FillNotes(){
			NoteList=ProcCodeNotes.GetList(ProcCode.CodeNum);
			gridNotes.BeginUpdate();
			gridNotes.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableProcedureNotes","Prov"),80);
			gridNotes.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcedureNotes","Time"),150);
			gridNotes.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableProcedureNotes","Note"),400);
			gridNotes.Columns.Add(col);
			gridNotes.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<NoteList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Providers.GetAbbr(NoteList[i].ProvNum));
				row.Cells.Add(NoteList[i].ProcTime);
				row.Cells.Add(NoteList[i].Note);
				gridNotes.Rows.Add(row);
			}
			gridNotes.EndUpdate();
		}

		private void tbTime_CellClicked(object sender, CellEventArgs e){
			if(e.Row<strBTime.Length){
				if(strBTime[e.Row]=='/'){
					strBTime.Replace('/','X',e.Row,1);
				}
				else{
					strBTime.Replace(strBTime[e.Row],'/',e.Row,1);
				}
			}
			FillTime();
		}

		private void butSlider_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=true;
			mouseOrigin=new Point(e.X+butSlider.Location.X
				,e.Y+butSlider.Location.Y);
			sliderOrigin=butSlider.Location;
			
		}

		private void butSlider_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(!mouseIsDown)return;
			//tempPoint represents the new location of button of smooth dragging.
			Point tempPoint=new Point(sliderOrigin.X
				,sliderOrigin.Y+(e.Y+butSlider.Location.Y)-mouseOrigin.Y);
			int step=(int)(Math.Round((Decimal)(tempPoint.Y-tbTime.Location.Y)/14));
			if(step==strBTime.Length)return;
			if(step<1)return;
			if(step>tbTime.MaxRows-1) return;
			if(step>strBTime.Length){
				strBTime.Append('/');
			}
			if(step<strBTime.Length){
				strBTime.Remove(step,1);
			}
			FillTime();
		}

		private void butSlider_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			mouseIsDown=false;
		}

		private void butColor_Click(object sender,EventArgs e) {
			ColorDialog colorDialog1=new ColorDialog();
			colorDialog1.Color=butColor.BackColor;
			colorDialog1.ShowDialog();
			butColor.BackColor=colorDialog1.Color;
		}

		private void butColorClear_Click(object sender,EventArgs e) {
			butColor.BackColor=Color.FromArgb(0);
		}

		private void butAddNote_Click(object sender,EventArgs e) {
			FormProcCodeNoteEdit FormP=new FormProcCodeNoteEdit();
			FormP.IsNew=true;
			FormP.NoteCur=new ProcCodeNote();
			FormP.NoteCur.CodeNum=ProcCode.CodeNum;
			FormP.NoteCur.Note=textNote.Text;
			FormP.NoteCur.ProcTime=strBTime.ToString();
			FormP.ShowDialog();
			FillNotes();
		}

		private void gridNotes_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormProcCodeNoteEdit FormP=new FormProcCodeNoteEdit();
			FormP.NoteCur=NoteList[e.Row].Copy();
			FormP.ShowDialog();
			FillNotes();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textMedicalCode.Text!="" && !ProcedureCodes.HList.Contains(textMedicalCode.Text)){
				MsgBox.Show(this,"Invalid medical code.  It must refer to an existing procedure code entered separately");
				return;
			}
			if(textSubstitutionCode.Text!="" && !ProcedureCodes.HList.Contains(textSubstitutionCode.Text)) {
				MsgBox.Show(this,"Invalid substitution code.  It must refer to an existing procedure code entered separately");
				return;
			}
			bool DoSynchRecall=false;
			if(IsNew && checkSetRecall.Checked){
				DoSynchRecall=true;
			}
			else if(ProcCode.SetRecall!=checkSetRecall.Checked){//set recall changed
				DoSynchRecall=true;
			}
			if(DoSynchRecall){
				if(!MsgBox.Show(this,true,"Because you have changed the recall setting for this procedure code, all your patient recalls will be resynchronized, which can take a minute or two.  Do you want to continue?")){
					return;
				}
			}
			ProcCode.AlternateCode1=textAlternateCode1.Text;
			ProcCode.MedicalCode=textMedicalCode.Text;
			ProcCode.SubstitutionCode=textSubstitutionCode.Text;
			ProcCode.SubstOnlyIf=(SubstitutionCondition)comboSubstOnlyIf.SelectedIndex;
			ProcCode.Descript=textDescription.Text;
			ProcCode.AbbrDesc=textAbbrev.Text;
			ProcCode.LaymanTerm=textLaymanTerm.Text;
			ProcCode.ProcTime=strBTime.ToString();
			ProcCode.GraphicColor=butColor.BackColor;
			ProcCode.SetRecall=checkSetRecall.Checked;
			ProcCode.NoBillIns=checkNoBillIns.Checked;
			ProcCode.IsProsth=checkIsProsth.Checked;
			ProcCode.IsHygiene=checkIsHygiene.Checked;
			ProcCode.IsCanadianLab=checkIsCanadianLab.Checked;
			ProcCode.DefaultNote=textNote.Text;
			ProcCode.PaintType=(ToothPaintingType)listPaintType.SelectedIndex;
			ProcCode.TreatArea=(TreatmentArea)listTreatArea.SelectedIndex+1;
			ProcCode.BaseUnits=Int16.Parse(textBaseUnits.Text.ToString());
			if(listCategory.SelectedIndex!=-1)
				ProcCode.ProcCat=DefB.Short[(int)DefCat.ProcCodeCats][listCategory.SelectedIndex].DefNum;
			ProcedureCodes.Update(ProcCode);//whether new or not.
			if(DoSynchRecall){
				Cursor=Cursors.WaitCursor;
				Recalls.SynchAllPatients();
				Cursor=Cursors.Default;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProcCodeEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(FeeChanged){
				DataValid.SetInvalid(InvalidTypes.Fees);
				DialogResult=DialogResult.OK;
			}
		}

		

		
	}
}
