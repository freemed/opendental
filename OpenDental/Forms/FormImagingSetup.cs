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
		private System.Windows.Forms.Label label13;
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
		private ValidNum textDoc;
		private ComboBox comboType;
		private CheckBox checkBinned;
		private ComputerPref computerPrefs;

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
				this.label12,
				this.label13,
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
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormImagingSetup));
			this.label12=new System.Windows.Forms.Label();
			this.label13=new System.Windows.Forms.Label();
			this.groupBox1=new System.Windows.Forms.GroupBox();
			this.groupBox2=new System.Windows.Forms.GroupBox();
			this.label1=new System.Windows.Forms.Label();
			this.label25=new System.Windows.Forms.Label();
			this.groupBox3=new System.Windows.Forms.GroupBox();
			this.label37=new System.Windows.Forms.Label();
			this.groupBox4=new System.Windows.Forms.GroupBox();
			this.checkBinned=new System.Windows.Forms.CheckBox();
			this.comboType=new System.Windows.Forms.ComboBox();
			this.upDownPort=new System.Windows.Forms.NumericUpDown();
			this.label4=new System.Windows.Forms.Label();
			this.label3=new System.Windows.Forms.Label();
			this.label2=new System.Windows.Forms.Label();
			this.upDownExposure=new System.Windows.Forms.NumericUpDown();
			this.butMounts=new OpenDental.UI.Button();
			this.slider=new OpenDental.UI.ContrWindowingSlider();
			this.textDoc=new OpenDental.ValidNum();
			this.butCancel=new OpenDental.UI.Button();
			this.butOK=new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDownPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownExposure)).BeginInit();
			this.SuspendLayout();
			// 
			// label12
			// 
			this.label12.Location=new System.Drawing.Point(14,15);
			this.label12.Name="label12";
			this.label12.Size=new System.Drawing.Size(464,15);
			this.label12.TabIndex=14;
			this.label12.Text="JPEG Compression - Quality After Scanning, 0-100. 100=No compression.  Typical se"+
					"tting: 40.";
			this.label12.TextAlign=System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label13
			// 
			this.label13.Location=new System.Drawing.Point(14,58);
			this.label13.Name="label13";
			this.label13.Size=new System.Drawing.Size(464,19);
			this.label13.TabIndex=15;
			this.label13.Text="Suggested setting for scanning documents is Greyscale, 150 dpi.";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textDoc);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.FlatStyle=System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location=new System.Drawing.Point(20,13);
			this.groupBox1.Name="groupBox1";
			this.groupBox1.Size=new System.Drawing.Size(484,79);
			this.groupBox1.TabIndex=17;
			this.groupBox1.TabStop=false;
			this.groupBox1.Text="Documents";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.slider);
			this.groupBox2.FlatStyle=System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location=new System.Drawing.Point(20,98);
			this.groupBox2.Name="groupBox2";
			this.groupBox2.Size=new System.Drawing.Size(484,96);
			this.groupBox2.TabIndex=18;
			this.groupBox2.TabStop=false;
			this.groupBox2.Text="Radiographs";
			// 
			// label1
			// 
			this.label1.Location=new System.Drawing.Point(13,14);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(392,19);
			this.label1.TabIndex=22;
			this.label1.Text="Default pixel windowing for new radiographs";
			this.label1.TextAlign=System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label25
			// 
			this.label25.Location=new System.Drawing.Point(13,65);
			this.label25.Name="label25";
			this.label25.Size=new System.Drawing.Size(465,24);
			this.label25.TabIndex=15;
			this.label25.Text="Suggested setting for scanning panos is Greyscale, 300 dpi.  For BWs, 400dpi.";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.FlatStyle=System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location=new System.Drawing.Point(20,200);
			this.groupBox3.Name="groupBox3";
			this.groupBox3.Size=new System.Drawing.Size(484,43);
			this.groupBox3.TabIndex=19;
			this.groupBox3.TabStop=false;
			this.groupBox3.Text="Photos";
			// 
			// label37
			// 
			this.label37.Location=new System.Drawing.Point(14,18);
			this.label37.Name="label37";
			this.label37.Size=new System.Drawing.Size(464,20);
			this.label37.TabIndex=15;
			this.label37.Text="Suggested setting for scanning photos is Color, 300 dpi.";
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
			this.groupBox4.Location=new System.Drawing.Point(20,260);
			this.groupBox4.Name="groupBox4";
			this.groupBox4.Size=new System.Drawing.Size(484,115);
			this.groupBox4.TabIndex=21;
			this.groupBox4.TabStop=false;
			this.groupBox4.Text="XRay Imaging";
			// 
			// checkBinned
			// 
			this.checkBinned.AutoSize=true;
			this.checkBinned.Location=new System.Drawing.Point(242,83);
			this.checkBinned.Name="checkBinned";
			this.checkBinned.Size=new System.Drawing.Size(150,17);
			this.checkBinned.TabIndex=8;
			this.checkBinned.Text="Binned (not normally used)";
			this.checkBinned.UseVisualStyleBackColor=true;
			// 
			// comboType
			// 
			this.comboType.FormattingEnabled=true;
			this.comboType.Location=new System.Drawing.Point(242,39);
			this.comboType.Name="comboType";
			this.comboType.Size=new System.Drawing.Size(37,21);
			this.comboType.TabIndex=7;
			this.comboType.Text="D";
			// 
			// upDownPort
			// 
			this.upDownPort.Location=new System.Drawing.Point(14,83);
			this.upDownPort.Maximum=new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.upDownPort.Name="upDownPort";
			this.upDownPort.Size=new System.Drawing.Size(33,20);
			this.upDownPort.TabIndex=6;
			// 
			// label4
			// 
			this.label4.AutoSize=true;
			this.label4.Location=new System.Drawing.Point(11,66);
			this.label4.Name="label4";
			this.label4.Size=new System.Drawing.Size(210,13);
			this.label4.TabIndex=5;
			this.label4.Text="Sensor Port (place where sensor connects)";
			// 
			// label3
			// 
			this.label3.AutoSize=true;
			this.label3.Location=new System.Drawing.Point(239,20);
			this.label3.Name="label3";
			this.label3.Size=new System.Drawing.Size(229,13);
			this.label3.TabIndex=3;
			this.label3.Text="Sensor Type (last serical letter on sensor cable)";
			// 
			// label2
			// 
			this.label2.AutoSize=true;
			this.label2.Location=new System.Drawing.Point(11,20);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(189,13);
			this.label2.TabIndex=2;
			this.label2.Text="Exposure Level (1-7) for New Captures";
			// 
			// upDownExposure
			// 
			this.upDownExposure.Location=new System.Drawing.Point(14,39);
			this.upDownExposure.Maximum=new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.upDownExposure.Minimum=new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.upDownExposure.Name="upDownExposure";
			this.upDownExposure.Size=new System.Drawing.Size(33,20);
			this.upDownExposure.TabIndex=1;
			this.upDownExposure.Value=new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// butMounts
			// 
			this.butMounts.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butMounts.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Right)));
			this.butMounts.Autosize=true;
			this.butMounts.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMounts.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butMounts.CornerRadius=4F;
			this.butMounts.Location=new System.Drawing.Point(544,21);
			this.butMounts.Name="butMounts";
			this.butMounts.Size=new System.Drawing.Size(79,26);
			this.butMounts.TabIndex=20;
			this.butMounts.Text="Setup Mounts";
			this.butMounts.Click+=new System.EventHandler(this.butMounts_Click);
			// 
			// slider
			// 
			this.slider.Location=new System.Drawing.Point(14,38);
			this.slider.MaxVal=128;
			this.slider.Name="slider";
			this.slider.Size=new System.Drawing.Size(194,14);
			this.slider.TabIndex=21;
			// 
			// textDoc
			// 
			this.textDoc.Location=new System.Drawing.Point(16,35);
			this.textDoc.MaxVal=255;
			this.textDoc.MinVal=0;
			this.textDoc.Name="textDoc";
			this.textDoc.Size=new System.Drawing.Size(68,20);
			this.textDoc.TabIndex=20;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.DialogResult=System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location=new System.Drawing.Point(548,371);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,26);
			this.butCancel.TabIndex=3;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butOK.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize=true;
			this.butOK.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius=4F;
			this.butOK.Location=new System.Drawing.Point(548,333);
			this.butOK.Name="butOK";
			this.butOK.Size=new System.Drawing.Size(75,26);
			this.butOK.TabIndex=2;
			this.butOK.Text="&OK";
			this.butOK.Click+=new System.EventHandler(this.butOK_Click);
			// 
			// FormImagingSetup
			// 
			this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
			this.ClientSize=new System.Drawing.Size(650,422);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.butMounts);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox=false;
			this.MinimizeBox=false;
			this.Name="FormImagingSetup";
			this.ShowInTaskbar=false;
			this.SizeGripStyle=System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Imaging Setup";
			this.Load+=new System.EventHandler(this.FormImagingSetup_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDownPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownExposure)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImagingSetup_Load(object sender, System.EventArgs e) {
			comboType.Items.Add("B");
			comboType.Items.Add("D");
			textDoc.Text=PrefB.GetInt("ScannerCompression").ToString();
			slider.MinVal=PrefB.GetInt("ImageWindowingMin");
			slider.MaxVal=PrefB.GetInt("ImageWindowingMax");
			computerPrefs=ComputerPrefs.GetForLocalComputer();
			upDownPort.Value=computerPrefs.SensorPort;
			comboType.Text=computerPrefs.SensorType;
			checkBinned.Checked=computerPrefs.SensorBinned;
			int exposureLevelVal=computerPrefs.SensorExposure;
			if(exposureLevelVal<(int)upDownExposure.Minimum || exposureLevelVal>(int)upDownExposure.Maximum){
				exposureLevelVal=(int)upDownExposure.Minimum;//Play it safe with the default exposure.
			}
			upDownExposure.Value=exposureLevelVal;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textDoc.errorProvider1.GetError(textDoc)!=""){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			Prefs.UpdateInt("ScannerCompression",PIn.PInt(textDoc.Text));
			Prefs.UpdateInt("ImageWindowingMin",slider.MinVal);
			Prefs.UpdateInt("ImageWindowingMax",slider.MaxVal);
			computerPrefs.SensorType=comboType.Text;
			computerPrefs.SensorPort=(int)upDownPort.Value;
			computerPrefs.SensorExposure=(int)upDownExposure.Value;
			computerPrefs.SensorBinned=checkBinned.Checked;
			ComputerPrefs.Update(computerPrefs);
			DataValid.SetInvalid(InvalidTypes.Prefs);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butMounts_Click(object sender,EventArgs e) {
			FormMountDefs FormM=new FormMountDefs();
			FormM.ShowDialog();
		}


		
	}
}
