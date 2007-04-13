using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary></summary>
	public class FormClaimSupplemental : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butNone;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.ListBox listRef;
		private System.Windows.Forms.TextBox textRefNum;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox textAccidentST;
		private OpenDental.ValidDate textAccidentDate;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ListBox listPlaceService;
		private System.Windows.Forms.ListBox listEmployRelated;
		private System.Windows.Forms.ListBox listAccident;
		private System.Windows.Forms.Label label10;
		//public string RefNumString;
		///<summary>Set this externally before opening claim.</summary>
		public Claim ClaimCur;
	
		///<summary></summary>
		public FormClaimSupplemental(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClaimSupplemental));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listRef = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textRefNum = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butNone = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.label25 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label24 = new System.Windows.Forms.Label();
			this.textAccidentST = new System.Windows.Forms.TextBox();
			this.textAccidentDate = new OpenDental.ValidDate();
			this.label23 = new System.Windows.Forms.Label();
			this.listAccident = new System.Windows.Forms.ListBox();
			this.label10 = new System.Windows.Forms.Label();
			this.listPlaceService = new System.Windows.Forms.ListBox();
			this.listEmployRelated = new System.Windows.Forms.ListBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			resources.ApplyResources(this.butCancel,"butCancel");
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Name = "butCancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			resources.ApplyResources(this.butOK,"butOK");
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Name = "butOK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listRef
			// 
			resources.ApplyResources(this.listRef,"listRef");
			this.listRef.Name = "listRef";
			this.listRef.DoubleClick += new System.EventHandler(this.listRef_DoubleClick);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1,"label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2,"label2");
			this.label2.Name = "label2";
			// 
			// textRefNum
			// 
			resources.ApplyResources(this.textRefNum,"textRefNum");
			this.textRefNum.Name = "textRefNum";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3,"label3");
			this.label3.Name = "label3";
			// 
			// butNone
			// 
			this.butNone.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butNone.Autosize = true;
			this.butNone.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butNone.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butNone.CornerRadius = 4F;
			resources.ApplyResources(this.butNone,"butNone");
			this.butNone.Name = "butNone";
			this.butNone.Click += new System.EventHandler(this.butNone_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			resources.ApplyResources(this.butAdd,"butAdd");
			this.butAdd.Name = "butAdd";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label25
			// 
			resources.ApplyResources(this.label25,"label25");
			this.label25.Name = "label25";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.textAccidentST);
			this.groupBox2.Controls.Add(this.textAccidentDate);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.listAccident);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			resources.ApplyResources(this.groupBox2,"groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// label24
			// 
			resources.ApplyResources(this.label24,"label24");
			this.label24.Name = "label24";
			// 
			// textAccidentST
			// 
			resources.ApplyResources(this.textAccidentST,"textAccidentST");
			this.textAccidentST.Name = "textAccidentST";
			// 
			// textAccidentDate
			// 
			resources.ApplyResources(this.textAccidentDate,"textAccidentDate");
			this.textAccidentDate.Name = "textAccidentDate";
			// 
			// label23
			// 
			resources.ApplyResources(this.label23,"label23");
			this.label23.Name = "label23";
			// 
			// listAccident
			// 
			resources.ApplyResources(this.listAccident,"listAccident");
			this.listAccident.Name = "listAccident";
			// 
			// label10
			// 
			resources.ApplyResources(this.label10,"label10");
			this.label10.Name = "label10";
			// 
			// listPlaceService
			// 
			resources.ApplyResources(this.listPlaceService,"listPlaceService");
			this.listPlaceService.Name = "listPlaceService";
			// 
			// listEmployRelated
			// 
			resources.ApplyResources(this.listEmployRelated,"listEmployRelated");
			this.listEmployRelated.Name = "listEmployRelated";
			// 
			// FormClaimSupplemental
			// 
			this.AcceptButton = this.butOK;
			resources.ApplyResources(this,"$this");
			this.CancelButton = this.butCancel;
			this.Controls.Add(this.listEmployRelated);
			this.Controls.Add(this.listPlaceService);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butNone);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textRefNum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listRef);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.groupBox2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClaimSupplemental";
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.FormClaimSupplemental_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormClaimSupplemental_Load(object sender, System.EventArgs e) {
			textRefNum.Text=ClaimCur.RefNumString;
			string[] enumPlaceOfService=Enum.GetNames(typeof(PlaceOfService));
			for(int i=0;i<enumPlaceOfService.Length;i++){;
				listPlaceService.Items.Add(Lan.g("enumPlaceOfService",enumPlaceOfService[i]));
			}
			listPlaceService.SelectedIndex=(int)ClaimCur.PlaceService;
			string[] enumYN=Enum.GetNames(typeof(YN));
			for(int i=0;i<enumYN.Length;i++){;
				listEmployRelated.Items.Add(Lan.g("enumYN",enumYN[i]));
			}
			listEmployRelated.SelectedIndex=(int)ClaimCur.EmployRelated;
			listAccident.Items.Add(Lan.g(this,"No"));
			listAccident.Items.Add(Lan.g(this,"Auto"));
			listAccident.Items.Add(Lan.g(this,"Employment"));
			listAccident.Items.Add(Lan.g(this,"Other"));
			switch(ClaimCur.AccidentRelated){
				case "":
					listAccident.SelectedIndex=0;
					break;
				case "A":
					listAccident.SelectedIndex=1;
					break;
				case "E":
					listAccident.SelectedIndex=2;
					break;
				case "O":
					listAccident.SelectedIndex=3;
					break;
			}
			if(ClaimCur.AccidentDate.Year<1880){
				textAccidentDate.Text="";
			}
			else{
				textAccidentDate.Text=ClaimCur.AccidentDate.ToShortDateString();
			}
			textAccidentST.Text=ClaimCur.AccidentST;
			FillRefs();
		}

		private void FillRefs(){
			Referrals.Refresh();
			listRef.Items.Clear();
			for(int i=0;i<Referrals.List.Length;i++){
				listRef.Items.Add(Referrals.List[i].LName+", "+Referrals.List[i].FName);
				if(ClaimCur.ReferringProv==Referrals.List[i].ReferralNum){
					listRef.SelectedIndex=i;
				}
			}
		}

		private void butNone_Click(object sender, System.EventArgs e) {
			listRef.SelectedIndex=-1;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Referral refCur=new Referral();
			//this will never be a patient; always a dentist
			FormReferralEdit FormREdit=new FormReferralEdit(refCur);
			FormREdit.IsNew=true;
			FormREdit.ShowDialog();
			FillRefs();
		}

		private void listRef_DoubleClick(object sender, System.EventArgs e) {
			FormReferralEdit FormREdit=new FormReferralEdit(Referrals.List[listRef.SelectedIndex]);
			FormREdit.ShowDialog();
			FillRefs();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textAccidentDate.errorProvider1.GetError(textAccidentDate)!=""
				//|| textDateRec.errorProvider1.GetError(textDateRec)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			ClaimCur.RefNumString=textRefNum.Text;
			if(listRef.SelectedIndex==-1)
				ClaimCur.ReferringProv=0;
			else
				ClaimCur.ReferringProv=Referrals.List[listRef.SelectedIndex].ReferralNum;
			ClaimCur.PlaceService=(PlaceOfService)listPlaceService.SelectedIndex;
			ClaimCur.EmployRelated=(YN)listEmployRelated.SelectedIndex;
			switch(listAccident.SelectedIndex){
				case 0:
					ClaimCur.AccidentRelated="";
					break;
				case 1:
					ClaimCur.AccidentRelated="A";
					break;
				case 2:
					ClaimCur.AccidentRelated="E";
					break;
				case 3:
					ClaimCur.AccidentRelated="O";
					break;
			}
			ClaimCur.AccidentDate=PIn.PDate(textAccidentDate.Text);
			ClaimCur.AccidentST=textAccidentST.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}
