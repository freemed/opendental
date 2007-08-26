using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMountDefs : System.Windows.Forms.Form{
		private System.Windows.Forms.ListBox listMain;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private bool changed;

		///<summary></summary>
		public FormMountDefs()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMountDefs));
			this.listMain = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listMain
			// 
			this.listMain.Location = new System.Drawing.Point(24,42);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(262,264);
			this.listMain.TabIndex = 2;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(21,10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(461,27);
			this.label1.TabIndex = 11;
			this.label1.Text = "This is a list of radiograph mounts or image composites.  You can freely delete o" +
    "r move any of these items without affecting patient records";
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
			this.butAdd.Location = new System.Drawing.Point(24,313);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(396,313);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(204,313);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(82,26);
			this.butDown.TabIndex = 36;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(113,313);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(82,26);
			this.butUp.TabIndex = 37;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormMountDefs
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(494,356);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMountDefs";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Mount Definitions";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMounts_FormClosing);
			this.Load += new System.EventHandler(this.FormMountDefs_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMountDefs_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			MountDefs.Refresh();
			listMain.Items.Clear();
			for(int i=0;i<MountDefs.Listt.Count;i++){
				listMain.Items.Add(MountDefs.Listt[i].Description);
			}
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			MountDef mount=new MountDef();
			FormMountDefEdit FormM=new FormMountDefEdit(mount);
			FormM.IsNew=true;
			FormM.ShowDialog();
			FillList();
			changed=true;
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			if(listMain.SelectedIndex==-1){
				return;
			}
			FormMountDefEdit FormM=new FormMountDefEdit(MountDefs.Listt[listMain.SelectedIndex]);
			FormM.ShowDialog();
			FillList();
			changed=true;
		}

		private void butUp_Click(object sender,EventArgs e) {
			/*int selected=0;
			if(listViewButtons.SelectedIndices.Count==0) {
				return;
			}
			else if(listViewButtons.SelectedIndices[0]==0) {
				return;
			}
			else {
				ProcButton but=ButtonList[listViewButtons.SelectedIndices[0]].Copy();
				but.ItemOrder--;
				ProcButtons.Update(but);
				selected=but.ItemOrder;
				but=ButtonList[listViewButtons.SelectedIndices[0]-1].Copy();
				but.ItemOrder++;
				ProcButtons.Update(but);
			}
			FillButtons();
			changed=true;
			listViewButtons.SelectedIndices.Clear();
			listViewButtons.SelectedIndices.Add(selected);*/
		}

		private void butDown_Click(object sender,EventArgs e) {

		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormMounts_FormClosing(object sender,FormClosingEventArgs e) {
			if(changed) {
				DataValid.SetInvalid(InvalidTypes.ToolBut);
			}
		}

		

		



		
	}
}





















