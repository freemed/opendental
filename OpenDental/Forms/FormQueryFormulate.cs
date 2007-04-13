using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormQueryFormulate : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.ListBox list2;
		private System.Windows.Forms.TextBox textQuery;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textTitle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textFileName;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label3;
		public UserQuery UserQueryCur;

		///<summary></summary>
		public FormQueryFormulate(){
			InitializeComponent();
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQueryFormulate));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.list2 = new System.Windows.Forms.ListBox();
			this.textQuery = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textTitle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
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
			this.butOK.Location = new System.Drawing.Point(808,560);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(808,596);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// list2
			// 
			this.list2.Location = new System.Drawing.Point(32,20);
			this.list2.Name = "list2";
			this.list2.Size = new System.Drawing.Size(186,524);
			this.list2.TabIndex = 0;
			this.list2.DoubleClick += new System.EventHandler(this.list2_DoubleClick);
			this.list2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.list2_MouseDown);
			// 
			// textQuery
			// 
			this.textQuery.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textQuery.Location = new System.Drawing.Point(252,90);
			this.textQuery.Multiline = true;
			this.textQuery.Name = "textQuery";
			this.textQuery.ReadOnly = true;
			this.textQuery.Size = new System.Drawing.Size(522,462);
			this.textQuery.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(252,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,14);
			this.label1.TabIndex = 11;
			this.label1.Text = "Title";
			// 
			// textTitle
			// 
			this.textTitle.Location = new System.Drawing.Point(252,36);
			this.textTitle.Name = "textTitle";
			this.textTitle.ReadOnly = true;
			this.textTitle.Size = new System.Drawing.Size(360,20);
			this.textTitle.TabIndex = 12;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(252,70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,14);
			this.label2.TabIndex = 13;
			this.label2.Text = "Query";
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(250,592);
			this.textFileName.Name = "textFileName";
			this.textFileName.ReadOnly = true;
			this.textFileName.Size = new System.Drawing.Size(360,20);
			this.textFileName.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(250,572);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(271,14);
			this.label3.TabIndex = 15;
			this.label3.Text = "Save As File Name";
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(34,567);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(82,26);
			this.butAdd.TabIndex = 34;
			this.butAdd.Text = "&New";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(141,567);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(82,26);
			this.butDelete.TabIndex = 35;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormQueryFormulate
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(900,652);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textFileName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textTitle);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textQuery);
			this.Controls.Add(this.list2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormQueryFormulate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Query Favorites";
			this.Load += new System.EventHandler(this.FormQueryFormulate_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormQueryFormulate_Load(object sender, System.EventArgs e) {
			FillList();
		}

		private void FillList(){
			UserQueries.Refresh();
			int tempIndex=list2.SelectedIndex;
			list2.Items.Clear();
			for(int i=0;i<UserQueries.List.Length;i++){
				this.list2.Items.Add(UserQueries.List[i].Description);
			}
			list2.SelectedIndex=tempIndex;
		}

		private void list2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(list2.IndexFromPoint(e.X,e.Y)<0){//>list2.Items.Count){
				return;
			}
			UserQueryCur=UserQueries.List[list2.IndexFromPoint(e.X,e.Y)];
			textQuery.Text=UserQueryCur.QueryText;
			textTitle.Text=UserQueryCur.Description;
			textFileName.Text=UserQueryCur.FileName;
		}

		private void list2_DoubleClick(object sender, System.EventArgs e) {
			FormQueryEdit FormQE=new FormQueryEdit();
			FormQE.UserQueryCur=UserQueryCur;
			FormQE.IsNew=false;
			FormQE.ShowDialog();
			FillList();
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(list2.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
			}
			if(MessageBox.Show(Lan.g(this,"Delete Item?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			UserQueries.Delete(UserQueryCur);
			list2.SelectedIndex=-1;
			FillList();
			textTitle.Text="";
			textQuery.Text="";
			textFileName.Text="";
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(list2.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a query first."));
				return;
			}
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=UserQueryCur.QueryText;
			DialogResult=DialogResult.OK;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormQueryEdit FormQE=new FormQueryEdit();
			FormQE.IsNew=true;
			FormQE.UserQueryCur=new UserQuery();
			FormQE.ShowDialog();
			if(FormQE.DialogResult==DialogResult.OK){
				FillList();
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
		
		}

	}
}
