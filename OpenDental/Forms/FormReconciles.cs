using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormReconciles : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private OpenDental.UI.Button butAdd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid grid;
		private Reconcile[] RList;
		private int AccountNum;

		///<summary></summary>
		public FormReconciles(int accountNum)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			AccountNum=accountNum;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReconciles));
			this.grid = new OpenDental.UI.ODGrid();
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.HScrollVisible = false;
			this.grid.Location = new System.Drawing.Point(18,13);
			this.grid.Name = "grid";
			this.grid.ScrollValue = 0;
			this.grid.Size = new System.Drawing.Size(191,450);
			this.grid.TabIndex = 1;
			this.grid.Title = "Existing Reconciles";
			this.grid.TranslationName = "TableReconciles";
			this.grid.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid_CellDoubleClick);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(242,509);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(18,475);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(77,26);
			this.butAdd.TabIndex = 98;
			this.butAdd.Text = "Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormReconciles
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(346,546);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReconciles";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reconciles";
			this.Load += new System.EventHandler(this.FormReconciles_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReconciles_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			RList=Reconciles.GetList(AccountNum);
			grid.BeginUpdate();
			grid.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableReconciles","Date"),80);
			grid.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableReconciles","Ending Bal"),100,HorizontalAlignment.Right);
			grid.Columns.Add(col);
			grid.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<RList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(RList[i].DateReconcile.ToShortDateString());
				row.Cells.Add(RList[i].EndingBal.ToString("F"));
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
		}

		private void grid_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormReconcileEdit FormR=new FormReconcileEdit(RList[e.Row]);
			FormR.ShowDialog();
			if(FormR.DialogResult==DialogResult.Cancel){
				return;
			}
			FillGrid();
		}

		///<summary></summary>
		private void butAdd_Click(object sender, System.EventArgs e) {
			Reconcile rec=new Reconcile();
			rec.DateReconcile=DateTime.Today;
			rec.AccountNum=AccountNum;
			Reconciles.Insert(rec);
			FormReconcileEdit FormR=new FormReconcileEdit(rec);
			FormR.IsNew=true;
			FormR.ShowDialog();
			if(FormR.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		

		

		

		


	}
}





















