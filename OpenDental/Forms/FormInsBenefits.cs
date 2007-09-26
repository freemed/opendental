using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormInsBenefits : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private CheckBox checkCalendarYear;
		private OpenDental.UI.Button butClear;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.ODGrid gridBenefits;
		private CheckBox checkSimplified;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>This needs to be set externally.  It will only be altered when user clicks OK and form closes.</summary>
		public List<Benefit> OriginalBenList;
		private int PlanNum;
		private Label label1;
		private ODtextBox textSubscNote;
		private Label label28;
		private ValidDouble textAnnualMax;
		private int PatPlanNum;
		///<summary>The subscriber note.  Gets set before form opens.</summary>
		public string Note;
		private ValidDouble textDeductible;
		private Label label2;
		private ValidDouble textDeductPrev;
		private Label label3;
		private Label label4;
		private Label label5;
		private ValidNumber textBW;
		private Label label6;
		private GroupBox groupBox1;
		private ComboBox comboExams;
		private ValidNumber textExams;
		private Label label8;
		private ComboBox comboPano;
		private ValidNumber textPano;
		private Label label7;
		private ComboBox comboBW;
		private GroupBox groupBox3;
		private ValidNumber textOrthoPercent;
		private Label label11;
		private ValidDouble textOrthoMax;
		private Label label10;
		private ValidNumber textStand1;
		private ValidNumber textStand2;
		private GroupBox groupBox4;
		private ValidNumber textOralSurg;
		private Label label20;
		private ValidNumber textPerio;
		private Label label19;
		private ValidNumber textEndo;
		private Label label18;
		private ValidNumber textRoutinePrev;
		private Label label9;
		private ValidNumber textCrowns;
		private Label label15;
		private ValidNumber textRestorative;
		private Label label16;
		private ValidNumber textDiagnostic;
		private Label label17;
		private ValidNumber textProsth;
		private Label label21;
		private ValidNumber textMaxProsth;
		private Label label22;
		private ValidNumber textAccident;
		private Label label23;
		private ValidNumber textFlo;
		private CheckBox checkCalYearMain;
		private Panel panelSimple;
		///<summary>This is the list used to display on this form.</summary>
		private List<Benefit> benefitList;
		private ValidNumber textStand4;
		private Label label24;
		private Label label12;
		private Panel panel3;
		private Panel panel2;
		private Panel panel1;
		///<summary>This is the list of all benefits to display on this form.  Some will be in the simple view, and the rest will be transferred to benefitList for display in the grid.</summary>
		private List<Benefit> benefitListAll;
		private Label label13;
		private Label label14;
		private ValidDouble textAnnualMaxFam;
		private ValidDouble textDeductibleFam;
		private ValidDouble textDeductPrevFam;
		private bool dontAllowSimplified;

		///<summary></summary>
		public FormInsBenefits(int planNum,int patPlanNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			PatPlanNum=patPlanNum;
			PlanNum=planNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsBenefits));
			this.checkCalendarYear = new System.Windows.Forms.CheckBox();
			this.checkSimplified = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboExams = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboPano = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBW = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.checkCalYearMain = new System.Windows.Forms.CheckBox();
			this.panelSimple = new System.Windows.Forms.Panel();
			this.label24 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textAnnualMaxFam = new OpenDental.ValidDouble();
			this.textDeductibleFam = new OpenDental.ValidDouble();
			this.textDeductPrevFam = new OpenDental.ValidDouble();
			this.textAnnualMax = new OpenDental.ValidDouble();
			this.textFlo = new OpenDental.ValidNumber();
			this.textStand4 = new OpenDental.ValidNumber();
			this.textAccident = new OpenDental.ValidNumber();
			this.textStand2 = new OpenDental.ValidNumber();
			this.textMaxProsth = new OpenDental.ValidNumber();
			this.textStand1 = new OpenDental.ValidNumber();
			this.textProsth = new OpenDental.ValidNumber();
			this.textOralSurg = new OpenDental.ValidNumber();
			this.textPerio = new OpenDental.ValidNumber();
			this.textEndo = new OpenDental.ValidNumber();
			this.textRoutinePrev = new OpenDental.ValidNumber();
			this.textCrowns = new OpenDental.ValidNumber();
			this.textRestorative = new OpenDental.ValidNumber();
			this.textDiagnostic = new OpenDental.ValidNumber();
			this.textDeductible = new OpenDental.ValidDouble();
			this.textOrthoPercent = new OpenDental.ValidNumber();
			this.textOrthoMax = new OpenDental.ValidDouble();
			this.textDeductPrev = new OpenDental.ValidDouble();
			this.textExams = new OpenDental.ValidNumber();
			this.textPano = new OpenDental.ValidNumber();
			this.textBW = new OpenDental.ValidNumber();
			this.textSubscNote = new OpenDental.ODtextBox();
			this.gridBenefits = new OpenDental.UI.ODGrid();
			this.butClear = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panelSimple.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkCalendarYear
			// 
			this.checkCalendarYear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCalendarYear.Location = new System.Drawing.Point(261,524);
			this.checkCalendarYear.Name = "checkCalendarYear";
			this.checkCalendarYear.Size = new System.Drawing.Size(121,21);
			this.checkCalendarYear.TabIndex = 154;
			this.checkCalendarYear.Text = "Calendar Year";
			this.checkCalendarYear.ThreeState = true;
			this.checkCalendarYear.UseVisualStyleBackColor = true;
			this.checkCalendarYear.Click += new System.EventHandler(this.checkCalendarYear_Click);
			// 
			// checkSimplified
			// 
			this.checkSimplified.Checked = true;
			this.checkSimplified.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkSimplified.Location = new System.Drawing.Point(12,31);
			this.checkSimplified.Name = "checkSimplified";
			this.checkSimplified.Size = new System.Drawing.Size(123,17);
			this.checkSimplified.TabIndex = 157;
			this.checkSimplified.Text = "Simplified View";
			this.checkSimplified.UseVisualStyleBackColor = true;
			this.checkSimplified.Click += new System.EventHandler(this.checkSimplified_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(90,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,21);
			this.label1.TabIndex = 159;
			this.label1.Text = "Annual Max";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(38,563);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(74,41);
			this.label28.TabIndex = 160;
			this.label28.Text = "Notes";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(43,46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147,21);
			this.label2.TabIndex = 163;
			this.label2.Text = "General Deductible";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(1,66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(189,21);
			this.label3.TabIndex = 165;
			this.label3.Text = "Preventive Deductible (if different)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(90,86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,21);
			this.label4.TabIndex = 167;
			this.label4.Text = "Fluoride to Age";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5,28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70,21);
			this.label5.TabIndex = 168;
			this.label5.Text = "BWs";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(77,12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(39,15);
			this.label6.TabIndex = 170;
			this.label6.Text = "#";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboExams);
			this.groupBox1.Controls.Add(this.textExams);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.comboPano);
			this.groupBox1.Controls.Add(this.textPano);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.comboBW);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBW);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(31,133);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(269,99);
			this.groupBox1.TabIndex = 171;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Frequencies";
			// 
			// comboExams
			// 
			this.comboExams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboExams.FormattingEnabled = true;
			this.comboExams.Items.AddRange(new object[] {
            "Every # Years",
            "# Per Year",
            "Every # Months"});
			this.comboExams.Location = new System.Drawing.Point(120,71);
			this.comboExams.Name = "comboExams";
			this.comboExams.Size = new System.Drawing.Size(136,21);
			this.comboExams.TabIndex = 178;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(5,70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(70,21);
			this.label8.TabIndex = 176;
			this.label8.Text = "Exams";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboPano
			// 
			this.comboPano.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPano.FormattingEnabled = true;
			this.comboPano.Items.AddRange(new object[] {
            "Every # Years",
            "# Per Year",
            "Every # Months"});
			this.comboPano.Location = new System.Drawing.Point(120,50);
			this.comboPano.Name = "comboPano";
			this.comboPano.Size = new System.Drawing.Size(136,21);
			this.comboPano.TabIndex = 175;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(5,49);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70,21);
			this.label7.TabIndex = 173;
			this.label7.Text = "Pano/FMX";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBW
			// 
			this.comboBW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBW.FormattingEnabled = true;
			this.comboBW.Items.AddRange(new object[] {
            "Every # Years",
            "# Per Year",
            "Every # Months"});
			this.comboBW.Location = new System.Drawing.Point(120,29);
			this.comboBW.Name = "comboBW";
			this.comboBW.Size = new System.Drawing.Size(136,21);
			this.comboBW.TabIndex = 172;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textOrthoPercent);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.textOrthoMax);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Location = new System.Drawing.Point(68,243);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(232,71);
			this.groupBox3.TabIndex = 175;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Ortho";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(38,42);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(70,21);
			this.label11.TabIndex = 174;
			this.label11.Text = "Percentage";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(7,16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100,21);
			this.label10.TabIndex = 163;
			this.label10.Text = "Lifetime Max";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.panel3);
			this.groupBox4.Controls.Add(this.panel2);
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.Controls.Add(this.textStand4);
			this.groupBox4.Controls.Add(this.textAccident);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.textStand2);
			this.groupBox4.Controls.Add(this.textMaxProsth);
			this.groupBox4.Controls.Add(this.label22);
			this.groupBox4.Controls.Add(this.textStand1);
			this.groupBox4.Controls.Add(this.textProsth);
			this.groupBox4.Controls.Add(this.label21);
			this.groupBox4.Controls.Add(this.textOralSurg);
			this.groupBox4.Controls.Add(this.label20);
			this.groupBox4.Controls.Add(this.textPerio);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.textEndo);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.textRoutinePrev);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.textCrowns);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.textRestorative);
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.textDiagnostic);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Location = new System.Drawing.Point(422,3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(282,311);
			this.groupBox4.TabIndex = 176;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Percentages";
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Underline,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label12.Location = new System.Drawing.Point(189,8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(87,22);
			this.label12.TabIndex = 199;
			this.label12.Text = "Quick Entry";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel3.Location = new System.Drawing.Point(18,209);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(246,1);
			this.panel3.TabIndex = 198;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel2.Location = new System.Drawing.Point(18,162);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(246,1);
			this.panel2.TabIndex = 197;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Location = new System.Drawing.Point(18,75);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(246,1);
			this.panel1.TabIndex = 196;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(1,232);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(120,21);
			this.label23.TabIndex = 194;
			this.label23.Text = "Accident";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(1,212);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(120,21);
			this.label22.TabIndex = 192;
			this.label22.Text = "Maxillofacial Prosth";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(1,185);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(120,21);
			this.label21.TabIndex = 190;
			this.label21.Text = "Prosthodontics";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(1,138);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(120,21);
			this.label20.TabIndex = 188;
			this.label20.Text = "Oral Surgery";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(1,118);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(120,21);
			this.label19.TabIndex = 186;
			this.label19.Text = "Perio";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(1,98);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120,21);
			this.label18.TabIndex = 184;
			this.label18.Text = "Endo";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(1,51);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120,21);
			this.label9.TabIndex = 182;
			this.label9.Text = "Routine Preventive";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(1,165);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(120,21);
			this.label15.TabIndex = 180;
			this.label15.Text = "Crowns";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(1,78);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(120,21);
			this.label16.TabIndex = 178;
			this.label16.Text = "Restorative";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(1,31);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(120,21);
			this.label17.TabIndex = 176;
			this.label17.Text = "Diagnostic";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkCalYearMain
			// 
			this.checkCalYearMain.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCalYearMain.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkCalYearMain.Location = new System.Drawing.Point(87,107);
			this.checkCalYearMain.Name = "checkCalYearMain";
			this.checkCalYearMain.Size = new System.Drawing.Size(121,21);
			this.checkCalYearMain.TabIndex = 178;
			this.checkCalYearMain.Text = "Calendar Year";
			this.checkCalYearMain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkCalYearMain.UseVisualStyleBackColor = true;
			// 
			// panelSimple
			// 
			this.panelSimple.Controls.Add(this.label14);
			this.panelSimple.Controls.Add(this.textAnnualMaxFam);
			this.panelSimple.Controls.Add(this.textDeductibleFam);
			this.panelSimple.Controls.Add(this.textDeductPrevFam);
			this.panelSimple.Controls.Add(this.label13);
			this.panelSimple.Controls.Add(this.textAnnualMax);
			this.panelSimple.Controls.Add(this.checkCalYearMain);
			this.panelSimple.Controls.Add(this.label1);
			this.panelSimple.Controls.Add(this.textFlo);
			this.panelSimple.Controls.Add(this.label2);
			this.panelSimple.Controls.Add(this.groupBox4);
			this.panelSimple.Controls.Add(this.textDeductible);
			this.panelSimple.Controls.Add(this.groupBox3);
			this.panelSimple.Controls.Add(this.label3);
			this.panelSimple.Controls.Add(this.textDeductPrev);
			this.panelSimple.Controls.Add(this.groupBox1);
			this.panelSimple.Controls.Add(this.label4);
			this.panelSimple.Location = new System.Drawing.Point(-2,24);
			this.panelSimple.Name = "panelSimple";
			this.panelSimple.Size = new System.Drawing.Size(765,321);
			this.panelSimple.TabIndex = 179;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(3,7);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(735,13);
			this.label24.TabIndex = 180;
			this.label24.Text = "Please note that some fields are for informational purposes only, and do not affe" +
    "ct estimate calculations.  This includes fluoride, frequencies, and ortho max.";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(193,10);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(94,15);
			this.label13.TabIndex = 179;
			this.label13.Text = "Individual";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(290,10);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(94,15);
			this.label14.TabIndex = 183;
			this.label14.Text = "Family";
			this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// textAnnualMaxFam
			// 
			this.textAnnualMaxFam.Location = new System.Drawing.Point(291,26);
			this.textAnnualMaxFam.Name = "textAnnualMaxFam";
			this.textAnnualMaxFam.Size = new System.Drawing.Size(93,20);
			this.textAnnualMaxFam.TabIndex = 180;
			// 
			// textDeductibleFam
			// 
			this.textDeductibleFam.Location = new System.Drawing.Point(291,46);
			this.textDeductibleFam.Name = "textDeductibleFam";
			this.textDeductibleFam.Size = new System.Drawing.Size(93,20);
			this.textDeductibleFam.TabIndex = 181;
			// 
			// textDeductPrevFam
			// 
			this.textDeductPrevFam.Location = new System.Drawing.Point(291,66);
			this.textDeductPrevFam.Name = "textDeductPrevFam";
			this.textDeductPrevFam.Size = new System.Drawing.Size(93,20);
			this.textDeductPrevFam.TabIndex = 182;
			// 
			// textAnnualMax
			// 
			this.textAnnualMax.Location = new System.Drawing.Point(194,26);
			this.textAnnualMax.Name = "textAnnualMax";
			this.textAnnualMax.Size = new System.Drawing.Size(93,20);
			this.textAnnualMax.TabIndex = 162;
			// 
			// textFlo
			// 
			this.textFlo.Location = new System.Drawing.Point(194,86);
			this.textFlo.MaxVal = 255;
			this.textFlo.MinVal = 0;
			this.textFlo.Name = "textFlo";
			this.textFlo.Size = new System.Drawing.Size(39,20);
			this.textFlo.TabIndex = 177;
			// 
			// textStand4
			// 
			this.textStand4.Location = new System.Drawing.Point(189,178);
			this.textStand4.MaxVal = 255;
			this.textStand4.MinVal = 0;
			this.textStand4.Name = "textStand4";
			this.textStand4.Size = new System.Drawing.Size(60,20);
			this.textStand4.TabIndex = 180;
			this.textStand4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textStand4_KeyUp);
			// 
			// textAccident
			// 
			this.textAccident.Location = new System.Drawing.Point(123,233);
			this.textAccident.MaxVal = 255;
			this.textAccident.MinVal = 0;
			this.textAccident.Name = "textAccident";
			this.textAccident.Size = new System.Drawing.Size(60,20);
			this.textAccident.TabIndex = 195;
			// 
			// textStand2
			// 
			this.textStand2.Location = new System.Drawing.Point(189,109);
			this.textStand2.MaxVal = 255;
			this.textStand2.MinVal = 0;
			this.textStand2.Name = "textStand2";
			this.textStand2.Size = new System.Drawing.Size(60,20);
			this.textStand2.TabIndex = 179;
			this.textStand2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textStand2_KeyUp);
			// 
			// textMaxProsth
			// 
			this.textMaxProsth.Location = new System.Drawing.Point(123,213);
			this.textMaxProsth.MaxVal = 255;
			this.textMaxProsth.MinVal = 0;
			this.textMaxProsth.Name = "textMaxProsth";
			this.textMaxProsth.Size = new System.Drawing.Size(60,20);
			this.textMaxProsth.TabIndex = 193;
			// 
			// textStand1
			// 
			this.textStand1.Location = new System.Drawing.Point(189,42);
			this.textStand1.MaxVal = 255;
			this.textStand1.MinVal = 0;
			this.textStand1.Name = "textStand1";
			this.textStand1.Size = new System.Drawing.Size(60,20);
			this.textStand1.TabIndex = 177;
			this.textStand1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textStand1_KeyUp);
			// 
			// textProsth
			// 
			this.textProsth.Location = new System.Drawing.Point(123,186);
			this.textProsth.MaxVal = 255;
			this.textProsth.MinVal = 0;
			this.textProsth.Name = "textProsth";
			this.textProsth.Size = new System.Drawing.Size(60,20);
			this.textProsth.TabIndex = 191;
			// 
			// textOralSurg
			// 
			this.textOralSurg.Location = new System.Drawing.Point(123,139);
			this.textOralSurg.MaxVal = 255;
			this.textOralSurg.MinVal = 0;
			this.textOralSurg.Name = "textOralSurg";
			this.textOralSurg.Size = new System.Drawing.Size(60,20);
			this.textOralSurg.TabIndex = 189;
			// 
			// textPerio
			// 
			this.textPerio.Location = new System.Drawing.Point(123,119);
			this.textPerio.MaxVal = 255;
			this.textPerio.MinVal = 0;
			this.textPerio.Name = "textPerio";
			this.textPerio.Size = new System.Drawing.Size(60,20);
			this.textPerio.TabIndex = 187;
			// 
			// textEndo
			// 
			this.textEndo.Location = new System.Drawing.Point(123,99);
			this.textEndo.MaxVal = 255;
			this.textEndo.MinVal = 0;
			this.textEndo.Name = "textEndo";
			this.textEndo.Size = new System.Drawing.Size(60,20);
			this.textEndo.TabIndex = 185;
			// 
			// textRoutinePrev
			// 
			this.textRoutinePrev.Location = new System.Drawing.Point(123,52);
			this.textRoutinePrev.MaxVal = 255;
			this.textRoutinePrev.MinVal = 0;
			this.textRoutinePrev.Name = "textRoutinePrev";
			this.textRoutinePrev.Size = new System.Drawing.Size(60,20);
			this.textRoutinePrev.TabIndex = 183;
			// 
			// textCrowns
			// 
			this.textCrowns.Location = new System.Drawing.Point(123,166);
			this.textCrowns.MaxVal = 255;
			this.textCrowns.MinVal = 0;
			this.textCrowns.Name = "textCrowns";
			this.textCrowns.Size = new System.Drawing.Size(60,20);
			this.textCrowns.TabIndex = 181;
			// 
			// textRestorative
			// 
			this.textRestorative.Location = new System.Drawing.Point(123,79);
			this.textRestorative.MaxVal = 255;
			this.textRestorative.MinVal = 0;
			this.textRestorative.Name = "textRestorative";
			this.textRestorative.Size = new System.Drawing.Size(60,20);
			this.textRestorative.TabIndex = 179;
			// 
			// textDiagnostic
			// 
			this.textDiagnostic.Location = new System.Drawing.Point(123,32);
			this.textDiagnostic.MaxVal = 255;
			this.textDiagnostic.MinVal = 0;
			this.textDiagnostic.Name = "textDiagnostic";
			this.textDiagnostic.Size = new System.Drawing.Size(60,20);
			this.textDiagnostic.TabIndex = 177;
			// 
			// textDeductible
			// 
			this.textDeductible.Location = new System.Drawing.Point(194,46);
			this.textDeductible.Name = "textDeductible";
			this.textDeductible.Size = new System.Drawing.Size(93,20);
			this.textDeductible.TabIndex = 164;
			// 
			// textOrthoPercent
			// 
			this.textOrthoPercent.Location = new System.Drawing.Point(110,43);
			this.textOrthoPercent.MaxVal = 255;
			this.textOrthoPercent.MinVal = 0;
			this.textOrthoPercent.Name = "textOrthoPercent";
			this.textOrthoPercent.Size = new System.Drawing.Size(60,20);
			this.textOrthoPercent.TabIndex = 175;
			// 
			// textOrthoMax
			// 
			this.textOrthoMax.Location = new System.Drawing.Point(110,17);
			this.textOrthoMax.Name = "textOrthoMax";
			this.textOrthoMax.Size = new System.Drawing.Size(93,20);
			this.textOrthoMax.TabIndex = 164;
			// 
			// textDeductPrev
			// 
			this.textDeductPrev.Location = new System.Drawing.Point(194,66);
			this.textDeductPrev.Name = "textDeductPrev";
			this.textDeductPrev.Size = new System.Drawing.Size(93,20);
			this.textDeductPrev.TabIndex = 166;
			// 
			// textExams
			// 
			this.textExams.Location = new System.Drawing.Point(77,71);
			this.textExams.MaxVal = 255;
			this.textExams.MinVal = 0;
			this.textExams.Name = "textExams";
			this.textExams.Size = new System.Drawing.Size(39,20);
			this.textExams.TabIndex = 177;
			// 
			// textPano
			// 
			this.textPano.Location = new System.Drawing.Point(77,50);
			this.textPano.MaxVal = 255;
			this.textPano.MinVal = 0;
			this.textPano.Name = "textPano";
			this.textPano.Size = new System.Drawing.Size(39,20);
			this.textPano.TabIndex = 174;
			// 
			// textBW
			// 
			this.textBW.Location = new System.Drawing.Point(77,29);
			this.textBW.MaxVal = 255;
			this.textBW.MinVal = 0;
			this.textBW.Name = "textBW";
			this.textBW.Size = new System.Drawing.Size(39,20);
			this.textBW.TabIndex = 169;
			// 
			// textSubscNote
			// 
			this.textSubscNote.AcceptsReturn = true;
			this.textSubscNote.Location = new System.Drawing.Point(112,560);
			this.textSubscNote.Multiline = true;
			this.textSubscNote.Name = "textSubscNote";
			this.textSubscNote.QuickPasteType = OpenDentBusiness.QuickPasteType.InsPlan;
			this.textSubscNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textSubscNote.Size = new System.Drawing.Size(485,98);
			this.textSubscNote.TabIndex = 161;
			this.textSubscNote.Text = "1\r\n2\r\n3 lines will show here in 46 vert.\r\n4 lines will show here in 59 vert.\r\n5 l" +
    "ines in 72 vert\r\n6 lines in 85 vert\r\n7 lines in 98";
			// 
			// gridBenefits
			// 
			this.gridBenefits.HScrollVisible = false;
			this.gridBenefits.Location = new System.Drawing.Point(23,346);
			this.gridBenefits.Name = "gridBenefits";
			this.gridBenefits.ScrollValue = 0;
			this.gridBenefits.Size = new System.Drawing.Size(574,171);
			this.gridBenefits.TabIndex = 156;
			this.gridBenefits.Title = "Other Benefits";
			this.gridBenefits.TranslationName = "TableInsBenefits";
			this.gridBenefits.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBenefits_CellDoubleClick);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(122,524);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(96,26);
			this.butClear.TabIndex = 153;
			this.butClear.Text = "Clear All";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(23,524);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(90,26);
			this.butAdd.TabIndex = 152;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(719,591);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(719,632);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsBenefits
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(806,670);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.checkSimplified);
			this.Controls.Add(this.panelSimple);
			this.Controls.Add(this.textSubscNote);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.gridBenefits);
			this.Controls.Add(this.checkCalendarYear);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsBenefits";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Benefits";
			this.Load += new System.EventHandler(this.FormInsBenefits_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.panelSimple.ResumeLayout(false);
			this.panelSimple.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsBenefits_Load(object sender,EventArgs e) {
			benefitListAll=new List<Benefit>(OriginalBenList);
			if(CovCats.GetForEbenCat(EbenefitCategory.Accident)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Crowns)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Diagnostic)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Endodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.General)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.MaxillofacialProsth)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.OralSurgery)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Orthodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Periodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.Restorative)==null
				|| CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive)==null
				)
			{
				dontAllowSimplified=true;
				checkSimplified.Checked=false;
				panelSimple.Visible=false;
				gridBenefits.Location=new Point(gridBenefits.Left,checkSimplified.Bottom+3);
				gridBenefits.Height=butAdd.Top-gridBenefits.Top-5;
			}
			FillSimple();
			FillGrid();
			textSubscNote.Text=Note;
		}

		private void checkSimplified_Click(object sender,EventArgs e) {
			if(checkSimplified.Checked){
				if(dontAllowSimplified){
					checkSimplified.Checked=false;
					MsgBox.Show(this,"Not allowed to use simplified view until you fix your Insurance Categories from the setup menu.  At least one of each e-benefit category must be present.");
					return;
				}
				gridBenefits.Title=Lan.g(this,"Other Benefits");
				panelSimple.Visible=true;
				gridBenefits.Location=new Point(gridBenefits.Left,panelSimple.Bottom+4);
				gridBenefits.Height=butAdd.Top-gridBenefits.Top-5;
				//FillSimple handles all further logic.
			}
			else{
				if(!ConvertFormToBenefits()){
					checkSimplified.Checked=true;
					return;
				}
				gridBenefits.Title=Lan.g(this,"Benefits");
				panelSimple.Visible=false;
				gridBenefits.Location=new Point(gridBenefits.Left,checkSimplified.Bottom+3);
				gridBenefits.Height=butAdd.Top-gridBenefits.Top-5;
			}
			FillSimple();
			FillGrid();
		}

		///<summary>This will only be run when the form first opens or if user switches to simple view.  FillGrid should always be run after this.</summary>
		private void FillSimple(){
			if(!panelSimple.Visible){
				benefitList=new List<Benefit>(benefitListAll);
				return;
			}
			textAnnualMax.Text="";
			textDeductible.Text="";
			textDeductPrev.Text="";
			textAnnualMaxFam.Text="";
			textDeductibleFam.Text="";
			textDeductPrevFam.Text="";
			textFlo.Text="";
			checkCalYearMain.Checked=true;//default is calendar year unless a service year is found
			textBW.Text="";
			textPano.Text="";
			textExams.Text="";
			comboBW.SelectedIndex=0;
			comboPano.SelectedIndex=0;
			comboExams.SelectedIndex=0;
			textOrthoMax.Text="";
			textOrthoPercent.Text="";
			textStand1.Text="";
			textStand2.Text="";
			textStand4.Text="";
			//textStand3.Text="";
			textDiagnostic.Text="";
			textRoutinePrev.Text="";
			textRestorative.Text="";
			textEndo.Text="";
			textPerio.Text="";
			textOralSurg.Text="";
			textCrowns.Text="";
			textProsth.Text="";
			textMaxProsth.Text="";
			textAccident.Text="";
			benefitList=new List<Benefit>();
			Benefit ben;
			for(int i=0;i<benefitListAll.Count;i++){
				#region Loop
				ben=benefitListAll[i];
				//annual max individual
				if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Limitations
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Individual)
				{
					textAnnualMax.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear){
						checkCalYearMain.Checked=false;
					}
				}
				//annual max family
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Limitations
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Family) 
				{
					textAnnualMaxFam.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//deductible individual
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Deductible
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Individual)
				{
					textDeductible.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//deductible family
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Deductible
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Family) 
				{
					textDeductibleFam.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//deductible preventive individual
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Deductible
					&& (ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum
					|| ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum)
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Individual)
				{
					textDeductPrev.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//deductible preventive family
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Deductible
					&& (ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum
					|| ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum)
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)
					&& ben.CoverageLevel==BenefitCoverageLevel.Family) 
				{
					textDeductPrevFam.Text=ben.MonetaryAmt.ToString("n");
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Flo
				else if(ProcedureCodes.GetStringProcCode(ben.CodeNum)=="D1204"
					&& ben.BenefitType==InsBenefitType.Limitations
					//&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum//ignored
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Percent==0
					&& ben.QuantityQualifier==BenefitQuantity.AgeLimit)
					//&& ben.TimePeriod might be none or calYear, or ServiceYear.
				{
					textFlo.Text=ben.Quantity.ToString();
				}
				//BWs
				else if(ProcedureCodes.GetStringProcCode(ben.CodeNum)=="D0274"//4BW
					&& ben.BenefitType==InsBenefitType.Limitations
					//&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum//ignored
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Percent==0
					&& (ben.QuantityQualifier==BenefitQuantity.Months 
						|| ben.QuantityQualifier==BenefitQuantity.Years
						|| ben.QuantityQualifier==BenefitQuantity.NumberOfServices))
					//&& ben.TimePeriod might be none, or calYear, or ServiceYear, or Years.
				{
					textBW.Text=ben.Quantity.ToString();
					if(ben.QuantityQualifier==BenefitQuantity.Months){
						comboBW.SelectedIndex=2;
					}
					else if(ben.QuantityQualifier==BenefitQuantity.Years){
						comboBW.SelectedIndex=0;
					}
					else{
						comboBW.SelectedIndex=1;//# per year
					}
				}
				//Pano
				else if(ProcedureCodes.GetStringProcCode(ben.CodeNum)=="D0330"//Pano
					&& ben.BenefitType==InsBenefitType.Limitations
					//&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum//ignored
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Percent==0
					&& (ben.QuantityQualifier==BenefitQuantity.Months 
						|| ben.QuantityQualifier==BenefitQuantity.Years
						|| ben.QuantityQualifier==BenefitQuantity.NumberOfServices))
				//&& ben.TimePeriod might be none, or calYear, or ServiceYear, or Years.
				{
					textPano.Text=ben.Quantity.ToString();
					if(ben.QuantityQualifier==BenefitQuantity.Months) {
						comboPano.SelectedIndex=2;
					}
					else if(ben.QuantityQualifier==BenefitQuantity.Years) {
						comboPano.SelectedIndex=0;
					}
					else {
						comboPano.SelectedIndex=1;//# per year
					}
				}
				//Exams
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Limitations
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Percent==0
					&& (ben.QuantityQualifier==BenefitQuantity.Months 
						|| ben.QuantityQualifier==BenefitQuantity.Years
						|| ben.QuantityQualifier==BenefitQuantity.NumberOfServices))
				//&& ben.TimePeriod might be none, or calYear, or ServiceYear, or Years.
				{
					textExams.Text=ben.Quantity.ToString();
					if(ben.QuantityQualifier==BenefitQuantity.Months) {
						comboExams.SelectedIndex=2;
					}
					else if(ben.QuantityQualifier==BenefitQuantity.Years) {
						comboExams.SelectedIndex=0;
					}
					else {
						comboExams.SelectedIndex=1;//# per year
					}
				}
				//OrthoMax
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Limitations
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum
					&& ben.PatPlanNum==0
					&& ben.Percent==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& ben.TimePeriod==BenefitTimePeriod.Lifetime)
				{
					textOrthoMax.Text=ben.MonetaryAmt.ToString("n");
				}
				//OrthoPercent
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear))
				{
					textOrthoPercent.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear){
						checkCalYearMain.Checked=false;
					}
				}
				//Stand1
				//Stand2
				//Stand4
				//Diagnostic
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear))
				{
					textDiagnostic.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//RoutinePreventive
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) 
				{
					textRoutinePrev.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Restorative
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) 
				{
					textRestorative.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Endo
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) 
				{
					textEndo.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Perio
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textPerio.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//OralSurg
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.OralSurgery).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textOralSurg.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Crowns
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Crowns).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textCrowns.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Prosth
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textProsth.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//MaxProsth
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.MaxillofacialProsth).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textMaxProsth.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//Accident
				else if(ben.CodeNum==0
					&& ben.BenefitType==InsBenefitType.Percentage
					&& ben.CovCatNum==CovCats.GetForEbenCat(EbenefitCategory.Accident).CovCatNum
					&& ben.MonetaryAmt==0
					&& ben.PatPlanNum==0
					&& ben.Quantity==0
					&& ben.QuantityQualifier==BenefitQuantity.None
					&& (ben.TimePeriod==BenefitTimePeriod.CalendarYear	|| ben.TimePeriod==BenefitTimePeriod.ServiceYear)) {
					textAccident.Text=ben.Percent.ToString();
					if(ben.TimePeriod==BenefitTimePeriod.ServiceYear) {
						checkCalYearMain.Checked=false;
					}
				}
				//any benefit that didn't get handled above
				else{
					benefitList.Add(ben);
				}
				#endregion Loop
			}
			if(textDiagnostic.Text !="" && textDiagnostic.Text==textRoutinePrev.Text){
				textStand1.Text=textDiagnostic.Text;
			}
			if(textRestorative.Text !="" && textRestorative.Text==textEndo.Text 
				&& textRestorative.Text==textPerio.Text	&& textRestorative.Text==textOralSurg.Text)
			{
				textStand2.Text=textRestorative.Text;
			}
			if(textCrowns.Text !="" && textCrowns.Text==textProsth.Text) {
				textStand4.Text=textCrowns.Text;
			}
		}

		private void textStand1_KeyUp(object sender,KeyEventArgs e) {
			textDiagnostic.Text=textStand1.Text;
			textRoutinePrev.Text=textStand1.Text;
		}

		private void textStand2_KeyUp(object sender,KeyEventArgs e) {
			textRestorative.Text=textStand2.Text;
			textEndo.Text=textStand2.Text;
			textPerio.Text=textStand2.Text;
			textOralSurg.Text=textStand2.Text;
		}

		private void textStand4_KeyUp(object sender,KeyEventArgs e) {
			textCrowns.Text=textStand4.Text;
			textProsth.Text=textStand4.Text;
		}

		///<summary>This only fills the grid on the screen.  It does not get any data from the database.</summary>
		private void FillGrid() {
			benefitList.Sort();
			gridBenefits.BeginUpdate();
			gridBenefits.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Pat",35);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Level",60);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Type",90);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Category",90);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("%",35);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Amt",50);//,HorizontalAlignment.Right);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Time Period",80);
			gridBenefits.Columns.Add(col);
			col=new ODGridColumn("Quantity",115);
			gridBenefits.Columns.Add(col);
			gridBenefits.Rows.Clear();
			ODGridRow row;
			bool allCalendarYear=true;
			bool allServiceYear=true;
			for(int i=0;i<benefitList.Count;i++) {
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear) {
					allServiceYear=false;
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear) {
					allCalendarYear=false;
				}
				row=new ODGridRow();
				if(((Benefit)benefitList[i]).PatPlanNum==0) {//attached to plan
					row.Cells.Add("");
				}
				else {
					row.Cells.Add("X");
				}
				if(benefitList[i].CoverageLevel==BenefitCoverageLevel.Individual){
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(Lan.g("enumBenefitCoverageLevel",benefitList[i].CoverageLevel.ToString()));
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage) {
					row.Cells.Add("%");
				}
				else if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Limitations
					&& (((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear
					|| ((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear)
					&& ((Benefit)benefitList[i]).QuantityQualifier==BenefitQuantity.None) {//annual max
					row.Cells.Add(Lan.g(this,"Annual Max"));
				}
				else {
					row.Cells.Add(Lan.g("enumInsBenefitType",((Benefit)benefitList[i]).BenefitType.ToString()));
				}
				if(((Benefit)benefitList[i]).CodeNum==0) {
					row.Cells.Add(CovCats.GetDesc(((Benefit)benefitList[i]).CovCatNum));
				}
				else {
					row.Cells.Add(ProcedureCodes.GetProcCode(((Benefit)benefitList[i]).CodeNum).AbbrDesc);
				}
				if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Percentage) {
					row.Cells.Add(((Benefit)benefitList[i]).Percent.ToString());
				}
				else {
					row.Cells.Add("");
				}
				if(((Benefit)benefitList[i]).MonetaryAmt==0) {
					if(((Benefit)benefitList[i]).BenefitType==InsBenefitType.Deductible) {
						row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
					}
					else {
						row.Cells.Add("");
					}
				}
				else {
					row.Cells.Add(((Benefit)benefitList[i]).MonetaryAmt.ToString("n0"));
				}
				if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.None) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(Lan.g("enumBenefitTimePeriod",((Benefit)benefitList[i]).TimePeriod.ToString()));
				}
				if(((Benefit)benefitList[i]).Quantity>0) {
					row.Cells.Add(((Benefit)benefitList[i]).Quantity.ToString()+" "
						+Lan.g("enumBenefitQuantity",((Benefit)benefitList[i]).QuantityQualifier.ToString()));
				}
				else {
					row.Cells.Add("");
				}
				gridBenefits.Rows.Add(row);
			}
			gridBenefits.EndUpdate();
			if(allCalendarYear){
				checkCalendarYear.CheckState=CheckState.Checked;
			}
			else if(allServiceYear){
				checkCalendarYear.CheckState=CheckState.Unchecked;
			}
			else{
				checkCalendarYear.CheckState=CheckState.Indeterminate;
			}
		}

		private void gridBenefits_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e){
			FormBenefitEdit FormB=new FormBenefitEdit(PatPlanNum,PlanNum);
			FormB.BenCur=(Benefit)benefitList[e.Row];
			FormB.ShowDialog();
			if(FormB.BenCur==null){//user deleted
				benefitList.RemoveAt(e.Row);
			}
			FillGrid();
		}

		private void checkCalendarYear_Click(object sender,EventArgs e) {
			//checkstate will have already changed.
			if(checkCalendarYear.CheckState==CheckState.Checked){//change all to calendarYear
				for(int i=0;i<benefitList.Count;i++){
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.ServiceYear){
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.CalendarYear;
					}
				}
			}
			if(checkCalendarYear.CheckState==CheckState.Indeterminate
				|| checkCalendarYear.CheckState==CheckState.Unchecked) {//change all to serviceYear
				for(int i=0;i<benefitList.Count;i++) {
					if(((Benefit)benefitList[i]).TimePeriod==BenefitTimePeriod.CalendarYear) {
						((Benefit)benefitList[i]).TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			Benefit ben=new Benefit();
			ben.PlanNum=PlanNum;
			if(checkCalendarYear.CheckState==CheckState.Checked) {
				ben.TimePeriod=BenefitTimePeriod.CalendarYear;
			}
			if(checkCalendarYear.CheckState==CheckState.Unchecked) {
				ben.TimePeriod=BenefitTimePeriod.ServiceYear;
			}
			if(CovCatB.ListShort.Length>0){
				ben.CovCatNum=CovCatB.ListShort[0].CovCatNum;
			}
			ben.BenefitType=InsBenefitType.Percentage;
			FormBenefitEdit FormB=new FormBenefitEdit(PatPlanNum,PlanNum);
			FormB.IsNew=true;
			FormB.BenCur=ben;
			FormB.ShowDialog();
			if(FormB.DialogResult==DialogResult.OK){
				benefitList.Add(FormB.BenCur);	
			}
			FillGrid();
		}

		private void butClear_Click(object sender,EventArgs e) {
			benefitList=new List<Benefit>();
			FillGrid();
		}

		///<summary>Only called if in simple view.  This takes all the data on the form and converts it to benefit items.  A new benefitListAll is created based on a combination of benefitList and the new items from the form.  This is used when clicking OK from simple view, or when switching from simple view to complex view.</summary>
		private bool ConvertFormToBenefits(){
			if(textAnnualMax.errorProvider1.GetError(textAnnualMax) != ""
				|| textDeductible.errorProvider1.GetError(textDeductible) != ""
				|| textDeductPrev.errorProvider1.GetError(textDeductPrev) != ""
				|| textAnnualMaxFam.errorProvider1.GetError(textAnnualMaxFam) != ""
				|| textDeductibleFam.errorProvider1.GetError(textDeductibleFam) != ""
				|| textDeductPrevFam.errorProvider1.GetError(textDeductPrevFam) != ""
				|| textFlo.errorProvider1.GetError(textFlo) != ""
				|| textBW.errorProvider1.GetError(textBW) != ""
				|| textPano.errorProvider1.GetError(textPano) != ""
				|| textExams.errorProvider1.GetError(textExams) != ""
				|| textOrthoMax.errorProvider1.GetError(textOrthoMax) != ""
				|| textOrthoPercent.errorProvider1.GetError(textOrthoPercent) != ""
				|| textStand1.errorProvider1.GetError(textStand1) != ""
				|| textStand2.errorProvider1.GetError(textStand2) != ""
				|| textStand4.errorProvider1.GetError(textStand4) != ""
				|| textDiagnostic.errorProvider1.GetError(textDiagnostic) != ""
				|| textRoutinePrev.errorProvider1.GetError(textRoutinePrev) != ""
				|| textRestorative.errorProvider1.GetError(textRestorative) != ""
				|| textEndo.errorProvider1.GetError(textEndo) != ""
				|| textPerio.errorProvider1.GetError(textPerio) != ""
				|| textOralSurg.errorProvider1.GetError(textOralSurg) != ""
				|| textCrowns.errorProvider1.GetError(textCrowns) != ""
				|| textProsth.errorProvider1.GetError(textProsth) != ""
				|| textMaxProsth.errorProvider1.GetError(textMaxProsth) != ""
				|| textAccident.errorProvider1.GetError(textAccident) != "")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			benefitListAll=new List<Benefit>(benefitList);
			Benefit ben;
			//annual max individual
			if(textAnnualMax.Text !=""){
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked){
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else{
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;					
				}
				ben.MonetaryAmt=PIn.PDouble(textAnnualMax.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Individual;
				benefitListAll.Add(ben);
			}
			//annual max family
			if(textAnnualMaxFam.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				ben.MonetaryAmt=PIn.PDouble(textAnnualMaxFam.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Family;
				benefitListAll.Add(ben);
			}
			//deductible individual
			if(textDeductible.Text !=""){
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked){
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else{
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;					
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductible.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Individual;
				benefitListAll.Add(ben);
			}
			//deductible family
			if(textDeductibleFam.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductibleFam.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Family;
				benefitListAll.Add(ben);
			}
			//deductible preventive individual
			if(textDeductPrev.Text !=""){
				//diagnostic
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked){
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else{
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;					
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductPrev.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Individual;
				benefitListAll.Add(ben);
				//preventive
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductPrev.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Individual;
				benefitListAll.Add(ben);
			}
			//deductible preventive family
			if(textDeductPrevFam.Text !="") {
				//diagnostic
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductPrevFam.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Family;
				benefitListAll.Add(ben);
				//preventive
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				ben.MonetaryAmt=PIn.PDouble(textDeductPrevFam.Text);
				ben.CoverageLevel=BenefitCoverageLevel.Family;
				benefitListAll.Add(ben);
			}
			//Flo
			if(textFlo.Text !=""){
				ben=new Benefit();
				ben.CodeNum=ProcedureCodes.GetCodeNum("D1204");
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;//ignored
				ben.PlanNum=PlanNum;
				ben.QuantityQualifier=BenefitQuantity.AgeLimit;
				ben.Quantity=PIn.PInt(textFlo.Text);
				benefitListAll.Add(ben);
			}
			//frequency BW
			if(textBW.Text !="") {
				ben=new Benefit();
				ben.CodeNum=ProcedureCodes.GetCodeNum("D0274");//4BW
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;//ignored
				ben.PlanNum=PlanNum;
				if(comboBW.SelectedIndex==0){
					ben.QuantityQualifier=BenefitQuantity.Years;
				}
				else if(comboBW.SelectedIndex==1){
					ben.QuantityQualifier=BenefitQuantity.NumberOfServices;
					if(checkCalYearMain.Checked) {
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					}
					else {
						ben.TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
				else if(comboBW.SelectedIndex==2){
					ben.QuantityQualifier=BenefitQuantity.Months;
				}
				ben.Quantity=PIn.PInt(textBW.Text);
				//ben.TimePeriod is none for years or months, although calYear, or ServiceYear, or Years might work too
				benefitListAll.Add(ben);
			}
			//Frequency pano
			if(textPano.Text !="") {
				ben=new Benefit();
				ben.CodeNum=ProcedureCodes.GetCodeNum("D0330");//Pano
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.General).CovCatNum;//ignored
				ben.PlanNum=PlanNum;
				if(comboPano.SelectedIndex==0) {
					ben.QuantityQualifier=BenefitQuantity.Years;
				}
				else if(comboPano.SelectedIndex==1) {
					ben.QuantityQualifier=BenefitQuantity.NumberOfServices;
					if(checkCalYearMain.Checked) {
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					}
					else {
						ben.TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
				else if(comboPano.SelectedIndex==2) {
					ben.QuantityQualifier=BenefitQuantity.Months;
				}
				ben.Quantity=PIn.PInt(textPano.Text);
				//ben.TimePeriod is none for years or months, although calYear, or ServiceYear, or Years might work too
				benefitListAll.Add(ben);
			}
			//Frequency exam
			if(textExams.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
				ben.PlanNum=PlanNum;
				if(comboExams.SelectedIndex==0) {
					ben.QuantityQualifier=BenefitQuantity.Years;
				}
				else if(comboExams.SelectedIndex==1) {
					ben.QuantityQualifier=BenefitQuantity.NumberOfServices;
					if(checkCalYearMain.Checked) {
						ben.TimePeriod=BenefitTimePeriod.CalendarYear;
					}
					else {
						ben.TimePeriod=BenefitTimePeriod.ServiceYear;
					}
				}
				else if(comboExams.SelectedIndex==2) {
					ben.QuantityQualifier=BenefitQuantity.Months;
				}
				ben.Quantity=PIn.PInt(textExams.Text);
				//ben.TimePeriod is none for years or months, although calYear, or ServiceYear, or Years might work too
				benefitListAll.Add(ben);
			}
			//ortho max
			if(textOrthoMax.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
				ben.PlanNum=PlanNum;
				ben.TimePeriod=BenefitTimePeriod.Lifetime;
				ben.MonetaryAmt=PIn.PDouble(textOrthoMax.Text);
				benefitListAll.Add(ben);
			}
			//ortho percent
			if(textOrthoPercent.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Orthodontics).CovCatNum;
				ben.Percent=PIn.PInt(textOrthoPercent.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Diagnostic
			if(textDiagnostic.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Diagnostic).CovCatNum;
				ben.Percent=PIn.PInt(textDiagnostic.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//RoutinePreventive
			if(textRoutinePrev.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.RoutinePreventive).CovCatNum;
				ben.Percent=PIn.PInt(textRoutinePrev.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Restorative
			if(textRestorative.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Restorative).CovCatNum;
				ben.Percent=PIn.PInt(textRestorative.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Endo
			if(textEndo.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Endodontics).CovCatNum;
				ben.Percent=PIn.PInt(textEndo.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Perio
			if(textPerio.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Periodontics).CovCatNum;
				ben.Percent=PIn.PInt(textPerio.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//OralSurg
			if(textOralSurg.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.OralSurgery).CovCatNum;
				ben.Percent=PIn.PInt(textOralSurg.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Crowns
			if(textCrowns.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Crowns).CovCatNum;
				ben.Percent=PIn.PInt(textCrowns.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Prosth
			if(textProsth.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Prosthodontics).CovCatNum;
				ben.Percent=PIn.PInt(textProsth.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//MaxProsth
			if(textMaxProsth.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.MaxillofacialProsth).CovCatNum;
				ben.Percent=PIn.PInt(textMaxProsth.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			//Accident
			if(textAccident.Text !="") {
				ben=new Benefit();
				ben.CodeNum=0;
				ben.BenefitType=InsBenefitType.Percentage;
				ben.CovCatNum=CovCats.GetForEbenCat(EbenefitCategory.Accident).CovCatNum;
				ben.Percent=PIn.PInt(textAccident.Text);
				ben.PlanNum=PlanNum;
				if(checkCalYearMain.Checked) {
					ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				}
				else {
					ben.TimePeriod=BenefitTimePeriod.ServiceYear;
				}
				benefitListAll.Add(ben);
			}
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(panelSimple.Visible){
				if(!ConvertFormToBenefits()){
					return;
				}
				OriginalBenList.Clear();
				for(int i=0;i<benefitListAll.Count;i++){
					OriginalBenList.Add(benefitListAll[i]);
				}
			}
			else{//not simple view
				OriginalBenList.Clear();
				for(int i=0;i<benefitList.Count;i++){
					OriginalBenList.Add(benefitList[i]);
				}
			}
			Note=textSubscNote.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		


	}
}





















