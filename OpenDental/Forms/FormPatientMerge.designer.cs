namespace OpenDental{
	partial class FormPatientMerge {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(FormPatientMerge));
			this.butMerge=new OpenDental.UI.Button();
			this.butCancel=new OpenDental.UI.Button();
			this.groupBox1=new System.Windows.Forms.GroupBox();
			this.butChangePatientInto=new OpenDental.UI.Button();
			this.textPatientNameInto=new System.Windows.Forms.TextBox();
			this.label2=new System.Windows.Forms.Label();
			this.label1=new System.Windows.Forms.Label();
			this.textPatientIDInto=new System.Windows.Forms.TextBox();
			this.groupBox2=new System.Windows.Forms.GroupBox();
			this.butRemoveSelectedPatientsFrom=new OpenDental.UI.Button();
			this.butAddPatientFrom=new OpenDental.UI.Button();
			this.gridMergeFromPatients=new OpenDental.UI.ODGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butMerge
			// 
			this.butMerge.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butMerge.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butMerge.Autosize=true;
			this.butMerge.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMerge.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butMerge.CornerRadius=4F;
			this.butMerge.Enabled=false;
			this.butMerge.Location=new System.Drawing.Point(554,431);
			this.butMerge.Name="butMerge";
			this.butMerge.Size=new System.Drawing.Size(75,24);
			this.butMerge.TabIndex=3;
			this.butMerge.Text="Merge";
			this.butMerge.Click+=new System.EventHandler(this.butMerge_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butCancel.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize=true;
			this.butCancel.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius=4F;
			this.butCancel.Location=new System.Drawing.Point(554,472);
			this.butCancel.Name="butCancel";
			this.butCancel.Size=new System.Drawing.Size(75,24);
			this.butCancel.TabIndex=2;
			this.butCancel.Text="&Cancel";
			this.butCancel.Click+=new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butChangePatientInto);
			this.groupBox1.Controls.Add(this.textPatientNameInto);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textPatientIDInto);
			this.groupBox1.Location=new System.Drawing.Point(12,12);
			this.groupBox1.Name="groupBox1";
			this.groupBox1.Size=new System.Drawing.Size(630,89);
			this.groupBox1.TabIndex=4;
			this.groupBox1.TabStop=false;
			this.groupBox1.Text="Patient to merge into. All patient data will be merged into this patient account."+
					"";
			// 
			// butChangePatientInto
			// 
			this.butChangePatientInto.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butChangePatientInto.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butChangePatientInto.Autosize=true;
			this.butChangePatientInto.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChangePatientInto.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butChangePatientInto.CornerRadius=4F;
			this.butChangePatientInto.Location=new System.Drawing.Point(542,47);
			this.butChangePatientInto.Name="butChangePatientInto";
			this.butChangePatientInto.Size=new System.Drawing.Size(75,24);
			this.butChangePatientInto.TabIndex=4;
			this.butChangePatientInto.Text="Change";
			this.butChangePatientInto.Click+=new System.EventHandler(this.butChangePatientInto_Click);
			// 
			// textPatientNameInto
			// 
			this.textPatientNameInto.Location=new System.Drawing.Point(153,49);
			this.textPatientNameInto.Name="textPatientNameInto";
			this.textPatientNameInto.ReadOnly=true;
			this.textPatientNameInto.Size=new System.Drawing.Size(369,20);
			this.textPatientNameInto.TabIndex=3;
			// 
			// label2
			// 
			this.label2.AutoSize=true;
			this.label2.Location=new System.Drawing.Point(150,31);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(116,13);
			this.label2.TabIndex=2;
			this.label2.Text="Selected Patient Name";
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Location=new System.Drawing.Point(7,31);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(99,13);
			this.label1.TabIndex=1;
			this.label1.Text="Selected Patient ID";
			// 
			// textPatientIDInto
			// 
			this.textPatientIDInto.Location=new System.Drawing.Point(6,50);
			this.textPatientIDInto.Name="textPatientIDInto";
			this.textPatientIDInto.ReadOnly=true;
			this.textPatientIDInto.Size=new System.Drawing.Size(124,20);
			this.textPatientIDInto.TabIndex=0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butRemoveSelectedPatientsFrom);
			this.groupBox2.Controls.Add(this.butAddPatientFrom);
			this.groupBox2.Controls.Add(this.gridMergeFromPatients);
			this.groupBox2.Location=new System.Drawing.Point(12,107);
			this.groupBox2.Name="groupBox2";
			this.groupBox2.Size=new System.Drawing.Size(630,299);
			this.groupBox2.TabIndex=5;
			this.groupBox2.TabStop=false;
			this.groupBox2.Text="Patients to merge from. WARNING: These accounts will be merged into the account a"+
					"bove and then permanently deleted.";
			// 
			// butRemoveSelectedPatientsFrom
			// 
			this.butRemoveSelectedPatientsFrom.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butRemoveSelectedPatientsFrom.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butRemoveSelectedPatientsFrom.Autosize=true;
			this.butRemoveSelectedPatientsFrom.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRemoveSelectedPatientsFrom.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butRemoveSelectedPatientsFrom.CornerRadius=4F;
			this.butRemoveSelectedPatientsFrom.Location=new System.Drawing.Point(87,19);
			this.butRemoveSelectedPatientsFrom.Name="butRemoveSelectedPatientsFrom";
			this.butRemoveSelectedPatientsFrom.Size=new System.Drawing.Size(99,24);
			this.butRemoveSelectedPatientsFrom.TabIndex=5;
			this.butRemoveSelectedPatientsFrom.Text="Remove Selected";
			this.butRemoveSelectedPatientsFrom.Click+=new System.EventHandler(this.butRemoveSelectedPatientsFrom_Click);
			// 
			// butAddPatientFrom
			// 
			this.butAddPatientFrom.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butAddPatientFrom.Anchor=((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Right)));
			this.butAddPatientFrom.Autosize=true;
			this.butAddPatientFrom.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddPatientFrom.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddPatientFrom.CornerRadius=4F;
			this.butAddPatientFrom.Location=new System.Drawing.Point(6,19);
			this.butAddPatientFrom.Name="butAddPatientFrom";
			this.butAddPatientFrom.Size=new System.Drawing.Size(75,24);
			this.butAddPatientFrom.TabIndex=4;
			this.butAddPatientFrom.Text="Add";
			this.butAddPatientFrom.Click+=new System.EventHandler(this.butAddPatientFrom_Click);
			// 
			// gridMergeFromPatients
			// 
			this.gridMergeFromPatients.HScrollVisible=false;
			this.gridMergeFromPatients.Location=new System.Drawing.Point(6,49);
			this.gridMergeFromPatients.Name="gridMergeFromPatients";
			this.gridMergeFromPatients.ScrollValue=0;
			this.gridMergeFromPatients.SelectionMode=OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMergeFromPatients.Size=new System.Drawing.Size(614,241);
			this.gridMergeFromPatients.TabIndex=0;
			this.gridMergeFromPatients.Title=null;
			this.gridMergeFromPatients.TranslationName=null;
			// 
			// FormPatientMerge
			// 
			this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize=new System.Drawing.Size(654,523);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butMerge);
			this.Controls.Add(this.butCancel);
			this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name="FormPatientMerge";
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Merge Patients";
			this.Load+=new System.EventHandler(this.FormPatientMerge_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butMerge;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textPatientNameInto;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPatientIDInto;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.ODGrid gridMergeFromPatients;
		private OpenDental.UI.Button butChangePatientInto;
		private OpenDental.UI.Button butAddPatientFrom;
		private OpenDental.UI.Button butRemoveSelectedPatientsFrom;
	}
}