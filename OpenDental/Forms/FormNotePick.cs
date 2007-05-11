using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormNotePick:System.Windows.Forms.Form {
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridMain;
		private Label label1;
		private TextBox textNote;
		private Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		private string[] Notes;
		private OpenDental.UI.Button butCancel;
		///<summary>Upon closing with OK, this will be the final note to save.</summary>
		public string SelectedNote;

		///<summary></summary>
		public FormNotePick(string[] notes)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Notes=new string[notes.Length];
			notes.CopyTo(Notes,0);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotePick));
			this.butOK = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(473,516);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(29,40);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(627,322);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Pick Note";
			this.gridMain.TranslationName = "TableNotePick";
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(29,2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(627,35);
			this.label1.TabIndex = 3;
			this.label1.Text = "Multiple versions of the note exist.  Please pick or edit one version to retain. " +
    " This note will apply to ALL similar plans.  You can also pick multiple rows to " +
    "combine notes.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(29,396);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(627,99);
			this.textNote.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(29,365);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(627,28);
			this.label2.TabIndex = 5;
			this.label2.Text = "This is the final note that will be saved for all similar plans.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(581,516);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormNotePick
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(692,564);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormNotePick";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FormNotePick_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormNotePick_Load(object sender,EventArgs e) {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",630);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Notes.Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(Notes[i]);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			if(Notes.Length>0){
				gridMain.SetSelected(0,true);
				textNote.Text=Notes[0];
			}
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			textNote.Text="";
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				textNote.Text+=Notes[gridMain.SelectedIndices[i]];
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedNote=Notes[e.Row];
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			SelectedNote=textNote.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

	


	}
}





















