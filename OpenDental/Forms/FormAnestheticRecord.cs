using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public class FormAnestheticRecord : System.Windows.Forms.Form{
		private ListBox listAnesthetic;
		private OpenDental.UI.Button button2;
		private Label label2;
		private OpenDental.UI.Button button3;
		private OpenDental.UI.Button button4;
		private OpenDental.UI.Button button5;
		private OpenDental.UI.Button button6;
		private OpenDental.UI.Button button7;
		private OpenDental.UI.Button button8;
		private OpenDental.UI.Button button9;
		private OpenDental.UI.Button button10;
		private OpenDental.UI.Button button11;
		private OpenDental.UI.Button button12;
		private OpenDental.UI.Button button13;
		private OpenDental.UI.Button button14;
		private OpenDental.UI.Button button15;
		private OpenDental.UI.Button button16;
		private OpenDental.UI.Button button17;
		private ListBox listAnestheticMeds;
		private OpenDental.UI.Button button18;
		private OpenDental.UI.Button button19;
		private Label label4;
		private ComboBox comboBox1;
		private Label label6;
		private Label label7;
		private OpenDental.UI.Button butAnesthOpen;
		private OpenDental.UI.Button butSurgOpen;
		private OpenDental.UI.Button butSurgClose;
		private OpenDental.UI.Button butAnesthClose;
		private OpenDental.UI.Button button25;
		private ComboBox comboBox2;
		private ComboBox comboBox3;
		private Label label9;
		private ComboBox comboBox4;
		private Label label10;
		private TextBox textAnesthOpen;
		private TextBox textSurgOpen;
		private TextBox textSurgClose;
		private TextBox textAnesthClose;
		private OpenDental.UI.Button button26;
		private RichTextBox richTextBox1;
		private TextBox textBox6;
		private Label label22;
		private ComboBox comboBox11;
		private Label label23;
		private Label label24;
		private TextBox textBox7;
		private Label label25;
		private Label label26;
		private TextBox textBox8;
		private Label label27;
		private TextBox textBox9;
		private OpenDental.UI.SignatureBox sigBox;
		private OpenDental.UI.Button butClearSig;
		private Label label28;
		private GroupBox groupBox1;
		private Label label12;
		private OpenDental.UI.Button button20;
		private ComboBox comboBox6;
		private Label label20;
		private TextBox textBox5;
		private Label label19;
		private ComboBox comboBox10;
		private Label label18;
		private ComboBox comboBox9;
		private Label label17;
		private ComboBox comboBox8;
		private Label label16;
		private Label label15;
		private Label label14;
		private Label label13;
		private ComboBox comboBox7;
		private CheckBox checkBox2;
		private CheckBox CheckBox1;
		private ComboBox comboBox5;
		private Label label11;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private GroupBox groupBox4;
		private TextBox textBox11;
		private DataGridView dataGridView1;
		private TextBox textBox10;
		private GroupBox groupBox5;
		private OpenDental.UI.Button button27;
		private OpenDental.UI.Button button1;
		private Patient PatCur;
		private ComboBox comboBox12;
		private Label label8;
		private GroupBox groupBox6;
		private RadioButton radioButton2;
		private RadioButton radioButton1;
		private ComboBox comboBox13;
		private Label label3;
		private Label label5;
		private TextBox textBox1;
		private RadioButton radioButton7;
		private RadioButton radioButton6;
		private RadioButton radioButton5;
		private RadioButton radioButton4;
		private RadioButton radioButton3;
		private Label label1;
    
		public FormAnestheticRecord(Patient patCur)
		{			
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();
			PatCur=patCur;
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnestheticRecord));
			this.listAnesthetic = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.listAnestheticMeds = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textAnesthOpen = new System.Windows.Forms.TextBox();
			this.textSurgOpen = new System.Windows.Forms.TextBox();
			this.textSurgClose = new System.Windows.Forms.TextBox();
			this.textAnesthClose = new System.Windows.Forms.TextBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.comboBox11 = new System.Windows.Forms.ComboBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox12 = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.comboBox10 = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.comboBox9 = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.comboBox8 = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.comboBox7 = new System.Windows.Forms.ComboBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.CheckBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox5 = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox13 = new System.Windows.Forms.ComboBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBox11 = new System.Windows.Forms.TextBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.textBox10 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.button20 = new OpenDental.UI.Button();
			this.button13 = new OpenDental.UI.Button();
			this.button3 = new OpenDental.UI.Button();
			this.button4 = new OpenDental.UI.Button();
			this.button5 = new OpenDental.UI.Button();
			this.button6 = new OpenDental.UI.Button();
			this.button7 = new OpenDental.UI.Button();
			this.button8 = new OpenDental.UI.Button();
			this.button9 = new OpenDental.UI.Button();
			this.button10 = new OpenDental.UI.Button();
			this.button11 = new OpenDental.UI.Button();
			this.button12 = new OpenDental.UI.Button();
			this.button14 = new OpenDental.UI.Button();
			this.button15 = new OpenDental.UI.Button();
			this.button16 = new OpenDental.UI.Button();
			this.button17 = new OpenDental.UI.Button();
			this.button25 = new OpenDental.UI.Button();
			this.button1 = new OpenDental.UI.Button();
			this.button2 = new OpenDental.UI.Button();
			this.butAnesthOpen = new OpenDental.UI.Button();
			this.butSurgOpen = new OpenDental.UI.Button();
			this.butAnesthClose = new OpenDental.UI.Button();
			this.button19 = new OpenDental.UI.Button();
			this.button18 = new OpenDental.UI.Button();
			this.butSurgClose = new OpenDental.UI.Button();
			this.button27 = new OpenDental.UI.Button();
			this.button26 = new OpenDental.UI.Button();
			this.butClearSig = new OpenDental.UI.Button();
			this.sigBox = new OpenDental.UI.SignatureBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.radioButton6 = new System.Windows.Forms.RadioButton();
			this.radioButton7 = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// listAnesthetic
			// 
			this.listAnesthetic.FormattingEnabled = true;
			this.listAnesthetic.Location = new System.Drawing.Point(15, 21);
			this.listAnesthetic.Name = "listAnesthetic";
			this.listAnesthetic.Size = new System.Drawing.Size(120, 121);
			this.listAnesthetic.TabIndex = 0;
			this.listAnesthetic.SelectedIndexChanged += new System.EventHandler(this.listAnesthetic_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(157, 171);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 55;
			this.label2.Text = "Anesthetic";
			// 
			// listAnestheticMeds
			// 
			this.listAnestheticMeds.FormattingEnabled = true;
			this.listAnestheticMeds.Location = new System.Drawing.Point(148, 204);
			this.listAnestheticMeds.Name = "listAnestheticMeds";
			this.listAnestheticMeds.Size = new System.Drawing.Size(204, 82);
			this.listAnestheticMeds.TabIndex = 72;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(355, 349);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(0, 13);
			this.label4.TabIndex = 76;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(159, 189);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(139, 21);
			this.comboBox1.TabIndex = 77;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(72, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(92, 13);
			this.label6.TabIndex = 80;
			this.label6.Text = "Vital Sign Monitor:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(295, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(46, 13);
			this.label7.TabIndex = 81;
			this.label7.Text = "Serial #:";
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(158, 139);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(100, 21);
			this.comboBox2.TabIndex = 87;
			// 
			// comboBox3
			// 
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(372, 127);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(100, 21);
			this.comboBox3.TabIndex = 89;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(369, 110);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(49, 13);
			this.label9.TabIndex = 90;
			this.label9.Text = "Assistant";
			// 
			// comboBox4
			// 
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Location = new System.Drawing.Point(478, 127);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(100, 21);
			this.comboBox4.TabIndex = 91;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(475, 110);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(51, 13);
			this.label10.TabIndex = 92;
			this.label10.Text = "Circulator";
			// 
			// textAnesthOpen
			// 
			this.textAnesthOpen.Location = new System.Drawing.Point(147, 67);
			this.textAnesthOpen.Name = "textAnesthOpen";
			this.textAnesthOpen.Size = new System.Drawing.Size(97, 20);
			this.textAnesthOpen.TabIndex = 93;
			this.textAnesthOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textAnesthOpen.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textSurgOpen
			// 
			this.textSurgOpen.Location = new System.Drawing.Point(249, 67);
			this.textSurgOpen.Name = "textSurgOpen";
			this.textSurgOpen.Size = new System.Drawing.Size(97, 20);
			this.textSurgOpen.TabIndex = 94;
			this.textSurgOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textSurgOpen.TextChanged += new System.EventHandler(this.textSurgOpen_TextChanged);
			// 
			// textSurgClose
			// 
			this.textSurgClose.Location = new System.Drawing.Point(236, 51);
			this.textSurgClose.Name = "textSurgClose";
			this.textSurgClose.Size = new System.Drawing.Size(97, 20);
			this.textSurgClose.TabIndex = 95;
			this.textSurgClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textSurgClose.TextChanged += new System.EventHandler(this.textSurgClose_TextChanged_1);
			// 
			// textAnesthClose
			// 
			this.textAnesthClose.Location = new System.Drawing.Point(339, 51);
			this.textAnesthClose.Name = "textAnesthClose";
			this.textAnesthClose.Size = new System.Drawing.Size(97, 20);
			this.textAnesthClose.TabIndex = 96;
			this.textAnesthClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textAnesthClose.TextChanged += new System.EventHandler(this.textAnesthClose_TextChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(26, 557);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(229, 133);
			this.richTextBox1.TabIndex = 103;
			this.richTextBox1.Text = "";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(346, 102);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(88, 20);
			this.textBox6.TabIndex = 122;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(240, 661);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(0, 13);
			this.label22.TabIndex = 123;
			// 
			// comboBox11
			// 
			this.comboBox11.FormattingEnabled = true;
			this.comboBox11.Items.AddRange(new object[] {
            "12 MN",
            "1 AM",
            "2 AM",
            "3 AM",
            "4 AM",
            "5 AM",
            "6 AM",
            "7 AM",
            "8 AM",
            "9 AM",
            "10 AM",
            "11 AM",
            "12 PM",
            "1 PM",
            "2 PM",
            "3 PM",
            "4 PM",
            "5 PM",
            "6 PM",
            "7 PM",
            "8 PM",
            "9 PM",
            "10 PM",
            "11 PM"});
			this.comboBox11.Location = new System.Drawing.Point(346, 74);
			this.comboBox11.Name = "comboBox11";
			this.comboBox11.Size = new System.Drawing.Size(54, 21);
			this.comboBox11.TabIndex = 124;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(281, 77);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(58, 13);
			this.label23.TabIndex = 125;
			this.label23.Text = "NPO since";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(273, 105);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(66, 13);
			this.label24.TabIndex = 126;
			this.label24.Text = "Escort name";
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(346, 128);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(88, 20);
			this.textBox7.TabIndex = 127;
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(274, 131);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(65, 13);
			this.label25.TabIndex = 128;
			this.label25.Text = "Relationship";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(301, 26);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(38, 13);
			this.label26.TabIndex = 130;
			this.label26.Text = "Height";
			this.label26.Click += new System.EventHandler(this.label26_Click);
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(346, 23);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(88, 20);
			this.textBox8.TabIndex = 129;
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(298, 47);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(41, 13);
			this.label27.TabIndex = 132;
			this.label27.Text = "Weight";
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(346, 47);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(88, 20);
			this.textBox9.TabIndex = 131;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(460, 16);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(110, 23);
			this.label28.TabIndex = 133;
			this.label28.Text = "Signature / Initials";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton7);
			this.groupBox1.Controls.Add(this.radioButton6);
			this.groupBox1.Controls.Add(this.radioButton5);
			this.groupBox1.Controls.Add(this.radioButton4);
			this.groupBox1.Controls.Add(this.radioButton3);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboBox12);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.button20);
			this.groupBox1.Controls.Add(this.comboBox6);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.textBox5);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.comboBox10);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.comboBox9);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.comboBox8);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.comboBox7);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.CheckBox1);
			this.groupBox1.Controls.Add(this.comboBox5);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Location = new System.Drawing.Point(610, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(171, 518);
			this.groupBox1.TabIndex = 136;
			this.groupBox1.TabStop = false;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(88, 328);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(43, 17);
			this.radioButton2.TabIndex = 154;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Left";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(20, 328);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(50, 17);
			this.radioButton1.TabIndex = 133;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Right";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 237);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 153;
			this.label1.Text = "Gauge";
			// 
			// comboBox12
			// 
			this.comboBox12.FormattingEnabled = true;
			this.comboBox12.Items.AddRange(new object[] {
            "18 ga.",
            "20 ga.",
            "21 ga.",
            "22 ga."});
			this.comboBox12.Location = new System.Drawing.Point(17, 253);
			this.comboBox12.Name = "comboBox12";
			this.comboBox12.Size = new System.Drawing.Size(65, 21);
			this.comboBox12.TabIndex = 152;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(113, 80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(34, 13);
			this.label12.TabIndex = 151;
			this.label12.Text = "L/min";
			// 
			// comboBox6
			// 
			this.comboBox6.FormattingEnabled = true;
			this.comboBox6.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBox6.Location = new System.Drawing.Point(65, 75);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(40, 21);
			this.comboBox6.TabIndex = 150;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(75, 439);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(26, 13);
			this.label20.TabIndex = 149;
			this.label20.Text = "cc\'s";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(18, 436);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(51, 20);
			this.textBox5.TabIndex = 148;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(15, 391);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(42, 13);
			this.label19.TabIndex = 147;
			this.label19.Text = "IV Fluid";
			// 
			// comboBox10
			// 
			this.comboBox10.FormattingEnabled = true;
			this.comboBox10.Items.AddRange(new object[] {
            "D5(1/2)NS",
            "D5NS",
            "D5LR",
            "D5W",
            "LR",
            "NS"});
			this.comboBox10.Location = new System.Drawing.Point(17, 407);
			this.comboBox10.Name = "comboBox10";
			this.comboBox10.Size = new System.Drawing.Size(119, 21);
			this.comboBox10.TabIndex = 146;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(57, 361);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(48, 13);
			this.label18.TabIndex = 145;
			this.label18.Text = "Attempts";
			// 
			// comboBox9
			// 
			this.comboBox9.FormattingEnabled = true;
			this.comboBox9.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBox9.Location = new System.Drawing.Point(18, 358);
			this.comboBox9.Name = "comboBox9";
			this.comboBox9.Size = new System.Drawing.Size(30, 21);
			this.comboBox9.TabIndex = 144;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(16, 284);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(38, 13);
			this.label17.TabIndex = 143;
			this.label17.Text = "IV Site";
			// 
			// comboBox8
			// 
			this.comboBox8.FormattingEnabled = true;
			this.comboBox8.Items.AddRange(new object[] {
            "Antecubital fossa",
            "Forearm (dorsal)",
            "Forearm (ventral)",
            "Hand",
            "Wrist"});
			this.comboBox8.Location = new System.Drawing.Point(17, 301);
			this.comboBox8.Name = "comboBox8";
			this.comboBox8.Size = new System.Drawing.Size(119, 21);
			this.comboBox8.TabIndex = 142;
			this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.comboBox8_SelectedIndexChanged);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(15, 197);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(55, 13);
			this.label16.TabIndex = 137;
			this.label16.Text = "IV Access";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(15, 123);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(36, 13);
			this.label15.TabIndex = 133;
			this.label15.Text = "Route";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(14, 59);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(96, 13);
			this.label14.TabIndex = 132;
			this.label14.Text = "Inhalational agents";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(113, 102);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(34, 13);
			this.label13.TabIndex = 131;
			this.label13.Text = "L/min";
			// 
			// comboBox7
			// 
			this.comboBox7.FormattingEnabled = true;
			this.comboBox7.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBox7.Location = new System.Drawing.Point(65, 97);
			this.comboBox7.Name = "comboBox7";
			this.comboBox7.Size = new System.Drawing.Size(40, 21);
			this.comboBox7.TabIndex = 130;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(18, 101);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(46, 17);
			this.checkBox2.TabIndex = 128;
			this.checkBox2.Text = "N20";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// CheckBox1
			// 
			this.CheckBox1.AutoSize = true;
			this.CheckBox1.Location = new System.Drawing.Point(18, 77);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(40, 17);
			this.CheckBox1.TabIndex = 127;
			this.CheckBox1.Text = "O2";
			this.CheckBox1.UseVisualStyleBackColor = true;
			// 
			// comboBox5
			// 
			this.comboBox5.FormattingEnabled = true;
			this.comboBox5.Items.AddRange(new object[] {
            "I",
            "II",
            "III"});
			this.comboBox5.Location = new System.Drawing.Point(17, 32);
			this.comboBox5.Name = "comboBox5";
			this.comboBox5.Size = new System.Drawing.Size(50, 21);
			this.comboBox5.TabIndex = 125;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(15, 16);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(95, 13);
			this.label11.TabIndex = 126;
			this.label11.Text = "ASA  Classification";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.comboBox13);
			this.groupBox2.Controls.Add(this.groupBox5);
			this.groupBox2.Controls.Add(this.listAnesthetic);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.listAnestheticMeds);
			this.groupBox2.Controls.Add(this.comboBox3);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.comboBox4);
			this.groupBox2.Controls.Add(this.textAnesthOpen);
			this.groupBox2.Controls.Add(this.textSurgOpen);
			this.groupBox2.Controls.Add(this.butAnesthOpen);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.butSurgOpen);
			this.groupBox2.Controls.Add(this.butAnesthClose);
			this.groupBox2.Controls.Add(this.button19);
			this.groupBox2.Controls.Add(this.button18);
			this.groupBox2.Controls.Add(this.groupBox6);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(592, 334);
			this.groupBox2.TabIndex = 137;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "IV Anesthetics";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(295, 158);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 13);
			this.label5.TabIndex = 100;
			this.label5.Text = "Dose";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(298, 177);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(54, 20);
			this.textBox1.TabIndex = 99;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(249, 110);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 98;
			this.label3.Text = "Surgeon";
			// 
			// comboBox13
			// 
			this.comboBox13.FormattingEnabled = true;
			this.comboBox13.Location = new System.Drawing.Point(252, 127);
			this.comboBox13.Name = "comboBox13";
			this.comboBox13.Size = new System.Drawing.Size(100, 21);
			this.comboBox13.TabIndex = 97;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.button13);
			this.groupBox5.Controls.Add(this.button3);
			this.groupBox5.Controls.Add(this.button4);
			this.groupBox5.Controls.Add(this.button5);
			this.groupBox5.Controls.Add(this.button6);
			this.groupBox5.Controls.Add(this.button7);
			this.groupBox5.Controls.Add(this.button8);
			this.groupBox5.Controls.Add(this.button9);
			this.groupBox5.Controls.Add(this.button10);
			this.groupBox5.Controls.Add(this.button11);
			this.groupBox5.Controls.Add(this.button12);
			this.groupBox5.Controls.Add(this.button14);
			this.groupBox5.Controls.Add(this.button15);
			this.groupBox5.Controls.Add(this.button16);
			this.groupBox5.Controls.Add(this.button17);
			this.groupBox5.Controls.Add(this.button25);
			this.groupBox5.Location = new System.Drawing.Point(378, 159);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(200, 177);
			this.groupBox5.TabIndex = 54;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Click to add dose";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.butSurgClose);
			this.groupBox6.Controls.Add(this.textSurgClose);
			this.groupBox6.Controls.Add(this.textAnesthClose);
			this.groupBox6.Location = new System.Drawing.Point(142, 16);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(444, 83);
			this.groupBox6.TabIndex = 96;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Times";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button27);
			this.groupBox3.Controls.Add(this.label27);
			this.groupBox3.Controls.Add(this.button26);
			this.groupBox3.Controls.Add(this.textBox9);
			this.groupBox3.Controls.Add(this.label26);
			this.groupBox3.Controls.Add(this.butClearSig);
			this.groupBox3.Controls.Add(this.textBox8);
			this.groupBox3.Controls.Add(this.sigBox);
			this.groupBox3.Controls.Add(this.label25);
			this.groupBox3.Controls.Add(this.label28);
			this.groupBox3.Controls.Add(this.textBox7);
			this.groupBox3.Controls.Add(this.textBox6);
			this.groupBox3.Controls.Add(this.label24);
			this.groupBox3.Controls.Add(this.comboBox11);
			this.groupBox3.Controls.Add(this.label23);
			this.groupBox3.Location = new System.Drawing.Point(12, 537);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(769, 180);
			this.groupBox3.TabIndex = 138;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Notes (record additional meds/routes/times here)";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textBox11);
			this.groupBox4.Controls.Add(this.dataGridView1);
			this.groupBox4.Controls.Add(this.textBox10);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Location = new System.Drawing.Point(12, 352);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(592, 178);
			this.groupBox4.TabIndex = 139;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Vital Signs";
			// 
			// textBox11
			// 
			this.textBox11.Location = new System.Drawing.Point(347, 19);
			this.textBox11.Name = "textBox11";
			this.textBox11.Size = new System.Drawing.Size(88, 20);
			this.textBox11.TabIndex = 132;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(15, 51);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(564, 108);
			this.dataGridView1.TabIndex = 131;
			// 
			// textBox10
			// 
			this.textBox10.Location = new System.Drawing.Point(170, 19);
			this.textBox10.Name = "textBox10";
			this.textBox10.Size = new System.Drawing.Size(88, 20);
			this.textBox10.TabIndex = 130;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(156, 122);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(59, 13);
			this.label8.TabIndex = 88;
			this.label8.Text = "Anesthetist";
			// 
			// button20
			// 
			this.button20.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button20.Autosize = true;
			this.button20.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button20.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button20.CornerRadius = 4F;
			this.button20.Location = new System.Drawing.Point(15, 467);
			this.button20.Name = "button20";
			this.button20.Size = new System.Drawing.Size(131, 32);
			this.button20.TabIndex = 129;
			this.button20.Text = "Post anesthesia score";
			this.button20.UseVisualStyleBackColor = true;
			this.button20.Click += new System.EventHandler(this.button20_Click_1);
			// 
			// button13
			// 
			this.button13.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button13.Autosize = true;
			this.button13.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button13.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button13.CornerRadius = 4F;
			this.button13.Location = new System.Drawing.Point(47, 136);
			this.button13.Name = "button13";
			this.button13.Size = new System.Drawing.Size(32, 32);
			this.button13.TabIndex = 67;
			this.button13.Text = "10";
			this.button13.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button3.Autosize = true;
			this.button3.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button3.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button3.CornerRadius = 4F;
			this.button3.Location = new System.Drawing.Point(9, 22);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(32, 32);
			this.button3.TabIndex = 57;
			this.button3.Text = "7";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button4.Autosize = true;
			this.button4.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button4.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button4.CornerRadius = 4F;
			this.button4.Location = new System.Drawing.Point(47, 22);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.TabIndex = 58;
			this.button4.Text = "8";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button5.Autosize = true;
			this.button5.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button5.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button5.CornerRadius = 4F;
			this.button5.Location = new System.Drawing.Point(85, 22);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.TabIndex = 59;
			this.button5.Text = "9";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button6.Autosize = true;
			this.button6.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button6.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button6.CornerRadius = 4F;
			this.button6.Location = new System.Drawing.Point(9, 60);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(32, 32);
			this.button6.TabIndex = 60;
			this.button6.Text = "6";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button7.Autosize = true;
			this.button7.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button7.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button7.CornerRadius = 4F;
			this.button7.Location = new System.Drawing.Point(47, 60);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(32, 32);
			this.button7.TabIndex = 61;
			this.button7.Text = "5";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// button8
			// 
			this.button8.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button8.Autosize = true;
			this.button8.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button8.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button8.CornerRadius = 4F;
			this.button8.Location = new System.Drawing.Point(85, 60);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(32, 32);
			this.button8.TabIndex = 62;
			this.button8.Text = "4";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// button9
			// 
			this.button9.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button9.Autosize = true;
			this.button9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button9.CornerRadius = 4F;
			this.button9.Location = new System.Drawing.Point(9, 98);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(32, 32);
			this.button9.TabIndex = 63;
			this.button9.Text = "3";
			this.button9.UseVisualStyleBackColor = true;
			// 
			// button10
			// 
			this.button10.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button10.Autosize = true;
			this.button10.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button10.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button10.CornerRadius = 4F;
			this.button10.Location = new System.Drawing.Point(47, 98);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(32, 32);
			this.button10.TabIndex = 64;
			this.button10.Text = "2";
			this.button10.UseVisualStyleBackColor = true;
			// 
			// button11
			// 
			this.button11.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button11.Autosize = true;
			this.button11.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button11.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button11.CornerRadius = 4F;
			this.button11.Location = new System.Drawing.Point(85, 98);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(32, 32);
			this.button11.TabIndex = 65;
			this.button11.Text = "1";
			this.button11.UseVisualStyleBackColor = true;
			// 
			// button12
			// 
			this.button12.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button12.Autosize = true;
			this.button12.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button12.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button12.CornerRadius = 4F;
			this.button12.Location = new System.Drawing.Point(9, 136);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(32, 32);
			this.button12.TabIndex = 66;
			this.button12.Text = "0";
			this.button12.UseVisualStyleBackColor = true;
			// 
			// button14
			// 
			this.button14.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button14.Autosize = true;
			this.button14.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button14.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button14.CornerRadius = 4F;
			this.button14.Location = new System.Drawing.Point(123, 22);
			this.button14.Name = "button14";
			this.button14.Size = new System.Drawing.Size(70, 32);
			this.button14.TabIndex = 68;
			this.button14.Text = "25";
			this.button14.UseVisualStyleBackColor = true;
			// 
			// button15
			// 
			this.button15.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button15.Autosize = true;
			this.button15.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button15.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button15.CornerRadius = 4F;
			this.button15.Location = new System.Drawing.Point(123, 60);
			this.button15.Name = "button15";
			this.button15.Size = new System.Drawing.Size(70, 32);
			this.button15.TabIndex = 69;
			this.button15.Text = "50";
			this.button15.UseVisualStyleBackColor = true;
			// 
			// button16
			// 
			this.button16.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button16.Autosize = true;
			this.button16.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button16.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button16.CornerRadius = 4F;
			this.button16.Location = new System.Drawing.Point(123, 98);
			this.button16.Name = "button16";
			this.button16.Size = new System.Drawing.Size(70, 32);
			this.button16.TabIndex = 70;
			this.button16.Text = "100";
			this.button16.UseVisualStyleBackColor = true;
			// 
			// button17
			// 
			this.button17.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button17.Autosize = true;
			this.button17.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button17.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button17.CornerRadius = 4F;
			this.button17.Location = new System.Drawing.Point(123, 136);
			this.button17.Name = "button17";
			this.button17.Size = new System.Drawing.Size(70, 32);
			this.button17.TabIndex = 71;
			this.button17.Text = "Enter";
			this.button17.UseVisualStyleBackColor = true;
			// 
			// button25
			// 
			this.button25.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button25.Autosize = true;
			this.button25.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button25.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button25.CornerRadius = 4F;
			this.button25.Location = new System.Drawing.Point(85, 136);
			this.button25.Name = "button25";
			this.button25.Size = new System.Drawing.Size(32, 32);
			this.button25.TabIndex = 86;
			this.button25.Text = ".";
			this.button25.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button1.Autosize = true;
			this.button1.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button1.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button1.CornerRadius = 4F;
			this.button1.Image = global::OpenDental.Properties.Resources.Add;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(34, 151);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(82, 26);
			this.button1.TabIndex = 53;
			this.button1.Text = "Add";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button2.Autosize = true;
			this.button2.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button2.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button2.CornerRadius = 4F;
			this.button2.Image = global::OpenDental.Properties.Resources.deleteX;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(34, 183);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(82, 26);
			this.button2.TabIndex = 3;
			this.button2.Text = "Delete";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// butAnesthOpen
			// 
			this.butAnesthOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthOpen.Autosize = true;
			this.butAnesthOpen.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthOpen.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthOpen.CornerRadius = 4F;
			this.butAnesthOpen.Location = new System.Drawing.Point(147, 35);
			this.butAnesthOpen.Name = "butAnesthOpen";
			this.butAnesthOpen.Size = new System.Drawing.Size(96, 26);
			this.butAnesthOpen.TabIndex = 82;
			this.butAnesthOpen.Text = "Anesthesia Open";
			this.butAnesthOpen.UseVisualStyleBackColor = true;
			this.butAnesthOpen.Click += new System.EventHandler(this.buttonAnesthOpen_Click);
			// 
			// butSurgOpen
			// 
			this.butSurgOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSurgOpen.Autosize = true;
			this.butSurgOpen.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSurgOpen.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSurgOpen.CornerRadius = 4F;
			this.butSurgOpen.Location = new System.Drawing.Point(249, 35);
			this.butSurgOpen.Name = "butSurgOpen";
			this.butSurgOpen.Size = new System.Drawing.Size(97, 26);
			this.butSurgOpen.TabIndex = 83;
			this.butSurgOpen.Text = "Surgery Open";
			this.butSurgOpen.UseVisualStyleBackColor = true;
			this.butSurgOpen.Click += new System.EventHandler(this.butSurgOpen_Click);
			// 
			// butAnesthClose
			// 
			this.butAnesthClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAnesthClose.Autosize = true;
			this.butAnesthClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAnesthClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAnesthClose.CornerRadius = 4F;
			this.butAnesthClose.Location = new System.Drawing.Point(481, 35);
			this.butAnesthClose.Name = "butAnesthClose";
			this.butAnesthClose.Size = new System.Drawing.Size(97, 26);
			this.butAnesthClose.TabIndex = 85;
			this.butAnesthClose.Text = "Anesthesia Close";
			this.butAnesthClose.UseVisualStyleBackColor = true;
			this.butAnesthClose.Click += new System.EventHandler(this.butAnesthClose_Click);
			// 
			// button19
			// 
			this.button19.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button19.Autosize = true;
			this.button19.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button19.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button19.CornerRadius = 4F;
			this.button19.Image = global::OpenDental.Properties.Resources.deleteX;
			this.button19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button19.Location = new System.Drawing.Point(270, 296);
			this.button19.Name = "button19";
			this.button19.Size = new System.Drawing.Size(82, 26);
			this.button19.TabIndex = 74;
			this.button19.Text = "Delete";
			this.button19.UseVisualStyleBackColor = true;
			this.button19.Click += new System.EventHandler(this.button19_Click);
			// 
			// button18
			// 
			this.button18.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button18.Autosize = true;
			this.button18.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button18.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button18.CornerRadius = 4F;
			this.button18.Image = global::OpenDental.Properties.Resources.Add;
			this.button18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button18.Location = new System.Drawing.Point(182, 296);
			this.button18.Name = "button18";
			this.button18.Size = new System.Drawing.Size(82, 26);
			this.button18.TabIndex = 73;
			this.button18.Text = "Add";
			this.button18.UseVisualStyleBackColor = true;
			this.button18.Click += new System.EventHandler(this.button18_Click);
			// 
			// butSurgClose
			// 
			this.butSurgClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSurgClose.Autosize = true;
			this.butSurgClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSurgClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSurgClose.CornerRadius = 4F;
			this.butSurgClose.Location = new System.Drawing.Point(237, 19);
			this.butSurgClose.Name = "butSurgClose";
			this.butSurgClose.Size = new System.Drawing.Size(96, 26);
			this.butSurgClose.TabIndex = 84;
			this.butSurgClose.Text = "Surgery Close";
			this.butSurgClose.UseVisualStyleBackColor = true;
			this.butSurgClose.Click += new System.EventHandler(this.butSurgClose_Click);
			// 
			// button27
			// 
			this.button27.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button27.Autosize = true;
			this.button27.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button27.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button27.CornerRadius = 4F;
			this.button27.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button27.Location = new System.Drawing.Point(671, 137);
			this.button27.Name = "button27";
			this.button27.Size = new System.Drawing.Size(88, 32);
			this.button27.TabIndex = 136;
			this.button27.Text = "Close";
			this.button27.UseVisualStyleBackColor = true;
			this.button27.Click += new System.EventHandler(this.button27_Click);
			// 
			// button26
			// 
			this.button26.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.button26.Autosize = true;
			this.button26.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.button26.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.button26.CornerRadius = 4F;
			this.button26.Image = global::OpenDental.Properties.Resources.butPrintSmall;
			this.button26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button26.Location = new System.Drawing.Point(577, 137);
			this.button26.Name = "button26";
			this.button26.Size = new System.Drawing.Size(88, 32);
			this.button26.TabIndex = 102;
			this.button26.Text = "Print";
			this.button26.UseVisualStyleBackColor = true;
			// 
			// butClearSig
			// 
			this.butClearSig.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClearSig.Autosize = true;
			this.butClearSig.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClearSig.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClearSig.CornerRadius = 4F;
			this.butClearSig.Location = new System.Drawing.Point(671, 32);
			this.butClearSig.Name = "butClearSig";
			this.butClearSig.Size = new System.Drawing.Size(81, 25);
			this.butClearSig.TabIndex = 134;
			this.butClearSig.Text = "Clear Sig";
			// 
			// sigBox
			// 
			this.sigBox.Location = new System.Drawing.Point(481, 32);
			this.sigBox.Name = "sigBox";
			this.sigBox.Size = new System.Drawing.Size(176, 79);
			this.sigBox.TabIndex = 135;
			this.sigBox.Click += new System.EventHandler(this.sigBox_Click);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(17, 212);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(65, 17);
			this.radioButton3.TabIndex = 155;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Catheter";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new System.Drawing.Point(82, 212);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(63, 17);
			this.radioButton4.TabIndex = 156;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "Butterfly";
			this.radioButton4.UseVisualStyleBackColor = true;
			// 
			// radioButton5
			// 
			this.radioButton5.AutoSize = true;
			this.radioButton5.Location = new System.Drawing.Point(18, 138);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(79, 17);
			this.radioButton5.TabIndex = 157;
			this.radioButton5.TabStop = true;
			this.radioButton5.Text = "Nasal hood";
			this.radioButton5.UseVisualStyleBackColor = true;
			// 
			// radioButton6
			// 
			this.radioButton6.AutoSize = true;
			this.radioButton6.Location = new System.Drawing.Point(18, 157);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(93, 17);
			this.radioButton6.TabIndex = 158;
			this.radioButton6.TabStop = true;
			this.radioButton6.Text = "Nasal cannula";
			this.radioButton6.UseVisualStyleBackColor = true;
			// 
			// radioButton7
			// 
			this.radioButton7.AutoSize = true;
			this.radioButton7.Location = new System.Drawing.Point(18, 175);
			this.radioButton7.Name = "radioButton7";
			this.radioButton7.Size = new System.Drawing.Size(112, 17);
			this.radioButton7.TabIndex = 159;
			this.radioButton7.TabStop = true;
			this.radioButton7.Text = "Endotracheal tube";
			this.radioButton7.UseVisualStyleBackColor = true;
			// 
			// FormAnestheticRecord
			// 
			this.ClientSize = new System.Drawing.Size(793, 732);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAnestheticRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Anesthetic Record";
			this.Load += new System.EventHandler(this.FormAnestheticRecord_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void FormAnestheticRecord_Load(object sender, EventArgs e){

		}

		private void button1_Click(object sender, EventArgs e){

		}

		private void button20_Click(object sender, EventArgs e){

		}

		private void comboBox5_SelectedIndexChanged(object sender, EventArgs e){

		}

		private void checkBox7_CheckedChanged(object sender, EventArgs e){

		}

		private void label26_Click(object sender, EventArgs e){

		}

		 private void button27_Click(object sender, System.EventArgs e){
			Close();}

		private void comboBox8_SelectedIndexChanged(object sender, EventArgs e){

		}

		private void butClose_Click(object sender, System.EventArgs e){
			Close();
		}

		private void FormAnestheticRecord_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{


		}

		private void listAnesthetic_SelectedIndexChanged(object sender, EventArgs e){

		}

		private void sigBox_Click(object sender, EventArgs e)
		{

		}

		private void button20_Click_1(object sender, EventArgs e)
		{
			
		}
		
	

		private void buttonAnesthOpen_Click(object sender, EventArgs e)
		{
			textAnesthOpen.Text = MiscData.GetNowDateTime().ToString("HH:mm");
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void textSurgOpen_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void textAnesthClose_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void textSurgClose_TextChanged_1(object sender, EventArgs e)
		{
			
		}

		private void butSurgOpen_Click(object sender, EventArgs e)
		{
			textSurgOpen.Text = MiscData.GetNowDateTime().ToString("HH:mm");
		}

		private void butSurgClose_Click(object sender, EventArgs e)
		{
			textSurgClose.Text = MiscData.GetNowDateTime().ToString("HH:mm");
		}

		private void butAnesthClose_Click(object sender, EventArgs e)
		{
			textAnesthClose.Text = MiscData.GetNowDateTime().ToString("HH:mm");
		}

		private void button19_Click(object sender, EventArgs e)
		{

		}

		private void button18_Click(object sender, EventArgs e)
		{

		}
		
		}
	}
