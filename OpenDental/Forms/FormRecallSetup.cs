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
		private System.Windows.Forms.TextBox textPostcardMessage;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textPostcardsPerSheet;
		private System.Windows.Forms.ListBox listProcs;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBW;
		private System.Windows.Forms.TextBox textProcsAdult;
		private System.Windows.Forms.TextBox textPatternAdult;
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
		private TextBox textFMXPanoProc;
		private Label label16;
		private Label label17;
		private TextBox textFMXPanoYrInterval;
		private Label label18;
		private TextBox textProcsChild;
		private CheckBox checkDisableAutoFilms;
		private GroupBox groupBox4;
		private GroupBox groupBox5;
		private Label label6;
		private TextBox textPatternChild;
		private GroupBox groupBox6;
		private TextBox textProcsPerio;
		private TextBox textPatternPerio;
		private Label label20;
		private Label label21;
		private CheckBox checkDisablePerioAlt;
		private Label label22;
		private GroupBox groupBox7;
		private TextBox textPerioTriggerProcs;
		private Label label24;
		private Label label27;
		private TextBox textBox2;
		private TextBox textBox3;
		private TextBox textBox4;
		private Label label19;
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRecallSetup(){
			InitializeComponent();
			Lan.F(this);
			Lan.C(this, new System.Windows.Forms.Control[] {
				textBox1,
				//textBox6
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
			this.textPostcardMessage = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textPostcardsPerSheet = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.listProcs = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.textPerioTriggerProcs = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.textProcsPerio = new System.Windows.Forms.TextBox();
			this.textPatternPerio = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.checkDisablePerioAlt = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textProcsAdult = new System.Windows.Forms.TextBox();
			this.textPatternAdult = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textPatternChild = new System.Windows.Forms.TextBox();
			this.textProcsChild = new System.Windows.Forms.TextBox();
			this.checkDisableAutoFilms = new System.Windows.Forms.CheckBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.textFMXPanoYrInterval = new System.Windows.Forms.TextBox();
			this.textFMXPanoProc = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBW = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.checkGroupFamilies = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textDaysFuture = new OpenDental.ValidNum();
			this.textDaysPast = new OpenDental.ValidNum();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label19 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(47, 21);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(500, 30);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "The following information is used to automate the process of creating recall appo" +
				"intments from the recall list.  You can make changes to the appointment after it" +
				" has been created.";
			// 
			// textPostcardMessage
			// 
			this.textPostcardMessage.AcceptsReturn = true;
			this.textPostcardMessage.Location = new System.Drawing.Point(172, 392);
			this.textPostcardMessage.MaxLength = 255;
			this.textPostcardMessage.Multiline = true;
			this.textPostcardMessage.Name = "textPostcardMessage";
			this.textPostcardMessage.Size = new System.Drawing.Size(466, 70);
			this.textPostcardMessage.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(4, 395);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167, 61);
			this.label7.TabIndex = 17;
			this.label7.Text = "Recall Postcard message.  Use ?DueDate wherever you want the due date to be inser" +
				"ted.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardsPerSheet
			// 
			this.textPostcardsPerSheet.Location = new System.Drawing.Point(172, 614);
			this.textPostcardsPerSheet.Name = "textPostcardsPerSheet";
			this.textPostcardsPerSheet.Size = new System.Drawing.Size(34, 20);
			this.textPostcardsPerSheet.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(45, 617);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(127, 31);
			this.label8.TabIndex = 19;
			this.label8.Text = "Postcards per sheet (1,3,or 4)";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listProcs
			// 
			this.listProcs.BackColor = System.Drawing.SystemColors.Control;
			this.listProcs.Location = new System.Drawing.Point(770, 392);
			this.listProcs.Name = "listProcs";
			this.listProcs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.listProcs.Size = new System.Drawing.Size(130, 82);
			this.listProcs.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox4);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.groupBox7);
			this.groupBox1.Controls.Add(this.groupBox6);
			this.groupBox1.Controls.Add(this.groupBox5);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.checkDisableAutoFilms);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.textFMXPanoYrInterval);
			this.groupBox1.Controls.Add(this.textFMXPanoProc);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.textBW);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(7, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(893, 374);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Recall Automation";
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.SystemColors.Control;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox4.Location = new System.Drawing.Point(32, 173);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(321, 40);
			this.textBox4.TabIndex = 39;
			this.textBox4.Text = "Enter these now even if you are not using auto entry and disable the check to the" +
				" right. This will enable auto indication of when last films were taken.";
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox3.Location = new System.Drawing.Point(475, 173);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(77, 27);
			this.textBox3.TabIndex = 38;
			this.textBox3.Text = "Blank to only use BW\'s";
			this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox2.Location = new System.Drawing.Point(368, 173);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(98, 23);
			this.textBox2.TabIndex = 37;
			this.textBox2.Text = "(One or the other)";
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.label19);
			this.groupBox7.Controls.Add(this.textPerioTriggerProcs);
			this.groupBox7.Controls.Add(this.label24);
			this.groupBox7.Location = new System.Drawing.Point(463, 219);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(424, 146);
			this.groupBox7.TabIndex = 36;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Perio Codes";
			// 
			// textPerioTriggerProcs
			// 
			this.textPerioTriggerProcs.Location = new System.Drawing.Point(141, 69);
			this.textPerioTriggerProcs.Multiline = true;
			this.textPerioTriggerProcs.Name = "textPerioTriggerProcs";
			this.textPerioTriggerProcs.Size = new System.Drawing.Size(277, 38);
			this.textPerioTriggerProcs.TabIndex = 36;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(1, 109);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(422, 33);
			this.label24.TabIndex = 37;
			this.label24.Text = "If perio exceptions are enabled, any or all of the above codes completed for a pa" +
				"tient will trigger perio exceptions in creation of recall appointment.";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label27);
			this.groupBox6.Controls.Add(this.label22);
			this.groupBox6.Controls.Add(this.textProcsPerio);
			this.groupBox6.Controls.Add(this.textPatternPerio);
			this.groupBox6.Controls.Add(this.label20);
			this.groupBox6.Controls.Add(this.label21);
			this.groupBox6.Controls.Add(this.checkDisablePerioAlt);
			this.groupBox6.Location = new System.Drawing.Point(6, 219);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(452, 146);
			this.groupBox6.TabIndex = 35;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Perio Exceptions";
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label27.Location = new System.Drawing.Point(2, 108);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(449, 36);
			this.label27.TabIndex = 38;
			this.label27.Text = "If a patient has a history of perio treatment or maintenance as determined by cod" +
				"es on the right, these values will overide the above adult or child values in re" +
				"call apptointments.";
			this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(314, 48);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(117, 19);
			this.label22.TabIndex = 33;
			this.label22.Text = "(only /\'s and X\'s)";
			// 
			// textProcsPerio
			// 
			this.textProcsPerio.Location = new System.Drawing.Point(139, 68);
			this.textProcsPerio.Multiline = true;
			this.textProcsPerio.Name = "textProcsPerio";
			this.textProcsPerio.Size = new System.Drawing.Size(295, 38);
			this.textProcsPerio.TabIndex = 29;
			// 
			// textPatternPerio
			// 
			this.textPatternPerio.Location = new System.Drawing.Point(139, 46);
			this.textPatternPerio.Name = "textPatternPerio";
			this.textPatternPerio.Size = new System.Drawing.Size(170, 20);
			this.textPatternPerio.TabIndex = 28;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(12, 50);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(123, 16);
			this.label20.TabIndex = 30;
			this.label20.Text = "Time Pattern";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(6, 69);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(131, 41);
			this.label21.TabIndex = 31;
			this.label21.Text = "Procedures (valid codes separated by commas)";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkDisablePerioAlt
			// 
			this.checkDisablePerioAlt.AutoSize = true;
			this.checkDisablePerioAlt.Checked = true;
			this.checkDisablePerioAlt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkDisablePerioAlt.Location = new System.Drawing.Point(8, 19);
			this.checkDisablePerioAlt.Name = "checkDisablePerioAlt";
			this.checkDisablePerioAlt.Size = new System.Drawing.Size(196, 17);
			this.checkDisablePerioAlt.TabIndex = 27;
			this.checkDisablePerioAlt.Text = "Disable exceptions for perio patients";
			this.checkDisablePerioAlt.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.textProcsAdult);
			this.groupBox5.Controls.Add(this.textPatternAdult);
			this.groupBox5.Controls.Add(this.label4);
			this.groupBox5.Controls.Add(this.label5);
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Location = new System.Drawing.Point(6, 53);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(451, 95);
			this.groupBox5.TabIndex = 34;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Adults";
			// 
			// textProcsAdult
			// 
			this.textProcsAdult.Location = new System.Drawing.Point(133, 41);
			this.textProcsAdult.Multiline = true;
			this.textProcsAdult.Name = "textProcsAdult";
			this.textProcsAdult.Size = new System.Drawing.Size(295, 42);
			this.textProcsAdult.TabIndex = 13;
			// 
			// textPatternAdult
			// 
			this.textPatternAdult.Location = new System.Drawing.Point(133, 19);
			this.textPatternAdult.Name = "textPatternAdult";
			this.textPatternAdult.Size = new System.Drawing.Size(170, 20);
			this.textPatternAdult.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(311, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(117, 19);
			this.label4.TabIndex = 18;
			this.label4.Text = "(only /\'s and X\'s)";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(6, 58);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 34);
			this.label5.TabIndex = 19;
			this.label5.Text = "(valid codes separated by commas)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Time Pattern";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 13);
			this.label2.TabIndex = 16;
			this.label2.Text = "Procedures";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.textPatternChild);
			this.groupBox4.Controls.Add(this.textProcsChild);
			this.groupBox4.Location = new System.Drawing.Point(463, 53);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(329, 95);
			this.groupBox4.TabIndex = 33;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Children under 12";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(190, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(117, 19);
			this.label6.TabIndex = 29;
			this.label6.Text = "(only /\'s and X\'s)";
			// 
			// textPatternChild
			// 
			this.textPatternChild.Location = new System.Drawing.Point(14, 19);
			this.textPatternChild.Name = "textPatternChild";
			this.textPatternChild.Size = new System.Drawing.Size(170, 20);
			this.textPatternChild.TabIndex = 28;
			// 
			// textProcsChild
			// 
			this.textProcsChild.Location = new System.Drawing.Point(14, 41);
			this.textProcsChild.Multiline = true;
			this.textProcsChild.Name = "textProcsChild";
			this.textProcsChild.Size = new System.Drawing.Size(294, 42);
			this.textProcsChild.TabIndex = 27;
			// 
			// checkDisableAutoFilms
			// 
			this.checkDisableAutoFilms.Location = new System.Drawing.Point(604, 153);
			this.checkDisableAutoFilms.Name = "checkDisableAutoFilms";
			this.checkDisableAutoFilms.Size = new System.Drawing.Size(260, 17);
			this.checkDisableAutoFilms.TabIndex = 26;
			this.checkDisableAutoFilms.Text = "Disable automated film entry to appointment";
			this.checkDisableAutoFilms.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(460, 152);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(36, 16);
			this.label18.TabIndex = 25;
			this.label18.Text = "every";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(528, 152);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(70, 16);
			this.label17.TabIndex = 24;
			this.label17.Text = "years if due.";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textFMXPanoYrInterval
			// 
			this.textFMXPanoYrInterval.Location = new System.Drawing.Point(502, 152);
			this.textFMXPanoYrInterval.Name = "textFMXPanoYrInterval";
			this.textFMXPanoYrInterval.Size = new System.Drawing.Size(20, 20);
			this.textFMXPanoYrInterval.TabIndex = 23;
			// 
			// textFMXPanoProc
			// 
			this.textFMXPanoProc.Location = new System.Drawing.Point(377, 152);
			this.textFMXPanoProc.Name = "textFMXPanoProc";
			this.textFMXPanoProc.Size = new System.Drawing.Size(77, 20);
			this.textFMXPanoProc.TabIndex = 21;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(240, 152);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(131, 18);
			this.label16.TabIndex = 22;
			this.label16.Text = "- or - FMX/Pano code";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBW
			// 
			this.textBW.Location = new System.Drawing.Point(160, 152);
			this.textBW.Name = "textBW";
			this.textBW.Size = new System.Drawing.Size(74, 20);
			this.textBW.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(156, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "BiteWing code every year";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(662, 394);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(103, 83);
			this.label9.TabIndex = 22;
			this.label9.Text = "Procedures that Trigger Recall - You can change these in procedure code setup";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkReturnAdd
			// 
			this.checkReturnAdd.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkReturnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkReturnAdd.Location = new System.Drawing.Point(2, 647);
			this.checkReturnAdd.Name = "checkReturnAdd";
			this.checkReturnAdd.Size = new System.Drawing.Size(184, 19);
			this.checkReturnAdd.TabIndex = 43;
			this.checkReturnAdd.Text = "Show return address";
			this.checkReturnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textConfirmPostcardMessage
			// 
			this.textConfirmPostcardMessage.AcceptsReturn = true;
			this.textConfirmPostcardMessage.Location = new System.Drawing.Point(172, 538);
			this.textConfirmPostcardMessage.MaxLength = 255;
			this.textConfirmPostcardMessage.Multiline = true;
			this.textConfirmPostcardMessage.Name = "textConfirmPostcardMessage";
			this.textConfirmPostcardMessage.Size = new System.Drawing.Size(466, 70);
			this.textConfirmPostcardMessage.TabIndex = 44;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0, 541);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(170, 61);
			this.label10.TabIndex = 45;
			this.label10.Text = "Confirmation Postcard message.  Use ?date  and ?time where you want those values " +
				"to be inserted";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPostcardFamMsg
			// 
			this.textPostcardFamMsg.AcceptsReturn = true;
			this.textPostcardFamMsg.Location = new System.Drawing.Point(172, 465);
			this.textPostcardFamMsg.MaxLength = 255;
			this.textPostcardFamMsg.Multiline = true;
			this.textPostcardFamMsg.Name = "textPostcardFamMsg";
			this.textPostcardFamMsg.Size = new System.Drawing.Size(466, 70);
			this.textPostcardFamMsg.TabIndex = 46;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(4, 468);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(167, 61);
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
			this.groupBox2.Location = new System.Drawing.Point(340, 612);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(204, 74);
			this.groupBox2.TabIndex = 48;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Adjust Postcard Position in Inches";
			// 
			// textDown
			// 
			this.textDown.Location = new System.Drawing.Point(113, 47);
			this.textDown.Name = "textDown";
			this.textDown.Size = new System.Drawing.Size(73, 20);
			this.textDown.TabIndex = 6;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(49, 46);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(60, 20);
			this.label12.TabIndex = 5;
			this.label12.Text = "Down";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRight
			// 
			this.textRight.Location = new System.Drawing.Point(113, 22);
			this.textRight.Name = "textRight";
			this.textRight.Size = new System.Drawing.Size(73, 20);
			this.textRight.TabIndex = 4;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(49, 21);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(60, 20);
			this.label13.TabIndex = 4;
			this.label13.Text = "Right";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkGroupFamilies
			// 
			this.checkGroupFamilies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.Location = new System.Drawing.Point(6, 15);
			this.checkGroupFamilies.Name = "checkGroupFamilies";
			this.checkGroupFamilies.Size = new System.Drawing.Size(121, 18);
			this.checkGroupFamilies.TabIndex = 49;
			this.checkGroupFamilies.Text = "Group Families";
			this.checkGroupFamilies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkGroupFamilies.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6, 34);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(101, 20);
			this.label14.TabIndex = 50;
			this.label14.Text = "Days Past";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(9, 56);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(98, 20);
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
			this.groupBox3.Location = new System.Drawing.Point(665, 517);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(234, 87);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Recall List Default View";
			// 
			// textDaysFuture
			// 
			this.textDaysFuture.Location = new System.Drawing.Point(113, 57);
			this.textDaysFuture.MaxVal = 10000;
			this.textDaysFuture.MinVal = -10000;
			this.textDaysFuture.Name = "textDaysFuture";
			this.textDaysFuture.Size = new System.Drawing.Size(53, 20);
			this.textDaysFuture.TabIndex = 53;
			// 
			// textDaysPast
			// 
			this.textDaysPast.Location = new System.Drawing.Point(113, 35);
			this.textDaysPast.MaxVal = 10000;
			this.textDaysPast.MinVal = -10000;
			this.textDaysPast.Name = "textDaysPast";
			this.textDaysPast.Size = new System.Drawing.Size(53, 20);
			this.textDaysPast.TabIndex = 51;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(813, 620);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(813, 658);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(4, 68);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(131, 41);
			this.label19.TabIndex = 39;
			this.label19.Text = "Procedures (valid codes separated by commas)";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormRecallSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(912, 699);
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
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRecallSetup_Load(object sender, System.EventArgs e) {
			textPatternAdult.Text=PrefB.GetString("RecallPattern");
			textProcsAdult.Text = ((Pref)PrefB.HList["RecallProcedures"]).ValueString;
			textPatternChild.Text = PrefB.GetString("RecallPatternChild");
			textProcsChild.Text = ((Pref)PrefB.HList["RecallProceduresChild"]).ValueString;
			textPatternPerio.Text = PrefB.GetString("RecallPatternPerio");
			textProcsPerio.Text = ((Pref)PrefB.HList["RecallProceduresPerio"]).ValueString;
			textPerioTriggerProcs.Text = ((Pref)PrefB.HList["RecallPerioTriggerProcs"]).ValueString;
			textBW.Text = ((Pref)PrefB.HList["RecallBW"]).ValueString;
			textFMXPanoProc.Text = ((Pref)PrefB.HList["RecallFMXPanoProc"]).ValueString;
			checkDisableAutoFilms.Checked = PrefB.GetBool("RecallDisableAutoFilms");
			checkDisablePerioAlt.Checked = PrefB.GetBool("RecallDisablePerioAlt");
			textFMXPanoYrInterval.Text = PrefB.GetInt("RecallFMXPanoYrInterval").ToString();
			checkGroupFamilies.Checked = PrefB.GetBool("RecallGroupByFamily");
			textPostcardMessage.Text = PrefB.GetString("RecallPostcardMessage");
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
			if(textFMXPanoYrInterval.Text!="1"
				&& textFMXPanoYrInterval.Text!="2"
				&& textFMXPanoYrInterval.Text!="3"
				&& textFMXPanoYrInterval.Text!="4"
				&& textFMXPanoYrInterval.Text!="5"
				&& textFMXPanoYrInterval.Text!="6"
				&& textFMXPanoYrInterval.Text!="7"
				&& textFMXPanoYrInterval.Text!="")
			{
				textFMXPanoYrInterval.Text="";
				MsgBox.Show(this,"The value for FMX/Pano interval must be a single number between 1 and 7 years, or you may leave it blank to disable this and only use BW's.");
				return;
			}


			Prefs.UpdateInt("RecallFMXPanoYrInterval", PIn.PInt(textFMXPanoYrInterval.Text));
			Prefs.UpdateString("RecallFMXPanoProc", textFMXPanoProc.Text);
			Prefs.UpdateBool("RecallDisableAutoFilms", checkDisableAutoFilms.Checked);

			Prefs.UpdateString("RecallPerioTriggerProcs", textPerioTriggerProcs.Text);
			
			Prefs.UpdateString("RecallProceduresPerio", textProcsPerio.Text);
			Prefs.UpdateString("RecallPatternPerio", textPatternPerio.Text);
			Prefs.UpdateBool("RecallDisablePerioAlt", checkDisablePerioAlt.Checked);

			Prefs.UpdateString("RecallProceduresChild", textProcsChild.Text);
			Prefs.UpdateString("RecallPatternChild", textPatternChild.Text);
			
			Prefs.UpdateString("RecallPattern",textPatternAdult.Text);
			
			Prefs.UpdateString("RecallProcedures",textProcsAdult.Text);

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
