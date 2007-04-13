using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLanguagesUsed : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private ListBox listAvailable;
		private Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Label label2;
		private Label label3;
		private ListBox listUsed;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private CultureInfo[] AllCultures;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private List<string> LangsUsed;

		///<summary></summary>
		public FormLanguagesUsed()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>Clean up any resources being used.</summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLanguagesUsed));
			this.listAvailable = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listUsed = new System.Windows.Forms.ListBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listAvailable
			// 
			this.listAvailable.FormattingEnabled = true;
			this.listAvailable.Location = new System.Drawing.Point(32,107);
			this.listAvailable.Name = "listAvailable";
			this.listAvailable.Size = new System.Drawing.Size(278,394);
			this.listAvailable.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(281,23);
			this.label1.TabIndex = 2;
			this.label1.Text = "All Languages";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(29,26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(474,53);
			this.label2.TabIndex = 3;
			this.label2.Text = "This window lets you define which languages will be available to assign to patien" +
    "ts.\r\nThis will not change the language of the user interface.\r\nIt will only be u" +
    "sed when interacting with patients.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(444,80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(281,23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Languages used by patients";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listUsed
			// 
			this.listUsed.FormattingEnabled = true;
			this.listUsed.Location = new System.Drawing.Point(446,107);
			this.listUsed.Name = "listUsed";
			this.listUsed.Size = new System.Drawing.Size(278,134);
			this.listUsed.TabIndex = 4;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(649,434);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 6;
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
			this.butCancel.Location = new System.Drawing.Point(649,475);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.Location = new System.Drawing.Point(547,250);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(53,26);
			this.butUp.TabIndex = 9;
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(446,250);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(83,26);
			this.butDelete.TabIndex = 8;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Right;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.butAdd.Location = new System.Drawing.Point(340,107);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.Location = new System.Drawing.Point(618,250);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(53,26);
			this.butDown.TabIndex = 10;
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// FormLanguagesUsed
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(776,528);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.listUsed);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listAvailable);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLanguagesUsed";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Language Definitions";
			this.Load += new System.EventHandler(this.FormLanguagesUsed_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLanguagesUsed_Load(object sender,EventArgs e) {
			AllCultures=CultureInfo.GetCultures(CultureTypes.NeutralCultures);
			string[] culturedescripts=new string[AllCultures.Length];
			for(int i=0;i<AllCultures.Length;i++){
				culturedescripts[i]=AllCultures[i].DisplayName;
			}
			Array.Sort(culturedescripts,AllCultures);//sort based on descriptions
			for(int i=0;i<AllCultures.Length;i++){
				listAvailable.Items.Add(AllCultures[i].DisplayName);
			}
			if(PrefB.GetString("LanguagesUsedByPatients")==""){
				LangsUsed=new List<string>();
			}
			else{
				LangsUsed=new List<string>(PrefB.GetString("LanguagesUsedByPatients").Split(','));
			}
			FillListUsed();
		}

		private void FillListUsed(){
			listUsed.Items.Clear();
			for(int i=0;i<LangsUsed.Count;i++) {
				listUsed.Items.Add(CultureInfo.GetCultureInfo(LangsUsed[i]).DisplayName);
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(listAvailable.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a language first");
				return;
			}
			string lang=AllCultures[listAvailable.SelectedIndex].Name;//en,fr, etc
			if(LangsUsed.Contains(lang)){
				MsgBox.Show(this,"Language already added.");
				return;
			}
			LangsUsed.Add(lang);
			FillListUsed();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(listUsed.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a language first");
				return;
			}
			LangsUsed.RemoveAt(listUsed.SelectedIndex);
			FillListUsed();
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(listUsed.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a language first");
				return;
			}
			if(listUsed.SelectedIndex==0){
				return;
			}
			int newIndex=listUsed.SelectedIndex-1;
			LangsUsed.Reverse(listUsed.SelectedIndex-1,2);
			FillListUsed();
			listUsed.SetSelected(newIndex,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(listUsed.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a language first");
				return;
			}
			if(listUsed.SelectedIndex==listUsed.Items.Count-1) {
				return;
			}
			int newIndex=listUsed.SelectedIndex+1;
			LangsUsed.Reverse(listUsed.SelectedIndex,2);
			FillListUsed();
			listUsed.SetSelected(newIndex,true);
		}

		private void butOK_Click(object sender,EventArgs e) {
			string str="";
			for(int i=0;i<LangsUsed.Count;i++){
				if(i>0){
					str+=",";
				}
				str+=LangsUsed[i];
			}
			Prefs.UpdateString("LanguagesUsedByPatients",str);
			//prefs refresh handled by the calling form.
			DialogResult=DialogResult.OK;
		}
		
		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















