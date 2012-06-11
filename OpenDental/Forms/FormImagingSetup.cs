using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
///<summary></summary>
	public class FormImagingSetup:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label12;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label37;
		private ContrWindowingSlider slider;
		private Label label1;
		private OpenDental.UI.Button butMounts;
		private GroupBox groupBox4;
		private Label label2;
		private NumericUpDown upDownExposure;
		private NumericUpDown upDownPort;
		private Label label4;
		private Label label3;
		private ValidNum textScanDocQuality;
		private ComboBox comboType;
		private ValidNum textScanDocResolution;
		private Label label6;
		private Label label5;
		private UI.Button butSetScanner;
		private CheckBox checkScanDocSelectSource;
		private CheckBox checkScanDocDuplex;
		private Label label7;
		private CheckBox checkScanDocGrayscale;
		private RadioButton radioScanDocUseOptionsBelow;
		private RadioButton radioScanDocShowOptions;
		private GroupBox groupScanningOptions;
		private Label label8;
		private CheckBox checkBinned;
		//private ComputerPref computerPrefs;

		///<summary></summary>
		public FormImagingSetup(){
			InitializeComponent();
			//too many labels to use Lan.F()
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				this,
				this.groupBox1,
				this.groupBox2,
				this.groupBox3,
				this.label1,
				this.label2,
				this.label3,
				this.label4,
				this.label5,
				this.label6,
				this.label7,
				this.label12,
				this.label25,
				this.label37
			});
			Lan.C("All", new System.Windows.Forms.Control[] {
				butOK,
				butCancel,
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImagingSetup));
			this.label12 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioScanDocUseOptionsBelow = new System.Windows.Forms.RadioButton();
			this.radioScanDocShowOptions = new System.Windows.Forms.RadioButton();
			this.groupScanningOptions = new System.Windows.Forms.GroupBox();
			this.checkScanDocDuplex = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textScanDocResolution = new OpenDental.ValidNum();
			this.checkScanDocGrayscale = new System.Windows.Forms.CheckBox();
			this.checkScanDocSelectSource = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textScanDocQuality = new OpenDental.ValidNum();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.slider = new OpenDental.UI.ContrWindowingSlider();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label37 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.checkBinned = new System.Windows.Forms.CheckBox();
			this.comboType = new System.Windows.Forms.ComboBox();
			this.upDownPort = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.upDownExposure = new System.Windows.Forms.NumericUpDown();
			this.butSetScanner = new OpenDental.UI.Button();
			this.butMounts = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupScanningOptions.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDownPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownExposure)).BeginInit();
			this.SuspendLayout();
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(17, 42);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(254, 15);
			this.label12.TabIndex = 14;
			this.label12.Text = "JPEG Compression - Quality After Scanning";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioScanDocUseOptionsBelow);
			this.groupBox1.Controls.Add(this.radioScanDocShowOptions);
			this.groupBox1.Controls.Add(this.groupScanningOptions);
			this.groupBox1.Controls.Add(this.checkScanDocSelectSource);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textScanDocQuality);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(20, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(622, 197);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Documents - Settings apply only to this workstation";
			// 
			// radioScanDocUseOptionsBelow
			// 
			this.radioScanDocUseOptionsBelow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioScanDocUseOptionsBelow.Location = new System.Drawing.Point(85, 81);
			this.radioScanDocUseOptionsBelow.Name = "radioScanDocUseOptionsBelow";
			this.radioScanDocUseOptionsBelow.Size = new System.Drawing.Size(202, 18);
			this.radioScanDocUseOptionsBelow.TabIndex = 26;
			this.radioScanDocUseOptionsBelow.TabStop = true;
			this.radioScanDocUseOptionsBelow.Text = "Use the Options Below";
			this.radioScanDocUseOptionsBelow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioScanDocUseOptionsBelow.UseVisualStyleBackColor = true;
			// 
			// radioScanDocShowOptions
			// 
			this.radioScanDocShowOptions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioScanDocShowOptions.Location = new System.Drawing.Point(67, 63);
			this.radioScanDocShowOptions.Name = "radioScanDocShowOptions";
			this.radioScanDocShowOptions.Size = new System.Drawing.Size(220, 18);
			this.radioScanDocShowOptions.TabIndex = 25;
			this.radioScanDocShowOptions.TabStop = true;
			this.radioScanDocShowOptions.Text = "Show Scanner Options Window";
			this.radioScanDocShowOptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioScanDocShowOptions.UseVisualStyleBackColor = true;
			this.radioScanDocShowOptions.CheckedChanged += new System.EventHandler(this.radioScanDocShowOptions_CheckedChanged);
			// 
			// groupScanningOptions
			// 
			this.groupScanningOptions.Controls.Add(this.label8);
			this.groupScanningOptions.Controls.Add(this.checkScanDocDuplex);
			this.groupScanningOptions.Controls.Add(this.label5);
			this.groupScanningOptions.Controls.Add(this.label6);
			this.groupScanningOptions.Controls.Add(this.textScanDocResolution);
			this.groupScanningOptions.Controls.Add(this.checkScanDocGrayscale);
			this.groupScanningOptions.Location = new System.Drawing.Point(32, 100);
			this.groupScanningOptions.Name = "groupScanningOptions";
			this.groupScanningOptions.Size = new System.Drawing.Size(584, 90);
			this.groupScanningOptions.TabIndex = 24;
			this.groupScanningOptions.TabStop = false;
			this.groupScanningOptions.Text = "Scanning Options";
			// 
			// checkScanDocDuplex
			// 
			this.checkScanDocDuplex.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocDuplex.Location = new System.Drawing.Point(9, 19);
			this.checkScanDocDuplex.Name = "checkScanDocDuplex";
			this.checkScanDocDuplex.Size = new System.Drawing.Size(247, 18);
			this.checkScanDocDuplex.TabIndex = 21;
			this.checkScanDocDuplex.Text = "Multipage Scans Duplex (both sides)";
			this.checkScanDocDuplex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocDuplex.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(52, 66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(186, 19);
			this.label5.TabIndex = 15;
			this.label5.Text = "Resolution";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(313, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(180, 19);
			this.label6.TabIndex = 15;
			this.label6.Text = "> 50. Typical setting: 150 dpi";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textScanDocResolution
			// 
			this.textScanDocResolution.Location = new System.Drawing.Point(241, 65);
			this.textScanDocResolution.MaxVal = 1000;
			this.textScanDocResolution.MinVal = 51;
			this.textScanDocResolution.Name = "textScanDocResolution";
			this.textScanDocResolution.Size = new System.Drawing.Size(68, 20);
			this.textScanDocResolution.TabIndex = 20;
			// 
			// checkScanDocGrayscale
			// 
			this.checkScanDocGrayscale.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocGrayscale.Location = new System.Drawing.Point(110, 47);
			this.checkScanDocGrayscale.Name = "checkScanDocGrayscale";
			this.checkScanDocGrayscale.Size = new System.Drawing.Size(146, 18);
			this.checkScanDocGrayscale.TabIndex = 22;
			this.checkScanDocGrayscale.Text = "Grayscale";
			this.checkScanDocGrayscale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocGrayscale.UseVisualStyleBackColor = true;
			// 
			// checkScanDocSelectSource
			// 
			this.checkScanDocSelectSource.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocSelectSource.Location = new System.Drawing.Point(32, 19);
			this.checkScanDocSelectSource.Name = "checkScanDocSelectSource";
			this.checkScanDocSelectSource.Size = new System.Drawing.Size(256, 18);
			this.checkScanDocSelectSource.TabIndex = 23;
			this.checkScanDocSelectSource.Text = "Show Select Scanner Window";
			this.checkScanDocSelectSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkScanDocSelectSource.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(345, 42);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(246, 15);
			this.label7.TabIndex = 23;
			this.label7.Text = "0-100. 100=No compression.  Typical setting: 40";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textScanDocQuality
			// 
			this.textScanDocQuality.Location = new System.Drawing.Point(273, 40);
			this.textScanDocQuality.MaxVal = 100;
			this.textScanDocQuality.MinVal = 0;
			this.textScanDocQuality.Name = "textScanDocQuality";
			this.textScanDocQuality.Size = new System.Drawing.Size(68, 20);
			this.textScanDocQuality.TabIndex = 20;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.slider);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(20, 251);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(484, 96);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Radiographs";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(425, 19);
			this.label1.TabIndex = 22;
			this.label1.Text = "Default pixel windowing for new radiographs, both scanned and digital";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(13, 65);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(465, 24);
			this.label25.TabIndex = 15;
			this.label25.Text = "Suggested setting for scanning panos is Greyscale, 300 dpi.  For BWs, 400dpi.";
			// 
			// slider
			// 
			this.slider.Location = new System.Drawing.Point(14, 38);
			this.slider.MaxVal = 128;
			this.slider.Name = "slider";
			this.slider.Size = new System.Drawing.Size(194, 14);
			this.slider.TabIndex = 21;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(20, 353);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(484, 43);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Photos";
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(14, 18);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(464, 20);
			this.label37.TabIndex = 15;
			this.label37.Text = "Suggested setting for scanning photos is Color, 300 dpi.";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.checkBinned);
			this.groupBox4.Controls.Add(this.comboType);
			this.groupBox4.Controls.Add(this.upDownPort);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.upDownExposure);
			this.groupBox4.Location = new System.Drawing.Point(21, 402);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(484, 137);
			this.groupBox4.TabIndex = 21;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Suni Imaging";
			// 
			// checkBinned
			// 
			this.checkBinned.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBinned.Location = new System.Drawing.Point(81, 96);
			this.checkBinned.Name = "checkBinned";
			this.checkBinned.Size = new System.Drawing.Size(206, 17);
			this.checkBinned.TabIndex = 8;
			this.checkBinned.Text = "Binned (not normally used)";
			this.checkBinned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBinned.UseVisualStyleBackColor = true;
			// 
			// comboType
			// 
			this.comboType.FormattingEnabled = true;
			this.comboType.Location = new System.Drawing.Point(273, 69);
			this.comboType.Name = "comboType";
			this.comboType.Size = new System.Drawing.Size(37, 21);
			this.comboType.TabIndex = 7;
			this.comboType.Text = "D";
			// 
			// upDownPort
			// 
			this.upDownPort.Location = new System.Drawing.Point(273, 43);
			this.upDownPort.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.upDownPort.Name = "upDownPort";
			this.upDownPort.Size = new System.Drawing.Size(33, 20);
			this.upDownPort.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(239, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Sensor Port (place where sensor connects)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(14, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(253, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Sensor Type (last serial letter on sensor cable)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(239, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Exposure Level (1-7) for New Captures";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// upDownExposure
			// 
			this.upDownExposure.Location = new System.Drawing.Point(273, 17);
			this.upDownExposure.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.upDownExposure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.upDownExposure.Name = "upDownExposure";
			this.upDownExposure.Size = new System.Drawing.Size(33, 20);
			this.upDownExposure.TabIndex = 1;
			this.upDownExposure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// butSetScanner
			// 
			this.butSetScanner.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSetScanner.Autosize = true;
			this.butSetScanner.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetScanner.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetScanner.CornerRadius = 4F;
			this.butSetScanner.Location = new System.Drawing.Point(21, 12);
			this.butSetScanner.Name = "butSetScanner";
			this.butSetScanner.Size = new System.Drawing.Size(110, 24);
			this.butSetScanner.TabIndex = 22;
			this.butSetScanner.Text = "Set Default Scanner";
			this.butSetScanner.Click += new System.EventHandler(this.butSetScanner_Click);
			// 
			// butMounts
			// 
			this.butMounts.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butMounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butMounts.Autosize = true;
			this.butMounts.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMounts.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMounts.CornerRadius = 4F;
			this.butMounts.Location = new System.Drawing.Point(656, 12);
			this.butMounts.Name = "butMounts";
			this.butMounts.Size = new System.Drawing.Size(79, 24);
			this.butMounts.TabIndex = 20;
			this.butMounts.Text = "Setup Mounts";
			this.butMounts.Visible = false;
			this.butMounts.Click += new System.EventHandler(this.butMounts_Click);
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
			this.butCancel.Location = new System.Drawing.Point(660, 639);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(660, 601);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(260, 9);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(260, 34);
			this.label8.TabIndex = 23;
			this.label8.Text = "If this setting causes your scanner to malfunction, use the \"Show Scanner Options" +
    " Window\" instead";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormImagingSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(762, 687);
			this.Controls.Add(this.butSetScanner);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.butMounts);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormImagingSetup";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Imaging Setup";
			this.Load += new System.EventHandler(this.FormImagingSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupScanningOptions.ResumeLayout(false);
			this.groupScanningOptions.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.upDownPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownExposure)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImagingSetup_Load(object sender, System.EventArgs e) {
			comboType.Items.Add("B");
			comboType.Items.Add("D");
			checkScanDocSelectSource.Checked=ComputerPrefs.LocalComputer.ScanDocSelectSource;
			if(ComputerPrefs.LocalComputer.ScanDocShowOptions) {
				radioScanDocShowOptions.Checked=true;
				radioScanDocUseOptionsBelow.Checked=false;
				groupScanningOptions.Enabled=false;
			}
			else {
				radioScanDocShowOptions.Checked=false;
				radioScanDocUseOptionsBelow.Checked=true;
				groupScanningOptions.Enabled=true;
			}
			checkScanDocDuplex.Checked=ComputerPrefs.LocalComputer.ScanDocDuplex;
			checkScanDocGrayscale.Checked=ComputerPrefs.LocalComputer.ScanDocGrayscale;
			textScanDocResolution.Text=ComputerPrefs.LocalComputer.ScanDocResolution.ToString();
			textScanDocQuality.Text=ComputerPrefs.LocalComputer.ScanDocQuality.ToString();
			//textScanDocQuality.Text=PrefC.GetLong(PrefName.ScannerCompression).ToString();
			slider.MinVal=PrefC.GetInt(PrefName.ImageWindowingMin);
			slider.MaxVal=PrefC.GetInt(PrefName.ImageWindowingMax);
			upDownPort.Value=ComputerPrefs.LocalComputer.SensorPort;
			comboType.Text=ComputerPrefs.LocalComputer.SensorType;
			checkBinned.Checked=ComputerPrefs.LocalComputer.SensorBinned;
			int exposureLevelVal=ComputerPrefs.LocalComputer.SensorExposure;
			if(exposureLevelVal<(int)upDownExposure.Minimum || exposureLevelVal>(int)upDownExposure.Maximum){
				exposureLevelVal=(int)upDownExposure.Minimum;//Play it safe with the default exposure.
			}
			upDownExposure.Value=exposureLevelVal;
			//checkScanDocShowOptions.Checked=PrefC.GetBool(PrefName.ScannerSuppressDialog);
			//textScanDocResolution.Text=PrefC.GetString(PrefName.ScannerResolution);
		}

		private void radioScanDocShowOptions_CheckedChanged(object sender,EventArgs e) {
			if(radioScanDocShowOptions.Checked) {
				groupScanningOptions.Enabled=false;
			}
			else {
				groupScanningOptions.Enabled=true;
			}
		}

		private void butMounts_Click(object sender,EventArgs e) {
			FormMountDefs FormM=new FormMountDefs();
			FormM.ShowDialog();
		}

		private void butSetScanner_Click(object sender,EventArgs e) {
			xImageDeviceManager.Obfuscator.ActivateEZTwain();
			EZTwain.SelectImageSource(this.Handle);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textScanDocQuality.errorProvider1.GetError(textScanDocQuality)!=""
				|| textScanDocResolution.errorProvider1.GetError(textScanDocResolution)!="")
			{
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			ComputerPrefs.LocalComputer.ScanDocSelectSource=checkScanDocSelectSource.Checked;
			ComputerPrefs.LocalComputer.ScanDocShowOptions=radioScanDocShowOptions.Checked;
			ComputerPrefs.LocalComputer.ScanDocDuplex=checkScanDocDuplex.Checked;
			ComputerPrefs.LocalComputer.ScanDocGrayscale=checkScanDocGrayscale.Checked;
			ComputerPrefs.LocalComputer.ScanDocResolution=PIn.Int(textScanDocResolution.Text);
			ComputerPrefs.LocalComputer.ScanDocQuality=PIn.Byte(textScanDocQuality.Text);
			//Prefs.UpdateLong(PrefName.ScannerCompression,PIn.Long(textScanDocQuality.Text));
			Prefs.UpdateLong(PrefName.ImageWindowingMin,slider.MinVal);
			Prefs.UpdateLong(PrefName.ImageWindowingMax,slider.MaxVal);
			//Prefs.UpdateBool(PrefName.ScannerSuppressDialog,checkScanDocShowOptions.Checked);
			//Prefs.UpdateLong(PrefName.ScannerResolution,PIn.Long(textScanDocResolution.Text));
			ComputerPrefs.LocalComputer.SensorType=comboType.Text;
			ComputerPrefs.LocalComputer.SensorPort=(int)upDownPort.Value;
			ComputerPrefs.LocalComputer.SensorExposure=(int)upDownExposure.Value;
			ComputerPrefs.LocalComputer.SensorBinned=checkBinned.Checked;
			ComputerPrefs.Update(ComputerPrefs.LocalComputer);
			DataValid.SetInvalid(InvalidType.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


		
	}
}
