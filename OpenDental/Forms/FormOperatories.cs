using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormOperatories : System.Windows.Forms.Form{
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.ODGrid gridMain;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private bool changed;

		///<summary></summary>
		public FormOperatories()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperatories));
			this.butClose = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
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
			this.butClose.Location = new System.Drawing.Point(542,445);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
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
			this.butAdd.Location = new System.Drawing.Point(19,445);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80,26);
			this.butAdd.TabIndex = 10;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(21,31);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(595,385);
			this.gridMain.TabIndex = 11;
			this.gridMain.Title = "Operatories";
			this.gridMain.TranslationName = "TableOperatories";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20,7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(588,20);
			this.label1.TabIndex = 12;
			this.label1.Text = "(Also, see the appointment views section)";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.butDown.Location = new System.Drawing.Point(224,445);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79,26);
			this.butDown.TabIndex = 14;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(122,445);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79,26);
			this.butUp.TabIndex = 13;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// FormOperatories
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(649,494);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOperatories";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Operatories";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormOperatories_Closing);
			this.Load += new System.EventHandler(this.FormOperatories_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormOperatories_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Operatories.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableOperatories","Op Name"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","Abbrev"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","IsHidden"),64,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","Clinic"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","Dentist"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","Hygienist"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableOperatories","IsHygiene"),72,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			UI.ODGridRow row;
			for(int i=0;i<Operatories.List.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(Operatories.List[i].OpName);
				row.Cells.Add(Operatories.List[i].Abbrev);
				if(Operatories.List[i].IsHidden){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(Clinics.GetDesc(Operatories.List[i].ClinicNum));
				row.Cells.Add(Providers.GetAbbr(Operatories.List[i].ProvDentist));
				row.Cells.Add(Providers.GetAbbr(Operatories.List[i].ProvHygienist));
				if(Operatories.List[i].IsHygiene){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormOperatoryEdit FormE=new FormOperatoryEdit(Operatories.List[e.Row]);
			FormE.ShowDialog();
			FillGrid();
			changed=true;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Operatory opCur=new Operatory();
			if(gridMain.SelectedIndices.Length>0){//a row is selected
				opCur.ItemOrder=gridMain.SelectedIndices[0];
			}
			else{
				opCur.ItemOrder=Operatories.List.Length;//goes at end of list
			}
			FormOperatoryEdit FormE=new FormOperatoryEdit(opCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult==DialogResult.Cancel){
				return;
			}
			if(gridMain.SelectedIndices.Length>0){
				//fix the itemOrder of every Operatory following this one
				for(int i=gridMain.SelectedIndices[0];i<Operatories.List.Length;i++){
					Operatories.List[i].ItemOrder++;
					Operatories.InsertOrUpdate(Operatories.List[i],false);
				}
			}
			FillGrid();
			changed=true;
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"You must first select a row.");
				return;
			}
			int selected=gridMain.SelectedIndices[0];
			if(selected==0){
				return;//already at the top
			}
			//move selected item up
			Operatories.List[selected].ItemOrder--;
			Operatories.InsertOrUpdate(Operatories.List[selected],false);
			//move the one above it down
			Operatories.List[selected-1].ItemOrder++;
			Operatories.InsertOrUpdate(Operatories.List[selected-1],false);
			FillGrid();
			gridMain.SetSelected(selected-1,true);
			changed=true;
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(gridMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"You must first select a row.");
				return;
			}
			int selected=gridMain.SelectedIndices[0];
			if(selected==Operatories.List.Length-1){
				return;//already at the bottom
			}
			//move selected item down
			Operatories.List[selected].ItemOrder++;
			Operatories.InsertOrUpdate(Operatories.List[selected],false);
			//move the one below it up
			Operatories.List[selected+1].ItemOrder--;
			Operatories.InsertOrUpdate(Operatories.List[selected+1],false);
			FillGrid();
			gridMain.SetSelected(selected+1,true);
			changed=true;
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormOperatories_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Operatories);
			}
		}

		

		

		



		
	}
}





















