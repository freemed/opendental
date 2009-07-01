using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary> </summary>
	public class FormEClinicalWorks:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.CheckBox checkEnabled;
		private IContainer components;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textProgName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textProgDesc;// Required designer variable.
		/// <summary>This Program link is new.</summary>
		public bool IsNew;
		public Program ProgramCur;
		private List<ProgramProperty> PropertyList;
		//private static Thread thread;
		private TextBox textHL7FolderIn;
		private TextBox textHL7FolderOut;
		private Label label4;
		private GroupBox groupBox1;
		private Label label5;
		private Label label6;
		private ComboBox comboDefaultUserGroup;
		private CheckBox checkShowImages;
		private Label label3;

		///<summary></summary>
		public FormEClinicalWorks() {
			components=new System.ComponentModel.Container();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEClinicalWorks));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textProgName = new System.Windows.Forms.TextBox();
			this.textProgDesc = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textHL7FolderIn = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textHL7FolderOut = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.comboDefaultUserGroup = new System.Windows.Forms.ComboBox();
			this.checkShowImages = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(524,269);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(443,269);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// checkEnabled
			// 
			this.checkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEnabled.Location = new System.Drawing.Point(161,60);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.Size = new System.Drawing.Size(98,18);
			this.checkEnabled.TabIndex = 41;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkEnabled.Click += new System.EventHandler(this.checkEnabled_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(58,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(187,18);
			this.label1.TabIndex = 44;
			this.label1.Text = "Internal Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textProgName
			// 
			this.textProgName.Location = new System.Drawing.Point(246,9);
			this.textProgName.Name = "textProgName";
			this.textProgName.ReadOnly = true;
			this.textProgName.Size = new System.Drawing.Size(275,20);
			this.textProgName.TabIndex = 45;
			// 
			// textProgDesc
			// 
			this.textProgDesc.Location = new System.Drawing.Point(246,34);
			this.textProgDesc.Name = "textProgDesc";
			this.textProgDesc.Size = new System.Drawing.Size(275,20);
			this.textProgDesc.TabIndex = 47;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(57,35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187,18);
			this.label2.TabIndex = 46;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHL7FolderIn
			// 
			this.textHL7FolderIn.Location = new System.Drawing.Point(234,43);
			this.textHL7FolderIn.Name = "textHL7FolderIn";
			this.textHL7FolderIn.Size = new System.Drawing.Size(275,20);
			this.textHL7FolderIn.TabIndex = 49;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3,44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(229,18);
			this.label3.TabIndex = 48;
			this.label3.Text = "In to eClinicalWorks";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textHL7FolderOut
			// 
			this.textHL7FolderOut.Location = new System.Drawing.Point(234,69);
			this.textHL7FolderOut.Name = "textHL7FolderOut";
			this.textHL7FolderOut.Size = new System.Drawing.Size(275,20);
			this.textHL7FolderOut.TabIndex = 51;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(226,18);
			this.label4.TabIndex = 50;
			this.label4.Text = "Out from eClinicalWorks";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textHL7FolderOut);
			this.groupBox1.Controls.Add(this.textHL7FolderIn);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12,86);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(538,101);
			this.groupBox1.TabIndex = 52;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HL7 Synch Folders";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6,19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(478,18);
			this.label5.TabIndex = 45;
			this.label5.Text = "Folder locations must be valid on the computer where the HL7 process is running";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12,202);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(232,18);
			this.label6.TabIndex = 53;
			this.label6.Text = "Default User Group for new users";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboDefaultUserGroup
			// 
			this.comboDefaultUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDefaultUserGroup.FormattingEnabled = true;
			this.comboDefaultUserGroup.Location = new System.Drawing.Point(246,202);
			this.comboDefaultUserGroup.Name = "comboDefaultUserGroup";
			this.comboDefaultUserGroup.Size = new System.Drawing.Size(215,21);
			this.comboDefaultUserGroup.TabIndex = 54;
			// 
			// checkShowImages
			// 
			this.checkShowImages.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowImages.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkShowImages.Location = new System.Drawing.Point(60,229);
			this.checkShowImages.Name = "checkShowImages";
			this.checkShowImages.Size = new System.Drawing.Size(199,18);
			this.checkShowImages.TabIndex = 55;
			this.checkShowImages.Text = "Show Images Module";
			this.checkShowImages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkShowImages.Click += new System.EventHandler(this.checkShowImages_Click);
			// 
			// FormEClinicalWorks
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(611,305);
			this.Controls.Add(this.checkShowImages);
			this.Controls.Add(this.comboDefaultUserGroup);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textProgDesc);
			this.Controls.Add(this.textProgName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkEnabled);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEClinicalWorks";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "eClinicalWorks Setup";
			this.Load += new System.EventHandler(this.FormEClinicalWorks_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProgramLinkEdit_Closing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormEClinicalWorks_Load(object sender, System.EventArgs e) {
			FillForm();
		}

		private void FillForm(){
			ProgramProperties.RefreshCache();
			PropertyList=ProgramProperties.GetListForProgram(ProgramCur.ProgramNum);
			textProgName.Text=ProgramCur.ProgName;
			textProgDesc.Text=ProgramCur.ProgDesc;
			checkEnabled.Checked=ProgramCur.Enabled;
			textHL7FolderIn.Text=GetProp("HL7FolderIn");
			textHL7FolderOut.Text=GetProp("HL7FolderOut");
			comboDefaultUserGroup.Items.Clear();
			for(int i=0;i<UserGroups.List.Length;i++) {
				comboDefaultUserGroup.Items.Add(UserGroups.List[i].Description);
				if(GetProp("DefaultUserGroup")==UserGroups.List[i].UserGroupNum.ToString()) {
					comboDefaultUserGroup.SelectedIndex=i;
				}
			}
			checkShowImages.Checked=GetProp("ShowImagesModule")=="1";
		}

		private string GetProp(string desc){
			for(int i=0;i<PropertyList.Count;i++){
				if(PropertyList[i].PropertyDesc==desc){
					return PropertyList[i].PropertyValue;
				}
			}
			throw new ApplicationException("Property not found: "+desc);
		}

		private void checkEnabled_Click(object sender,EventArgs e) {
			//security already checked when launching this form.  Only admin allowed.
			if(Security.CurUser.Password != "") {
				MsgBox.Show(this,"Admin password must be blank before this change can be made.");
				checkEnabled.Checked=!checkEnabled.Checked;
				return;
			}
			MsgBox.Show(this,"You will need to restart Open Dental to see the effects.");
		}

		private void checkShowImages_Click(object sender,EventArgs e) {
			MsgBox.Show(this,"You will need to restart Open Dental to see the effects.");
		}

		private bool SaveToDb(){
			if(textProgDesc.Text==""){
				MessageBox.Show("Description may not be blank.");
				return false;
			}
			if(comboDefaultUserGroup.SelectedIndex==-1) {
				MessageBox.Show("Please select a default user group first.");
				return false;
			}
			ProgramCur.ProgDesc=textProgDesc.Text;
			ProgramCur.Enabled=checkEnabled.Checked;
			Programs.Update(ProgramCur);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"HL7FolderIn",textHL7FolderIn.Text);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"HL7FolderOut",textHL7FolderOut.Text);
			ProgramProperties.SetProperty(ProgramCur.ProgramNum,"DefaultUserGroup",
				UserGroups.List[comboDefaultUserGroup.SelectedIndex].UserGroupNum.ToString());
			if(checkShowImages.Checked) {
				ProgramProperties.SetProperty(ProgramCur.ProgramNum,"ShowImagesModule","1");
			}
			else {
				ProgramProperties.SetProperty(ProgramCur.ProgramNum,"ShowImagesModule","0");
			}
			DataValid.SetInvalid(InvalidType.Programs);
			return true;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(!SaveToDb()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormProgramLinkEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			
		}

	

	

		

		

	

		

		

		
		


	}
}





















