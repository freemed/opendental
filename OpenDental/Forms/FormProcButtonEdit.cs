using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormProcButtonEdit:System.Windows.Forms.Form {
		private IContainer components;
		private System.Windows.Forms.ListBox listAutoCodes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textDescript;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		///<summary></summary>
    public bool IsNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListBox listADA;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label4;
		private Label label5;
		private ComboBox comboCategory;
		private PictureBox pictureBox;
		private Label label6;
		private OpenDental.UI.Button butImport;
		private OpenDental.UI.Button butClear;
		private ListView listView;
		private Label label7;
		private ImageList imageList;
		private ProcButton ProcButtonCur;

		///<summary></summary>
		public FormProcButtonEdit(ProcButton procButtonCur){
			InitializeComponent();
			Lan.F(this);
			ProcButtonCur=procButtonCur;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcButtonEdit));
			this.listAutoCodes = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.listADA = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.label6 = new System.Windows.Forms.Label();
			this.butImport = new OpenDental.UI.Button();
			this.butClear = new OpenDental.UI.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.label7 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// listAutoCodes
			// 
			this.listAutoCodes.Location = new System.Drawing.Point(258,154);
			this.listAutoCodes.Name = "listAutoCodes";
			this.listAutoCodes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listAutoCodes.Size = new System.Drawing.Size(158,355);
			this.listAutoCodes.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(41,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124,13);
			this.label1.TabIndex = 25;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(165,2);
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(316,20);
			this.textDescript.TabIndex = 24;
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
			this.butCancel.Location = new System.Drawing.Point(756,553);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 27;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(756,515);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 26;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(36,137);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188,14);
			this.label2.TabIndex = 29;
			this.label2.Text = "Add Procedure Codes";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(256,129);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(191,22);
			this.label3.TabIndex = 31;
			this.label3.Text = "Highlight Auto Codes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listADA
			// 
			this.listADA.Location = new System.Drawing.Point(36,154);
			this.listADA.Name = "listADA";
			this.listADA.Size = new System.Drawing.Size(160,355);
			this.listADA.TabIndex = 32;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Underline,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.label4.Location = new System.Drawing.Point(41,106);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(358,23);
			this.label4.TabIndex = 35;
			this.label4.Text = "Add any number of procedure codes and Auto Codes";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
			this.butAdd.Location = new System.Drawing.Point(35,519);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(75,26);
			this.butAdd.TabIndex = 36;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(122,519);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,26);
			this.butDelete.TabIndex = 37;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(41,33);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(124,13);
			this.label5.TabIndex = 38;
			this.label5.Text = "Category";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboCategory
			// 
			this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCategory.FormattingEnabled = true;
			this.comboCategory.Location = new System.Drawing.Point(165,30);
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(215,21);
			this.comboCategory.TabIndex = 39;
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox.Location = new System.Drawing.Point(165,59);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(20,20);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 40;
			this.pictureBox.TabStop = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(40,63);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(124,13);
			this.label6.TabIndex = 41;
			this.label6.Text = "Image (20x20)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butImport
			// 
			this.butImport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImport.Autosize = true;
			this.butImport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImport.CornerRadius = 4F;
			this.butImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butImport.Location = new System.Drawing.Point(272,56);
			this.butImport.Name = "butImport";
			this.butImport.Size = new System.Drawing.Size(75,26);
			this.butImport.TabIndex = 42;
			this.butImport.Text = "Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// butClear
			// 
			this.butClear.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClear.Autosize = true;
			this.butClear.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClear.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClear.CornerRadius = 4F;
			this.butClear.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butClear.Location = new System.Drawing.Point(191,56);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(75,26);
			this.butClear.TabIndex = 43;
			this.butClear.Text = "Clear";
			this.butClear.Click += new System.EventHandler(this.butClear_Click);
			// 
			// listView
			// 
			this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView.Location = new System.Drawing.Point(539,56);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(292,141);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 44;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.SmallIcon;
			this.listView.ItemActivate += new System.EventHandler(this.listView_ItemActivate);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0,"procButtonAmalgam.gif");
			this.imageList.Images.SetKeyName(1,"procButtonComp.gif");
			this.imageList.Images.SetKeyName(2,"procButtonCrown.gif");
			this.imageList.Images.SetKeyName(3,"procButtonExtract.gif");
			this.imageList.Images.SetKeyName(4,"procButtonPA.gif");
			this.imageList.Images.SetKeyName(5,"procButtonRCT.gif");
			this.imageList.Images.SetKeyName(6,"procButtonRCTbuPFM.gif");
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(353,60);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(180,19);
			this.label7.TabIndex = 45;
			this.label7.Text = "Or pick an image from this list";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormProcButtonEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(848,595);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.butImport);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.comboCategory);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listADA);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.listAutoCodes);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProcButtonEdit";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Procedure Button";
			this.Load += new System.EventHandler(this.FormChartProcedureEntryEdit_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormChartProcedureEntryEdit_Load(object sender, System.EventArgs e) {
      AutoCodes.Refresh(); 
      ProcButtonItems.Refresh();     
      if(IsNew){
        this.Text=Lan.g(this,"Add Procedure Button");
      }
      else{
        this.Text=Lan.g(this,"Edit Procedure Button");
      }
			textDescript.Text=ProcButtonCur.Description;
			for(int i=0;i<DefB.Short[(int)DefCat.ProcButtonCats].Length;i++){
				comboCategory.Items.Add(DefB.Short[(int)DefCat.ProcButtonCats][i].ItemName);
				if(ProcButtonCur.Category==DefB.Short[(int)DefCat.ProcButtonCats][i].DefNum){
					comboCategory.SelectedIndex=i;
				}
			}
			if(comboCategory.SelectedIndex==-1){
				comboCategory.SelectedIndex=0;//we know that there will always be at least one cat. Validated in FormProcButtons
			}
			pictureBox.Image=ProcButtonCur.ButtonImage;
			int[] codeNumList=ProcButtonItems.GetCodeNumListForButton(ProcButtonCur.ProcButtonNum);
			int[] auto=ProcButtonItems.GetAutoListForButton(ProcButtonCur.ProcButtonNum);
			listADA.Items.Clear();
			for(int i=0;i<codeNumList.Length;i++) {
				listADA.Items.Add(ProcedureCodes.GetStringProcCode(codeNumList[i]));
			}
			listAutoCodes.Items.Clear();
      for(int i=0;i<AutoCodes.ListShort.Length;i++){
        listAutoCodes.Items.Add(AutoCodes.ListShort[i].Description);
				for(int j=0;j<auto.Length;j++){
					if(auto[j]==AutoCodes.ListShort[i].AutoCodeNum){
						listAutoCodes.SetSelected(i,true);
						break;
					}
				}
      }
			//fill images to pick from
			for(int i=0;i<imageList.Images.Count;i++){
				listView.Items.Add("",i);
			}
		}

		private void butImport_Click(object sender,EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			if(dlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			try{
				pictureBox.Image=Image.FromFile(dlg.FileName);
			}
			catch{
				MsgBox.Show(this,"Error loading file.");
			}
		}

		private void listView_ItemActivate(object sender,EventArgs e) {
			if(listView.SelectedIndices.Count==0){
				return;
			}
			pictureBox.Image=imageList.Images[listView.SelectedIndices[0]];
			listView.SelectedIndices.Clear();
		}

		private void butClear_Click(object sender,EventArgs e) {
			pictureBox.Image=null;
		} 

		private void butAdd_Click(object sender, System.EventArgs e) {
		  FormProcCodes FormP=new FormProcCodes();
      FormP.IsSelectionMode=true;
      FormP.ShowDialog();
      if(FormP.DialogResult!=DialogResult.Cancel){
        listADA.Items.Add(ProcedureCodes.GetStringProcCode(FormP.SelectedCodeNum));  
      } 
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
      if(listADA.SelectedIndex < 0){
				MessageBox.Show(Lan.g(this,"Please select an item first."));
				return;
      }
      listADA.Items.RemoveAt(listADA.SelectedIndex);        
		}

	 	private void butOK_Click(object sender, System.EventArgs e) {
      if(textDescript.Text==""){
				MessageBox.Show(Lan.g(this,"You must type in a description."));
				return; 
      }
			if(listADA.Items.Count==0  && listAutoCodes.SelectedIndices.Count==0){
        MessageBox.Show(Lan.g(this,"You must pick at least one Auto Code or Procedure Code."));
        return;
      }
      ProcButtonCur.Description=textDescript.Text;
			if(ProcButtonCur.Category != DefB.Short[(int)DefCat.ProcButtonCats][comboCategory.SelectedIndex].DefNum){
				//This will put it at the end of the order in the new category
				ProcButtonCur.ItemOrder
					=ProcButtons.GetForCat(DefB.Short[(int)DefCat.ProcButtonCats][comboCategory.SelectedIndex].DefNum).Length;
			}
			ProcButtonCur.Category=DefB.Short[(int)DefCat.ProcButtonCats][comboCategory.SelectedIndex].DefNum;
			ProcButtonCur.ButtonImage=(Bitmap)pictureBox.Image;
      if(IsNew){
        ProcButtonCur.ItemOrder=ProcButtons.List.Length;        
        ProcButtons.Insert(ProcButtonCur);
      }
      else{
        ProcButtons.Update(ProcButtonCur);
      }
      ProcButtonItems.DeleteAllForButton(ProcButtonCur.ProcButtonNum);
			ProcButtonItem item;
      for(int i=0;i<listADA.Items.Count;i++){
        item=new ProcButtonItem();
        item.ProcButtonNum=ProcButtonCur.ProcButtonNum;
        item.CodeNum=ProcedureCodes.GetCodeNum(listADA.Items[i].ToString());    
        ProcButtonItems.Insert(item);
      }
      for(int i=0;i<listAutoCodes.SelectedIndices.Count;i++){
        item=new ProcButtonItem();
        item.ProcButtonNum=ProcButtonCur.ProcButtonNum;
        item.AutoCodeNum=AutoCodes.ListShort[listAutoCodes.SelectedIndices[i]].AutoCodeNum;
        ProcButtonItems.Insert(item);
      }
      DialogResult=DialogResult.OK;    
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
   
	}
}
