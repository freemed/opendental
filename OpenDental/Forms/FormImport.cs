using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
		public class FormImport:System.Windows.Forms.Form {
		/// <summary>
		/// Required designer variable.
		/// </summary>
				private System.ComponentModel.Container components = null;
				private DataTable table;
		///<summary>The name of the column which is the primary key, or "" for no primary key.</summary>
				private string pkCol="";
				private string[] AllowedColNames;
				private DataGrid grid;
				private TabPage tabFinalImport;
				private RadioButton radioUpdate;
				private RadioButton radioInsert;
				private Button butImport;
				private Button butValidate;
				private TabPage tabScript;
				private Label label47;
				private Button butReloadScript;
				private Button butSaveScript;
				private Button butRunScript;
				private TextBox textScript;
				private TabPage tabEdit;
				private GroupBox groupBox12;
				private ComboBox comboColCopyFrom;
				private Button butColCopy;
				private Label label34;
				private GroupBox groupBox11;
				private TextBox textPadLength;
				private Label label32;
				private TextBox textPadChar;
				private Label label31;
				private Button butColPad;
				private GroupBox groupBox10;
				private Label label36;
				private Button butColCapAll;
				private Label label26;
				private Button butColCap;
				private GroupBox groupBox9;
				private Button butColClear;
				private GroupBox groupBox1;
				private Button butSubstitute;
				private TextBox textSubstNew;
				private Label label5;
				private TextBox textSubstOld;
				private Label label4;
				private Label label3;
				private ComboBox comboColEdit;
				private TabPage tabRows;
				private Label label23;
				private TextBox textRows;
				private Label label12;
				private Button butDeleteRows;
				private TabPage tabQuery;
				private Label label28;
				private Button butQuerySubmit;
				private TextBox textQuery;
				private TabPage tabColumns;
				private GroupBox groupBox14;
				private TextBox textColsDelete;
				private Button butColsDelete;
				private Label label37;
				private GroupBox groupBox8;
				private ComboBox comboColAdd;
				private Button butAddCol;
				private Label label24;
				private GroupBox groupBox2;
				private ComboBox comboColDelete;
				private Button butDeleteCol;
				private Label label16;
				private GroupBox groupBox6;
				private RadioButton radioAfter;
				private RadioButton radioFirst;
				private Button butMoveCol;
				private Label label15;
				private ComboBox comboColMove;
				private ComboBox comboColMoveAfter;
				private GroupBox groupBox4;
				private ComboBox comboColCombine2;
				private ComboBox comboColCombine1;
				private Button butCombine;
				private TextBox textSepChar;
				private Label label9;
				private Label label10;
				private Label label11;
				private GroupBox groupBox5;
				private ComboBox comboColNewName;
				private ComboBox comboColRename;
				private Button butRename;
				private Label label13;
				private Label label14;
				private TabPage tabPK;
				private CheckBox checkDontUsePK;
				private Label label25;
				private GroupBox groupBox7;
				private RadioButton radioPatients;
				private RadioButton radioCarriers;
				private Button butClearPK;
				private ComboBox comboColPK;
				private Label label1;
				private Button butSetPK;
				private TabPage tabSpecialtyF;
				private GroupBox groupBox15;
				private TextBox textBox7;
				private Label label46;
				private Label label45;
				private TextBox textBox5;
				private Label label43;
				private TextBox textBox6;
				private Label label44;
				private Button butGuarAccount;
				private TextBox textBox4;
				private Label label41;
				private TextBox textBox3;
				private Label label40;
				private TextBox textBox2;
				private Label label38;
				private Label label39;
				private Button butGuarantor;
				private GroupBox groupBox13;
				private ComboBox comboColDateTo;
				private Label label29;
				private Label label30;
				private Button butConvertDate;
				private TextBox textDateBase;
				private Label label33;
				private Label label35;
				private ComboBox comboColDateFrom;
				private GroupBox groupBox3;
				private TextBox textDateNewFormat;
				private Label label27;
				private ComboBox comboColDateDest;
				private Label label17;
				private Label label6;
				private Button butFixDates;
				private TextBox textDateOldFormats;
				private Label label7;
				private Label label8;
				private ComboBox comboColDateSource;
				private TabPage tabLoadData;
				private GroupBox groupBox16;
				private RadioButton radioGenerateColNames;
				private RadioButton radioColNamRow;
				private Label label42;
				private ComboBox comboTableName;
				private Button butDeleteTable;
				private Label label22;
				private Label label21;
				private Button butLoad;
				private Button butBrowse;
				private Label label20;
				private TextBox textFileName;
				private TextBox textNewTable;
				private TextBox textBox1;
				private Label label19;
				private Label label18;
				private Button butRefresh;
				private Label label2;
				private TabControl tabContr;
				private TabPage tabTable;
				private GroupBox groupBox17;
				private Label label49;
				private NumericUpDown upDownNumTempCols;
				private TextBox textCreateTempName;
				private TextBox textBox9;
				private Label label48;
				private Button butCreateTemporaryTable;
				private GroupBox groupBox19;
				private Label label51;
				private Label label50;
				private Button butCopyAppendRows;
				private Button butToTableNoColumn;
				private Button butToTableDown;
				private Button butToTableUp;
				private Button butFromTableNoColumn;
				private Button butFromTableDown;
				private Button butFromTableUp;
				private ListBox listCopyToTable;
				private ListBox listCopyFromTable;
				private ComboBox comboCopyToTable;
				private ComboBox comboCopyFromTable;
				private Label label53;
				private Label label52;
				private GroupBox groupBox18;
				private Label label54;
				private NumericUpDown upDownCopyFromInc;
				private Label label55;
				private NumericUpDown upDownCopyToInc;
				private GroupBox groupBox20;
				private ComboBox comboColUnused;
				private Button butColMarkUnused;
				private Label label56;
				private GroupBox groupBox21;
				private ListBox listDupRowCols;
				private ComboBox comboFindDups;
				private Button button1;
				private TextBox textDupRowTable;
				private TextBox textBox10;
				private Label label57;
				private Label label59;
				private Label label58;
				private CheckBox checkFindDupsInsensitive;
				private GroupBox groupBox22;
				private Label label60;
				private ComboBox comboDeleteExDupsTable;
				private Button butDeleteExDups;
		private string currentDb;

		///<summary></summary>
		public FormImport()
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
				System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport));
				this.grid = new System.Windows.Forms.DataGrid();
				this.tabFinalImport = new System.Windows.Forms.TabPage();
				this.radioUpdate = new System.Windows.Forms.RadioButton();
				this.radioInsert = new System.Windows.Forms.RadioButton();
				this.butImport = new System.Windows.Forms.Button();
				this.butValidate = new System.Windows.Forms.Button();
				this.tabScript = new System.Windows.Forms.TabPage();
				this.label47 = new System.Windows.Forms.Label();
				this.butReloadScript = new System.Windows.Forms.Button();
				this.butSaveScript = new System.Windows.Forms.Button();
				this.butRunScript = new System.Windows.Forms.Button();
				this.textScript = new System.Windows.Forms.TextBox();
				this.tabEdit = new System.Windows.Forms.TabPage();
				this.groupBox12 = new System.Windows.Forms.GroupBox();
				this.comboColCopyFrom = new System.Windows.Forms.ComboBox();
				this.butColCopy = new System.Windows.Forms.Button();
				this.label34 = new System.Windows.Forms.Label();
				this.groupBox11 = new System.Windows.Forms.GroupBox();
				this.textPadLength = new System.Windows.Forms.TextBox();
				this.label32 = new System.Windows.Forms.Label();
				this.textPadChar = new System.Windows.Forms.TextBox();
				this.label31 = new System.Windows.Forms.Label();
				this.butColPad = new System.Windows.Forms.Button();
				this.groupBox10 = new System.Windows.Forms.GroupBox();
				this.label36 = new System.Windows.Forms.Label();
				this.butColCapAll = new System.Windows.Forms.Button();
				this.label26 = new System.Windows.Forms.Label();
				this.butColCap = new System.Windows.Forms.Button();
				this.groupBox9 = new System.Windows.Forms.GroupBox();
				this.butColClear = new System.Windows.Forms.Button();
				this.groupBox1 = new System.Windows.Forms.GroupBox();
				this.butSubstitute = new System.Windows.Forms.Button();
				this.textSubstNew = new System.Windows.Forms.TextBox();
				this.label5 = new System.Windows.Forms.Label();
				this.textSubstOld = new System.Windows.Forms.TextBox();
				this.label4 = new System.Windows.Forms.Label();
				this.label3 = new System.Windows.Forms.Label();
				this.comboColEdit = new System.Windows.Forms.ComboBox();
				this.tabRows = new System.Windows.Forms.TabPage();
				this.groupBox22 = new System.Windows.Forms.GroupBox();
				this.butDeleteExDups = new System.Windows.Forms.Button();
				this.label60 = new System.Windows.Forms.Label();
				this.comboDeleteExDupsTable = new System.Windows.Forms.ComboBox();
				this.groupBox21 = new System.Windows.Forms.GroupBox();
				this.checkFindDupsInsensitive = new System.Windows.Forms.CheckBox();
				this.label59 = new System.Windows.Forms.Label();
				this.label58 = new System.Windows.Forms.Label();
				this.button1 = new System.Windows.Forms.Button();
				this.textDupRowTable = new System.Windows.Forms.TextBox();
				this.textBox10 = new System.Windows.Forms.TextBox();
				this.label57 = new System.Windows.Forms.Label();
				this.listDupRowCols = new System.Windows.Forms.ListBox();
				this.comboFindDups = new System.Windows.Forms.ComboBox();
				this.groupBox18 = new System.Windows.Forms.GroupBox();
				this.label12 = new System.Windows.Forms.Label();
				this.butDeleteRows = new System.Windows.Forms.Button();
				this.label23 = new System.Windows.Forms.Label();
				this.textRows = new System.Windows.Forms.TextBox();
				this.groupBox19 = new System.Windows.Forms.GroupBox();
				this.label55 = new System.Windows.Forms.Label();
				this.upDownCopyToInc = new System.Windows.Forms.NumericUpDown();
				this.label54 = new System.Windows.Forms.Label();
				this.upDownCopyFromInc = new System.Windows.Forms.NumericUpDown();
				this.label51 = new System.Windows.Forms.Label();
				this.label50 = new System.Windows.Forms.Label();
				this.butCopyAppendRows = new System.Windows.Forms.Button();
				this.butToTableNoColumn = new System.Windows.Forms.Button();
				this.butToTableDown = new System.Windows.Forms.Button();
				this.butToTableUp = new System.Windows.Forms.Button();
				this.butFromTableNoColumn = new System.Windows.Forms.Button();
				this.butFromTableDown = new System.Windows.Forms.Button();
				this.butFromTableUp = new System.Windows.Forms.Button();
				this.listCopyToTable = new System.Windows.Forms.ListBox();
				this.listCopyFromTable = new System.Windows.Forms.ListBox();
				this.comboCopyToTable = new System.Windows.Forms.ComboBox();
				this.comboCopyFromTable = new System.Windows.Forms.ComboBox();
				this.label53 = new System.Windows.Forms.Label();
				this.label52 = new System.Windows.Forms.Label();
				this.tabQuery = new System.Windows.Forms.TabPage();
				this.label28 = new System.Windows.Forms.Label();
				this.butQuerySubmit = new System.Windows.Forms.Button();
				this.textQuery = new System.Windows.Forms.TextBox();
				this.tabColumns = new System.Windows.Forms.TabPage();
				this.groupBox20 = new System.Windows.Forms.GroupBox();
				this.comboColUnused = new System.Windows.Forms.ComboBox();
				this.butColMarkUnused = new System.Windows.Forms.Button();
				this.label56 = new System.Windows.Forms.Label();
				this.groupBox14 = new System.Windows.Forms.GroupBox();
				this.textColsDelete = new System.Windows.Forms.TextBox();
				this.butColsDelete = new System.Windows.Forms.Button();
				this.label37 = new System.Windows.Forms.Label();
				this.groupBox8 = new System.Windows.Forms.GroupBox();
				this.comboColAdd = new System.Windows.Forms.ComboBox();
				this.butAddCol = new System.Windows.Forms.Button();
				this.label24 = new System.Windows.Forms.Label();
				this.groupBox2 = new System.Windows.Forms.GroupBox();
				this.comboColDelete = new System.Windows.Forms.ComboBox();
				this.butDeleteCol = new System.Windows.Forms.Button();
				this.label16 = new System.Windows.Forms.Label();
				this.groupBox6 = new System.Windows.Forms.GroupBox();
				this.radioAfter = new System.Windows.Forms.RadioButton();
				this.radioFirst = new System.Windows.Forms.RadioButton();
				this.butMoveCol = new System.Windows.Forms.Button();
				this.label15 = new System.Windows.Forms.Label();
				this.comboColMove = new System.Windows.Forms.ComboBox();
				this.comboColMoveAfter = new System.Windows.Forms.ComboBox();
				this.groupBox4 = new System.Windows.Forms.GroupBox();
				this.comboColCombine2 = new System.Windows.Forms.ComboBox();
				this.comboColCombine1 = new System.Windows.Forms.ComboBox();
				this.butCombine = new System.Windows.Forms.Button();
				this.textSepChar = new System.Windows.Forms.TextBox();
				this.label9 = new System.Windows.Forms.Label();
				this.label10 = new System.Windows.Forms.Label();
				this.label11 = new System.Windows.Forms.Label();
				this.groupBox5 = new System.Windows.Forms.GroupBox();
				this.comboColNewName = new System.Windows.Forms.ComboBox();
				this.comboColRename = new System.Windows.Forms.ComboBox();
				this.butRename = new System.Windows.Forms.Button();
				this.label13 = new System.Windows.Forms.Label();
				this.label14 = new System.Windows.Forms.Label();
				this.tabPK = new System.Windows.Forms.TabPage();
				this.checkDontUsePK = new System.Windows.Forms.CheckBox();
				this.label25 = new System.Windows.Forms.Label();
				this.groupBox7 = new System.Windows.Forms.GroupBox();
				this.radioPatients = new System.Windows.Forms.RadioButton();
				this.radioCarriers = new System.Windows.Forms.RadioButton();
				this.butClearPK = new System.Windows.Forms.Button();
				this.comboColPK = new System.Windows.Forms.ComboBox();
				this.label1 = new System.Windows.Forms.Label();
				this.butSetPK = new System.Windows.Forms.Button();
				this.tabSpecialtyF = new System.Windows.Forms.TabPage();
				this.groupBox15 = new System.Windows.Forms.GroupBox();
				this.textBox7 = new System.Windows.Forms.TextBox();
				this.label46 = new System.Windows.Forms.Label();
				this.label45 = new System.Windows.Forms.Label();
				this.textBox5 = new System.Windows.Forms.TextBox();
				this.label43 = new System.Windows.Forms.Label();
				this.textBox6 = new System.Windows.Forms.TextBox();
				this.label44 = new System.Windows.Forms.Label();
				this.butGuarAccount = new System.Windows.Forms.Button();
				this.textBox4 = new System.Windows.Forms.TextBox();
				this.label41 = new System.Windows.Forms.Label();
				this.textBox3 = new System.Windows.Forms.TextBox();
				this.label40 = new System.Windows.Forms.Label();
				this.textBox2 = new System.Windows.Forms.TextBox();
				this.label38 = new System.Windows.Forms.Label();
				this.label39 = new System.Windows.Forms.Label();
				this.butGuarantor = new System.Windows.Forms.Button();
				this.groupBox13 = new System.Windows.Forms.GroupBox();
				this.comboColDateTo = new System.Windows.Forms.ComboBox();
				this.label29 = new System.Windows.Forms.Label();
				this.label30 = new System.Windows.Forms.Label();
				this.butConvertDate = new System.Windows.Forms.Button();
				this.textDateBase = new System.Windows.Forms.TextBox();
				this.label33 = new System.Windows.Forms.Label();
				this.label35 = new System.Windows.Forms.Label();
				this.comboColDateFrom = new System.Windows.Forms.ComboBox();
				this.groupBox3 = new System.Windows.Forms.GroupBox();
				this.textDateNewFormat = new System.Windows.Forms.TextBox();
				this.label27 = new System.Windows.Forms.Label();
				this.comboColDateDest = new System.Windows.Forms.ComboBox();
				this.label17 = new System.Windows.Forms.Label();
				this.label6 = new System.Windows.Forms.Label();
				this.butFixDates = new System.Windows.Forms.Button();
				this.textDateOldFormats = new System.Windows.Forms.TextBox();
				this.label7 = new System.Windows.Forms.Label();
				this.label8 = new System.Windows.Forms.Label();
				this.comboColDateSource = new System.Windows.Forms.ComboBox();
				this.tabLoadData = new System.Windows.Forms.TabPage();
				this.groupBox16 = new System.Windows.Forms.GroupBox();
				this.radioGenerateColNames = new System.Windows.Forms.RadioButton();
				this.radioColNamRow = new System.Windows.Forms.RadioButton();
				this.label42 = new System.Windows.Forms.Label();
				this.comboTableName = new System.Windows.Forms.ComboBox();
				this.butDeleteTable = new System.Windows.Forms.Button();
				this.label22 = new System.Windows.Forms.Label();
				this.label21 = new System.Windows.Forms.Label();
				this.butLoad = new System.Windows.Forms.Button();
				this.butBrowse = new System.Windows.Forms.Button();
				this.label20 = new System.Windows.Forms.Label();
				this.textFileName = new System.Windows.Forms.TextBox();
				this.textNewTable = new System.Windows.Forms.TextBox();
				this.textBox1 = new System.Windows.Forms.TextBox();
				this.label19 = new System.Windows.Forms.Label();
				this.label18 = new System.Windows.Forms.Label();
				this.butRefresh = new System.Windows.Forms.Button();
				this.label2 = new System.Windows.Forms.Label();
				this.tabContr = new System.Windows.Forms.TabControl();
				this.tabTable = new System.Windows.Forms.TabPage();
				this.groupBox17 = new System.Windows.Forms.GroupBox();
				this.butCreateTemporaryTable = new System.Windows.Forms.Button();
				this.label49 = new System.Windows.Forms.Label();
				this.upDownNumTempCols = new System.Windows.Forms.NumericUpDown();
				this.textCreateTempName = new System.Windows.Forms.TextBox();
				this.textBox9 = new System.Windows.Forms.TextBox();
				this.label48 = new System.Windows.Forms.Label();
				((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
				this.tabFinalImport.SuspendLayout();
				this.tabScript.SuspendLayout();
				this.tabEdit.SuspendLayout();
				this.groupBox12.SuspendLayout();
				this.groupBox11.SuspendLayout();
				this.groupBox10.SuspendLayout();
				this.groupBox9.SuspendLayout();
				this.groupBox1.SuspendLayout();
				this.tabRows.SuspendLayout();
				this.groupBox22.SuspendLayout();
				this.groupBox21.SuspendLayout();
				this.groupBox18.SuspendLayout();
				this.groupBox19.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)(this.upDownCopyToInc)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.upDownCopyFromInc)).BeginInit();
				this.tabQuery.SuspendLayout();
				this.tabColumns.SuspendLayout();
				this.groupBox20.SuspendLayout();
				this.groupBox14.SuspendLayout();
				this.groupBox8.SuspendLayout();
				this.groupBox2.SuspendLayout();
				this.groupBox6.SuspendLayout();
				this.groupBox4.SuspendLayout();
				this.groupBox5.SuspendLayout();
				this.tabPK.SuspendLayout();
				this.groupBox7.SuspendLayout();
				this.tabSpecialtyF.SuspendLayout();
				this.groupBox15.SuspendLayout();
				this.groupBox13.SuspendLayout();
				this.groupBox3.SuspendLayout();
				this.tabLoadData.SuspendLayout();
				this.groupBox16.SuspendLayout();
				this.tabContr.SuspendLayout();
				this.tabTable.SuspendLayout();
				this.groupBox17.SuspendLayout();
				((System.ComponentModel.ISupportInitialize)(this.upDownNumTempCols)).BeginInit();
				this.SuspendLayout();
				// 
				// grid
				// 
				this.grid.DataMember = "";
				this.grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
				this.grid.Location = new System.Drawing.Point(0,244);
				this.grid.Name = "grid";
				this.grid.ReadOnly = true;
				this.grid.Size = new System.Drawing.Size(1191,602);
				this.grid.TabIndex = 0;
				// 
				// tabFinalImport
				// 
				this.tabFinalImport.Controls.Add(this.radioUpdate);
				this.tabFinalImport.Controls.Add(this.radioInsert);
				this.tabFinalImport.Controls.Add(this.butImport);
				this.tabFinalImport.Controls.Add(this.butValidate);
				this.tabFinalImport.Location = new System.Drawing.Point(4,22);
				this.tabFinalImport.Name = "tabFinalImport";
				this.tabFinalImport.Size = new System.Drawing.Size(1046,217);
				this.tabFinalImport.TabIndex = 4;
				this.tabFinalImport.Text = "Final Import";
				this.tabFinalImport.UseVisualStyleBackColor = true;
				// 
				// radioUpdate
				// 
				this.radioUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioUpdate.Location = new System.Drawing.Point(9,27);
				this.radioUpdate.Name = "radioUpdate";
				this.radioUpdate.Size = new System.Drawing.Size(290,18);
				this.radioUpdate.TabIndex = 12;
				this.radioUpdate.Text = "Update some columns for existing rows";
				// 
				// radioInsert
				// 
				this.radioInsert.Checked = true;
				this.radioInsert.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioInsert.Location = new System.Drawing.Point(9,7);
				this.radioInsert.Name = "radioInsert";
				this.radioInsert.Size = new System.Drawing.Size(266,18);
				this.radioInsert.TabIndex = 11;
				this.radioInsert.TabStop = true;
				this.radioInsert.Text = "Insert new rows";
				// 
				// butImport
				// 
				this.butImport.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butImport.Location = new System.Drawing.Point(10,99);
				this.butImport.Name = "butImport";
				this.butImport.Size = new System.Drawing.Size(75,23);
				this.butImport.TabIndex = 10;
				this.butImport.Text = "Import";
				this.butImport.Click += new System.EventHandler(this.butImport_Click);
				// 
				// butValidate
				// 
				this.butValidate.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butValidate.Location = new System.Drawing.Point(10,67);
				this.butValidate.Name = "butValidate";
				this.butValidate.Size = new System.Drawing.Size(75,23);
				this.butValidate.TabIndex = 9;
				this.butValidate.Text = "Validate";
				this.butValidate.Click += new System.EventHandler(this.butValidate_Click);
				// 
				// tabScript
				// 
				this.tabScript.Controls.Add(this.label47);
				this.tabScript.Controls.Add(this.butReloadScript);
				this.tabScript.Controls.Add(this.butSaveScript);
				this.tabScript.Controls.Add(this.butRunScript);
				this.tabScript.Controls.Add(this.textScript);
				this.tabScript.Location = new System.Drawing.Point(4,22);
				this.tabScript.Name = "tabScript";
				this.tabScript.Size = new System.Drawing.Size(1046,217);
				this.tabScript.TabIndex = 8;
				this.tabScript.Text = "Script";
				this.tabScript.UseVisualStyleBackColor = true;
				// 
				// label47
				// 
				this.label47.Location = new System.Drawing.Point(3,84);
				this.label47.Name = "label47";
				this.label47.Size = new System.Drawing.Size(72,46);
				this.label47.TabIndex = 13;
				this.label47.Text = "(not needed)";
				// 
				// butReloadScript
				// 
				this.butReloadScript.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butReloadScript.Location = new System.Drawing.Point(0,58);
				this.butReloadScript.Name = "butReloadScript";
				this.butReloadScript.Size = new System.Drawing.Size(75,23);
				this.butReloadScript.TabIndex = 12;
				this.butReloadScript.Text = "Reload";
				this.butReloadScript.Click += new System.EventHandler(this.butReloadScript_Click);
				// 
				// butSaveScript
				// 
				this.butSaveScript.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butSaveScript.Location = new System.Drawing.Point(0,29);
				this.butSaveScript.Name = "butSaveScript";
				this.butSaveScript.Size = new System.Drawing.Size(75,23);
				this.butSaveScript.TabIndex = 11;
				this.butSaveScript.Text = "Save";
				this.butSaveScript.Click += new System.EventHandler(this.butSaveScript_Click);
				// 
				// butRunScript
				// 
				this.butRunScript.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butRunScript.Location = new System.Drawing.Point(0,0);
				this.butRunScript.Name = "butRunScript";
				this.butRunScript.Size = new System.Drawing.Size(75,23);
				this.butRunScript.TabIndex = 10;
				this.butRunScript.Text = "Run";
				this.butRunScript.Click += new System.EventHandler(this.butRunScript_Click);
				// 
				// textScript
				// 
				this.textScript.AcceptsReturn = true;
				this.textScript.Location = new System.Drawing.Point(75,1);
				this.textScript.MaxLength = 327670000;
				this.textScript.Multiline = true;
				this.textScript.Name = "textScript";
				this.textScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
				this.textScript.Size = new System.Drawing.Size(971,215);
				this.textScript.TabIndex = 0;
				// 
				// tabEdit
				// 
				this.tabEdit.Controls.Add(this.groupBox12);
				this.tabEdit.Controls.Add(this.groupBox11);
				this.tabEdit.Controls.Add(this.groupBox10);
				this.tabEdit.Controls.Add(this.groupBox9);
				this.tabEdit.Controls.Add(this.groupBox1);
				this.tabEdit.Controls.Add(this.label3);
				this.tabEdit.Controls.Add(this.comboColEdit);
				this.tabEdit.Location = new System.Drawing.Point(4,22);
				this.tabEdit.Name = "tabEdit";
				this.tabEdit.Size = new System.Drawing.Size(1046,217);
				this.tabEdit.TabIndex = 2;
				this.tabEdit.Text = "Edit";
				this.tabEdit.UseVisualStyleBackColor = true;
				// 
				// groupBox12
				// 
				this.groupBox12.Controls.Add(this.comboColCopyFrom);
				this.groupBox12.Controls.Add(this.butColCopy);
				this.groupBox12.Controls.Add(this.label34);
				this.groupBox12.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox12.Location = new System.Drawing.Point(7,135);
				this.groupBox12.Name = "groupBox12";
				this.groupBox12.Size = new System.Drawing.Size(246,68);
				this.groupBox12.TabIndex = 19;
				this.groupBox12.TabStop = false;
				this.groupBox12.Text = "Copy Column";
				// 
				// comboColCopyFrom
				// 
				this.comboColCopyFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColCopyFrom.Location = new System.Drawing.Point(116,13);
				this.comboColCopyFrom.MaxDropDownItems = 100;
				this.comboColCopyFrom.Name = "comboColCopyFrom";
				this.comboColCopyFrom.Size = new System.Drawing.Size(123,21);
				this.comboColCopyFrom.TabIndex = 16;
				// 
				// butColCopy
				// 
				this.butColCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColCopy.Location = new System.Drawing.Point(163,38);
				this.butColCopy.Name = "butColCopy";
				this.butColCopy.Size = new System.Drawing.Size(75,23);
				this.butColCopy.TabIndex = 9;
				this.butColCopy.Text = "Copy";
				this.butColCopy.Click += new System.EventHandler(this.butColCopy_Click);
				// 
				// label34
				// 
				this.label34.Location = new System.Drawing.Point(10,16);
				this.label34.Name = "label34";
				this.label34.Size = new System.Drawing.Size(100,16);
				this.label34.TabIndex = 3;
				this.label34.Text = "From Column";
				this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox11
				// 
				this.groupBox11.Controls.Add(this.textPadLength);
				this.groupBox11.Controls.Add(this.label32);
				this.groupBox11.Controls.Add(this.textPadChar);
				this.groupBox11.Controls.Add(this.label31);
				this.groupBox11.Controls.Add(this.butColPad);
				this.groupBox11.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox11.Location = new System.Drawing.Point(298,6);
				this.groupBox11.Name = "groupBox11";
				this.groupBox11.Size = new System.Drawing.Size(188,90);
				this.groupBox11.TabIndex = 18;
				this.groupBox11.TabStop = false;
				this.groupBox11.Text = "Pad Left (Add characters on left)";
				// 
				// textPadLength
				// 
				this.textPadLength.Location = new System.Drawing.Point(148,37);
				this.textPadLength.Name = "textPadLength";
				this.textPadLength.Size = new System.Drawing.Size(31,20);
				this.textPadLength.TabIndex = 22;
				// 
				// label32
				// 
				this.label32.Location = new System.Drawing.Point(43,39);
				this.label32.Name = "label32";
				this.label32.Size = new System.Drawing.Size(100,16);
				this.label32.TabIndex = 21;
				this.label32.Text = "Final Length";
				this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textPadChar
				// 
				this.textPadChar.Location = new System.Drawing.Point(148,16);
				this.textPadChar.MaxLength = 1;
				this.textPadChar.Name = "textPadChar";
				this.textPadChar.Size = new System.Drawing.Size(31,20);
				this.textPadChar.TabIndex = 20;
				// 
				// label31
				// 
				this.label31.Location = new System.Drawing.Point(43,18);
				this.label31.Name = "label31";
				this.label31.Size = new System.Drawing.Size(100,16);
				this.label31.TabIndex = 19;
				this.label31.Text = "Pad Character";
				this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butColPad
				// 
				this.butColPad.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColPad.Location = new System.Drawing.Point(105,62);
				this.butColPad.Name = "butColPad";
				this.butColPad.Size = new System.Drawing.Size(75,23);
				this.butColPad.TabIndex = 9;
				this.butColPad.Text = "Pad";
				this.butColPad.Click += new System.EventHandler(this.butColPad_Click);
				// 
				// groupBox10
				// 
				this.groupBox10.Controls.Add(this.label36);
				this.groupBox10.Controls.Add(this.butColCapAll);
				this.groupBox10.Controls.Add(this.label26);
				this.groupBox10.Controls.Add(this.butColCap);
				this.groupBox10.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox10.Location = new System.Drawing.Point(498,10);
				this.groupBox10.Name = "groupBox10";
				this.groupBox10.Size = new System.Drawing.Size(244,95);
				this.groupBox10.TabIndex = 17;
				this.groupBox10.TabStop = false;
				this.groupBox10.Text = "Capitalize first letter of each word";
				// 
				// label36
				// 
				this.label36.Location = new System.Drawing.Point(5,47);
				this.label36.Name = "label36";
				this.label36.Size = new System.Drawing.Size(155,40);
				this.label36.TabIndex = 22;
				this.label36.Text = "Fixes LName, FName, Address, Address2, and City";
				this.label36.TextAlign = System.Drawing.ContentAlignment.TopRight;
				// 
				// butColCapAll
				// 
				this.butColCapAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColCapAll.Location = new System.Drawing.Point(163,49);
				this.butColCapAll.Name = "butColCapAll";
				this.butColCapAll.Size = new System.Drawing.Size(75,23);
				this.butColCapAll.TabIndex = 21;
				this.butColCapAll.Text = "Capitalize";
				this.butColCapAll.Click += new System.EventHandler(this.butColCapAll_Click);
				// 
				// label26
				// 
				this.label26.Location = new System.Drawing.Point(4,20);
				this.label26.Name = "label26";
				this.label26.Size = new System.Drawing.Size(155,16);
				this.label26.TabIndex = 20;
				this.label26.Text = "Selected Column Only";
				this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butColCap
				// 
				this.butColCap.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColCap.Location = new System.Drawing.Point(163,16);
				this.butColCap.Name = "butColCap";
				this.butColCap.Size = new System.Drawing.Size(75,23);
				this.butColCap.TabIndex = 9;
				this.butColCap.Text = "Capitalize";
				this.butColCap.Click += new System.EventHandler(this.butColCap_Click);
				// 
				// groupBox9
				// 
				this.groupBox9.Controls.Add(this.butColClear);
				this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox9.Location = new System.Drawing.Point(298,159);
				this.groupBox9.Name = "groupBox9";
				this.groupBox9.Size = new System.Drawing.Size(188,48);
				this.groupBox9.TabIndex = 16;
				this.groupBox9.TabStop = false;
				this.groupBox9.Text = "Clear all values";
				// 
				// butColClear
				// 
				this.butColClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColClear.Location = new System.Drawing.Point(105,17);
				this.butColClear.Name = "butColClear";
				this.butColClear.Size = new System.Drawing.Size(75,23);
				this.butColClear.TabIndex = 9;
				this.butColClear.Text = "Clear";
				this.butColClear.Click += new System.EventHandler(this.butColClear_Click);
				// 
				// groupBox1
				// 
				this.groupBox1.Controls.Add(this.butSubstitute);
				this.groupBox1.Controls.Add(this.textSubstNew);
				this.groupBox1.Controls.Add(this.label5);
				this.groupBox1.Controls.Add(this.textSubstOld);
				this.groupBox1.Controls.Add(this.label4);
				this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox1.Location = new System.Drawing.Point(8,39);
				this.groupBox1.Name = "groupBox1";
				this.groupBox1.Size = new System.Drawing.Size(245,91);
				this.groupBox1.TabIndex = 7;
				this.groupBox1.TabStop = false;
				this.groupBox1.Text = "Substitution";
				// 
				// butSubstitute
				// 
				this.butSubstitute.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butSubstitute.Location = new System.Drawing.Point(162,62);
				this.butSubstitute.Name = "butSubstitute";
				this.butSubstitute.Size = new System.Drawing.Size(75,23);
				this.butSubstitute.TabIndex = 9;
				this.butSubstitute.Text = "Substitute";
				this.butSubstitute.Click += new System.EventHandler(this.butSubstitute_Click);
				// 
				// textSubstNew
				// 
				this.textSubstNew.Location = new System.Drawing.Point(114,37);
				this.textSubstNew.Name = "textSubstNew";
				this.textSubstNew.Size = new System.Drawing.Size(123,20);
				this.textSubstNew.TabIndex = 8;
				// 
				// label5
				// 
				this.label5.Location = new System.Drawing.Point(8,38);
				this.label5.Name = "label5";
				this.label5.Size = new System.Drawing.Size(100,16);
				this.label5.TabIndex = 7;
				this.label5.Text = "New Value";
				this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textSubstOld
				// 
				this.textSubstOld.Location = new System.Drawing.Point(114,13);
				this.textSubstOld.Name = "textSubstOld";
				this.textSubstOld.Size = new System.Drawing.Size(123,20);
				this.textSubstOld.TabIndex = 6;
				// 
				// label4
				// 
				this.label4.Location = new System.Drawing.Point(9,15);
				this.label4.Name = "label4";
				this.label4.Size = new System.Drawing.Size(100,16);
				this.label4.TabIndex = 5;
				this.label4.Text = "Old Value";
				this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label3
				// 
				this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif",9F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
				this.label3.Location = new System.Drawing.Point(9,7);
				this.label3.Name = "label3";
				this.label3.Size = new System.Drawing.Size(92,16);
				this.label3.TabIndex = 3;
				this.label3.Text = "Column";
				this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// comboColEdit
				// 
				this.comboColEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColEdit.Location = new System.Drawing.Point(105,5);
				this.comboColEdit.MaxDropDownItems = 100;
				this.comboColEdit.Name = "comboColEdit";
				this.comboColEdit.Size = new System.Drawing.Size(142,21);
				this.comboColEdit.TabIndex = 8;
				// 
				// tabRows
				// 
				this.tabRows.Controls.Add(this.groupBox22);
				this.tabRows.Controls.Add(this.groupBox21);
				this.tabRows.Controls.Add(this.groupBox18);
				this.tabRows.Controls.Add(this.groupBox19);
				this.tabRows.Location = new System.Drawing.Point(4,22);
				this.tabRows.Name = "tabRows";
				this.tabRows.Size = new System.Drawing.Size(1046,217);
				this.tabRows.TabIndex = 5;
				this.tabRows.Text = "Rows";
				this.tabRows.UseVisualStyleBackColor = true;
				// 
				// groupBox22
				// 
				this.groupBox22.BackColor = System.Drawing.SystemColors.Control;
				this.groupBox22.Controls.Add(this.butDeleteExDups);
				this.groupBox22.Controls.Add(this.label60);
				this.groupBox22.Controls.Add(this.comboDeleteExDupsTable);
				this.groupBox22.Location = new System.Drawing.Point(3,146);
				this.groupBox22.Name = "groupBox22";
				this.groupBox22.Size = new System.Drawing.Size(288,68);
				this.groupBox22.TabIndex = 20;
				this.groupBox22.TabStop = false;
				this.groupBox22.Text = "Delete Exact Duplicates";
				// 
				// butDeleteExDups
				// 
				this.butDeleteExDups.Location = new System.Drawing.Point(166,40);
				this.butDeleteExDups.Name = "butDeleteExDups";
				this.butDeleteExDups.Size = new System.Drawing.Size(75,23);
				this.butDeleteExDups.TabIndex = 2;
				this.butDeleteExDups.Text = "Delete";
				this.butDeleteExDups.UseVisualStyleBackColor = true;
				this.butDeleteExDups.Click += new System.EventHandler(this.butDeleteExDups_Click);
				// 
				// label60
				// 
				this.label60.AutoSize = true;
				this.label60.Location = new System.Drawing.Point(9,23);
				this.label60.Name = "label60";
				this.label60.Size = new System.Drawing.Size(119,13);
				this.label60.TabIndex = 1;
				this.label60.Text = "Table To Remove From";
				// 
				// comboDeleteExDupsTable
				// 
				this.comboDeleteExDupsTable.FormattingEnabled = true;
				this.comboDeleteExDupsTable.Location = new System.Drawing.Point(9,40);
				this.comboDeleteExDupsTable.Name = "comboDeleteExDupsTable";
				this.comboDeleteExDupsTable.Size = new System.Drawing.Size(121,21);
				this.comboDeleteExDupsTable.TabIndex = 0;
				// 
				// groupBox21
				// 
				this.groupBox21.BackColor = System.Drawing.SystemColors.Control;
				this.groupBox21.Controls.Add(this.checkFindDupsInsensitive);
				this.groupBox21.Controls.Add(this.label59);
				this.groupBox21.Controls.Add(this.label58);
				this.groupBox21.Controls.Add(this.button1);
				this.groupBox21.Controls.Add(this.textDupRowTable);
				this.groupBox21.Controls.Add(this.textBox10);
				this.groupBox21.Controls.Add(this.label57);
				this.groupBox21.Controls.Add(this.listDupRowCols);
				this.groupBox21.Controls.Add(this.comboFindDups);
				this.groupBox21.Location = new System.Drawing.Point(573,3);
				this.groupBox21.Name = "groupBox21";
				this.groupBox21.Size = new System.Drawing.Size(267,211);
				this.groupBox21.TabIndex = 19;
				this.groupBox21.TabStop = false;
				this.groupBox21.Text = "Find Duplicate Table Rows";
				// 
				// checkFindDupsInsensitive
				// 
				this.checkFindDupsInsensitive.AutoSize = true;
				this.checkFindDupsInsensitive.Location = new System.Drawing.Point(81,158);
				this.checkFindDupsInsensitive.Name = "checkFindDupsInsensitive";
				this.checkFindDupsInsensitive.Size = new System.Drawing.Size(103,17);
				this.checkFindDupsInsensitive.TabIndex = 17;
				this.checkFindDupsInsensitive.Text = "Case Insensitive";
				this.checkFindDupsInsensitive.UseVisualStyleBackColor = true;
				// 
				// label59
				// 
				this.label59.AutoSize = true;
				this.label59.Location = new System.Drawing.Point(133,17);
				this.label59.Name = "label59";
				this.label59.Size = new System.Drawing.Size(97,13);
				this.label59.TabIndex = 16;
				this.label59.Text = "Columns To Check";
				// 
				// label58
				// 
				this.label58.AutoSize = true;
				this.label58.Location = new System.Drawing.Point(6,17);
				this.label58.Name = "label58";
				this.label58.Size = new System.Drawing.Size(71,13);
				this.label58.TabIndex = 15;
				this.label58.Text = "Source Table";
				// 
				// button1
				// 
				this.button1.Location = new System.Drawing.Point(70,178);
				this.button1.Name = "button1";
				this.button1.Size = new System.Drawing.Size(125,23);
				this.button1.TabIndex = 14;
				this.button1.Text = "Find Duplicates";
				this.button1.UseVisualStyleBackColor = true;
				this.button1.Click += new System.EventHandler(this.button1_Click);
				// 
				// textDupRowTable
				// 
				this.textDupRowTable.Location = new System.Drawing.Point(51,134);
				this.textDupRowTable.Name = "textDupRowTable";
				this.textDupRowTable.Size = new System.Drawing.Size(123,20);
				this.textDupRowTable.TabIndex = 13;
				// 
				// textBox10
				// 
				this.textBox10.Location = new System.Drawing.Point(7,134);
				this.textBox10.Name = "textBox10";
				this.textBox10.ReadOnly = true;
				this.textBox10.Size = new System.Drawing.Size(46,20);
				this.textBox10.TabIndex = 12;
				this.textBox10.Text = "temp";
				this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				// 
				// label57
				// 
				this.label57.Location = new System.Drawing.Point(6,111);
				this.label57.Name = "label57";
				this.label57.Size = new System.Drawing.Size(122,16);
				this.label57.TabIndex = 11;
				this.label57.Text = "Output Table Name";
				this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// listDupRowCols
				// 
				this.listDupRowCols.FormattingEnabled = true;
				this.listDupRowCols.Location = new System.Drawing.Point(134,34);
				this.listDupRowCols.Name = "listDupRowCols";
				this.listDupRowCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
				this.listDupRowCols.Size = new System.Drawing.Size(120,95);
				this.listDupRowCols.TabIndex = 1;
				// 
				// comboFindDups
				// 
				this.comboFindDups.FormattingEnabled = true;
				this.comboFindDups.Location = new System.Drawing.Point(7,34);
				this.comboFindDups.Name = "comboFindDups";
				this.comboFindDups.Size = new System.Drawing.Size(121,21);
				this.comboFindDups.TabIndex = 0;
				this.comboFindDups.SelectedIndexChanged += new System.EventHandler(this.comboFindDups_SelectedIndexChanged);
				// 
				// groupBox18
				// 
				this.groupBox18.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
				this.groupBox18.BackColor = System.Drawing.SystemColors.Control;
				this.groupBox18.Controls.Add(this.label12);
				this.groupBox18.Controls.Add(this.butDeleteRows);
				this.groupBox18.Controls.Add(this.label23);
				this.groupBox18.Controls.Add(this.textRows);
				this.groupBox18.Location = new System.Drawing.Point(3,3);
				this.groupBox18.Name = "groupBox18";
				this.groupBox18.Size = new System.Drawing.Size(288,137);
				this.groupBox18.TabIndex = 18;
				this.groupBox18.TabStop = false;
				this.groupBox18.Text = "Delete Rows";
				// 
				// label12
				// 
				this.label12.Location = new System.Drawing.Point(6,38);
				this.label12.Name = "label12";
				this.label12.Size = new System.Drawing.Size(100,19);
				this.label12.TabIndex = 12;
				this.label12.Text = "Number of Rows";
				this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butDeleteRows
				// 
				this.butDeleteRows.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butDeleteRows.Location = new System.Drawing.Point(115,110);
				this.butDeleteRows.Name = "butDeleteRows";
				this.butDeleteRows.Size = new System.Drawing.Size(93,21);
				this.butDeleteRows.TabIndex = 11;
				this.butDeleteRows.Text = "Delete Row(s)";
				this.butDeleteRows.Click += new System.EventHandler(this.butDeleteRows_Click);
				// 
				// label23
				// 
				this.label23.Location = new System.Drawing.Point(112,30);
				this.label23.Name = "label23";
				this.label23.Size = new System.Drawing.Size(170,74);
				this.label23.TabIndex = 14;
				this.label23.Text = "Warning!  There is a bug in the delete feature.  Do not delete a row if you have " +
    "reordered any column by clicking on a column header.";
				// 
				// textRows
				// 
				this.textRows.BackColor = System.Drawing.SystemColors.ControlLightLight;
				this.textRows.Location = new System.Drawing.Point(24,60);
				this.textRows.Name = "textRows";
				this.textRows.ReadOnly = true;
				this.textRows.Size = new System.Drawing.Size(70,20);
				this.textRows.TabIndex = 13;
				// 
				// groupBox19
				// 
				this.groupBox19.BackColor = System.Drawing.SystemColors.Control;
				this.groupBox19.Controls.Add(this.label55);
				this.groupBox19.Controls.Add(this.upDownCopyToInc);
				this.groupBox19.Controls.Add(this.label54);
				this.groupBox19.Controls.Add(this.upDownCopyFromInc);
				this.groupBox19.Controls.Add(this.label51);
				this.groupBox19.Controls.Add(this.label50);
				this.groupBox19.Controls.Add(this.butCopyAppendRows);
				this.groupBox19.Controls.Add(this.butToTableNoColumn);
				this.groupBox19.Controls.Add(this.butToTableDown);
				this.groupBox19.Controls.Add(this.butToTableUp);
				this.groupBox19.Controls.Add(this.butFromTableNoColumn);
				this.groupBox19.Controls.Add(this.butFromTableDown);
				this.groupBox19.Controls.Add(this.butFromTableUp);
				this.groupBox19.Controls.Add(this.listCopyToTable);
				this.groupBox19.Controls.Add(this.listCopyFromTable);
				this.groupBox19.Controls.Add(this.comboCopyToTable);
				this.groupBox19.Controls.Add(this.comboCopyFromTable);
				this.groupBox19.Controls.Add(this.label53);
				this.groupBox19.Controls.Add(this.label52);
				this.groupBox19.Location = new System.Drawing.Point(297,3);
				this.groupBox19.Name = "groupBox19";
				this.groupBox19.Size = new System.Drawing.Size(270,211);
				this.groupBox19.TabIndex = 17;
				this.groupBox19.TabStop = false;
				this.groupBox19.Text = "Append Rows By Column";
				// 
				// label55
				// 
				this.label55.AutoSize = true;
				this.label55.Location = new System.Drawing.Point(183,195);
				this.label55.Name = "label55";
				this.label55.Size = new System.Drawing.Size(22,13);
				this.label55.TabIndex = 19;
				this.label55.Text = "Inc";
				// 
				// upDownCopyToInc
				// 
				this.upDownCopyToInc.Location = new System.Drawing.Point(209,191);
				this.upDownCopyToInc.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
				this.upDownCopyToInc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
				this.upDownCopyToInc.Name = "upDownCopyToInc";
				this.upDownCopyToInc.Size = new System.Drawing.Size(46,20);
				this.upDownCopyToInc.TabIndex = 18;
				this.upDownCopyToInc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
				// 
				// label54
				// 
				this.label54.AutoSize = true;
				this.label54.Location = new System.Drawing.Point(15,193);
				this.label54.Name = "label54";
				this.label54.Size = new System.Drawing.Size(22,13);
				this.label54.TabIndex = 17;
				this.label54.Text = "Inc";
				// 
				// upDownCopyFromInc
				// 
				this.upDownCopyFromInc.Location = new System.Drawing.Point(41,189);
				this.upDownCopyFromInc.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
				this.upDownCopyFromInc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
				this.upDownCopyFromInc.Name = "upDownCopyFromInc";
				this.upDownCopyFromInc.Size = new System.Drawing.Size(46,20);
				this.upDownCopyFromInc.TabIndex = 16;
				this.upDownCopyFromInc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
				// 
				// label51
				// 
				this.label51.AutoSize = true;
				this.label51.Location = new System.Drawing.Point(132,61);
				this.label51.Name = "label51";
				this.label51.Size = new System.Drawing.Size(63,13);
				this.label51.TabIndex = 15;
				this.label51.Text = "To Columns";
				// 
				// label50
				// 
				this.label50.AutoSize = true;
				this.label50.Location = new System.Drawing.Point(6,61);
				this.label50.Name = "label50";
				this.label50.Size = new System.Drawing.Size(73,13);
				this.label50.TabIndex = 14;
				this.label50.Text = "From Columns";
				// 
				// butCopyAppendRows
				// 
				this.butCopyAppendRows.Location = new System.Drawing.Point(97,183);
				this.butCopyAppendRows.Name = "butCopyAppendRows";
				this.butCopyAppendRows.Size = new System.Drawing.Size(75,26);
				this.butCopyAppendRows.TabIndex = 13;
				this.butCopyAppendRows.Text = "Append";
				this.butCopyAppendRows.UseVisualStyleBackColor = true;
				this.butCopyAppendRows.Click += new System.EventHandler(this.butCopyAppendRows_Click);
				// 
				// butToTableNoColumn
				// 
				this.butToTableNoColumn.Location = new System.Drawing.Point(171,153);
				this.butToTableNoColumn.Name = "butToTableNoColumn";
				this.butToTableNoColumn.Size = new System.Drawing.Size(85,27);
				this.butToTableNoColumn.TabIndex = 12;
				this.butToTableNoColumn.Text = "No Column";
				this.butToTableNoColumn.UseVisualStyleBackColor = true;
				this.butToTableNoColumn.Click += new System.EventHandler(this.butToTableNoColumn_Click);
				// 
				// butToTableDown
				// 
				this.butToTableDown.Image = global::OpenDental.Properties.Resources.down;
				this.butToTableDown.Location = new System.Drawing.Point(152,153);
				this.butToTableDown.Name = "butToTableDown";
				this.butToTableDown.Size = new System.Drawing.Size(20,27);
				this.butToTableDown.TabIndex = 11;
				this.butToTableDown.UseVisualStyleBackColor = true;
				this.butToTableDown.Click += new System.EventHandler(this.butToTableDown_Click);
				// 
				// butToTableUp
				// 
				this.butToTableUp.Image = global::OpenDental.Properties.Resources.up;
				this.butToTableUp.Location = new System.Drawing.Point(134,153);
				this.butToTableUp.Name = "butToTableUp";
				this.butToTableUp.Size = new System.Drawing.Size(19,27);
				this.butToTableUp.TabIndex = 10;
				this.butToTableUp.UseVisualStyleBackColor = true;
				this.butToTableUp.Click += new System.EventHandler(this.butToTableUp_Click);
				// 
				// butFromTableNoColumn
				// 
				this.butFromTableNoColumn.Location = new System.Drawing.Point(43,153);
				this.butFromTableNoColumn.Name = "butFromTableNoColumn";
				this.butFromTableNoColumn.Size = new System.Drawing.Size(85,27);
				this.butFromTableNoColumn.TabIndex = 8;
				this.butFromTableNoColumn.Text = "No Column";
				this.butFromTableNoColumn.UseVisualStyleBackColor = true;
				this.butFromTableNoColumn.Click += new System.EventHandler(this.butFromTableNoColumn_Click);
				// 
				// butFromTableDown
				// 
				this.butFromTableDown.Image = global::OpenDental.Properties.Resources.down;
				this.butFromTableDown.Location = new System.Drawing.Point(24,153);
				this.butFromTableDown.Name = "butFromTableDown";
				this.butFromTableDown.Size = new System.Drawing.Size(20,27);
				this.butFromTableDown.TabIndex = 7;
				this.butFromTableDown.UseVisualStyleBackColor = true;
				this.butFromTableDown.Click += new System.EventHandler(this.butFromTableDown_Click);
				// 
				// butFromTableUp
				// 
				this.butFromTableUp.Image = global::OpenDental.Properties.Resources.up;
				this.butFromTableUp.Location = new System.Drawing.Point(6,153);
				this.butFromTableUp.Name = "butFromTableUp";
				this.butFromTableUp.Size = new System.Drawing.Size(19,27);
				this.butFromTableUp.TabIndex = 6;
				this.butFromTableUp.UseVisualStyleBackColor = true;
				this.butFromTableUp.Click += new System.EventHandler(this.butFromTableUp_Click);
				// 
				// listCopyToTable
				// 
				this.listCopyToTable.FormattingEnabled = true;
				this.listCopyToTable.Location = new System.Drawing.Point(135,80);
				this.listCopyToTable.Name = "listCopyToTable";
				this.listCopyToTable.Size = new System.Drawing.Size(120,69);
				this.listCopyToTable.TabIndex = 5;
				// 
				// listCopyFromTable
				// 
				this.listCopyFromTable.FormattingEnabled = true;
				this.listCopyFromTable.Location = new System.Drawing.Point(6,80);
				this.listCopyFromTable.Name = "listCopyFromTable";
				this.listCopyFromTable.Size = new System.Drawing.Size(120,69);
				this.listCopyFromTable.TabIndex = 4;
				// 
				// comboCopyToTable
				// 
				this.comboCopyToTable.FormattingEnabled = true;
				this.comboCopyToTable.Location = new System.Drawing.Point(135,35);
				this.comboCopyToTable.Name = "comboCopyToTable";
				this.comboCopyToTable.Size = new System.Drawing.Size(121,21);
				this.comboCopyToTable.TabIndex = 3;
				this.comboCopyToTable.SelectedIndexChanged += new System.EventHandler(this.comboCopyToTable_SelectedIndexChanged);
				// 
				// comboCopyFromTable
				// 
				this.comboCopyFromTable.FormattingEnabled = true;
				this.comboCopyFromTable.Location = new System.Drawing.Point(6,35);
				this.comboCopyFromTable.Name = "comboCopyFromTable";
				this.comboCopyFromTable.Size = new System.Drawing.Size(121,21);
				this.comboCopyFromTable.TabIndex = 2;
				this.comboCopyFromTable.SelectedIndexChanged += new System.EventHandler(this.comboCopyFromTable_SelectedIndexChanged);
				// 
				// label53
				// 
				this.label53.AutoSize = true;
				this.label53.Location = new System.Drawing.Point(131,17);
				this.label53.Name = "label53";
				this.label53.Size = new System.Drawing.Size(90,13);
				this.label53.TabIndex = 1;
				this.label53.Text = "Append To Table";
				// 
				// label52
				// 
				this.label52.AutoSize = true;
				this.label52.Location = new System.Drawing.Point(6,17);
				this.label52.Name = "label52";
				this.label52.Size = new System.Drawing.Size(87,13);
				this.label52.TabIndex = 0;
				this.label52.Text = "Copy From Table";
				// 
				// tabQuery
				// 
				this.tabQuery.Controls.Add(this.label28);
				this.tabQuery.Controls.Add(this.butQuerySubmit);
				this.tabQuery.Controls.Add(this.textQuery);
				this.tabQuery.Location = new System.Drawing.Point(4,22);
				this.tabQuery.Name = "tabQuery";
				this.tabQuery.Size = new System.Drawing.Size(1046,217);
				this.tabQuery.TabIndex = 7;
				this.tabQuery.Text = "Query";
				this.tabQuery.UseVisualStyleBackColor = true;
				// 
				// label28
				// 
				this.label28.Location = new System.Drawing.Point(545,8);
				this.label28.Name = "label28";
				this.label28.Size = new System.Drawing.Size(182,30);
				this.label28.TabIndex = 11;
				this.label28.Text = "Should be a command, not a query to retrieve a table";
				// 
				// butQuerySubmit
				// 
				this.butQuerySubmit.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butQuerySubmit.Location = new System.Drawing.Point(545,169);
				this.butQuerySubmit.Name = "butQuerySubmit";
				this.butQuerySubmit.Size = new System.Drawing.Size(75,23);
				this.butQuerySubmit.TabIndex = 10;
				this.butQuerySubmit.Text = "Submit";
				this.butQuerySubmit.Click += new System.EventHandler(this.butQuerySubmit_Click);
				// 
				// textQuery
				// 
				this.textQuery.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
				this.textQuery.Location = new System.Drawing.Point(1,2);
				this.textQuery.Multiline = true;
				this.textQuery.Name = "textQuery";
				this.textQuery.Size = new System.Drawing.Size(537,190);
				this.textQuery.TabIndex = 0;
				// 
				// tabColumns
				// 
				this.tabColumns.Controls.Add(this.groupBox20);
				this.tabColumns.Controls.Add(this.groupBox14);
				this.tabColumns.Controls.Add(this.groupBox8);
				this.tabColumns.Controls.Add(this.groupBox2);
				this.tabColumns.Controls.Add(this.groupBox6);
				this.tabColumns.Controls.Add(this.groupBox4);
				this.tabColumns.Controls.Add(this.groupBox5);
				this.tabColumns.Location = new System.Drawing.Point(4,22);
				this.tabColumns.Name = "tabColumns";
				this.tabColumns.Size = new System.Drawing.Size(1046,217);
				this.tabColumns.TabIndex = 1;
				this.tabColumns.Text = "Columns";
				this.tabColumns.UseVisualStyleBackColor = true;
				// 
				// groupBox20
				// 
				this.groupBox20.Controls.Add(this.comboColUnused);
				this.groupBox20.Controls.Add(this.butColMarkUnused);
				this.groupBox20.Controls.Add(this.label56);
				this.groupBox20.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox20.Location = new System.Drawing.Point(773,136);
				this.groupBox20.Name = "groupBox20";
				this.groupBox20.Size = new System.Drawing.Size(250,77);
				this.groupBox20.TabIndex = 16;
				this.groupBox20.TabStop = false;
				this.groupBox20.Text = "Mark Unused";
				// 
				// comboColUnused
				// 
				this.comboColUnused.Location = new System.Drawing.Point(119,18);
				this.comboColUnused.MaxDropDownItems = 100;
				this.comboColUnused.Name = "comboColUnused";
				this.comboColUnused.Size = new System.Drawing.Size(123,21);
				this.comboColUnused.TabIndex = 18;
				// 
				// butColMarkUnused
				// 
				this.butColMarkUnused.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColMarkUnused.Location = new System.Drawing.Point(166,47);
				this.butColMarkUnused.Name = "butColMarkUnused";
				this.butColMarkUnused.Size = new System.Drawing.Size(75,23);
				this.butColMarkUnused.TabIndex = 9;
				this.butColMarkUnused.Text = "Mark";
				this.butColMarkUnused.Click += new System.EventHandler(this.butColMarkUnused_Click);
				// 
				// label56
				// 
				this.label56.Location = new System.Drawing.Point(10,21);
				this.label56.Name = "label56";
				this.label56.Size = new System.Drawing.Size(100,16);
				this.label56.TabIndex = 3;
				this.label56.Text = "Name";
				this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox14
				// 
				this.groupBox14.Controls.Add(this.textColsDelete);
				this.groupBox14.Controls.Add(this.butColsDelete);
				this.groupBox14.Controls.Add(this.label37);
				this.groupBox14.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox14.Location = new System.Drawing.Point(262,136);
				this.groupBox14.Name = "groupBox14";
				this.groupBox14.Size = new System.Drawing.Size(250,77);
				this.groupBox14.TabIndex = 15;
				this.groupBox14.TabStop = false;
				this.groupBox14.Text = "Delete Multiple Columns From the End";
				// 
				// textColsDelete
				// 
				this.textColsDelete.Location = new System.Drawing.Point(198,18);
				this.textColsDelete.Name = "textColsDelete";
				this.textColsDelete.Size = new System.Drawing.Size(42,20);
				this.textColsDelete.TabIndex = 10;
				// 
				// butColsDelete
				// 
				this.butColsDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butColsDelete.Location = new System.Drawing.Point(166,47);
				this.butColsDelete.Name = "butColsDelete";
				this.butColsDelete.Size = new System.Drawing.Size(75,23);
				this.butColsDelete.TabIndex = 9;
				this.butColsDelete.Text = "Delete";
				this.butColsDelete.Click += new System.EventHandler(this.butColsDelete_Click);
				// 
				// label37
				// 
				this.label37.Location = new System.Drawing.Point(15,21);
				this.label37.Name = "label37";
				this.label37.Size = new System.Drawing.Size(176,16);
				this.label37.TabIndex = 3;
				this.label37.Text = "Number of Columns";
				this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox8
				// 
				this.groupBox8.Controls.Add(this.comboColAdd);
				this.groupBox8.Controls.Add(this.butAddCol);
				this.groupBox8.Controls.Add(this.label24);
				this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox8.Location = new System.Drawing.Point(517,136);
				this.groupBox8.Name = "groupBox8";
				this.groupBox8.Size = new System.Drawing.Size(250,77);
				this.groupBox8.TabIndex = 14;
				this.groupBox8.TabStop = false;
				this.groupBox8.Text = "Add Column";
				// 
				// comboColAdd
				// 
				this.comboColAdd.Location = new System.Drawing.Point(119,18);
				this.comboColAdd.MaxDropDownItems = 100;
				this.comboColAdd.Name = "comboColAdd";
				this.comboColAdd.Size = new System.Drawing.Size(123,21);
				this.comboColAdd.TabIndex = 18;
				// 
				// butAddCol
				// 
				this.butAddCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butAddCol.Location = new System.Drawing.Point(166,47);
				this.butAddCol.Name = "butAddCol";
				this.butAddCol.Size = new System.Drawing.Size(75,23);
				this.butAddCol.TabIndex = 9;
				this.butAddCol.Text = "Add";
				this.butAddCol.Click += new System.EventHandler(this.butAddCol_Click);
				// 
				// label24
				// 
				this.label24.Location = new System.Drawing.Point(10,21);
				this.label24.Name = "label24";
				this.label24.Size = new System.Drawing.Size(100,16);
				this.label24.TabIndex = 3;
				this.label24.Text = "Name";
				this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox2
				// 
				this.groupBox2.Controls.Add(this.comboColDelete);
				this.groupBox2.Controls.Add(this.butDeleteCol);
				this.groupBox2.Controls.Add(this.label16);
				this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox2.Location = new System.Drawing.Point(7,136);
				this.groupBox2.Name = "groupBox2";
				this.groupBox2.Size = new System.Drawing.Size(250,77);
				this.groupBox2.TabIndex = 13;
				this.groupBox2.TabStop = false;
				this.groupBox2.Text = "Delete Column";
				// 
				// comboColDelete
				// 
				this.comboColDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColDelete.Location = new System.Drawing.Point(118,18);
				this.comboColDelete.MaxDropDownItems = 100;
				this.comboColDelete.Name = "comboColDelete";
				this.comboColDelete.Size = new System.Drawing.Size(123,21);
				this.comboColDelete.TabIndex = 16;
				// 
				// butDeleteCol
				// 
				this.butDeleteCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butDeleteCol.Location = new System.Drawing.Point(166,47);
				this.butDeleteCol.Name = "butDeleteCol";
				this.butDeleteCol.Size = new System.Drawing.Size(75,23);
				this.butDeleteCol.TabIndex = 9;
				this.butDeleteCol.Text = "Delete";
				this.butDeleteCol.Click += new System.EventHandler(this.butDeleteCol_Click);
				// 
				// label16
				// 
				this.label16.Location = new System.Drawing.Point(10,21);
				this.label16.Name = "label16";
				this.label16.Size = new System.Drawing.Size(100,16);
				this.label16.TabIndex = 3;
				this.label16.Text = "Column";
				this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox6
				// 
				this.groupBox6.Controls.Add(this.radioAfter);
				this.groupBox6.Controls.Add(this.radioFirst);
				this.groupBox6.Controls.Add(this.butMoveCol);
				this.groupBox6.Controls.Add(this.label15);
				this.groupBox6.Controls.Add(this.comboColMove);
				this.groupBox6.Controls.Add(this.comboColMoveAfter);
				this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox6.Location = new System.Drawing.Point(7,6);
				this.groupBox6.Name = "groupBox6";
				this.groupBox6.Size = new System.Drawing.Size(219,128);
				this.groupBox6.TabIndex = 12;
				this.groupBox6.TabStop = false;
				this.groupBox6.Text = "Move Column";
				// 
				// radioAfter
				// 
				this.radioAfter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
				this.radioAfter.Checked = true;
				this.radioAfter.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioAfter.Location = new System.Drawing.Point(8,61);
				this.radioAfter.Name = "radioAfter";
				this.radioAfter.Size = new System.Drawing.Size(72,19);
				this.radioAfter.TabIndex = 11;
				this.radioAfter.TabStop = true;
				this.radioAfter.Text = "After";
				this.radioAfter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				this.radioAfter.Click += new System.EventHandler(this.radioAfter_Click);
				// 
				// radioFirst
				// 
				this.radioFirst.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
				this.radioFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioFirst.Location = new System.Drawing.Point(7,42);
				this.radioFirst.Name = "radioFirst";
				this.radioFirst.Size = new System.Drawing.Size(73,19);
				this.radioFirst.TabIndex = 10;
				this.radioFirst.Text = "First";
				this.radioFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				this.radioFirst.Click += new System.EventHandler(this.radioFirst_Click);
				// 
				// butMoveCol
				// 
				this.butMoveCol.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butMoveCol.Location = new System.Drawing.Point(132,95);
				this.butMoveCol.Name = "butMoveCol";
				this.butMoveCol.Size = new System.Drawing.Size(75,23);
				this.butMoveCol.TabIndex = 9;
				this.butMoveCol.Text = "Move";
				this.butMoveCol.Click += new System.EventHandler(this.butMoveCol_Click);
				// 
				// label15
				// 
				this.label15.Location = new System.Drawing.Point(11,21);
				this.label15.Name = "label15";
				this.label15.Size = new System.Drawing.Size(68,16);
				this.label15.TabIndex = 3;
				this.label15.Text = "Column";
				this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// comboColMove
				// 
				this.comboColMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColMove.Location = new System.Drawing.Point(85,18);
				this.comboColMove.MaxDropDownItems = 100;
				this.comboColMove.Name = "comboColMove";
				this.comboColMove.Size = new System.Drawing.Size(123,21);
				this.comboColMove.TabIndex = 14;
				// 
				// comboColMoveAfter
				// 
				this.comboColMoveAfter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColMoveAfter.Location = new System.Drawing.Point(85,59);
				this.comboColMoveAfter.MaxDropDownItems = 100;
				this.comboColMoveAfter.Name = "comboColMoveAfter";
				this.comboColMoveAfter.Size = new System.Drawing.Size(123,21);
				this.comboColMoveAfter.TabIndex = 15;
				// 
				// groupBox4
				// 
				this.groupBox4.Controls.Add(this.comboColCombine2);
				this.groupBox4.Controls.Add(this.comboColCombine1);
				this.groupBox4.Controls.Add(this.butCombine);
				this.groupBox4.Controls.Add(this.textSepChar);
				this.groupBox4.Controls.Add(this.label9);
				this.groupBox4.Controls.Add(this.label10);
				this.groupBox4.Controls.Add(this.label11);
				this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox4.Location = new System.Drawing.Point(230,6);
				this.groupBox4.Name = "groupBox4";
				this.groupBox4.Size = new System.Drawing.Size(235,128);
				this.groupBox4.TabIndex = 10;
				this.groupBox4.TabStop = false;
				this.groupBox4.Text = "Combine Columns";
				// 
				// comboColCombine2
				// 
				this.comboColCombine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColCombine2.Location = new System.Drawing.Point(101,42);
				this.comboColCombine2.MaxDropDownItems = 100;
				this.comboColCombine2.Name = "comboColCombine2";
				this.comboColCombine2.Size = new System.Drawing.Size(123,21);
				this.comboColCombine2.TabIndex = 16;
				// 
				// comboColCombine1
				// 
				this.comboColCombine1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColCombine1.Location = new System.Drawing.Point(101,16);
				this.comboColCombine1.MaxDropDownItems = 100;
				this.comboColCombine1.Name = "comboColCombine1";
				this.comboColCombine1.Size = new System.Drawing.Size(123,21);
				this.comboColCombine1.TabIndex = 15;
				// 
				// butCombine
				// 
				this.butCombine.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butCombine.Location = new System.Drawing.Point(150,95);
				this.butCombine.Name = "butCombine";
				this.butCombine.Size = new System.Drawing.Size(75,23);
				this.butCombine.TabIndex = 9;
				this.butCombine.Text = "Combine";
				this.butCombine.Click += new System.EventHandler(this.butCombine_Click);
				// 
				// textSepChar
				// 
				this.textSepChar.Location = new System.Drawing.Point(101,68);
				this.textSepChar.Name = "textSepChar";
				this.textSepChar.Size = new System.Drawing.Size(42,20);
				this.textSepChar.TabIndex = 8;
				// 
				// label9
				// 
				this.label9.Location = new System.Drawing.Point(12,69);
				this.label9.Name = "label9";
				this.label9.Size = new System.Drawing.Size(83,16);
				this.label9.TabIndex = 7;
				this.label9.Text = "Sep Char";
				this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label10
				// 
				this.label10.Location = new System.Drawing.Point(12,45);
				this.label10.Name = "label10";
				this.label10.Size = new System.Drawing.Size(83,16);
				this.label10.TabIndex = 5;
				this.label10.Text = "Column 2";
				this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label11
				// 
				this.label11.Location = new System.Drawing.Point(12,21);
				this.label11.Name = "label11";
				this.label11.Size = new System.Drawing.Size(83,16);
				this.label11.TabIndex = 3;
				this.label11.Text = "Column 1";
				this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// groupBox5
				// 
				this.groupBox5.Controls.Add(this.comboColNewName);
				this.groupBox5.Controls.Add(this.comboColRename);
				this.groupBox5.Controls.Add(this.butRename);
				this.groupBox5.Controls.Add(this.label13);
				this.groupBox5.Controls.Add(this.label14);
				this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox5.Location = new System.Drawing.Point(468,6);
				this.groupBox5.Name = "groupBox5";
				this.groupBox5.Size = new System.Drawing.Size(250,128);
				this.groupBox5.TabIndex = 11;
				this.groupBox5.TabStop = false;
				this.groupBox5.Text = "Rename Column";
				// 
				// comboColNewName
				// 
				this.comboColNewName.Location = new System.Drawing.Point(116,43);
				this.comboColNewName.MaxDropDownItems = 100;
				this.comboColNewName.Name = "comboColNewName";
				this.comboColNewName.Size = new System.Drawing.Size(123,21);
				this.comboColNewName.TabIndex = 17;
				// 
				// comboColRename
				// 
				this.comboColRename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColRename.Location = new System.Drawing.Point(116,18);
				this.comboColRename.MaxDropDownItems = 100;
				this.comboColRename.Name = "comboColRename";
				this.comboColRename.Size = new System.Drawing.Size(123,21);
				this.comboColRename.TabIndex = 16;
				// 
				// butRename
				// 
				this.butRename.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butRename.Location = new System.Drawing.Point(166,95);
				this.butRename.Name = "butRename";
				this.butRename.Size = new System.Drawing.Size(75,23);
				this.butRename.TabIndex = 9;
				this.butRename.Text = "Rename";
				this.butRename.Click += new System.EventHandler(this.butRename_Click);
				// 
				// label13
				// 
				this.label13.Location = new System.Drawing.Point(10,45);
				this.label13.Name = "label13";
				this.label13.Size = new System.Drawing.Size(100,16);
				this.label13.TabIndex = 5;
				this.label13.Text = "New Name";
				this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label14
				// 
				this.label14.Location = new System.Drawing.Point(10,21);
				this.label14.Name = "label14";
				this.label14.Size = new System.Drawing.Size(100,16);
				this.label14.TabIndex = 3;
				this.label14.Text = "Column";
				this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// tabPK
				// 
				this.tabPK.Controls.Add(this.checkDontUsePK);
				this.tabPK.Controls.Add(this.label25);
				this.tabPK.Controls.Add(this.groupBox7);
				this.tabPK.Controls.Add(this.butClearPK);
				this.tabPK.Controls.Add(this.comboColPK);
				this.tabPK.Controls.Add(this.label1);
				this.tabPK.Controls.Add(this.butSetPK);
				this.tabPK.Location = new System.Drawing.Point(4,22);
				this.tabPK.Name = "tabPK";
				this.tabPK.Size = new System.Drawing.Size(1046,217);
				this.tabPK.TabIndex = 6;
				this.tabPK.Text = "Primary Key";
				this.tabPK.UseVisualStyleBackColor = true;
				// 
				// checkDontUsePK
				// 
				this.checkDontUsePK.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.checkDontUsePK.Location = new System.Drawing.Point(234,14);
				this.checkDontUsePK.Name = "checkDontUsePK";
				this.checkDontUsePK.Size = new System.Drawing.Size(360,19);
				this.checkDontUsePK.TabIndex = 31;
				this.checkDontUsePK.Text = "Do not use the primary key values in the final import";
				// 
				// label25
				// 
				this.label25.Location = new System.Drawing.Point(294,78);
				this.label25.Name = "label25";
				this.label25.Size = new System.Drawing.Size(511,19);
				this.label25.TabIndex = 30;
				this.label25.Text = "If you set an empty column as the primary key, it will automatically be filled wi" +
    "th numbers";
				// 
				// groupBox7
				// 
				this.groupBox7.Controls.Add(this.radioPatients);
				this.groupBox7.Controls.Add(this.radioCarriers);
				this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox7.Location = new System.Drawing.Point(15,11);
				this.groupBox7.Name = "groupBox7";
				this.groupBox7.Size = new System.Drawing.Size(200,203);
				this.groupBox7.TabIndex = 29;
				this.groupBox7.TabStop = false;
				this.groupBox7.Text = "Table Type";
				// 
				// radioPatients
				// 
				this.radioPatients.Checked = true;
				this.radioPatients.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioPatients.Location = new System.Drawing.Point(16,20);
				this.radioPatients.Name = "radioPatients";
				this.radioPatients.Size = new System.Drawing.Size(154,18);
				this.radioPatients.TabIndex = 19;
				this.radioPatients.TabStop = true;
				this.radioPatients.Text = "Patients";
				this.radioPatients.Click += new System.EventHandler(this.radioPatients_Click);
				// 
				// radioCarriers
				// 
				this.radioCarriers.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.radioCarriers.Location = new System.Drawing.Point(16,37);
				this.radioCarriers.Name = "radioCarriers";
				this.radioCarriers.Size = new System.Drawing.Size(154,18);
				this.radioCarriers.TabIndex = 20;
				this.radioCarriers.Text = "Carriers";
				this.radioCarriers.Click += new System.EventHandler(this.radioCarriers_Click);
				// 
				// butClearPK
				// 
				this.butClearPK.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butClearPK.Location = new System.Drawing.Point(499,47);
				this.butClearPK.Name = "butClearPK";
				this.butClearPK.Size = new System.Drawing.Size(69,23);
				this.butClearPK.TabIndex = 28;
				this.butClearPK.Text = "Clear";
				this.butClearPK.Click += new System.EventHandler(this.butClearPK_Click);
				// 
				// comboColPK
				// 
				this.comboColPK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColPK.Location = new System.Drawing.Point(296,48);
				this.comboColPK.MaxDropDownItems = 100;
				this.comboColPK.Name = "comboColPK";
				this.comboColPK.Size = new System.Drawing.Size(123,21);
				this.comboColPK.TabIndex = 27;
				// 
				// label1
				// 
				this.label1.Location = new System.Drawing.Point(221,51);
				this.label1.Name = "label1";
				this.label1.Size = new System.Drawing.Size(71,18);
				this.label1.TabIndex = 25;
				this.label1.Text = "Primary Key";
				this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butSetPK
				// 
				this.butSetPK.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butSetPK.Location = new System.Drawing.Point(422,47);
				this.butSetPK.Name = "butSetPK";
				this.butSetPK.Size = new System.Drawing.Size(70,23);
				this.butSetPK.TabIndex = 26;
				this.butSetPK.Text = "Set";
				this.butSetPK.Click += new System.EventHandler(this.butSetPK_Click);
				// 
				// tabSpecialtyF
				// 
				this.tabSpecialtyF.Controls.Add(this.groupBox15);
				this.tabSpecialtyF.Controls.Add(this.groupBox13);
				this.tabSpecialtyF.Controls.Add(this.groupBox3);
				this.tabSpecialtyF.Location = new System.Drawing.Point(4,22);
				this.tabSpecialtyF.Name = "tabSpecialtyF";
				this.tabSpecialtyF.Size = new System.Drawing.Size(1046,217);
				this.tabSpecialtyF.TabIndex = 3;
				this.tabSpecialtyF.Text = "Specialty Functions";
				this.tabSpecialtyF.UseVisualStyleBackColor = true;
				// 
				// groupBox15
				// 
				this.groupBox15.Controls.Add(this.textBox7);
				this.groupBox15.Controls.Add(this.label46);
				this.groupBox15.Controls.Add(this.label45);
				this.groupBox15.Controls.Add(this.textBox5);
				this.groupBox15.Controls.Add(this.label43);
				this.groupBox15.Controls.Add(this.textBox6);
				this.groupBox15.Controls.Add(this.label44);
				this.groupBox15.Controls.Add(this.butGuarAccount);
				this.groupBox15.Controls.Add(this.textBox4);
				this.groupBox15.Controls.Add(this.label41);
				this.groupBox15.Controls.Add(this.textBox3);
				this.groupBox15.Controls.Add(this.label40);
				this.groupBox15.Controls.Add(this.textBox2);
				this.groupBox15.Controls.Add(this.label38);
				this.groupBox15.Controls.Add(this.label39);
				this.groupBox15.Controls.Add(this.butGuarantor);
				this.groupBox15.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox15.Location = new System.Drawing.Point(634,4);
				this.groupBox15.Name = "groupBox15";
				this.groupBox15.Size = new System.Drawing.Size(302,200);
				this.groupBox15.TabIndex = 11;
				this.groupBox15.TabStop = false;
				this.groupBox15.Text = "Fill Guarantor";
				// 
				// textBox7
				// 
				this.textBox7.Location = new System.Drawing.Point(122,133);
				this.textBox7.Name = "textBox7";
				this.textBox7.ReadOnly = true;
				this.textBox7.Size = new System.Drawing.Size(167,20);
				this.textBox7.TabIndex = 29;
				this.textBox7.Text = "tempHoH";
				// 
				// label46
				// 
				this.label46.Location = new System.Drawing.Point(5,135);
				this.label46.Name = "label46";
				this.label46.Size = new System.Drawing.Size(115,16);
				this.label46.TabIndex = 28;
				this.label46.Text = "HoH Indic Column";
				this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label45
				// 
				this.label45.Location = new System.Drawing.Point(9,98);
				this.label45.Name = "label45";
				this.label45.Size = new System.Drawing.Size(100,17);
				this.label45.TabIndex = 27;
				this.label45.Text = "OR:";
				// 
				// textBox5
				// 
				this.textBox5.Location = new System.Drawing.Point(122,153);
				this.textBox5.Name = "textBox5";
				this.textBox5.ReadOnly = true;
				this.textBox5.Size = new System.Drawing.Size(167,20);
				this.textBox5.TabIndex = 26;
				this.textBox5.Text = "Guarantor";
				// 
				// label43
				// 
				this.label43.Location = new System.Drawing.Point(5,155);
				this.label43.Name = "label43";
				this.label43.Size = new System.Drawing.Size(115,16);
				this.label43.TabIndex = 25;
				this.label43.Text = "Destination Column";
				this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textBox6
				// 
				this.textBox6.Location = new System.Drawing.Point(122,113);
				this.textBox6.Name = "textBox6";
				this.textBox6.ReadOnly = true;
				this.textBox6.Size = new System.Drawing.Size(167,20);
				this.textBox6.TabIndex = 24;
				this.textBox6.Text = "tempAccountNum";
				// 
				// label44
				// 
				this.label44.Location = new System.Drawing.Point(5,115);
				this.label44.Name = "label44";
				this.label44.Size = new System.Drawing.Size(115,16);
				this.label44.TabIndex = 23;
				this.label44.Text = "Reference Column";
				this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butGuarAccount
				// 
				this.butGuarAccount.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butGuarAccount.Location = new System.Drawing.Point(215,174);
				this.butGuarAccount.Name = "butGuarAccount";
				this.butGuarAccount.Size = new System.Drawing.Size(75,22);
				this.butGuarAccount.TabIndex = 22;
				this.butGuarAccount.Text = "Fill";
				this.butGuarAccount.Click += new System.EventHandler(this.butGuarAccount_Click);
				// 
				// textBox4
				// 
				this.textBox4.Location = new System.Drawing.Point(121,32);
				this.textBox4.Name = "textBox4";
				this.textBox4.ReadOnly = true;
				this.textBox4.Size = new System.Drawing.Size(167,20);
				this.textBox4.TabIndex = 21;
				this.textBox4.Text = "ChartNumber";
				// 
				// label41
				// 
				this.label41.Location = new System.Drawing.Point(4,34);
				this.label41.Name = "label41";
				this.label41.Size = new System.Drawing.Size(115,16);
				this.label41.TabIndex = 20;
				this.label41.Text = "Key Column";
				this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textBox3
				// 
				this.textBox3.Location = new System.Drawing.Point(121,72);
				this.textBox3.Name = "textBox3";
				this.textBox3.ReadOnly = true;
				this.textBox3.Size = new System.Drawing.Size(167,20);
				this.textBox3.TabIndex = 19;
				this.textBox3.Text = "Guarantor";
				// 
				// label40
				// 
				this.label40.Location = new System.Drawing.Point(4,75);
				this.label40.Name = "label40";
				this.label40.Size = new System.Drawing.Size(115,16);
				this.label40.TabIndex = 18;
				this.label40.Text = "Destination Column";
				this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textBox2
				// 
				this.textBox2.Location = new System.Drawing.Point(121,52);
				this.textBox2.Name = "textBox2";
				this.textBox2.ReadOnly = true;
				this.textBox2.Size = new System.Drawing.Size(167,20);
				this.textBox2.TabIndex = 17;
				this.textBox2.Text = "tempGuarantor";
				// 
				// label38
				// 
				this.label38.Location = new System.Drawing.Point(4,54);
				this.label38.Name = "label38";
				this.label38.Size = new System.Drawing.Size(115,16);
				this.label38.TabIndex = 16;
				this.label38.Text = "Reference Column";
				this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label39
				// 
				this.label39.Location = new System.Drawing.Point(14,15);
				this.label39.Name = "label39";
				this.label39.Size = new System.Drawing.Size(275,20);
				this.label39.TabIndex = 12;
				this.label39.Text = "Used when the guarantor is not in PatNum format.";
				// 
				// butGuarantor
				// 
				this.butGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butGuarantor.Location = new System.Drawing.Point(214,92);
				this.butGuarantor.Name = "butGuarantor";
				this.butGuarantor.Size = new System.Drawing.Size(75,22);
				this.butGuarantor.TabIndex = 9;
				this.butGuarantor.Text = "Fill";
				this.butGuarantor.Click += new System.EventHandler(this.butGuarantor_Click);
				// 
				// groupBox13
				// 
				this.groupBox13.Controls.Add(this.comboColDateTo);
				this.groupBox13.Controls.Add(this.label29);
				this.groupBox13.Controls.Add(this.label30);
				this.groupBox13.Controls.Add(this.butConvertDate);
				this.groupBox13.Controls.Add(this.textDateBase);
				this.groupBox13.Controls.Add(this.label33);
				this.groupBox13.Controls.Add(this.label35);
				this.groupBox13.Controls.Add(this.comboColDateFrom);
				this.groupBox13.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox13.Location = new System.Drawing.Point(7,4);
				this.groupBox13.Name = "groupBox13";
				this.groupBox13.Size = new System.Drawing.Size(302,189);
				this.groupBox13.TabIndex = 10;
				this.groupBox13.TabStop = false;
				this.groupBox13.Text = "Convert numbers to dates";
				// 
				// comboColDateTo
				// 
				this.comboColDateTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColDateTo.Location = new System.Drawing.Point(121,76);
				this.comboColDateTo.MaxDropDownItems = 100;
				this.comboColDateTo.Name = "comboColDateTo";
				this.comboColDateTo.Size = new System.Drawing.Size(170,21);
				this.comboColDateTo.TabIndex = 16;
				// 
				// label29
				// 
				this.label29.Location = new System.Drawing.Point(14,20);
				this.label29.Name = "label29";
				this.label29.Size = new System.Drawing.Size(275,30);
				this.label29.TabIndex = 12;
				this.label29.Text = "The number of days since a given date";
				// 
				// label30
				// 
				this.label30.Location = new System.Drawing.Point(16,80);
				this.label30.Name = "label30";
				this.label30.Size = new System.Drawing.Size(100,16);
				this.label30.TabIndex = 10;
				this.label30.Text = "Destination Col";
				this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butConvertDate
				// 
				this.butConvertDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butConvertDate.Location = new System.Drawing.Point(218,150);
				this.butConvertDate.Name = "butConvertDate";
				this.butConvertDate.Size = new System.Drawing.Size(75,23);
				this.butConvertDate.TabIndex = 9;
				this.butConvertDate.Text = "Convert";
				this.butConvertDate.Click += new System.EventHandler(this.butConvertDate_Click);
				// 
				// textDateBase
				// 
				this.textDateBase.Location = new System.Drawing.Point(121,100);
				this.textDateBase.Name = "textDateBase";
				this.textDateBase.Size = new System.Drawing.Size(96,20);
				this.textDateBase.TabIndex = 6;
				// 
				// label33
				// 
				this.label33.Location = new System.Drawing.Point(4,102);
				this.label33.Name = "label33";
				this.label33.Size = new System.Drawing.Size(115,16);
				this.label33.TabIndex = 5;
				this.label33.Text = "Base date";
				this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label35
				// 
				this.label35.Location = new System.Drawing.Point(16,57);
				this.label35.Name = "label35";
				this.label35.Size = new System.Drawing.Size(100,16);
				this.label35.TabIndex = 3;
				this.label35.Text = "Source Column";
				this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// comboColDateFrom
				// 
				this.comboColDateFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColDateFrom.Location = new System.Drawing.Point(121,52);
				this.comboColDateFrom.MaxDropDownItems = 100;
				this.comboColDateFrom.Name = "comboColDateFrom";
				this.comboColDateFrom.Size = new System.Drawing.Size(170,21);
				this.comboColDateFrom.TabIndex = 15;
				// 
				// groupBox3
				// 
				this.groupBox3.Controls.Add(this.textDateNewFormat);
				this.groupBox3.Controls.Add(this.label27);
				this.groupBox3.Controls.Add(this.comboColDateDest);
				this.groupBox3.Controls.Add(this.label17);
				this.groupBox3.Controls.Add(this.label6);
				this.groupBox3.Controls.Add(this.butFixDates);
				this.groupBox3.Controls.Add(this.textDateOldFormats);
				this.groupBox3.Controls.Add(this.label7);
				this.groupBox3.Controls.Add(this.label8);
				this.groupBox3.Controls.Add(this.comboColDateSource);
				this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.groupBox3.Location = new System.Drawing.Point(322,4);
				this.groupBox3.Name = "groupBox3";
				this.groupBox3.Size = new System.Drawing.Size(302,189);
				this.groupBox3.TabIndex = 9;
				this.groupBox3.TabStop = false;
				this.groupBox3.Text = "Fix Dates";
				// 
				// textDateNewFormat
				// 
				this.textDateNewFormat.Location = new System.Drawing.Point(121,130);
				this.textDateNewFormat.Name = "textDateNewFormat";
				this.textDateNewFormat.Size = new System.Drawing.Size(170,20);
				this.textDateNewFormat.TabIndex = 18;
				this.textDateNewFormat.Text = "MM/dd/yyyy";
				// 
				// label27
				// 
				this.label27.Location = new System.Drawing.Point(4,132);
				this.label27.Name = "label27";
				this.label27.Size = new System.Drawing.Size(115,16);
				this.label27.TabIndex = 17;
				this.label27.Text = "New Format";
				this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// comboColDateDest
				// 
				this.comboColDateDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColDateDest.Location = new System.Drawing.Point(121,84);
				this.comboColDateDest.MaxDropDownItems = 100;
				this.comboColDateDest.Name = "comboColDateDest";
				this.comboColDateDest.Size = new System.Drawing.Size(170,21);
				this.comboColDateDest.TabIndex = 16;
				// 
				// label17
				// 
				this.label17.Location = new System.Drawing.Point(14,15);
				this.label17.Name = "label17";
				this.label17.Size = new System.Drawing.Size(275,41);
				this.label17.TabIndex = 12;
				this.label17.Text = "Rarely used.  Might be used if month and day are switched in source file or if da" +
    "te is in non-standard format, like yyyyMMdd";
				// 
				// label6
				// 
				this.label6.Location = new System.Drawing.Point(16,88);
				this.label6.Name = "label6";
				this.label6.Size = new System.Drawing.Size(100,16);
				this.label6.TabIndex = 10;
				this.label6.Text = "Destination Col";
				this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// butFixDates
				// 
				this.butFixDates.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butFixDates.Location = new System.Drawing.Point(218,158);
				this.butFixDates.Name = "butFixDates";
				this.butFixDates.Size = new System.Drawing.Size(75,23);
				this.butFixDates.TabIndex = 9;
				this.butFixDates.Text = "Fix";
				this.butFixDates.Click += new System.EventHandler(this.butFixDates_Click);
				// 
				// textDateOldFormats
				// 
				this.textDateOldFormats.Location = new System.Drawing.Point(121,108);
				this.textDateOldFormats.Name = "textDateOldFormats";
				this.textDateOldFormats.Size = new System.Drawing.Size(170,20);
				this.textDateOldFormats.TabIndex = 6;
				this.textDateOldFormats.Text = "dd/MM/yyyy,d/M/yyyy,dd/M/yyyy,d/MM/yyyy";
				// 
				// label7
				// 
				this.label7.Location = new System.Drawing.Point(4,110);
				this.label7.Name = "label7";
				this.label7.Size = new System.Drawing.Size(115,16);
				this.label7.TabIndex = 5;
				this.label7.Text = "Old Formats sep by ,";
				this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label8
				// 
				this.label8.Location = new System.Drawing.Point(16,65);
				this.label8.Name = "label8";
				this.label8.Size = new System.Drawing.Size(100,16);
				this.label8.TabIndex = 3;
				this.label8.Text = "Source Column";
				this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// comboColDateSource
				// 
				this.comboColDateSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboColDateSource.Location = new System.Drawing.Point(121,60);
				this.comboColDateSource.MaxDropDownItems = 100;
				this.comboColDateSource.Name = "comboColDateSource";
				this.comboColDateSource.Size = new System.Drawing.Size(170,21);
				this.comboColDateSource.TabIndex = 15;
				// 
				// tabLoadData
				// 
				this.tabLoadData.Controls.Add(this.groupBox16);
				this.tabLoadData.Controls.Add(this.label42);
				this.tabLoadData.Controls.Add(this.comboTableName);
				this.tabLoadData.Controls.Add(this.butDeleteTable);
				this.tabLoadData.Controls.Add(this.label22);
				this.tabLoadData.Controls.Add(this.label21);
				this.tabLoadData.Controls.Add(this.butLoad);
				this.tabLoadData.Controls.Add(this.butBrowse);
				this.tabLoadData.Controls.Add(this.label20);
				this.tabLoadData.Controls.Add(this.textFileName);
				this.tabLoadData.Controls.Add(this.textNewTable);
				this.tabLoadData.Controls.Add(this.textBox1);
				this.tabLoadData.Controls.Add(this.label19);
				this.tabLoadData.Controls.Add(this.label18);
				this.tabLoadData.Controls.Add(this.butRefresh);
				this.tabLoadData.Controls.Add(this.label2);
				this.tabLoadData.Location = new System.Drawing.Point(4,22);
				this.tabLoadData.Name = "tabLoadData";
				this.tabLoadData.Size = new System.Drawing.Size(1046,217);
				this.tabLoadData.TabIndex = 0;
				this.tabLoadData.Text = "Load Data";
				this.tabLoadData.UseVisualStyleBackColor = true;
				// 
				// groupBox16
				// 
				this.groupBox16.Controls.Add(this.radioGenerateColNames);
				this.groupBox16.Controls.Add(this.radioColNamRow);
				this.groupBox16.Location = new System.Drawing.Point(110,75);
				this.groupBox16.Name = "groupBox16";
				this.groupBox16.Size = new System.Drawing.Size(406,38);
				this.groupBox16.TabIndex = 20;
				this.groupBox16.TabStop = false;
				this.groupBox16.Text = "Get Table Column Names From:";
				// 
				// radioGenerateColNames
				// 
				this.radioGenerateColNames.AutoSize = true;
				this.radioGenerateColNames.Location = new System.Drawing.Point(173,19);
				this.radioGenerateColNames.Name = "radioGenerateColNames";
				this.radioGenerateColNames.Size = new System.Drawing.Size(185,17);
				this.radioGenerateColNames.TabIndex = 1;
				this.radioGenerateColNames.Text = "Generate numberically left to right.";
				this.radioGenerateColNames.UseVisualStyleBackColor = true;
				this.radioGenerateColNames.Click += new System.EventHandler(this.radioGenerateColNames_Click);
				// 
				// radioColNamRow
				// 
				this.radioColNamRow.AutoSize = true;
				this.radioColNamRow.Checked = true;
				this.radioColNamRow.Location = new System.Drawing.Point(6,19);
				this.radioColNamRow.Name = "radioColNamRow";
				this.radioColNamRow.Size = new System.Drawing.Size(129,17);
				this.radioColNamRow.TabIndex = 0;
				this.radioColNamRow.TabStop = true;
				this.radioColNamRow.Text = "First row of input data.";
				this.radioColNamRow.UseVisualStyleBackColor = true;
				this.radioColNamRow.Click += new System.EventHandler(this.radioColNamRow_Click);
				// 
				// label42
				// 
				this.label42.Location = new System.Drawing.Point(193,124);
				this.label42.Name = "label42";
				this.label42.Size = new System.Drawing.Size(836,18);
				this.label42.TabIndex = 19;
				this.label42.Text = "If you get an error loading,  consider the possibility that your A-Z folder is no" +
    "t accessible from both the server and the workstation";
				this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// comboTableName
				// 
				this.comboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
				this.comboTableName.Location = new System.Drawing.Point(110,171);
				this.comboTableName.MaxDropDownItems = 18;
				this.comboTableName.Name = "comboTableName";
				this.comboTableName.Size = new System.Drawing.Size(122,21);
				this.comboTableName.TabIndex = 18;
				this.comboTableName.SelectedIndexChanged += new System.EventHandler(this.comboTableName_SelectedIndexChanged);
				// 
				// butDeleteTable
				// 
				this.butDeleteTable.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butDeleteTable.Location = new System.Drawing.Point(321,169);
				this.butDeleteTable.Name = "butDeleteTable";
				this.butDeleteTable.Size = new System.Drawing.Size(75,23);
				this.butDeleteTable.TabIndex = 17;
				this.butDeleteTable.Text = "Delete";
				this.butDeleteTable.Click += new System.EventHandler(this.butDeleteTable_Click);
				// 
				// label22
				// 
				this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif",9.5F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
				this.label22.Location = new System.Drawing.Point(9,147);
				this.label22.Name = "label22";
				this.label22.Size = new System.Drawing.Size(631,19);
				this.label22.TabIndex = 16;
				this.label22.Text = "Or, if you already loaded a file earlier, you can select an existing temp table t" +
    "o work with";
				this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// label21
				// 
				this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif",9.5F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
				this.label21.Location = new System.Drawing.Point(9,195);
				this.label21.Name = "label21";
				this.label21.Size = new System.Drawing.Size(477,19);
				this.label21.TabIndex = 15;
				this.label21.Text = "Then, check settings in the Primary Key tab";
				this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// butLoad
				// 
				this.butLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butLoad.Location = new System.Drawing.Point(112,121);
				this.butLoad.Name = "butLoad";
				this.butLoad.Size = new System.Drawing.Size(75,23);
				this.butLoad.TabIndex = 14;
				this.butLoad.Text = "Load";
				this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
				// 
				// butBrowse
				// 
				this.butBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butBrowse.Location = new System.Drawing.Point(772,21);
				this.butBrowse.Name = "butBrowse";
				this.butBrowse.Size = new System.Drawing.Size(75,23);
				this.butBrowse.TabIndex = 13;
				this.butBrowse.Text = "Browse";
				this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
				// 
				// label20
				// 
				this.label20.Location = new System.Drawing.Point(8,51);
				this.label20.Name = "label20";
				this.label20.Size = new System.Drawing.Size(100,16);
				this.label20.TabIndex = 12;
				this.label20.Text = "File Name";
				this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// textFileName
				// 
				this.textFileName.Location = new System.Drawing.Point(112,49);
				this.textFileName.Name = "textFileName";
				this.textFileName.Size = new System.Drawing.Size(736,20);
				this.textFileName.TabIndex = 11;
				// 
				// textNewTable
				// 
				this.textNewTable.Location = new System.Drawing.Point(156,27);
				this.textNewTable.Name = "textNewTable";
				this.textNewTable.ReadOnly = true;
				this.textNewTable.Size = new System.Drawing.Size(123,20);
				this.textNewTable.TabIndex = 10;
				// 
				// textBox1
				// 
				this.textBox1.Location = new System.Drawing.Point(112,27);
				this.textBox1.Name = "textBox1";
				this.textBox1.ReadOnly = true;
				this.textBox1.Size = new System.Drawing.Size(46,20);
				this.textBox1.TabIndex = 9;
				this.textBox1.Text = "temp";
				this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				// 
				// label19
				// 
				this.label19.Location = new System.Drawing.Point(9,29);
				this.label19.Name = "label19";
				this.label19.Size = new System.Drawing.Size(100,16);
				this.label19.TabIndex = 8;
				this.label19.Text = "New Table Name";
				this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// label18
				// 
				this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif",9.5F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
				this.label18.Location = new System.Drawing.Point(9,5);
				this.label18.Name = "label18";
				this.label18.Size = new System.Drawing.Size(477,19);
				this.label18.TabIndex = 7;
				this.label18.Text = "Load data from a text file into a temp table";
				this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// butRefresh
				// 
				this.butRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.butRefresh.Location = new System.Drawing.Point(236,169);
				this.butRefresh.Name = "butRefresh";
				this.butRefresh.Size = new System.Drawing.Size(75,23);
				this.butRefresh.TabIndex = 6;
				this.butRefresh.Text = "Refresh";
				this.butRefresh.Click += new System.EventHandler(this.butRefresh_Click);
				// 
				// label2
				// 
				this.label2.Location = new System.Drawing.Point(7,174);
				this.label2.Name = "label2";
				this.label2.Size = new System.Drawing.Size(100,16);
				this.label2.TabIndex = 3;
				this.label2.Text = "Temp Table";
				this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// tabContr
				// 
				this.tabContr.Controls.Add(this.tabLoadData);
				this.tabContr.Controls.Add(this.tabSpecialtyF);
				this.tabContr.Controls.Add(this.tabPK);
				this.tabContr.Controls.Add(this.tabColumns);
				this.tabContr.Controls.Add(this.tabQuery);
				this.tabContr.Controls.Add(this.tabRows);
				this.tabContr.Controls.Add(this.tabEdit);
				this.tabContr.Controls.Add(this.tabScript);
				this.tabContr.Controls.Add(this.tabFinalImport);
				this.tabContr.Controls.Add(this.tabTable);
				this.tabContr.Location = new System.Drawing.Point(0,0);
				this.tabContr.Name = "tabContr";
				this.tabContr.SelectedIndex = 0;
				this.tabContr.Size = new System.Drawing.Size(1054,243);
				this.tabContr.TabIndex = 14;
				// 
				// tabTable
				// 
				this.tabTable.Controls.Add(this.groupBox17);
				this.tabTable.Location = new System.Drawing.Point(4,22);
				this.tabTable.Name = "tabTable";
				this.tabTable.Padding = new System.Windows.Forms.Padding(3);
				this.tabTable.Size = new System.Drawing.Size(1046,217);
				this.tabTable.TabIndex = 9;
				this.tabTable.Text = "Table";
				this.tabTable.UseVisualStyleBackColor = true;
				// 
				// groupBox17
				// 
				this.groupBox17.BackColor = System.Drawing.SystemColors.Control;
				this.groupBox17.Controls.Add(this.butCreateTemporaryTable);
				this.groupBox17.Controls.Add(this.label49);
				this.groupBox17.Controls.Add(this.upDownNumTempCols);
				this.groupBox17.Controls.Add(this.textCreateTempName);
				this.groupBox17.Controls.Add(this.textBox9);
				this.groupBox17.Controls.Add(this.label48);
				this.groupBox17.Location = new System.Drawing.Point(8,6);
				this.groupBox17.Name = "groupBox17";
				this.groupBox17.Size = new System.Drawing.Size(415,100);
				this.groupBox17.TabIndex = 0;
				this.groupBox17.TabStop = false;
				this.groupBox17.Text = "Create Temporary";
				this.groupBox17.UseCompatibleTextRendering = true;
				// 
				// butCreateTemporaryTable
				// 
				this.butCreateTemporaryTable.Location = new System.Drawing.Point(173,54);
				this.butCreateTemporaryTable.Name = "butCreateTemporaryTable";
				this.butCreateTemporaryTable.Size = new System.Drawing.Size(101,23);
				this.butCreateTemporaryTable.TabIndex = 16;
				this.butCreateTemporaryTable.Text = "Create Table";
				this.butCreateTemporaryTable.UseVisualStyleBackColor = true;
				this.butCreateTemporaryTable.Click += new System.EventHandler(this.butCreateTemporaryTable_Click);
				// 
				// label49
				// 
				this.label49.AutoSize = true;
				this.label49.Location = new System.Drawing.Point(12,59);
				this.label49.Name = "label49";
				this.label49.Size = new System.Drawing.Size(99,13);
				this.label49.TabIndex = 15;
				this.label49.Text = "Number of Columns";
				// 
				// upDownNumTempCols
				// 
				this.upDownNumTempCols.Location = new System.Drawing.Point(116,57);
				this.upDownNumTempCols.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
				this.upDownNumTempCols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
				this.upDownNumTempCols.Name = "upDownNumTempCols";
				this.upDownNumTempCols.Size = new System.Drawing.Size(46,20);
				this.upDownNumTempCols.TabIndex = 14;
				this.upDownNumTempCols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
				// 
				// textCreateTempName
				// 
				this.textCreateTempName.Location = new System.Drawing.Point(159,19);
				this.textCreateTempName.Name = "textCreateTempName";
				this.textCreateTempName.Size = new System.Drawing.Size(123,20);
				this.textCreateTempName.TabIndex = 13;
				// 
				// textBox9
				// 
				this.textBox9.Location = new System.Drawing.Point(115,19);
				this.textBox9.Name = "textBox9";
				this.textBox9.ReadOnly = true;
				this.textBox9.Size = new System.Drawing.Size(46,20);
				this.textBox9.TabIndex = 12;
				this.textBox9.Text = "temp";
				this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
				// 
				// label48
				// 
				this.label48.Location = new System.Drawing.Point(12,21);
				this.label48.Name = "label48";
				this.label48.Size = new System.Drawing.Size(100,16);
				this.label48.TabIndex = 11;
				this.label48.Text = "New Table Name";
				this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
				// 
				// FormImport
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
				this.ClientSize = new System.Drawing.Size(1156,842);
				this.Controls.Add(this.tabContr);
				this.Controls.Add(this.grid);
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
				this.Name = "FormImport";
				this.ShowInTaskbar = false;
				this.Text = "Import";
				this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
				this.Load += new System.EventHandler(this.FormImport_Load);
				((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
				this.tabFinalImport.ResumeLayout(false);
				this.tabScript.ResumeLayout(false);
				this.tabScript.PerformLayout();
				this.tabEdit.ResumeLayout(false);
				this.groupBox12.ResumeLayout(false);
				this.groupBox11.ResumeLayout(false);
				this.groupBox11.PerformLayout();
				this.groupBox10.ResumeLayout(false);
				this.groupBox9.ResumeLayout(false);
				this.groupBox1.ResumeLayout(false);
				this.groupBox1.PerformLayout();
				this.tabRows.ResumeLayout(false);
				this.groupBox22.ResumeLayout(false);
				this.groupBox22.PerformLayout();
				this.groupBox21.ResumeLayout(false);
				this.groupBox21.PerformLayout();
				this.groupBox18.ResumeLayout(false);
				this.groupBox18.PerformLayout();
				this.groupBox19.ResumeLayout(false);
				this.groupBox19.PerformLayout();
				((System.ComponentModel.ISupportInitialize)(this.upDownCopyToInc)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.upDownCopyFromInc)).EndInit();
				this.tabQuery.ResumeLayout(false);
				this.tabQuery.PerformLayout();
				this.tabColumns.ResumeLayout(false);
				this.groupBox20.ResumeLayout(false);
				this.groupBox14.ResumeLayout(false);
				this.groupBox14.PerformLayout();
				this.groupBox8.ResumeLayout(false);
				this.groupBox2.ResumeLayout(false);
				this.groupBox6.ResumeLayout(false);
				this.groupBox4.ResumeLayout(false);
				this.groupBox4.PerformLayout();
				this.groupBox5.ResumeLayout(false);
				this.tabPK.ResumeLayout(false);
				this.groupBox7.ResumeLayout(false);
				this.tabSpecialtyF.ResumeLayout(false);
				this.groupBox15.ResumeLayout(false);
				this.groupBox15.PerformLayout();
				this.groupBox13.ResumeLayout(false);
				this.groupBox13.PerformLayout();
				this.groupBox3.ResumeLayout(false);
				this.groupBox3.PerformLayout();
				this.tabLoadData.ResumeLayout(false);
				this.tabLoadData.PerformLayout();
				this.groupBox16.ResumeLayout(false);
				this.groupBox16.PerformLayout();
				this.tabContr.ResumeLayout(false);
				this.tabTable.ResumeLayout(false);
				this.groupBox17.ResumeLayout(false);
				this.groupBox17.PerformLayout();
				((System.ComponentModel.ISupportInitialize)(this.upDownNumTempCols)).EndInit();
				this.ResumeLayout(false);

		}
		#endregion

		private void FormImport_Load(object sender, System.EventArgs e){
			if(FormChooseDatabase.DBtype!=DatabaseType.MySql) {
				MessageBox.Show("This tool only works with MySQL.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			grid.Width=ClientSize.Width-3;
			grid.Height=ClientSize.Height-grid.Top-2;
			tabContr.Width=ClientSize.Width-3;
			FillTableNames();
			currentDb=MiscData.GetCurrentDatabase();
			if(!Regex.IsMatch(currentDb,@"^[a-z0-9]+$")){
				MsgBox.Show(this,"Name of database is invalid for use with this tool.  Must be all lowercase, numbers optional, but with no other characters.  Use the format lnamefname of the dentist.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			/*if(Application.CurrentCulture.Name=="en-US"){
				MsgBox.Show(this,"This database was not derived from the standard usa database.  You must use a blank usa database.");
				DialogResult=DialogResult.Cancel;
				return;
			}*/
			string command="SELECT COUNT(*) FROM patient";
			if(General.GetCount(command)!="0"){
				if(!MsgBox.Show(this,true,"Warning! This database already has at least one patient.  It is typically recommended to start with a blank database.  Continue anyway?"))
				{
					DialogResult=DialogResult.Cancel;
					return;
				}
			}
			if(comboTableName.Items.Count>0){
				comboTableName.SelectedIndex=0;//this triggers a fillgrid
				//FillGrid();
				GetPK();
			}
			else{
				MsgBox.Show(this,"Warning.  This feature is only intended for advanced users doing data conversions from other software.  It is not yet easy enough for beginners to use.");
			}
			GetAllowedCols();
			//set script file:
			if(File.Exists(GetScriptPath())){
				textScript.Text=File.ReadAllText(GetScriptPath());
			}
			else{
				FileStream fs=File.Create(GetScriptPath());
				fs.Flush();
				fs.Dispose();
			}
		}
		
		private string GetScriptPath(){
			string progPath=Application.ExecutablePath.Remove(
				 Application.ExecutablePath.LastIndexOf(Path.DirectorySeparatorChar));
			return ODFileUtils.CombinePaths(progPath,currentDb+".txt");
		}

		///<summary>Depending on whether this is patients, carriers, or..., this sets a few of the comboboxes and also enables proper validation when doing the final export.</summary>
		private void GetAllowedCols(){
			if(radioPatients.Checked){
				AllowedColNames=new string[]
				{
					"Address",
					"Address2",
					"AddrNote",
					"Balance",
					"Bal_0_30",
					"Bal_31_60",
					"Bal_61_90",
					"BalOver90",
					"Birthdate",
					"ChartNumber",
					"City",
					"DateFirstVisit",
					"FName",
					"Gender",
					"Guarantor",
					"HmPhone",
					"LName",
					"MedicaidID",
					"MedUrgNote",
					"MiddleI",
					"PatNum",
					"Position",
					"Preferred",
					"PriProv",
					"SSN",
					"State",
					"WkPhone",
					"Zip",
					"PatStatus"
				};
			}
			else if(radioCarriers.Checked){
				AllowedColNames=new string[]
				{
					"Address",
					"Address2",
					"CarrierNum",
					"CarrierName",
					"City",
					"Phone",
					"State",
					"Zip"
				};
			}
			for(int i=0;i<AllowedColNames.Length;i++){
				comboColNewName.Items.Add(AllowedColNames[i]);
				comboColAdd.Items.Add(AllowedColNames[i]);
			}
		}
		
		private void butBrowse_Click(object sender, System.EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			//dlg.InitialDirectory=textRawPath.Text;
			if(dlg.ShowDialog()==DialogResult.Cancel){
				return;
			}
			textFileName.Text=dlg.FileName;
			int fileNameStartPos=dlg.FileName.LastIndexOf(Path.DirectorySeparatorChar)+1;
			string fileName=dlg.FileName.Substring(fileNameStartPos,dlg.FileName.Length-fileNameStartPos);
			int fileNameEndLen=(fileName+".").IndexOf('.');
			textNewTable.Text=fileName.Substring(0,fileNameEndLen).ToLower();
		}

		private void butLoad_Click(object sender, System.EventArgs e) {
			if(textNewTable.Text=="" || textFileName.Text==""){
				MsgBox.Show(this,"Please enter a table name and a file name");
				return;
			}
			if(!File.Exists(textFileName.Text)){
				MsgBox.Show(this,"File does not exist.");
				return;
			}
			string newTable="temp"+textNewTable.Text;
			if(!Regex.IsMatch(newTable,@"^[a-z]+$")){
				MsgBox.Show(this,"Table name must be all lowercase letters.");
				return;
			}
			//make sure table doesn't already exist
			string command="SHOW TABLES";
			DataTable tempT=General.GetTable(command);
			for(int i=0;i<tempT.Rows.Count;i++){
				if(tempT.Rows[i][0].ToString()==newTable){
					MsgBox.Show(this,"Table already exists.");
					return;
				}
			}			
			//get a list of the new column name
			//missing feature: check for duplicate column names
			string line="";
			try{
				using(StreamReader sr=new StreamReader(textFileName.Text)){
					line=sr.ReadLine(); 
				}
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			if(line.Length==0){
				MsgBox.Show(this,"First line is blank.");
				return;
			}
			line=line.Replace("\"","");
			string[] colNames=line.Split(new char[] {'\t'});	
			if(this.radioGenerateColNames.Checked){
				colNames=new string[colNames.Length];//Get number of columns.
				for(int i=0;i<colNames.Length;i++){
						colNames[i]="tempCol"+(i+1).ToString();
				}
			}			
			//ArrayList ALcolNames=new ArrayList();
			//ALcolNames.AddRange(colNames);
			command="CREATE TABLE "+newTable+"(";
			for(int i=0;i<colNames.Length;i++){
				colNames[i]=Regex.Replace(colNames[i],"[^a-zA-Z0-9]","");//gets rid of any character that's not a letter or number.
				//strip a trailing tab on first line
				if(colNames[i]=="" && i==colNames.Length-1){
					command=command.Substring(0,command.Length-1);//remove the last comma
				}
				else{
					//missing feature: check for duplicate column names
					command+=colNames[i]+" text NOT NULL";
					if(i<colNames.Length-1){
						command+=",";
					}
				}
			}
			command+=")";
			General.NonQ(command);
			//We might not be on the server, so make a copy of the file in the temporary directory 
			//on the local computer before loading.
			string newPath=ODFileUtils.CombinePaths(Path.GetTempPath(),Path.GetFileName(textFileName.Text));
			File.Delete(newPath);//Ensure that a file by the same name is not already present (so that the copy is smooth).
			File.Copy(textFileName.Text,newPath);
			command="LOAD DATA INFILE '"+POut.PString(newPath)+"' INTO TABLE "+newTable
				+@" FIELDS TERMINATED BY '\t' "
				+"OPTIONALLY ENCLOSED BY '"+POut.PString("\"")
				+@"' ESCAPED BY '\\' LINES TERMINATED BY '\r\n'";
			//MessageBox.Show("Preparing to run: "+command);
			General.NonQ(command);
			File.Delete(newPath);//Remove temporary file.
			if(this.radioColNamRow.Checked){
				//Remove the first row containing the column names, as they are not useful and possibly harmful to the conversion.
				command="DELETE FROM "+newTable+" LIMIT 1";
				General.NonQ(command);
			}
			FillTableNames();
			comboTableName.SelectedItem=newTable;
			FillGrid();
			//MessageBox.Show("done");
		}

		private void FillTableNames(){
			string command="SHOW TABLES";
			DataTable tempT=General.GetTable(command);
			comboTableName.Items.Clear();
			comboCopyFromTable.Items.Clear();
			comboCopyToTable.Items.Clear();
			comboFindDups.Items.Clear();
			comboDeleteExDupsTable.Items.Clear();
			for(int i=0;i<tempT.Rows.Count;i++){
				if(tempT.Rows[i][0].ToString().Length>4 && tempT.Rows[i][0].ToString().Substring(0,4)=="temp"){
					string val=tempT.Rows[i][0].ToString();
					comboTableName.Items.Add(val);
					comboCopyFromTable.Items.Add(val);
					comboCopyToTable.Items.Add(val);
					comboFindDups.Items.Add(val);
					comboDeleteExDupsTable.Items.Add(val);
				}
			}
		}

		private void FillGrid(){
			if(comboTableName.SelectedItem==null){
				if(comboTableName.Items.Count<1){
						return;
				}
				FillTableNames();
				comboTableName.SelectedIndex=0;
			}
			string command="SELECT * FROM "+POut.PString(comboTableName.SelectedItem.ToString());
 			table=General.GetTable(command);
			comboColEdit.Items.Clear();
			comboColMove.Items.Clear();
			comboColMoveAfter.Items.Clear();
			comboColCombine1.Items.Clear();
			comboColCombine2.Items.Clear();
			comboColRename.Items.Clear();
			comboColDelete.Items.Clear();
			comboColDateFrom.Items.Clear();
			comboColDateTo.Items.Clear();
			comboColDateSource.Items.Clear();
			comboColDateDest.Items.Clear();
			comboColPK.Items.Clear();
			comboColCopyFrom.Items.Clear();
			comboColUnused.Items.Clear();
			for(int i=0;i<table.Columns.Count;i++){
				comboColEdit.Items.Add(table.Columns[i].ColumnName);
				comboColMove.Items.Add(table.Columns[i].ColumnName);
				comboColMoveAfter.Items.Add(table.Columns[i].ColumnName);
				comboColCombine1.Items.Add(table.Columns[i].ColumnName);
				comboColCombine2.Items.Add(table.Columns[i].ColumnName);
				comboColRename.Items.Add(table.Columns[i].ColumnName);
				comboColDelete.Items.Add(table.Columns[i].ColumnName);
				comboColDateFrom.Items.Add(table.Columns[i].ColumnName);
				comboColDateTo.Items.Add(table.Columns[i].ColumnName);
				comboColDateSource.Items.Add(table.Columns[i].ColumnName);
				comboColDateDest.Items.Add(table.Columns[i].ColumnName);
				comboColPK.Items.Add(table.Columns[i].ColumnName);
				comboColCopyFrom.Items.Add(table.Columns[i].ColumnName);
				comboColUnused.Items.Add(table.Columns[i].ColumnName);
			}
			grid.SetDataBinding(table,"");
			textRows.Text=table.Rows.Count.ToString();
		}

		///<summary>Gets primary key and sets the combobox in the primary key tab to display it.</summary>
		private void GetPK(){
			//missing feature: check for no table selected
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataTable tempT=General.GetTable(command);
			if(tempT.Rows.Count==0){
				comboColPK.SelectedIndex=-1;
				pkCol="";
			}
			else{
				comboColPK.SelectedItem=tempT.Rows[0][4].ToString();
				pkCol=tempT.Rows[0][4].ToString();
			}
		}

		private void radioPatients_Click(object sender, System.EventArgs e) {
			GetAllowedCols();
		}

		private void radioCarriers_Click(object sender, System.EventArgs e) {
			GetAllowedCols();
		}

		///<summary>Most queries get run goes throught this method EXCEPT the ones that are performing validation.  The query then gets sent to the db as well as to the script file.  The script file is reloaded from disk just to make sure it's perfectly synched. Returns false if the query failed, true otherwise.</summary>
		private bool ProcessCommand(string command){
			if(command[command.Length-1]!=';'){
				 command+=';';//Ensure that commands are always terminated, but only with a single ';', otherwise there are blank queries that are somtimes created due to 2 ';' characters.
			}
			try{
				 General.NonQEx(command);//If SQL command fails, then the SQL statement is not written to the conversion script.
				 StreamWriter writer=File.AppendText(GetScriptPath());
				 writer.Write("\r\n"+command);
				 writer.Flush();
				 writer.Dispose();
				 textScript.Text=File.ReadAllText(GetScriptPath());
			}catch(Exception ex){
				 MessageBox.Show(ex.Message);
				 return false;
			}
			return true;
			//scroll to end?			
		}
		
		private void butSetPK_Click(object sender, System.EventArgs e) {
			if(comboColPK.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column name first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataTable tempT=General.GetTable(command);
			if(tempT.Rows.Count!=0){//if a primary key exists
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP PRIMARY KEY";
				General.NonQ(command);
			}
			pkCol=comboColPK.SelectedItem.ToString();
			//first, test to see if it's a blank column
			command="SELECT COUNT(*) FROM "+comboTableName.SelectedItem.ToString()+" WHERE "+pkCol+"!=''";
			tempT=General.GetTable(command);
			if(tempT.Rows[0][0].ToString()=="0"){//all blank
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" CHANGE "+pkCol+" "+pkCol
					+" int unsigned NOT NULL auto_increment, "
					+" ADD PRIMARY KEY ("+pkCol+")";
				General.NonQ(command);
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP PRIMARY KEY,"
				//General.NonQ(command);
				//command="ALTER TABLE "+comboTableName.SelectedItem.ToString()
					+" CHANGE "+pkCol+" "+pkCol+" text NOT NULL,"
					+" ADD PRIMARY KEY ("+pkCol+"(10))";
				General.NonQ(command);
			}
			else{//primary keys already exist.
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" ADD PRIMARY KEY ("+pkCol+"(10))";
				bool commandSuccess=true;
				try{
						General.NonQEx(command);
				}catch{
						commandSuccess=false;
				}
				if(!commandSuccess){
					command="SELECT COUNT(*) - COUNT(DISTINCT "+pkCol+") FROM "+comboTableName.SelectedItem.ToString();
					DataTable dupCount=General.GetTable(command);
					if(MessageBox.Show("There are "+PIn.PInt(dupCount.Rows[0][0].ToString())
						+" duplicate values in the given column. Remove duplicates and continue?",
						"Duplicate Values",
						MessageBoxButtons.YesNo)==DialogResult.Yes){
						command="ALTER IGNORE TABLE "+comboTableName.SelectedItem.ToString()+" ADD PRIMARY KEY ("+pkCol+"(10))";
						General.NonQ(command);
					}
				}
			}
			FillGrid();
			GetPK();
			Cursor=Cursors.Default;
		}

		private void butClearPK_Click(object sender, System.EventArgs e) {
			string command="SHOW INDEX FROM "+POut.PString(comboTableName.SelectedItem.ToString());
			DataTable tempT=General.GetTable(command);
			if(tempT.Rows.Count==0){
				MsgBox.Show(this,"No primary key to clear.");
				GetPK();
			}
			else{//if a primary key exists
				command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" DROP PRIMARY KEY";
				General.NonQ(command);
				GetPK();
				MessageBox.Show("done");
			}
		}

		private void comboTableName_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillGrid();
			GetPK();
		}

		private void butRefresh_Click(object sender, System.EventArgs e) {
			if(comboTableName.SelectedIndex==-1){
				table=new DataTable();
				grid.SetDataBinding(table,"");
				textRows.Text=table.Rows.Count.ToString();
				return;
			}
			FillGrid();
			GetPK();
		}

		private void butDeleteTable_Click(object sender, System.EventArgs e) {
			if(comboTableName.SelectedIndex==-1){
				MsgBox.Show(this,"No table to delete");
				return;
			}
			if(!MsgBox.Show(this,true,"Delete the temp table?")){
				return;
			}
			string command="DROP TABLE "+comboTableName.SelectedItem;
			General.NonQ(command);
			FillTableNames();
			if(comboTableName.Items.Count>0){
				comboTableName.SelectedIndex=0;
			}
			//clear existing grid
			if(comboTableName.SelectedIndex==-1){
				table=new DataTable();
				grid.SetDataBinding(table,"");
				textRows.Text=table.Rows.Count.ToString();
				return;
			}
			FillGrid();
			GetPK();
		}

		private void butSubstitute_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColEdit.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(textSubstOld.Text.Length==0){
				MsgBox.Show(this,"Please enter an old value first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColEdit.SelectedItem.ToString();
			int count=0;
			string newVal;
			string command;
			for(int i=0;i<table.Rows.Count;i++){
				newVal=table.Rows[i][colName].ToString().Replace(textSubstOld.Text,textSubstNew.Text);
				if(newVal!=table.Rows[i][colName].ToString()){
					count++;
					command="UPDATE "+comboTableName.SelectedItem.ToString()+" SET "+colName+"='"+POut.PString(newVal)+"' "
						+"WHERE "+pkCol+"='"+POut.PString(table.Rows[i][pkCol].ToString())+"'";
					ProcessCommand(command);
				}
			}
			//textSubstOld.Text="";
			//textSubstNew.Text="";
			int selectedCol=comboColEdit.SelectedIndex;
			FillGrid();
			comboColEdit.SelectedIndex=selectedCol;
			Cursor=Cursors.Default;
			MessageBox.Show(count.ToString()+" "+Lan.g(this,"rows changed"));
		}

		private void butColClear_Click(object sender, System.EventArgs e) {
			if(comboColEdit.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColEdit.SelectedItem.ToString();
			string command="UPDATE "+comboTableName.SelectedItem.ToString()+" SET "+colName+"=''";
			ProcessCommand(command);
			comboColEdit.SelectedIndex=-1;
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butColCap_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColEdit.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColEdit.SelectedItem.ToString();
			string oldVal;
			StringBuilder strBuild;
			string command;
			for(int i=0;i<table.Rows.Count;i++){
				oldVal=table.Rows[i][colName].ToString();
				if(oldVal==""){
					continue;
				}
				strBuild=new StringBuilder();
				for(int s=0;s<oldVal.Length;s++){
					if(s==0 || oldVal.Substring(s-1,1)==" " || oldVal.Substring(s-1,1)=="." || oldVal.Substring(s-1,1)=="-"
						|| (s==2 && strBuild[0]=='M' && strBuild[1]=='c')){
						strBuild.Append(oldVal.Substring(s,1).ToUpper());
					}
					else{
						strBuild.Append(oldVal.Substring(s,1).ToLower());
					}
				}
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())
					+" SET "+colName+"='"+POut.PString(strBuild.ToString())+"' "
					+"WHERE "+pkCol+"='"+table.Rows[i][pkCol].ToString()+"'";
				ProcessCommand(command);
			}
			comboColEdit.SelectedIndex=-1;
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butColCapAll_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName;
			string oldVal;
			StringBuilder strBuild;
			string command;
			for(int j=0;j<table.Columns.Count;j++){
				colName=table.Columns[j].ColumnName;
				if(radioPatients.Checked){
					if(colName!="Address" && colName!="Address2" && colName!="City" && colName!="FName"
						&& colName!="LName" && colName!="Preferred")
					{
						continue;
					}
				}
				else if(radioCarriers.Checked){
					if(colName!="Address" && colName!="Address2" && colName!="CarrierName" && colName!="City"){
						continue;
					}
				}
				for(int i=0;i<table.Rows.Count;i++){
					oldVal=table.Rows[i][colName].ToString();
					if(oldVal==""){
						continue;
					}
					strBuild=new StringBuilder();
					for(int s=0;s<oldVal.Length;s++){
						if(s==0 || oldVal.Substring(s-1,1)==" " || oldVal.Substring(s-1,1)=="." || oldVal.Substring(s-1,1)=="-"
							|| (s==2 && strBuild[0]=='M' && strBuild[1]=='c')){
							strBuild.Append(oldVal.Substring(s,1).ToUpper());
						}
						else{
							strBuild.Append(oldVal.Substring(s,1).ToLower());
						}
					}
					command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())
						+" SET "+colName+"='"+POut.PString(strBuild.ToString())+"' "
						+"WHERE "+pkCol+"='"+table.Rows[i][pkCol].ToString()+"'";
					ProcessCommand(command);
				}//for rows
			}//for columns
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butColPad_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColEdit.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			int padWidth=0;
			try{
				padWidth=Convert.ToInt32(textPadLength.Text);
			}
			catch{
				MsgBox.Show(this,"Please enter a valid final length first.");
				return;
			}
			if(textPadChar.Text.Length!=1){
				MsgBox.Show(this,"Please enter a pad character first.");
				return;
			}
			char padChar;
			try{
				padChar=Convert.ToChar(textPadChar.Text);
			}
			catch{
				MsgBox.Show(this,"Please enter a valid pad character first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColEdit.SelectedItem.ToString();
			string oldVal;
			string newVal;
			string command;
			for(int i=0;i<table.Rows.Count;i++){
				oldVal=table.Rows[i][colName].ToString();
				//if(oldVal==""){
				//	continue;
				//}
				newVal=oldVal.PadLeft(padWidth,padChar);
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())
					+" SET "+colName+"='"+POut.PString(newVal)+"' "
					+"WHERE "+pkCol+"='"+table.Rows[i][pkCol].ToString()+"'";
				ProcessCommand(command);
			}
			comboColEdit.SelectedIndex=-1;
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butColCopy_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColEdit.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(comboColCopyFrom.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColEdit.SelectedItem.ToString();
			string colFrom=comboColCopyFrom.SelectedItem.ToString();
			string command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())
				+" SET "+colName+"="+colFrom;
			ProcessCommand(command);
			comboColEdit.SelectedIndex=-1;
			comboColCopyFrom.SelectedIndex=-1;
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butConvertDate_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColDateFrom.SelectedIndex==-1 || comboColDateTo.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			DateTime baseDate=DateTime.MinValue;
			try{
				baseDate=DateTime.Parse(textDateBase.Text);
			}
			catch{
				MsgBox.Show(this,"Please enter valid base date first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colFrom=comboColDateFrom.SelectedItem.ToString();
			string colTo=comboColDateTo.SelectedItem.ToString();
			DateTime newDate;
			string command;
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i][colFrom].ToString()==""){
					newDate=DateTime.MinValue;
				}
				else{
					try{
						newDate=baseDate.AddDays(PIn.PDouble(table.Rows[i][colFrom].ToString()));
					}
					catch{
						MessageBox.Show("Value out of range: "+table.Rows[i][colFrom].ToString());
						break;
					}
				}
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())+" SET "+colTo+"="
					+POut.PDate(newDate)+" "
					+"WHERE "+POut.PString(pkCol)+"='"+table.Rows[i][pkCol].ToString()+"'";
				ProcessCommand(command);
			}
			//comboColDateFrom.Text="";
			//comboColDateTo.Text="";
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butFixDates_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			if(comboColDateSource.SelectedIndex==-1 || comboColDateDest.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			if(textDateOldFormats.Text=="" || textDateNewFormat.Text==""){
				MsgBox.Show(this,"Please enter date formats first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colSource=comboColDateSource.SelectedItem.ToString();
			string colDest=comboColDateDest.SelectedItem.ToString();
			string newVal;
			DateTime date;
			string command;
			string[] formats=textDateOldFormats.Text.Split(new char[] {','});
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i][colSource].ToString()==""){
					continue;
				}
				try{
					date=DateTime.ParseExact(table.Rows[i][colSource].ToString(),formats,Application.CurrentCulture,DateTimeStyles.AllowWhiteSpaces);
				}
				catch{
					continue;
				}
				newVal=date.ToString(textDateNewFormat.Text);
				command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())+" SET "+colDest+"='"+POut.PString(newVal)+"' "
					+"WHERE "+POut.PString(pkCol)+"='"+table.Rows[i][pkCol].ToString()+"'";
				ProcessCommand(command);
			}
			//comboColDateSource.Text="";
			//comboColDateDest.Text="";
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butGuarantor_Click(object sender, System.EventArgs e) {
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
				MessageBox.Show("Does not work with Oracle.");
				return;
			}
			if(!radioPatients.Checked){
				MsgBox.Show(this,"Must be set to patient table type in the primary key tab.");
				return;
			}
			if(checkDontUsePK.Checked){
				MsgBox.Show(this,"You must first uncheck the box in the Primary Keys tab called 'Do not use the primary key values in the final import'.");
				return;
			}
			if(!radioInsert.Checked){
				MsgBox.Show(this,"You must first check the box in the Final Import tab called 'Insert new rows'.");
				return;
			}
			if(table.Columns["ChartNumber"]==null){
				MsgBox.Show(this,"You must first create a column called ChartNumber, and fill it with unique values.");
				return;
			}
			//Make sure every ChartNumber is filled and is unique
			ArrayList chartNumList=new ArrayList();//strings
			for(int r=0;r<table.Rows.Count;r++){
				if(table.Rows[r]["ChartNumber"].ToString()==""){
					MsgBox.Show(this,"The ChartNumber column cannot have blank values.");
					return;
				}
				if(chartNumList.Contains(table.Rows[r]["ChartNumber"].ToString())){
					MessageBox.Show(Lan.g(this,"The ChartNumber column contains a duplicate value: ")+table.Rows[r]["ChartNumber"].ToString());
					return;
				}
				else{
					chartNumList.Add(table.Rows[r]["ChartNumber"].ToString());
				}
			}
			if(table.Columns["tempGuarantor"]==null){
				MsgBox.Show(this,"You must first create a column called tempGuarantor, and fill it with references to the guarantors.");
				return;
			}
			if(table.Columns["Guarantor"]==null){
				MsgBox.Show(this,"You must first create an empty column called Guarantor.");
				return;
			}
			if(table.Columns["PatNum"]==null){
				MsgBox.Show(this,"You must first create a column called PatNum and make it the primary key.");
				return;
			}
			if(pkCol!="PatNum"){
				MsgBox.Show(this,"Please make PatNum the primary key first.");
				return;
			}
			//check for valid numbers in PatNum column
			for(int r=0;r<table.Rows.Count;r++){
				if(!Regex.IsMatch(table.Rows[r]["PatNum"].ToString(),@"^\d+$")){
					MessageBox.Show(Lan.g(this,"Invalid value in column")+" PatNum: "
						+table.Rows[r]["PatNum"].ToString()+". "+Lan.g(this,"Must contain only digits."));
					return;
				}
			}
			//Make sure none of the patnums already exists
			string command="SELECT PatNum FROM patient";
			DataTable tempT=General.GetTable(command);
			for(int j=0;j<table.Rows.Count;j++){
				for(int i=0;i<tempT.Rows.Count;i++){
					if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
						MessageBox.Show("Duplicate PatNum. "+table.Rows[j]["PatNum"].ToString()+" already exists.");
					}
				}
			}
			//Make sure every every reference in tempGuarantor refers to a valid ChartNumber
			string invalidGuars="";
			for(int r=0;r<table.Rows.Count;r++){
				if(table.Rows[r]["tempGuarantor"].ToString()==""){
					MsgBox.Show(this,"tempGuarantor cannot have any blank values.");
					return;
				}
				if(chartNumList.Contains(table.Rows[r]["tempGuarantor"].ToString())){
					continue;
				}
				invalidGuars+=table.Rows[r]["tempGuarantor"].ToString()+"\r\n";
			}
			if(invalidGuars!=""){
				if(MessageBox.Show(Lan.g(this,"All values in tempGuarantor should refer to valid values in ChartNumber. CONTINUE ANYWAY ??")+"\r\n"
					+Lan.g(this,"(invalid references found:")+"\r\n"
					+invalidGuars,"",MessageBoxButtons.OKCancel)==DialogResult.Cancel){
						return;
				}
			}
			Cursor=Cursors.WaitCursor;
			command="UPDATE "+comboTableName.SelectedItem.ToString()+" AS t1, "//t1 is the key  //FIXME:UPDATE-MULTIPLE-TABLES
				+comboTableName.SelectedItem.ToString()+" AS t2 "//t2 is the target
				+"SET t2.Guarantor=t1.PatNum "
				+"WHERE t1.ChartNumber=t2.tempGuarantor";
			ProcessCommand(command);
			command="UPDATE "+comboTableName.SelectedItem.ToString()
				+" SET Guarantor=PatNum WHERE Guarantor=''";
			ProcessCommand(command);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butGuarAccount_Click(object sender, System.EventArgs e) {
			if(!radioPatients.Checked){
				MsgBox.Show(this,"Must be set to patient table type in the primary key tab.");
				return;
			}
			if(checkDontUsePK.Checked){
				MsgBox.Show(this,"You must first uncheck the box in the Primary Keys tab called 'Do not use the primary key values in the final import'.");
				return;
			}
			if(!radioInsert.Checked){
				MsgBox.Show(this,"You must first check the box in the Final Import tab called 'Insert new rows'.");
				return;
			}
			if(table.Columns["tempAccountNum"]==null){
				MsgBox.Show(this,"You must first create a column called tempAccountNum which is filled with keys to the family.  This AccountNum has nothing to do with the PatNum keys.");
				return;
			}
			for(int r=0;r<table.Rows.Count;r++){//Make sure every tempAccountNum is filled
				if(table.Rows[r]["tempAccountNum"].ToString()==""){
					MsgBox.Show(this,"The tempAccountNum column cannot have blank values.");
					return;
				}
			}
			if(table.Columns["Guarantor"]==null){
				MsgBox.Show(this,"You must first create an empty column called Guarantor.");
				return;
			}
			if(table.Columns["tempHoH"]==null){
				MsgBox.Show(this,"You must first create a column called tempHoH.  The allowed values are 'x' or blank.");
				return;
			}
			for(int r=0;r<table.Rows.Count;r++){//Make sure every tempHoH is valid
				if(table.Rows[r]["tempHoH"].ToString()!="" && table.Rows[r]["tempHoH"].ToString()!="x"){
					MessageBox.Show(Lan.g(this,"The only allowed values in the tempHoH column are 'x' or blank. Value found: ")
						+"'"+table.Rows[r]["tempHoH"].ToString()+"'");
					return;
				}
			}
			if(table.Columns["PatNum"]==null){
				MsgBox.Show(this,"You must first create a column called PatNum and make it the primary key.");
				return;
			}
			if(pkCol!="PatNum"){
				MsgBox.Show(this,"Please make PatNum the primary key first.");
				return;
			}
			//check for valid numbers in PatNum column
			for(int r=0;r<table.Rows.Count;r++){
				if(!Regex.IsMatch(table.Rows[r]["PatNum"].ToString(),@"^\d+$")){
					MessageBox.Show(Lan.g(this,"Invalid value in column")+" PatNum: "
						+table.Rows[r]["PatNum"].ToString()+". "+Lan.g(this,"Must contain only digits."));
					return;
				}
			}
			//Make sure none of the patnums already exists
			string command="SELECT PatNum FROM patient";
			DataTable tempT=General.GetTable(command);
			for(int j=0;j<table.Rows.Count;j++){
				for(int i=0;i<tempT.Rows.Count;i++){
					if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
						MessageBox.Show("Duplicate PatNum. "+table.Rows[j]["PatNum"].ToString()+" already exists.");
					}
				}
			}
			Cursor=Cursors.WaitCursor;
			//create a hashtable to store all the account numbers in
			Hashtable hashAccounts=new Hashtable();//key=AccountNum, value=PatNum
			//first loop only considers tempHoH
			for(int r=0;r<table.Rows.Count;r++){
				if(table.Rows[r]["tempHoH"].ToString()!="x"){
					continue;
				}
				if(!hashAccounts.ContainsKey(table.Rows[r]["tempAccountNum"].ToString())){
					hashAccounts.Add(table.Rows[r]["tempAccountNum"].ToString(),table.Rows[r]["PatNum"].ToString());
				}
			}
			//second loop only considers non tempHoH
			for(int r=0;r<table.Rows.Count;r++){
				if(table.Rows[r]["tempHoH"].ToString()=="x"){
					continue;
				}
				if(!hashAccounts.ContainsKey(table.Rows[r]["tempAccountNum"].ToString())){
					hashAccounts.Add(table.Rows[r]["tempAccountNum"].ToString(),table.Rows[r]["PatNum"].ToString());
				}
			}
			//third loop does the actual update
			for(int r=0;r<table.Rows.Count;r++){
				command="UPDATE "+comboTableName.SelectedItem.ToString()
					+" SET Guarantor='"+hashAccounts[table.Rows[r]["tempAccountNum"].ToString()]
					+"' WHERE PatNum='"+table.Rows[r]["PatNum"].ToString()+"'";
				ProcessCommand(command);
			}
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void butCombine_Click(object sender, System.EventArgs e) {
			if(comboColCombine1.SelectedIndex==-1 || comboColCombine1.SelectedIndex==-1){
				MsgBox.Show(this,"Please select columns first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string col1=comboColCombine1.SelectedItem.ToString();
			string col2=comboColCombine2.SelectedItem.ToString();
			string sep=textSepChar.Text;
			string command;
      command="UPDATE "+POut.PString(comboTableName.SelectedItem.ToString())+" SET "+col1+"=CONCAT("+col1+",'"+sep+"',"+col2+")";
			ProcessCommand(command);
      command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" DROP "+col2;
			ProcessCommand(command);
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butRename_Click(object sender, System.EventArgs e) {
			if(comboColRename.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(comboColNewName.Text==""){
				MsgBox.Show(this,"Please enter a new name.");
				return;
			}
			//note that user has a list of acceptable col names to choose from, but they may also type in their own name.
			string newName=comboColNewName.Text;
			if(!Regex.IsMatch(newName,@"^[a-zA-Z0-9]+$")){
				MsgBox.Show(this,"New name must be only upper and lowercase letters and numbers with no other symbols.");
				return;
			}
			for(int i=0;i<comboColRename.Items.Count;i++){//go through the list of existing col names
				if(newName==comboColRename.Items[i].ToString()){
					MsgBox.Show(this,"The new column name duplicates an existing column name.");
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColRename.SelectedItem.ToString();
			string command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" CHANGE "+colName
				+" "+newName+" text NOT NULL";
			ProcessCommand(command);
			FillGrid();
			GetPK();//in case the primary key column was renamed
			Cursor=Cursors.Default;
			//MessageBox.Show("done");
		}

		private void butMoveCol_Click(object sender, System.EventArgs e) {
			//if(pkCol==""){
			//	MessageBox.Show("Please set a primary key first.");
			//	return;
			//}
			if(comboColMove.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			if(radioAfter.Checked && comboColMoveAfter.SelectedIndex==-1){
				MsgBox.Show(this,"A column must be selected to move after.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string colName=comboColMove.SelectedItem.ToString();
			string colAfter="";
			if(radioAfter.Checked){
				colAfter=comboColMoveAfter.SelectedItem.ToString();
			}
			string command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" MODIFY "+colName+" text NOT NULL ";
			if(radioFirst.Checked){
				command+="FIRST";
			}
			else{
				command+="AFTER "+colAfter;
			}
			ProcessCommand(command);
			FillGrid();
			Cursor=Cursors.Default;
		}

		private void radioFirst_Click(object sender, System.EventArgs e) {
			comboColMoveAfter.Enabled=false;
			comboColMoveAfter.SelectedIndex=-1;
		}

		private void radioAfter_Click(object sender, System.EventArgs e) {
			comboColMoveAfter.Enabled=true;
		}

		private void butDeleteCol_Click(object sender, System.EventArgs e) {
			//if(pkCol==""){
			//	MessageBox.Show("Please set a primary key first.");
			//	return;
			//}
			if(comboColDelete.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a column first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string col=comboColDelete.SelectedItem.ToString();
			string command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP "+col;
			ProcessCommand(command);
			FillGrid();
			Cursor=Cursors.Default;
			//MessageBox.Show("done");
		}

		private void butColsDelete_Click(object sender, System.EventArgs e) {
			int numCols=0;
			try{
				numCols=Convert.ToInt32(textColsDelete.Text);
			}
			catch{
				MsgBox.Show(this,"Please enter a valid number of columns first.");
				return;
			}
			if(numCols>table.Columns.Count-1){
				MsgBox.Show(this,"Number of columns is too high.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string col;
			string command;
			for(int i=0;i<numCols;i++){
				col=table.Columns[table.Columns.Count-1-i].ColumnName;
				command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" DROP "+col;
				ProcessCommand(command);
			}
			FillGrid();
			Cursor=Cursors.Default;
			//MessageBox.Show("done");
		}

		private void butAddCol_Click(object sender, System.EventArgs e) {
			if(comboColAdd.Text==""){
				MsgBox.Show(this,"Please enter a name first.");
				return;
			}
			//note that user has a list of acceptable col names to choose from, but they may also type in their own name.
			string newName=comboColAdd.Text;
			if(!Regex.IsMatch(newName,@"^[a-zA-Z0-9]+$")){
				MsgBox.Show(this,"Name must be only upper and lowercase letters and numbers with no other symbols.");
				return;
			}
			for(int i=0;i<comboColMove.Items.Count;i++){//go through the list of existing col names
				if(newName==comboColMove.Items[i].ToString()){
					MsgBox.Show(this,"The new column name duplicates an existing column name.");
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			string command="ALTER TABLE "+comboTableName.SelectedItem.ToString()+" ADD "+newName+" text NOT NULL";
			ProcessCommand(command);
			FillGrid();
			comboColAdd.Text="";
			Cursor=Cursors.Default;
		}

		private void butDeleteRows_Click(object sender, System.EventArgs e) {
			if(pkCol==""){
				MsgBox.Show(this,"Please set a primary key first.");
				return;
			}
			ArrayList al=new ArrayList();			
			for(int i=0;i<table.Rows.Count;i++){
				if(grid.IsSelected(i)){
					//MessageBox.Show(((DataTable)grid.DataSource).Rows[i][pkCol].ToString());//table.Rows[i][pkCol].ToString());
					//return;
					al.Add(table.Rows[i][pkCol].ToString());
				}
			}
			if(al.Count==0){
				MsgBox.Show(this,"Please select at least one row first.");
				return;
			}
			string[] selectedRowPKs=new string[al.Count];
			al.CopyTo(selectedRowPKs);
			string command="DELETE FROM "+comboTableName.SelectedItem.ToString()+" WHERE";
			for(int i=0;i<selectedRowPKs.Length;i++){
				if(i>0){
					command+=" OR";
				}
				command+=" "+pkCol+"='"+POut.PString(selectedRowPKs[i])+"'";
			}
			ProcessCommand(command);
			FillGrid();
		}

		private void butQuerySubmit_Click(object sender, System.EventArgs e) {
			if(textQuery.Text==""){
				MsgBox.Show(this,"Please enter a query first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string command=textQuery.Text;
			ProcessCommand(command);
			FillGrid();
			FillTableNames();//Just in case the command was a create table command.
			Cursor=Cursors.Default;
			MessageBox.Show("done");
		}

		private void butRunScript_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			General.NonQ(textScript.Text);
			Cursor=Cursors.Default;
			FillGrid();
			GetPK();
			FillTableNames();
			MessageBox.Show("done");			
		}

		private void butSaveScript_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Overwrite existing file with the text that is showing?")){
				return;
			}
			File.WriteAllText(GetScriptPath(),textScript.Text);
		}

		private void butReloadScript_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Reload script from file, overwriting what is showing?")) {
				return;
			}
			textScript.Text=File.ReadAllText(GetScriptPath());
		}

		private void butValidate_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try{
				ValidateData();
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MessageBox.Show("valid");
		}

		private void butImport_Click(object sender, System.EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try{
				ValidateData();
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
				return;
			}
			//int defaultProvNum=PrefB.GetInt("PracticeDefaultProv");
			int provNum;
			int billType=PrefB.GetInt("PracticeDefaultBillType");
			Patient pat;
			Adjustment adj;
			int adjType=2;
			Patient patOld=new Patient();
			Carrier CarrierCur;
			for(int i=0;i<table.Rows.Count;i++){
				pat=new Patient();
				CarrierCur=new Carrier();
				for(int j=0;j<table.Columns.Count;j++){
					if(radioPatients.Checked){
						string fieldVal=table.Rows[i][j].ToString();
						switch(table.Columns[j].ColumnName){
							case "Address":
								pat.Address=PIn.PString(fieldVal);
								break;
							case "Address2":
								pat.Address2=PIn.PString(fieldVal);
								break;
							case "AddrNote":
								pat.AddrNote=PIn.PString(fieldVal);
								break;
							//Balance
							case "Birthdate":
								pat.Birthdate=PIn.PDate(fieldVal);//handles "" ok
								break;
							case "ChartNumber":
								pat.ChartNumber=PIn.PString(fieldVal);
								break;
							case "City":
								pat.City=PIn.PString(fieldVal);
								break;
							case "DateFirstVisit":
								pat.DateFirstVisit=PIn.PDate(fieldVal);
								break;
							case "FName":
								pat.FName=PIn.PString(fieldVal);
								break;
							case "Gender":
								if(fieldVal=="M")
									pat.Gender=PatientGender.Male;
								else if(fieldVal=="F")
									pat.Gender=PatientGender.Female;
								else if(fieldVal=="U")
									pat.Gender=PatientGender.Unknown;
								break;
							case "Guarantor":
								pat.Guarantor=PIn.PInt(fieldVal);
								break;
							case "HmPhone":
								pat.HmPhone=PIn.PString(fieldVal);
								break;
							case "LName":
								pat.LName=PIn.PString(fieldVal);
								break;
							case "MedicaidID":
								pat.MedicaidID=PIn.PString(fieldVal);
								break;
							case "MedUrgNote":
								pat.MedUrgNote=PIn.PString(fieldVal);
								break;
							case "MiddleI":
								pat.MiddleI=PIn.PString(fieldVal);
								break;
							case "PatNum":
								pat.PatNum=PIn.PInt(fieldVal);
								break;
							case "Position":
								if(fieldVal.ToUpper()=="M")
									pat.Position=PatientPosition.Married;
								else if(fieldVal.ToUpper()=="S")
									pat.Position=PatientPosition.Single;
								else if(fieldVal.ToUpper()=="W")
									pat.Position=PatientPosition.Widowed;
								else if(fieldVal.ToUpper()=="C"){
										pat.Position=PatientPosition.Child;
								}else if(fieldVal.ToUpper()=="D"){
										pat.Position=PatientPosition.Divorced;
								}
								break;
							case "Preferred":
								pat.Preferred=PIn.PString(fieldVal);
								break;
							case "PriProv":
//								pat.PriProv=PIn.PString(fieldVal);
								break;
							case "SSN":
								pat.SSN=PIn.PString(fieldVal);
								break;
							case "State":
								pat.State=PIn.PString(fieldVal);
								break;
							case "WkPhone":
								pat.WkPhone=PIn.PString(fieldVal);
								break;
							case "Zip":
								pat.Zip=PIn.PString(fieldVal);
								break;
							case "PatStatus":
								if(fieldVal.ToLower()=="archived"){
										pat.PatStatus=PatientStatus.Archived;
								}else if(fieldVal.ToLower()=="deceased"){
										pat.PatStatus=PatientStatus.Deceased;
								}else if(fieldVal.ToLower()=="deleted"){
										pat.PatStatus=PatientStatus.Deleted;
								}else if(fieldVal.ToLower()=="inactive"){
										pat.PatStatus=PatientStatus.Inactive;
								}else if(fieldVal.ToLower()=="nonpatient"){
										pat.PatStatus=PatientStatus.NonPatient;
								}else if(fieldVal.ToLower()=="patient"){
										pat.PatStatus=PatientStatus.Patient;
								}
								break;
						}
					}
					else if(radioCarriers.Checked){
						switch(table.Columns[j].ColumnName){
							case "Address":
								CarrierCur.Address=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Address2":
								CarrierCur.Address2=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "CarrierName":
								CarrierCur.CarrierName=PIn.PString(table.Rows[i][j].ToString());
								break;
							//case CarrierNum
							case "City":
								CarrierCur.City=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "State":
								CarrierCur.State=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Zip":
								CarrierCur.Zip=PIn.PString(table.Rows[i][j].ToString());
								break;
							case "Phone":
								CarrierCur.Phone=PIn.PString(table.Rows[i][j].ToString());
								break;
						}
					}
				}//columns
				if(radioPatients.Checked){
					if(radioInsert.Checked){
						pat.BillingType=billType;
					}
					if(table.Columns["Guarantor"]==null){//no guarantor column
						pat.Guarantor=pat.PatNum;
					}
					if(table.Columns["PriProv"]==null){//no prov column
						if(radioInsert.Checked){//must supply a prov anyway
							pat.PriProv=PrefB.GetInt("PracticeDefaultProv");
						}
					}
					else{//prov column
						if(table.Rows[i][table.Columns["PriProv"].Ordinal].ToString()==""){
							pat.PriProv=PrefB.GetInt("PracticeDefaultProv");
						}
						else{
							provNum=0;
							for(int p=0;p<Providers.ListLong.Length;p++){
								if(Providers.ListLong[p].LName==table.Rows[i][table.Columns["PriProv"].Ordinal].ToString()){
									provNum=Providers.ListLong[p].ProvNum;
								}
							}
							if(provNum==0){
								Provider ProvCur=new Provider();
								ProvCur.ItemOrder=Providers.ListLong[Providers.ListLong.Length-1].ItemOrder+1;
								ProvCur.LName=table.Rows[i][table.Columns["PriProv"].Ordinal].ToString();
								ProvCur.Abbr=ProvCur.LName;
								ProvCur.FeeSched=DefB.Short[(int)DefCat.FeeSchedNames][0].DefNum;
								ProvCur.ProvColor=Color.White;
								ProvCur.SigOnFile=true;
								ProvCur.OutlineColor=Color.Gray;
								Providers.Insert(ProvCur);
								Providers.Refresh();//this is because SetInvalid might be too slow
								DataValid.SetInvalid(InvalidTypes.Providers);//also refreshes local
								provNum=ProvCur.ProvNum;
							}
							pat.PriProv=provNum;
						}
					}
					if(radioInsert.Checked){
						if(checkDontUsePK.Checked){
							Patients.Insert(pat,false);
						}
						else{
							Patients.Insert(pat,true);
						}
					}
					else{
						Patients.Update(pat,patOld);
					}
				}
				else if(radioCarriers.Checked){
					if(radioInsert.Checked){
						Carriers.Insert(CarrierCur);
					}
					else{
						//pat.Update(patOld);
					}
				}
				//if the balance column exists. Needs to come AFTER initial insert in case we don't have primary key to work with
				if(radioPatients.Checked){
						if(table.Columns["Balance"]!=null){
								adj=new Adjustment();
								adj.PatNum=PIn.PInt(table.Rows[i][pkCol].ToString());
								adj.AdjAmt=PIn.PDouble(table.Rows[i][table.Columns["Balance"].Ordinal].ToString());
								adj.AdjType=adjType;
								//adj.AdjDate=DateTime.Today;//automatically handled
								adj.ProcDate=DateTime.Today;
								adj.AdjDate=DateTime.Today;
								adj.ProvNum=pat.PriProv;
								Adjustments.InsertOrUpdate(adj,true);
						}
				}
			}//rows
			Cursor=Cursors.Default;
			MessageBox.Show("Done.  Don't forget to run the telephone number utility now.");
		}

		/*
		if(table.Columns["PriProv"]==null){
				MessageBox.Show("null");
			}
			else{
				MessageBox.Show(table.Columns["PriProv"].Ordinal.ToString());
			}
		*/
		/*
		///<summary>Used in various places to search through the column names for a specific match.  Returns either the index of the column, or -1 if not found.</summary>
		private int ColumnIndex(string colName){
			for(int i=0;i<table.Columns.Count;i++){
				if(table.Columns[i].ColumnName==colName){
					return i;
				}
			}
			return -1;
		}*/

		///<summary></summary>
		private void ValidateData(){
			if(checkDontUsePK.Checked){//For now, this only makes sense if just doing inserts.  Later, pk could be multiple fields.
				if(radioUpdate.Checked){
					throw new Exception(Lan.g(this,"Primary key must be used if doing updates."));
				}
			}
			else{//pk's will be used
				if(radioPatients.Checked && pkCol!="PatNum"){
					throw new Exception(Lan.g(this,"PatNum must be set as primary key first."));
				}
				else if(radioCarriers.Checked && pkCol!="CarrierNum"){
					throw new Exception(Lan.g(this,"CarrierNum must be set as primary key first."));
				}
			}
			//column names-----------------------------------------------------------------------------------------------------
			string colNamesAll="";
			for(int i=0;i<AllowedColNames.Length;i++){
				if(i>0){
					colNamesAll+=", ";
				}
				colNamesAll+=AllowedColNames[i];
			}
			bool valid;
			for(int i=0;i<table.Columns.Count;i++){
				if(table.Columns[i].ColumnName.Length>4 && table.Columns[i].ColumnName.StartsWith("temp")){
					continue;
				}
				valid=false;
				for(int j=0;j<AllowedColNames.Length;j++){
					if(AllowedColNames[j]==table.Columns[i].ColumnName){
						valid=true;
						break;
					}
				}
				if(!valid){
					throw new Exception(table.Columns[i].ColumnName+Lan.g(this," is not a valid column name.  Column name must start with 'temp' or must be one of the following: ")
						+colNamesAll);
				}
			}
			string warnings="";
			bool allCaps;
			int invalidValues;
			for(int i=0;i<table.Columns.Count;i++){
				//Warnings---------------------------------------------------------------------------------------------------------
				if(radioPatients.Checked){
					switch(table.Columns[i].ColumnName){
						case "Address":
							allCaps=false;
							for(int r=0;r<3;r++){//only check the first 4 rows
								if(!Regex.IsMatch(table.Rows[r][i].ToString(),"[a-z]")){//if no lowercase letters are present
									allCaps=true;
								}
							}
							if(allCaps){
								warnings+="Address: Fix capitalization.\r\n";
							}
							break;
						case "ChartNumber":
							int duplicates=0;
							ArrayList numList=new ArrayList();//strings
							for(int r=0;r<table.Rows.Count;r++){
								if(table.Rows[r][i].ToString()==""){
									continue;
								}
								if(numList.Contains(table.Rows[r][i].ToString())){
									duplicates++;
								}
								else{
									numList.Add(table.Rows[r][i].ToString());
								}
							}
							if(duplicates>0){
								warnings+="ChartNumber: Duplicates found: "+duplicates.ToString()+".\r\n";
							}
							break;
						case "City":
							allCaps=false;
							for(int r=0;r<3;r++){//only check the first 4 rows
								if(!Regex.IsMatch(table.Rows[r][i].ToString(),"[a-z]")){//if no lowercase letters are present
									allCaps=true;
								}
							}
							if(allCaps){
								warnings+="City: Fix capitalization.\r\n";
							}
							break;
						case "FName":
							allCaps=false;
							for(int r=0;r<3;r++){//only check the first 4 rows
								if(!Regex.IsMatch(table.Rows[r][i].ToString(),"[a-z]")){//if no lowercase letters are present
									allCaps=true;
								}
							}
							if(allCaps){
								warnings+="FName: Fix capitalization.\r\n";
							}
							break;
						case "LName":
							allCaps=false;
							for(int r=0;r<3;r++){//only check the first 4 rows
								if(!Regex.IsMatch(table.Rows[r][i].ToString(),"[a-z]")){//if no lowercase letters are present
									allCaps=true;
								}
							}
							if(allCaps){
								warnings+="LName: Fix capitalization.\r\n";
							}
							break;
						case "PriProv":
							invalidValues=0;
							for(int r=0;r<table.Rows.Count;r++){
								if(table.Rows[r][i].ToString()==""){
									continue;
								}
								if(Regex.IsMatch(table.Rows[r][i].ToString(),@"[,\(\) ]")){//, or ( or ) or space
									invalidValues++;
								}
							}
							if(invalidValues>0){
								warnings+="Column PriProv: Should only be Last Name or number of provider.  "
									+"Number of invalid values: "+invalidValues.ToString()+".\r\n";
							}
							break;
						case "Zip":
							invalidValues=0;
							for(int r=0;r<table.Rows.Count;r++){
								if(table.Rows[r][i].ToString().Length!=0
									&& table.Rows[r][i].ToString().Length!=5
									&& table.Rows[r][i].ToString().Length!=10)
								{
									string str=table.Rows[r][i].ToString();
									invalidValues++;
								}
							}
							if(invalidValues>0){
								warnings+="Zip: Number of possible invalid values found: "+invalidValues.ToString()+".\r\n";
							}
							break;
					}
				}
				//Errors-----------------------------------------------------------------------------------------------------------
				for(int r=0;r<table.Rows.Count;r++){
					if(radioPatients.Checked){
						string fieldVal=table.Rows[r][i].ToString();
						switch(table.Columns[i].ColumnName){
							case "Address":
								break;
							case "Address2":
								break;
							case "AddrNote":
								break;
							//Balance
							case "Birthdate":
								if(fieldVal==""){
									continue;
								}
								try{
									DateTime.Parse(fieldVal);
								}
								catch{
									throw new Exception("Column Birthdate, unrecognizable date: "+fieldVal);
								}
								break;
							case "ChartNumber":
								break;
							case "City":
								break;
							case "DateFirstVisit":
								if(fieldVal==""){
									continue;
								}
								try{
									DateTime.Parse(fieldVal);
								}
								catch{
									throw new Exception("Column DateFirstVisit, unrecognizable date: "+fieldVal);
								}
								break;
							case "FName":
								//handled in warnings
								break;
							case "Gender":
								if(fieldVal!="M" && fieldVal!="F" && fieldVal!="U"){
									throw new Exception(Lan.g(this,"Column Gender, invalid value: ")+fieldVal
										+".  Only valid values are M,F, or U.");
								}
								break;
							case "Guarantor":
								if(!Regex.IsMatch(fieldVal,@"^\d+$")){
									throw new Exception(Lan.g(this,"Invalid value in column Guarantor: ")
										+fieldVal+". "+Lan.g(this,"Must contain only digits."));
								}
								break;
							case "HmPhone":
								break;
							case "LName":
								break;
							case "MedicaidID":
								break;
							case "MedUrgNote":
								break;
							case "MiddleI":
								break;
							case "PatNum":
								if(!Regex.IsMatch(fieldVal,@"^\d+$")){
									throw new Exception(Lan.g(this,"Invalid value in column")+" PatNum: "
										+fieldVal+". "+Lan.g(this,"Must contain only digits."));
								}
								break;
							case "Position":
								if(fieldVal!="M"//married
									&& fieldVal!="S"//single
									&& fieldVal!="W"//widowed
									&& fieldVal!="C"//child
									&& fieldVal!="D")//divorced
								{
									throw new Exception("Column Position, invalid value: "+fieldVal+".  Only valid values are M,S,C,D,or W.");
								}
								break;
							case "Preferred":
								break;
							case "SSN":
								if(Regex.IsMatch(fieldVal,"-")){
									throw new Exception(Lan.g(this,"Invalid value in column SSN: ")
										+fieldVal+". "+Lan.g(this,"Cannot contain dashes."));
								}
								break;
							case "State":
								break;
							case "WkPhone":
								break;
							case "Zip":
								break;
							case "PatStatus":
								if(fieldVal.ToLower()!="archived" &&
										fieldVal.ToLower()!="deceased" &&
										fieldVal.ToLower()!="deleted" &&
										fieldVal.ToLower()!="inactive" &&
										fieldVal.ToLower()!="nonpatient" &&
										fieldVal.ToLower()!="patient"){
										throw new Exception("Only allowed values for PatStatus are: archived, deceased, deleted, inactive, nonpatient, and patient. Found the following value in the PatStatus: "+fieldVal);
								}
								break;
						}//switch colName
					}//if patients
				}//r rows
			}//i columns
			if(warnings!=""){
				if(MessageBox.Show("WARNINGS:\r\n"
					+warnings
					+"\r\nYou should CANCEL and fix before continuing.","",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
				{
					throw new Exception("Cancelled");
				}
			}
//need to make sure all guarantor fk entries exist
			if(checkDontUsePK.Checked){
				return;
			}
			if(radioPatients.Checked){
				//make sure no PatNum already exists
				string command="SELECT PatNum FROM patient";
				DataTable tempT=General.GetTable(command);
				bool exists;
				if(radioInsert.Checked){//Insert: no duplicates allowed
					for(int j=0;j<table.Rows.Count;j++){
						for(int i=0;i<tempT.Rows.Count;i++){
							if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
								throw new Exception("Duplicate PatNum. "+table.Rows[j]["PatNum"].ToString()+" already exists.");
							}
						}
					}
				}
				else{//Update: Every primary key must match
					for(int j=0;j<table.Rows.Count;j++){
						exists=false;
						for(int i=0;i<tempT.Rows.Count;i++){
							if(tempT.Rows[i][0].ToString()==table.Rows[j]["PatNum"].ToString()){
								exists=true;
								break;
							}
						}
						if(!exists){
							throw new Exception(Lan.g(this,"Primary key not present in database: ")+table.Rows[j]["PatNum"].ToString());
						}
					}
				}
				
			}//patients
			else if(radioCarriers.Checked){
				
			}
			
		}

		private void radioColNamRow_Click(object sender,EventArgs e) {
				radioColNamRow.Checked=true;
				radioGenerateColNames.Checked=false;
		}

		private void radioGenerateColNames_Click(object sender,EventArgs e) {
				radioColNamRow.Checked=false;
				radioGenerateColNames.Checked=true;
		}

		private void butCreateTemporaryTable_Click(object sender,EventArgs e) {
				if(this.textCreateTempName.Text==""){
						MessageBox.Show("You must first enter a table name.");
						return;
				}
				string command="CREATE TABLE temp"+this.textCreateTempName.Text+" (";
				for(int i=0;i<(int)this.upDownNumTempCols.Value;i++){
						command+="tempCol"+(i+1).ToString()+" text default ''";
						if(i<(int)this.upDownNumTempCols.Value-1){
								command+=',';
						}
				}				
				command+=")";
				ProcessCommand(command);
				FillTableNames();
		}

		private void comboCopyFromTable_SelectedIndexChanged(object sender,EventArgs e) {
				if(comboCopyFromTable.SelectedIndex<0){
						return;
				}
				//Get the column names of the newly selected temporary table.
				string command="SELECT * FROM "+comboCopyFromTable.SelectedItem.ToString()+" LIMIT 1";
				DataTable result=General.GetTable(command);
				listCopyFromTable.Items.Clear();
				for(int i=0;i<result.Columns.Count;i++){
						listCopyFromTable.Items.Add(result.Columns[i].ColumnName);
				}
		}

		private void comboCopyToTable_SelectedIndexChanged(object sender,EventArgs e) {
				if(comboCopyToTable.SelectedIndex<0){
						return;
				}
				//Get the column names of the newly selected temporary table.
				string command="SELECT * FROM "+comboCopyToTable.SelectedItem.ToString()+" LIMIT 1";
				DataTable result=General.GetTable(command);
				listCopyToTable.Items.Clear();
				for(int i=0;i<result.Columns.Count;i++){
						listCopyToTable.Items.Add(result.Columns[i].ColumnName);
				}
		}

		private void butFromTableUp_Click(object sender,EventArgs e) {
				for(int i=0;i<(int)this.upDownCopyFromInc.Value;i++){
						if(listCopyFromTable.SelectedIndex<1){
								return;
						}
						object temp=listCopyFromTable.Items[listCopyFromTable.SelectedIndex];
						listCopyFromTable.Items[listCopyFromTable.SelectedIndex]=listCopyFromTable.Items[listCopyFromTable.SelectedIndex-1];
						listCopyFromTable.Items[listCopyFromTable.SelectedIndex-1]=temp;
						listCopyFromTable.SelectedIndex--;
				}
		}

		private void butFromTableDown_Click(object sender,EventArgs e) {
				for(int i=0;i<(int)this.upDownCopyFromInc.Value;i++){
						if(listCopyFromTable.SelectedIndex>listCopyFromTable.Items.Count-2 || listCopyFromTable.SelectedIndex<0){
								return;
						}
						object temp=listCopyFromTable.Items[listCopyFromTable.SelectedIndex];
						listCopyFromTable.Items[listCopyFromTable.SelectedIndex]=listCopyFromTable.Items[listCopyFromTable.SelectedIndex+1];
						listCopyFromTable.Items[listCopyFromTable.SelectedIndex+1]=temp;
						listCopyFromTable.SelectedIndex++;
				}
		}

		private void butFromTableNoColumn_Click(object sender,EventArgs e) {
				listCopyFromTable.Items.Insert(listCopyFromTable.SelectedIndex+1,"@NO COLUMN");
		}

		private void butToTableUp_Click(object sender,EventArgs e) {
				for(int i=0;i<(int)this.upDownCopyToInc.Value;i++){
						if(listCopyToTable.SelectedIndex<1){
								return;
						}
						object temp=listCopyToTable.Items[listCopyToTable.SelectedIndex];
						listCopyToTable.Items[listCopyToTable.SelectedIndex]=listCopyToTable.Items[listCopyToTable.SelectedIndex-1];
						listCopyToTable.Items[listCopyToTable.SelectedIndex-1]=temp;
						listCopyToTable.SelectedIndex--;
				}
		}

		private void butToTableDown_Click(object sender,EventArgs e) {
				for(int i=0;i<(int)this.upDownCopyToInc.Value;i++){
						if(listCopyToTable.SelectedIndex>listCopyToTable.Items.Count-2 || listCopyToTable.SelectedIndex<0){
								return;
						}
						object temp=listCopyToTable.Items[listCopyToTable.SelectedIndex];
						listCopyToTable.Items[listCopyToTable.SelectedIndex]=listCopyToTable.Items[listCopyToTable.SelectedIndex+1];
						listCopyToTable.Items[listCopyToTable.SelectedIndex+1]=temp;
						listCopyToTable.SelectedIndex++;
				}
		}

		private void butToTableNoColumn_Click(object sender,EventArgs e) {
				listCopyToTable.Items.Insert(listCopyToTable.SelectedIndex+1,"@NO COLUMN");
		}

		private void butCopyAppendRows_Click(object sender,EventArgs e) {
				int numCols=Math.Min(listCopyFromTable.Items.Count,listCopyToTable.Items.Count);
				int numColsCopied=0;
				string command="INSERT INTO "+comboCopyToTable.Text+" (";
				for(int i=0;i<numCols;i++){
						if(listCopyFromTable.Items[i].ToString()[0]!='@' && 
								listCopyToTable.Items[i].ToString()[0]!='@'){
								if(numColsCopied>0){
										command+=",";
								}
								command+=listCopyToTable.Items[i].ToString();
								numColsCopied++;
						}
				}
				command+=") SELECT ";
				if(numColsCopied<1){
						MessageBox.Show("No columns marked for copy.");
						return;
				}
				if(MessageBox.Show("Click yes to allow duplicate rows, no to force distinct rows.",
						"",MessageBoxButtons.YesNo)==DialogResult.No){
						command+="DISTINCTROW ";
				}
				numColsCopied=0;
				for(int i=0;i<numCols;i++){
						if(listCopyFromTable.Items[i].ToString()[0]!='@' && 
								listCopyToTable.Items[i].ToString()[0]!='@'){
								if(numColsCopied>0){
										command+=",";
								}
								command+=listCopyFromTable.Items[i].ToString();
								numColsCopied++;
						}
				}
				command+=" FROM "+comboCopyFromTable.Text;
				ProcessCommand(command);
		}

		private void butColMarkUnused_Click(object sender,EventArgs e) {
			if(this.comboColUnused.Text==""){
				MsgBox.Show(this,"Please choose a name first.");
				return;
			}
			//note that user has a list of acceptable col names to choose from, but they may also type in their own name.
			string newName=this.comboColUnused.Text;
			if(!Regex.IsMatch(newName,@"^[a-zA-Z0-9]+$")){
				MsgBox.Show(this,"Name must be only upper and lowercase letters and numbers with no other symbols.");
				return;
			}
			newName="temp"+newName;
			for(int i=0;i<this.comboColUnused.Items.Count;i++){//go through the list of existing col names
				if(newName==comboColUnused.Items[i].ToString()){
					MsgBox.Show(this,"The new column name duplicates an existing column name.");
					return;
				}
			}
			Cursor=Cursors.WaitCursor;
			string colName=this.comboColUnused.Text;
			string command="ALTER TABLE "+POut.PString(comboTableName.SelectedItem.ToString())+" CHANGE "+colName
				+" "+newName+" text NOT NULL";
			ProcessCommand(command);
			FillGrid();
			GetPK();//in case the primary key column was renamed
			Cursor=Cursors.Default;
		}

		private void comboFindDups_SelectedIndexChanged(object sender,EventArgs e) {
				if(comboFindDups.SelectedIndex<0){
						return;
				}
				string tempTable=comboFindDups.SelectedItem.ToString();
				//Get the column names of the newly selected temporary table.
				string command="SELECT * FROM "+tempTable+" LIMIT 1";
				DataTable result=General.GetTable(command);
				listDupRowCols.Items.Clear();
				for(int i=0;i<result.Columns.Count;i++){
						listDupRowCols.Items.Add(result.Columns[i].ColumnName);
				}
				textDupRowTable.Text=tempTable.Substring(4,tempTable.Length-4)+"dups";
		}

		private void button1_Click(object sender,EventArgs e) {
				if(comboFindDups.Text.Length<4 || comboFindDups.Text.Substring(0,4).ToLower()!="temp"){
						MessageBox.Show("You must first enter a valid temporary table name.");
						return;
				}
				if(listDupRowCols.SelectedItems.Count<1){
						MessageBox.Show("You must first select at least 1 column of the temporary table to perform duplicate analysis on.");
						return;
				}
				if(textDupRowTable.Text==""){
						MessageBox.Show("You must first enter an output temporary table name for the duplicate row results to be placed into.");
						return;
				}
				Cursor=Cursors.WaitCursor;
				//Create the table where the result will be stored.
				string tab="temp"+textDupRowTable.Text;
				string command="CREATE TABLE "+tab+" SELECT * FROM "+comboFindDups.Text;
				General.NonQ(command);
				//Modify the copy table so it is all upper-cased if we care about case insensitivity.
				if(checkFindDupsInsensitive.Checked){
						command="UPDATE "+tab+" SET ";
						for(int i=0;i<listDupRowCols.SelectedItems.Count;i++){
								string colName=listDupRowCols.SelectedItems[i].ToString();
								command+=colName+"=UPPER("+colName+")";
								if(i<listDupRowCols.SelectedItems.Count-1){
										command+=",";
								}
						}
						General.NonQ(command);
				}
				string tab2=tab+"2";
				command="CREATE TABLE "+tab2+" SELECT * FROM "+tab;
				General.NonQ(command);
				command="DELETE FROM "+tab+" WHERE (SELECT COUNT(*) FROM "+tab2+" WHERE ";
				for(int i=0;i<listDupRowCols.SelectedItems.Count;i++){
						string colName=listDupRowCols.SelectedItems[i].ToString();
						command+=tab+"."+colName+"="+tab2+"."+colName;
						if(i<listDupRowCols.SelectedItems.Count-1){
								command+=" AND ";
						}
				}			
				command+=" LIMIT 2)=1";
				General.NonQ(command);
				command="DROP TABLE "+tab2;
				General.NonQ(command);
				Cursor=Cursors.Default;			
				FillTableNames();
				comboTableName.Text=tab;
				FillGrid();
				MessageBox.Show("Done");
			}

			private void butDeleteExDups_Click(object sender,EventArgs e) {
				if(comboDeleteExDupsTable.Text==""){
						MessageBox.Show("You must select a table name first.");
						return;
				}
				string command="";
				//Calculate the current number of rows in the table.
				command="SELECT COUNT(*) FROM "+comboDeleteExDupsTable.Text;
				DataTable result=General.GetTable(command);	
				int oldNumCols=PIn.PInt(result.Rows[0][0].ToString());				
				//Copy the table to another temporary table, removing rows which are exact duplicates.
				string tempTab=comboDeleteExDupsTable.Text+"Copy";
				command="CREATE TABLE "+tempTab+" SELECT DISTINCTROW * FROM "+comboDeleteExDupsTable.Text;
				General.NonQ(command);
				//Now delete the original temporary table.
				command="DROP TABLE "+comboDeleteExDupsTable.Text;
				General.NonQ(command);
				//Then rename the new temporary table to the old table name.
				command="ALTER TABLE "+tempTab+" RENAME TO "+comboDeleteExDupsTable.Text;
				General.NonQ(command);
				//Recalculate the current number of rows in the table.
				command="SELECT COUNT(*) FROM "+comboDeleteExDupsTable.Text;
				result=General.GetTable(command);
				int newNumCols=PIn.PInt(result.Rows[0][0].ToString());
				MessageBox.Show("Deleted "+(oldNumCols-newNumCols)+" duplicate rows.");				
			}

		


	}
}





















