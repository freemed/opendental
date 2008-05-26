using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormReferralSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.TableRefSelect tbRefSelect;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsSelectionMode;
		private OpenDental.UI.Button butEdit;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butAdd;//disables double click to choose referral. Hides some buttons.
    private ArrayList AList;
		///<summary>This will contain the referral that was selected.</summary>
		public Referral SelectedReferral;

		///<summary></summary>
		public FormReferralSelect(){
			InitializeComponent();
			tbRefSelect.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbRefSelect_CellDoubleClicked);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReferralSelect));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.tbRefSelect = new OpenDental.TableRefSelect();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.butEdit = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.SuspendLayout();
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
			this.butCancel.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			this.butCancel.Location = new System.Drawing.Point(872,646);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(82,26);
			this.butCancel.TabIndex = 6;
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
			this.butOK.Location = new System.Drawing.Point(872,614);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(82,26);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// tbRefSelect
			// 
			this.tbRefSelect.BackColor = System.Drawing.SystemColors.Window;
			this.tbRefSelect.Location = new System.Drawing.Point(8,6);
			this.tbRefSelect.Name = "tbRefSelect";
			this.tbRefSelect.ScrollValue = 1;
			this.tbRefSelect.SelectedIndices = new int[0];
			this.tbRefSelect.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.tbRefSelect.Size = new System.Drawing.Size(829,602);
			this.tbRefSelect.TabIndex = 7;
			// 
			// checkHidden
			// 
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(844,22);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.Size = new System.Drawing.Size(104,24);
			this.checkHidden.TabIndex = 11;
			this.checkHidden.Text = "Show Hidden  ";
			this.checkHidden.Click += new System.EventHandler(this.checkHidden_Click);
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.CornerRadius = 4F;
			this.butEdit.Image = global::OpenDental.Properties.Resources.editPencil;
			this.butEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butEdit.Location = new System.Drawing.Point(872,496);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(80,26);
			this.butEdit.TabIndex = 14;
			this.butEdit.Text = "&Edit";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
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
			this.butDelete.Location = new System.Drawing.Point(872,464);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(80,26);
			this.butDelete.TabIndex = 13;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
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
			this.butAdd.Location = new System.Drawing.Point(872,430);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(80,26);
			this.butAdd.TabIndex = 12;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// FormReferralSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,696);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.tbRefSelect);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormReferralSelect";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Referrals";
			this.Load += new System.EventHandler(this.FormReferralSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormReferralSelect_Load(object sender, System.EventArgs e) {
			FillTable();
		}		

    private void FillTable(){
      Referrals.Refresh();
      AList=new ArrayList();
      if(!checkHidden.Checked){
        for(int i=0;i<Referrals.List.Length;i++){
          if(!Referrals.List[i].IsHidden){
            AList.Add(Referrals.List[i]);
          }
        } 
      }
      else{
        for(int i=0;i<Referrals.List.Length;i++){
            AList.Add(Referrals.List[i]);
        } 
      }          
			tbRefSelect.ResetRows(AList.Count);
			tbRefSelect.SetGridColor(Color.Gray);
			tbRefSelect.SetBackGColor(Color.White);      
			for(int i=0;i<AList.Count;i++){
				tbRefSelect.Cell[0,i]=((Referral)(AList[i])).LName;
        tbRefSelect.Cell[1,i]=((Referral)(AList[i])).FName;
        if(((Referral)(AList[i])).MName!=""){
          tbRefSelect.Cell[2,i]=((Referral)(AList[i])).MName.Substring(0,1).ToUpper();
        } 
				tbRefSelect.Cell[3,i]=((Referral)(AList[i])).Title;
        if(((Referral)(AList[i])).PatNum==0 && !((Referral)(AList[i])).NotPerson){
          tbRefSelect.Cell[4,i]
						=Lan.g("enumDentalSpecialty",((DentalSpecialty)(((Referral)(AList[i])).Specialty)).ToString());
        }
				if(((Referral)(AList[i])).PatNum>0)
					tbRefSelect.Cell[5,i]="X";
				else
					tbRefSelect.Cell[5,i]="";
        tbRefSelect.Cell[6,i]=((Referral)(AList[i])).Note;
				if(((Referral)(AList[i])).IsHidden)
					tbRefSelect.SetTextColorRow(i,SystemColors.GrayText);
			}
			tbRefSelect.LayoutTables();  
      //if(tbRefSelect.SelectedRow!=-1){ 
      //  tbRefSelect.ColorRow(tbRefSelect.SelectedRow,Color.Silver);
      //} 
    }

    private void tbRefSelect_CellDoubleClicked(object sender, CellEventArgs e){
			if(IsSelectionMode){
				SelectedReferral=(Referral)AList[tbRefSelect.SelectedRow];
				DialogResult=DialogResult.OK;
			}
			//otherwise, don't do anything
    }

		private void butAdd_Click(object sender, System.EventArgs e) {
      Referral refCur=new Referral();
			bool referralIsNew=true;
			if(MessageBox.Show(Lan.g(this,"Is the referral source an existing patient?"),""
				,MessageBoxButtons.YesNo)==DialogResult.Yes)
			{
				FormPatientSelect FormPS=new FormPatientSelect();
				FormPS.SelectionModeOnly=true;        
				FormPS.ShowDialog();
				if(FormPS.DialogResult!=DialogResult.OK){
					return;  
				}
				refCur.PatNum=FormPS.SelectedPatNum;
				for(int i=0;i<Referrals.List.Length;i++){
					if(Referrals.List[i].PatNum==FormPS.SelectedPatNum){//referral already existed
						refCur=Referrals.List[i];
						referralIsNew=false;
						break;
					}
				}
			}
			FormReferralEdit FormRE2=new FormReferralEdit(refCur);//the ReferralNum must be added here
			FormRE2.IsNew=referralIsNew; 
      FormRE2.ShowDialog();
			if(FormRE2.DialogResult==DialogResult.Cancel){
				return;
			}
			if(IsSelectionMode){
				SelectedReferral=FormRE2.RefCur;
				DialogResult=DialogResult.OK;
			}
			else{
				tbRefSelect.SelectedRow=-1;
				FillTable();
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
      if(tbRefSelect.SelectedRow==-1){ 
        return; 
      }  
 		  Referral RefCur=(Referral)AList[tbRefSelect.SelectedRow];
			if(RefAttaches.IsReferralAttached(RefCur.ReferralNum)){
				MessageBox.Show(Lan.g(this,"Cannot delete Referral because it is attached to patients"));
				return;
			}
      if(!MsgBox.Show(this,true,"Delete Referral?")){
        return;   
      }
      Referrals.Delete(RefCur);
      tbRefSelect.SelectedRow=-1;
      FillTable();
    }

		private void butEdit_Click(object sender, System.EventArgs e) {
      if(tbRefSelect.SelectedRow==-1){
        return;  
      }
  		FormReferralEdit FormRE=new FormReferralEdit((Referral)AList[tbRefSelect.SelectedRow]);
      FormRE.ShowDialog();
      FillTable();
		}

		private void checkHidden_Click(object sender, System.EventArgs e) {
      tbRefSelect.SelectedRow=-1; 
		  FillTable();
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(IsSelectionMode){
				if(tbRefSelect.SelectedRow==-1){
					MsgBox.Show(this,"Please select a referral first");
          return;
        }
				SelectedReferral=(Referral)AList[tbRefSelect.SelectedRow];  
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
