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
	public class FormClearinghouses : System.Windows.Forms.Form{
		private System.Windows.Forms.TextBox textBox1;
		private OpenDental.TableClearinghouses gridMain;
		private OpenDental.UI.Button butClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private bool listHasChanged;

		///<summary></summary>
		public FormClearinghouses()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.C(this, new System.Windows.Forms.Control[]
			{
				textBox1
			});
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClearinghouses));
			this.butClose = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.TableClearinghouses();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.butAdd = new OpenDental.UI.Button();
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
			this.butClose.Location = new System.Drawing.Point(807,465);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 0;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// gridMain
			// 
			this.gridMain.BackColor = System.Drawing.SystemColors.Window;
			this.gridMain.Location = new System.Drawing.Point(6,61);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 1;
			this.gridMain.SelectedIndices = new int[0];
			this.gridMain.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.gridMain.Size = new System.Drawing.Size(879,318);
			this.gridMain.TabIndex = 2;
			this.gridMain.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(this.gridMain_CellDoubleClicked);
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(10,8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(597,50);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(805,385);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80,26);
			this.butAdd.TabIndex = 8;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormClearinghouses
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(891,503);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClearinghouses";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "E-Claims";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormClearinghouses_Closing);
			this.Load += new System.EventHandler(this.FormClearinghouses_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClearinghouses_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Clearinghouses.Refresh();
			gridMain.ResetRows(Clearinghouses.List.Length);
			gridMain.SetGridColor(Color.Gray);
			gridMain.SetBackGColor(Color.White);
			for(int i=0;i<Clearinghouses.List.Length;i++){
				gridMain.Cell[0,i]=Clearinghouses.List[i].Description;
				gridMain.Cell[1,i]=Clearinghouses.List[i].ExportPath;
				gridMain.Cell[2,i]=Clearinghouses.List[i].Eformat.ToString();
				if(Clearinghouses.List[i].IsDefault){
					gridMain.Cell[3,i]="X";
				}
				gridMain.Cell[4,i]=Clearinghouses.List[i].Payors;
			}
			gridMain.LayoutTables();
		}

		private void gridMain_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			FormClearinghouseEdit FormCE=new FormClearinghouseEdit();
			FormCE.ClearinghouseCur=Clearinghouses.List[e.Row];
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			listHasChanged=true;
			FillGrid();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormClearinghouseEdit FormCE=new FormClearinghouseEdit();
			FormCE.ClearinghouseCur=new Clearinghouse();
			FormCE.IsNew=true;
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK)
				return;
			listHasChanged=true;
			FillGrid();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormClearinghouses_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			int defaultsSelected=0;
			for(int i=0;i<Clearinghouses.List.Length;i++){
				if(Clearinghouses.List[i].IsDefault){
					defaultsSelected++;
				}
			}
			if(defaultsSelected==0 && Clearinghouses.List.Length>0){
				if(!MsgBox.Show(this,true,"At least one clearinghouse should be selected as the default. Continue anyway?")){
					e.Cancel=true;
					return;
				}
			}
			if(defaultsSelected>1){
				if(!MsgBox.Show(this,true,"Only one clearinghouse should be selected as the default. Continue anyway?")) {
					e.Cancel=true;
					return;
				}
			}
			if(listHasChanged){
				//update all computers including this one:
				DataValid.SetInvalid(InvalidTypes.ClearHouses);
			}
		}

		

		

	}
}





















