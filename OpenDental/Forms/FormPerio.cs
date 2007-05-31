using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPerio : System.Windows.Forms.Form{
		private OpenDental.UI.Button but7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioLeft;
		private System.Windows.Forms.RadioButton radioRight;
		private OpenDental.UI.Button but3;
		private OpenDental.UI.Button but2;
		private OpenDental.UI.Button but1;
		private OpenDental.UI.Button but6;
		private OpenDental.UI.Button but9;
		private OpenDental.UI.Button but5;
		private OpenDental.UI.Button but4;
		private OpenDental.UI.Button but8;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button butColorBleed;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button butColorPus;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button but0;
		private OpenDental.UI.Button but10;
		private OpenDental.UI.Button butBleed;
		private OpenDental.UI.Button butPus;
		private System.Windows.Forms.CheckBox checkThree;
		private bool localDefsChanged;
		private System.Windows.Forms.ListBox listExams;
		private OpenDental.UI.Button butSkip;
		private OpenDental.UI.Button butPrint;
		private System.Windows.Forms.Button butColorCalculus;
		private System.Windows.Forms.Button butColorPlaque;
		private OpenDental.UI.Button butCalculus;
		private OpenDental.UI.Button butPlaque;
		private System.Windows.Forms.TextBox textIndexPlaque;
		private System.Windows.Forms.TextBox textIndexSupp;
		private System.Windows.Forms.TextBox textIndexBleeding;
		private System.Windows.Forms.TextBox textIndexCalculus;
		private OpenDental.UI.Button butCalcIndex;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textRedProb;
		private OpenDental.UI.Button butCount;
		private System.Windows.Forms.DomainUpDown updownProb;
		private System.Windows.Forms.TextBox textCountProb;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textCountMGJ;
		private System.Windows.Forms.DomainUpDown updownMGJ;
		private System.Windows.Forms.TextBox textRedMGJ;
		private System.Windows.Forms.TextBox textCountGing;
		private System.Windows.Forms.DomainUpDown updownGing;
		private System.Windows.Forms.TextBox textRedGing;
		private System.Windows.Forms.TextBox textCountCAL;
		private System.Windows.Forms.DomainUpDown updownCAL;
		private System.Windows.Forms.TextBox textRedCAL;
		private System.Windows.Forms.TextBox textCountFurc;
		private System.Windows.Forms.DomainUpDown updownFurc;
		private System.Windows.Forms.TextBox textRedFurc;
		private System.Windows.Forms.TextBox textCountMob;
		private System.Windows.Forms.DomainUpDown updownMob;
		private System.Windows.Forms.TextBox textRedMob;
		//private OpenDental.ContrPerio gridP;
		//private OpenDental.ContrPerio contrPerio1;
		private OpenDental.ContrPerio gridP;
		private System.Drawing.Printing.PrintDocument pd2;
		private System.Windows.Forms.PrintDialog printDialog2;
		private bool TenIsDown;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDlg;
		//private int pagesPrinted;
		private ArrayList MissingTeeth;
		private Patient PatCur;
		private PerioExam PerioExamCur;

		///<summary></summary>
		public FormPerio(Patient patCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			PatCur=patCur;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPerio));
			this.butClose = new OpenDental.UI.Button();
			this.but7 = new OpenDental.UI.Button();
			this.but3 = new OpenDental.UI.Button();
			this.but2 = new OpenDental.UI.Button();
			this.but1 = new OpenDental.UI.Button();
			this.but6 = new OpenDental.UI.Button();
			this.but9 = new OpenDental.UI.Button();
			this.but5 = new OpenDental.UI.Button();
			this.but4 = new OpenDental.UI.Button();
			this.but8 = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioRight = new System.Windows.Forms.RadioButton();
			this.radioLeft = new System.Windows.Forms.RadioButton();
			this.butDelete = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.but0 = new OpenDental.UI.Button();
			this.but10 = new OpenDental.UI.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butBleed = new OpenDental.UI.Button();
			this.butPus = new OpenDental.UI.Button();
			this.butColorBleed = new System.Windows.Forms.Button();
			this.butColorPus = new System.Windows.Forms.Button();
			this.butSkip = new OpenDental.UI.Button();
			this.butColorCalculus = new System.Windows.Forms.Button();
			this.butColorPlaque = new System.Windows.Forms.Button();
			this.butCalculus = new OpenDental.UI.Button();
			this.butPlaque = new OpenDental.UI.Button();
			this.butCalcIndex = new OpenDental.UI.Button();
			this.butCount = new OpenDental.UI.Button();
			this.checkThree = new System.Windows.Forms.CheckBox();
			this.butPrint = new OpenDental.UI.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textCountMob = new System.Windows.Forms.TextBox();
			this.updownMob = new System.Windows.Forms.DomainUpDown();
			this.textRedMob = new System.Windows.Forms.TextBox();
			this.textCountFurc = new System.Windows.Forms.TextBox();
			this.updownFurc = new System.Windows.Forms.DomainUpDown();
			this.textRedFurc = new System.Windows.Forms.TextBox();
			this.textCountCAL = new System.Windows.Forms.TextBox();
			this.updownCAL = new System.Windows.Forms.DomainUpDown();
			this.textRedCAL = new System.Windows.Forms.TextBox();
			this.textCountGing = new System.Windows.Forms.TextBox();
			this.updownGing = new System.Windows.Forms.DomainUpDown();
			this.textRedGing = new System.Windows.Forms.TextBox();
			this.textCountMGJ = new System.Windows.Forms.TextBox();
			this.updownMGJ = new System.Windows.Forms.DomainUpDown();
			this.textRedMGJ = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textCountProb = new System.Windows.Forms.TextBox();
			this.updownProb = new System.Windows.Forms.DomainUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.textRedProb = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.listExams = new System.Windows.Forms.ListBox();
			this.textIndexPlaque = new System.Windows.Forms.TextBox();
			this.textIndexSupp = new System.Windows.Forms.TextBox();
			this.textIndexBleeding = new System.Windows.Forms.TextBox();
			this.textIndexCalculus = new System.Windows.Forms.TextBox();
			this.gridP = new OpenDental.ContrPerio();
			this.pd2 = new System.Drawing.Printing.PrintDocument();
			this.printDialog2 = new System.Windows.Forms.PrintDialog();
			this.printPreviewDlg = new System.Windows.Forms.PrintPreviewDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(885,658);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// but7
			// 
			this.but7.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but7.Autosize = true;
			this.but7.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but7.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but7.CornerRadius = 4F;
			this.but7.Location = new System.Drawing.Point(763,68);
			this.but7.Name = "but7";
			this.but7.Size = new System.Drawing.Size(32,32);
			this.but7.TabIndex = 3;
			this.but7.Text = "7";
			this.but7.Click += new System.EventHandler(this.but7_Click);
			// 
			// but3
			// 
			this.but3.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but3.Autosize = true;
			this.but3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but3.CornerRadius = 4F;
			this.but3.Location = new System.Drawing.Point(833,138);
			this.but3.Name = "but3";
			this.but3.Size = new System.Drawing.Size(32,32);
			this.but3.TabIndex = 4;
			this.but3.Text = "3";
			this.but3.Click += new System.EventHandler(this.but3_Click);
			// 
			// but2
			// 
			this.but2.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but2.Autosize = true;
			this.but2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but2.CornerRadius = 4F;
			this.but2.Location = new System.Drawing.Point(798,138);
			this.but2.Name = "but2";
			this.but2.Size = new System.Drawing.Size(32,32);
			this.but2.TabIndex = 5;
			this.but2.Text = "2";
			this.but2.Click += new System.EventHandler(this.but2_Click);
			// 
			// but1
			// 
			this.but1.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but1.Autosize = true;
			this.but1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but1.CornerRadius = 4F;
			this.but1.Location = new System.Drawing.Point(763,138);
			this.but1.Name = "but1";
			this.but1.Size = new System.Drawing.Size(32,32);
			this.but1.TabIndex = 6;
			this.but1.Text = "1";
			this.but1.Click += new System.EventHandler(this.but1_Click);
			// 
			// but6
			// 
			this.but6.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but6.Autosize = true;
			this.but6.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but6.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but6.CornerRadius = 4F;
			this.but6.Location = new System.Drawing.Point(833,103);
			this.but6.Name = "but6";
			this.but6.Size = new System.Drawing.Size(32,32);
			this.but6.TabIndex = 7;
			this.but6.Text = "6";
			this.but6.Click += new System.EventHandler(this.but6_Click);
			// 
			// but9
			// 
			this.but9.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but9.Autosize = true;
			this.but9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but9.CornerRadius = 4F;
			this.but9.Location = new System.Drawing.Point(833,68);
			this.but9.Name = "but9";
			this.but9.Size = new System.Drawing.Size(32,32);
			this.but9.TabIndex = 8;
			this.but9.Text = "9";
			this.but9.Click += new System.EventHandler(this.but9_Click);
			// 
			// but5
			// 
			this.but5.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but5.Autosize = true;
			this.but5.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but5.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but5.CornerRadius = 4F;
			this.but5.Location = new System.Drawing.Point(798,103);
			this.but5.Name = "but5";
			this.but5.Size = new System.Drawing.Size(32,32);
			this.but5.TabIndex = 9;
			this.but5.Text = "5";
			this.but5.Click += new System.EventHandler(this.but5_Click);
			// 
			// but4
			// 
			this.but4.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but4.Autosize = true;
			this.but4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but4.CornerRadius = 4F;
			this.but4.Location = new System.Drawing.Point(763,103);
			this.but4.Name = "but4";
			this.but4.Size = new System.Drawing.Size(32,32);
			this.but4.TabIndex = 10;
			this.but4.Text = "4";
			this.but4.Click += new System.EventHandler(this.but4_Click);
			// 
			// but8
			// 
			this.but8.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but8.Autosize = true;
			this.but8.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but8.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but8.CornerRadius = 4F;
			this.but8.Location = new System.Drawing.Point(798,68);
			this.but8.Name = "but8";
			this.but8.Size = new System.Drawing.Size(32,32);
			this.but8.TabIndex = 11;
			this.but8.Text = "8";
			this.but8.Click += new System.EventHandler(this.but8_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioRight);
			this.groupBox1.Controls.Add(this.radioLeft);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(765,4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(185,43);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Auto Advance";
			// 
			// radioRight
			// 
			this.radioRight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioRight.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioRight.Location = new System.Drawing.Point(10,20);
			this.radioRight.Name = "radioRight";
			this.radioRight.Size = new System.Drawing.Size(75,18);
			this.radioRight.TabIndex = 1;
			this.radioRight.Text = "Right";
			this.radioRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioRight.Click += new System.EventHandler(this.radioRight_Click);
			// 
			// radioLeft
			// 
			this.radioLeft.Checked = true;
			this.radioLeft.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioLeft.Location = new System.Drawing.Point(98,20);
			this.radioLeft.Name = "radioLeft";
			this.radioLeft.Size = new System.Drawing.Size(75,18);
			this.radioLeft.TabIndex = 0;
			this.radioLeft.TabStop = true;
			this.radioLeft.Text = "Left";
			this.radioLeft.Click += new System.EventHandler(this.radioLeft_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(7,228);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82,26);
			this.butDelete.TabIndex = 34;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label2.Location = new System.Drawing.Point(136,102);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(18,23);
			this.label2.TabIndex = 35;
			this.label2.Text = "F";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label3.Location = new System.Drawing.Point(136,562);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18,23);
			this.label3.TabIndex = 36;
			this.label3.Text = "F";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label4.Location = new System.Drawing.Point(136,428);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(18,23);
			this.label4.TabIndex = 37;
			this.label4.Text = "L";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif",10F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label5.Location = new System.Drawing.Point(136,222);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18,23);
			this.label5.TabIndex = 38;
			this.label5.Text = "L";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// but0
			// 
			this.but0.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but0.Autosize = true;
			this.but0.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but0.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but0.CornerRadius = 4F;
			this.but0.Location = new System.Drawing.Point(763,173);
			this.but0.Name = "but0";
			this.but0.Size = new System.Drawing.Size(67,32);
			this.but0.TabIndex = 39;
			this.but0.Text = "0";
			this.but0.Click += new System.EventHandler(this.but0_Click);
			// 
			// but10
			// 
			this.but10.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.but10.Autosize = true;
			this.but10.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.but10.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.but10.CornerRadius = 4F;
			this.but10.Location = new System.Drawing.Point(833,173);
			this.but10.Name = "but10";
			this.but10.Size = new System.Drawing.Size(32,32);
			this.but10.TabIndex = 40;
			this.but10.Text = "10";
			this.toolTip1.SetToolTip(this.but10,"Or hold down the Ctrl key");
			this.but10.Click += new System.EventHandler(this.but10_Click);
			// 
			// butBleed
			// 
			this.butBleed.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBleed.Autosize = true;
			this.butBleed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBleed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBleed.CornerRadius = 4F;
			this.butBleed.Location = new System.Drawing.Point(762,301);
			this.butBleed.Name = "butBleed";
			this.butBleed.Size = new System.Drawing.Size(88,26);
			this.butBleed.TabIndex = 41;
			this.butBleed.Text = "Bleeding";
			this.toolTip1.SetToolTip(this.butBleed,"Space bar or B on your keyboard");
			this.butBleed.Click += new System.EventHandler(this.butBleed_Click);
			// 
			// butPus
			// 
			this.butPus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPus.Autosize = true;
			this.butPus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPus.CornerRadius = 4F;
			this.butPus.Location = new System.Drawing.Point(762,331);
			this.butPus.Name = "butPus";
			this.butPus.Size = new System.Drawing.Size(88,26);
			this.butPus.TabIndex = 42;
			this.butPus.Text = "Suppuration";
			this.toolTip1.SetToolTip(this.butPus,"S on your keyboard");
			this.butPus.Click += new System.EventHandler(this.butPus_Click);
			// 
			// butColorBleed
			// 
			this.butColorBleed.BackColor = System.Drawing.Color.Red;
			this.butColorBleed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorBleed.Location = new System.Drawing.Point(850,302);
			this.butColorBleed.Name = "butColorBleed";
			this.butColorBleed.Size = new System.Drawing.Size(12,24);
			this.butColorBleed.TabIndex = 43;
			this.toolTip1.SetToolTip(this.butColorBleed,"Edit Color");
			this.butColorBleed.UseVisualStyleBackColor = false;
			this.butColorBleed.Click += new System.EventHandler(this.butColorBleed_Click);
			// 
			// butColorPus
			// 
			this.butColorPus.BackColor = System.Drawing.Color.Gold;
			this.butColorPus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorPus.Location = new System.Drawing.Point(850,332);
			this.butColorPus.Name = "butColorPus";
			this.butColorPus.Size = new System.Drawing.Size(12,24);
			this.butColorPus.TabIndex = 50;
			this.toolTip1.SetToolTip(this.butColorPus,"Edit Color");
			this.butColorPus.UseVisualStyleBackColor = false;
			this.butColorPus.Click += new System.EventHandler(this.butColorPus_Click);
			// 
			// butSkip
			// 
			this.butSkip.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSkip.Autosize = true;
			this.butSkip.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSkip.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSkip.CornerRadius = 4F;
			this.butSkip.Location = new System.Drawing.Point(764,609);
			this.butSkip.Name = "butSkip";
			this.butSkip.Size = new System.Drawing.Size(88,26);
			this.butSkip.TabIndex = 61;
			this.butSkip.Text = "SkipTeeth";
			this.toolTip1.SetToolTip(this.butSkip,"Toggle the selected teeth as skipped");
			this.butSkip.Click += new System.EventHandler(this.butSkip_Click);
			// 
			// butColorCalculus
			// 
			this.butColorCalculus.BackColor = System.Drawing.Color.Green;
			this.butColorCalculus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorCalculus.Location = new System.Drawing.Point(850,272);
			this.butColorCalculus.Name = "butColorCalculus";
			this.butColorCalculus.Size = new System.Drawing.Size(12,24);
			this.butColorCalculus.TabIndex = 67;
			this.toolTip1.SetToolTip(this.butColorCalculus,"Edit Color");
			this.butColorCalculus.UseVisualStyleBackColor = false;
			this.butColorCalculus.Click += new System.EventHandler(this.butColorCalculus_Click);
			// 
			// butColorPlaque
			// 
			this.butColorPlaque.BackColor = System.Drawing.Color.RoyalBlue;
			this.butColorPlaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorPlaque.Location = new System.Drawing.Point(850,242);
			this.butColorPlaque.Name = "butColorPlaque";
			this.butColorPlaque.Size = new System.Drawing.Size(12,24);
			this.butColorPlaque.TabIndex = 66;
			this.toolTip1.SetToolTip(this.butColorPlaque,"Edit Color");
			this.butColorPlaque.UseVisualStyleBackColor = false;
			this.butColorPlaque.Click += new System.EventHandler(this.butColorPlaque_Click);
			// 
			// butCalculus
			// 
			this.butCalculus.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCalculus.Autosize = true;
			this.butCalculus.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCalculus.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCalculus.CornerRadius = 4F;
			this.butCalculus.Location = new System.Drawing.Point(762,271);
			this.butCalculus.Name = "butCalculus";
			this.butCalculus.Size = new System.Drawing.Size(88,26);
			this.butCalculus.TabIndex = 65;
			this.butCalculus.Text = "Calculus";
			this.toolTip1.SetToolTip(this.butCalculus,"C on your keyboard");
			this.butCalculus.Click += new System.EventHandler(this.butCalculus_Click);
			// 
			// butPlaque
			// 
			this.butPlaque.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPlaque.Autosize = true;
			this.butPlaque.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPlaque.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPlaque.CornerRadius = 4F;
			this.butPlaque.Location = new System.Drawing.Point(762,241);
			this.butPlaque.Name = "butPlaque";
			this.butPlaque.Size = new System.Drawing.Size(88,26);
			this.butPlaque.TabIndex = 64;
			this.butPlaque.Text = "Plaque";
			this.toolTip1.SetToolTip(this.butPlaque,"P on your keyboard");
			this.butPlaque.Click += new System.EventHandler(this.butPlaque_Click);
			// 
			// butCalcIndex
			// 
			this.butCalcIndex.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCalcIndex.Autosize = true;
			this.butCalcIndex.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCalcIndex.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCalcIndex.CornerRadius = 4F;
			this.butCalcIndex.Location = new System.Drawing.Point(863,215);
			this.butCalcIndex.Name = "butCalcIndex";
			this.butCalcIndex.Size = new System.Drawing.Size(84,23);
			this.butCalcIndex.TabIndex = 74;
			this.butCalcIndex.Text = "Calc Index %";
			this.toolTip1.SetToolTip(this.butCalcIndex,"Calculate the Index for all four types");
			this.butCalcIndex.Click += new System.EventHandler(this.butCalcIndex_Click);
			// 
			// butCount
			// 
			this.butCount.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCount.Autosize = true;
			this.butCount.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCount.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCount.CornerRadius = 4F;
			this.butCount.Location = new System.Drawing.Point(92,18);
			this.butCount.Name = "butCount";
			this.butCount.Size = new System.Drawing.Size(84,23);
			this.butCount.TabIndex = 1;
			this.butCount.Text = "Count Teeth";
			this.toolTip1.SetToolTip(this.butCount,"Count all six types");
			this.butCount.Click += new System.EventHandler(this.butCount_Click);
			// 
			// checkThree
			// 
			this.checkThree.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkThree.Location = new System.Drawing.Point(765,49);
			this.checkThree.Name = "checkThree";
			this.checkThree.Size = new System.Drawing.Size(146,19);
			this.checkThree.TabIndex = 57;
			this.checkThree.Text = "Three at a time";
			this.toolTip1.SetToolTip(this.checkThree,"Enter numbers three at a time");
			this.checkThree.Click += new System.EventHandler(this.checkThree_Click);
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(885,609);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,26);
			this.butPrint.TabIndex = 62;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textCountMob);
			this.groupBox2.Controls.Add(this.updownMob);
			this.groupBox2.Controls.Add(this.textRedMob);
			this.groupBox2.Controls.Add(this.textCountFurc);
			this.groupBox2.Controls.Add(this.updownFurc);
			this.groupBox2.Controls.Add(this.textRedFurc);
			this.groupBox2.Controls.Add(this.textCountCAL);
			this.groupBox2.Controls.Add(this.updownCAL);
			this.groupBox2.Controls.Add(this.textRedCAL);
			this.groupBox2.Controls.Add(this.textCountGing);
			this.groupBox2.Controls.Add(this.updownGing);
			this.groupBox2.Controls.Add(this.textRedGing);
			this.groupBox2.Controls.Add(this.textCountMGJ);
			this.groupBox2.Controls.Add(this.updownMGJ);
			this.groupBox2.Controls.Add(this.textRedMGJ);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.textCountProb);
			this.groupBox2.Controls.Add(this.updownProb);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.textRedProb);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.butCount);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(764,378);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(196,201);
			this.groupBox2.TabIndex = 49;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Numbers in red";
			// 
			// textCountMob
			// 
			this.textCountMob.Location = new System.Drawing.Point(141,170);
			this.textCountMob.Name = "textCountMob";
			this.textCountMob.ReadOnly = true;
			this.textCountMob.Size = new System.Drawing.Size(34,20);
			this.textCountMob.TabIndex = 26;
			// 
			// updownMob
			// 
			this.updownMob.InterceptArrowKeys = false;
			this.updownMob.Location = new System.Drawing.Point(97,170);
			this.updownMob.Name = "updownMob";
			this.updownMob.Size = new System.Drawing.Size(19,20);
			this.updownMob.TabIndex = 25;
			this.updownMob.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// textRedMob
			// 
			this.textRedMob.Location = new System.Drawing.Point(70,170);
			this.textRedMob.Name = "textRedMob";
			this.textRedMob.ReadOnly = true;
			this.textRedMob.Size = new System.Drawing.Size(27,20);
			this.textRedMob.TabIndex = 24;
			// 
			// textCountFurc
			// 
			this.textCountFurc.Location = new System.Drawing.Point(141,150);
			this.textCountFurc.Name = "textCountFurc";
			this.textCountFurc.ReadOnly = true;
			this.textCountFurc.Size = new System.Drawing.Size(34,20);
			this.textCountFurc.TabIndex = 23;
			// 
			// updownFurc
			// 
			this.updownFurc.InterceptArrowKeys = false;
			this.updownFurc.Location = new System.Drawing.Point(97,150);
			this.updownFurc.Name = "updownFurc";
			this.updownFurc.Size = new System.Drawing.Size(19,20);
			this.updownFurc.TabIndex = 22;
			this.updownFurc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// textRedFurc
			// 
			this.textRedFurc.Location = new System.Drawing.Point(70,150);
			this.textRedFurc.Name = "textRedFurc";
			this.textRedFurc.ReadOnly = true;
			this.textRedFurc.Size = new System.Drawing.Size(27,20);
			this.textRedFurc.TabIndex = 21;
			// 
			// textCountCAL
			// 
			this.textCountCAL.Location = new System.Drawing.Point(141,130);
			this.textCountCAL.Name = "textCountCAL";
			this.textCountCAL.ReadOnly = true;
			this.textCountCAL.Size = new System.Drawing.Size(34,20);
			this.textCountCAL.TabIndex = 20;
			// 
			// updownCAL
			// 
			this.updownCAL.InterceptArrowKeys = false;
			this.updownCAL.Location = new System.Drawing.Point(97,130);
			this.updownCAL.Name = "updownCAL";
			this.updownCAL.Size = new System.Drawing.Size(19,20);
			this.updownCAL.TabIndex = 19;
			this.updownCAL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// textRedCAL
			// 
			this.textRedCAL.Location = new System.Drawing.Point(70,130);
			this.textRedCAL.Name = "textRedCAL";
			this.textRedCAL.ReadOnly = true;
			this.textRedCAL.Size = new System.Drawing.Size(27,20);
			this.textRedCAL.TabIndex = 18;
			// 
			// textCountGing
			// 
			this.textCountGing.Location = new System.Drawing.Point(141,110);
			this.textCountGing.Name = "textCountGing";
			this.textCountGing.ReadOnly = true;
			this.textCountGing.Size = new System.Drawing.Size(34,20);
			this.textCountGing.TabIndex = 17;
			// 
			// updownGing
			// 
			this.updownGing.InterceptArrowKeys = false;
			this.updownGing.Location = new System.Drawing.Point(97,110);
			this.updownGing.Name = "updownGing";
			this.updownGing.Size = new System.Drawing.Size(19,20);
			this.updownGing.TabIndex = 16;
			this.updownGing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// textRedGing
			// 
			this.textRedGing.Location = new System.Drawing.Point(70,110);
			this.textRedGing.Name = "textRedGing";
			this.textRedGing.ReadOnly = true;
			this.textRedGing.Size = new System.Drawing.Size(27,20);
			this.textRedGing.TabIndex = 15;
			// 
			// textCountMGJ
			// 
			this.textCountMGJ.Location = new System.Drawing.Point(141,90);
			this.textCountMGJ.Name = "textCountMGJ";
			this.textCountMGJ.ReadOnly = true;
			this.textCountMGJ.Size = new System.Drawing.Size(34,20);
			this.textCountMGJ.TabIndex = 14;
			// 
			// updownMGJ
			// 
			this.updownMGJ.InterceptArrowKeys = false;
			this.updownMGJ.Location = new System.Drawing.Point(97,90);
			this.updownMGJ.Name = "updownMGJ";
			this.updownMGJ.Size = new System.Drawing.Size(19,20);
			this.updownMGJ.TabIndex = 13;
			this.updownMGJ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// textRedMGJ
			// 
			this.textRedMGJ.Location = new System.Drawing.Point(70,90);
			this.textRedMGJ.Name = "textRedMGJ";
			this.textRedMGJ.ReadOnly = true;
			this.textRedMGJ.Size = new System.Drawing.Size(27,20);
			this.textRedMGJ.TabIndex = 12;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(125,49);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(52,16);
			this.label14.TabIndex = 11;
			this.label14.Text = "# Teeth";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCountProb
			// 
			this.textCountProb.Location = new System.Drawing.Point(141,70);
			this.textCountProb.Name = "textCountProb";
			this.textCountProb.ReadOnly = true;
			this.textCountProb.Size = new System.Drawing.Size(34,20);
			this.textCountProb.TabIndex = 10;
			// 
			// updownProb
			// 
			this.updownProb.InterceptArrowKeys = false;
			this.updownProb.Location = new System.Drawing.Point(97,70);
			this.updownProb.Name = "updownProb";
			this.updownProb.Size = new System.Drawing.Size(19,20);
			this.updownProb.TabIndex = 9;
			this.updownProb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updownRed_MouseDown);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(7,50);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(84,16);
			this.label13.TabIndex = 8;
			this.label13.Text = "Red if >=";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRedProb
			// 
			this.textRedProb.Location = new System.Drawing.Point(70,70);
			this.textRedProb.Name = "textRedProb";
			this.textRedProb.ReadOnly = true;
			this.textRedProb.Size = new System.Drawing.Size(27,20);
			this.textRedProb.TabIndex = 0;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6,152);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64,16);
			this.label12.TabIndex = 7;
			this.label12.Text = "Furc";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6,172);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64,16);
			this.label7.TabIndex = 6;
			this.label7.Text = "Mobility";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6,132);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(64,16);
			this.label11.TabIndex = 5;
			this.label11.Text = "CAL";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6,112);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64,16);
			this.label10.TabIndex = 4;
			this.label10.Text = "Ging Marg";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6,92);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64,16);
			this.label9.TabIndex = 3;
			this.label9.Text = "MGJ (<=)";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6,72);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64,16);
			this.label8.TabIndex = 2;
			this.label8.Text = "Probing";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5,13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112,19);
			this.label1.TabIndex = 51;
			this.label1.Text = "Exams";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(7,197);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82,26);
			this.butAdd.TabIndex = 53;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(757,641);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(123,42);
			this.label6.TabIndex = 54;
			this.label6.Text = "(All exams are saved automatically)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// listExams
			// 
			this.listExams.ItemHeight = 14;
			this.listExams.Location = new System.Drawing.Point(7,37);
			this.listExams.Name = "listExams";
			this.listExams.Size = new System.Drawing.Size(124,130);
			this.listExams.TabIndex = 59;
			this.listExams.DoubleClick += new System.EventHandler(this.listExams_DoubleClick);
			this.listExams.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listExams_MouseDown);
			// 
			// textIndexPlaque
			// 
			this.textIndexPlaque.Location = new System.Drawing.Point(868,245);
			this.textIndexPlaque.Name = "textIndexPlaque";
			this.textIndexPlaque.ReadOnly = true;
			this.textIndexPlaque.Size = new System.Drawing.Size(38,20);
			this.textIndexPlaque.TabIndex = 70;
			this.textIndexPlaque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textIndexSupp
			// 
			this.textIndexSupp.Location = new System.Drawing.Point(868,335);
			this.textIndexSupp.Name = "textIndexSupp";
			this.textIndexSupp.ReadOnly = true;
			this.textIndexSupp.Size = new System.Drawing.Size(38,20);
			this.textIndexSupp.TabIndex = 71;
			this.textIndexSupp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textIndexBleeding
			// 
			this.textIndexBleeding.Location = new System.Drawing.Point(868,305);
			this.textIndexBleeding.Name = "textIndexBleeding";
			this.textIndexBleeding.ReadOnly = true;
			this.textIndexBleeding.Size = new System.Drawing.Size(38,20);
			this.textIndexBleeding.TabIndex = 72;
			this.textIndexBleeding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textIndexCalculus
			// 
			this.textIndexCalculus.Location = new System.Drawing.Point(868,275);
			this.textIndexCalculus.Name = "textIndexCalculus";
			this.textIndexCalculus.ReadOnly = true;
			this.textIndexCalculus.Size = new System.Drawing.Size(38,20);
			this.textIndexCalculus.TabIndex = 73;
			this.textIndexCalculus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// gridP
			// 
			this.gridP.BackColor = System.Drawing.SystemColors.Window;
			this.gridP.Location = new System.Drawing.Point(157,11);
			this.gridP.Name = "gridP";
			this.gridP.SelectedExam = 0;
			this.gridP.Size = new System.Drawing.Size(595,665);
			this.gridP.TabIndex = 75;
			this.gridP.Text = "contrPerio2";
			this.gridP.DirectionChangedLeft += new System.EventHandler(this.gridP_DirectionChangedLeft);
			this.gridP.DirectionChangedRight += new System.EventHandler(this.gridP_DirectionChangedRight);
			// 
			// printPreviewDlg
			// 
			this.printPreviewDlg.AutoScrollMargin = new System.Drawing.Size(0,0);
			this.printPreviewDlg.AutoScrollMinSize = new System.Drawing.Size(0,0);
			this.printPreviewDlg.ClientSize = new System.Drawing.Size(400,300);
			this.printPreviewDlg.Enabled = true;
			this.printPreviewDlg.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDlg.Icon")));
			this.printPreviewDlg.Name = "printPreviewDlg";
			this.printPreviewDlg.Visible = false;
			// 
			// FormPerio
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(982,700);
			this.Controls.Add(this.gridP);
			this.Controls.Add(this.butCalcIndex);
			this.Controls.Add(this.textIndexCalculus);
			this.Controls.Add(this.textIndexBleeding);
			this.Controls.Add(this.textIndexSupp);
			this.Controls.Add(this.textIndexPlaque);
			this.Controls.Add(this.butColorCalculus);
			this.Controls.Add(this.butColorPlaque);
			this.Controls.Add(this.butCalculus);
			this.Controls.Add(this.butPlaque);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butSkip);
			this.Controls.Add(this.listExams);
			this.Controls.Add(this.checkThree);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butColorPus);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butColorBleed);
			this.Controls.Add(this.butPus);
			this.Controls.Add(this.butBleed);
			this.Controls.Add(this.but10);
			this.Controls.Add(this.but0);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.but8);
			this.Controls.Add(this.but4);
			this.Controls.Add(this.but5);
			this.Controls.Add(this.but9);
			this.Controls.Add(this.but6);
			this.Controls.Add(this.but1);
			this.Controls.Add(this.but2);
			this.Controls.Add(this.but3);
			this.Controls.Add(this.but7);
			this.Controls.Add(this.butClose);
			this.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPerio";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Perio Chart";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPerio_Closing);
			this.Load += new System.EventHandler(this.FormPerio_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPerio_Load(object sender, System.EventArgs e) {
			butColorBleed.BackColor=DefB.Short[(int)DefCat.MiscColors][1].ItemColor;
			butColorPus.BackColor=DefB.Short[(int)DefCat.MiscColors][2].ItemColor;
			butColorPlaque.BackColor=DefB.Short[(int)DefCat.MiscColors][4].ItemColor;
			butColorCalculus.BackColor=DefB.Short[(int)DefCat.MiscColors][5].ItemColor;
			textRedProb.Text=((Pref)PrefB.HList["PerioRedProb"]).ValueString;
			textRedMGJ.Text =((Pref)PrefB.HList["PerioRedMGJ"] ).ValueString;
			textRedGing.Text=((Pref)PrefB.HList["PerioRedGing"]).ValueString;
			textRedCAL.Text =((Pref)PrefB.HList["PerioRedCAL"] ).ValueString;
			textRedFurc.Text=((Pref)PrefB.HList["PerioRedFurc"]).ValueString;
			textRedMob.Text =((Pref)PrefB.HList["PerioRedMob"] ).ValueString;
			//Procedure[] procList=Procedures.Refresh(PatCur.PatNum);
			ToothInitial[] initialList=ToothInitials.Refresh(PatCur.PatNum);
			MissingTeeth=ToothInitials.GetMissingOrHiddenTeeth(initialList);
			RefreshListExams();
			listExams.SelectedIndex=PerioExams.List.Length-1;//this works even if no items.
			FillGrid();
		}

		///<summary>After this method runs, the selected index is usually set.</summary>
		private void RefreshListExams(){
			//most recent date at the bottom
			PerioExams.Refresh(PatCur.PatNum);
			PerioMeasures.Refresh(PatCur.PatNum);
			listExams.Items.Clear();
			for(int i=0;i<PerioExams.List.Length;i++){
				listExams.Items.Add(PerioExams.List[i].ExamDate.ToShortDateString()+"   "
					+Providers.GetAbbr(PerioExams.List[i].ProvNum));
			}
		}

		///<summary>Usually set the selected index first</summary>
		private void FillGrid(){
			if(listExams.SelectedIndex!=-1){
				PerioExamCur=PerioExams.List[listExams.SelectedIndex];
			}
			gridP.SelectedExam=listExams.SelectedIndex;
			gridP.LoadData();
			FillIndexes();
			FillCounts();
			gridP.Invalidate();
			gridP.Focus();//this still doesn't seem to work to enable first arrow click to move
		}

		private void listExams_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(listExams.SelectedIndex==gridP.SelectedExam)
				return;
			//Only continues if clicked on other than current exam
			gridP.SaveCurExam(PerioExamCur.PerioExamNum);
			//no need to RefreshListExams because it has not changed
			PerioExams.Refresh(PatCur.PatNum);//refresh instead
			PerioMeasures.Refresh(PatCur.PatNum);
			FillGrid();
		}

		private void listExams_DoubleClick(object sender, System.EventArgs e) {
			//remember that the first click may not have triggered the mouse down routine
			//and the second click will never trigger it.
			if(listExams.SelectedIndex==-1)
				return;
			//a PerioExam.Cur will always have been set through mousedown(or similar),then FillGrid
			gridP.SaveCurExam(PerioExamCur.PerioExamNum);
			PerioExams.Refresh(PatCur.PatNum);//list will not change
			PerioMeasures.Refresh(PatCur.PatNum);
			FormPerioEdit FormPE=new FormPerioEdit();
			FormPE.PerioExamCur=PerioExamCur;
			FormPE.ShowDialog();
			int curIndex=listExams.SelectedIndex;
			RefreshListExams();
			listExams.SelectedIndex=curIndex;
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			if(listExams.SelectedIndex!=-1){
				gridP.SaveCurExam(PerioExamCur.PerioExamNum);
			}
			PerioExamCur=new PerioExam();
			PerioExamCur.PatNum=PatCur.PatNum;
			PerioExamCur.ExamDate=DateTime.Today;
			PerioExamCur.ProvNum=PatCur.PriProv;
			PerioExams.Insert(PerioExamCur);
			ArrayList skippedTeeth=new ArrayList();//int 1-32
			if(PerioExams.List.Length==0){
				for(int i=0;i<MissingTeeth.Count;i++){
					if(((string)MissingTeeth[i]).CompareTo("A")<0//if a number
						|| ((string)MissingTeeth[i]).CompareTo("Z")>0)
					{
						skippedTeeth.Add(PIn.PInt((string)MissingTeeth[i]));
					}
				}
			}
			else{
				//set skipped teeth based on the last exam in the list: 
				skippedTeeth=PerioMeasures.GetSkipped
					(PerioExams.List[PerioExams.List.Length-1].PerioExamNum);
			}
			PerioMeasures.SetSkipped(PerioExamCur.PerioExamNum,skippedTeeth);
			RefreshListExams();
			listExams.SelectedIndex=PerioExams.List.Length-1;
			FillGrid();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listExams.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Exam?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			int curselected=listExams.SelectedIndex;
			PerioExams.Delete(PerioExamCur);
			RefreshListExams();
			if(curselected < listExams.Items.Count)
				listExams.SelectedIndex=curselected;
			else
				listExams.SelectedIndex=PerioExams.List.Length-1;
			FillGrid();
		}
		
		private void radioRight_Click(object sender, System.EventArgs e) {
			gridP.DirectionIsRight=true;
			gridP.Focus();
		}

		private void radioLeft_Click(object sender, System.EventArgs e) {
			gridP.DirectionIsRight=false;
			gridP.Focus();
		}

		private void gridP_DirectionChangedRight(object sender, System.EventArgs e) {
			radioRight.Checked=true;
		}

		private void gridP_DirectionChangedLeft(object sender, System.EventArgs e) {
			radioLeft.Checked=true;
		}

		private void checkThree_Click(object sender, System.EventArgs e) {
			gridP.ThreeAtATime=checkThree.Checked;
			gridP.Focus();
		}

		private void but0_Click(object sender, System.EventArgs e) {
			NumberClicked(0);
		}

		private void but1_Click(object sender, System.EventArgs e) {
			NumberClicked(1);
		}

		private void but2_Click(object sender, System.EventArgs e) {
			NumberClicked(2);
		}

		private void but3_Click(object sender, System.EventArgs e) {
			NumberClicked(3);
		}

		private void but4_Click(object sender, System.EventArgs e) {
			NumberClicked(4);
		}

		private void but5_Click(object sender, System.EventArgs e) {
			NumberClicked(5);
		}

		private void but6_Click(object sender, System.EventArgs e) {
			NumberClicked(6);
		}

		private void but7_Click(object sender, System.EventArgs e) {
			NumberClicked(7);
		}

		private void but8_Click(object sender, System.EventArgs e) {
			NumberClicked(8);
		}

		private void but9_Click(object sender, System.EventArgs e) {
			NumberClicked(9);
		}

		///<summary>The only valid numbers are 0 through 9</summary>
		private void NumberClicked(int number){
			if(gridP.SelectedExam==-1){
				MessageBox.Show(Lan.g(this,"Please add or select an exam first in the list to the left."));
				return;
			}
			if(TenIsDown){
				gridP.ButtonPressed(10+number);
			}
			else{
				gridP.ButtonPressed(number);
			}
			TenIsDown=false;
			gridP.Focus();
		}

		private void but10_Click(object sender, System.EventArgs e) {
			TenIsDown=true;
		}

		private void butCalcIndex_Click(object sender, System.EventArgs e) {
			FillIndexes();
		}

		private void FillIndexes(){
			textIndexPlaque.Text=gridP.ComputeIndex(BleedingFlags.Plaque);
			textIndexCalculus.Text=gridP.ComputeIndex(BleedingFlags.Calculus);
			textIndexBleeding.Text=gridP.ComputeIndex(BleedingFlags.Blood);
			textIndexSupp.Text=gridP.ComputeIndex(BleedingFlags.Suppuration);
		}

		private void butBleed_Click(object sender, System.EventArgs e) {
			TenIsDown=false;
			gridP.ButtonPressed("b");
			gridP.Focus();
		}

		private void butPus_Click(object sender, System.EventArgs e) {
			TenIsDown=false;
			gridP.ButtonPressed("s");
			gridP.Focus();
		}

		private void butPlaque_Click(object sender, System.EventArgs e) {
			TenIsDown=false;
			gridP.ButtonPressed("p");
			gridP.Focus();
		}

		private void butCalculus_Click(object sender, System.EventArgs e) {
			TenIsDown=false;
			gridP.ButtonPressed("c");
			gridP.Focus();
		}

		private void butColorBleed_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColorBleed.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			butColorBleed.BackColor=colorDialog1.Color;
			Def DefCur=DefB.Short[(int)DefCat.MiscColors][1].Copy();
			DefCur.ItemColor=colorDialog1.Color;
			Defs.Update(DefCur);
			Defs.Refresh();
			localDefsChanged=true;
			gridP.SetColors();
			gridP.Invalidate();
			gridP.Focus();
		}

		private void butColorPus_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColorPus.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			butColorPus.BackColor=colorDialog1.Color;
			Def DefCur=DefB.Short[(int)DefCat.MiscColors][2].Copy();
			DefCur.ItemColor=colorDialog1.Color;
			Defs.Update(DefCur);
			Defs.Refresh();
			localDefsChanged=true;
			gridP.SetColors();
			gridP.Invalidate();
			gridP.Focus();
		}

		private void butColorPlaque_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColorPlaque.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			butColorPlaque.BackColor=colorDialog1.Color;
			Def DefCur=DefB.Short[(int)DefCat.MiscColors][4].Copy();
			DefCur.ItemColor=colorDialog1.Color;
			Defs.Update(DefCur);
			Defs.Refresh();
			localDefsChanged=true;
			gridP.SetColors();
			gridP.Invalidate();
			gridP.Focus();
		}

		private void butColorCalculus_Click(object sender, System.EventArgs e) {
			colorDialog1.Color=butColorCalculus.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK){
				return;
			}
			butColorCalculus.BackColor=colorDialog1.Color;
			Def DefCur=DefB.Short[(int)DefCat.MiscColors][5].Copy();
			DefCur.ItemColor=colorDialog1.Color;
			Defs.Update(DefCur);
			Defs.Refresh();
			localDefsChanged=true;
			gridP.SetColors();
			gridP.Invalidate();
			gridP.Focus();
		}

		private void butSkip_Click(object sender, System.EventArgs e) {
			gridP.ToggleSkip(PerioExamCur.PerioExamNum);
		}

		private void updownRed_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//this is necessary because Microsoft's updown control is too buggy to be useful
			Cursor=Cursors.WaitCursor;
			Pref pref=null;
			if(sender==updownProb){
				//textRedProb.Cursor=Cursors.WaitCursor;
				pref=(Pref)PrefB.HList["PerioRedProb"];//erioRedNumber
			}
			else if(sender==updownMGJ){
				pref=(Pref)PrefB.HList["PerioRedMGJ"];
			}
			else if(sender==updownGing){
				pref=(Pref)PrefB.HList["PerioRedGing"];
			}
			else if(sender==updownCAL){
				pref=(Pref)PrefB.HList["PerioRedCAL"];
			}
			else if(sender==updownFurc){
				pref=(Pref)PrefB.HList["PerioRedFurc"];
			}
			else if(sender==updownMob){
				pref=(Pref)PrefB.HList["PerioRedMob"];
			}
			int currentValue=PIn.PInt(pref.ValueString);
			if(e.Y<8){//up
				currentValue++;
			}
			else{//down
				if(currentValue==0){
					Cursor=Cursors.Default;
					return;
				}
				currentValue--;
			}
			pref.ValueString=currentValue.ToString();
			Prefs.Update(pref);
			localDefsChanged=true;
			Prefs.Refresh();
			if(sender==updownProb){
				textRedProb.Text=currentValue.ToString();
				textCountProb.Text=gridP.CountTeeth(PerioSequenceType.Probing).Count.ToString();
			}
			else if(sender==updownMGJ){
				textRedMGJ.Text=currentValue.ToString();
				textCountMGJ.Text=gridP.CountTeeth(PerioSequenceType.MGJ).Count.ToString();
			}
			else if(sender==updownGing){
				textRedGing.Text=currentValue.ToString();
				textCountGing.Text=gridP.CountTeeth(PerioSequenceType.GingMargin).Count.ToString();
			}
			else if(sender==updownCAL){
				textRedCAL.Text=currentValue.ToString();
				textCountCAL.Text=gridP.CountTeeth(PerioSequenceType.CAL).Count.ToString();
			}
			else if(sender==updownFurc){
				textRedFurc.Text=currentValue.ToString();
				textCountFurc.Text=gridP.CountTeeth(PerioSequenceType.Furcation).Count.ToString();
			}
			else if(sender==updownMob){
				textRedMob.Text=currentValue.ToString();
				textCountMob.Text=gridP.CountTeeth(PerioSequenceType.Mobility).Count.ToString();
			}
			gridP.Invalidate();
			Cursor=Cursors.Default;
			gridP.Focus();
		}

		private void butCount_Click(object sender, System.EventArgs e) {
			FillCounts();
			gridP.Focus();
		}

		private void FillCounts(){
			textCountProb.Text=gridP.CountTeeth(PerioSequenceType.Probing).Count.ToString();
			textCountMGJ.Text=gridP.CountTeeth(PerioSequenceType.MGJ).Count.ToString();
			textCountGing.Text=gridP.CountTeeth(PerioSequenceType.GingMargin).Count.ToString();
			textCountCAL.Text=gridP.CountTeeth(PerioSequenceType.CAL).Count.ToString();
			textCountFurc.Text=gridP.CountTeeth(PerioSequenceType.Furcation).Count.ToString();
			textCountMob.Text=gridP.CountTeeth(PerioSequenceType.Mobility).Count.ToString();
		}

		private void butPrint_Click(object sender, System.EventArgs e) {
			//pagesPrinted=0;
			pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			if(!Printers.SetPrinter(pd2,PrintSituation.TPPerio)){
				return;
			}
			/*
			printDialog2=new PrintDialog();
			printDialog2.PrinterSettings=new PrinterSettings();
			printDialog2.PrinterSettings.PrinterName=Computers.Cur.PrinterName;
			if(printDialog2.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(printDialog2.PrinterSettings.IsValid){
				pd2.PrinterSettings=printDialog2.PrinterSettings;
			}
			//uses default printer if selected printer not valid
			*/
			try{
				pd2.Print();
			}
			catch{
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
			gridP.Focus();
		}

		private void pd2_PrintPage(object sender, PrintPageEventArgs ev){//raised for each page to be printed.
			Graphics grfx=ev.Graphics;
			//MessageBox.Show(grfx.
			float yPos=75;
			float xPos=100;
			Font font=new Font("Arial",9);
			StringFormat format=new StringFormat();
			format.Alignment=StringAlignment.Center;
			grfx.DrawString("Periodontal Charting",new Font("Arial",15),Brushes.Black
				,new RectangleF(xPos,yPos,650,25),format);
			yPos+=30;
			grfx.DrawString(PatCur.GetNameFL(),font,Brushes.Black
				,new RectangleF(xPos,yPos,650,25),format);
			yPos+=25;
			grfx.TranslateTransform(xPos,yPos);
			gridP.DrawChart(grfx);
			grfx.TranslateTransform(-xPos,-yPos);
			yPos+=680;
			grfx.FillEllipse(new SolidBrush(butColorPlaque.BackColor),xPos,yPos+3,8,8);
			grfx.DrawString("Plaque Index: "+gridP.ComputeIndex(BleedingFlags.Plaque)+" %"
				,font,Brushes.Black,xPos+12,yPos);
			yPos+=20;
			grfx.FillEllipse(new SolidBrush(butColorCalculus.BackColor),xPos,yPos+3,8,8);
			grfx.DrawString("Calculus Index: "+gridP.ComputeIndex(BleedingFlags.Calculus)+" %"
				,font,Brushes.Black,xPos+12,yPos);
			yPos+=20;
			grfx.FillEllipse(new SolidBrush(butColorBleed.BackColor),xPos,yPos+3,8,8);
			grfx.DrawString("Bleeding Index: "+gridP.ComputeIndex(BleedingFlags.Blood)+" %"
				,font,Brushes.Black,xPos+12,yPos);
			yPos+=20;
			grfx.FillEllipse(new SolidBrush(butColorPus.BackColor),xPos,yPos+3,8,8);
			grfx.DrawString("Suppuration Index: "+gridP.ComputeIndex(BleedingFlags.Suppuration)+" %"
				,font,Brushes.Black,xPos+12,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with Probing greater than or equal to "+textRedProb.Text+" mm: "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.Probing))
				,font,Brushes.Black,xPos,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with MGJ less than or equal to "+textRedMGJ.Text+" mm: "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.MGJ))
				,font,Brushes.Black,xPos,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with Gingival Margin greater than or equal to "+textRedGing.Text+" mm: "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.GingMargin))
				,font,Brushes.Black,xPos,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with CAL greater than or equal to "+textRedCAL.Text+" mm: "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.CAL))
				,font,Brushes.Black,xPos,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with Furcations greater than or equal to class "+textRedFurc.Text+": "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.Furcation))
				,font,Brushes.Black,xPos,yPos);
			yPos+=20;
			grfx.DrawString("Teeth with Mobility greater than or equal to "+textRedMob.Text+": "
				+ConvertALtoString(gridP.CountTeeth(PerioSequenceType.Mobility))
				,font,Brushes.Black,xPos,yPos);
			//pagesPrinted++;
			ev.HasMorePages=false;
			grfx.Dispose();
		}

		private string ConvertALtoString(ArrayList ALteeth){
			if(ALteeth.Count==0){
				return "none";
			}
			string retVal="";
			for(int i=0;i<ALteeth.Count;i++){
				if(i>0)
					retVal+=",";
				retVal+=ALteeth[i];
			}
			return retVal;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormPerio_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(localDefsChanged){
				DataValid.SetInvalid(InvalidTypes.Defs | InvalidTypes.Prefs);
			}
			if(listExams.SelectedIndex!=-1){
				gridP.SaveCurExam(PerioExamCur.PerioExamNum);
			}
		}

		

		

		

		

		
		

		

		

		

		

		

		

		

		

		

		

		

		

		

		


	}
}





















