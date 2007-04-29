using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormAutoItemEdit : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textADA;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listConditions;
		private OpenDental.UI.Button butChange;
		///<summary></summary>
    public bool IsNew;
		///<summary>Set this value externally before opening this form, even if IsNew.</summary>
		public AutoCodeItem AutoCodeItemCur;

		///<summary></summary>
		public FormAutoItemEdit(){
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

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoItemEdit));
			this.textADA = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listConditions = new System.Windows.Forms.ListBox();
			this.butChange = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textADA
			// 
			this.textADA.Location = new System.Drawing.Point(108,54);
			this.textADA.Name = "textADA";
			this.textADA.ReadOnly = true;
			this.textADA.Size = new System.Drawing.Size(100,20);
			this.textADA.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96,12);
			this.label1.TabIndex = 1;
			this.label1.Text = "Code";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listConditions
			// 
			this.listConditions.Location = new System.Drawing.Point(334,56);
			this.listConditions.Name = "listConditions";
			this.listConditions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listConditions.Size = new System.Drawing.Size(166,407);
			this.listConditions.TabIndex = 2;
			// 
			// butChange
			// 
			this.butChange.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butChange.Autosize = true;
			this.butChange.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butChange.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butChange.CornerRadius = 4F;
			this.butChange.Location = new System.Drawing.Point(214,50);
			this.butChange.Name = "butChange";
			this.butChange.Size = new System.Drawing.Size(76,25);
			this.butChange.TabIndex = 24;
			this.butChange.Text = "C&hange";
			this.butChange.Click += new System.EventHandler(this.butChange_Click);
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
			this.butCancel.Location = new System.Drawing.Point(540,442);
			this.butCancel.Name = "butCancel";
			this.butCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 23;
			this.butCancel.Text = "&Cancel";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(540,408);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 22;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(356,40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118,14);
			this.label2.TabIndex = 25;
			this.label2.Text = "Conditions";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// FormAutoItemEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(644,490);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butChange);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.listConditions);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textADA);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoItemEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit AutoItem";
			this.Load += new System.EventHandler(this.FormAutoItemEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

 		private void FormAutoItemEdit_Load(object sender, System.EventArgs e) { 
      AutoCodeConds.Refresh();    
			if(IsNew){
				this.Text=Lan.g(this,"Add Auto Code Item");  
			}
			else{ 
				this.Text=Lan.g(this,"Edit Auto Code Item");
				textADA.Text=ProcedureCodes.GetStringProcCode(AutoCodeItemCur.CodeNum);    
			}
			FillList();
		}

    private void FillList(){
      listConditions.Items.Clear();
      foreach(string s in Enum.GetNames(typeof(AutoCondition))){
         listConditions.Items.Add(Lan.g("enumAutoConditions",s));
      }  
			for(int i=0;i<AutoCodeConds.List.Length;i++){
        if(AutoCodeConds.List[i].AutoCodeItemNum==AutoCodeItemCur.AutoCodeItemNum){
          listConditions.SetSelected((int)AutoCodeConds.List[i].Cond,true);
        }   
      }
    } 

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textADA.Text==""){
			  MessageBox.Show(Lan.g(this,"Code cannot be left blank."));
        listConditions.SelectedIndex=-1;
				FillList();
				return;
      }
      AutoCodeItemCur.CodeNum=ProcedureCodes.GetCodeNum(textADA.Text);
      if(IsNew){
        AutoCodeItems.Insert(AutoCodeItemCur);
      }
      else{
        AutoCodeItems.Update(AutoCodeItemCur);
      } 
      AutoCodeConds.DeleteForItemNum(AutoCodeItemCur.AutoCodeItemNum);
      for(int i=0;i<listConditions.SelectedIndices.Count;i++){
        AutoCodeCond AutoCodeCondCur=new AutoCodeCond();
        AutoCodeCondCur.AutoCodeItemNum=AutoCodeItemCur.AutoCodeItemNum;
        AutoCodeCondCur.Cond=(AutoCondition)listConditions.SelectedIndices[i];
        AutoCodeConds.Insert(AutoCodeCondCur); 
      }
      DialogResult=DialogResult.OK;
		}

		private void butChange_Click(object sender, System.EventArgs e) {
			FormProcCodes FormP=new FormProcCodes();
      FormP.IsSelectionMode=true;
      FormP.ShowDialog();
      if(FormP.DialogResult==DialogResult.Cancel){
        textADA.Text=ProcedureCodes.GetStringProcCode(AutoCodeItemCur.CodeNum);
				return;
      }
			if(AutoCodeItems.HList.ContainsKey(FormP.SelectedCodeNum)
				&& (int)AutoCodeItems.HList[FormP.SelectedCodeNum] != AutoCodeItemCur.AutoCodeNum)
			{
				//This section is a fix for an old bug that did not cause items to get deleted properly
				if(!AutoCodes.HList.ContainsKey((int)AutoCodeItems.HList[FormP.SelectedCodeNum])){
					AutoCodeItems.Delete((int)AutoCodeItems.HList[FormP.SelectedCodeNum]);
					textADA.Text=ProcedureCodes.GetStringProcCode(FormP.SelectedCodeNum);
				}
				else{
					MessageBox.Show(Lan.g(this,"That procedure code is already in use in a different Auto Code.  Not allowed to use it here."));
					textADA.Text=ProcedureCodes.GetStringProcCode(AutoCodeItemCur.CodeNum);
				}
			}
			else{
				textADA.Text=ProcedureCodes.GetStringProcCode(FormP.SelectedCodeNum);
			}
		}

	}
}










