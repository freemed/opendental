using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class FormClaimCanadian : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private OpenDental.ValidDate textAccidentDate;
		private System.Windows.Forms.Label label23;
		///<summary>Set this externally before opening claim.  The only field we need is accident date.</summary>
		public Claim ClaimCur;
		private GroupBox groupBox1;
		private Label label1;
		private TextBox textReferralProvider;
		private ComboBox comboReferralReason;
		private GroupBox groupBox3;
		private ListBox listEligibilityCode;
		private Label label5;
		private TextBox textSchoolName;
		private GroupBox groupBox4;
		private CheckBox checkImages;
		private CheckBox checkXrays;
		private CheckBox checkModels;
		private CheckBox checkCorrespondence;
		private CheckBox checkEmail;
		private GroupBox groupBox5;
		private RadioButton radioSecondaryYes;
		private RadioButton radioSecondaryX;
		private RadioButton radioSecondaryNo;
		private GroupBox groupBox2;
		private RadioButton radioMaxProsthX;
		private RadioButton radioMaxProsthNo;
		private RadioButton radioMaxProsthYes;
		private Label label3;
		private ValidDate textDateInitialUpper;
		private Label label6;
		private ComboBox comboMaxProsthMaterial;
		private Label label7;
		private Label label2;
		private ListBox listPayeeCode;
		private Label label8;
		private GroupBox groupBox6;
		private Label label9;
		private ValidDate textDateInitialLower;
		private Label label10;
		private ComboBox comboMandProsthMaterial;
		private RadioButton radioMandProsthX;
		private RadioButton radioMandProsthNo;
		private RadioButton radioMandProsthYes;
		private Label label11;
		private Label label12;
		private GroupBox groupBox7;
		private ListBox listMissing;
		private Label label14;
		private Label label13;
		///<summary>The current CanadianClaim that we are working with in this window.</summary>
		public CanadianClaim CanCur;
		private List<CanadianExtract> MissingList;
	
		///<summary></summary>
		public FormClaimCanadian(){
			InitializeComponent();
			Lan.F(this);
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
			OpenDental.UI.Button butAddMissing;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimCanadian));
			OpenDental.UI.Button butDeleteMissing;
			OpenDental.UI.Button butEditMissing;
			this.label23 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboReferralReason = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textReferralProvider = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.listEligibilityCode = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textSchoolName = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.checkImages = new System.Windows.Forms.CheckBox();
			this.checkXrays = new System.Windows.Forms.CheckBox();
			this.checkModels = new System.Windows.Forms.CheckBox();
			this.checkCorrespondence = new System.Windows.Forms.CheckBox();
			this.checkEmail = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.radioSecondaryX = new System.Windows.Forms.RadioButton();
			this.radioSecondaryNo = new System.Windows.Forms.RadioButton();
			this.radioSecondaryYes = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textDateInitialUpper = new OpenDental.ValidDate();
			this.label6 = new System.Windows.Forms.Label();
			this.comboMaxProsthMaterial = new System.Windows.Forms.ComboBox();
			this.radioMaxProsthX = new System.Windows.Forms.RadioButton();
			this.radioMaxProsthNo = new System.Windows.Forms.RadioButton();
			this.radioMaxProsthYes = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.listPayeeCode = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textDateInitialLower = new OpenDental.ValidDate();
			this.label10 = new System.Windows.Forms.Label();
			this.comboMandProsthMaterial = new System.Windows.Forms.ComboBox();
			this.radioMandProsthX = new System.Windows.Forms.RadioButton();
			this.radioMandProsthNo = new System.Windows.Forms.RadioButton();
			this.radioMandProsthYes = new System.Windows.Forms.RadioButton();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.listMissing = new System.Windows.Forms.ListBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textAccidentDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			butAddMissing = new OpenDental.UI.Button();
			butDeleteMissing = new OpenDental.UI.Button();
			butEditMissing = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.SuspendLayout();
			// 
			// butAddMissing
			// 
			butAddMissing.AdjustImageLocation = new System.Drawing.Point(0,0);
			butAddMissing.Autosize = true;
			butAddMissing.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butAddMissing.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butAddMissing.CornerRadius = 4F;
			butAddMissing.Image = global::OpenDental.Properties.Resources.Add;
			resources.ApplyResources(butAddMissing,"butAddMissing");
			butAddMissing.Name = "butAddMissing";
			butAddMissing.Click += new System.EventHandler(this.butAddMissing_Click);
			// 
			// butDeleteMissing
			// 
			butDeleteMissing.AdjustImageLocation = new System.Drawing.Point(0,0);
			butDeleteMissing.Autosize = true;
			butDeleteMissing.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butDeleteMissing.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butDeleteMissing.CornerRadius = 4F;
			butDeleteMissing.Image = global::OpenDental.Properties.Resources.deleteX;
			resources.ApplyResources(butDeleteMissing,"butDeleteMissing");
			butDeleteMissing.Name = "butDeleteMissing";
			butDeleteMissing.Click += new System.EventHandler(this.butDeleteMissing_Click);
			// 
			// butEditMissing
			// 
			butEditMissing.AdjustImageLocation = new System.Drawing.Point(0,0);
			butEditMissing.Autosize = true;
			butEditMissing.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butEditMissing.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butEditMissing.CornerRadius = 4F;
			butEditMissing.Image = global::OpenDental.Properties.Resources.editPencil;
			resources.ApplyResources(butEditMissing,"butEditMissing");
			butEditMissing.Name = "butEditMissing";
			butEditMissing.Click += new System.EventHandler(this.butEditMissing_Click);
			// 
			// label23
			// 
			resources.ApplyResources(this.label23,"label23");
			this.label23.Name = "label23";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.comboReferralReason);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textReferralProvider);
			resources.ApplyResources(this.groupBox1,"groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			resources.ApplyResources(this.label2,"label2");
			this.label2.Name = "label2";
			// 
			// comboReferralReason
			// 
			this.comboReferralReason.FormattingEnabled = true;
			resources.ApplyResources(this.comboReferralReason,"comboReferralReason");
			this.comboReferralReason.Name = "comboReferralReason";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1,"label1");
			this.label1.Name = "label1";
			// 
			// textReferralProvider
			// 
			resources.ApplyResources(this.textReferralProvider,"textReferralProvider");
			this.textReferralProvider.Name = "textReferralProvider";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.listEligibilityCode);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.textSchoolName);
			resources.ApplyResources(this.groupBox3,"groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// label12
			// 
			resources.ApplyResources(this.label12,"label12");
			this.label12.Name = "label12";
			// 
			// listEligibilityCode
			// 
			this.listEligibilityCode.FormattingEnabled = true;
			resources.ApplyResources(this.listEligibilityCode,"listEligibilityCode");
			this.listEligibilityCode.Name = "listEligibilityCode";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5,"label5");
			this.label5.Name = "label5";
			// 
			// textSchoolName
			// 
			resources.ApplyResources(this.textSchoolName,"textSchoolName");
			this.textSchoolName.Name = "textSchoolName";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.checkImages);
			this.groupBox4.Controls.Add(this.checkXrays);
			this.groupBox4.Controls.Add(this.checkModels);
			this.groupBox4.Controls.Add(this.checkCorrespondence);
			this.groupBox4.Controls.Add(this.checkEmail);
			resources.ApplyResources(this.groupBox4,"groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			// 
			// checkImages
			// 
			resources.ApplyResources(this.checkImages,"checkImages");
			this.checkImages.Name = "checkImages";
			this.checkImages.UseVisualStyleBackColor = true;
			// 
			// checkXrays
			// 
			resources.ApplyResources(this.checkXrays,"checkXrays");
			this.checkXrays.Name = "checkXrays";
			this.checkXrays.UseVisualStyleBackColor = true;
			// 
			// checkModels
			// 
			resources.ApplyResources(this.checkModels,"checkModels");
			this.checkModels.Name = "checkModels";
			this.checkModels.UseVisualStyleBackColor = true;
			// 
			// checkCorrespondence
			// 
			resources.ApplyResources(this.checkCorrespondence,"checkCorrespondence");
			this.checkCorrespondence.Name = "checkCorrespondence";
			this.checkCorrespondence.UseVisualStyleBackColor = true;
			// 
			// checkEmail
			// 
			resources.ApplyResources(this.checkEmail,"checkEmail");
			this.checkEmail.Name = "checkEmail";
			this.checkEmail.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.radioSecondaryX);
			this.groupBox5.Controls.Add(this.radioSecondaryNo);
			this.groupBox5.Controls.Add(this.radioSecondaryYes);
			resources.ApplyResources(this.groupBox5,"groupBox5");
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			// 
			// radioSecondaryX
			// 
			resources.ApplyResources(this.radioSecondaryX,"radioSecondaryX");
			this.radioSecondaryX.Name = "radioSecondaryX";
			this.radioSecondaryX.TabStop = true;
			this.radioSecondaryX.UseVisualStyleBackColor = true;
			// 
			// radioSecondaryNo
			// 
			resources.ApplyResources(this.radioSecondaryNo,"radioSecondaryNo");
			this.radioSecondaryNo.Name = "radioSecondaryNo";
			this.radioSecondaryNo.TabStop = true;
			this.radioSecondaryNo.UseVisualStyleBackColor = true;
			// 
			// radioSecondaryYes
			// 
			resources.ApplyResources(this.radioSecondaryYes,"radioSecondaryYes");
			this.radioSecondaryYes.Name = "radioSecondaryYes";
			this.radioSecondaryYes.TabStop = true;
			this.radioSecondaryYes.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.textDateInitialUpper);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.comboMaxProsthMaterial);
			this.groupBox2.Controls.Add(this.radioMaxProsthX);
			this.groupBox2.Controls.Add(this.radioMaxProsthNo);
			this.groupBox2.Controls.Add(this.radioMaxProsthYes);
			this.groupBox2.Controls.Add(this.label3);
			resources.ApplyResources(this.groupBox2,"groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7,"label7");
			this.label7.Name = "label7";
			// 
			// textDateInitialUpper
			// 
			resources.ApplyResources(this.textDateInitialUpper,"textDateInitialUpper");
			this.textDateInitialUpper.Name = "textDateInitialUpper";
			// 
			// label6
			// 
			resources.ApplyResources(this.label6,"label6");
			this.label6.Name = "label6";
			// 
			// comboMaxProsthMaterial
			// 
			this.comboMaxProsthMaterial.FormattingEnabled = true;
			resources.ApplyResources(this.comboMaxProsthMaterial,"comboMaxProsthMaterial");
			this.comboMaxProsthMaterial.Name = "comboMaxProsthMaterial";
			// 
			// radioMaxProsthX
			// 
			resources.ApplyResources(this.radioMaxProsthX,"radioMaxProsthX");
			this.radioMaxProsthX.Name = "radioMaxProsthX";
			this.radioMaxProsthX.TabStop = true;
			this.radioMaxProsthX.UseVisualStyleBackColor = true;
			// 
			// radioMaxProsthNo
			// 
			resources.ApplyResources(this.radioMaxProsthNo,"radioMaxProsthNo");
			this.radioMaxProsthNo.Name = "radioMaxProsthNo";
			this.radioMaxProsthNo.TabStop = true;
			this.radioMaxProsthNo.UseVisualStyleBackColor = true;
			// 
			// radioMaxProsthYes
			// 
			resources.ApplyResources(this.radioMaxProsthYes,"radioMaxProsthYes");
			this.radioMaxProsthYes.Name = "radioMaxProsthYes";
			this.radioMaxProsthYes.TabStop = true;
			this.radioMaxProsthYes.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3,"label3");
			this.label3.Name = "label3";
			// 
			// listPayeeCode
			// 
			this.listPayeeCode.FormattingEnabled = true;
			resources.ApplyResources(this.listPayeeCode,"listPayeeCode");
			this.listPayeeCode.Name = "listPayeeCode";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8,"label8");
			this.label8.Name = "label8";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label9);
			this.groupBox6.Controls.Add(this.textDateInitialLower);
			this.groupBox6.Controls.Add(this.label10);
			this.groupBox6.Controls.Add(this.comboMandProsthMaterial);
			this.groupBox6.Controls.Add(this.radioMandProsthX);
			this.groupBox6.Controls.Add(this.radioMandProsthNo);
			this.groupBox6.Controls.Add(this.radioMandProsthYes);
			this.groupBox6.Controls.Add(this.label11);
			resources.ApplyResources(this.groupBox6,"groupBox6");
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			// 
			// label9
			// 
			resources.ApplyResources(this.label9,"label9");
			this.label9.Name = "label9";
			// 
			// textDateInitialLower
			// 
			resources.ApplyResources(this.textDateInitialLower,"textDateInitialLower");
			this.textDateInitialLower.Name = "textDateInitialLower";
			// 
			// label10
			// 
			resources.ApplyResources(this.label10,"label10");
			this.label10.Name = "label10";
			// 
			// comboMandProsthMaterial
			// 
			this.comboMandProsthMaterial.FormattingEnabled = true;
			resources.ApplyResources(this.comboMandProsthMaterial,"comboMandProsthMaterial");
			this.comboMandProsthMaterial.Name = "comboMandProsthMaterial";
			// 
			// radioMandProsthX
			// 
			resources.ApplyResources(this.radioMandProsthX,"radioMandProsthX");
			this.radioMandProsthX.Name = "radioMandProsthX";
			this.radioMandProsthX.TabStop = true;
			this.radioMandProsthX.UseVisualStyleBackColor = true;
			// 
			// radioMandProsthNo
			// 
			resources.ApplyResources(this.radioMandProsthNo,"radioMandProsthNo");
			this.radioMandProsthNo.Name = "radioMandProsthNo";
			this.radioMandProsthNo.TabStop = true;
			this.radioMandProsthNo.UseVisualStyleBackColor = true;
			// 
			// radioMandProsthYes
			// 
			resources.ApplyResources(this.radioMandProsthYes,"radioMandProsthYes");
			this.radioMandProsthYes.Name = "radioMandProsthYes";
			this.radioMandProsthYes.TabStop = true;
			this.radioMandProsthYes.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			resources.ApplyResources(this.label11,"label11");
			this.label11.Name = "label11";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(butAddMissing);
			this.groupBox7.Controls.Add(butDeleteMissing);
			this.groupBox7.Controls.Add(butEditMissing);
			this.groupBox7.Controls.Add(this.label13);
			this.groupBox7.Controls.Add(this.listMissing);
			this.groupBox7.Controls.Add(this.label14);
			resources.ApplyResources(this.groupBox7,"groupBox7");
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.TabStop = false;
			// 
			// label13
			// 
			resources.ApplyResources(this.label13,"label13");
			this.label13.Name = "label13";
			// 
			// listMissing
			// 
			this.listMissing.FormattingEnabled = true;
			resources.ApplyResources(this.listMissing,"listMissing");
			this.listMissing.Name = "listMissing";
			this.listMissing.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listMissing.DoubleClick += new System.EventHandler(this.listMissing_DoubleClick);
			// 
			// label14
			// 
			resources.ApplyResources(this.label14,"label14");
			this.label14.Name = "label14";
			// 
			// textAccidentDate
			// 
			resources.ApplyResources(this.textAccidentDate,"textAccidentDate");
			this.textAccidentDate.Name = "textAccidentDate";
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			resources.ApplyResources(this.butCancel,"butCancel");
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Name = "butCancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			resources.ApplyResources(this.butOK,"butOK");
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Name = "butOK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormClaimCanadian
			// 
			resources.ApplyResources(this,"$this");
			this.Controls.Add(this.groupBox7);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.listPayeeCode);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.textAccidentDate);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimCanadian";
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.FormClaimCanadian_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimCanadian_Load(object sender, System.EventArgs e) {
			comboReferralReason.Items.Add("none");//0. -1 never used
			comboReferralReason.Items.Add("Pathological Anomalies");//1
			comboReferralReason.Items.Add("Disabled (physical or mental)");
			comboReferralReason.Items.Add("Complexity of Treatment");
			comboReferralReason.Items.Add("Seizure Disorders");
			comboReferralReason.Items.Add("Extensive Surgery");
			comboReferralReason.Items.Add("Surgical Complexity");
			comboReferralReason.Items.Add("Rampant decay");
			comboReferralReason.Items.Add("Medical History (to provide details upon request)");
			comboReferralReason.Items.Add("Temporal Mandibular Joint Anomalies");
			comboReferralReason.Items.Add("Accidental Injury");
			comboReferralReason.Items.Add("Anaesthesia complications (local or general)");
			comboReferralReason.Items.Add("Developmental Anomalies");
			comboReferralReason.Items.Add("Behavioral Management");//13
			listEligibilityCode.Items.Add("Full-time student");//0
			listEligibilityCode.Items.Add("Disabled");
			listEligibilityCode.Items.Add("Disabled student");
			listEligibilityCode.Items.Add("Code not applicable");
			listPayeeCode.Items.Add("Pay the subscriber");//0
			listPayeeCode.Items.Add("Pay other third party");
			listPayeeCode.Items.Add("Reserved");
			listPayeeCode.Items.Add("Pay the dentist");
			comboMaxProsthMaterial.Items.Add("not applicable");//this always starts out selected. -1 never used.
			comboMaxProsthMaterial.Items.Add("Fixed bridge");
			comboMaxProsthMaterial.Items.Add("Maryland bridge");
			comboMaxProsthMaterial.Items.Add("Denture (Acrylic)");
			comboMaxProsthMaterial.Items.Add("Denture (Chrome Cobalt)");
			comboMaxProsthMaterial.Items.Add("Implant (Fixed)");
			comboMaxProsthMaterial.Items.Add("Implant (Removable)");
			comboMaxProsthMaterial.Items.Add("Crown");//7.  not an official type
			comboMandProsthMaterial.Items.Add("not applicable");//this always starts out selected. -1 never used.
			comboMandProsthMaterial.Items.Add("Fixed bridge");
			comboMandProsthMaterial.Items.Add("Maryland bridge");
			comboMandProsthMaterial.Items.Add("Denture (Acrylic)");
			comboMandProsthMaterial.Items.Add("Denture (Chrome Cobalt)");
			comboMandProsthMaterial.Items.Add("Implant (Fixed)");
			comboMandProsthMaterial.Items.Add("Implant (Removable)");
			comboMandProsthMaterial.Items.Add("Crown");
			//Load data for this claim---------------------------------------------------------------------------------------------
			if(CanCur.MaterialsForwarded.Contains("E")){
				checkEmail.Checked=true;
			}
			if(CanCur.MaterialsForwarded.Contains("C")) {
				checkCorrespondence.Checked=true;
			}
			if(CanCur.MaterialsForwarded.Contains("M")) {
				checkModels.Checked=true;
			}
			if(CanCur.MaterialsForwarded.Contains("X")) {
				checkXrays.Checked=true;
			}
			if(CanCur.MaterialsForwarded.Contains("I")) {
				checkImages.Checked=true;
			}
			//this starts out blank, but they might be editing an existing claim
			if(CanCur.SecondaryCoverage=="Y") {
				radioSecondaryYes.Checked=true;
			}
			if(CanCur.SecondaryCoverage=="N") {
				radioSecondaryNo.Checked=true;
			}
			if(CanCur.SecondaryCoverage=="X") {
				radioSecondaryX.Checked=true;
			}
			textReferralProvider.Text=CanCur.ReferralProviderNum;
			comboReferralReason.SelectedIndex=CanCur.ReferralReason;
			listEligibilityCode.SelectedIndex=CanCur.EligibilityCode-1;
			//eg if code is 0, then -1 selected. If code is 1(full-time student), then 0 is selected
			textSchoolName.Text=CanCur.SchoolName;
			listPayeeCode.SelectedIndex=CanCur.PayeeCode-1;
			if(ClaimCur.AccidentDate.Year<1900){
				textAccidentDate.Text="";
			}
			else{
				textAccidentDate.Text=ClaimCur.AccidentDate.ToShortDateString();
			}
			/*if(CanCur.CardSequenceNumber==0){
				textCardSequenceNumber.Text="";
			}
			else{
				textCardSequenceNumber.Text=CanCur.CardSequenceNumber.ToString();
			}*/
			//max prosth-----------------------------------------------------------------------------------------------------
			if(CanCur.IsInitialUpper=="Y") {
				radioMaxProsthYes.Checked=true;
			}
			if(CanCur.IsInitialUpper=="N") {
				radioMaxProsthNo.Checked=true;
			}
			if(CanCur.IsInitialUpper=="X") {
				radioMaxProsthX.Checked=true;
			}
			if(CanCur.DateInitialUpper.Year<1880) {
				textDateInitialUpper.Text="";
			}
			else {
				textDateInitialUpper.Text=CanCur.DateInitialUpper.ToShortDateString();
			}
			comboMaxProsthMaterial.SelectedIndex=CanCur.MaxProsthMaterial;
			//mand prosth-----------------------------------------------------------------------------------------------------
			if(CanCur.IsInitialLower=="Y") {
				radioMandProsthYes.Checked=true;
			}
			if(CanCur.IsInitialLower=="N") {
				radioMandProsthNo.Checked=true;
			}
			if(CanCur.IsInitialLower=="X") {
				radioMandProsthX.Checked=true;
			}
			if(CanCur.DateInitialLower.Year<1880) {
				textDateInitialLower.Text="";
			}
			else {
				textDateInitialLower.Text=CanCur.DateInitialLower.ToShortDateString();
			}
			comboMandProsthMaterial.SelectedIndex=CanCur.MandProsthMaterial;
			//Missing teeth--------------------------------------------------------------------------------------------------
			MissingList=CanadianExtracts.GetForClaim(CanCur.ClaimNum);
			FillMissing();
		}

		///<summary>Does not get any data from db.  Only updates display based on MissingList.</summary>
		private void FillMissing(){
			MissingList.Sort(CanadianExtracts.CompareByToothNum);
			listMissing.Items.Clear();
			string display="";
			for(int i=0;i<MissingList.Count;i++){
				display=Tooth.ToInternat(MissingList[i].ToothNum);
				if(MissingList[i].DateExtraction.Year>1880){
					display+="  "+MissingList[i].DateExtraction.ToShortDateString();
				}
				listMissing.Items.Add(display);
			}
		}

		private void listMissing_DoubleClick(object sender,EventArgs e) {
			if(listMissing.SelectedIndices.Count==0) {
				return;
			}
			FormCanadianExtract FormC=new FormCanadianExtract();
			FormC.Cur=MissingList[listMissing.SelectedIndices[0]];//we really do want to edit the item is the list.
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK) {
				return;
			}
			FillMissing();
		}

		private void butAddMissing_Click(object sender,EventArgs e) {
			FormCanadianExtract FormC=new FormCanadianExtract();
			FormC.Cur=new CanadianExtract();
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK){
				return;
			}
			//make sure tooth number has not already been added
			for(int i=0;i<MissingList.Count;i++){
				if(MissingList[i].ToothNum==FormC.Cur.ToothNum){
					MsgBox.Show(this,"Tooth has already been added previously.");
					return;
				}
			}
			MissingList.Add(FormC.Cur);
			FillMissing();
		}

		private void butEditMissing_Click(object sender,EventArgs e) {
			if(listMissing.SelectedIndices.Count==0){
				MsgBox.Show(this,"Please select one or more items in the list first.");
				return;
			}
			if(listMissing.SelectedIndices.Count==1){
				FormCanadianExtract FormC=new FormCanadianExtract();
				FormC.Cur=MissingList[listMissing.SelectedIndices[0]];//we really do want to edit the item is the list.
				FormC.ShowDialog();
				if(FormC.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else{//multiple
				FormCanadianExtract FormC=new FormCanadianExtract();
				FormC.Cur=MissingList[listMissing.SelectedIndices[0]].Copy();
				FormC.IsMulti=true;
				FormC.ShowDialog();
				if(FormC.DialogResult!=DialogResult.OK) {
					return;
				}
				for(int i=0;i<listMissing.SelectedIndices.Count;i++){
					MissingList[listMissing.SelectedIndices[i]].DateExtraction=FormC.Cur.DateExtraction;
				}
			}
			FillMissing();
		}

		private void butDeleteMissing_Click(object sender,EventArgs e) {
			if(listMissing.SelectedIndices.Count==0) {
				MsgBox.Show(this,"Please select one or more items in the list first.");
				return;
			}
			if(!MsgBox.Show(this,true,"Delete selected items?")){
				return;
			}
			//for(int i=0;i<listMissing.SelectedIndices.Count;i++){
			//	MissingList.Remove(
			//}
			for(int i=listMissing.SelectedIndices.Count-1;i>=0;i--){//go backwards
				MissingList.RemoveAt(listMissing.SelectedIndices[i]);
			}
			FillMissing();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textAccidentDate.errorProvider1.GetError(textAccidentDate)!=""
				|| textDateInitialUpper.errorProvider1.GetError(textDateInitialUpper)!=""
				|| textDateInitialLower.errorProvider1.GetError(textDateInitialLower)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
#region Warnings
			string warning="";
			if(!radioSecondaryYes.Checked && !radioSecondaryNo.Checked && !radioSecondaryX.Checked){
				if(warning!=""){
					warning+="\r\n";
				}
				warning+="Secondary coverage not indicated.";
			}
			if(textReferralProvider.Text!="" && comboReferralReason.SelectedIndex==0){
				if(warning!=""){
					warning+="\r\n";
				}
				warning+="Referral reason is required if provider indicated.";
			}
			if(textReferralProvider.Text=="" && comboReferralReason.SelectedIndex!=0){
				if(warning!=""){
					warning+="\r\n";
				}
				warning+="Referring provider required if referring reason is indicated.";
			}
			if(listEligibilityCode.SelectedIndex==0 && textSchoolName.Text=="") {
				if(warning!="") {
					warning+="\r\n";
				}
				warning+="School should be entered if full-time student.";
			}
			if(listEligibilityCode.SelectedIndex==-1) {
				if(warning!="") {
					warning+="\r\n";
				}
				warning+="Eligibility code is required.";
			}
			if(listPayeeCode.SelectedIndex==-1) {
				if(warning!="") {
					warning+="\r\n";
				}
				warning+="Payee not selected.";
			}
			if(textAccidentDate.Text!="") {
				if(PIn.PDate(textAccidentDate.Text)>DateTime.Today) {
					if(warning!="") {
						warning+="\r\n";
					}
					warning+="Accident date must be in the past.";
				}
				if(PIn.PDate(textAccidentDate.Text).Year<1980) {
					if(warning!="") {
						warning+="\r\n";
					}
					warning+="Accident date is not reasonable.";
				}
			}
			//Max prosth----------------------------------------------------------------------------------------------------------
			if(!radioMaxProsthYes.Checked && !radioMaxProsthNo.Checked && !radioMaxProsthX.Checked){
				if(warning!=""){
					warning+="\r\n";
				}
				warning+="Max prosth not indicated.";
			}
			if(textDateInitialUpper.Text!="") {
				if(PIn.PDate(textDateInitialUpper.Text)>DateTime.Today) {
					if(warning!="") {
						warning+="\r\n";
					}
					warning+="Initial max date must be in the past.";
				}
				if(PIn.PDate(textDateInitialUpper.Text).Year<1900) {
					if(warning!="") {
						warning+="\r\n";
					}
					warning+="Initial max date is not reasonable.";
				}
			}
			if(radioMaxProsthNo.Checked){
				if(textDateInitialUpper.Text==""){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Max initial date is required if 'no' is checked.";
				}
				if(comboMaxProsthMaterial.SelectedIndex==0){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Max prosth material must be indicated";// (unless for a crown).";
				}
			}
			if(comboMaxProsthMaterial.SelectedIndex!=0 && radioMaxProsthX.Checked){
				if(warning!="") {
					warning+="\r\n";
				}
				warning+="Max prosth should not have a material selected.";
			}
			//Mand prosth----------------------------------------------------------------------------------------------------------
			if(!radioMandProsthYes.Checked && !radioMandProsthNo.Checked && !radioMandProsthX.Checked){
				if(warning!=""){
					warning+="\r\n";
				}
				warning+="Mand prosth not indicated.";
			}
			if(textDateInitialLower.Text!=""){
				if(PIn.PDate(textDateInitialLower.Text)>DateTime.Today){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Initial mand date must be in the past.";
				}
				if(PIn.PDate(textDateInitialLower.Text).Year<1900){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Initial mand date is not reasonable.";
				}
			}
			if(radioMandProsthNo.Checked){
				if(textDateInitialLower.Text==""){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Mand initial date is required if 'no' is checked.";
				}
				if(comboMandProsthMaterial.SelectedIndex==0){
					if(warning!=""){
						warning+="\r\n";
					}
					warning+="Mand prosth material must be indicated";// (unless for a crown).";
				}
			}
			if(comboMandProsthMaterial.SelectedIndex!=0 && radioMandProsthX.Checked) {
				if(warning!="") {
					warning+="\r\n";
				}
				warning+="Mand prosth should not have a material selected.";
			}
			//missing teeth------------------------------------------------------------------------------
			if(radioMandProsthYes.Checked){
				if(comboMandProsthMaterial.SelectedIndex!=7){//if not a crown
					if(MissingList.Count==0){
						if(warning!="") {
							warning+="\r\n";
						}
						warning+="Missing teeth need to be entered.";
					}
					else{
						bool missingDatesPresent=false;
						for(int i=0;i<MissingList.Count;i++){
							if(MissingList[i].DateExtraction.Year>1880){
								missingDatesPresent=true;
							}
						}
						if(!missingDatesPresent){
							if(warning!="") {
								warning+="\r\n";
							}
							warning+="Dates need to be entered for missing teeth.";
						}
					}
				}
			}
			if(radioMaxProsthYes.Checked) {
				if(comboMaxProsthMaterial.SelectedIndex!=7) {//if not a crown
					if(MissingList.Count==0) {
						if(warning!="") {
							warning+="\r\n";
						}
						warning+="Missing teeth need to be entered.";
					}
					else {
						bool missingDatesPresent=false;
						for(int i=0;i<MissingList.Count;i++) {
							if(MissingList[i].DateExtraction.Year>1880) {
								missingDatesPresent=true;
							}
						}
						if(!missingDatesPresent) {
							if(warning!="") {
								warning+="\r\n";
							}
							warning+="Dates need to be entered for missing teeth.";
						}
					}
				}
			}
			if(warning!=""){
				DialogResult result=MessageBox.Show("Warnings:\r\n"+warning+"\r\nDo you wish to continue anyway?","",
					MessageBoxButtons.OKCancel);
				if(result!=DialogResult.OK){
					return;
				}
			}
#endregion Warnings
			CanCur.MaterialsForwarded="";
			if(checkEmail.Checked){
				CanCur.MaterialsForwarded+="E";
			}
			if(checkCorrespondence.Checked) {
				CanCur.MaterialsForwarded+="C";
			}
			if(checkModels.Checked) {
				CanCur.MaterialsForwarded+="M";
			}
			if(checkXrays.Checked) {
				CanCur.MaterialsForwarded+="X";
			}
			if(checkImages.Checked) {
				CanCur.MaterialsForwarded+="I";
			}
			if(radioSecondaryYes.Checked){
				CanCur.SecondaryCoverage="Y";
			}
			if(radioSecondaryNo.Checked) {
				CanCur.SecondaryCoverage="N";
			}
			if(radioSecondaryX.Checked) {
				CanCur.SecondaryCoverage="X";
			}
			CanCur.ReferralProviderNum=textReferralProvider.Text;
			CanCur.ReferralReason=comboReferralReason.SelectedIndex;
			CanCur.EligibilityCode=listEligibilityCode.SelectedIndex+1;
			CanCur.SchoolName=textSchoolName.Text;
			CanCur.PayeeCode=listPayeeCode.SelectedIndex+1;
			ClaimCur.AccidentDate=PIn.PDate(textAccidentDate.Text);
			//CanCur.CardSequenceNumber=PIn.PInt(textCardSequenceNumber.Text);
			//max prosth-----------------------------------------------------------------------------------------------------
			if(radioMaxProsthYes.Checked) {
				CanCur.IsInitialUpper="Y";
			}
			if(radioMaxProsthNo.Checked) {
				CanCur.IsInitialUpper="N";
			}
			if(radioMandProsthX.Checked) {
				CanCur.IsInitialUpper="X";
			}
			CanCur.DateInitialUpper=PIn.PDate(textDateInitialUpper.Text);
			CanCur.MaxProsthMaterial=comboMaxProsthMaterial.SelectedIndex;
			//mand prosth-----------------------------------------------------------------------------------------------------
			if(radioMandProsthYes.Checked) {
				CanCur.IsInitialLower="Y";
			}
			if(radioMandProsthNo.Checked) {
				CanCur.IsInitialLower="N";
			}
			if(radioMandProsthX.Checked) {
				CanCur.IsInitialLower="X";
			}
			CanCur.DateInitialLower=PIn.PDate(textDateInitialLower.Text);
			CanCur.MandProsthMaterial=comboMandProsthMaterial.SelectedIndex;
			CanadianExtracts.UpdateForClaim(ClaimCur.ClaimNum,MissingList);
			CanadianClaims.Update(CanCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		
		
	}
}
