using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// </summary>
	public class FormAutoNotes:System.Windows.Forms.Form {
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.ListBox listMain;
		private OpenDental.UI.Button butAdd;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip toolTip1;
		//private List<AutoNote> AutoNoteList;
		public AutoNote AutoNoteCur;

		///<summary></summary>
		public FormAutoNotes(){
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing )
			{
				if(components != null)	{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoNotes));
			this.listMain = new System.Windows.Forms.ListBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// listMain
			// 
			this.listMain.Location = new System.Drawing.Point(18,21);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(265,641);
			this.listMain.TabIndex = 2;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(356,637);
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
			this.butAdd.Location = new System.Drawing.Point(356,320);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 7;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormAutoNotes
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(447,675);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoNotes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auto Notes";
			this.Load += new System.EventHandler(this.FormAutoNotes_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormAutoNotes_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){			
			AutoNotes.RefreshCache();
			listMain.Items.Clear();
			for(int i=0;i<AutoNotes.Listt.Count;i++) {
				listMain.Items.Add(AutoNotes.Listt[i].AutoNoteName);
			}
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			if(listMain.SelectedIndex==-1){
				return;	
			}
			FormAutoNoteEdit form=new FormAutoNoteEdit();
			form.IsNew=false;
			form.AutoNoteCur=AutoNotes.Listt[listMain.SelectedIndex];
			form.ShowDialog();
			FillList();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {			
			FormAutoNoteEdit FormA=new FormAutoNoteEdit();
			FormA.IsNew=true;
			FormA.AutoNoteCur=new AutoNote();
			FormA.ShowDialog();
			FillList();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}


		

	}
}



























