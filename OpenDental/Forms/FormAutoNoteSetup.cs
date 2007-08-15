using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// </summary>
	public class FormAutoNoteSetup:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.ListBox listMain;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.IContainer components;
		private Label label1;
		private System.Windows.Forms.ToolTip toolTip1;
		private OpenDental.UI.Button butOK;
		///<summary>Set to true when user is using this to select a disease def. Currently used when adding Alerts to Rx.</summary>
		public bool IsSelectionMode;
		///<summary>If IsSelectionMode, then after closing with OK, this will contain number.</summary>
		//public int SelectedDiseaseDefNum;
		private List<AutoNote> AutoNoteList;

		///<summary></summary>
		public FormAutoNoteSetup()
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoNoteSetup));
			this.listMain = new System.Windows.Forms.ListBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listMain
			// 
			this.listMain.Location = new System.Drawing.Point(18,34);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(265,628);
			this.listMain.TabIndex = 2;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(15,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(413,24);
			this.label1.TabIndex = 8;
			this.label1.Text = "This is a list of auto notes.";
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(349,637);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(79,26);
			this.butClose.TabIndex = 1;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(349,394);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(349,605);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(79,26);
			this.butOK.TabIndex = 15;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// FormAutoNoteSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(447,675);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoNoteSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto Notes";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDiseaseDefs_FormClosing);
			this.Load += new System.EventHandler(this.FormDiseaseDefs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDiseaseDefs_Load(object sender, System.EventArgs e) {
			if(IsSelectionMode){
				butClose.Text=Lan.g(this,"Cancel");
			}
			else{
				butOK.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid(){
			listMain.Items.Clear();
			AutoNoteList=AutoNotes.Refresh();
			string s;
			for(int i = 0;i < AutoNoteList.Count;i++) {
				listMain.Items.Add(AutoNoteList[i].AutoNoteName);
			}
			/*DiseaseDefs.Refresh();
			listMain.Items.Clear();
			string s;
			for(int i=0;i<DiseaseDefs.ListLong.Length;i++){
				s=DiseaseDefs.ListLong[i].DiseaseName;
				if(DiseaseDefs.ListLong[i].IsHidden){
					s+=" "+Lan.g(this,"(hidden)");
				}
				listMain.Items.Add(s);
			}*/
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			/*if(listMain.SelectedIndex==-1){
				return;
			}
			if(IsSelectionMode){
				SelectedDiseaseDefNum=DiseaseDefs.ListLong[listMain.SelectedIndex].DiseaseDefNum;
				DialogResult=DialogResult.OK;
				return;
			}
			FormDiseaseDefEdit FormD=new FormDiseaseDefEdit(DiseaseDefs.ListLong[listMain.SelectedIndex]);
			FormD.ShowDialog();
			if(FormD.DialogResult!=DialogResult.OK)
				return;*/
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			/*DiseaseDef def=new DiseaseDef();
			def.ItemOrder=DiseaseDefs.ListLong.Length;
			FormDiseaseDefEdit FormD=new FormDiseaseDefEdit(def);
			FormD.IsNew=true;
			FormD.ShowDialog();*/
			FillGrid();
		}


		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(listMain.SelectedIndex==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			//SelectedDiseaseDefNum=DiseaseDefs.ListLong[listMain.SelectedIndex].DiseaseDefNum;
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;//also closes if not IsSelectionMode
		}

		private void FormDiseaseDefs_FormClosing(object sender,FormClosingEventArgs e) {
			DataValid.SetInvalid(InvalidTypes.Email);
		}

		

		

		

		

		

		

		

		


	}
}



























