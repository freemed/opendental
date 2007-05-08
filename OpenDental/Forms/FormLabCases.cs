using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormLabCases : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private ODGrid gridMain;// Required designer variable.
		private DataTable table;

		///<summary></summary>
		public FormLabCases()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabCases));
			this.butClose = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(592,469);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(33,33);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(634,409);
			this.gridMain.TabIndex = 1;
			this.gridMain.Title = "Lab Cases";
			this.gridMain.TranslationName = "TableLabCases";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormLabCases
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(719,520);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLabCases";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lab Cases";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLabCases_FormClosing);
			this.Load += new System.EventHandler(this.FormLabCases_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLabCases_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			table=LabCases.Refresh(DateTime.Today,DateTime.Today.AddDays(5));
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableLabCases","Description"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Phone"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCases","Notes"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			/*for(int i=0;i<ListLabs.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(ListLabs[i].Description);
				row.Cells.Add(ListLabs[i].Phone);
				row.Cells.Add(ListLabs[i].Notes);
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/*FormLaboratoryEdit FormL=new FormLaboratoryEdit();
			FormL.LabCur=ListLabs[e.Row];
			FormL.ShowDialog();
			//if(FormL.DialogResult==DialogResult.OK){
				//changed=true;
			FillGrid();*/
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormLabCases_FormClosing(object sender,FormClosingEventArgs e) {
			//if(changed){
				//Labs are not global.
				//DataValid.SetInvalid(InvalidTypes.Providers);
			//}
		}

		

		

		


	}
}





















