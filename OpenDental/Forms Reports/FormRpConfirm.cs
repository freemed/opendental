using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenDental{
///<summary></summary>
	public class FormRpConfirm : System.Windows.Forms.Form{
		private System.Windows.Forms.Label labelPatient;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.ComponentModel.Container components = null;
		private ArrayList ALSelect;
		private ArrayList ALSelect2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listSelect;
		private System.Windows.Forms.ListBox listSelect2; 
    private int[] AptNums;
  
		///<summary></summary>
		public FormRpConfirm(int[] aptNums){
			InitializeComponent();
      AptNums=(int[])aptNums.Clone();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpConfirm));
			this.labelPatient = new System.Windows.Forms.Label();
			this.listSelect = new System.Windows.Forms.ListBox();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listSelect2 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// labelPatient
			// 
			this.labelPatient.Location = new System.Drawing.Point(12,16);
			this.labelPatient.Name = "labelPatient";
			this.labelPatient.Size = new System.Drawing.Size(170,14);
			this.labelPatient.TabIndex = 6;
			this.labelPatient.Text = "Standard Fields";
			this.labelPatient.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listSelect
			// 
			this.listSelect.Location = new System.Drawing.Point(12,30);
			this.listSelect.Name = "listSelect";
			this.listSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listSelect.Size = new System.Drawing.Size(170,355);
			this.listSelect.TabIndex = 5;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(434,362);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 8;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(434,326);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 7;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(208,16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170,14);
			this.label1.TabIndex = 10;
			this.label1.Text = "Other Available Fields";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listSelect2
			// 
			this.listSelect2.Location = new System.Drawing.Point(208,30);
			this.listSelect2.Name = "listSelect2";
			this.listSelect2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listSelect2.Size = new System.Drawing.Size(170,355);
			this.listSelect2.TabIndex = 9;
			// 
			// FormRpConfirm
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(538,408);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listSelect2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.labelPatient);
			this.Controls.Add(this.listSelect);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpConfirm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Confirmation Report";
			this.Load += new System.EventHandler(this.FormRpConfirm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRpConfirm_Load(object sender, System.EventArgs e){
			ALSelect=new ArrayList();
			ALSelect.Add(Lan.g(this,"LName"));
			ALSelect.Add(Lan.g(this,"FName")); 
			ALSelect.Add(Lan.g(this,"MiddleI"));
      ALSelect.Add(Lan.g(this,"Preferred"));
			ALSelect.Add(Lan.g(this,"Salutation")); 
      ALSelect.Add(Lan.g(this,"Address")); 
      ALSelect.Add(Lan.g(this,"Address2"));
			ALSelect.Add(Lan.g(this,"City")); 
			ALSelect.Add(Lan.g(this,"State"));
			ALSelect.Add(Lan.g(this,"Zip"));
			ALSelect.Add(Lan.g(this,"HmPhone"));
			ALSelect.Add(Lan.g(this,"WkPhone")); 
			ALSelect.Add(Lan.g(this,"WirelessPhone")); 
			ALSelect.Add(Lan.g(this,"Birthdate"));
			ALSelect.Add(Lan.g(this,"AptDateTime"));
			ALSelect2=new ArrayList();
      ALSelect2.Add(Lan.g(this,"BillingType"));      
      ALSelect2.Add(Lan.g(this,"CreditType"));
			ALSelect2.Add(Lan.g(this,"SSN"));  
			ALSelect2.Add(Lan.g(this,"ChartNumber"));   
			ALSelect2.Add(Lan.g(this,"FeeSched"));
			ALSelect2.Add(Lan.g(this,"Position")); 
			ALSelect2.Add(Lan.g(this,"Gender"));
			ALSelect2.Add(Lan.g(this,"PatStatus"));
			ALSelect2.Add(Lan.g(this,"PatNum")); 
      ALSelect2.Add(Lan.g(this,"Email"));
      ALSelect2.Add(Lan.g(this,"EstBalance")); 
      ALSelect2.Add(Lan.g(this,"AddrNote")); 
      ALSelect2.Add(Lan.g(this,"FamFinUrgNote")); 
      ALSelect2.Add(Lan.g(this,"MedUrgNote")); 
			ALSelect2.Add(Lan.g(this,"ApptModNote"));
      ALSelect2.Add(Lan.g(this,"PriPlanNum"));//Primary Carrier?
      ALSelect2.Add(Lan.g(this,"PriRelationship"));// ?
			ALSelect2.Add(Lan.g(this,"SecPlanNum"));//Secondary Carrier? 
			ALSelect2.Add(Lan.g(this,"SecRelationship"));// ?
      ALSelect2.Add(Lan.g(this,"RecallInterval")); 
			ALSelect2.Add(Lan.g(this,"StudentStatus"));
      ALSelect2.Add(Lan.g(this,"SchoolName")); 
			ALSelect2.Add(Lan.g(this,"PriProv")); 
      ALSelect2.Add(Lan.g(this,"SecProv"));
			ALSelect2.Add(Lan.g(this,"NextAptNum")); 
			ALSelect2.Add(Lan.g(this,"Guarantor")); 
			ALSelect2.Add(Lan.g(this,"ImageFolder"));
			for(int i=0;i<ALSelect.Count;i++){
				listSelect.Items.Add(ALSelect[i]);
				listSelect.SetSelected(i,true);
			}
			for(int i=0;i<ALSelect2.Count;i++){
				listSelect2.Items.Add(ALSelect2[i]);
			}
 		}

		private void butOK_Click(object sender, System.EventArgs e) {
			string[] fieldsSelected=new string[listSelect.SelectedItems.Count+listSelect2.SelectedItems.Count]; 
			if(listSelect.SelectedItems.Count==0 && listSelect2.SelectedItems.Count==0){
				MsgBox.Show(this,"At least one field must be selected.");
				return;
			}
			listSelect.SelectedItems.CopyTo(fieldsSelected,0);
			listSelect2.SelectedItems.CopyTo(fieldsSelected,listSelect.SelectedItems.Count);
			string command="SELECT ";
			for(int i=0;i<fieldsSelected.Length;i++){
				if(i>0){
					command+=",";
				}
				if(fieldsSelected[i]=="AptDateTime"){
					command+="appointment."+fieldsSelected[i];
				}
				else{
					command+="patient."+fieldsSelected[i];
				}
			}
			command+=" FROM patient,appointment "
				+"WHERE patient.PatNum=appointment.PatNum AND(";       
			for(int i=0;i<AptNums.Length;i++){
				if(i>0){ 
					command+=" OR";
				}
				command+=" appointment.AptNum='"+AptNums[i]+"'";
			}
			command+=")";
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query=command;
			FormQuery FormQ=new FormQuery();
			FormQ.IsReport=false;
			FormQ.SubmitQuery();	
      FormQ.textQuery.Text=Queries.CurReport.Query;					
			FormQ.ShowDialog();		
      DialogResult=DialogResult.OK; 
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	}
}
