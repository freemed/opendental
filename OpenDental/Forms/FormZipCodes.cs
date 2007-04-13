using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormZipCodes : System.Windows.Forms.Form{
		private OpenDental.TableZips tbZips;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butClose;
		private System.ComponentModel.Container components = null;
		private bool changed;

		///<summary></summary>
		public FormZipCodes(){
			InitializeComponent();
      tbZips.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbZips_CellDoubleClicked);
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormZipCodes));
			this.tbZips = new OpenDental.TableZips();
			this.butAdd = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// tbZips
			// 
			this.tbZips.BackColor = System.Drawing.SystemColors.Window;
			this.tbZips.Location = new System.Drawing.Point(19,14);
			this.tbZips.Name = "tbZips";
			this.tbZips.ScrollValue = 1;
			this.tbZips.SelectedIndices = new int[0];
			this.tbZips.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbZips.Size = new System.Drawing.Size(519,531);
			this.tbZips.TabIndex = 25;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(615,374);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(76,26);
			this.butAdd.TabIndex = 28;
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
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(615,513);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(76,26);
			this.butClose.TabIndex = 26;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(615,410);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(76,26);
			this.butDelete.TabIndex = 31;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormZipCodes
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(715,563);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.tbZips);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormZipCodes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Zip Codes";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormZipCodes_Closing);
			this.Load += new System.EventHandler(this.FormZipCodes_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormZipCodes_Load(object sender, System.EventArgs e) {
		  FillTable();
		}

		private void FillTable(){
			ZipCodes.Refresh();
  		tbZips.ResetRows(ZipCodes.List.Length);
			tbZips.SetGridColor(Color.Gray);
			tbZips.SetBackGColor(Color.White);      
			for(int i=0;i<ZipCodes.List.Length;i++){
				tbZips.Cell[0,i]=ZipCodes.List[i].ZipCodeDigits;
				tbZips.Cell[1,i]=ZipCodes.List[i].City;
				tbZips.Cell[2,i]=ZipCodes.List[i].State;
				if(ZipCodes.List[i].IsFrequent){
					tbZips.Cell[3,i]="X";
				}
			}
			tbZips.SelectedRow=-1;
			tbZips.LayoutTables(); 
		}

		private void tbZips_CellDoubleClicked(object sender, CellEventArgs e){
			if(tbZips.SelectedRow==-1){
				return;
			}
      FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ZipCodeCur=ZipCodes.List[tbZips.SelectedRow];
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillTable(); 
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(tbZips.SelectedRow==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}	
			ZipCode ZipCur=ZipCodes.List[tbZips.SelectedRow];		
			if(MessageBox.Show(Lan.g(this,"Delete Zipcode?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;   
			}
			changed=true;
			ZipCodes.Delete(ZipCur);
			FillTable();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormZipCodeEdit FormZCE=new FormZipCodeEdit();
			FormZCE.ZipCodeCur=new ZipCode();
			FormZCE.IsNew=true;
			FormZCE.ShowDialog();
			if(FormZCE.DialogResult!=DialogResult.OK){
				return;
			}
			changed=true;
			FillTable(); 				
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormZipCodes_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.ZipCodes);
			}
		}
	

	}
}
