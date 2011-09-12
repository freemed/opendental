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
		private GroupBox groupBox1;
		private UI.Button butDefaultMedical;
		private UI.Button butDefaultDental;
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.butDefaultDental = new OpenDental.UI.Button();
			this.butDefaultMedical = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
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
			this.butClose.Size = new System.Drawing.Size(75,24);
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
			this.gridMain.SelectionMode = System.Windows.Forms.SelectionMode.One;
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
			this.butAdd.Size = new System.Drawing.Size(80,24);
			this.butAdd.TabIndex = 8;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butDefaultMedical);
			this.groupBox1.Controls.Add(this.butDefaultDental);
			this.groupBox1.Location = new System.Drawing.Point(6,387);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(97,86);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Set Default";
			// 
			// butDefaultDental
			// 
			this.butDefaultDental.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefaultDental.Autosize = true;
			this.butDefaultDental.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaultDental.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaultDental.CornerRadius = 4F;
			this.butDefaultDental.Location = new System.Drawing.Point(15,19);
			this.butDefaultDental.Name = "butDefaultDental";
			this.butDefaultDental.Size = new System.Drawing.Size(75,24);
			this.butDefaultDental.TabIndex = 1;
			this.butDefaultDental.Text = "Dental";
			this.butDefaultDental.Click += new System.EventHandler(this.butDefaultDental_Click);
			// 
			// butDefaultMedical
			// 
			this.butDefaultMedical.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDefaultMedical.Autosize = true;
			this.butDefaultMedical.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDefaultMedical.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDefaultMedical.CornerRadius = 4F;
			this.butDefaultMedical.Location = new System.Drawing.Point(15,49);
			this.butDefaultMedical.Name = "butDefaultMedical";
			this.butDefaultMedical.Size = new System.Drawing.Size(75,24);
			this.butDefaultMedical.TabIndex = 2;
			this.butDefaultMedical.Text = "Medical";
			this.butDefaultMedical.Click += new System.EventHandler(this.butDefaultMedical_Click);
			// 
			// FormClearinghouses
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(891,503);
			this.Controls.Add(this.groupBox1);
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
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClearinghouses_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			Clearinghouses.RefreshCache();
			gridMain.ResetRows(Clearinghouses.Listt.Length);
			gridMain.SetGridColor(Color.Gray);
			gridMain.SetBackGColor(Color.White);
			for(int i=0;i<Clearinghouses.Listt.Length;i++){
				gridMain.Cell[0,i]=Clearinghouses.Listt[i].Description;
				gridMain.Cell[1,i]=Clearinghouses.Listt[i].ExportPath;
				gridMain.Cell[2,i]=Clearinghouses.Listt[i].Eformat.ToString();
				string s="";
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultDent)==Clearinghouses.Listt[i].ClearinghouseNum){
					s+="Dent";
				}
				if(PrefC.GetLong(PrefName.ClearinghouseDefaultMed)==Clearinghouses.Listt[i].ClearinghouseNum){
					if(s!=""){
						s+=",";
					}
					s+="Med";
				}
				gridMain.Cell[3,i]=s;
				gridMain.Cell[4,i]=Clearinghouses.Listt[i].Payors;
			}
			gridMain.LayoutTables();
		}

		private void gridMain_CellDoubleClicked(object sender, OpenDental.CellEventArgs e) {
			FormClearinghouseEdit FormCE=new FormClearinghouseEdit();
			FormCE.ClearinghouseCur=Clearinghouses.Listt[e.Row];
			FormCE.ShowDialog();
			if(FormCE.DialogResult!=DialogResult.OK){
				return;
			}
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

		private void butDefaultDental_Click(object sender,EventArgs e) {
			if(gridMain.SelectedRow==-1){
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			Clearinghouse ch=Clearinghouses.Listt[gridMain.SelectedRow];
			if(ch.Eformat==ElectronicClaimFormat.x837_5010_med_inst){//med/inst clearinghouse
				MsgBox.Show(this,"The selected clearinghouse must first be set to a dental e-claim format.");
				return;
			}
			Prefs.UpdateLong(PrefName.ClearinghouseDefaultDent,ch.ClearinghouseNum);
			FillGrid();
			DataValid.SetInvalid(InvalidType.Prefs);
		}

		private void butDefaultMedical_Click(object sender,EventArgs e) {
			if(gridMain.SelectedRow==-1){
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			Clearinghouse ch=Clearinghouses.Listt[gridMain.SelectedRow];
			if(ch.Eformat!=ElectronicClaimFormat.x837_5010_med_inst){//anything except the med/inst format
				MsgBox.Show(this,"The selected clearinghouse must first be set to the med/inst e-claim format.");
				return;
			}
			Prefs.UpdateLong(PrefName.ClearinghouseDefaultMed,Clearinghouses.Listt[gridMain.SelectedRow].ClearinghouseNum);
			FillGrid();
			DataValid.SetInvalid(InvalidType.Prefs);
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormClearinghouses_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(PrefC.GetLong(PrefName.ClearinghouseDefaultDent)==0){
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"A default clearinghouse should be set. Continue anyway?")){
					e.Cancel=true;
					return;
				}
			}
			//validate that the default dental clearinghouse is not type mismatched.
			Clearinghouse chDent=Clearinghouses.GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultDent));
			if(chDent.Eformat==ElectronicClaimFormat.x837_5010_med_inst){//mismatch
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"The default dental clearinghouse should be set to a dental e-claim format.  Continue anyway?")){
					e.Cancel=true;
					return;
				}
			}
			//validate medical clearinghouse
			Clearinghouse chMed=Clearinghouses.GetClearinghouse(PrefC.GetLong(PrefName.ClearinghouseDefaultMed));
			if(chMed.Eformat!=ElectronicClaimFormat.x837_5010_med_inst){//mismatch
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"The default medical clearinghouse should be set to a med/inst e-claim format.  Continue anyway?")){
					e.Cancel=true;
					return;
				}
			}
			if(listHasChanged){
				//update all computers including this one:
				DataValid.SetInvalid(InvalidType.ClearHouses);
			}
		}

		

		

		

	}
}





















